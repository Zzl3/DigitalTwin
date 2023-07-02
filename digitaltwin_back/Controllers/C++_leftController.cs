using auth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;


namespace auth.Controllers
{
    [ApiController]
    [Route("c++_left")]
    public class leftController : ControllerBase
    {

        public leftController()
        {
        }

        [HttpGet("getPercent")]
        public IActionResult getPercent()
        {
            int percent = CaculatePercent();
            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data =percent
            });
        }

        [HttpGet("getNumber")]
        public IActionResult getNumber()
        {
            double number = CaculateNumber();
            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data = number
            });
        }

        [DllImport("/CaculateDll.dll", EntryPoint = "CaculatePercent", CallingConvention = CallingConvention.StdCall)]
        public static extern int CaculatePercent();
        [DllImport("/CaculateDll.dll", EntryPoint = "CaculateNumber", CallingConvention = CallingConvention.StdCall)]
        public static extern double CaculateNumber();
    }
}
