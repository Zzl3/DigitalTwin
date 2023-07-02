using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class UserloadingService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public UserloadingService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Userloading>> getUserloadings()
        {
            return await _database.Userloadings.ToListAsync();
        }

        public async Task<bool> addUserloading(Userloading Userloading)
        {
            if (Userloading == null) { 
                return false;
            }
            await _database.Userloadings.AddAsync(Userloading);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteUserloading(string time)
        {
            Userloading data = await _database.Userloadings.FirstOrDefaultAsync(u => u.time.Equals(time));
            if (data == null) { 
                return false;
            }
            _database.Userloadings.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
