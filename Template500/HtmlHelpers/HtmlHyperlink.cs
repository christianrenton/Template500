using System.Web.Mvc;
using System.Web.Mvc.Html;

public static class LinkExtensions
{
	public static MvcHtmlString Hyperlink(
		this HtmlHelper htmlHelper,
		string linkText,
		string url,
		object htmlAttributes = null
	)
	{
		return htmlHelper.ActionLink(linkText, "To", "Navigate", new { href = url }, htmlAttributes);
	}
}