using auth.Services;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [ApiController]
    [Route("Runstrategy")]
    public class RunstrategyController : ControllerBase
    {
        private readonly RunstrategyService _RunstrategyService;

        public RunstrategyController(RunstrategyService RunstrategyService)
        {
            _RunstrategyService=RunstrategyService;
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
                data=_RunstrategyService.getRunstrategys().Result
            });
        }

        [HttpPost("upgradeData")]
        public IActionResult upgradeData(R12Dto r12Dto)
        {
            R1Dto r1Dto = new R1Dto();
            r1Dto.S1time1=r12Dto.S1time1;
            r1Dto.S1time2=r12Dto.S1time2;
            r1Dto.S2time1=r12Dto.S2time1;
            r1Dto.S2time2=r12Dto.S2time2;
            r1Dto.S3time1=r12Dto.S3time1;
            r1Dto.S3time2=r12Dto.S3time2;
            R2Dto r2Dto = new R2Dto();
            r2Dto.S1time1=r12Dto.S1pressure1;
            r2Dto.S1time2=r12Dto.S1pressure2;
            r2Dto.S2time1=r12Dto.S2pressure1;
            r2Dto.S2time2=r12Dto.S2pressure2;
            r2Dto.S3time1=r12Dto.S3pressure1;
            r2Dto.S3time2=r12Dto.S3pressure2;

            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data=_RunstrategyService.upgradeData(r1Dto,r2Dto).Result
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
                    data = await _RunstrategyService.deleteRunstrategy(min)
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
        public async Task<IActionResult> addNew([FromBody] Runstrategy Runstrategy)
        {
            try
            {
                var code = 200;
                var msg = "success";
                return Ok(new
                {
                    code,
                    msg,
                    data = await _RunstrategyService.addRunstrategy(Runstrategy)
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
