using Template500.Domain.Entities;
using System.Collections.Generic;

namespace Template500.Services.Interfaces
{
	public interface ISiteSettingsService : IService<SiteSettings>
	{
		SiteSettings Get();
	}
}
