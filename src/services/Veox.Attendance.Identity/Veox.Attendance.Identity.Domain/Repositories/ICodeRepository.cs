using System.Threading.Tasks;
using Veox.Attendance.Identity.Domain.Entities;

namespace Veox.Attendance.Identity.Domain.Repositories
{
    public interface ICodeRepository
    {
        Task<ActivationCodeEntity> Create(ActivationCodeEntity activationCodeEntity);
    }
}