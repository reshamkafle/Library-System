using AutoMapper;
using LMS.API.Models;
using LMS.Core.Entities;
using LMS.Service.Identity;
using LMS.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<LoginDto, LoginViewModel>().ReverseMap();
            CreateMap<ChangePasswordDto, ChangePasswordViewModel>().ReverseMap();

            CreateMap<ApplicationUser, UserViewModelWithoutPassword>().ReverseMap();
            CreateMap<UserDto, UserViewModelWithoutPassword>().ReverseMap();

            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            CreateMap<MediaType, MediaTypeViewModel>().ReverseMap();
            CreateMap<Media, MediaViewModel>().ReverseMap();
            CreateMap<MediaDto, MediaViewModel>().ReverseMap();
            CreateMap<Checkout, CheckoutViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();
            CreateMap<HistoryDto, HistoryViewModel>().ReverseMap();

        }
    }
}
