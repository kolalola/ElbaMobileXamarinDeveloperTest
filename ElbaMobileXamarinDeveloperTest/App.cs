using System;

using Android.App;
using Android.Runtime;
using Autofac;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory;
using ElbaMobileXamarinDeveloperTest.Core.Services.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.Services.Phone;
using ElbaMobileXamarinDeveloperTest.Core.Services.Rest;
using ElbaMobileXamarinDeveloperTest.Core.ViewModels;

namespace ElbaMobileXamarinDeveloperTest
{
    [Application(Label = "@string/app_name")]
	public class App : Application
	{
		public static IContainer Container { get; set; }

		public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
		{
		}

		public override void OnCreate()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<RestService>().As<IRestService>().InstancePerLifetimeScope();
			builder.RegisterType<ContactsLoaderService>().As<IContactsLoaderService>().InstancePerLifetimeScope();
			builder.RegisterType<ContactsRepository>().As<IContactsRepository>().InstancePerLifetimeScope();
			builder.RegisterType<DownloadsHistoryRepository>().As<IDownloadsHistoryRepository>().InstancePerLifetimeScope();
			builder.RegisterType<PhoneService>().As<IPhoneService>().SingleInstance();
			builder.RegisterType<MainViewModel>().InstancePerLifetimeScope();
			builder.RegisterType<FullContactViewModel>().InstancePerLifetimeScope();

			App.Container = builder.Build();

			base.OnCreate();
		}
	}
}