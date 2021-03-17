using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veox.Attendance.Identity.Application.Exceptions;
using Veox.Attendance.Identity.Application.Helpers;
using Veox.Attendance.Identity.Application.Models;
using Veox.Attendance.Identity.Application.Services.Interfaces;
using Veox.Attendance.Identity.Application.Services.Interfaces.Common;
using Veox.Attendance.Identity.Application.Validators;
using Veox.Attendance.Identity.Domain.Repositories;
using Veox.Attendance.Identity.Infraestructure.Jwt.Builders.Interfaces;

namespace Veox.Attendance.Identity.Application.Services.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IUserRepository _userRepository;

        public AuthService(
            IJwtBuilder jwtBuilder,
            IUserRepository userRepository)
        {
            _jwtBuilder = jwtBuilder;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            Validate(new AuthenticationRequestValidator(), authenticationRequest);

            var userEntity = await _userRepository.GetByEmail(authenticationRequest.Email);

            if (userEntity == null)
            {
                throw new ApiException("Su correo electrónico no está registrado en la plataforma");
            }

            var passwordEncrypted = EncryptorHelper.GetHash(authenticationRequest.Password, userEntity.PasswordKey);

            var isValid = userEntity.Password.Equals(passwordEncrypted);

            if (!isValid)
            {
                throw new ApiException("Las credenciales brindadas son incorrectas");
            }

            switch (userEntity.IsVerified)
            {
                case false:
                    throw new ApiException("NO_VERIFIED");
                case true when !userEntity.IsEnabled:
                    throw new ApiException("Se ha desabilitado su acceso a la plataforma");
            }

            var tokenContent = new Dictionary<string, string>
            {
                {"userId", userEntity.Id}
            };

            var token = _jwtBuilder.GenerateToken(tokenContent);

            return new AuthenticationResponse
            {
                Token = token
            };
        }

        public Task<bool> ValidateAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}