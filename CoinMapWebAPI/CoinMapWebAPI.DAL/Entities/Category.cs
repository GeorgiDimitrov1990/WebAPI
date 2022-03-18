using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Entities
{
    public class Category : Entity
    {
        public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
    }
}
