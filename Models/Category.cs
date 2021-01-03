using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenDiscussionPlatform.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu!")]
        [StringLength(25, ErrorMessage = "Numele categoriei nu poate avea mai mult de 25 de caractere!")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }
}
