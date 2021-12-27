using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class UserProvider : IUserProvider
    {
        private DatabaseContext _database;

        public UserProvider(DatabaseContext database)
        {
            _database = database;
        }

        public User? CreateUser(User request)
        {
            var user = _database.Users
                    .Where(u => u.Email == request.Email)
                    .FirstOrDefault();

            if(user != null)
                return null;

            _database.Users.Add(request);
            _database.SaveChanges();
            return request;
        }

        public User GetByEmail(string email)
        {
            var user = _database.Users
                .Where(u => u.Email == email)
                .FirstOrDefault();

            if (user == null)
                return null;

            user.LastLogin = DateTime.Now.ToUniversalTime();

            _database.Users.Update(user);
            _database.SaveChanges();

            return user;
        }

        public List<User> GetUsers()
        {
            return _database.Users.ToList();
        }
    }
}
