using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Requests.Venue;
using CoinMapWebAPI.Models.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/venues")]
    [Authorize]
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

        [Authorize (Policy = "VenueCreatorOrAdmin")]
        [HttpPut("{venueId}")]
        public async Task<IActionResult> EditVenue(EditVenueRequestDTO venue, int venueId)
        {
            await _venueService.EditVenueAsync(_mapper.Map<Venue>(venue), venueId, venue.CategoryId);

            return Ok();
        }

        [Authorize(Policy = "VenueCreatorOrAdmin")]
        [HttpDelete("{venueId}")]
        public async Task<IActionResult> DeleteVenue(int venueId)
        {
            await _venueService.DeleteVenueAsync(venueId);

            return Ok();
        }

        [HttpGet("{venueId}/comments")]
        public async Task<IActionResult> GetComments(int venueId)
        {
            List<Comment> comments = await _venueService.GetCommnetsAsync(venueId);

            return Ok(_mapper.Map<List<CommentResponseDTO>>(comments));
        }
    }
}
