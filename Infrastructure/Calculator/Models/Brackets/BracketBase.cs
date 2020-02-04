using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;
using Calculator.Models;

namespace Calculator.Models.Brackets
{
    public abstract class BracketBase : IExpressionBracket
    {
        public BracketBase(BracketSign sign)
        {
            BracketSign = sign;
        }

        public ExpressionElementTypes Type => ExpressionElementTypes.Bracket;

        public OperationPriority Priority 
            => BracketSign == BracketSign.Open ? OperationPriority.Zero : OperationPriority.Infinity;

        public BracketSign BracketSign { get; private set; }

        public override int GetHashCode() => GetType().Name.GetHashCode();

        public override bool Equals(object obj) 
            => obj != null && obj is BracketBase && GetHashCode() == obj.GetHashCode();

    }
}
