using System;
using System.Collections.Generic;
using NHibernate;
using Template500.Repositories.Interfaces;

namespace Template500.Repositories
{
	public abstract class Repository<DomainType> : IRepository<DomainType> where DomainType : class
	{
		public ISession _session;

		// Inject NHibernate session.
		internal void InjectSession(ISession session)
		{
			if (session == null) { throw new NullReferenceException("Session can not be null"); }

			_session = session;
		}
		// --------------------------------

		public virtual DomainType Get(int ID)
		{
			return _session.Get<DomainType>(ID);
		}

		public virtual IList<DomainType> SelectAll()
		{
			return _session.QueryOver<DomainType>().List<DomainType>();
		}

		public virtual IList<DomainType> SelectAll(int limit)
		{
			return _session.CreateCriteria<DomainType>()
						   .SetMaxResults(limit)
						   .List<DomainType>();
		}

		public virtual DomainType Save(DomainType domainType)
		{
			_session.SaveOrUpdate(domainType);
			return domainType;
		}

		public virtual void Delete(DomainType domainType)
		{
			_session.Delete(domainType);
		}
		
		public Repository() { }
	
	}
}
