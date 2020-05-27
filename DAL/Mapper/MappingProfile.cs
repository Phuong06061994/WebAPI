using AutoMapper;
using DAL.Repository.Impl;
using DAL.Request;
using DAL.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Entities;

namespace DAL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsRequest, News>();
            CreateMap<News, NewsResponse>();
        }
    }
}
