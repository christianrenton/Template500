using System;

namespace Template500.Domain.Entities
{
	public class BlogEntry
	{
		public virtual string Url { get; set; }
		public virtual string Title { get; set; }
		public virtual string Intro { get; set; }
		public virtual string Body { get; set; }
		public virtual DateTime Date { get; set; }

		public BlogEntry()
		{
			this.Date = DateTime.Now;
		}
	}	
}
