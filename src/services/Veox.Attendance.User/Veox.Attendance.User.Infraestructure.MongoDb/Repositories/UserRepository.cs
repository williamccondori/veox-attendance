using System.Threading.Tasks;
using MongoDB.Driver;
using Veox.Attendance.User.Domain.Entities;
using Veox.Attendance.User.Domain.Repositories;
using Veox.Attendance.User.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.User.Infraestructure.MongoDb.Repositories
{
    public class UserRepository: IUserRepository
    {        
        private readonly IMongoDbContext _context;

        public UserRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetByIdAndEmail(string userId, string email)
        {
            var cursor = await _context.Users.FindAsync(
                x => x.Id.Equals(userId) && x.Email.Equals(email));

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task Create(UserEntity userEntity)
        {
            await _context.Users.InsertOneAsync(userEntity);
        }
    }
}