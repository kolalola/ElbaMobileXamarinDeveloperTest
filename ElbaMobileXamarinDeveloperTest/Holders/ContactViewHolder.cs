using Android.Views;
using Android.Widget;
using static Android.Support.V7.Widget.RecyclerView;

namespace ElbaMobileXamarinDeveloperTest.Holders
{
    public class ContactViewHolder : ViewHolder
    {
        public TextView NameTextView { get; set; }

        public TextView PhoneTextView { get; set; }

        public TextView HeightTextView { get; set; }

        public ContactViewHolder(View itemView) : base(itemView)
        {
            NameTextView = itemView.FindViewById<TextView>(Resource.Id.name_textView);
            PhoneTextView = itemView.FindViewById<TextView>(Resource.Id.phone_textView);
            HeightTextView = itemView.FindViewById<TextView>(Resource.Id.height_textView);
        }

    }
}