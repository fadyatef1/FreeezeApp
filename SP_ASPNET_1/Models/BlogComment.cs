using SP_ASPNET_1.DbFiles.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class BlogComment
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("BlogPost")]
        public int BlogPostID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public ApplicationUser User { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
    }
}