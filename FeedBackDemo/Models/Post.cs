﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models
{
    public class Post
    {
        public int Id { get; set; }
        [StringLength(150)]
        [Required]
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<Comment> PostComments { get; set; }

    }
}