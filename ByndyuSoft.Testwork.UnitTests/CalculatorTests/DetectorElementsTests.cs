using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Calculator.Detector.Separators;
using Calculator.Detectors.Brackets;
using Calculator.Detectors.Operands;
using Calculator.Detectors.Operators;
using Calculator.Models;
using Calculator.Models.Brackets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ByndyuSoft.Testwork.UnitTests.CalculatorTests
{
    [TestClass]
    public class DetectorElementsTests
    {
        private SeparatorDetector _separatorDetector;
        private RoundBracketDetector _roundBracketDetector;
        private CurlyBracketDetector _curlyBracketDetector;
        private NumericOperandDetector _numericOperandDetector;
        private SumNumericOperatorDetector _sumNumericOperatorDetector;

        [TestInitialize]
        public void ExpressionElementTestsInit()
        {
            _separatorDetector = new SeparatorDetector();
            _roundBracketDetector = new RoundBracketDetector();
            _curlyBracketDetector = new CurlyBracketDetector();
            _numericOperandDetector = new NumericOperandDetector();
            _sumNumericOperatorDetector = new SumNumericOperatorDetector();
        }

        #region Separator...
        [TestMethod]
        public void SeparatorDetector_CorrectSyntax_Good()
        {
            var checkList = new string[] { " ", "    ", "\t", "\t\t", Environment.NewLine, " \t\r\n" };

            Assert.IsTrue(checkList.All(str => {
                var element = _separatorDetector.GetElement(str);
                return element != null && element.Type == Calculator.Const.ExpressionElementTypes.Separator;
            }));
        }
        [TestMethod]
        public void SeparatorDetector_IncorrectSyntax_Error()
        {
            var checkList = new string[] { "a", "1", " a", "a " };
            Assert.IsFalse(checkList.Any(str => _separatorDetector.GetElement(str) != null));
        }
        #endregion

        #region Brackets... 
        [TestMethod]
        public void RoundBracketDetector_LeftBraket_Good()
        {
            var braket = _roundBracketDetector.GetElement("(");
            
            Assert.IsNotNull(braket);
            Assert.AreEqual(braket.Type, Calculator.Const.ExpressionElementTypes.Bracket);
            Assert.IsInstanceOfType(braket, typeof(RoundBracket));

            Assert.AreEqual(((RoundBracket)braket).BracketSign, Calculator.Const.BracketSign.Open);
        }
        [TestMethod]
        public void RoundBracketDetector_RightBraket_Good()
        {
            var braket = _roundBracketDetector.GetElement(")");
            Assert.IsNotNull(braket);
            Assert.IsInstanceOfType(braket, typeof(RoundBracket));

            Assert.AreEqual(((RoundBracket)braket).BracketSign, Calculator.Const.BracketSign.Close);
        }
        [TestMethod]
        public void RoundBracketDetector_IncorrectSyntax_Error()
        {
            var checkList = new string[] { "()", " (", "( ", " )", ") ", "[", "]", "{", "}" };
            Assert.IsFalse(checkList.Any(str => _roundBracketDetector.GetElement(str) != null));

        }
        
        [TestMethod]
        public void CurlyBracketDetector_LeftBraket_Good()
        {
            var braket = _curlyBracketDetector.GetElement("{");

            Assert.IsNotNull(braket);
            Assert.AreEqual(braket.Type, Calculator.Const.ExpressionElementTypes.Bracket);
            Assert.IsInstanceOfType(braket, typeof(СurlyBracket));
            Assert.AreEqual(((СurlyBracket)braket).BracketSign, Calculator.Const.BracketSign.Open);
        }
        [TestMethod]
        public void CurlyBracketDetector_RightBraket_Good()
        {
            var braket = _curlyBracketDetector.GetElement("}");

            Assert.IsNotNull(braket);
            Assert.IsInstanceOfType(braket, typeof(СurlyBracket));
            Assert.AreEqual(((СurlyBracket)braket).BracketSign, Calculator.Const.BracketSign.Close);
        }
        [TestMethod]
        public void CurlyBracketDetector_IncorrectSyntax_Error()
        {
            var checkList = new string[] { "{}", " {", "} ", " }", "} ", "[", "]", "(", ")" };
            Assert.IsFalse(checkList.Any(str => _curlyBracketDetector.GetElement(str) != null));
        }
        #endregion

        #region NumericOperands... 
        [TestMethod]
        public void NumericOperandsDetector_CorrectSyntax_Good()
        {
            var checkList = new Dictionary<string, double> { { "123", 123 }, { "321.123", 321.123 } };

            Assert.IsTrue(checkList.All(operand => {
                var element = _numericOperandDetector.GetElement(operand.Key);
                return element != null && element.Type == Calculator.Const.ExpressionElementTypes.Operand 
                       && ((IExpressionOperand<double>)element).Value == operand.Value;
            }));
        }
        [TestMethod]
        public void NumericOperandsDetector_IncorrectSyntax_Error()
        {
            var checkList = new string[] { " 1", " 1", "1 ", "-1", "a", "1a","a1", ".1", "1.", "1 .1", "1. 1", "1.1.1", };
            
            Assert.IsFalse(checkList.Any(str => _numericOperandDetector.GetElement(str) != null));
        }

        #endregion

        #region Operators
        [TestMethod]
        public void SumNumericOperatorDetector_Good()
        {
            var element = _sumNumericOperatorDetector.GetElement("+");
            Assert.IsNotNull(element);
            Assert.IsInstanceOfType(element, typeof(IExpressionOperator<double>));
            Assert.AreEqual(((IExpressionOperator<double>)element).OperationType, Calculator.Const.OperationType.Any);
            Assert.AreEqual(((IExpressionOperator<double>)element).Priority, Calculator.Const.OperationPriority.Low);
        }
        [TestMethod]
        public void SumNumericOperatorDetector_IncorrectSyntax_Error()
        {
            var checkList = new string[] { " +", "+ ", "++", "1+", "+1", "+-", "+(", "(+", "1+1" };
            Assert.IsFalse(checkList.Any(str => _sumNumericOperatorDetector.GetElement(str) != null));
        }
        [TestMethod]
        public void SumNumericOperatorDetector_OperationResultTest()
        {
            var sumOperator = _sumNumericOperatorDetector.GetElement("+") as IExpressionOperator<double>;

            Assert.AreEqual(sumOperator.Calculate(1, 2), 3);
            Assert.AreNotEqual(sumOperator.Calculate(1, 2), 0);
            Assert.AreNotEqual(sumOperator.Calculate(1, 2), 1);
            Assert.AreNotEqual(sumOperator.Calculate(1, 2), 2);

            Assert.AreEqual(sumOperator.Calculate(1.125, 2.875), 4);
            Assert.AreEqual(sumOperator.Calculate(1, 2.875), 3.875);
            Assert.AreEqual(sumOperator.Calculate(1.125, 2), 3.125);
        }

        #endregion

    }
}
