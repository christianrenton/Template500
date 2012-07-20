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
			if (_settingsService.Get() == null)
				_settingsService.Save(new SiteSettings(false, true, false));

			return View();
		}

		[Transaction]
		[Authenticate(Roles = "Admin, Dev")]
		public ActionResult Dashboard()
		{
			SiteSettingsViewModel model = Mapper.Map<SiteSettings, SiteSettingsViewModel>(_settingsService.Get());
			return View(model);
		}

		[HttpPost]
		[Transaction]
		[Authenticate(Roles = "Admin, Dev")]
		public ActionResult Dashboard(SiteSettingsViewModel model)
		{
			if (ModelState.IsValid)
			{
				SiteSettings settings = _settingsService.Save(Mapper.Map<SiteSettingsViewModel, SiteSettings>(model));
				model = Mapper.Map<SiteSettings, SiteSettingsViewModel>(settings);
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
