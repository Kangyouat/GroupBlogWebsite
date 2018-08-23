using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Queries;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.Data
{
    public interface IRepo
    {
        IEnumerable<BlogPost> GetAllBlogs();
		IEnumerable<BlogPost> GetAllPending();
		IEnumerable<BlogPost> GetSearchResults(string searchTag);

        BlogPost GetBlog(int id);
        void AddBlog(BlogPost toAdd);
        void UpdateBlog(BlogPost toUpdate);
		void DeleteBlog(int id);

        IEnumerable<StaticPost> GetAllStaticPosts();
		IEnumerable<SiteStaticLink> GetStaticLinks();
        StaticPost GetStaticPost(int id);
        void AddStaticPost(StaticPost pageToAdd);
        void UpdateStaticPost(StaticPost pageToUpdate);
		void DeleteStaticPost(int id);

		//IEnumerable<AppRole> GetRoles();
		//AppRole GetRole(string id);

		IEnumerable<AppUser> GetAllBloggers();
		AppUser GetBlogger(string id);
        void UpdateBlogger(AppUser user);
        void DeleteBlogger(string toDelete);
	}
}
