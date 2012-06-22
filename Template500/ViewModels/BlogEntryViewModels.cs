using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Template500.ViewModels
{
	public class BlogEntryViewModel
	{
		public string Url { get; set; }

		[Required(ErrorMessage = "Enter a title for your blog entry")]
		[StringLength(250, ErrorMessage = "Title can only be 250 characters or less")]
		public string Title { get; set; }

		[AllowHtml]
		public string Introduction { get; set; }

		[AllowHtml]
		[Required(ErrorMessage="Entry something for your blog")]
		public string Body { get; set; }

		public DateTime Date { get; set; }
	}
}