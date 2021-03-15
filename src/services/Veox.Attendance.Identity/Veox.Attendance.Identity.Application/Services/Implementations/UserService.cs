using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Exceptions;
using Veox.Attendance.Identity.Application.Helpers;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Interfaces;
using Veox.Attendance.Identity.Domain.Entities;
using Veox.Attendance.Identity.Domain.Repositories;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.Common;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces;

namespace Veox.Attendance.Identity.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICodeRepository _codeRepository;
        private readonly IEmailProducer _emailProducer;

        public UserService(
            ICodeRepository codeRepository,
            IUserRepository userRepository,
            IEmailProducer emailProducer)
        {
            _userRepository = userRepository;
            _codeRepository = codeRepository;
            _emailProducer = emailProducer;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest authRegister)
        {
            var existingUser = await _userRepository.GetByEmail(authRegister.Email);

            if (existingUser != null)
            {
                throw new ApiException("A user has already registered with this email");
            }

            var passwordKey = EncryptorHelper.GetPasswordKey();
            var password = EncryptorHelper.GetHash(authRegister.Password, passwordKey);

            var userEntity = UserEntity
                .Create(authRegister.Name, authRegister.LastName, authRegister.Email, password, passwordKey);
            var newUser = await _userRepository.Create(userEntity);

            var code = RandomHelper.GenerateActivationCode();
            var activationCode = ActivationCodeEntity.Create(newUser.Id, code);

            var newActivationCode = await _codeRepository.Create(activationCode);

            var activationCodeEmail = new ActivationCodeEmail
            {
                Email = newUser.Email,
                EmailType = EmailType.ActivationCode,
                ActivationCode = newActivationCode.Code,
                FullName = newUser.FullName
            };

            _emailProducer.SendActivationCode(activationCodeEmail);

            return new RegisterResponse
            {
                Name = newUser.Name,
                LastName = newUser.LastName,
                FullName = newUser.FullName,
                Email = newUser.Email
            };
        }
    }
}