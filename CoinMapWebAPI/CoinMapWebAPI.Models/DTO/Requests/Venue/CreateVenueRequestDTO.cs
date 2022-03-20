using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Models.DTO.Requests.Venue
{
    public class CreateVenueRequestDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Address { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
