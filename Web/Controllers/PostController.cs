using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoMapper;
using BLL;
using Helper;
using Model;
using Web.ViewModels.Post;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private AppSettings _appSettings;
        private IMapper _mapper;
        private PostBLL _post;
        private UserManager<User> _userManager;

        public PostController(
            IMapper mapper, 
            PostBLL post,
            UserManager<User> userManager,
            IOptionsSnapshot<AppSettings> appSettingsAccessor)
        {
            _appSettings = appSettingsAccessor.Value;
            _mapper = mapper;
            _post = post;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index ()
        {
            IEnumerable<PostViewModel> model = null;

            var recentPosts = await _post.GetRecentPostsAsync(5); 
            if (recentPosts != null && recentPosts.Count() != 0)
            {
                model = _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(recentPosts);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData[Constants.FormSubmit] = new FormSubmit
            {
                Result = null,
                IndexUrl = Url.Action(nameof(Index), "Post")
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddPostViewModel model)
        {
            FormSubmit submitInfo = new FormSubmit
            {
                IndexUrl = Url.Action(nameof(Index), "Post")
            };

            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    BlogId = _appSettings.BlogID,
                    Title = model.Title,
                    Content = model.Content,
                    RowCreatedBy = Convert.ToInt32(_userManager.GetUserId(HttpContext.User)),
                    RowModifiedBy = Convert.ToInt32(_userManager.GetUserId(HttpContext.User)),
                    RowCreatedDateTime = DateTime.Now,
                    RowModifiedDateTime = DateTime.Now
                };

                Result result = new Result();
                
                try
                {
                    await _post.AddPostAsync(post);
                    
                    result.Succeeded = true;
                    result.Message = "Post added.";
                }
                catch (Exception ex)
                {
                    result.Message = "Unable to add post at this time.";

                    SysException exception = new SysException(ex)
                    {
                        Url = UrlHelper.GetRequestUrl(HttpContext)
                    };
                }

                submitInfo.Result = result;
            }

            ViewData[Constants.FormSubmit] = submitInfo;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Post post = await _post.GetPostAsync(id);

            var model = _mapper.Map<Post, PostViewModel>(post);

            return View(model);
        }
    }
}