using System.Threading.Tasks;
using Veox.Attendance.User.Domain.Entities;

namespace Veox.Attendance.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAndEmail(string userId,string email);
        Task Create(UserEntity userEntity);
    }
}