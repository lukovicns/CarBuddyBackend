using CarBuddy.Application.Contracts;
using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Models;
using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public Result Register(User user)
        {
            var existingUser = _userRepository.GetUserByEmail(user.Email);

            if (!existingUser.IsEmpty)
                return new Result(Constants.UserAlreadyExists, 403);

            user.Password = _passwordHasher.Hash(user.Password);
            user.ActivationToken = Guid.NewGuid().ToString();
            var newUser = _userRepository.CreateUser(user);

            return new Result(new
            {
                Id = newUser.Id.ToString(),
                newUser.Email,
                newUser.ActivationToken
            }, 200);
        }

        public Result Login(Credentials credentials)
        {
            var user = _userRepository.GetUserByEmail(credentials.Email);

            if (user.IsEmpty)
                return new Result(Constants.UserNotFound, 403);

            if (!user.IsActivated)
                return new Result(Constants.UserNotActivated, 403);

            if (!_passwordHasher.Verify(user.Password, credentials.Password))
                return new Result(Constants.InvalidPassword, 403);

            var token = _jwtTokenGenerator.Generate(user);

            return new Result(new { Token = token }, 200);
        }

        public Result ConfirmEmail(string userId, string token)
        {
            var validator = new GuidValidator(userId);

            if (!validator.IsValid)
                return new Result(Constants.InvalidIdProvided, 403);

            var user = _userRepository.GetUserById(validator.Value);

            if (user.IsEmpty)
                return new Result(Constants.UserNotFound, 403);

            if (!(user.CreatedAt.AddMinutes(3).CompareTo(DateTime.Now) > 0) || !user.ActivationToken.Equals(token))
                return new Result(Constants.EmailConfirmationTokenIsInvalid, 403);

            if (user.IsActivated)
                return new Result(Constants.UserIsAlreadyActivated, 403);

            _userRepository.ActivateUser(user);
            return new Result(new { IsActivated = true }, 200);
        }
    }
}
