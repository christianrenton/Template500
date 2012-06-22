using Template500.Domain.Entities;
using Template500.Repositories.Interfaces;
using Template500.Services.Interfaces;
using System.Collections.Generic;

namespace Template500.Services
{
	public class SiteSettingsService : Service<SiteSettings, ISiteSettingsRepository>, ISiteSettingsService
	{
		public SiteSettingsService(ISiteSettingsRepository repository)
		{
			base.InjectRepository(repository);
		}
		
		public SiteSettings Get()
		{
			return _repository.Get(1);
		}
	}
}
