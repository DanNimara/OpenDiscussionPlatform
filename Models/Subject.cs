using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenDiscussionPlatform.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        
        //public IEnumerable<SelectListItem> Categ { get; set;  }
        //UserID, DelByMod
    }
}