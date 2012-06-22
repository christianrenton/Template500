using System;

namespace Template500.Domain.Entities
{
	public class SiteSettings
	{
		public virtual int Id { get; set; }
		public virtual bool Blog { get; set; }
		public virtual bool Register { get; set; }
		public virtual bool RegisterAdmin { get; set; }

		protected SiteSettings() { }

		public SiteSettings(bool blog, bool register, bool registerAdmin)
		{
			this.Blog = blog;
			this.Register = register;
			this.RegisterAdmin = registerAdmin;
		}
	}	
}
