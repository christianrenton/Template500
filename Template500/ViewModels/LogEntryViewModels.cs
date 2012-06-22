using System;

namespace Template500.ViewModels
{
	public class LogEntryViewModel
	{
		public string Message { get; set; }
		public string Code { get; set; }
		public DateTime Date { get; set; }
		public string User { get; set; }
		public string Application { get; set; }
		public string View { get; set; }
	}
}