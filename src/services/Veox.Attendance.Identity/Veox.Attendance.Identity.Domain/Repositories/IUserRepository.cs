using System.Threading.Tasks;
using Veox.Attendance.Identity.Domain.Entities;

namespace Veox.Attendance.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByEmail(string email);
        Task<UserEntity> Create(UserEntity userEntity);
    }
}