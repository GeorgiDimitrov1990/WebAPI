using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public CommentsController(IMapper mapper, ICommentService commentService, IUserService userService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _userService = userService;
        }
    }
}
