using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class UserusingService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public UserusingService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Userusing>> getUserusings()
        {
            return await _database.Userusings.ToListAsync();
        }

        public async Task<bool> addUserusing(Userusing Userusing)
        {
            if (Userusing == null) { 
                return false;
            }
            await _database.Userusings.AddAsync(Userusing);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteUserusing(int user)
        {
            Userusing data = await _database.Userusings.FirstOrDefaultAsync(u => u.user.Equals(user));
            if (data == null) { 
                return false;
            }
            _database.Userusings.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
