using AutoMapper;
using Template500.Domain.Entities;
using Template500.ViewModels;

namespace Template500.App_Start
{
	public static class AppMaps
	{
		public static void Create()
		{
			// Mappings to Views
			Mapper.CreateMap<LogEntry, LogEntryViewModel>();
			Mapper.CreateMap<BlogEntry, BlogEntryViewModel>();

			// Mappings to Domains
			Mapper.CreateMap<LogEntryViewModel, LogEntry>();
			Mapper.CreateMap<BlogEntryViewModel, BlogEntry>();
		}
	}
}