using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Model.Identities;

namespace TheCodingVine.UI.ViewModels
{
    public class UserViewModel: SiteLinksVM
    {
        public string User { get; set; }

        public UserViewModel()
        {
            
        }

        public void SetUser(string user)
        {
            User = user;
        }
    }
}