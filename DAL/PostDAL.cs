using Helper;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class PostDAL
    {
        private BlogContext _context { get; }

        public PostDAL(BlogContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _context.Posts.SingleAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(SortOptions options, int count)
        {
            return await _context.Posts.OrderBy(options)
                                       .Take(count)
                                       .ToListAsync();
        }
    }
}