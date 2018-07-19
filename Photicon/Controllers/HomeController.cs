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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MainPage(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");
            Users user = new Users();
            user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();
            user.PicturesList = db.Pictures.Where(m => m.UserFK == user.Id).Select(m => m).ToList();
            user.PicturesList = user.PicturesList.OrderByDescending(x => x.UploadDate).ToList();
            return View(user);
        }

        public ActionResult ViewOtherUserProfile(string UserId, string ViewedUserId)
        {
            if (!User.Identity.IsAuthenticated || UserId == null)
                return RedirectToAction("Index");

            Users user = db.Users.Where(m => m.Id == UserId).Select(m => m).SingleOrDefault();
            Users viewedUser = db.Users.Where(m => m.Id == ViewedUserId).Select(m => m).SingleOrDefault();

            var model = new CommunityProfilesViewModels { User = user, ViewedUser = viewedUser };
            return View(model);
        }

        public ActionResult ViewOtherUserPicture(string UserId, int ViewedPictureId)
        {
            if (!User.Identity.IsAuthenticated || UserId == null)
                return RedirectToAction("Index");

            Users user = db.Users.Where(m => m.Id == UserId).Select(m => m).SingleOrDefault();
            Pictures viewedPicture = db.Pictures.Where(m => m.Id == ViewedPictureId).Select(m => m).SingleOrDefault();

            var model = new CommunityPicturesViewModels { User = user, ViewedPicture = viewedPicture };
            return View(model);
        }

        public ActionResult Community(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");

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