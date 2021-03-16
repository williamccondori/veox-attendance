using System.Linq;
using System.Threading.Tasks;
using Veox.Attendance.User.Application.Exceptions;
using Veox.Attendance.User.Application.Models;
using Veox.Attendance.User.Application.Services.Implementations.Common;
using Veox.Attendance.User.Application.Services.Interfaces;
using Veox.Attendance.User.Domain.Entities;
using Veox.Attendance.User.Domain.Repositories;

namespace Veox.Attendance.User.Application.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        //private readonly IUserProducer _userProducer;

        public UserService(
            IUserRepository userRepository
            //IUserProducer userProducer
        )
        {
            _userRepository = userRepository;
            //_userProducer = userProducer;
        }

        public async Task SaveAsync(SaveUserRequest saveUserRequest)
        {
            var existingUser = await _userRepository.GetByIdAndEmail(saveUserRequest.Id, saveUserRequest.Email);

            if (existingUser != null)
            {
                throw new ApiException("A user has already registered with this ID");
            }

            var initials = string.Concat(
                saveUserRequest.Name?.First(), 
                saveUserRequest.LastName?.First());
            
            var imageProfile = GetImageProfile(initials.ToUpper());
            
            var userEntity = UserEntity.Create(saveUserRequest.Id, saveUserRequest.Name, saveUserRequest.LastName,
                saveUserRequest.Email, imageProfile, saveUserRequest.Id);

            await _userRepository.Create(userEntity);
        }
    }
}