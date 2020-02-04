using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Calculator.Models;
using Calculator.Detectors;
using Calculator.Models.Separators;

namespace Calculator.Detector.Separators
{
    public class SeparatorDetector : IElementDetector
    {
        private Regex _searchRegexObject = null;

        public IExpressionElement GetElement(string inputString)
        {
            return (_searchRegexObject ?? (_searchRegexObject = new Regex(@"^\s+$"))).IsMatch(inputString) ?
                new DefaultSeparator() : null;
        }
    }
}
