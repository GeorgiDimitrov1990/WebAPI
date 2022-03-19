using CoinMapWebAPI.DAL.Data;
using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(DatabaseContext context) : base (context)
        {

        }
    }
}
