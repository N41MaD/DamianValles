using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightLifting.Library.Business.Interfaces
{
    public interface IAuthService
    {
        bool IsValidUser(string username, string password);
        string CreateToken(string userName);
    }
}
