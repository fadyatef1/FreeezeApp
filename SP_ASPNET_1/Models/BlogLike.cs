using SP_ASPNET_1.DbFiles.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class BlogLike
    {
        [Key, Column(Order = 1)]
        public int BlogPostID { get; set; }
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}