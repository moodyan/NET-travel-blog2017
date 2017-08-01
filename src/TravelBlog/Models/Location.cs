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
        public Location()
        {
            this.Experience = new HashSet<Experience>();
        }
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string FlavorText { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string Population { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Experience> Experience { get; set; }

    }
}
