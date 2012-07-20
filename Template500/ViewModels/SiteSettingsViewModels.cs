using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Template500.ViewModels
{
	public class SiteSettingsViewModel
	{
		[Display(Name="Blog")]
		public virtual bool Blog { get; set; }

		[Display(Name = "Public Registration")]
		public virtual bool Register { get; set; }

		[Display(Name = "Admin Registration")]
		public virtual bool RegisterAdmin { get; set; }
	}
}