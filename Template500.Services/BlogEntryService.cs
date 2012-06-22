using Template500.Domain.Entities;
using Template500.Repositories.Interfaces;
using Template500.Services.Interfaces;
using System.Collections.Generic;

namespace Template500.Services
{
	public class BlogEntryService : Service<BlogEntry, IBlogEntryRepository>, IBlogEntryService
	{
		public BlogEntryService(IBlogEntryRepository repository)
		{
			base.InjectRepository(repository);
		}		

		public BlogEntry Get(string url)
		{
			return _repository.Get(url);
		}

		public void Update(BlogEntry blogEntry)
		{
			BlogEntry currentBlogEntry = _repository.Get(blogEntry.Url);
			currentBlogEntry.Title = blogEntry.Title;
			currentBlogEntry.Intro = blogEntry.Intro;
			currentBlogEntry.Body = blogEntry.Body;
			_repository.Save(currentBlogEntry);
		}

		public void Delete(string url)
		{
			_repository.Delete(url);
		}
	}
}
