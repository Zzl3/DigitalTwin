using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace auth.Controllers
{
    [ApiController]
    [Route("Test")]
    public class TestController
    {
        [DllImport("/C++Project.dll", EntryPoint = "Sum", CallingConvention = CallingConvention.StdCall)]
        public static extern int Sum(int a,int b);
        
        [HttpGet("test")]
        public int testing()
        {
            try
            {
                int numberA = 12;
                int numberB = 13;
                int numberC = Sum(numberA, numberB);
                return numberC;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex:{ex}");
                return 0;
            }
            return 0;
        }
    }
}
