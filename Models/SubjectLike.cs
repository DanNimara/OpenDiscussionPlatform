using System;
using System.ComponentModel.DataAnnotations;

namespace OpenDiscussionPlatform.Models
{
    public class SubjectLike
    {
        [Key]
        public int SubjectLikeID { get; set; }
        
        [Required]
        public int SubjectID { get; set; }
        
        [Required]
        public string UserID { get; set; }
        
        [Required]
        public bool IsLike { get; set; } // true for like, false for dislike
        
        public DateTime Date { get; set; }

        // Navigation properties
        public virtual Subject Subject { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}