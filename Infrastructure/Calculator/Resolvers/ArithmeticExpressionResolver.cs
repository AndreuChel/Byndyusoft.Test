using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Calculator.Detector.Separators;
using Calculator.Detectors;
using Calculator.Detectors.Brackets;
using Calculator.Detectors.Operands;
using Calculator.Detectors.Operators;
using Calculator.Models;
using Calculator.Models.Separators;

namespace Calculator.Resolvers
{
    public class ArithmeticExpressionResolver : ExpressionResolver
    {
        public ArithmeticExpressionResolver()
        {
            /*separators*/
            AddDetector(new SeparatorDetector());
            
            /*brackets*/
            AddDetector(new RoundBracketDetector());
            AddDetector(new CurlyBracketDetector());
            
            /*Operands*/
            AddDetector(new NumericOperandDetector());
            
            /*Operators*/
            AddDetector(new SumNumericOperatorDetector());
            AddDetector(new SubNumericOperatorDetector());
            AddDetector(new MulNumericOperatorDetector());
            AddDetector(new DivNumericOperatorDetector());
            AddDetector(new PowNumericOperatorDetector());
            AddDetector(new LgFunctionNumericOperatorDetector());
        }
    }
}
