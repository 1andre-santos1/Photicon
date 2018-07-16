using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photicon.Models.Api
{
    public class UserModel
    {
        public UserModel(Users user)
        {
            Id = user.Id;
            UserName = user.UserName;
            ProfilePhoto = user.ProfilePhoto;
            Pictures = user.PicturesList.Count;
            Likes = user.LikedPictures.Count;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePhoto { get; set; }
        public int Pictures;
        public int Likes { get; set; }
    }
}