using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Helper;
using Model;

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

        public async Task<IEnumerable<Post>> GetPostsAsync(int page, int count)
        {
            return await _context.Posts.OrderByDescending(p => p.RowCreatedDateTime)
                                       .Skip((page - 1) * count)
                                       .Take(count)
                                       .ToListAsync();
        }

        public async Task<Post> GetPostAndCommentsAsync(int id)
        {
            return await _context.Posts.Where(p => p.Id == id)
                                       .Include(p => p.Comments)
                                          .ThenInclude(c => c.Replies)
                                       .SingleOrDefaultAsync();
        }

        public async Task AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task EditPostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await GetPostAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}