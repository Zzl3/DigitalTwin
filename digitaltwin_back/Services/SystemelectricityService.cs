using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class SystemelectricityService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public SystemelectricityService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Systemelectricity>> getSystemelectricitys()
        {
            return await _database.Systemelectricitys.ToListAsync();
        }

        public async Task<bool> addSystemelectricity(Systemelectricity Systemelectricity)
        {
            if (Systemelectricity == null) { 
                return false;
            }
            await _database.Systemelectricitys.AddAsync(Systemelectricity);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteSystemelectricity(int min)
        {
            Systemelectricity data = await _database.Systemelectricitys.FirstOrDefaultAsync(u => u.date.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Systemelectricitys.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
