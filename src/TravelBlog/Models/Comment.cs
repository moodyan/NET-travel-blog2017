using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TravelBlog.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [DataType(DataType.MultilineText)]
        public string CommentBody { get; set; }
        public int ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
