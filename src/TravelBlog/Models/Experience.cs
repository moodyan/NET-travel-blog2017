using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table("Experiences")]
    public class Experience
    {
        //public Experience()
        //{
        //    this.Comment = new HashSet<Comment>();
        //}

    [Key]
        public int ExperienceId { get; set; }
        public string Title { get; set; }
        public string Eat { get; set; }
        public string Drink { get; set; }
        public string See { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
       
    }
}
