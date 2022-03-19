using CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services
{
    public class VenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<Venue> CreateVenueAsync(Venue venue, string creatorId)
        {
            var venueByName = await _venueRepository.FindByVenueNameAsync(venue.Name);

            if (venueByName != null)
                throw new VenueAlreadyExistException(venue.Name);

            var venueToAdd = new Venue
            {
                CreatorId = creatorId,
                Country = venue.Country,
                City = venue.City,
                Address = venue.Address,
                Name = venue.Name
            };

            await _venueRepository.AddAsync(venueToAdd);

            return venueToAdd;
        }
    }
}
