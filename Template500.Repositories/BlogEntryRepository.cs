using NHibernate;
using Template500.Domain.Entities;
using Template500.Repositories.Interfaces;
using System.Collections.Generic;

namespace Template500.Repositories
{
	public class BlogEntryRepository : Repository<BlogEntry>, IBlogEntryRepository
	{
		public BlogEntryRepository(ISession session)
		{
			base.InjectSession(session);
		}

		public override IList<BlogEntry> SelectAll(int limit)
		{
			return _session.CreateCriteria<BlogEntry>()
						   .SetMaxResults(limit)
						   .AddOrder(new NHibernate.Criterion.Order("Date", false))
						   .List<BlogEntry>();
		}

		public BlogEntry Get(string url)
		{
			return _session.Get<BlogEntry>(url);
		}

		public override IList<BlogEntry> SelectAll()
		{
			return _session.QueryOver<BlogEntry>().OrderBy(x => x.Date).Desc.List<BlogEntry>();
		}

		public void Delete(string url)
		{
			BlogEntry blogEntry = Get(url);
			_session.Delete(blogEntry);
		}
	}
}
