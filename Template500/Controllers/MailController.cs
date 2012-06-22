using ActionMailer.Net.Mvc;
using Template500.ViewModels;

namespace Template500.Controllers
{
	public class MailController : MailerBase
	{
		public EmailResult SendContact(ContactViewModel model)
		{
			To.Add("hello@Template500.co.uk");
			From = "hello@Template500.co.uk";
			ReplyTo.Add(model.From);
			Subject = model.Subject;
			return Email("Contact", model);
		}

	}
}
