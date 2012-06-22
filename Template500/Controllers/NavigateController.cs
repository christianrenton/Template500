using System;
using System.Configuration;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Ninject;
using Template500.Domain.Entities;
using Template500.Services.Interfaces;
using Template500.Controllers.ActionFilters;

namespace Template500.Controllers
{
	public class NavigateController : _Controller
	{
		[Inject]
		public ILogEntryService log { private get; set; }

		[Transaction]
		public RedirectResult To(string href)
		{
			bool linkIsBroken = false;

			if (string.IsNullOrWhiteSpace(href))
			{
				log.Save(new LogEntry("Empty Link", "404", User.Identity.Name, Membership.ApplicationName, href));

				CustomErrorsSection customErrors = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");
				CustomError custom404 = customErrors.Errors.Get("404");

				return Redirect("/" + custom404.Redirect);
			}
			else
			{
				try
				{
					WebRequest http = HttpWebRequest.Create(href);
					http.Method = "HEAD";
					http.Timeout = 5000;
					HttpWebResponse httpresponse = (HttpWebResponse)http.GetResponse();

					if (httpresponse.StatusCode != HttpStatusCode.OK)
					{
						linkIsBroken = true;
					}
				}
				catch (Exception)
				{
					linkIsBroken = true;
				}
			}

			if (linkIsBroken)
			{
				log.Save(new LogEntry("Broken Link", "404", User.Identity.Name, Membership.ApplicationName, href));

				CustomErrorsSection customErrors = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");
				CustomError custom404 = customErrors.Errors.Get("404");

				return Redirect("/" + custom404.Redirect);
			}

			return Redirect(href);
		}
	}
}