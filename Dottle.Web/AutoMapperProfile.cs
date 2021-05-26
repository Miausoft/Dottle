using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Web.Models;

namespace Dottle.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserViewModel, User>();
            CreateMap<CreatePostViewModel, Post>();
            CreateMap<Post, PostViewModel>();
            CreateMap<TimeSheet, TimeSheetViewModel>();
        }
    }
}
