using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Data;
using TheCodingVine.Model.Identities;

namespace TheCodingVine.UI.ViewModels
{
    public class UsersViewModel: SiteLinksVM
    {
        public List<AppUser> UserList { get; set; }
		public List<AppRole> RolesList { get; set; }


        public UsersViewModel()
        {
            UserList = new List<AppUser>();
			RolesList = new List<AppRole>();
        }

        public void SetUserList(IEnumerable<AppUser> userList)
        {
            foreach (var user in userList)
            {
                UserList.Add(user);
            }
        }

		public void SetRolesList(IEnumerable<AppRole> rolesList)
		{
			foreach(var role in rolesList)
			{
				RolesList.Add(role);
			}
		}

		private void GetAllUsers()
		{
			BlogManager manager = BlogManagerFactory.Create();
			var users = manager.GetAllBloggers();
			SetUserList(users);
		}

		//private void GetAllRoles()
		//{
		//	BlogManager manager = BlogManagerFactory.Create();
		//	var roles = manager.GetAllRoles();
		//	SetRolesList(roles);
		//}


    }
}