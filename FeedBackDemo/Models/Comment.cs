using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedBackDemo.Models
{
    public class Comment
    {
        public Comment()
        {
            this.VoteList = new HashSet<Vote>();
           // this.User = new ApplicationUser();
        }
        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Order { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Vote> VoteList { get; set; }

        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
    }
}