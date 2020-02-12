using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculator;
using Microsoft.AspNetCore.Mvc;

namespace ByndyuSoft.WebReactApp.Controllers
{
    [Route("api/expression")]
    [ApiController]
    public class ExpressionController : ControllerBase
    {
        private IExpressionCalculator<double> _calculator;

        public ExpressionController(IExpressionCalculator<double> calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] string expression)
        {
            double result = 0;
            try
            {
                result = _calculator.Calculate(expression);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
            
        }
    }
}
