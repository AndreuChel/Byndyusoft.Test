using System;
using System.Collections.Generic;
using System.Text;
using Calculator;
using Calculator.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ByndyuSoft.Testwork.UnitTests.CalculatorTests
{
    [TestClass]
    public class ExpressionCalculatorTest
    {
        [TestMethod]
        public void ExpressionCalculator_EmptyResolver_Throw()
        {
            Assert.ThrowsException<EmptyResolverException>(() 
                => new ExpressionCalculator<double>().Calculate("1+1")
            );
        }

        [TestMethod]
        public void ExpressionCalculator_DetectUnsupportedElements_Throw()
        {
            Assert.ThrowsException<UnsupportedElementTypeException>(() 
                => new ExpressionCalculator<string>(new ArithmeticExpressionResolver()).Calculate("1+1")
            );

        }


        [TestMethod]
        public void ExpressionCalculator_OnePlusOne_Good()
        {
            var result = new NumericCalculator().Calculate("1+1");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void ExpressionCalculator_OnePlusOneWithSpaces_Good()
        {
            var result = new NumericCalculator().Calculate("   1    + 1             ");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void ExpressionCalculator_OnePlusOne_WithRoundBracket_Good()
        {
            var result = new NumericCalculator().Calculate("(1+1)");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void ExpressionCalculator_OnePlusOne_WithCurlyBracket_Good()
        {
            var result = new NumericCalculator().Calculate("(1+1)");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void ExpressionCalculator_OnePlusOne_WithRoundAndCurlyBracket_Good()
        {
            var result = new NumericCalculator().Calculate("{(1+1)}");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void ExpressionCalculator_OnlyOpenBracket_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("(1+1"));
        }

        [TestMethod]
        public void ExpressionCalculator_OnlyCloseBracket_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("1+1)"));
        }

        [TestMethod]
        public void ExpressionCalculator_ErrorPlaceBracket_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("1)+1"));
        }

        [TestMethod]
        public void ExpressionCalculatorDifferentBrackets_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("{1+1)"));
        }

        [TestMethod]
        public void ExpressionCalculator_OnePlusEmptyBracketBlock_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("1+()"));
        }

        [TestMethod]
        public void ExpressionCalculator_EmptyBracketBlock_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("()"));
        }

        [TestMethod]
        public void ExpressionCalculator_OneOperandInBracketBlock_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("(1)"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_TwoOperatorInRow_Error()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("1+-1"));
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryPlus_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("+1"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryPlusInBrackets_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("(+1)"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryMinus_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("-1"), -1);
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryMinusInBrackets_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("(-1)"), -1);
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryMinusWithAnotherOp_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("+1 + (-1)"), 0);
        }

        [TestMethod]
        public void ExpressionCalculator_TwoMinusOne_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 - 1"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_2x2_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2*2"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryMultiply_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("(*1)"));
        }

        [TestMethod]
        public void ExpressionCalculator_MultiplyPriorityTest1_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2*2+1"), 5);
        }

        [TestMethod]
        public void ExpressionCalculator_MultiplyPriorityTest2_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("1+2*2"), 5);
        }

        [TestMethod]
        public void ExpressionCalculator_MultiplyPriorityTest3_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2*(3-1)"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_MultiplyPriorityTest4_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("5-(2*2)"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_MultiplyPriorityTest5_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2*{+2 + (-2*2)} - 1"), -5);
        }

        [TestMethod]
        public void ExpressionCalculator_MultiplyPriorityTest6_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 * { +2 - (-2*2) } - 1"), 11);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionSimpleTest_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("4 / 2"), 2);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionByZero_Throw()
        {
            Assert.ThrowsException<DivideByZeroException>(() => new NumericCalculator().Calculate("4/0"));
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryDivision_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("(/1)"));
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest1_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("1 + 4 / 2"), 3);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest2_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("4 / 2 + 1"), 3);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest3_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("4 / (1 + 1)"), 2);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest4_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("4 / 2 * 2"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest5_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("4 / (2 * 2)"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest6_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 * 2 / 4"), 1);
        }

        [TestMethod]
        public void ExpressionCalculator_DivisionPriorityTest7_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 * (4 / 2)"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_PowSimpleTest_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 ^ 3"), 8);
        }

        [TestMethod]
        public void ExpressionCalculator_UnaryPow_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("(^1)"));
        }

        [TestMethod]
        public void ExpressionCalculator_PowPriorityTest1_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 * 2 ^ 3"), 16);
        }

        [TestMethod]
        public void ExpressionCalculator_PowPriorityTest2_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 + 2 ^ 3"), 10);
        }

        [TestMethod]
        public void ExpressionCalculator_PowPriorityTest3_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 ^ 3 * 2"), 16);
        }

        [TestMethod]
        public void ExpressionCalculator_PowPriorityTest4_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 ^ 3 + 2"), 10);
        }

        [TestMethod]
        public void ExpressionCalculator_PowPriorityTest5_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("{(1+1)*2}^2+2"), 18);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionSimpleTest_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg 100"), 2);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionSimpleTest2_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg(100)"), 2);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionSimpleTest3_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg(10^2)"), 2);
        }

        [TestMethod]
        public void ExpressionCalculator_AsBinaryOperation_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new NumericCalculator().Calculate("10 lg 10)"));
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionPriorityTest1_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg(100) + 1"), 3);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionPriorityTest2_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg(100) * 2"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionPriorityTest3_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg 100 * 2"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionPriorityTest4_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("2 * Lg 100"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionPriorityTest5_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg 100 ^ 2"), 4);
        }

        [TestMethod]
        public void ExpressionCalculator_LogFunctionPriorityTest6_Good()
        {
            Assert.AreEqual<double>(new NumericCalculator().Calculate("Lg {101 - 1}"), 2);
        }
    }
}
