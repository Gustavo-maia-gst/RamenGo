using AutoMapper;
using RamenGo.Api.DTOs;
using RamenGo.Domain.Entities;

namespace RamenGo.Api.Profiles
{
    public class RamenGoProfile : Profile
    {
        public RamenGoProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<Broth, OrderItemDto>()
                .ReverseMap();
            CreateMap<Protein, OrderItemDto>()
                .ReverseMap();
        }
    }
}
