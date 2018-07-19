using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Photicon.Models
{
    public class Users : IdentityUser
    {
        public Users()
        {
            LikedPictures = new HashSet<Pictures>();
            PicturesList = new HashSet<Pictures>();
            CommentsList = new HashSet<Comments>();
        }

        public string ProfilePhoto { get; set; }
        public virtual ICollection<Pictures> LikedPictures { get; set; }

        public virtual ICollection<Pictures> PicturesList { get; set; }

        public virtual ICollection<Comments> CommentsList { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Users> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}