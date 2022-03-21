using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Exceptions.Unauthorized
{
    public class UnauthorizedLoginException : Exception
    {
        public UnauthorizedLoginException(string message) : base(message)
        {
        }
    }
}
