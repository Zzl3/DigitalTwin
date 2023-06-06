using auth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using auth.Models;
using auth.Dtos;
using Newtonsoft.Json;


namespace auth.Controllers
{
    [ApiController]
    [Route("c++_right")]
    public class rightController : ControllerBase
    {

        public rightController()
        {
        }

        [HttpGet("getStress")]
        public IActionResult getStress2()
        {
            int num = CaculatePercent();
            double result = Math.Sin(num) * Math.Cos(num) / Math.Log(num, 2); 
            result=(result+10)*10;
            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data =result
            });
        }

        [HttpGet("getWaterAndElectricity")]
        public IActionResult getWaterAndElectricity2()
        {
            double num =CaculateNumber();
            double result = Math.Pow(Math.PI, num) * Math.Cos(num) / Math.Log(num + Math.E, Math.E); // 数学公式变换，将num取π的num次方，再取余弦，再除以以e为底的
            result=result*100;
            var code = 200;
            var msg = "success";
            return Ok(new
            {
                code,
                msg,
                data = result
            });
        }

        [HttpGet("getNowElectricity")]
        public IActionResult getNowElectricity2()
        {
            double num = CaculateNumber();
            double result = Math.Atan(Math.Abs(Math.Tan(num))) * Math.Log(Math.Sqrt(num), Math.E) / Math.Sqrt(Math.Pow(num, 2) + 1);
            var code = 200;
            var msg = "success";
            result=result*100;
            return Ok(new
            {
                code,
                msg,
                data = result
            });
        }

        [DllImport("/CaculateDll.dll", EntryPoint = "CaculatePercent", CallingConvention = CallingConvention.StdCall)]
        public static extern int CaculatePercent();
        [DllImport("/CaculateDll.dll", EntryPoint = "CaculateNumber", CallingConvention = CallingConvention.StdCall)]
        public static extern double CaculateNumber();
    }
}
