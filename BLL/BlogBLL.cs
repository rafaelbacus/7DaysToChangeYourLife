using DAL;
using Helper;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class BlogBLL 
    {
        private BlogDAL _blog;
        private PostDAL _post;

        public BlogBLL(BlogDAL blog, 
                       PostDAL post)
        {
            _blog = blog;
            _post = post;
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync(int count = 0)
        {
            SortOptions options = new SortOptions
            {
                SortBy = nameof(Post.RowCreatedDateTime),
                SortOrder = SortOrder.Descending
            };

            return await _post.GetPostsAsync(options, count);
        }

        public async Task<(IEnumerable<Post>, IEnumerable<Comment>)> GetRecentPostsAndCommentsAsync(int count = 0)
        {
            SortOptions options = new SortOptions
            {
                SortBy = nameof(Post.RowCreatedDateTime),
                SortOrder = SortOrder.Descending
            };

            return await _blog.GetRecentPostAndComments(options, count);
        }
    }
}