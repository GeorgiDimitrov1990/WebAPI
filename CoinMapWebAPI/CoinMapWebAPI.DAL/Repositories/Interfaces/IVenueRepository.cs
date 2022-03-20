using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        Task<Venue> FindByVenueNameAsync(string venueName);
        Task<List<Comment>> GetCommentsAsync(int venueId);
    }
}
