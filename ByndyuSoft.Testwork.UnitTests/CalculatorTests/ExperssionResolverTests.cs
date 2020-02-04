using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator;
using Calculator.Const;
using Calculator.Detectors;
using Calculator.Models;
using Calculator.Models.Brackets;
using Calculator.Models.Operands;
using Calculator.Models.Operators;
using Calculator.Models.Separators;
using Calculator.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ByndyuSoft.Testwork.UnitTests.CalculatorTests
{
    [TestClass]
    public class ExperssionResolverTests
    {
        [TestMethod]
        public void ExpressionResolver_AddDetectors_Good()
        {
            var mockDetector = new Mock<IElementDetector>();
            mockDetector
                .Setup(el => el.GetElement(It.IsAny<string>()))
                .Returns(new Mock<IExpressionSeparator>().Object);

            Assert.IsTrue(
                new ExpressionResolver()
                .AddDetector(mockDetector.Object)
                .FirstOrDefault()?.GetElement("") is IExpressionSeparator
            );
        }

        [TestMethod]
        public void ExpressionResolver_IncorrectIncomingString_Throw()
        {
            Assert.ThrowsException<InvalidExpressionException>(() => new ExpressionResolver().Parse("1"));
        }

        [TestMethod]
        public void ExpressionResolver_SeveralDetectorsToItem_Throw()
        {
            var mockDetector = new Mock<IElementDetector>();
            mockDetector.Setup(el => el.GetElement(It.IsAny<string>())).Returns(new Mock<IExpressionElement>().Object);

            var mockDetector2 = new Mock<IElementDetector>();
            mockDetector2.Setup(el => el.GetElement(It.IsAny<string>())).Returns(new Mock<IExpressionElement>().Object);

            Assert.ThrowsException<InvalidResolverConfigureException>(() 
                => new ExpressionResolver()
                    .AddDetector(mockDetector.Object)
                    .AddDetector(mockDetector2.Object)
                    .Parse("1"));
        }

        [TestMethod]
        public void ArithmeticExpressionResolver_ParseResultTest()
        {
            var checkList = new Dictionary<string, IEnumerable<IExpressionElement>>
            {
                { " ", new IExpressionElement[] { new DefaultSeparator() } },
                { "1+", new IExpressionElement[] { new NumericOperand(1), new SumNumeric() } },

                { "1", new IExpressionElement[] { new NumericOperand(1) } },
                { "1.1", new IExpressionElement[] { new NumericOperand(1.1) } },

                { "()", new IExpressionElement[] { new RoundBracket(BracketSign.Open), new RoundBracket(BracketSign.Close) } },
                { "{}", new IExpressionElement[] { new СurlyBracket(BracketSign.Open), new СurlyBracket(BracketSign.Close) } },

                { "+", new IExpressionElement[] { new SumNumeric() } },
                
                { "(1.1+2.2)", new IExpressionElement[] {
                    new RoundBracket(BracketSign.Open), new NumericOperand(1.1),
                    new SumNumeric(), new NumericOperand(2.2), new RoundBracket(BracketSign.Close) }
                }
            };

            Assert.IsTrue(checkList.All(input => {
                var elementList = new ArithmeticExpressionResolver().Parse(input.Key);
                var validElementList = input.Value;

                return elementList.Any() 
                        && validElementList.Count() == elementList.Count()
                        && elementList.Select((element, index) => element.Equals(validElementList.ElementAtOrDefault(index)))
                                      .All(compareResult => compareResult == true);
            }));
        }

    }
}
