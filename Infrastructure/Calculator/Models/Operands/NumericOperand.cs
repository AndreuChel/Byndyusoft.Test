using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models.Operands
{
    public class NumericOperand : IExpressionOperand<double>
    {
        public NumericOperand(double value) 
        {
            Value = value;
        }

        public ExpressionElementTypes Type => ExpressionElementTypes.Operand;

        public double Value { get; }

        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj)
            => obj != null && obj is NumericOperand && GetHashCode() == obj.GetHashCode();
    }
}
