using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models.Operators
{
    public class SubNumeric : IExpressionOperator<double>, ICanCahngeOperationType
    {
        public ExpressionElementTypes Type => ExpressionElementTypes.Operator;

        public OperationPriority Priority => OperationPriority.Low;

        public OperationType OperationType { get; private set; } = OperationType.Any;

        public double Calculate(double operand1, double operand2 = 0)
        {
            return operand1 - operand2;
        }

        public void SetOperationType(OperationType newType)
        {
            OperationType = newType;
        }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj != null && obj is SubNumeric;

    }
}

