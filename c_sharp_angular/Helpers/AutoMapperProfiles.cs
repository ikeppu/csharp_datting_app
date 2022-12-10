using System;
using AutoMapper;
using c_sharp_angular.DTOs;
using c_sharp_angular.Entities;
using c_sharp_angular.Extensions;
using c_sharp_angular.Interfaces;

namespace c_sharp_angular.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                opt.MapFrom(src => src.Photos.FirstOrDefault(x =>
                x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto, AppUser>();

            CreateMap<Message, MessageDto>()
                .ForMember(d => d.SenderPhotoUrl, o =>
                    o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x =>
                    x.IsMain).Url))
                .ForMember(d => d.RecipientPhotoUrl, o =>
                    o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x =>
                    x.IsMain).Url));
        }
    }
}

