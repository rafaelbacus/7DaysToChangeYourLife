using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Helper;
using Model;

namespace BLL
{
    public class PostBLL
    {
        private PostDAL _post;

        public PostBLL(PostDAL post)
        {
            _post = post;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _post.GetPostAsync(id);
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync(int page = 1, int count = 10)
        {
            return await _post.GetPostsAsync(page, count);
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync(int count = 0)
        {
            SortOptions options = new SortOptions
            {
                SortBy = nameof(Post.RowCreatedDateTime),
                SortOrder = SortOrder.Descending
            };

            var posts = await _post.GetPostsAsync(options, count);

            return posts.Where(p => p.IsActive == true);
        }

        public async Task<Post> GetPostAndCommentsAsync(int id)
        {
            return await _post.GetPostAndCommentsAsync(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await _post.AddPostAsync(post);
        }

        public async Task EditPostAsync(Post post)
        {
            await _post.EditPostAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _post.DeletePostAsync(id);
        }
    }
}