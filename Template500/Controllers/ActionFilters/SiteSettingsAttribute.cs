using System;
using System.Web.Mvc;
using Ninject;
using Template500.Domain.Entities;
using Template500.Services.Interfaces;
using Template500.Domain.Enums;

namespace Template500.Controllers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class SiteSettingsAttribute : ActionFilterAttribute
	{
		[Inject]
		public ISiteSettingsService _siteSettings { private get; set; }

		public SiteSetting setting { get; set; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			SiteSettings settings = _siteSettings.Get();
			bool allowed = false;

			switch (setting)
			{
				case SiteSetting.Blog: allowed = settings.Blog; break;
				case SiteSetting.Register: allowed = settings.Register; break;
				case SiteSetting.RegisterAdmin: allowed = settings.RegisterAdmin; break;
				default: allowed = true; break;
			}

			if (!allowed)
			{
				filterContext.Result = new RedirectResult("/");
			}
		}
	}
}