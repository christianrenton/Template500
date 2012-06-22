using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Template500.ViewModels
{
	public class SiteSettingsViewModel
	{
		[Display(Name="Blog")]
		public virtual bool DisplayBlog { get; set; }

		[Display(Name = "Public Registration")]
		public virtual bool AllowPublicRegistration { get; set; }

		[Display(Name = "Admin Registration")]
		public virtual bool AllowAdminRegistration { get; set; }
	}
}