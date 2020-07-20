using SP_ASPNET_1.DbFiles.Contexts;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Repositories.RepositoryDecorators
{
    public class BlogLikeRepository : BaseRepository<BlogLike>,IBlogLikeRepository
    {
        public BlogLikeRepository(IceCreamBlogContext context) : base(context)
        {
            
        }

        public double AuthorAverageLikes(ICollection<int> BlogPostIDs)
        {
            int likes = this._dbSet.Where(r => BlogPostIDs.Contains(r.BlogPostID)).Count();
            double average = likes /(double) BlogPostIDs.Count();
            return Math.Round(average,1);
        }

        public int BlogLikeCount(int blogId)
        {
            int count = this._dbSet
            .Where(o => o.BlogPostID == blogId)
            .Count();
            return count;
        }

        public BlogLike GetBlogLikeByBlogIDAndUserID(int blogId, string userId)
        {
            return this._dbSet.FirstOrDefault(b => b.BlogPostID == blogId && b.UserId == userId);
        }

        public bool IsLikedBlog(int blogId, string userId)
        {
            return this._dbSet.Any(b => b.BlogPostID == blogId && b.UserId == userId);
        }
    }
}