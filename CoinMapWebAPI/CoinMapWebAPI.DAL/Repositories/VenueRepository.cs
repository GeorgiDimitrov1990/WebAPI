using CoinMapWebAPI.DAL.Data;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories
{
    public class VenueRepository : Repository<Venue>, IVenueRepository
    {
        public VenueRepository(DatabaseContext context) : base(context)
        {

        }

        public async Task<Venue> FindByVenueNameAsync(string venueName)
        {
            return await _context.Venues.FirstOrDefaultAsync(v => v.Name.Equals(venueName));
        }

        public async Task<List<Comment>> GetCommentsAsync(int venueId)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.Id == venueId);

            return venue.Comments.ToList();
        }
    }
}
