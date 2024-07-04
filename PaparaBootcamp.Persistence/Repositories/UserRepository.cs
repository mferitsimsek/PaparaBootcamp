using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Persistence.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { Username = "admin@admin.com", Password = "password" }
        };

        User IUserRepository.Login(string userName, string password)
        {
            return _users.FirstOrDefault(u => u.Username == userName && u.Password == password);
           
        }

    }
}
