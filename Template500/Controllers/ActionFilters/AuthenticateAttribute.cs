using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Ninject;
using Template500.Domain.Entities;
using Template500.Services.Interfaces;

namespace Template500.Controllers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthenticateAttribute : AuthorizeAttribute
	{
		[Inject]
		public ILogEntryService log { private get; set; }

		protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.Request.IsAuthenticated)
			{
				CustomErrorsSection customErrors = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");
				CustomError custom403 = customErrors.Errors.Get("403");
				filterContext.Result = new RedirectResult("/" + custom403.Redirect);

				log.Save(new LogEntry("Unauthorised Access Attempt",
									   "403",
									   filterContext.HttpContext.User.Identity.Name,
									   Membership.ApplicationName,
									   filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
									   + "/" + filterContext.ActionDescriptor.ActionName));
			}
			else
			{
				base.HandleUnauthorizedRequest(filterContext);
			}
		}
	}

}