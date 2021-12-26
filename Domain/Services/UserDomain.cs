using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Domain.Services
{
    public class UserDomain
    {
        private readonly IUserProvider _userProvider;
        private readonly IHashProvider _hashProvider;

        public UserDomain(IUserProvider userProvider, IHashProvider hashProvider)
        {
            _userProvider = userProvider;
            _hashProvider = hashProvider;
        }

        public List<User> GetUsers()
        {
            return _userProvider.GetUsers();
        }

        public User CreateUser(UserRequest request)
        {
            var dateTimeNow = DateTime.Now.ToUniversalTime();
            string password = _hashProvider.GenerateHash(request.Password);

            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                Password = password,
                Created = dateTimeNow,
                LastLogin = dateTimeNow,
                Modified = dateTimeNow,
            };

            var response = _userProvider.CreateUser(user);

            if(user == null)
                throw new Exception("This user already exists");

            return response;            
        }
    }
}
