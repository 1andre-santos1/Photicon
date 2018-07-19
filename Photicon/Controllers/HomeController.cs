using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photicon.Models;

namespace Photicon.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private PhoticonDB db = new PhoticonDB();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult MainPage(string Id)
        {
            Users user = new Users();
            user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();
            user.PicturesList = db.Pictures.Where(m => m.UserFK == user.Id).Select(m => m).ToList();
            user.PicturesList = user.PicturesList.OrderByDescending(x => x.UploadDate).ToList();
            return View(user);
        }

        [Authorize]
        public ActionResult ViewOtherUserProfile(string UserId, string ViewedUserId)
        {
            Users user = db.Users.Where(m => m.Id == UserId).Select(m => m).SingleOrDefault();
            Users viewedUser = db.Users.Where(m => m.Id == ViewedUserId).Select(m => m).SingleOrDefault();

            var model = new CommunityProfilesViewModels { User = user, ViewedUser = viewedUser };
            return View(model);
        }

        [Authorize]
        public ActionResult ViewOtherUserPicture(string UserId, int ViewedPictureId)
        {
            Users user = db.Users.Where(m => m.Id == UserId).Select(m => m).SingleOrDefault();
            Pictures viewedPicture = db.Pictures.Where(m => m.Id == ViewedPictureId).Select(m => m).SingleOrDefault();

            var model = new CommunityPicturesViewModels { User = user, ViewedPicture = viewedPicture };
            return View(model);
        }

        [Authorize]
        public ActionResult Community(string Id)
        {
            Users user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();

            List<Pictures> pics = new List<Pictures>();
            foreach (var picture in db.Pictures)
                if (picture.Visibility)
                    pics.Add(picture);

            pics = pics.OrderByDescending(m => m.UploadDate).ToList();

            var model = new UserAllPicturesViewModels { User = user, Pictures = pics };

            return View(model);
        }
    }
}