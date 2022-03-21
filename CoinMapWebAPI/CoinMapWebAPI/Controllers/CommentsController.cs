using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Requests.Comment;
using CoinMapWebAPI.Models.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/comments")]
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> CreateComment(int venueId, CreateCommentRequestDTO comment)
        {
            var currentUser = await _userService.GetCurrentUserAsync(User);
            var createdComment = await _commentService.CreateCommentAsync(comment.Text, venueId, currentUser.Id);

            return CreatedAtAction(nameof(GetComment), new { commentId = createdComment.Id }, _mapper.Map<CommentResponseDTO>(createdComment));
        }
        
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetComment(int commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);

            return Ok(_mapper.Map<CommentResponseDTO>(comment));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment> comments = await _commentService.GetAllCommentsAsync();

            return Ok(_mapper.Map<List<CommentResponseDTO>>(comments));
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> EditComment(int commentId, EditCommentRequestDTO comment)
        {
            await _commentService.EditCommentAsync(commentId, comment.Text);

            return Ok();
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);

            return Ok();
        }
    }
}
