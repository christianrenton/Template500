using System.Collections;
using System.Diagnostics;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Template500.Domain.Mappings;

namespace Template500.Services.Registrations
{
	public class NHibernateModule : NinjectModule
	{
		public const string SESSION_KEY = "NHibernate.ISession";

		public override void Load()
		{
			var fluentConfig = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008.DoNot.UseReflectionOptimizer()
				.ConnectionString(c => c.FromConnectionStringWithKey("Template500DB")))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogEntryMap>())
				.ExposeConfiguration(x => x.SetProperty("current_session_context_class", "web"))
				.ExposeConfiguration(BuildSchema);

			Bind<Configuration>().ToConstant(fluentConfig.BuildConfiguration());
			Bind<ISessionFactory>().ToConstant(fluentConfig.BuildSessionFactory());
			Bind<ISession>().ToMethod(x => GetRequestSession(x));
		}

		private static void BuildSchema(Configuration config)
		{
			// this NHibernate tool takes a configuration (with mapping info in)
			// and exports a database schema from it
			SchemaMetadataUpdater.QuoteTableAndColumns(config);
			new SchemaUpdate(config).Execute(false, true);
		}

		private ISession GetRequestSession(IContext Ctx)
		{
			IDictionary Dict = ReflectiveHttpContext.HttpContextCurrentItems;
			NHibernate.ISession Session;
			if (!Dict.Contains(SESSION_KEY))
			{
				// Create an NHibernate session for this request
				Debug.WriteLine("Creating an NHIbernate session");
				Session = Ctx.Kernel.Get<ISessionFactory>().OpenSession();
				Dict.Add(SESSION_KEY, Session);
				CurrentSessionContext.Bind(Session);
			}
			else
			{
				// Re-use the NHibernate session for this request
				Debug.WriteLine("Re-using the NHibernate session");
				Session = (NHibernate.ISession)Dict[SESSION_KEY];
			}
			return Session;
		}
	}
}