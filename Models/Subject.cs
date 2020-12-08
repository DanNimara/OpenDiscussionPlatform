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
        [Required(ErrorMessage = "Titlul este obligatoriu!")]
        [StringLength(100, ErrorMessage = "Titlul nu poate avea mai mult de 100 de caractere!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Continutul subiectului de discutie este obligatoriu!")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Categoria este obligatorie!")]
        public int CategoryID { get; set; }
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }

        public IEnumerable<SelectListItem> Categs { get; set; }
    }
}
