using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Systemair")]
    public class SystemairController : ControllerBase
    {
        private readonly SystemairService _SystemairService;

        public SystemairController(SystemairService SystemairService)
        {
            _SystemairService=SystemairService;
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
                data=_SystemairService.getSystemairs().Result
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
                    data = await _SystemairService.deleteSystemair(min)
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
        public async Task<IActionResult> addNew([FromBody] Systemair Systemair)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _SystemairService.addSystemair(Systemair)
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
