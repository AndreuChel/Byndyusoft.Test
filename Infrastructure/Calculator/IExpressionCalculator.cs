using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public interface IExpressionCalculator<TResult>
    {
        TResult Calculate(string incomingExpression);
    }
}
