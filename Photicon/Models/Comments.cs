using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Photicon.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }

        public string Comment{ get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Picture")]
        public int PictureFK { get; set; }
        public virtual Pictures Picture { get; set; }

        [ForeignKey("User")]
        public string UserFK { get; set; }
        public virtual Users User { get; set; }
    }
}