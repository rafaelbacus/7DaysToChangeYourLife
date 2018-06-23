using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;

namespace Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private BlogBLL _blog;

        public AdminController(BlogBLL blog)
        {
            _blog = blog;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Post> recentPosts = null;

            try
            {
                // recentPosts = await _blog.GetRecentPostsAsync(count: 5);            
            }
            catch (Exception ex)
            {
                // Log Exception
            }

            return View(recentPosts);
        }
    }
}