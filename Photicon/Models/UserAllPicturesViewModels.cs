using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photicon.Models
{
    public class UserAllPicturesViewModels
    {
        public Users User { get; set; }
        public ICollection<Pictures> Pictures { get; set; }
    }
}