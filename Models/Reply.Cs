using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenDiscussionPlatform.Models
{
    public class Reply
    {
        [Key]
        public int ReplyID { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int SubjectID { get; set; }

        public virtual Subject Subject { get; set; }
        //DelByMod, UserID
    }
}
