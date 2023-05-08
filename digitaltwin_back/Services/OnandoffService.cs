using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class OnandoffService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public OnandoffService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Onandoff>> getOnandoffs()
        {
            return await _database.Onandoffs.ToListAsync();
        }

        public async Task<List<NewOnandoff>> getNewOnandoffs()
        {
            return await _database.NewOnandoffs.ToListAsync();
        }
        public async Task<bool> addOnandoff(Onandoff onandoff)
        {
            if (onandoff == null) { 
                return false;
            }
            await _database.Onandoffs.AddAsync(onandoff);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteOnandoff(int min)
        {
            Onandoff data = await _database.Onandoffs.FirstOrDefaultAsync(u => u.min.Equals(min));
            if (data == null) { 
                return false;
            }
            _database.Onandoffs.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
