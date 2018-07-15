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

        public ActionResult AddPicture(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");
            Users user = new Users();
            user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddPicture(HttpPostedFileBase file, string Id, string PictureDescription, bool Visibility, bool IsProfilePicture, ICollection<string> Tag)
        {

            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    int picId = db.Pictures.Count() + 1;
                    string path = Path.Combine(Server.MapPath("~/UserPictures"), Path.GetFileName("picture" + picId + "_" + file.FileName));
                    file.SaveAs(path);

                    Users user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();
                    Pictures pic = new Pictures();
                    pic.User = user;
                    pic.CommentariesList = new List<string>();
                    pic.Description = PictureDescription;
                    pic.Link = "";
                    pic.Path = path.Substring(path.IndexOf("UserPictures") - 1);
                    if (Tag != null)
                    {
                        foreach (var tag in Tag)
                        {
                            if (db.Tags.Any(m => m.Name == tag))
                            {
                                Tags t = db.Tags.Where(m => m.Name == tag).Select(m => m).SingleOrDefault();
                                t.PicturesList.Add(pic);
                                pic.TagsList.Add(t);
                                db.Tags.AddOrUpdate(t);
                            }
                            else
                            {
                                Tags t = new Tags();
                                t.Id = db.Tags.Count() + 1;
                                t.Name = tag;
                                t.PicturesList.Add(pic);
                                pic.TagsList.Add(t);
                                db.Tags.AddOrUpdate(t);
                            }
                        }
                    }

                    pic.Likes = 0;
                    pic.Visibility = !Visibility;
                    pic.UploadDate = DateTime.Now.Date;
                    pic.UserFK = user.Id;
                    user.PicturesList.Add(pic);
                    if (IsProfilePicture)
                    {
                        user.ProfilePhoto = pic.Path;
                    }
                    db.Users.AddOrUpdate(user);
                    db.Pictures.Add(pic);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePicture(int? Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");

            //Session["DeleteSuccess"] = "No";
            Pictures pictureToDelete = db.Pictures.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();
            string userId = "";

            List<Users> users = db.Users.ToList();
            foreach (var user in users)
            {
                if (user.PicturesList.Contains(pictureToDelete))
                {
                    user.PicturesList.Remove(pictureToDelete);
                    userId = user.Id;
                    db.Users.AddOrUpdate(user);
                }
                if (user.LikedPictures.Contains(pictureToDelete))
                {
                    user.LikedPictures.Remove(pictureToDelete);
                    db.Users.AddOrUpdate(user);
                }
            }

            var photoPath = pictureToDelete.Path;
            string fullPath = Request.MapPath(photoPath);

            db.Pictures.Remove(pictureToDelete);
            db.SaveChanges();

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }

            return RedirectToAction("MainPage", "Home", new { Id = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProfile(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");

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

            return RedirectToAction("Index");
        }

        public ActionResult ViewPicture(int PictureId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users user = pic.User;//

            var model = new UserPictureViewModels { User = user, Picture = pic };

            return View(model);
        }

        [HttpPost]
        public ActionResult ViewPicture(int PictureId,string UserId, ICollection<string> Tag,bool Visibility,bool IsProfilePicture, string PictureDescription)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users user = pic.User;

            pic.Visibility = !Visibility;
            pic.Description = PictureDescription;
            if (IsProfilePicture)
            {
                user.ProfilePhoto = pic.Path;
            }
            for(int tagIndex = 0; tagIndex < pic.TagsList.Count; tagIndex++)
            {
                Tags tag = pic.TagsList.ElementAt(tagIndex);
                if(Tag == null)
                {
                    tag.PicturesList.Remove(pic);
                    pic.TagsList.Remove(tag);
                    db.Tags.AddOrUpdate(tag);
                    tagIndex--;
                }
                else if (!Tag.Contains(tag.Name))
                {
                    tag.PicturesList.Remove(pic);
                    pic.TagsList.Remove(tag);
                    db.Tags.AddOrUpdate(tag);
                    tagIndex--;
                }
            }
            if (Tag != null)
            {
                foreach (var tag in Tag)
                {
                    if (db.Tags.Any(m => m.Name == tag))
                    {
                        Tags t = db.Tags.Where(m => m.Name == tag).Select(m => m).SingleOrDefault();
                        t.PicturesList.Add(pic);
                        pic.TagsList.Add(t);
                        db.Tags.AddOrUpdate(t);
                    }
                    else
                    {
                        Tags t = new Tags();
                        t.Id = db.Tags.Count() + 1;
                        t.Name = tag;
                        t.PicturesList.Add(pic);
                        pic.TagsList.Add(t);
                        db.Tags.AddOrUpdate(t);
                    }
                }
            }
            db.Pictures.AddOrUpdate(pic);
            db.SaveChanges();

            return RedirectToAction("MainPage", "Home", new { Id = UserId });
        }

        public ActionResult EditPicture(int PictureId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users user = pic.User;

            var model = new UserPictureViewModels { User = user, Picture = pic };

            return View(model);
        }

        public ActionResult DownloadPicture(string PicturePath)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            string path = AppDomain.CurrentDomain.BaseDirectory;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + PicturePath.Substring(PicturePath.LastIndexOf("UserPictures\\") - 1));
            string fileName = PicturePath.Substring(PicturePath.LastIndexOf("UserPictures\\") - 1);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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

        public ActionResult EditProfile(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");
            Users user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult EditProfile(HttpPostedFileBase file, string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index");
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

        public ActionResult LikePicture(string UserId, int PictureId)
        {
            Pictures PicBeingLiked = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users UserLikingPicture = db.Users.Where(m => m.Id == UserId).Select(m => m).SingleOrDefault();

            if (!UserLikingPicture.LikedPictures.Contains(PicBeingLiked))
            {
                UserLikingPicture.LikedPictures.Add(PicBeingLiked);
                PicBeingLiked.UsersThatLiked.Add(UserLikingPicture);
                PicBeingLiked.Likes++;

                db.Users.AddOrUpdate(UserLikingPicture);
                db.Pictures.AddOrUpdate(PicBeingLiked);
                db.SaveChanges();
            }
            else
            {
                UserLikingPicture.LikedPictures.Remove(PicBeingLiked);
                PicBeingLiked.UsersThatLiked.Remove(UserLikingPicture);
                PicBeingLiked.Likes--;

                db.Users.AddOrUpdate(UserLikingPicture);
                db.Pictures.AddOrUpdate(PicBeingLiked);
                db.SaveChanges();
            }

            return RedirectToAction("Community", "Home", new { Id = UserLikingPicture.Id });
        }
    }
}