using NHibernate;
using Template500.Domain.Entities;
using Template500.Repositories.Interfaces;

namespace Template500.Repositories
{
	public class LogEntryRepository : Repository<LogEntry>, ILogEntryRepository
	{
		public LogEntryRepository(ISession session)
		{
			base.InjectSession(session);
		}
	}
}
