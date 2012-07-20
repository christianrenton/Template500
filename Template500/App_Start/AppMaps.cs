using System;
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
			Mapper.CreateMap<SiteSettings, SiteSettingsViewModel>();
			Mapper.CreateMap<BlogEntry, BlogEntryViewModel>();

			// Mappings to Domains
			Mapper.CreateMap<LogEntryViewModel, LogEntry>();
			Mapper.CreateMap<SiteSettingsViewModel, SiteSettings>().ForMember(x => x.Id, x => x.UseValue(1));
			Mapper.CreateMap<BlogEntryViewModel, BlogEntry>().ForMember(x => x.Date, x => x.UseValue(DateTime.Now));
		}
	}
}