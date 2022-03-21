using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Policies.Requirements
{
    public class CommentCreatorOrAdminRequirement : IAuthorizationRequirement
    {
        public CommentCreatorOrAdminRequirement()
        {

        }
    }
}
