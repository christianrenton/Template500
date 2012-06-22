using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Template500.ViewModels
{
	public class ContactViewModel
	{
		[Required(ErrorMessage = "Enter your email address")]
		[Email(ErrorMessage = "Invalid email format")]
		[Display(Name = "Your Email Address")]
		public string From { get; set; }

		public string Subject { get; set; }

		[Required(ErrorMessage = "A message is required")]
		public string Message { get; set; }

		[StringLength(0)]
		public string IsNotHuman { get; set; }
	}

}