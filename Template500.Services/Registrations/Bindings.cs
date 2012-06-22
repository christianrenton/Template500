using Ninject;
using Template500.Repositories;
using Template500.Repositories.Interfaces;
using Template500.Services.Interfaces;

namespace Template500.Services.Registrations
{
	public static class Bindings
	{
		public static void RegisterServices(IKernel kernel)
		{
			// Services
			kernel.Bind<IBlogEntryService>().To<BlogEntryService>();
			kernel.Bind<ISiteSettingsService>().To<SiteSettingsService>();

			// Repositories
			kernel.Bind<IBlogEntryRepository>().To<BlogEntryRepository>();
			kernel.Bind<ISiteSettingsRepository>().To<SiteSettingsRepository>();

			// Logging
			kernel.Bind<ILogEntryService>().To<LogEntryService>();
			kernel.Bind<ILogEntryRepository>().To<LogEntryRepository>();
		}
	}
}
