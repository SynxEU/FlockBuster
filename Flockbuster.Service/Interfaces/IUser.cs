using Flockbuster.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Service.Interfaces
{
    public interface IUser
    {
        List<Users> GetUsers();
    }
}
