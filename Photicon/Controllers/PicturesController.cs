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
    public class PicturesController : Controller
    {
        private PhoticonDB db = new PhoticonDB();

        public ActionResult Add(string Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index","Home");

            Users user = new Users();
            user = db.Users.Where(m => m.Id == Id).Select(m => m).SingleOrDefault();

            return View(user);
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file, string Id, string PictureDescription, bool Visibility, bool IsProfilePicture, ICollection<string> Tag)
        {

            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index","Home");

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
        public ActionResult Delete(int? Id)
        {
            if (!User.Identity.IsAuthenticated || Id == null)
                return RedirectToAction("Index","Home");

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



        public ActionResult View(int PictureId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");
            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users user = pic.User;//

            var model = new UserPictureViewModels { User = user, Picture = pic };

            return View(model);
        }

        [HttpPost]
        public ActionResult View(int PictureId, string UserId, ICollection<string> Tag, bool Visibility, bool IsProfilePicture, string PictureDescription)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");

            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users user = pic.User;

            pic.Visibility = !Visibility;
            pic.Description = PictureDescription;
            if (IsProfilePicture)
            {
                user.ProfilePhoto = pic.Path;
            }
            for (int tagIndex = 0; tagIndex < pic.TagsList.Count; tagIndex++)
            {
                Tags tag = pic.TagsList.ElementAt(tagIndex);
                if (Tag == null)
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

        public ActionResult Edit(int PictureId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");
            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).Select(m => m).SingleOrDefault();
            Users user = pic.User;

            var model = new UserPictureViewModels { User = user, Picture = pic };

            return View(model);
        }

        public ActionResult Download(string PicturePath)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");
            string path = AppDomain.CurrentDomain.BaseDirectory;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + PicturePath.Substring(PicturePath.LastIndexOf("UserPictures\\") - 1));
            string fileName = PicturePath.Substring(PicturePath.LastIndexOf("UserPictures\\") - 1);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Like(string UserId, int PictureId)
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