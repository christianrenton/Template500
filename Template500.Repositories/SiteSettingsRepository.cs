using NHibernate;
using Template500.Domain.Entities;
using Template500.Repositories.Interfaces;
using System.Collections.Generic;

namespace Template500.Repositories
{
	public class SiteSettingsRepository : Repository<SiteSettings>, ISiteSettingsRepository
	{
		public SiteSettingsRepository(ISession session)
		{
			base.InjectSession(session);
		}
	}
}
