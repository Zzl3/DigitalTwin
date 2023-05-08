using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class SystemairService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public SystemairService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Systemair>> getSystemairs()
        {
            return await _database.Systemairs.ToListAsync();
        }

        public async Task<bool> addSystemair(Systemair Systemair)
        {
            if (Systemair == null) { 
                return false;
            }
            await _database.Systemairs.AddAsync(Systemair);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteSystemair(string min)
        {
            Systemair data = await _database.Systemairs.FirstOrDefaultAsync(u => u.air.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Systemairs.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
