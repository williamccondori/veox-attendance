using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.Identity.Domain.Entities;
using Veox.Attendance.Identity.Domain.Repositories;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.Identity.Infraestructure.MongoDb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDbContext _context;

        public UserRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            var cursor = await _context.Users.FindAsync(x => x.Email.Equals(email));

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<UserEntity> Create(UserEntity userEntity)
        {
            await _context.Users.InsertOneAsync(userEntity);

            return userEntity;
        }
    }
}