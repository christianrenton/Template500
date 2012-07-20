using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Ninject;
using Template500.Controllers.ActionFilters;
using Template500.Domain.Entities;
using Template500.Services.Interfaces;
using Template500.ViewModels;

namespace Template500.Controllers
{
	[SiteSettings]
	public class BlogController : _Controller
	{
		[Inject]
		public IBlogEntryService blogService { get; set; }
		[Inject]
		public ILogEntryService log { private get; set; }

		[Transaction]
		public ActionResult Index()
		{
			IList<BlogEntryViewModel> blog = new List<BlogEntryViewModel>();
			IList<BlogEntry> blogEntries = blogService.SelectAll(12);

			if (blogEntries.Count == 0) return RedirectToAction("New");

			foreach (BlogEntry blogEntry in blogEntries)
			{
				blog.Add(Mapper.Map<BlogEntry, BlogEntryViewModel>(blogEntry));
			}

			return View(blog);
		}

		[Transaction]
		public ActionResult Archive()
		{
			IList<BlogEntryViewModel> blog = new List<BlogEntryViewModel>();
			IList<BlogEntry> blogEntries = blogService.SelectAll();

			if (blogEntries.Count == 0) return RedirectToAction("New");

			foreach (BlogEntry blogEntry in blogEntries)
			{
				blog.Add(Mapper.Map<BlogEntry, BlogEntryViewModel>(blogEntry));
			}

			return View(blog);
		}

		[Transaction]
		public ActionResult Article(string id)
		{
			if (!string.IsNullOrWhiteSpace(id))
			{
				BlogEntry blogEntry = blogService.Get(id);
				BlogEntryViewModel model = Mapper.Map<BlogEntry, BlogEntryViewModel>(blogEntry);

				if (string.IsNullOrWhiteSpace(model.Title))
				{
					return RedirectToAction("Index");
				}
				return View(model);
			}
			else
			{
				return RedirectToAction("Index");
			}
		}

		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult New()
		{
			return View();
		}

		[HttpPost]
		[Transaction]
		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult New(BlogEntryViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Url = model.Title.Replace(" ", "-");
				model.Url = Regex.Replace(model.Url, @"[^\w\-]", "");
				model.Url = Regex.Replace(model.Url, @"-{2,}", "-");

				BlogEntry existingBlogEntry = blogService.Get(model.Url);

				if (existingBlogEntry == null)
				{
					model.Body = HttpUtility.HtmlEncode(model.Body);
					BlogEntry newBlogEntry = Mapper.Map<BlogEntryViewModel, BlogEntry>(model);
					blogService.Save(newBlogEntry);
				}
				else
				{
					ModelState.AddModelError("duplicateTitle", "This title either already exists or is too similar to one that already exists");
					return View(model);
				}
			}
			return RedirectToAction("Article", new { id = model.Url });
		}

		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult Edit(string id)
		{
			if (!string.IsNullOrWhiteSpace(id))
			{
				BlogEntry blogEntry = blogService.Get(id);
				BlogEntryViewModel model = Mapper.Map<BlogEntry, BlogEntryViewModel>(blogEntry);

				if (string.IsNullOrWhiteSpace(model.Url))
				{
					return RedirectToAction("Index");
				}

				model.Body = HttpUtility.HtmlDecode(model.Body);
				return View(model);
			}
			else
			{
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[Transaction]
		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult Edit(BlogEntryViewModel model, string id)
		{
			if (ModelState.IsValid)
			{
				model.Url = id;
				model.Body = HttpUtility.HtmlEncode(model.Body);
				BlogEntry blogEntry = Mapper.Map<BlogEntryViewModel, BlogEntry>(model);

				blogService.Update(blogEntry);
			}
			return RedirectToAction("Article", new { id = id });
		}

		[Transaction]
		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult Delete(string id)
		{
			blogService.Delete(id);
			return RedirectToAction("Index");
		}

		[Authenticate(Roles = "Dev, Admin")]
		public PartialViewResult Gallery()
		{
			string galleryPath = Server.MapPath("~/img/BlogGallery");
			DirectoryInfo dir = new DirectoryInfo(galleryPath);
			FileInfo[] files = dir.GetFiles();
			return PartialView(files);
		}

		[HttpPost]
		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult Gallery(HttpPostedFileBase file)
		{
			if (file != null)
			{
				if (file != null &&
					file.ContentLength > 0 && file.ContentLength < 1000000 &&
					file.ContentType.StartsWith("image/"))
				{
					var fileName = Path.GetFileName(file.FileName);
					var path = Path.Combine(Server.MapPath("~/img/BlogGallery"), fileName);
					file.SaveAs(path);
				}
			}

			return RedirectToAction("Gallery");
		}

		[Authenticate(Roles = "Dev, Admin")]
		public ActionResult DeleteImage(string id)
		{
			string fileName = id;
			var path = Path.Combine(Server.MapPath("~/img/BlogGallery"), fileName);

			if (!string.IsNullOrWhiteSpace(path))
			{
				try
				{
					System.IO.File.Delete(path);
				}
				catch (Exception e)
				{
					log.Save(new LogEntry("Exception", e.Message, User.Identity.Name, Membership.ApplicationName, "/Blog/DeleteImage/" + fileName));
				}
			}

			return RedirectToAction("Gallery");
		}
	}
}