using AutoMapper;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentResponseDTO>().ReverseMap();
        }
    }
}
