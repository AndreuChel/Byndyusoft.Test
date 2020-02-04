using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Resolvers;

namespace Calculator
{
    public class NumericCalculator : ExpressionCalculator<double> 
    {
        public NumericCalculator() : base (new ArithmeticExpressionResolver())
        {
                
        }
    }
}
