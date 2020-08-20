﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.Services.Contacts;
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
			builder.RegisterType<MainViewModel>().InstancePerLifetimeScope();

			App.Container = builder.Build();

			base.OnCreate();
		}
	}
}