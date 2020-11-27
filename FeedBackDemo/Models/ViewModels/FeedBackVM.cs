using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models.ViewModels
{
    public class FeedBackVM
    {
        public int TotalPost { get; set; }

        public List<Post> PostList { get; set; }

        public int? page { get; set; }
        public int ItemPerPage { get; set; }
        public int HiddenItemPerPage { get; set; }

        public Pager pager { get; set; }

        public FeedBackVM()
        {
            pager = new Pager(0, 0);
        }
    }
}