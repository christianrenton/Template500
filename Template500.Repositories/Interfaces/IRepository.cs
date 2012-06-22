using System.Collections.Generic;

namespace Template500.Repositories.Interfaces
{
	public interface IRepository<DomainType>
	{
		DomainType Get(int ID);
		IList<DomainType> SelectAll();
		IList<DomainType> SelectAll(int limit);
		DomainType Save(DomainType domainType);
		void Delete(DomainType domainType);
	}
}
