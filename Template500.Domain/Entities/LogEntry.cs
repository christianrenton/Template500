using System;

namespace Template500.Domain.Entities
{
	public class LogEntry
	{
		public virtual int Id { get; set; }
		public virtual string Message { get; set; }
		public virtual string Code { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string User { get; set; }
		public virtual string Application { get; set; }
		public virtual string View { get; set; }

		protected LogEntry() { }

		public LogEntry(string message, string errorCode = null, string user = null, string application = null, string view = null)
		{
			if (string.IsNullOrWhiteSpace(user))
				user = "Anonymous";
			this.Date = DateTime.UtcNow;
			this.Message = message;
			this.Code = errorCode;
			this.User = user;
			this.Application = application;
			this.View = view;
		}
	}	
}
