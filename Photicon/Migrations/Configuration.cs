namespace Photicon.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Photicon.Models;
    using System;
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

            
        }
    }
}