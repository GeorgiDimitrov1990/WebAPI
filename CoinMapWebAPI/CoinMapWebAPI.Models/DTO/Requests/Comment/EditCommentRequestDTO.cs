using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Models.DTO.Requests.Comment
{
    public class EditCommentRequestDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Text { get; set; }
    }
}
