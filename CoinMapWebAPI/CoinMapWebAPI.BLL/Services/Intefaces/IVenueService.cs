using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Intefaces
{
    public interface IVenueService
    {
        Task<Venue> CreateVenueAsync(Venue venue, int categoryId, string creatorId);
        Task<Venue> GetVenueByIdAsync(int id);
        Task<List<Venue>> GetAllVenuesAsync();
        Task EditVenueAsync(Venue venue, int venueId, int categoryId);
        Task DeleteVenueAsync(int venueId);
        Task<List<Comment>> GetCommnetsAsync(int venueId);
    }
}
