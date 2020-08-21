using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Autofac;
using ElbaMobileXamarinDeveloperTest.Core.ViewModels;

namespace ElbaMobileXamarinDeveloperTest
{
    [Activity(Label = "ContactActivity")]
    public class ContactActivity : Activity
    {
        private FullContactViewModel _viewModel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_contact);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_material);
            toolbar.NavigationClick += (sender, args) => Finish();

            int.TryParse(Intent.GetStringExtra(Resources.GetString(Resource.String.contact_id_intent)), out int contactId);

            _viewModel = App.Container.Resolve<FullContactViewModel>()
                .Load(contactId);

            if (_viewModel.IsLoaded)
            {
                FindViewById<TextView>(Resource.Id.contact_name_textView).Text = _viewModel.Name;
                var phoneTextView = FindViewById<TextView>(Resource.Id.contact_phone_textView);
                phoneTextView.Text = _viewModel.Phone;
                phoneTextView.Click += (sender, args) =>
                {
                    var intent = new Intent(Intent.ActionCall, Android.Net.Uri.Parse($"tel:{Uri.EscapeDataString(_viewModel.Phone)}"));
                    StartActivity(intent);
                };

                FindViewById<TextView>(Resource.Id.contact_biography_texView).Text = _viewModel.Biography;
                FindViewById<TextView>(Resource.Id.contact_temperament_texView).Text = _viewModel.Temperament;
                FindViewById<TextView>(Resource.Id.contact_ed_period_texView).Text = _viewModel.EducationPeriod;
            }
        }
    }
}