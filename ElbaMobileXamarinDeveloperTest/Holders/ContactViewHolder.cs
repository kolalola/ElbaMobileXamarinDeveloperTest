using Android.Content;
using Android.Views;
using Android.Widget;
using static Android.Support.V7.Widget.RecyclerView;
using static Android.Views.View;

namespace ElbaMobileXamarinDeveloperTest.Holders
{
    public class ContactViewHolder : ViewHolder, IOnClickListener
    {
        private readonly Context _context;
        public TextView NameTextView { get; set; }

        public TextView PhoneTextView { get; set; }

        public TextView HeightTextView { get; set; }

        public TextView IdTextView { get; set; }

        public ContactViewHolder(View itemView, Context context) : base(itemView)
        {
            _context = context;
            NameTextView = itemView.FindViewById<TextView>(Resource.Id.name_textView);
            PhoneTextView = itemView.FindViewById<TextView>(Resource.Id.phone_textView);
            HeightTextView = itemView.FindViewById<TextView>(Resource.Id.height_textView);
            IdTextView = itemView.FindViewById<TextView>(Resource.Id.id_textView);

            itemView.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            var id = v.FindViewById<TextView>(Resource.Id.id_textView).Text;
            var intent = new Intent(_context, typeof(ContactActivity));
                intent.PutExtra(_context.Resources.GetString(Resource.String.contact_id_intent), id);
                _context.StartActivity(intent);
        }
    }
}