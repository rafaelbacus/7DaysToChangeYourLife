using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL;
using Model;
using Web.Models.Admin;

namespace Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private BlogBLL _blog;
        private IMapper _mapper;

        public AdminController(BlogBLL blog,
                               IMapper mapper)
        {
            _blog = blog;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel();

            try
            {
                (IEnumerable<Post> posts, IEnumerable<Comment> comments) recentResults = await _blog.GetRecentPostsAndCommentsAsync(count: 5);
                model.Posts = _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(recentResults.posts);
                model.Comments = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(recentResults.comments);
            }
            catch (Exception ex)
            {
                // Log Exception
            }

            return View(model);
        }
    }
}