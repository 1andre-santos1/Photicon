using Photicon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photicon.Controllers
{
    public class CommentsController : Controller
    {
        PhoticonDB db = new PhoticonDB();

        [HttpPost]
        public ActionResult Add(string UserId,int PictureId,string Comment)
        {
            Users user = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).SingleOrDefault();

            Comments comment = new Comments();
            comment.Comment = Comment;
            comment.Date = DateTime.Now;
            comment.User = user;
            comment.Picture = pic;

            user.CommentsList.Add(comment);
            pic.CommentsList.Add(comment);

            db.Users.AddOrUpdate(user);
            db.Pictures.AddOrUpdate(pic);
            db.Comments.AddOrUpdate(comment);
            db.SaveChanges();
            
            if(user.PicturesList.Contains(pic))
                return RedirectToAction("View","Pictures", new { PictureId = PictureId});
            else
                return RedirectToAction("ViewOtherUserPicture", "Home", new { UserId = UserId, ViewedPictureId = PictureId });
        }

        [HttpPost]
        public JsonResult AddWithAjax(string UserId, int PictureId, string Comment)
        {
            Users user = db.Users.Where(m => m.Id == UserId).SingleOrDefault();
            Pictures pic = db.Pictures.Where(m => m.Id == PictureId).SingleOrDefault();

            Comments comment = new Comments();
            comment.Comment = Comment;
            comment.Date = DateTime.Now;
            comment.User = user;
            comment.Picture = pic;

            user.CommentsList.Add(comment);
            pic.CommentsList.Add(comment);

            db.Users.AddOrUpdate(user);
            db.Pictures.AddOrUpdate(pic);
            db.Comments.AddOrUpdate(comment);
            db.SaveChanges();

            var model = new GetCommentsViewModels { UserName = comment.User.UserName, ProfilePhoto = comment.User.ProfilePhoto, Comment = comment.Comment };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}