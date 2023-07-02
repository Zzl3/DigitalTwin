using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Electricityfee")]
    public class ElectricityfeeController : ControllerBase
    {
        private readonly ElectricityfeeService _ElectricityfeeService;

        public ElectricityfeeController(ElectricityfeeService ElectricityfeeService)
        {
            _ElectricityfeeService=ElectricityfeeService;
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
                data=_ElectricityfeeService.getElectricityfees().Result
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
                    data = await _ElectricityfeeService.deleteElectricityfee(min)
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
        public async Task<IActionResult> addNew([FromBody] Electricityfee Electricityfee)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _ElectricityfeeService.addElectricityfee(Electricityfee)
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
