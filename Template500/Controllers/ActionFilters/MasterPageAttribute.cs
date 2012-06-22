using System;
using System.Web.Mvc;
using Ninject;
using Template500.Services.Interfaces;

namespace Template500.Controllers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class MasterPageAttribute : ActionFilterAttribute
	{
		[Inject]
		public IBlogEntryService _blogService { private get; set; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.Controller.ViewBag.Blogs = _blogService.SelectAll(5);
			filterContext.Controller.ViewBag.Release = true;
#if DEBUG
			filterContext.Controller.ViewBag.Release = false;
#endif

		}
	}
}