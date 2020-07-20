using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Like")]
    public class BlogLikeController : Controller
    {
        private readonly BlogLikeOperations _blogLikeOperations = new BlogLikeOperations();
        
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(BlogLike blogPost)
        {
            try
            {
                this._blogLikeOperations.Create(blogPost);
                return Json(new { msg = "Successfully added" });
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
                this._blogLikeOperations.Delete(id);
                return Json(new { msg = "Successfully Removed", JsonRequestBehavior.AllowGet });
            }
            catch
            {
                return this.HttpNotFound();
            }
        }
    }
}