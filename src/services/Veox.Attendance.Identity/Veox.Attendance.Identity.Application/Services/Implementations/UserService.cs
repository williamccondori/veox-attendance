using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Contexts.Interfaces;
using Veox.Attendance.Identity.Application.Exceptions;
using Veox.Attendance.Identity.Application.Helpers;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Interfaces;
using Veox.Attendance.Identity.Application.Services.Interfaces.Common;
using Veox.Attendance.Identity.Application.Validators;
using Veox.Attendance.Identity.Domain.Entities;
using Veox.Attendance.Identity.Domain.Repositories;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Models.User;
using Veox.Attendance.Identity.Infraestructure.RabbitMq.Producers.Interfaces;

namespace Veox.Attendance.Identity.Application.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProducer _userProducer;

        public UserService(
            IApplicationContext context,
            IUserRepository userRepository,
            IUserProducer userProducer) : base(context)
        {
            _userRepository = userRepository;
            _userProducer = userProducer;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            Validate(new RegisterRequestValidator(), registerRequest);

            var existingUser = await _userRepository.GetByEmail(registerRequest.Email);

            if (existingUser != null)
            {
                throw new ApiException("A user has already registered with this email");
            }

            var passwordKey = EncryptorHelper.GetPasswordKey();
            var password = EncryptorHelper.GetHash(registerRequest.Password, passwordKey);

            var userEntity = UserEntity.Create(registerRequest.Email, password, passwordKey);
            var newUser = await _userRepository.Create(userEntity);

            #region Not implemented code.

            // Creation of activation code [Not implement in this version].
            // var code = RandomHelper.GenerateActivationCode();
            // var activationCode = ActivationCodeEntity.Create(newUser.Id, code);
            //
            // var newActivationCode = await _codeRepository.Create(activationCode);
            //
            // var activationCodeEmail = new ActivationCodeEmail
            // {
            //     Email = newUser.Email,
            //     EmailType = EmailType.ActivationCode,
            //     ActivationCode = newActivationCode.Code,
            //     FullName = newUser.FullName
            // };
            // _emailProducer.SendActivationCode(activationCodeEmail);

            #endregion

            _userProducer.Save(new SaveUserRequest
            {
                Id = newUser.Id,
                Email = newUser.Email,
                Name = registerRequest.Name,
                LastName = registerRequest.LastName
            });

            return new RegisterResponse
            {
                Email = newUser.Email
            };
        }
    }
}