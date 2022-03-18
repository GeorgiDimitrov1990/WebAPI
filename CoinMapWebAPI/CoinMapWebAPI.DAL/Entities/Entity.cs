using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Entities
{
    public class Entity
    {
        public Entity()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
