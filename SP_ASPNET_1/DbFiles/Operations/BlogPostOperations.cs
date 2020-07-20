using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using SP_ASPNET_1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class BlogPostOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<BlogIndexViewModel> GetBlogIndexViewModelAsync()
        {
            List<BlogPost> blogPosts = (await _unitOfWork.BlogPostSchoolRepository.GetAsync(null, b => b.OrderByDescending(d => d.DateTime), "Author")).ToList();

            return new BlogIndexViewModel()
            {
                BlogPosts = blogPosts.GetRange(1, blogPosts.Count - 1),
                BlogPost = blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogIndexViewModel GetBlogIndexViewModel()
        {
            List<BlogPost> blogPosts = _unitOfWork.BlogPostSchoolRepository
                .Get(null, b => b.OrderByDescending(d => d.DateTime), "Author").ToList();
            if (!blogPosts.Any())
            {
                return new BlogIndexViewModel();
            }
            return new BlogIndexViewModel()
            {
                BlogPosts = blogPosts.GetRange(1, blogPosts.Count - 1),
                BlogPost = blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogIndexViewModel GetBlogIndexViewModelPagination(int currentPage, int maxRows)
        {
            List<BlogPost> blogPosts = _unitOfWork.BlogPostSchoolRepository.
                GetPagination(currentPage, maxRows, null, b => b.OrderByDescending(d => d.DateTime), "Author").ToList();

            if (!blogPosts.Any())
            {
                return new BlogIndexViewModel();
            }
            return new BlogIndexViewModel()
            {
                BlogPosts = blogPosts.GetRange(1, blogPosts.Count - 1),
                BlogPost = blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogPost GetBlogPostByIdD(int id)
        {
            return _unitOfWork.BlogPostSchoolRepository.GetByID(id);
        }

        public BlogSinglePostViewModel GetBlogPostByIdFull(int id)
        {
            BlogSinglePostViewModel blogSinglePostView = _unitOfWork.BlogPostSchoolRepository.Get(filter: x => x.BlogPostID == id,
                    orderBy: null,
                    includeProperties: "Author")
                .FirstOrDefault()
                .ToBlogSinglePostViewModel();
            blogSinglePostView.AuthorAverageLikes = AuthorAverageLikes(blogSinglePostView.Author.AuthorID);
            return blogSinglePostView;
        }

        public BlogSinglePostViewModel GetLatestBlogPost()
        {
            BlogSinglePostViewModel blogSinglePostView = _unitOfWork.BlogPostSchoolRepository.Get(filter: null,
                    orderBy: x => x.OrderByDescending(entity => entity.DateTime),
                    includeProperties: "Author")
                .Select(StaticHelpers.ToBlogSinglePostViewModel)
                .FirstOrDefault();
            blogSinglePostView.AuthorAverageLikes = AuthorAverageLikes(blogSinglePostView.Author.AuthorID);
            return blogSinglePostView;
        }

        public BlogSinglePostViewModel GetRandomBlogPost()
        {
            //TODO: Investigate
            List<BlogPost> posts = _unitOfWork.BlogPostSchoolRepository.Get(null,
                    x => x.OrderByDescending(entity => entity.DateTime),
                    "Author")
                .ToList();

            if(posts.Count is 0)
            {
                return null;
            }

            Random rnd = new Random();
            
            var randomPost = posts[rnd.Next(posts.Count)];
            BlogSinglePostViewModel blogSinglePostViewModel = randomPost.ToBlogSinglePostViewModel();
            blogSinglePostViewModel.AuthorAverageLikes = AuthorAverageLikes(blogSinglePostViewModel.Author.AuthorID);
            return blogSinglePostViewModel;
        }

        internal void Create(BlogPost blogPost)
        {
            try
            {
                this._unitOfWork.BlogPostSchoolRepository.Insert(blogPost);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        internal void Update(BlogPost blogPost)
        {
            try
            {
                this._unitOfWork.BlogPostSchoolRepository.Update(blogPost);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                BlogPost post = this.GetBlogPostByIdD(id);
                this._unitOfWork.BlogPostSchoolRepository.Remove(post);
                this._unitOfWork.Save();
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
                ICollection<int> blogsIds = _unitOfWork.BlogPostSchoolRepository.GetAuthorBlogsIds(authorId);
                return _unitOfWork.BlogLikeSchoolRepository.AuthorAverageLikes(blogsIds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}