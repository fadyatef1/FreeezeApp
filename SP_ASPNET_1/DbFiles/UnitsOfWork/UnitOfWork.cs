using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.DbFiles.Contexts;
using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.DbFiles.Repositories;
using SP_ASPNET_1.DbFiles.Repositories.RepositoryDecorators;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.DbFiles.UnitsOfWork
{
    public interface IUnitOfWork
    {
        BlogPostRepository BlogPostSchoolRepository { get; }
        IRepository<Author> AuthorSchoolRepository { get; }
        IRepository<ProductLine> ProductLineSchoolRepository { get; }
        IRepository<ProductItem> ProductItemSchoolRepository { get; }
        IBlogLikeRepository BlogLikeSchoolRepository { get; }
        IRepository<BlogComment> BlogCommentSchoolRepository { get; }
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IceCreamBlogContext _context = new IceCreamBlogContext();

        private IRepository<Author> _authorSchoolRepository;
        private BlogPostRepository _blogPostSchoolRepository;
        private IRepository<ProductLine> _productLineSchoolRepository;
        private IRepository<ProductItem> _productItemSchoolRepository;
        private IBlogLikeRepository _blogLikeSchoolRepository;
        private IRepository<BlogComment> _blogCommentSchoolRepository;

        public BlogPostRepository BlogPostSchoolRepository
        {
            get
            {
                if (this._blogPostSchoolRepository == null)
                {
                    this._blogPostSchoolRepository = new BlogPostRepository(this._context);
                }
                return _blogPostSchoolRepository;
            }
        }

        public IRepository<Author> AuthorSchoolRepository
        {
            get
            {
                if (this._authorSchoolRepository == null)
                {
                    this._authorSchoolRepository = new BaseRepository<Author>(this._context);
                }
                return _authorSchoolRepository;
            }
        }

        public IRepository<ProductLine> ProductLineSchoolRepository
        {
            get
            {
                if (this._productLineSchoolRepository == null)
                {
                    this._productLineSchoolRepository = new BaseRepository<ProductLine>(this._context);
                }
                return _productLineSchoolRepository;
            }
        }

        public IRepository<ProductItem> ProductItemSchoolRepository
        {
            get
            {
                if (this._productItemSchoolRepository == null)
                {
                    this._productItemSchoolRepository = new BaseRepository<ProductItem>(this._context);
                }
                return _productItemSchoolRepository;
            }
        }

        public IBlogLikeRepository BlogLikeSchoolRepository
        {
            get
            {
                if (this._blogLikeSchoolRepository == null)
                {
                    this._blogLikeSchoolRepository = new BlogLikeRepository(this._context);
                }
                return _blogLikeSchoolRepository;
            }
        }

        public IRepository<BlogComment> BlogCommentSchoolRepository
        {
            get
            {
                if (this._blogCommentSchoolRepository == null)
                {
                    this._blogCommentSchoolRepository = new BlogCommentRepository(this._context);
                }
                return _blogCommentSchoolRepository;
            }
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}