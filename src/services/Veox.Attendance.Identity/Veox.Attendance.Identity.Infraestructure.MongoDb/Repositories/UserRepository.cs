using System.Threading.Tasks;
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

        public Task<UserEntity> GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserEntity> Create(UserEntity userEntity)
        {
            await _context.Users.InsertOneAsync(userEntity);

            return userEntity;
        }
    }
}