using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarBuddyContext _context;

        public UserRepository(CarBuddyContext context) => _context = context;

        public User GetUserById(Guid driverId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == driverId)
                   ?? User.Empty;
        }

        public User GetDriverById(Guid driverId)
        {
            return _context.Users
                       .Include(u => u.Car)
                       .Include(u => u.RecipientRatings)
                       .AsEnumerable()
                       .SingleOrDefault(u => u.Id == driverId && u.IsDriver)
                   ?? User.Empty;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email)
                   ?? User.Empty;
        }

        public Car GetUserCar(Guid userId)
        {
            return _context.Cars.FirstOrDefault(c => c.DriverId == userId)
                   ?? Car.Empty;
        }

        public User CreateUser(User user)
        {
            user.CreatedAt = DateTime.Now;
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void ActivateUser(User user)
        {
            user.IsActivated = true;
            _context.Update(user);
            _context.SaveChanges();
        }

        public void UploadPhoto(User user, string photo)
        {
            user.Photo = photo;
            _context.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }
    }
}
