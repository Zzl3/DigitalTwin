using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class ConsumptionService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public ConsumptionService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Consumption>> getConsumptions()
        {
            return await _database.Consumptions.ToListAsync();
        }

        public async Task<List<NewConsumption>> getNewConsumptions()
        {
            return await _database.NewConsumptions.ToListAsync();
        }

        public async Task<bool> addConsumption(Consumption consumption)
        {
            if (consumption == null) { 
                return false;
            }
            await _database.Consumptions.AddAsync(consumption);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteConsumption(int min)
        {
            Consumption data = await _database.Consumptions.FirstOrDefaultAsync(u => u.min.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Consumptions.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
