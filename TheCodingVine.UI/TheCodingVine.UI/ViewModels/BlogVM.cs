using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Data;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.UI.ViewModels
{
	public class BlogVM : SiteLinksVM
	{
		public BlogPost Blog { get; set; }
		public AppUser BlogWriter {get; set;}

		public BlogVM()
		{
			Blog = new BlogPost();
			BlogWriter = new AppUser();
		}

		public void SetPost(BlogPost blog)
		{
			Blog = blog;
		}

		public void SetBlogWriter(AppUser writer)
		{
			BlogWriter = writer;
		}

	}
}