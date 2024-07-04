using PaparaBootcamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Interfaces
{
    public interface  IUserRepository
    {
       User Login(string userName, string password);
    }
}
