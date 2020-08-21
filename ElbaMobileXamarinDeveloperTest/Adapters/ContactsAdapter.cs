﻿using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using ElbaMobileXamarinDeveloperTest.Core.ViewModels;
using ElbaMobileXamarinDeveloperTest.Holders;

namespace ElbaMobileXamarinDeveloperTest.Adapters
{
    public class ContactsAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private readonly MainViewModel _viewModel;

        public ContactsAdapter(Context context, MainViewModel viewModel)
        {
            _context = context;
            _viewModel = viewModel;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) =>
            new ContactViewHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.contact_item, parent, false));

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            if (!(viewHolder is ContactViewHolder currentHolder)) return;

            var contact = _viewModel.Contacts[position];

            currentHolder.HeightTextView.Text = contact.Height;
            currentHolder.NameTextView.Text = contact.Name;
            currentHolder.PhoneTextView.Text = contact.Phone;

            currentHolder.ItemView.Click += null;
            currentHolder.ItemView.Click += (sender, args) =>
            {
                var intent = new Intent(_context, typeof(ContactActivity));
                intent.PutExtra(_context.Resources.GetString(Resource.String.contact_id_intent), contact.Id.ToString());
                _context.StartActivity(intent);
            };
        }

        public override int ItemCount => _viewModel.Contacts.Count;
    }
}