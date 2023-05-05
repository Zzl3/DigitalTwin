using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class AirelectricityService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public AirelectricityService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Airelectricity>> getAirelectricitys()
        {
            return await _database.Airelectricitys.ToListAsync();
        }

        public async Task<bool> addAirelectricity(Airelectricity Airelectricity)
        {
            if (Airelectricity == null) { 
                return false;
            }
            await _database.Airelectricitys.AddAsync(Airelectricity);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteAirelectricity(string min)
        {
            Airelectricity data = await _database.Airelectricitys.FirstOrDefaultAsync(u => u.total.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Airelectricitys.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
