using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photicon.Models
{
    public class Tags
    {
        public Tags()
        {
            PicturesList = new HashSet<Pictures>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Pictures> PicturesList { get; set; }
    }
}