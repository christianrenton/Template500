using FluentNHibernate.Mapping;
using Template500.Domain.Entities;

namespace Template500.Domain.Mappings
{
	public class BlogEntryMap : ClassMap<BlogEntry>
	{
		public BlogEntryMap()
		{
			Table("Blog");

			Id(x => x.Url);

			Map(x => x.Title);

			Map(x => x.Intro).CustomSqlType("nvarchar(MAX)");

			Map(x => x.Body).CustomSqlType("nvarchar(MAX)").Not.Nullable();

			Map(x => x.Date).Not.Nullable().Default("getdate()");
		}
	}
}
