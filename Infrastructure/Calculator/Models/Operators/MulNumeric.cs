using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models.Operators
{
    public class MulNumeric : IExpressionOperator<double>
    {
        public OperationType OperationType => OperationType.Binary;

        public OperationPriority Priority => OperationPriority.Medium;

        public ExpressionElementTypes Type => ExpressionElementTypes.Operator;

        public double Calculate(double operand1, double operand2 = 0)
        {
            return operand1 * operand2;
        }
    }
}
