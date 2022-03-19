using CoinMapWebAPI.DAL.Data;
using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories
{
    public class VenueRepository : Repository<Venue>
    {
        public VenueRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
