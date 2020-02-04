using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Models;
using Calculator.Models.Operators;

namespace Calculator.Detectors.Operators
{
    public class LgFunctionNumericOperatorDetector : IElementDetector
    {
        public IExpressionElement GetElement(string inputString)
        {
            return string.Equals(inputString,"lg", StringComparison.InvariantCultureIgnoreCase) 
                ? new LgFunctionNumeric() : null;
        }
    }
}
