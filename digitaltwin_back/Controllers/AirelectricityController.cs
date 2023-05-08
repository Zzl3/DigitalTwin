using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Airelectricity")]
    public class AirelectricityController : ControllerBase
    {
        private readonly AirelectricityService _AirelectricityService;

        public AirelectricityController(AirelectricityService AirelectricityService)
        {
            _AirelectricityService=AirelectricityService;
        }
        
        [HttpGet("getDatas")]
        public IActionResult getDatas()
        {
            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data=_AirelectricityService.getAirelectricitys().Result
            });
        }

        [HttpPost("deleteData")]
        public async Task<IActionResult> deleteData(string min)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _AirelectricityService.deleteAirelectricity(min)
                });
            }
            catch (Exception ex)
            {
                var code = 500;
                var msg = "failed";
                return Ok(new
                {
                    code,
                    msg,
                    data = ex.Message
                });
            }
        }


        [HttpPost("addData")]
        public async Task<IActionResult> addNew([FromBody] Airelectricity Airelectricity)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _AirelectricityService.addAirelectricity(Airelectricity)
                });
            }
            catch (Exception ex)
            {
                var code = 500;
                var msg = "failed";
                return Ok(new
                {
                    code,
                    msg,
                    data = ex.Message
                });
            }
        }
    }
}
