using System;
using System.Collections.Generic;
using System.Text;
using Calculator;
using Calculator.Detectors;
using Calculator.Models;
using Calculator.Models.Brackets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ByndyuSoft.Testwork.UnitTests
{
    [TestClass]
    public class DemoAbilities
    {
        [TestMethod]
        public void Demo_AddNewOperandType()
        {
            var mockOperand = new Mock<IExpressionOperand<double>>();
            mockOperand.Setup(el => el.Type).Returns(Calculator.Const.ExpressionElementTypes.Operand);
            mockOperand.Setup(el => el.Value).Returns(3.1415);

            var mockDetector = new Mock<IElementDetector>();
            mockDetector.Setup(detector => detector.GetElement("Pi")).Returns(mockOperand.Object);

            var calc = new NumericCalculator();
            calc.Resolver.AddDetector(mockDetector.Object);

            Assert.AreEqual(calc.Calculate("2 * Pi"), 6.283);

        }

        [TestMethod]
        public void Demo_AddNewOperator()
        {
            var mockOperator = new Mock<IExpressionOperator<double>>();
            mockOperator.Setup(el => el.Type).Returns(Calculator.Const.ExpressionElementTypes.Operator);
            mockOperator.Setup(el => el.Priority).Returns(Calculator.Const.OperationPriority.Medium);
            mockOperator.Setup(el => el.OperationType).Returns(Calculator.Const.OperationType.Binary);
            mockOperator.Setup(el => el.Calculate(9, 2)).Returns(1);

            var mockDetector = new Mock<IElementDetector>();
            mockDetector.Setup(detector => detector.GetElement("%")).Returns(mockOperator.Object);

            var calc = new NumericCalculator();
            calc.Resolver.AddDetector(mockDetector.Object);

            Assert.AreEqual(calc.Calculate("1 + 9 % 2"), 2);
        }

        [TestMethod]
        public void Demo_AddNewFunction()
        {
            var mockOperator = new Mock<IExpressionOperator<double>>();
            mockOperator.Setup(el => el.Type).Returns(Calculator.Const.ExpressionElementTypes.Operator);
            mockOperator.Setup(el => el.Priority).Returns(Calculator.Const.OperationPriority.Highest);
            mockOperator.Setup(el => el.OperationType).Returns(Calculator.Const.OperationType.Unary);
            mockOperator.Setup(el => el.Calculate(0, 8)).Returns(1);

            var mockDetector = new Mock<IElementDetector>();
            mockDetector.Setup(detector => detector.GetElement("test")).Returns(mockOperator.Object);

            var calc = new NumericCalculator();
            calc.Resolver.AddDetector(mockDetector.Object);

            Assert.AreEqual(calc.Calculate("1 + test(8)"), 2);
        }

        [TestMethod]
        public void Demo_AddNewBracket()
        {
            var mockBraketOpen = new Mock<IExpressionBracket>();
            mockBraketOpen.Setup(el => el.Type).Returns(Calculator.Const.ExpressionElementTypes.Bracket);
            mockBraketOpen.Setup(el => el.Priority).Returns(Calculator.Const.OperationPriority.Zero);
            mockBraketOpen.Setup(el => el.BracketSign).Returns(Calculator.Const.BracketSign.Open);
            mockBraketOpen.Setup(el => el.GetHashCode()).Returns(1);
            mockBraketOpen.Setup(el => el.GetHashCode()).Returns(1);
            mockBraketOpen.Setup(el => el.Equals(It.IsAny<IExpressionBracket>())).Returns(true);

            var mockBraketClose = new Mock<IExpressionBracket>();
            mockBraketClose.Setup(el => el.Type).Returns(Calculator.Const.ExpressionElementTypes.Bracket);
            mockBraketClose.Setup(el => el.Priority).Returns(Calculator.Const.OperationPriority.Infinity);
            mockBraketClose.Setup(el => el.GetHashCode()).Returns(1);
            mockBraketClose.Setup(el => el.BracketSign).Returns(Calculator.Const.BracketSign.Close);
            mockBraketClose.Setup(el => el.Equals(It.IsAny<IExpressionBracket>())).Returns(true);


            var mockDetector = new Mock<IElementDetector>();
            mockDetector.Setup(detector => detector.GetElement("[")).Returns(mockBraketOpen.Object);
            mockDetector.Setup(detector => detector.GetElement("]")).Returns(mockBraketClose.Object);

            var calc = new NumericCalculator();
            calc.Resolver.AddDetector(mockDetector.Object);

            Assert.AreEqual(calc.Calculate("[1 + 1] * 2"), 4);
        }
    }
}
