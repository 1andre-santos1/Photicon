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

        [Authorize]
        [HttpPost]
        public ActionResult AddFriend(string UserId, string ViewedUserId)
        {
            Users user = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            Users viewedUser = db.Users.Where(m => m.Id == ViewedUserId).SingleOrDefault();

            viewedUser.FriendRequestsReceivedPending.Add(user);
            user.FriendRequestsSentPending.Add(viewedUser);

            db.Users.AddOrUpdate(viewedUser);
            db.Users.AddOrUpdate(user);
            db.SaveChanges();

            return RedirectToAction("ViewOtherUserProfile", "Home", new { UserId = UserId, ViewedUserId = ViewedUserId });
        }

        [Authorize]
        [HttpPost]
        public ActionResult AcceptFriendRequest(string UserId, string RequestUserId)
        {
            Users user = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            Users requestUser = db.Users.Where(m => m.Id == RequestUserId).SingleOrDefault();

            user.FriendRequestsReceivedPending.Remove(requestUser);
            requestUser.FriendRequestsSentPending.Remove(user);

            user.Friends.Add(requestUser);
            requestUser.Friends.Add(user);

            db.Users.AddOrUpdate(user);
            db.Users.AddOrUpdate(requestUser);
            db.SaveChanges();

            return RedirectToAction("Edit", "Users", new { Id = UserId});
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveFriendRequest(string UserId, string RequestUserId)
        {
            Users user = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            Users requestUser = db.Users.Where(m => m.Id == RequestUserId).SingleOrDefault();

            user.FriendRequestsReceivedPending.Remove(requestUser);
            requestUser.FriendRequestsSentPending.Remove(user);

            db.Users.AddOrUpdate(user);
            db.Users.AddOrUpdate(requestUser);
            db.SaveChanges();

            return RedirectToAction("Edit", "Users", new { Id = UserId });
        }
    }
}