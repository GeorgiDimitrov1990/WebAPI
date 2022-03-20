using CoinMapWebAPI.BLL.Services.Exceptions.NotFound;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IVenueRepository _venueRepository;

        public CommentService(ICommentRepository commentRepository, IVenueRepository venueRepository)
        {
            _commentRepository = commentRepository;
            _venueRepository = venueRepository;
        }

        public async Task<Comment> CreateCommentAsync(string text, int venueId, string userId)
        {
            var venue = await _venueRepository.GetByIdAsync(venueId);

            if (venue == null)
                throw new VenueNotFoundException(venueId);

            var comment = new Comment()
            {
                Text = text,
                Venue = venue,
                CreatorId = userId
            };

            await _commentRepository.AddAsync(comment);

            return comment;
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            if(comment == null)
                throw new CommentNotFoundException(commentId);

            return comment;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task EditCommentAsync(int commentId, string text)
        {
            var comment = await GetCommentById(commentId);

            comment.ModificationDate = DateTime.Now;
            comment.Text = text;

            await _commentRepository.EditAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await GetCommentById(commentId);

            await _commentRepository.DeleteAsync(comment);
        }
    }
}
