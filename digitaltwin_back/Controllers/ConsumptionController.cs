using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("consumption")]
    public class ConsumptionController : ControllerBase
    {
        private readonly ConsumptionService _ConsumptionService;

        public ConsumptionController(ConsumptionService ConsumptionService)
        {
            _ConsumptionService=ConsumptionService;
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
                data=_ConsumptionService.getConsumptions().Result
            });
        }

        [HttpGet("getnewDatas")]
        public IActionResult getnewDatas()
        {
            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data=_ConsumptionService.getNewConsumptions().Result
            });
        }

        [HttpPost("deleteData")]
        public async Task<IActionResult> deleteData(int min)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _ConsumptionService.deleteConsumption(min)
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
        public async Task<IActionResult> addNew([FromBody] Consumption consumption)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _ConsumptionService.addConsumption(consumption)
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
