using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Web.Helpers;
using Web.ViewModels.Admin;
using Web.ViewModels.Comment;

namespace Web.Controllers
 {
    [Authorize(Roles = Constants.AdminRole)]
    public class CommentController : Controller
    {
        private AppSettings _appSettings;
        private IMapper _mapper;
        private CommentBLL _comment;
        private UserManager<User> _userManager;

        public CommentController(
            IMapper mapper, 
            CommentBLL comment,
            UserManager<User> userManager,
            IOptionsSnapshot<AppSettings> appSettingsAccessor)
        {
            _appSettings = appSettingsAccessor.Value;
            _mapper = mapper;
            _comment = comment;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentViewModel model)
        {
            Result result = new Result(false, "Unable to add comment at this time.");

            if (String.IsNullOrWhiteSpace(model.Content))
            {
                result.Message = "Comment is required;";
            }
            else if (ModelState.IsValid)
            {
                var comment = _mapper.Map<AddCommentViewModel, Comment>(model);
                comment.RowCreatedBy = comment.RowModifiedBy = 1;
                comment.RowCreatedDateTime = comment.RowModifiedDateTime = DateTime.Now;
                
                try
                {
                    await _comment.AddCommentAsync(comment);

                    result.Succeeded = true;
                    result.Message = "Comment added.";                    
                }
                catch (Exception ex)
                {
                    SysException exception = new SysException(ex)
                    {
                        Url = UrlHelper.GetRequestUrl(HttpContext)
                    };
                }
            }

            return Json(result);
        }
    }
 }