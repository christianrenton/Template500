using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using System.Data;
using Ninject;

namespace Template500.Controllers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class TransactionAttribute : ActionFilterAttribute
	{

		private readonly ITransaction _currentTransaction;
		private readonly ISession _NHibernateSession;

		[Inject]
		public TransactionAttribute(ISession nhibernateSession)
		{
			if (nhibernateSession == null) { throw new NullReferenceException("NHibernateSession can not be null"); }
			_NHibernateSession = nhibernateSession;
			_currentTransaction = _NHibernateSession.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public TransactionAttribute()
		{

		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_currentTransaction.Begin();
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (_currentTransaction.IsActive)
			{
				if (filterContext.Exception == null)
				{
					_currentTransaction.Commit();
				}
				else
				{
					_currentTransaction.Rollback();
				}
			}
		}
	}
}