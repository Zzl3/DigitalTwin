using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class ElectricityfeeService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public ElectricityfeeService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Electricityfee>> getElectricityfees()
        {
            return await _database.Electricityfees.ToListAsync();
        }

        public async Task<bool> addElectricityfee(Electricityfee Electricityfee)
        {
            if (Electricityfee == null) { 
                return false;
            }
            await _database.Electricityfees.AddAsync(Electricityfee);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteElectricityfee(int min)
        {
            Electricityfee data = await _database.Electricityfees.FirstOrDefaultAsync(u => u.Feetoday.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Electricityfees.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
