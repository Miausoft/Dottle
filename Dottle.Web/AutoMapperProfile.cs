using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Web.Models.User;

namespace Dottle.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateViewModel, User>();
        }
    }
}
