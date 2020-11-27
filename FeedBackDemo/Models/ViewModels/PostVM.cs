using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models.ViewModels
{
    public class PostVM
    {
        public Post Posts { get; set; }
        public List<Comment> PostComments { get; set; }
    }
}