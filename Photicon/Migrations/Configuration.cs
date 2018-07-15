namespace Photicon.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Photicon.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Photicon.Models.PhoticonDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Photicon.Models.PhoticonDB context)
        {
            var userManager = new UserManager<Users>(new UserStore<Users>(context));

            //seed users
            if (!(context.Users.Any(u => u.UserName == "Barbara")))
            {
                var userToInsert = new Users { Id = "c08aeef6-78ad-4016-be74-851465764d93", Email = "Ba@mail.pt", UserName = "Barbara", ProfilePhoto = "/UserPictures/userc08aeef6-78ad-4016-be74-851465764d93_Barbara.jpg" };
                userManager.Create(userToInsert, "Ba1234!");
            }
            if (!(context.Users.Any(u => u.UserName == "william")))
            {
                var userToInsert = new Users { Id = "c08aeda9-78ad-4016-be81-851465785d93", Email = "william@gmail.com", UserName = "william", ProfilePhoto = "/UserPictures/userc08aeda9-78ad-4016-be81-851465785d93_William.jpg" };
                userManager.Create(userToInsert, "#1William1#");
            }
            if (!(context.Users.Any(u => u.UserName == "yasmin")))
            {
                var userToInsert = new Users { Id = "c08ae574-78ac-1004-be81-851465786c80", Email = "yasmin@hotmail.com", UserName = "yasmin", ProfilePhoto = "/UserPictures/userc08ae574-78ac-1004-be81-851465786c80_Yasmin.jpg" };
                userManager.Create(userToInsert, "Yas1234!");
            }
            if (!(context.Users.Any(u => u.UserName == "Rui")))
            {
                var userToInsert = new Users { Id = "c10ae801-789c-1004-be81-851465786000", Email = "rui@mail.com", UserName = "Rui", ProfilePhoto = "/UserPictures/userc10ae801-789c-1004-be81-851465786000_Rui.jpg" };
                userManager.Create(userToInsert, "Rui123!");
            }
            if (!(context.Users.Any(u => u.UserName == "Xavi")))
            {
                var userToInsert = new Users { Id = "carhn57-1dr2-1004-be81-8514651swoi9", Email = "xavi@mail.com", UserName = "Xavi", ProfilePhoto = "/UserPictures/usercarhn57-1dr2-1004-be81-8514651swoi9_Xavier.jpg" };
                userManager.Create(userToInsert, "!1Xavi1!");
            }
            if (!(context.Users.Any(u => u.UserName == "Cris")))
            {
                var userToInsert = new Users { Id = "14dsmny-1dse-245f-be87-1damdj6g5901", Email = "cris@mail.com", UserName = "Cris", ProfilePhoto = "/UserPictures/user14dsmny-1dse-245f-be87-1damdj6g5901_Cristina.jpg" };
                userManager.Create(userToInsert, "Cris!1234");
            }
            if (!(context.Users.Any(u => u.UserName == "louise")))
            {
                var userToInsert = new Users { Id = "mnhda34-1nda-4df1-67gh-d5f3d4fa5jh6", Email = "lu@hotmail.com", UserName = "louise", ProfilePhoto = "/UserPictures/usermnhda34-1nda-4df1-67gh-d5f3d4fa5jh6_Louise.jpg" };
                userManager.Create(userToInsert, "Louise1!");
            }
            if (!(context.Users.Any(u => u.UserName == "pete1000")))
            {
                var userToInsert = new Users { Id = "90jkfar-3dfs-4d32-dcvb-2sderfge78jk", Email = "peter@mail.com", UserName = "pete1000", ProfilePhoto = "/UserPictures/user90jkfar-3dfs-4d32-dcvb-2sderfge78jk_Peter.jpg" };
                userManager.Create(userToInsert, "#Peter1000#");
            }
            if (!(context.Users.Any(u => u.UserName == "Elsa")))
            {
                var userToInsert = new Users { Id = "45fghsu-uiuy-4fgh-vbmc-32dh7huyjkom", Email = "elsa@mail.pt", UserName = "Elsa", ProfilePhoto = "/UserPictures/user45fghsu-uiuy-4fgh-vbmc-32dh7huyjkom_Elsa.jpg" };
                userManager.Create(userToInsert, "Elsinha#1");
            }
            if (!(context.Users.Any(u => u.UserName == "Sam")))
            {
                var userToInsert = new Users { Id = "908ikfh-mvj7-8901-mnbg-cv4xzxs32fsm", Email = "sam@gmail.com", UserName = "Sam", ProfilePhoto = "/UserPictures/user908ikfh-mvj7-8901-mnbg-cv4xzxs32fsm_Sam.jpg" };
                userManager.Create(userToInsert, "Samuel9#");
            }

            //seed pictures
            if (!(context.Pictures.Any(u => u.Id == 0)))
            {
                var picToInsert = new Pictures { Id = 0, Description = "My dog", Path = "/UserPictures/picture0_dog.jpg", UploadDate = new DateTime(2018,05,28), Visibility = true, UserFK = "908ikfh-mvj7-8901-mnbg-cv4xzxs32fsm" };
                var user = context.Users.Where(m => m.Id == picToInsert.UserFK).Select(m => m).SingleOrDefault();
                

                List<string> usersThatLiked = new List<string> { "Barbara", "william", "yasmin","Cris","Elsa","Sam"};
                foreach (var name in usersThatLiked)
                {
                    picToInsert.Likes++;

                    Users u = context.Users.Where(m => m.UserName == name).Select(m => m).SingleOrDefault();
                    u.LikedPictures.Add(picToInsert);

                    picToInsert.UsersThatLiked.Add(u);

                    context.Users.AddOrUpdate(u);

                }
                user.PicturesList.Add(picToInsert);
                picToInsert.User = user;

                Tags tag = new Tags();
                tag.Id = 0;
                tag.Name = "Dog";
                tag.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag);
                picToInsert.TagsList.Add(tag);

                context.Users.AddOrUpdate(user);
                context.Pictures.AddOrUpdate(picToInsert);
            }
            if (!(context.Pictures.Any(u => u.Id == 1)))
            {
                var picToInsert = new Pictures { Id = 1, Description = "My little feet", Path = "/UserPictures/picture1_feet.jpg", UploadDate = new DateTime(2018, 07, 1), Visibility = true, UserFK = "90jkfar-3dfs-4d32-dcvb-2sderfge78jk" };
                var user = context.Users.Where(m => m.Id == picToInsert.UserFK).Select(m => m).SingleOrDefault();
                

                List<string> usersThatLiked = new List<string> { "Rui" };
                foreach (var name in usersThatLiked)
                {
                    picToInsert.Likes++;

                    Users u = context.Users.Where(m => m.UserName == name).Select(m => m).SingleOrDefault();
                    u.LikedPictures.Add(picToInsert);

                    picToInsert.UsersThatLiked.Add(u);

                    context.Users.AddOrUpdate(u);

                }
                user.PicturesList.Add(picToInsert);
                picToInsert.User = user;

                Tags tag1= new Tags();
                tag1.Id = 1;
                tag1.Name = "Feet";
                tag1.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag1);
                picToInsert.TagsList.Add(tag1);

                Tags tag2 = new Tags();
                tag2.Id = 2;
                tag2.Name = "Casual";
                tag2.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag2);
                picToInsert.TagsList.Add(tag2);

                context.Users.AddOrUpdate(user);
                context.Pictures.AddOrUpdate(picToInsert);
            }
            if (!(context.Pictures.Any(u => u.Id == 2)))
            {
                var picToInsert = new Pictures { Id = 2, Description = "Me and the Abyss", Path = "/UserPictures/picture2_abyss.jpg", UploadDate = new DateTime(2018, 07, 10), Visibility = true, UserFK = "c08aeda9-78ad-4016-be81-851465785d93" };
                var user = context.Users.Where(m => m.Id == picToInsert.UserFK).Select(m => m).SingleOrDefault();
                

                List<string> usersThatLiked = new List<string> { "Barbara", "yasmin", "Cris", "Elsa", "Sam" };
                foreach (var name in usersThatLiked)
                {
                    picToInsert.Likes++;

                    Users u = context.Users.Where(m => m.UserName == name).Select(m => m).SingleOrDefault();
                    u.LikedPictures.Add(picToInsert);

                    picToInsert.UsersThatLiked.Add(u);

                    context.Users.AddOrUpdate(u);

                }
                user.PicturesList.Add(picToInsert);
                picToInsert.User = user;

                Tags tag1 = new Tags();
                tag1.Id = 3;
                tag1.Name = "Height";
                tag1.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag1);
                picToInsert.TagsList.Add(tag1);

                Tags tag2 = new Tags();
                tag2.Id = 4;
                tag2.Name = "Danger";
                tag2.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag2);
                picToInsert.TagsList.Add(tag2);

                Tags tag3 = new Tags();
                tag3.Id = 5;
                tag3.Name = "Abyss";
                tag3.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag3);
                picToInsert.TagsList.Add(tag3);

                context.Users.AddOrUpdate(user);
                context.Pictures.AddOrUpdate(picToInsert);
            }
            if (!(context.Pictures.Any(u => u.Id == 3)))
            {
                var picToInsert = new Pictures { Id = 3, Description = "Waiting...", Path = "/UserPictures/picture3_waiting.jpg", UploadDate = new DateTime(2018, 03, 2), Visibility = true, UserFK = "carhn57-1dr2-1004-be81-8514651swoi9" };
                var user = context.Users.Where(m => m.Id == picToInsert.UserFK).Select(m => m).SingleOrDefault();
                

                List<string> usersThatLiked = new List<string> { "Barbara", "william", "yasmin", "Cris" };
                foreach (var name in usersThatLiked)
                {
                    picToInsert.Likes++;

                    Users u = context.Users.Where(m => m.UserName == name).Select(m => m).SingleOrDefault();
                    u.LikedPictures.Add(picToInsert);

                    picToInsert.UsersThatLiked.Add(u);

                    context.Users.AddOrUpdate(u);

                }
                user.PicturesList.Add(picToInsert);
                picToInsert.User = user;

                Tags tag = new Tags();
                tag.Id = 6;
                tag.Name = "Wait";
                tag.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag);
                picToInsert.TagsList.Add(tag);

                Tags tag2 = new Tags();
                tag2.Id = 7;
                tag2.Name = "Snow";
                tag2.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag2);
                picToInsert.TagsList.Add(tag2);

                context.Users.AddOrUpdate(user);
                context.Pictures.AddOrUpdate(picToInsert);
            }
            if (!(context.Pictures.Any(u => u.Id == 4)))
            {
                var picToInsert = new Pictures { Id = 4, Description = "Morning Walk", Path = "/UserPictures/picture4_walk.jpg", UploadDate = new DateTime(2018, 01, 4), Visibility = true, UserFK = "c08ae574-78ac-1004-be81-851465786c80" };
                var user = context.Users.Where(m => m.Id == picToInsert.UserFK).Select(m => m).SingleOrDefault();
               

                List<string> usersThatLiked = new List<string> { "Barbara", "Cris", "Sam" };
                foreach (var name in usersThatLiked)
                {
                    picToInsert.Likes++;

                    Users u = context.Users.Where(m => m.UserName == name).Select(m => m).SingleOrDefault();
                    u.LikedPictures.Add(picToInsert);

                    picToInsert.UsersThatLiked.Add(u);

                    context.Users.AddOrUpdate(u);

                }
                user.PicturesList.Add(picToInsert);
                picToInsert.User = user;

                Tags tag = new Tags();
                tag.Id = 8;
                tag.Name = "Walk";
                tag.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag);
                picToInsert.TagsList.Add(tag);

                context.Users.AddOrUpdate(user);
                context.Pictures.AddOrUpdate(picToInsert);
            }
            if (!(context.Pictures.Any(u => u.Id == 5)))
            {
                var picToInsert = new Pictures { Id = 5, Description = "Working Hours", Path = "/UserPictures/picture5_work.jpg", UploadDate = new DateTime(2018, 04, 2), Visibility = true, UserFK = "c08ae574-78ac-1004-be81-851465786c80" };
                var user = context.Users.Where(m => m.Id == picToInsert.UserFK).Select(m => m).SingleOrDefault();
                

                List<string> usersThatLiked = new List<string> { "Barbara" };
                foreach (var name in usersThatLiked)
                {
                    picToInsert.Likes++;

                    Users u = context.Users.Where(m => m.UserName == name).Select(m => m).SingleOrDefault();
                    u.LikedPictures.Add(picToInsert);

                    picToInsert.UsersThatLiked.Add(u);

                    context.Users.AddOrUpdate(u);

                }
                user.PicturesList.Add(picToInsert);
                picToInsert.User = user;

                Tags tag = new Tags();
                tag.Id = 9;
                tag.Name = "Work";
                tag.PicturesList.Add(picToInsert);
                context.Tags.AddOrUpdate(tag);
                picToInsert.TagsList.Add(tag);

                context.Users.AddOrUpdate(user);
                context.Pictures.AddOrUpdate(picToInsert);
            }
        }
    }
}