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
            Password = "Ba1234!";
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePhoto { get; set; }
        public string Password { get; set; }


    }
}