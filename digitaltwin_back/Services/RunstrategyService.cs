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

        public async Task<bool> addRunstrategy(Runstrategy Runstrategy)
        {
            if (Runstrategy == null) { 
                return false;
            }
            await _database.Runstrategys.AddAsync(Runstrategy);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteRunstrategy(string min)
        {
            Runstrategy data = await _database.Runstrategys.FirstOrDefaultAsync(u => u.air.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Runstrategys.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
