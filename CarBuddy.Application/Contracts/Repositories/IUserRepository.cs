using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        User CreateUser(User user);

        User GetUserById(Guid driverId);

        User GetDriverById(Guid id);

        User GetUserByEmail(string email);

        Car GetUserCar(Guid userId);

        void ActivateUser(User user);

        void UploadPhoto(User user, string photo);

        void DeleteUser(User user);
    }
}
