using System.Collections.Generic;
using AutoMapper;
using Model;
using Web.ViewModels.Comment;
using Web.ViewModels.Post;

namespace Web.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            Mapper.Initialize(config => {
                // Post
                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<Post, AddPostViewModel>().ReverseMap();

                // Comment
                config.CreateMap<Comment, CommentViewModel>();
                config.CreateMap<IEnumerable<Comment>, IEnumerable<CommentViewModel>>();
            });
        }
    }
}