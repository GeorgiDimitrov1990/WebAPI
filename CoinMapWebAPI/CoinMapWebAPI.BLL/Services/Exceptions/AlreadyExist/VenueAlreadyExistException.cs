using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist
{
    public class VenueAlreadyExistException : AlreadyExistsException
    {
        public VenueAlreadyExistException(string name) : base($"Venue with name '{name}' already exist!")
        {

        }
    }
}
