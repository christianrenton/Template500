using System;
using System.Web.Mvc;
using System.Web.Security;
using Template500.Controllers.ActionFilters;
using Template500.ViewModels;
using Template500.Domain.Enums;

namespace Template500.Controllers
{
	/// <summary>
	/// The Account Controller manages the authentication
	/// of the application. It doesn't use the normal repository
	/// pattern and goes directly to the Membership tables.
	/// </summary>
	public class AccountController : _Controller
	{
		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.UserName, model.Password))
				{
					FormsAuthentication.SetAuthCookie(model.UserName, true);
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
						&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[Authenticate]
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Home");
		}

		[SiteSettings(setting = SiteSetting.Register)]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[SiteSettings(setting = SiteSetting.Register)]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Attempt to register the user
				MembershipCreateStatus createStatus;
				Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

				if (createStatus == MembershipCreateStatus.Success)
				{
					// The very first user who register’s is a DEV
					// Subsequent registrations are Admins and no roles
					if (!Roles.RoleExists("Dev"))
					{
						Roles.CreateRole("Dev");
						Roles.AddUserToRole(model.UserName, "Dev");
					}

					FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", ErrorCodeToString(createStatus));
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[Authenticate(Roles = "Dev, Admin")]
		[SiteSettings(setting = SiteSetting.RegisterAdmin)]
		public ActionResult RegisterAdmin()
		{
			return View();
		}

		[HttpPost]
		[Authenticate(Roles = "Dev, Admin")]
		[SiteSettings(setting = SiteSetting.RegisterAdmin)]
		public ActionResult RegisterAdmin(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Attempt to register the new admin user
				MembershipCreateStatus createStatus;
				MembershipUser user = Membership.GetUser(model.UserName);
				if (user == null)
				{
					Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
				}
				else
				{
					createStatus = MembershipCreateStatus.DuplicateUserName;
				}

				if (createStatus == MembershipCreateStatus.Success || createStatus == MembershipCreateStatus.DuplicateUserName)
				{
					// The very first user who register’s is a Dev
					// Devs and Admins can register further Admins.
					if (!Roles.RoleExists("Admin")) Roles.CreateRole("Admin");

					Roles.AddUserToRole(model.UserName, "Admin");

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", ErrorCodeToString(createStatus));
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[Authenticate]
		public ActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[Authenticate]
		public ActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{

				// ChangePassword will throw an exception rather
				// than return false in certain failure scenarios.
				bool changePasswordSucceeded;
				try
				{
					MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
					changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
				}
				catch (Exception)
				{
					changePasswordSucceeded = false;
				}

				if (changePasswordSucceeded)
				{
					return RedirectToAction("ChangePasswordSuccess");
				}
				else
				{
					ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[Authenticate]
		public ActionResult ChangePasswordSuccess()
		{
			return View();
		}

		[Authenticate(Roles = "Dev")]
		public ActionResult ListUsers()
		{
			MembershipUserCollection users = Membership.GetAllUsers();
			return View(users);
		}

		#region Status Codes
		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}
		#endregion
	}
}
