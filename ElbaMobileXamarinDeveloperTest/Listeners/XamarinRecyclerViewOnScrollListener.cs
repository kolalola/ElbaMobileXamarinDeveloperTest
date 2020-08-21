using System;
using Android.Support.V7.Widget;

namespace ElbaMobileXamarinDeveloperTest.Listeners
{
    /// <summary>
    /// Позволяет вызывать метод, необходимый для подгрузки про скроле RecyclerView
    /// </summary>
    public class XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);

        public event LoadMoreEventHandler LoadMoreEvent;

        private readonly LinearLayoutManager _layoutManager;

        public XamarinRecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            _layoutManager = layoutManager;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var totalItemCount = recyclerView.GetAdapter().ItemCount;
            var pastVisiblesItems = _layoutManager.FindFirstVisibleItemPosition();

            if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            {
                LoadMoreEvent?.Invoke(this, null);
            }
        }
    }
}