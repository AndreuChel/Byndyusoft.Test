using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models.Operators
{
    public class PowNumeric : IExpressionOperator<double>
    {
        public OperationType OperationType => OperationType.Binary;

        public OperationPriority Priority => OperationPriority.High;

        public ExpressionElementTypes Type => ExpressionElementTypes.Operator;

        public double Calculate(double operand1, double operand2 = 0)
        {
            return Math.Pow(operand1, operand2);
        }
    }
}
