using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Systemelectricity")]
    public class SystemelectricityController : ControllerBase
    {
        private readonly SystemelectricityService _SystemelectricityService;

        public SystemelectricityController(SystemelectricityService SystemelectricityService)
        {
            _SystemelectricityService=SystemelectricityService;
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
                data=_SystemelectricityService.getSystemelectricitys().Result
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
                    data = await _SystemelectricityService.deleteSystemelectricity(min)
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
        public async Task<IActionResult> addNew([FromBody] Systemelectricity Systemelectricity)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _SystemelectricityService.addSystemelectricity(Systemelectricity)
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
