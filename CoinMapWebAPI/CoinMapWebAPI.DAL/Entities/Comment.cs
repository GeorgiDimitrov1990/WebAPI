using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Entities
{
    public class Comment : Entity 
    {
        public string Text { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
