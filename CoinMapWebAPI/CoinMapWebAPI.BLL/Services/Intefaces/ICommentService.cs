using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Intefaces
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(string text, int venueId, string userId);
        Task<Comment> GetCommentById(int commentId);
        Task<List<Comment>> GetAllCommentsAsync();
        Task EditCommentAsync(int commentId, string text);
        Task DeleteCommentAsync(int commentId);
    }
}
