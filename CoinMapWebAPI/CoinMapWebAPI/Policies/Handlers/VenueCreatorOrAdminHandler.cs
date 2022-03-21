using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Policies.Handlers
{
    public class VenueCreatorOrAdminHandler : AuthorizationHandler<VenueCreatorOrAdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IVenueService _venueService;

        public VenueCreatorOrAdminHandler(IHttpContextAccessor httpContextAccessor, IUserService userService, IVenueService venueService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _venueService = venueService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, VenueCreatorOrAdminRequirement requirement)
        {
            var currentUser = await _userService.GetCurrentUserAsync(context.User);

            if (currentUser == null)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;

            routeValues.TryGetValue("venueId", out object venueId);

            var venue = await _venueService.GetVenueByIdAsync(int.Parse(venueId.ToString()));

            if (venue == null)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            if (await _userService.IsUserInRoleAsync(currentUser.Id, "Admin") || venue.CreatorId == currentUser.Id)
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
                return;
            }

            context.Fail();
            await Task.CompletedTask;
            return;
        }
    }
}
