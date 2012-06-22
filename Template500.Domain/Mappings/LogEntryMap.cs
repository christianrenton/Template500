using FluentNHibernate.Mapping;
using Template500.Domain.Entities;

namespace Template500.Domain.Mappings
{
	public class LogEntryMap : ClassMap<LogEntry>
	{
		public LogEntryMap()
		{
			Table("Logs");

			Id(x => x.Id);

			Map(x => x.Application);
			
			Map(x => x.Code);

			Map(x => x.Date).Not.Nullable().Default("getdate()");

			Map(x => x.Message).Not.Nullable();
			
			Map(x => x.User);

			Map(x => x.View);
		}
	}
}
