using System.Threading.Tasks;
using Veox.Attendance.User.Domain.Entities;

namespace Veox.Attendance.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetById(string userId);
        Task<UserEntity> GetByIdAndEmail(string userId,string email);
        Task<UserEntity> Update(string userId, UserEntity userEntity);
        Task Create(UserEntity userEntity);
    }
}