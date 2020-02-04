using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Models;
using Calculator.Models.Operators;

namespace Calculator.Detectors.Operators
{
    public class DivNumericOperatorDetector : IElementDetector
    {
        public IExpressionElement GetElement(string inputString)
        {
            return inputString == "/" ? new DivNumeric() : null;
        }
    }
}
