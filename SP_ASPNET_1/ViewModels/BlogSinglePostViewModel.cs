using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.ViewModels
{
    public class BlogSinglePostViewModel : IBlogViewModel
    {
        public Author Author { get; set; }
        public BlogPost BlogPost { get; set; }
        public int LiksCount { get; set; }
        public bool IsLiked { get; set; }
        public double AuthorAverageLikes { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
    }
}