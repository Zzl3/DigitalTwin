using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Recentconsumption")]
    public class RecentconsumptionController : ControllerBase
    {
        private readonly RecentconsumptionService _RecentconsumptionService;

        public RecentconsumptionController(RecentconsumptionService RecentconsumptionService)
        {
            _RecentconsumptionService=RecentconsumptionService;
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
                data=_RecentconsumptionService.getRecentconsumptions().Result
            });
        }

        [HttpPost("deleteData")]
        public async Task<IActionResult> deleteData(int date)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _RecentconsumptionService.deleteRecentconsumption(date)
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
        public async Task<IActionResult> addNew([FromBody] Recentconsumption Recentconsumption)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _RecentconsumptionService.addRecentconsumption(Recentconsumption)
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
