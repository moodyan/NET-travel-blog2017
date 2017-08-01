using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string FlavorText { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string Population { get; set; }
        public string Image { get; set; }
        public int ExperienceId { get; set; }
        public int PersonID { get; set; }
        public virtual Experience Experience { get; set; }
        public virtual Person Person { get; set; }
    }
}
