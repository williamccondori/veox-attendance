using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veox.Attendance.User.Application.Contexts.Interfaces;
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

        public UserService(
            IApplicationContext context,
            IUserRepository userRepository
        ) : base(context)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> GetAsync()
        {
            var user = await GetUserById(_context.UserId);

            return new UserResponse
            {
                User = new UserModelResponse
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    Email = user.Email,
                    ImageProfile = user.ImageProfile
                }
            };
        }

        public async Task<UserResponse> GetByIdAsync(string userId)
        {
            var user = await GetUserById(_context.UserId);

            return new UserResponse
            {
                User = new UserModelResponse
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    Email = user.Email,
                    ImageProfile = user.ImageProfile
                }
            };
        }

        public async Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest userRequest)
        {
            var user = await GetUserById(_context.UserId);
            user.Name = userRequest.Name;
            user.LastName = userRequest.LastName;
            user.Update(_context.UserId);

            var userUpdated = await _userRepository.Update(user.Id, user);

            return new UserUpdateResponse
            {
                Name = userUpdated.Name,
                LastName = userUpdated.LastName,
                FullName = userUpdated.FullName,
                Email = userUpdated.Email,
                ImageProfile = userUpdated.ImageProfile
            };
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

        private async Task<UserEntity> GetUserById(string userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("The user has not been found");
            }

            return user;
        }
    }
}