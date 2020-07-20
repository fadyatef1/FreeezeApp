
using SP_ASPNET_1.DbFiles.Contexts;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Repositories.RepositoryDecorators
{
    public class BlogCommentRepository : BaseRepository<BlogComment>
    {
        public BlogCommentRepository(IceCreamBlogContext context):base(context)
        {

        }
    }
}