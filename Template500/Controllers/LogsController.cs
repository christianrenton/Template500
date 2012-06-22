using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Template500.Controllers.ActionFilters;
using Template500.Domain.Entities;
using Template500.Services.Interfaces;
using Template500.ViewModels;
using AutoMapper;

namespace Template500.Controllers
{
	[Authenticate(Roles="Dev")]
	public class LogsController : _Controller
	{
		[Inject]
		public ILogEntryService _logService { get; set; }

		[Transaction]
		public ActionResult Index()
		{
			IList<LogEntryViewModel> model = new List<LogEntryViewModel>();
			IList<LogEntry> logs = _logService.SelectAll();
			foreach (LogEntry log in logs)
			{
				model.Add(Mapper.Map<LogEntry, LogEntryViewModel>(log));
			}

			return View(model);
		}
	}
}
