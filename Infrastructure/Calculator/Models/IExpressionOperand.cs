using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Models
{
    public interface IExpressionOperand : IExpressionElement {   }

    public interface IExpressionOperand<T> : IExpressionOperand
    {
        T Value { get; }
    }

}
