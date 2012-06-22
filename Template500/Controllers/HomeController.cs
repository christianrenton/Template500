using System.Web.Mvc;
using Template500.ViewModels;
using Template500.Controllers.ActionFilters;
using Ninject;
using Template500.Services.Interfaces;
using Template500.Domain.Entities;
using AutoMapper;

namespace Template500.Controllers
{
	public class HomeController : _Controller
	{
		[Inject]
		public ISiteSettingsService _settingsService { get; set; }

		[Transaction]
		public ActionResult Index()
		{
			// Set up site settings
			SiteSettings settings = _settingsService.Get();

			if (settings == null)
			{
				settings = new SiteSettings(false, true, false);
				settings = _settingsService.Save(settings);
			}
			return View();
		}

		[Transaction]
		[Authenticate(Roles = "Admin, Dev")]
		public ActionResult Dashboard()
		{
			SiteSettings siteSettings = _settingsService.Get();
			SiteSettingsViewModel model = Mapper.Map<SiteSettings, SiteSettingsViewModel>(siteSettings);
			return View(model);
		}

		[HttpPost]
		[Transaction]
		[Authenticate(Roles = "Admin, Dev")]
		public ActionResult Dashboard(SiteSettingsViewModel model)
		{
			if (ModelState.IsValid)
			{
				SiteSettings settings = Mapper.Map<SiteSettingsViewModel, SiteSettings>(model);
				settings.Id = 1;
				_settingsService.Save(settings);
			}
			return View(model);
		}

		public ViewResult QuoteRequestSent()
		{
			return View();
		}

		public ViewResult Contact()
		{
			ContactViewModel model = new ContactViewModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult Contact(ContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				new MailController().SendContact(model).DeliverAsync();
				return RedirectToAction("ContactMessageSent");
			}
			else
			{
				return View("Contact", model);
			}
		}

		public ViewResult ContactMessageSent()
		{
			return View();
		}

		public ViewResult About()
		{
			return View();
		}

		public ViewResult Terms()
		{
			return View();
		}

		public ViewResult Privacy()
		{
			return View();
		}
	}
}
