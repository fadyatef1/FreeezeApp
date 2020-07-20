using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using SP_ASPNET_1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class BlogLikeOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        internal void Create(BlogLike blogLike)
        {
            try
            {
                this._unitOfWork.BlogLikeSchoolRepository.Insert(blogLike);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public int BlogLikeCount(int blogId) 
        {
            return this._unitOfWork.BlogLikeSchoolRepository.BlogLikeCount(blogId);
        }
        public void Delete(int id)
        {
            try
            {
                string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                BlogLike like = this._unitOfWork.BlogLikeSchoolRepository.GetBlogLikeByBlogIDAndUserID(id, userId);
                this._unitOfWork.BlogLikeSchoolRepository.Remove(like);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public bool IsLikedBlog(int blogId)
        {
            try
            {
                string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                return this._unitOfWork.BlogLikeSchoolRepository.IsLikedBlog(blogId, userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public double AuthorAverageLikes(int authorId)
        {
            try
            {
                ICollection<int> blogsIds = this._unitOfWork.BlogPostSchoolRepository.GetAuthorBlogsIds(authorId);
                return this._unitOfWork.BlogLikeSchoolRepository.AuthorAverageLikes(blogsIds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}