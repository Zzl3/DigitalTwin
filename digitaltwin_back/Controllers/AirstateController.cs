using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Airstate")]
    public class AirstateController : ControllerBase
    {
        private readonly AirstateService _AirstateService;

        public AirstateController(AirstateService AirstateService)
        {
            _AirstateService=AirstateService;
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
                data=_AirstateService.getAirstates().Result
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
                    data = await _AirstateService.deleteAirstate(min)
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
        public async Task<IActionResult> addNew([FromBody] Airstate Airstate)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _AirstateService.addAirstate(Airstate)
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
