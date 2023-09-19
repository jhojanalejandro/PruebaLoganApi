using AutoMapper;
using PruebaLogan.MODEL.Dto;
using PruebaLogan.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLogan.MODEL.Mapper
{
    public class Automaping : Profile
    {
        public Automaping()
        {
            CreateMap<UserDto, UserT>()
                .ForMember(c => c.Id, cd => cd.MapFrom(src => Guid.Parse(src.Id))).ReverseMap();
            CreateMap<UserTypeDto, UserType>().ReverseMap();
        }
    }
}
