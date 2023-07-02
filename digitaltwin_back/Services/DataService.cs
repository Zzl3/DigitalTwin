using auth.Database;
using auth.Models;
using auth.Utility;
using auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class DataManageService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public DataManageService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<List<Data>> getDatas()
        {
            return await _database.Datas.ToListAsync();
        }

        public async Task<bool> addData(Data data)
        {
            if (data == null||data.proper1==null) { 
                return false;
            }
            await _database.Datas.AddAsync(data);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteData(int id)
        {
            Data data = await _database.Datas.FirstOrDefaultAsync(u => u.id.Equals(id));
            if (data == null) { 
                return false;
            }
            _database.Datas.Remove(data);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateData(Data data)
        {
            if (data == null) { 
                return false;
            }
            // 从数据库中查找要更新
            var oldData = await _database.Datas.FindAsync(data.id);
            if (oldData == null) {
                return false;
            }
            // 更新信息
            oldData.proper1 = data.proper1;
            oldData.proper2 = data.proper2;
            oldData.proper3 = data.proper3;
            // 将更新后的数据保存到数据库中
            await _database.SaveChangesAsync();
            return true;
        }
    }
}
