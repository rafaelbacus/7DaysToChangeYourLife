using Helper;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentDAL
    {
        private BlogContext _context;

        public CommentDAL(BlogContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            return await _context.Comments.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(SortOptions options, int count)
        {
            return await _context.Comments.OrderBy(options)
                                          .Take(count)
                                          .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}