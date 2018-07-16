using Photicon.Models;
using Photicon.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Photicon.Api
{
    public class AboutController : ApiController
    {
        private PhoticonDB db = new PhoticonDB();

        // GET: api/About
        public IEnumerable<UserModel> Get()
        {
            return db.Users.ToList().Select(u => new UserModel(u));
        }
    }
}