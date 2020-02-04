using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Const;
using Calculator.Models;
using Calculator.Models.Operators;
using Calculator.Resolvers;

namespace Calculator
{
    public class ExpressionCalculator <TResult>
    {
        public IExpressionResolver Resolver { get; }

        public ExpressionCalculator(IExpressionResolver resolver = null)
        {
            Resolver = resolver;
        }

        public TResult Calculate (string incomingExpression)
        {
            if (Resolver == null) throw new EmptyResolverException();

            var expressionElements = Resolver.Parse(incomingExpression).Where(el => el.Type != ExpressionElementTypes.Separator).ToList();
            ValidateExpressionElements(expressionElements);
            if (!expressionElements.Any()) return default(TResult);

            //Обратная польская запись
            var resultBuffer = new Queue<IExpressionElement>();
            var operandBuffer = new Stack<IExpressionOperation>();

            IExpressionElement prevElement = null;
            foreach (var element in expressionElements)
            {
                if (element.Type == ExpressionElementTypes.Operand)
                    resultBuffer.Enqueue(element);

                if (element.Type == ExpressionElementTypes.Operator)
                {
                    var currentOperator = element as IExpressionOperator;

                    if (currentOperator.OperationType == OperationType.Any && currentOperator is ICanCahngeOperationType
                        && (prevElement == null || (prevElement is IExpressionBracket && (prevElement as IExpressionBracket).BracketSign == BracketSign.Open)))
                        (currentOperator as ICanCahngeOperationType).SetOperationType(OperationType.Unary);

                    while (operandBuffer.Count > 0 && operandBuffer.Peek().Priority >= currentOperator.Priority)
                        resultBuffer.Enqueue(operandBuffer.Pop());

                    operandBuffer.Push(currentOperator);
                }

                if (element.Type == ExpressionElementTypes.Bracket)
                {
                    var currentBracket = element as IExpressionBracket;

                    if (currentBracket.BracketSign == BracketSign.Open)
                        operandBuffer.Push(currentBracket);
                    else
                    {
                        var foundOpenBracketFlag = false;
                        while (operandBuffer.Count > 0 && !foundOpenBracketFlag)
                        {
                            var operandBufferPopElement = operandBuffer.Pop();

                            if (!(currentBracket.Equals(operandBufferPopElement)
                                    && (operandBufferPopElement as IExpressionBracket).BracketSign == BracketSign.Open))
                                resultBuffer.Enqueue(operandBufferPopElement);
                            else foundOpenBracketFlag = true;
                        }
                        if (!foundOpenBracketFlag)
                            throw new InvalidExpressionException("Неправильно расставлены скобки");
                    }
                }
                prevElement = element;
            }

            while (operandBuffer.Count > 0)
                resultBuffer.Enqueue(operandBuffer.Pop());


            var calcBuffer = new Stack<TResult>();
            while (resultBuffer.Count > 0)
            {
                var el = resultBuffer.Dequeue();

                if (el.Type == ExpressionElementTypes.Bracket)
                    throw new InvalidExpressionException("Неправильно расставлены скобки");

                if (el.Type == ExpressionElementTypes.Operand)
                    calcBuffer.Push((el as IExpressionOperand<TResult>).Value);

                if (el.Type == ExpressionElementTypes.Operator)
                {
                    var operation = el as IExpressionOperator<TResult>;

                    TResult operand1, operand2;
                    try {
                        operand2 = calcBuffer.Pop();
                        operand1 = operation.OperationType != OperationType.Unary ? calcBuffer.Pop() : default(TResult);
                    } catch (InvalidOperationException) {
                        throw new InvalidExpressionException("Неверный формат выражения");
                    }

                    calcBuffer.Push(operation.Calculate(operand1, operand2));
                }
            }

            if (calcBuffer.Count != 1)
                throw new InvalidExpressionException("Неверный формат выражения");

            return calcBuffer.Pop();

        }

        private void ValidateExpressionElements(IEnumerable<IExpressionElement> elements)
        {
            var SupportedElementTypes = new Dictionary<ExpressionElementTypes, Type> {
                { ExpressionElementTypes.Separator, typeof(IExpressionSeparator) },
                { ExpressionElementTypes.Bracket, typeof(IExpressionBracket) },
                { ExpressionElementTypes.Operand, typeof(IExpressionOperand<TResult>) },
                { ExpressionElementTypes.Operator, typeof(IExpressionOperator<TResult>) }
            };

            var error = elements.Select(element => {
                if (!SupportedElementTypes.Keys.Contains(element.Type))
                    return $"\"{Enum.GetName(typeof(ExpressionElementTypes), element.Type)}\" — неподдерживаемый тип элементов";

                if (!SupportedElementTypes[element.Type].IsInstanceOfType(element))
                    return $"Элементы с типом \"{Enum.GetName(typeof(ExpressionElementTypes), element.Type)}\" должны реализовывать интерфейс {SupportedElementTypes[element.Type].Name}";

                if (element.Type == ExpressionElementTypes.Operator 
                    && ((IExpressionOperator)element).OperationType == OperationType.Any
                    && !typeof(ICanCahngeOperationType).IsInstanceOfType(element))
                    return $"Операции с типом \"OperationType.Any\" должны реализовывать интерфейс { typeof(ICanCahngeOperationType).Name }";

                return string.Empty;

            }).Where(msg => !string.IsNullOrEmpty(msg)).FirstOrDefault();

            if (!string.IsNullOrEmpty(error))
                throw new UnsupportedElementTypeException(error);
        }
    }
}
