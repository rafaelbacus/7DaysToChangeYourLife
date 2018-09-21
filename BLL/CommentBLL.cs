using DAL;
using Helper;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentBLL
    {
        private CommentDAL _comment;

        public CommentBLL(CommentDAL comment)
        {
            _comment = comment;
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            return await _comment.GetCommentAsync(id);
        }

        public async Task AddCommentAsync(Comment comment){
            await _comment.AddCommentAsync(comment);
        }
    }
}