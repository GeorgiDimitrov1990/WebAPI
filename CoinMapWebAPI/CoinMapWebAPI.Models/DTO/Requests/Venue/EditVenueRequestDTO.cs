using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Models.DTO.Requests.Venue
{
    public class EditVenueRequestDTO
    {
        
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string Country { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string City { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        public string Address { get; set; }

        public int CategoryId { get; set; }
    }
}
