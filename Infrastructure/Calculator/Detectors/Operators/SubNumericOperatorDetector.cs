using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Models;
using Calculator.Models.Operators;

namespace Calculator.Detectors.Operators
{
    public class SubNumericOperatorDetector : IElementDetector
    {
        public IExpressionElement GetElement(string inputString)
        {
            return inputString == "-" ? new SubNumeric() : null;
        }
        
    }
}
