using Template500.Domain.Entities;
using Template500.Repositories.Interfaces;
using Template500.Services.Interfaces;

namespace Template500.Services
{
	public class LogEntryService : Service<LogEntry, ILogEntryRepository>, ILogEntryService
	{
		public LogEntryService(ILogEntryRepository repository)
		{
			base.InjectRepository(repository);
		}
	}
}
