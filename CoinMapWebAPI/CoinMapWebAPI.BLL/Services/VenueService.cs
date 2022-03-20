using CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist;
using CoinMapWebAPI.BLL.Services.Exceptions.NotFound;
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
        private readonly ICategoryRepository _categoryRepository;

        public VenueService(IVenueRepository venueRepository, ICategoryRepository categoryRepository)
        {
            _venueRepository = venueRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Venue> CreateVenueAsync(Venue venue, int categoryId, string creatorId)
        {
            var venueByName = await _venueRepository.FindByVenueNameAsync(venue.Name);

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (venueByName != null)
                throw new VenueAlreadyExistException(venue.Name);

            if (category == null)
                throw new CategoryNotFoundException(categoryId);

            var venueToAdd = new Venue
            {
                CreatorId = creatorId,
                Country = venue.Country,
                City = venue.City,
                Address = venue.Address,
                Name = venue.Name,
                Category = category
            };

            await _venueRepository.AddAsync(venueToAdd);

            return venueToAdd;
        }

        public async Task<Venue> GetVenueByIdAsync(int id)
        {
            var venue = await _venueRepository.GetByIdAsync(id);

            if (venue == null)
                throw new VenueNotFoundException(id);

            return venue;
        }

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            return await _venueRepository.GetAllAsync();
        }

        public async Task EditVenueAsync(Venue venue, int venueId, int categoryId)
        {
            var venueToEdit = await this.GetVenueByIdAsync(venueId);
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            venueToEdit.ModificationDate = DateTime.Now;
            venueToEdit.Name = string.IsNullOrEmpty(venue.Name) ? venueToEdit.Name : venue.Name;
            venueToEdit.City = string.IsNullOrEmpty(venue.City) ? venueToEdit.City : venue.City;
            venueToEdit.Country = string.IsNullOrEmpty(venue.Country) ? venueToEdit.Country : venue.Country;
            venueToEdit.Address = string.IsNullOrEmpty(venue.Address) ? venueToEdit.Address : venue.Address;
            venueToEdit.Category = string.IsNullOrEmpty(categoryId.ToString()) ? venueToEdit.Category : category;

            await _venueRepository.EditAsync(venueToEdit);

        }

        public async Task DeleteVenueAsync(int venueId)
        {
            var venue = await this.GetVenueByIdAsync(venueId);

            await _venueRepository.DeleteAsync(venue);
        }

        public async Task<List<Comment>> GetCommnetsAsync(int venueId)
        {
            var venue = await this.GetVenueByIdAsync(venueId);

            return await _venueRepository.GetCommentsAsync(venueId);
        }
    }
}
