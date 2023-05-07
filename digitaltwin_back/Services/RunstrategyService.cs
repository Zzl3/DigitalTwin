using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
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

        public async Task<List<Runstrategy>> upgradeData(R1Dto r1Dto, R2Dto r2Dto)
        {
            List<Runstrategy> runstrategys = new List<Runstrategy>();
            //r1Dto和r2Dto是前端传过来的数据
            //S1,S2,S3分别代表三个机器
            //r1Dto的time代表time，r2Dto的time代表pressure
            //返回的是runstrategys

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

            _database.Runstrategys.Update(runstrategy1);
            _database.Runstrategys.Update(runstrategy2);
            _database.Runstrategys.Update(runstrategy3);
            await _database.SaveChangesAsync();
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
