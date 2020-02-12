using System;
using System.Collections.Generic;
using System.Text;
using ByndyuSoft.WebReactApp.Controllers;
using Calculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ByndyuSoft.Testwork.UnitTests.WebReactAppTests
{
    [TestClass]
    public class ExpressionControllerTest
    {
        [TestMethod]
        public void ExpressionControllerTest_ValidExpressionCalculate_OkResult()
        {
            var calculator = new Mock<IExpressionCalculator<double>>();
            calculator.Setup(c => c.Calculate(It.IsAny<string>())).Returns(2);

            var controller = new ExpressionController(calculator.Object);
            var okActionResult = controller.Calculate("1+1") as OkObjectResult;

            Assert.IsNotNull(okActionResult);
            Assert.IsInstanceOfType(okActionResult.Value, typeof(double));
            Assert.AreEqual((double)okActionResult.Value, 2);
        }

        [TestMethod]
        public void ExpressionControllerTest_InvalidExpressionCalculate_BadRequest()
        {
            var calculator = new Mock<IExpressionCalculator<double>>();
            calculator.Setup(c => c.Calculate(It.IsAny<string>())).Throws(new Exception("Ошибка вычисления"));

            var controller = new ExpressionController(calculator.Object);
            var badRequestResult = controller.Calculate("1++1") as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(string));
            Assert.AreEqual((string)badRequestResult.Value, "Ошибка вычисления");
        }
    }
}
