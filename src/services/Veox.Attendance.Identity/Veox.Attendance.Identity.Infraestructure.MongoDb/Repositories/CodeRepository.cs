using System.Threading.Tasks;
using Veox.Attendance.Identity.Domain.Entities;
using Veox.Attendance.Identity.Domain.Repositories;
using Veox.Attendance.Identity.Infraestructure.MongoDb.Contexts.Interfaces;

namespace Veox.Attendance.Identity.Infraestructure.MongoDb.Repositories
{
    public class CodeRepository : ICodeRepository
    {
        private readonly IMongoDbContext _context;

        public CodeRepository(IMongoDbContext context)
        {
            _context = context;
        }
        
        public async Task<ActivationCodeEntity> Create(ActivationCodeEntity activationCodeEntity)
        {
            await _context.ActivationCodes.InsertOneAsync(activationCodeEntity);

            return activationCodeEntity;
        }
    }
}