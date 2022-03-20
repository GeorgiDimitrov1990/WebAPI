using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
    public class VenueNotFoundException : NotFoundException
    {
        public VenueNotFoundException(int id) : base($"Venue with Id '{id}' was not found.")
        {

        }
    }
}
