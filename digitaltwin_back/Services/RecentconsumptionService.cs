using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class RecentconsumptionService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public RecentconsumptionService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Recentconsumption>> getRecentconsumptions()
        {
            return await _database.Recentconsumptions.ToListAsync();
        }

        public async Task<bool> addRecentconsumption(Recentconsumption Recentconsumption)
        {
            if (Recentconsumption == null) { 
                return false;
            }
            await _database.Recentconsumptions.AddAsync(Recentconsumption);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteRecentconsumption(int date)
        {
            Recentconsumption data = await _database.Recentconsumptions.FirstOrDefaultAsync(u => u.date.Equals(date));
            if (data == null) { 
                return false;
            }
            _database.Recentconsumptions.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
