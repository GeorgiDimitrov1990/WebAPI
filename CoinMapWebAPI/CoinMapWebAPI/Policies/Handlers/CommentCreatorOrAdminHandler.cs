using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Policies.Handlers
{
    public class CommentCreatorOrAdminHandler : AuthorizationHandler<CommentCreatorOrAdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public CommentCreatorOrAdminHandler(IHttpContextAccessor httpContextAccessor, IUserService userService, ICommentService commentService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _commentService = commentService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CommentCreatorOrAdminRequirement requirement)
        {
            var currentUser = await _userService.GetCurrentUserAsync(context.User);

            if (currentUser == null)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;

            routeValues.TryGetValue("commentId", out object commentId);

            var comment = await _commentService.GetCommentById(int.Parse(commentId.ToString()));

            if (comment == null)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            if (await _userService.IsUserInRoleAsync(currentUser.Id, "Admin") || comment.CreatorId == currentUser.Id)
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
