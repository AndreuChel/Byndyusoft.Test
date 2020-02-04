using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Calculator.Models;
using Calculator.Models.Operands;

namespace Calculator.Detectors.Operands
{
    public class NumericOperandDetector : IElementDetector
    {
        private Regex _searchRegexObject = null;

        public IExpressionElement GetElement(string inputString)
        {
            return (_searchRegexObject ?? (_searchRegexObject = new Regex(@"^\d+(\.\d+)?$"))).IsMatch(inputString) ?
                new NumericOperand(double.Parse(inputString)) : null;
        }
    }
}
