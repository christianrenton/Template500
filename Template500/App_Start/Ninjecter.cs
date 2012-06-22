using System.Web.Mvc;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Template500.Controllers.ActionFilters;
using Template500.Domain.Entities;
using Template500.Services.Interfaces;
using Template500.Services.Registrations;
using Template500.ViewModels;

namespace Template500.App_Start
{
	public static class Ninjecter
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel(new NHibernateModule());
			RegisterServices(kernel);
			return kernel;
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		public static void RegisterServices(IKernel kernel)
		{
			// Services
			Services.Registrations.Bindings.RegisterServices(kernel);
			
			//Attributes
			kernel.BindFilter<TransactionAttribute>(FilterScope.Action, 0).WhenActionMethodHas<TransactionAttribute>();
		}
	}
}