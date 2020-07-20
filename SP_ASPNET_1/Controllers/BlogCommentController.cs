using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Comment")]
    public class BlogCommentController : Controller
    {
        private readonly BlogCommentOperations _blogCommentOperations = new BlogCommentOperations();

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(BlogComment blogComment)
        {
            try
            {
                blogComment.CommentDate = DateTime.Now;
                this._blogCommentOperations.Create(blogComment);
                return Json(new { msg = "Successfully added" });
            }
            catch
            {
                return View();
            }
        }
        [Route("Get/partial/{blogId:int?}")]
        [HttpGet]
        public ActionResult GetComments(int blogId)
        {
            try
            {
                ICollection<BlogComment> comments = this._blogCommentOperations.GetBlogComments(blogId);
                return PartialView("~/Views/BlogPost/_commentPartial.cshtml", comments);
            }
            catch
            {
                return View();
            }
        }

        [Route("Delete/{commentId:int?}")]
        [HttpGet]
        public ActionResult DeleteComment(int commentId)
        {
            try
            {
                this._blogCommentOperations.DeleteComment(commentId);
                return Json(new { msg = "Successfully Removed", JsonRequestBehavior.AllowGet });
            }
            catch
            {
                return View();
            }
        }
    }
}