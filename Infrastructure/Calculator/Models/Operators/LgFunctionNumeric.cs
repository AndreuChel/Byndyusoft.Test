using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models.Operators
{
    public class LgFunctionNumeric : IExpressionOperator<double>
    {
        public OperationType OperationType => OperationType.Unary;

        public OperationPriority Priority => OperationPriority.Highest;

        public ExpressionElementTypes Type => ExpressionElementTypes.Operator;

        public double Calculate(double operand1, double operand2 = 0)
        {
            return Math.Log10(operand2);
        }
    }
}
