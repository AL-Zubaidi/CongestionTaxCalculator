using Api.Infrastracture;
using Api.Infrastracture.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CongestionTaxCalculatorController : ControllerBase
    {
        private readonly ICongestionTaxCalculator _congestionTaxCalculator;

        public CongestionTaxCalculatorController(ICongestionTaxCalculator congestionTaxCalculator)
        {
            _congestionTaxCalculator = congestionTaxCalculator;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Vehicles vehicle, [FromQuery] List<DateTime> dates)
        {
            if (!dates.Any() || dates == null)
                return BadRequest();
            try
            {
                var response = _congestionTaxCalculator.GetCongestionTax(vehicle, dates);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}