using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 1 = Liked , 2 = Disliked
        /// </summary>
        public int Type { get; set; }
    }
}