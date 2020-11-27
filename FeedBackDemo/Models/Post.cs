using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser User { get; set; }

    }
}