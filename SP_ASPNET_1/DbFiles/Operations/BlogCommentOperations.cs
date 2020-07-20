using SP_ASPNET_1.BusinessLogic;
using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class BlogCommentOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        internal void Create(BlogComment blogComment)
        {
            try
            {
                this._unitOfWork.BlogCommentSchoolRepository.Insert(blogComment);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ICollection<BlogComment> GetBlogComments(int blogId)
        {
            return _unitOfWork.BlogCommentSchoolRepository.Get(filter: b=>b.BlogPostID == blogId,
                    orderBy: null,
                    includeProperties: "User").ToList();
        }
        public BlogComment GetBlogCommentByIdD(int commentId)
        {
            return _unitOfWork.BlogCommentSchoolRepository.GetByID(commentId);
        }
        public void DeleteComment(int commentId)
        {
            try
            {
                BlogComment blog = this.GetBlogCommentByIdD(commentId);
                this._unitOfWork.BlogCommentSchoolRepository.Remove(blog);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}