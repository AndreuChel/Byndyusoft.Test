using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Models;

namespace Calculator.Detectors
{
    public interface IElementDetector
    {
        IExpressionElement GetElement(string inputString);
    }
}
