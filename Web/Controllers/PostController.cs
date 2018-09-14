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
using Web.ViewModels.Admin;

namespace Web.Controllers
{
    [Authorize(Roles = Constants.AdminRole)]
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
                    RowCreatedBy = Convert.ToInt32(_userManager.GetUserId(User)),
                    RowModifiedBy = Convert.ToInt32(_userManager.GetUserId(User)),
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
            ViewData[Constants.FormSubmit] = new FormSubmit
            {
                Result = null,
                IndexUrl = Url.Action(nameof(Index), "Post")
            };

            Post post = await _post.GetPostAsync(id);
            var model = _mapper.Map<Post, EditPostViewModel>(post);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            FormSubmit submitInfo = new FormSubmit
            {
                IndexUrl = Url.Action(nameof(Index), "Post")
            };

            if (ModelState.IsValid)
            {
                Post post = await _post.GetPostAsync(model.Id);
                post.Title = model.Title;
                post.Content = model.Content;
                post.RowModifiedBy = Convert.ToInt32(_userManager.GetUserId(User));
                post.RowModifiedDateTime = DateTime.Now;

                Result result = new Result();
                
                try
                {
                    await _post.EditPostAsync(post);
                    
                    result.Succeeded = true;
                    result.Message = "Post edited.";
                }
                catch (Exception ex)
                {
                    result.Message = "Unable to edit post at this time.";

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
        public async Task<IActionResult> Delete(int id = 0)
        {
            ViewData[Constants.FormSubmit] = new FormSubmit
            {
                Id = id,
                Result = null,
                CancelUrl = Url.Action(nameof(Edit), "Post", new { id }),
                DeleteUrl = Url.Action(nameof(Delete), "Post", new { id }),
                IndexUrl = Url.Action(nameof(Index), "Post")
            };

            var post = await _post.GetPostAndCommentsAsync(id);

            DeletePostViewModel model = new DeletePostViewModel
            {
                Id = id,
                Title = post.Title,
                Content = post.Content,
                RowCreatedDateTime = post.RowCreatedDateTime,
                RowModifiedDateTime = post.RowModifiedDateTime
            };
            if (post.Comments != null && post.Comments.Count() != 0)
            {
                model.Comments = new List<CommentViewModel>();

                for (int i = 0; i < post.Comments.Count(); i++)
                {
                    var comment = post.Comments.ElementAt(i);

                    var commentViewModel = _mapper.Map<Comment, CommentViewModel>(comment);
                    model.Comments.Add(commentViewModel);

                    if (comment.Replies != null & comment.Replies.Count() != 0)
                    {
                        model.Comments.ElementAt(i).Replies = new List<CommentViewModel>();

                        for (int j = 0; j < comment.Replies.Count(); j++)
                        {
                            var reply = comment.Replies.ElementAt(j);

                            var tempReply = _mapper.Map<Comment, CommentViewModel>(reply);
                            model.Comments.ElementAt(i).Replies.Add(tempReply);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostViewModel model)
        {
            Result result = new Result(false, "Unable to delete post at this time.");

            // if(ModelState.IsValid)
            // {
            //     Post post = await _post.GetPostAndCommentsAsync(model.Id);
            //     post.IsActive = false;
            //     post.RowModifiedBy = Convert.ToInt32(_userManager.GetUserId(User));
            //     post.RowModifiedDateTime = DateTime.Now;

            //     if(post.Comments != null && post.Comments.Count() != 0)
            //     {
            //         foreach (var comment in post.Comments)
            //         {
            //             comment.IsActive = false;
            //             comment.RowModifiedBy = post.RowModifiedBy;
            //             comment.RowModifiedDateTime = post.RowModifiedDateTime;

            //             if (comment.Replies != null && comment.Replies.Count() != 0)
            //             {
            //                 foreach (var reply in comment.Replies)
            //                 {
            //                     reply.IsActive = false;
            //                     reply.RowModifiedBy = post.RowModifiedBy;
            //                     reply.RowModifiedDateTime = post.RowModifiedDateTime;
            //                 }
            //             }
            //         }
            //     }

            //     try
            //     {
            //         await _post.EditPostAsync(post);
                    
            //         result.Succeeded = true;
            //         result.Message = "Post deleted.";
            //     }
            //     catch (Exception ex)
            //     {
            //         result.Message = "Unable to delete post at this time.";

            //         SysException exception = new SysException(ex)
            //         {
            //             Url = UrlHelper.GetRequestUrl(HttpContext)
            //         };
            //     }
            // }

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> HardDelete(int id = 0)
        {
            Result result = new Result(false, "Unable to delete post at this time.");

            if(id != 0)
            {
                try
                {
                    await _post.DeletePostAsync(id);
                    
                    result.Succeeded = true;
                    result.Message = "Post deleted.";
                }
                catch (Exception ex)
                {
                    result.Message = "Unable to delete post at this time.";

                    SysException exception = new SysException(ex)
                    {
                        Url = UrlHelper.GetRequestUrl(HttpContext)
                    };
                }
            }

            return Json(new { result });
        }
    }
}