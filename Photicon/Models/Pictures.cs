using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Photicon.Models
{
    public class Pictures
    {
        public Pictures()
        {
            TagsList = new HashSet<Tags>();
            CommentsList = new HashSet<Comments>();
            UsersThatLiked = new HashSet<Users>();
        }

        [Key]
        public int Id { get; set; }

        public string Path { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime UploadDate { get; set; }
        public bool Visibility { get; set; }
        public int Likes { get; set; }

        [ForeignKey("User")]
        public string UserFK { get; set; }
        public virtual Users User { get; set; }

        public virtual ICollection<Tags> TagsList { get; set; }
        public virtual ICollection<Comments> CommentsList { get; set; }
        public virtual ICollection<Users> UsersThatLiked { get; set; }
    }
}