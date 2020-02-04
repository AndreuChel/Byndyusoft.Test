using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Calculator.Const;
using Calculator.Models;

namespace Calculator.Models.Separators
{
    public class DefaultSeparator : IExpressionSeparator
    {
        public ExpressionElementTypes Type => ExpressionElementTypes.Separator;

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => obj as DefaultSeparator != null;
    }
}
