using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.IO;

namespace auth.Services
{
    static class Constants
    {
        public const string python_path = "./Services/demo.py";
        public const int args_num = 12;
    }

    public class Send
    {
        public string flag_beg = "<data>";
        public string flag_end = "</data>";
        public Send()
        {

        }

        private string handle(string strOuput)
        {
            int beg = strOuput.IndexOf("<data>");
            int end = strOuput.IndexOf("</data>");
            if (end < beg + flag_beg.Length)
            {
                Console.WriteLine("Error : flag mismatch");
            }
            string result = strOuput.Substring(beg + flag_beg.Length, end - beg - flag_beg.Length);
            return result;
        }

        public void run(string[] args)
        {
            try
            {
                if (args.Length != Constants.args_num)
                {
                    Console.WriteLine("Error : args not enough");
                    return;
                }
                Process p = new Process();
                string line = "python " + Constants.python_path + " ";
                //设置要启动的应用程序
                p.StartInfo.FileName = "cmd.exe";
                //是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = true;
                //输出信息
                p.StartInfo.RedirectStandardOutput = true;
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = true;
                //启动程序
                p.Start();

                for (int i = 0; i < Constants.args_num; i++)
                {
                    line += args[i] + " ";
                }
                Console.WriteLine(line);
                p.StandardInput.WriteLine(line);
                p.StandardInput.WriteLine("exit");
                p.StandardInput.AutoFlush = true;
                //获取输出信息
                string strOuput = p.StandardOutput.ReadToEnd();
                //等待程序执行完退出进程
                //Console.WriteLine(strOuput);
                string result = handle(strOuput);
                //Console.WriteLine(result);
                File.WriteAllText(@".\WriteLines.txt", result);
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
        }
    }
    public class RunstrategyService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public RunstrategyService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Runstrategy>> getRunstrategys()
        {
            return await _database.Runstrategys.ToListAsync();
        }

        public async Task<List<Runstrategy>> upBestData()
        {
            return null;
        }

        public async Task<List<Runstrategy>> upgradeData(R1Dto r1Dto, R2Dto r2Dto)
        {
            List<Runstrategy> runstrategys = new List<Runstrategy>();
            //r1Dto和r2Dto是前端传过来的数据
            //S1,S2,S3分别代表三个机器
            //r1Dto的time代表time，r2Dto的time代表pressure
            //返回的是runstrategys
            Send se = new Send();
            string[] args = new string[Constants.args_num];
            args[0] = r1Dto.S1time1;
            args[1] = r2Dto.S1time1;
            args[2] = r1Dto.S1time2;
            args[3] = r2Dto.S1time2;
            args[4] = r1Dto.S2time1;
            args[5] = r2Dto.S2time1;
            args[6] = r1Dto.S2time2;
            args[7] = r2Dto.S2time2;
            args[8] = r1Dto.S3time1;
            args[9] = r2Dto.S3time1;
            args[10] = r1Dto.S3time2;
            args[11] = r2Dto.S3time2;
            se.run(args);

            //总而言之，需要改变的值共12个，如下
            //r1Dto.S1time1，r1Dto.S1time2，r2Dto.S1time1，r2Dto.S1time2
            //r1Dto.S2time1，r1Dto.S2time2，r2Dto.S2time1，r2Dto.S2time2
            //r1Dto.S3time1，r1Dto.S3time2，r2Dto.S3time1，r2Dto.S3time2


            Runstrategy runstrategy1 = new Runstrategy();
            runstrategy1.air = "Comp1"; //这个变量不能改变
            runstrategy1.time1 = r1Dto.S1time1;
            runstrategy1.time2 = r1Dto.S1time2;
            runstrategy1.pressure1 = r2Dto.S1time1;
            runstrategy1.pressure2 = r2Dto.S1time2;

            Runstrategy runstrategy2 = new Runstrategy();
            runstrategy2.air = "Comp2"; //这个变量不能改变
            runstrategy2.time1 = r1Dto.S2time1;
            runstrategy2.time2 = r1Dto.S2time2;
            runstrategy2.pressure1 = r2Dto.S2time1;
            runstrategy2.pressure2 = r2Dto.S2time2;

            Runstrategy runstrategy3 = new Runstrategy();
            runstrategy3.air = "Comp3"; //这个变量不能改变
            runstrategy3.time1 = r1Dto.S3time1;
            runstrategy3.time2 = r1Dto.S3time2;
            runstrategy3.pressure1 = r2Dto.S3time1;
            runstrategy3.pressure2 = r2Dto.S3time2;

            runstrategys.Add(runstrategy1);
            runstrategys.Add(runstrategy2);
            runstrategys.Add(runstrategy3);

            // 右边12个数据不需要存数据库
            // _database.Runstrategys.Update(runstrategy1);
            // _database.Runstrategys.Update(runstrategy2);
            // _database.Runstrategys.Update(runstrategy3);
            // await _database.SaveChangesAsync();

            // 读取txt中的文件，存入数据库
            // 读取文本文件存入数组
            List<double[]> arrays = new List<double[]>();
            using (StreamReader reader = new StreamReader("./WriteLines.txt"))
            {
                string line;
                //List<string> lines = new List<string>();
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("["))
                    {
                        // Console.WriteLine(line);
                        string[] nums = line.Trim('[', ']').Split(',').Select(x => x.Trim()).ToArray();
                        // Console.WriteLine(nums[0]);
                        // Console.WriteLine(nums.Length);
                        double[] array = Array.ConvertAll(nums, double.Parse);
                        arrays.Add(array);
                        //Console.WriteLine(array[0]);
                        //Console.WriteLine(array.Length);
                        //throw new ArgumentNullException("test");
                    }
                    // if (line.StartsWith("["))
                    // {
                    //     lines.Clear();
                    //     lines.Add(line);
                    // }
                    // else if (line.EndsWith("]"))
                    // {
                    //     lines.Add(line);
                    //     string[] nums = string.Concat(lines).Trim('[', ']').Split(',').Select(x => x.Trim()).ToArray();
                    //     double[] array = Array.ConvertAll(nums, double.Parse);
                    //     arrays.Add(array);
                    // }
                    // else
                    // {
                    //     lines.Add(line);
                    // }
                }
            }

            //打印数组长度
            Console.WriteLine(arrays.Count);
            // throw new ArgumentNullException("test");

            //清空数值
            _database.NewOnandoffs.RemoveRange(_database.NewOnandoffs);
            _database.NewConsumptions.RemoveRange(_database.NewConsumptions);
            _database.SaveChanges();

            //遍历前三个数组，存入数据库
            double[] nums0 = arrays[0];
            double[] nums1 = arrays[1];
            double[] nums2 = arrays[2];
            int length = Math.Min(nums0.Length, Math.Min(nums1.Length, nums2.Length));
            //throw new ArgumentNullException("test");
            for (int i = 0; i < length; i++)
            {
                NewOnandoff onandoff = new NewOnandoff();
                onandoff.min = i+1;
                onandoff.air1 = nums0[i].ToString();
                onandoff.air2 = nums1[i].ToString();
                onandoff.air3 = nums2[i].ToString();
                _database.NewOnandoffs.Add(onandoff);
            }

            //遍历最后一个数组，存入数据库
            double[] nums3 = arrays[3];
            for (int i = 0; i < nums3.Length; i++)
            {
                NewConsumption consumption = new NewConsumption();
                consumption.min = i+1;
                consumption.after = nums3[i].ToString();
                _database.NewConsumptions.Add(consumption);
            }

            //存入数据库
            _database.SaveChanges();

            return runstrategys;
        }

        public async Task<bool> addRunstrategy(Runstrategy Runstrategy)
        {
            if (Runstrategy == null)
            {
                return false;
            }
            await _database.Runstrategys.AddAsync(Runstrategy);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteRunstrategy(string min)
        {
            Runstrategy data = await _database.Runstrategys.FirstOrDefaultAsync(u => u.air.Equals(min));
            if (data == null)
            {
                return false;
            }
            _database.Runstrategys.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
