using Template500.Domain.Entities;
using System.Collections.Generic;

namespace Template500.Repositories.Interfaces
{
	public interface IBlogEntryRepository : IRepository<BlogEntry>
	{
		BlogEntry Get(string url);
		void Delete(string url);
	}
}
