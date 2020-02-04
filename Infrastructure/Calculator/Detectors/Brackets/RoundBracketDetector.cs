using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;
using Calculator.Models;
using Calculator.Models.Brackets;

namespace Calculator.Detectors.Brackets
{
    public class RoundBracketDetector : IElementDetector
    {
        public IExpressionElement GetElement(string inputString)
        {
            var isOpenBracket = false; 
            return !string.IsNullOrEmpty(inputString) && ((isOpenBracket = inputString == "(") || inputString == ")")
                ? new RoundBracket(isOpenBracket ? BracketSign.Open : BracketSign.Close) : null;
        }
    }
}
