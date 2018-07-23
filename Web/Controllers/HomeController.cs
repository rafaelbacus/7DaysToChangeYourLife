using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL;
using Web.ViewModels;
using Web.ViewModels.Post;
using Model;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private PostBLL _post;

        public HomeController(IMapper mapper, PostBLL post)
        {
            _post = post;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<PostViewModel> model = null;

            var recentPosts = await _post.GetRecentPostsAsync(page, count: 10);
            if (recentPosts != null && recentPosts.Count() != 0)
            {
                model = _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(recentPosts);
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Test()
        {
            return Redirect("/admin");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
