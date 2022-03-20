using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
    class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException(int id) : base($"Comment with Id '{id}' was not found.")
        {

        }
    }
}
