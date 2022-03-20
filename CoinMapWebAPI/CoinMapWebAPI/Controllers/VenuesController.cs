using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Requests.Venue;
using CoinMapWebAPI.Models.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/venues")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVenueService _venueService;
        private readonly IUserService _userService;

        public VenuesController(IMapper mapper, IVenueService venueService, IUserService userService)
        {
            _mapper = mapper;
            _venueService = venueService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(CreateVenueRequestDTO venue)
        {
            var currentUser = await _userService.GetCurrentUserAsync(User);

            var createdVenue = await _venueService.CreateVenueAsync(_mapper.Map<Venue>(venue), venue.CategoryId, currentUser.Id);

            return CreatedAtAction(nameof(GetVenue), new { venueId = createdVenue.Id }, _mapper.Map<VenueResponseDTO>(createdVenue));
        }

        [HttpGet("{venueId}")]
        public async Task<IActionResult> GetVenue(int venueId)
        {
            var venue = await _venueService.GetVenueByIdAsync(venueId);

            return Ok(_mapper.Map<VenueResponseDTO>(venue));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVenues()
        {
            List<Venue> venues = await _venueService.GetAllVenuesAsync();

            return Ok(_mapper.Map<List<VenueResponseDTO>>(venues));
        }

        [HttpPut("{venueId}")]
        public async Task<IActionResult> EditCategory(EditVenueRequestDTO venue, int venueId)
        {
            await _venueService.EditVenueAsync(_mapper.Map<Venue>(venue), venueId, venue.CategoryId);

            return Ok();
        }

        [HttpDelete("{venueId}")]
        public async Task<IActionResult> DeleteCategory(int venueId)
        {
            await _venueService.DeleteVenueAsync(venueId);

            return Ok();
        }

        [HttpGet("{venueId}/comments")]
        public async Task<IActionResult> GetComments(int venueId)
        {
            List<Comment> comments = await _venueService.GetCommnetsAsync(venueId);
            //Have to switch to CommentResponseDTO
            return Ok(_mapper.Map<List<CategoryResponseDTO>>(comments));
        }
    }
}
