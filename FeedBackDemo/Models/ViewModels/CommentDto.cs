using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models.ViewModels
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UserName { get; set; }

        public int LikedCount { get; set; }
        public int DislikedCount { get; set; }

    }
}