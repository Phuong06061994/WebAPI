using AutoMapper;
using DAL.Dto;
using DAL.Request;
using DAL.Response;

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
