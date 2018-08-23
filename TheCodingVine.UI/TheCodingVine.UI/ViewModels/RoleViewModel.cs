using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Model.Identities;
using TheCodingVine.UI.App_Start;

namespace TheCodingVine.UI.ViewModels
{
    public class RoleViewModel: SiteLinksVM
    {
        public RoleViewModel() { }

        public RoleViewModel(RegisterViewModel role)
        {
            //Id = role.Id;
            //Name = role.;
            Email = role.Email;
            Password = role.Password;
            ConfirmPassword = role.ConfirmPassword;
            RoleName = role.RoleName;

            
            
        }
        public void SetUser(AppUser role)
        {
            Email = role.Email;
            Password = role.PasswordHash;
            //ConfirmPassword = role.ConfirmPassword;
            //RoleName = role.RoleName;
        }

        //public string Id { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
    }
}
