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
                config.CreateMap<AddCommentViewModel, Comment>()
                    .ForMember(dest => dest.PostId, o => o.MapFrom(src => src.PostId))
                    .ForMember(dest => dest.Author, o => o.MapFrom(src => src.Author))
                    .ForMember(dest => dest.Content, o => o.MapFrom(src => src.Content))
                    .ForAllOtherMembers(o => o.Ignore());
                config.CreateMap<Comment, CommentViewModel>();
                config.CreateMap<IEnumerable<Comment>, IEnumerable<CommentViewModel>>();
            });
        }
    }
}