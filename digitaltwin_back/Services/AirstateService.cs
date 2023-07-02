using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class AirstateService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public AirstateService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Airstate>> getAirstates()
        {
            return await _database.Airstates.ToListAsync();
        }

        public async Task<bool> addAirstate(Airstate Airstate)
        {
            if (Airstate == null) { 
                return false;
            }
            await _database.Airstates.AddAsync(Airstate);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteAirstate(string min)
        {
            Airstate data = await _database.Airstates.FirstOrDefaultAsync(u => u.air.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Airstates.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
