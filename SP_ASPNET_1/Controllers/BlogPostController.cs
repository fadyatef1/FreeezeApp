using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Web.Mvc;
using System.Web.Routing;
using SP_ASPNET_1.BusinessLogic;
using System;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {
        private readonly BlogPostOperations _blogPostOperations = new BlogPostOperations();
        private readonly BlogLikeOperations _blogLikeOperations = new BlogLikeOperations();
        private readonly BlogCommentOperations _blogCommentOperations = new BlogCommentOperations();
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            //return this.View();
            int maxRows = 11;
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModelPagination(1, maxRows);
            ViewBag.Title = "Blog";
            Session["Page"] = 1;
            return this.View(result);
        }

        [Route("More")]
        [HttpGet]
        public ActionResult IndexPaging()
        {
            int maxRows = 10;
            int x= (int)Session["Page"]+1;
            Session["Page"] = x;
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModelPagination(x, maxRows);
            return PartialView("~/Views/BlogPost/_IndexItemsPartial.cshtml", result.BlogPosts);
        }

        [Route("Detail/{id:int?}")]
        [HttpGet]
        public ActionResult SinglePost(int? id)
        {
            ViewBag.Title = "single post";

            
            BlogSinglePostViewModel modelView;

            if (id == null)
            {
                modelView = this._blogPostOperations.GetLatestBlogPost();
                modelView.BlogComments = this._blogCommentOperations.GetBlogComments(modelView.BlogPost.BlogPostID);
                modelView.LiksCount = this._blogLikeOperations.BlogLikeCount(modelView.BlogPost.BlogPostID);
                modelView.IsLiked = this._blogLikeOperations.IsLikedBlog(modelView.BlogPost.BlogPostID);
            }
            else
            {
                modelView = this._blogPostOperations.GetBlogPostByIdFull((int)id);
                modelView.BlogComments = this._blogCommentOperations.GetBlogComments((int)id);
                modelView.LiksCount = this._blogLikeOperations.BlogLikeCount((int)id);
                modelView.IsLiked = this._blogLikeOperations.IsLikedBlog((int)id);
            }

            return View(modelView);
        }

        [Route("Detail/Random")]
        [HttpGet]
        public ActionResult RandomPost()
        {
            ViewBag.Title = "Random post";

            var viewModel = this._blogPostOperations.GetRandomBlogPost();
            viewModel.BlogComments = this._blogCommentOperations.GetBlogComments(viewModel.BlogPost.BlogPostID);
            viewModel.LiksCount = this._blogLikeOperations.BlogLikeCount(viewModel.BlogPost.BlogPostID);
            viewModel.IsLiked = this._blogLikeOperations.IsLikedBlog(viewModel.BlogPost.BlogPostID);
            return View(viewModel);
        }

        [Route("LatestPost")]
        [HttpGet]
        public ActionResult LatestPost()
        {
            var viewModel = this._blogPostOperations.GetLatestBlogPost();

            return this.PartialView("~/Views/BlogPost/_BlogPostRecentPartialView.cshtml", viewModel);
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(BlogPost blogPost)
        {
            try
            {
                this._blogPostOperations.Create(blogPost);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Route("Edit/{id:int?}")]
        [HttpGet]
        public ActionResult EditBlogPost(int id)
        {
            BlogPost blogPost;

            blogPost = this._blogPostOperations.GetBlogPostByIdD((int)id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        [Route("Edit/{id:int}")]
        [HttpPost]
        public ActionResult Edit(int id, BlogPost blogPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._blogPostOperations.Update(blogPost);
                    return RedirectToAction("Index");
                }
                return View(blogPost);

            }
            catch
            {
                return View();
            }
        }

        [Route("Delete/{id:int}")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                this._blogPostOperations.Delete(id);

                //CHECK: should return to blogs
                return RedirectToAction("Index");
            }
            catch
            {
                return this.HttpNotFound();
            }
        }
    }
}
