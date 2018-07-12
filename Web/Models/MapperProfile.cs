using System;
using AutoMapper;
using Model;
using Web.ViewModels.Admin;
using Web.ViewModels.Post;

namespace Web.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            Mapper.Initialize(config => {
                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<Comment, CommentViewModel>();
            });
        }
    }
}