using AutoMapper;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Requests.Venue;
using CoinMapWebAPI.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Profiles
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueResponseDTO>()
                .ForMember(dto => dto.CreationDate, c => c.MapFrom(r => r.CreationDate.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dto => dto.ModificationDate, c => c.MapFrom(r => r.ModificationDate.ToString("dd/MM/yyyy HH:mm:ss")));
            CreateMap<CreateVenueRequestDTO, Venue>().ReverseMap();
            CreateMap<EditVenueRequestDTO, Venue>().ReverseMap();
        }
    }
}
