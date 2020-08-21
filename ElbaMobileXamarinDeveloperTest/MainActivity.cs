using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using ElbaMobileXamarinDeveloperTest.Adapters;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.Services.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.ViewModels;
using ElbaMobileXamarinDeveloperTest.Listeners;
using Xamarin.Essentials;

namespace ElbaMobileXamarinDeveloperTest
{
    [Activity(Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView _recyclerView;
        private MainViewModel _viewModel;
        private ContactsAdapter _adapter;
        private SwipeRefreshLayout _refresher;
        private EditText _searchEditText;
        private ProgressBar _progressBar;
        private LinearLayout _contentLinearLayout;

        public bool IsSearching => !string.IsNullOrEmpty(_searchEditText.Text);

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            AskPermissions();

            SetContentView(Resource.Layout.activity_main);

            _progressBar = FindViewById<ProgressBar>(Resource.Id.main_progressBar);
            _contentLinearLayout = FindViewById<LinearLayout>(Resource.Id.content_linearLayout);

            StartProgressBar();

            ConfigureRecyclerView();

            ConfigureRefresher();

            ConfigureSearch();

            _viewModel = (await App.Container.Resolve<MainViewModel>()
                .UpdateOrNothing((message) => Toast.MakeText(this, message, ToastLength.Long).Show()))
                .ReloadContacts();

            ResetAdapter();

            FinishProgressBar();
        }

        private void ConfigureSearch()
        {
            _searchEditText = FindViewById<EditText>(Resource.Id.search_editText);
            _searchEditText.TextChanged += (sender, args) =>
            {
                _viewModel.StartNewSearch(string.Concat(args.Text));
                ResetAdapter();
            };
        }

        private void ConfigureRefresher()
        {
            _refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            _refresher.Refresh += (sender, args) => 
            {
                if (IsSearching)
                    _viewModel.StartNewSearch(_searchEditText.Text);
                else
                    _viewModel.RefreshContacts();

                ResetAdapter();
                _refresher.Refreshing = false;
            };
        }

        private void ConfigureRecyclerView()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.contacts_recyclerView);

            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            _recyclerView.SetLayoutManager(layoutManager);

            _recyclerView.VerticalScrollBarEnabled = false;

            var dividerItemDecoration = new DividerItemDecoration(_recyclerView.Context, layoutManager.Orientation);
            _recyclerView.AddItemDecoration(dividerItemDecoration);

            var onScrollListener = new XamarinRecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += LoadMore;
            _recyclerView.AddOnScrollListener(onScrollListener);
        }

        private void LoadMore(object sender, EventArgs e)
        {
            if (!IsSearching)
                _viewModel.LoadMoreContacts();
            else
                _viewModel.SearchMore(_searchEditText.Text);

            _adapter.NotifyDataSetChanged();
        }

        private void ResetAdapter()
        {
            _adapter = new ContactsAdapter(this, _viewModel);
            _recyclerView.SetAdapter(_adapter);
        }

        private void StartProgressBar()
        {
            _contentLinearLayout.Visibility = ViewStates.Gone;
            _progressBar.Visibility = ViewStates.Visible;
        }

        private void FinishProgressBar()
        {
            _contentLinearLayout.Visibility = ViewStates.Visible;
            _progressBar.Visibility = ViewStates.Gone;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void AskPermissions()
        {
            var localPermission = new List<string>();
            localPermission.Add(Manifest.Permission.ReadExternalStorage);
            localPermission.Add(Manifest.Permission.WriteExternalStorage);
            localPermission.Add(Manifest.Permission.Internet);
            localPermission.Add(Manifest.Permission.CallPhone);

            var denyied = localPermission.Where(w => ContextCompat.CheckSelfPermission(this, w) != Permission.Granted).ToArray();

            if (denyied.Length != 0)
                ActivityCompat.RequestPermissions(this, denyied, 9332);
        }
    }
}
