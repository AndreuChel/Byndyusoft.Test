using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Detectors;
using Calculator.Models;

namespace Calculator.Resolvers
{
    public interface IExpressionResolver : IEnumerable<IElementDetector>
    {
        IExpressionResolver AddDetector(IElementDetector detector);

        IEnumerable<IExpressionElement> Parse(string inputExpression);
    }
}
