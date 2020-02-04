using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models
{
    public interface IExpressionElement
    {
        ExpressionElementTypes Type { get; }
    }
}
