using Template500.Domain.Entities;
using System.Collections.Generic;

namespace Template500.Services.Interfaces
{
	public interface IBlogEntryService : IService<BlogEntry>
	{
		BlogEntry Get(string url);
		void Update(BlogEntry blogEntry);
		void Delete(string url);
	}
}
