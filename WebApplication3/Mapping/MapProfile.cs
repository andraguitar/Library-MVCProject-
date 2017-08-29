using System;
using AutoMapper;
using BLL.Entities;
using WebApplication3.ViewModels;

namespace WebApplication3.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BookEntity, BookViewModel>();
            CreateMap<BookViewModel, BookEntity>();
            CreateMap<RegisterViewModel, UserEntity>();
            CreateMap<OrderEntity, OrderViewModel>();
            CreateMap<OrderViewModel, OrderEntity>().ForMember("DateOrder",o=>o.MapFrom(c=>DateTime.UtcNow))
                .ForMember("DateDelivery",p=>p.MapFrom(o=>o.DateDelivery.AddHours(12)));
            CreateMap<ReaderEntity, ReaderViewModel>();
            CreateMap<ReaderViewModel, ReaderEntity>();
            CreateMap<UserEntity, UserViewModel>();
            CreateMap<UserViewModel, UserEntity>();
        }
    }
}