using Photicon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photicon.Controllers
{
    public class UsersController : Controller
    {
        private PhoticonDB db = new PhoticonDB();

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index","Home");

            //Session["DeleteSuccess"] = "No";
            Users userToDelete = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();

            List<Pictures> pics = db.Pictures.ToList();
            foreach (var pic in pics)
            {
                if (userToDelete.PicturesList.Contains(pic))
                {
                    List<Users> usersThatLikedPic = pic.UsersThatLiked.ToList();
                    foreach (var u in usersThatLikedPic)
                    {
                        u.LikedPictures.Remove(pic);
                        db.Users.AddOrUpdate(u);
                    }

                    userToDelete.PicturesList.Remove(pic);

                    var picPath = pic.Path;
                    string fullPicPath = Request.MapPath(picPath);

                    db.Pictures.Remove(pic);

                    if (System.IO.File.Exists(fullPicPath))
                        System.IO.File.Delete(fullPicPath);
                }
                if (userToDelete.LikedPictures.Contains(pic))
                    pic.UsersThatLiked.Remove(userToDelete);
            }
            db.Users.Remove(userToDelete);
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public ActionResult Edit(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index","Home");
            Users user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index","Home");
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/UserPictures"), Path.GetFileName("user" + Id + "_" + file.FileName));
                    file.SaveAs(path);

                    Users user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();
                    user.ProfilePhoto = path.Substring(path.IndexOf("UserPictures") - 1);

                    db.Users.AddOrUpdate(user);
                    db.SaveChanges();

                    ViewBag.Message = "File uploaded succesfully";
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR!" + e.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You didn't specified a file";
            }

            return RedirectToAction("MainPage", "Home", new { Id = Id });
        }
    }
}