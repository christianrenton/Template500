using System;
using System.Collections.Generic;
using Template500.Repositories.Interfaces;
using Template500.Services.Interfaces;

namespace Template500.Services
{
	public class Service<DomainType, RepositoryInterface> : IService<DomainType>
		where DomainType : class
		where RepositoryInterface : IRepository<DomainType>
	{
		public RepositoryInterface _repository;

		// Inject
		internal void InjectRepository(RepositoryInterface repository)
		{
			if (repository == null) throw new ArgumentNullException(
			   typeof(RepositoryInterface).AssemblyQualifiedName + "repository can not be null");

			_repository = repository;
		}
		// --------------------------------

		public virtual DomainType Get(int ID)
		{
			return _repository.Get(ID);
		}

		public virtual IList<DomainType> SelectAll()
		{
			return _repository.SelectAll();
		}

		public virtual IList<DomainType> SelectAll(int limit)
		{
			return _repository.SelectAll(limit);
		}

		public virtual DomainType Save(DomainType domainType)
		{
			return _repository.Save(domainType);
		}

		public virtual void Delete(DomainType domainType)
		{
			_repository.Delete(domainType);
		}

		public virtual void Delete(int ID)
		{
			DomainType domainType = _repository.Get(ID);
			_repository.Delete(domainType);
		}

		public Service() { }
	}
}
