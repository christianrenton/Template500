using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Template500.App_Start;

namespace Template500
{
	public class MvcApplication : HttpApplication
	{

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			/*
			 * Initialise Inversion of Control
			 * Includes the database initialisation using NHibernate
			 */
			Ninjecter.Start();
		}

		protected void Application_End()
		{
			Ninjecter.Stop();
		}


		public MvcApplication()
		{
			this.EndRequest += MvcApplication_EndRequest;
		}

		private void MvcApplication_EndRequest(object sender, System.EventArgs e)
		{
			if (Context.Items.Contains(Services.Registrations.NHibernateModule.SESSION_KEY))
			{
				NHibernate.ISession Session = (NHibernate.ISession)Context.Items[Services.Registrations.NHibernateModule.SESSION_KEY];
				Session.Dispose();
				Context.Items[Services.Registrations.NHibernateModule.SESSION_KEY] = null;
			}
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Terms", // Route name
				"Terms", // URL with parameters
				new { controller = "Home", action = "Terms", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Privacy", // Route name
				"Privacy", // URL with parameters
				new { controller = "Home", action = "Privacy", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"About", // Route name
				"About", // URL with parameters
				new { controller = "Home", action = "About", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Contact", // Route name
				"Contact", // URL with parameters
				new { controller = "Home", action = "Contact", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"MessageSent", // Route name
				"MessageSent", // URL with parameters
				new { controller = "Home", action = "ContactMessageSent", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Dashboard", // Route name
				"Dashboard", // URL with parameters
				new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}