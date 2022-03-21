using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Models.DTO.Requests.Category
{
    public class CreateCategoryRequestDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
    }
}
