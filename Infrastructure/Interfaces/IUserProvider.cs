using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserProvider
    {
        List<User> GetUsers();
        User? GetByEmail(string email);
        User? CreateUser(User request);
    }
}
