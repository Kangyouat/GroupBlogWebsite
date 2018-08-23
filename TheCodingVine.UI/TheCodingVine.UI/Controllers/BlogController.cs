using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using TheCodingVine.Data;
using TheCodingVine.Model;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Queries;
using TheCodingVine.Model.Tables;
using TheCodingVine.UI.ViewModels;

namespace TheCodingVine.UI.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MyBloggers()
        {
            BlogManager manager = BlogManagerFactory.Create();

            var bloggers = manager.GetAllBloggers();

            var userVm = new UsersViewModel();

            userVm.SetUserList(bloggers);
            // need a userVM

            return View(userVm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditABlogger(string id)
        {
            BlogManager manager = BlogManagerFactory.Create();

            var bloggerToEdit = manager.GetBlogger(id);
            var editVM = new EditUserViewModel();

            editVM.SetUser(bloggerToEdit);

            return View(editVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteABlogger(string id)
        {
            BlogManager manager = BlogManagerFactory.Create();

            var userVM = new UserViewModel();

            userVM.SetUser(id);

            return View(userVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteABlogger(AppUser toDelete)
        {
            BlogManager manager = BlogManagerFactory.Create();
            manager.DeleteBlogger(toDelete.UserName);

            return RedirectToAction("MyBloggers");
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyBlogs()
        {
            UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
            var user = userMgr.FindByName(User.Identity.Name);

            var blogRollVM = new BlogRollVM();

            blogRollVM.SetBlogRoll(user.UserPosts);

            return View(blogRollVM);
        }

		[HttpGet]
		[Authorize]
		public ActionResult ViewBlog (int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blog = manager.GetBlog(id);

			var blogVM = new BlogVM();
			blogVM.SetPost(blog);

			return View(blogVM);
		}

		[HttpGet]
		[Authorize]
		public ActionResult ViewSinglePending(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blog = manager.GetBlog(id);

			var blogVM = new BlogVM();
			blogVM.SetPost(blog);

			return View(blogVM);
		}

		[HttpGet]
		[Authorize]
		public ActionResult ViewSingleAll(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blog = manager.GetBlog(id);

			var blogVM = new BlogVM();
			blogVM.SetPost(blog);

			return View(blogVM);
		}

		[HttpGet]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult GetAllBlogs()
        {
            BlogManager manager = BlogManagerFactory.Create();
            var blogRoll = manager.GetAllBlogs();

            var blogRollVM = new BlogRollVM();
            blogRollVM.SetBlogRoll(blogRoll);

            return View(blogRollVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult AddBlogPost()
        {
            BlogPost blog = new BlogPost();

            var blogVM = new BlogVM();
            blogVM.SetPost(blog);

            return View(blogVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult AddBlogPost(BlogVM model)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(model.Blog.Title))
                {
                    ModelState.AddModelError("Title", "You must enter a title.");
                }
                else if (string.IsNullOrEmpty(model.Blog.Content))
                {
                    ModelState.AddModelError("Content", "You must enter something in the blog body.");
                }
                else
                {

                    TheCodingVineDbContext db = new TheCodingVineDbContext();


                    BlogManager manager = BlogManagerFactory.Create();

					if(model.Blog.TagInputs != null)
					{
						string[] tags = model.Blog.TagInputs.Split(',');

						foreach (var tag in tags)
						{
							var searchTag = new SearchTag()
							{
								SearchTagBody = tag
							};
							model.Blog.SearchTags.Add(searchTag);
						}

						model.Blog.TagInputs = null;
					}

                    UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));

                    model.Blog.BlogWriter = userManager.FindByName(User.Identity.Name);

                    manager.AddBlog(model.Blog);

                    return RedirectToAction("MyBlogs");
                }
            }

            return View(model); 
        }

        [HttpGet]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult EditABlog(int id)
        {
            BlogManager manager = BlogManagerFactory.Create();
            var blogToEdit = manager.GetBlog(id);

            foreach (var tag in blogToEdit.SearchTags)
            {
                if (blogToEdit.TagInputs == null)
                {
                    blogToEdit.TagInputs = tag.SearchTagBody;
                }
                else
                {
                    blogToEdit.TagInputs = (blogToEdit.TagInputs + "," + tag.SearchTagBody);
                }
            }

            var blogVM = new BlogVM();
            blogVM.SetPost(blogToEdit);

            return View(blogVM);
        }

		[HttpPost]
		[Authorize(Roles = "Admin,BlogWriter")]
		public ActionResult EditABlog(BlogVM blogVM)
		{
			BlogManager manager = BlogManagerFactory.Create();
			
			if(blogVM.Blog.TagInputs != null)
			{
				string[] tags = blogVM.Blog.TagInputs.Split(',');

				foreach (var tag in tags)
				{
					var searchTag = new SearchTag()
					{
						SearchTagBody = tag
					};

					blogVM.Blog.SearchTags.Add(searchTag);
				}

				blogVM.Blog.TagInputs = null;
			}

			manager.UpdateBlog(blogVM.Blog);

			return RedirectToAction("MyBlogs");
		}

		[HttpGet]
		[Authorize(Roles = "Admin,BlogWriter")]
		public ActionResult DeleteABlog(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();

			var blog = manager.GetBlog(id);

			BlogVM blogVM = new BlogVM();
			blogVM.SetPost(blog);

			return View(blogVM);
		}

		[HttpPost]
		[Authorize(Roles = "Admin,BlogWriter")]
		public ActionResult DeleteABlog(BlogVM blogVM)
		{
			BlogManager manager = BlogManagerFactory.Create();
			manager.DeleteBlog(blogVM.Blog.BlogPostId);
			return RedirectToAction("MyBlogs");
		}


		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult ViewPending()
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blogRoll = manager.GetAllPending();

			var blogRollVM = new BlogRollVM();
			blogRollVM.SetBlogRoll(blogRoll);

			return View(blogRollVM);
		}

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Review(int id)
        {
            BlogManager manager = BlogManagerFactory.Create();
            BlogPost toReview = manager.GetBlog(id);
            var siteLinks = manager.GetSiteLinks();

            foreach (var tag in toReview.SearchTags)
            {
                if (toReview.TagInputs == null)
                {
                    toReview.TagInputs = tag.SearchTagBody;
                }
                else
                {
                    toReview.TagInputs = (toReview.TagInputs + "," + tag.SearchTagBody);
                }
            }

            var blogVM = new BlogVM();
            blogVM.SetPost(toReview);

            return View(blogVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Review(BlogVM model)
        {
            BlogManager manager = BlogManagerFactory.Create();

			if(model.Blog.TagInputs != null)
			{
				string[] tags = model.Blog.TagInputs.Split(',');

				foreach (var tag in tags)
				{
					var searchTag = new SearchTag()
					{
						SearchTagBody = tag
					};

					model.Blog.SearchTags.Add(searchTag);
				}

				model.Blog.TagInputs = null;
			}

            manager.UpdateBlog(model.Blog);

            return RedirectToAction("ViewPending");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewSearchResults(string searchTag)
        {
            BlogManager manager = BlogManagerFactory.Create();
            var blogRoll = manager.GetSearchResults(searchTag);
            var approvedBlogs = blogRoll.Where(b => b.IsApproved == true).Where(d => d.PostDate <= DateTime.Now);

            var blogRollVM = new BlogRollVM();
            blogRollVM.SetBlogRoll(approvedBlogs);

            return View(blogRollVM);
        }

    }
}