using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;

namespace Domain.Services
{
    public class LoginDomain
    {
        private readonly IUserProvider _userProvider;
        private readonly IHashProvider _hashProvider;

        public LoginDomain(IUserProvider userProvider, IHashProvider hashProvider)
        {
            _userProvider = userProvider;
            _hashProvider = hashProvider;
        }

        public User Login(LoginRequest request)
        {
            User user = _userProvider.GetByEmail(request.Email);

            if (user == null)
                throw new Exception("User and/or password is invalid");

            string password = _hashProvider.GenerateHash(request.Password);

            if (user.Password != password)
                throw new UnauthorizedAccessException("User and/or password is invalid");

            return user;
        }
    }
}
