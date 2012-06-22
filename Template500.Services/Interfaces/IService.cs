using System.Collections.Generic;

namespace Template500.Services.Interfaces
{
	public interface IService<DomainType>
	{
		DomainType Get(int ID);
		IList<DomainType> SelectAll();
		IList<DomainType> SelectAll(int limit);
		DomainType Save(DomainType domainType);
		void Delete(DomainType domainType);
		void Delete(int ID);
	}
}