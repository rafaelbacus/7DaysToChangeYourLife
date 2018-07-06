using Helper;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public class BlogDAL
    {
        private BlogContext _context;
        private CommentDAL _comment;
        private PostDAL _post;

        public BlogDAL(BlogContext context, 
                       CommentDAL comment,
                       PostDAL post)
        {
            _comment = comment;
            _context = context;
            _post = post;
        }

        public async Task<(IEnumerable<Post>, IEnumerable<Comment>)> GetRecentPostAndComments(SortOptions options, int count)
        {
            IEnumerable<Post> posts = await _post.GetPostsAsync(options, count);
            IEnumerable<Comment> comments = await _comment.GetCommentsAsync(options, count);

            return (posts, comments);
        }
    }
}