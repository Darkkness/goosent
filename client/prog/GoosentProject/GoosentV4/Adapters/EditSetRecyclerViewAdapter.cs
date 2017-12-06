using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Support.V7.Widget;

namespace Goosent.Adapters
{
    public class EditSetRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<ChannelsSet> _setsList;
        

        private Context mContext;
        public event EventHandler<int> ItemClick;
        

        public EditSetRecyclerViewAdapter(Context context, ChannelsSetsList channelsSet)
        {
            mContext = context;
            _setsList = channelsSet.SetsList;
        }


        public override int ItemCount
        {
            get { return _setsList.Count; }
        }

        void OnClick(int position)
        {
            if (ItemClick !=null)
            {
                ItemClick(this, position);
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            EditSetViewHolder viewHolder = (EditSetViewHolder)holder;
            viewHolder.setNameTextView.Text = _setsList[position].Name;
            if (((MainActivity)mContext).SelectedSetIndex == position)
            {
                // Выделить карточку
                holder.ItemView.SetBackgroundDrawable(mContext.Resources.GetDrawable(Resource.Drawable.selected_card_background));
            }
            else
            {
                holder.ItemView.SetBackgroundColor(Android.Graphics.Color.White);
            }

            viewHolder.channelsViewGroup.RemoveAllViews();
            foreach (Channel channel in _setsList[position])
            {
                View channelView = LayoutInflater.From(mContext).Inflate(Resource.Layout.SetCardView_ChannelView, null);
                ((TextView)channelView.FindViewById(Resource.Id.setCardView_channelViewName_TextView)).Text = channel.Name;
                viewHolder.channelsViewGroup.AddView(channelView);
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(mContext).Inflate(Resource.Layout.SetCardView, parent, false);
            

            EditSetViewHolder vh = new EditSetViewHolder(view, OnClick);

            return vh;
        }

        public class EditSetViewHolder : RecyclerView.ViewHolder
        {
            public TextView setNameTextView { get; set; }
            public ViewGroup channelsViewGroup { get; set; }

            public EditSetViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                setNameTextView = (TextView)itemView.FindViewById(Resource.Id.setName_cardview_textView);
                channelsViewGroup = (ViewGroup)itemView.FindViewById(Resource.Id.setChannels_container_GridLayout);
                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }
    }

    
}