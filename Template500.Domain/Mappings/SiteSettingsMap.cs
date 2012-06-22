using FluentNHibernate.Mapping;
using Template500.Domain.Entities;

namespace Template500.Domain.Mappings
{
	public class SiteSettingsMap : ClassMap<SiteSettings>
	{
		public SiteSettingsMap()
		{
			Id(x => x.Id);

			Map(x => x.Blog);
			Map(x => x.Register);
			Map(x => x.RegisterAdmin);
		}
	}
}
