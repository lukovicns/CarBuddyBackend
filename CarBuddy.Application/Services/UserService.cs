using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public User GetUserById(Guid id) => _userRepository.GetUserById(id);

        public User GetDriverById(Guid id) => _userRepository.GetDriverById(id);

        public Car GetUserCar(Guid userId) => _userRepository.GetUserCar(userId);

        public void UploadPhoto(User user, string photo) => _userRepository.UploadPhoto(user, photo);

        public void DeleteUser(User user) => _userRepository.DeleteUser(user);
    }
}
