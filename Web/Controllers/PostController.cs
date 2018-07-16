using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL;

namespace Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private IMapper mapper;
        private PostBLL post;

        public PostController(IMapper _mapper, PostBLL _post)
        {
            mapper = _mapper;
            post = _post;
        }

        [HttpGet]
        public IActionResult Index ()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}