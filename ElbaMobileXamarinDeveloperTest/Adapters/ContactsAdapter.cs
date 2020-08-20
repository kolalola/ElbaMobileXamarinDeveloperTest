using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.ViewModels;
using ElbaMobileXamarinDeveloperTest.Holders;

namespace ElbaMobileXamarinDeveloperTest.Adapters
{
    public class ContactsAdapter : RecyclerView.Adapter
    {
        private readonly MainViewModel _viewModel;

        public ContactsAdapter(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.contact_item, parent, false);

            var vh = new ContactViewHolder(itemView);

            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            if (!(viewHolder is ContactViewHolder currentHolder)) return;

            var contact = _viewModel.Contacts[position];

            currentHolder.HeightTextView.Text = contact.Height;
            currentHolder.NameTextView.Text = contact.Name;
            currentHolder.PhoneTextView.Text = contact.Phone;

            //currentHolder.ItemView.Click += null;
            //currentHolder.ItemView.Click += (sender, args) =>
            //{
            //    var intent = new Intent(_context, typeof(ArticleActivity));
            //    intent.PutExtra(IntentParameters.ArticleId, article.Id.ToString());
            //    _context.StartActivity(intent);
            //};
        }

        public override int ItemCount => _viewModel.Contacts.Count;
        
    }
}