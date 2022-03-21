using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Models.DTO.Requests.Category
{
    public class EditCategoryRequestDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string CategoryName { get; set; }
    }
}
