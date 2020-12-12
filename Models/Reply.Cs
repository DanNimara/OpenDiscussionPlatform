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
        [Required(ErrorMessage = "Continutul raspunsului este obligatoriu!")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int SubjectID { get; set; }
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Subject Subject { get; set; }
        //DelByMod, UserID
    }
}
