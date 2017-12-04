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
    public class EditSetArrayAdapter : RecyclerView.Adapter
    {
        private List<Channel> _setsList;
        private Context mContext;
        public event EventHandler<int> ItemClick;

        public EditSetArrayAdapter(Context context, ChannelsSet channelsSet)
        {
            mContext = context;
            _setsList = channelsSet.Channels;
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


        //public override View GetView(int position, View convertView, ViewGroup parent)
        //{
        //    View view = convertView;
        //    if (view == null)
        //    {
        //        view = LayoutInflater.From(mContext).Inflate(Resource.Layout.EditSetListViewRow, null, false);
        //    }

        //    ImageView platformImg = (ImageView)view.FindViewById(Resource.Id.editSet_platformImg_ImageView);
        //    TextView setName = (TextView)view.FindViewById(Resource.Id.editSet_channelName_textView);

        //    platformImg.SetImageDrawable(mContext.Resources.GetDrawable(DATA.platformsImagesResourceIds[_setsList[position].Platform]));
        //    setName.Text = _setsList[position].Name;

        //    return view;
        //}

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            EditSetViewHolder bindHolder = (EditSetViewHolder)holder;
            bindHolder.platformImg.SetImageDrawable(mContext.Resources.GetDrawable(DATA.platformsImagesResourceIds[_setsList[position].Platform]));
            bindHolder.setName.Text = _setsList[position].Name;

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(mContext).Inflate(Resource.Layout.EditSetListViewRow, null);
            EditSetViewHolder vh = new EditSetViewHolder(view, OnClick);

            return vh;
        }

        public class EditSetViewHolder : RecyclerView.ViewHolder
        {
            public ImageView platformImg { get; set; }
            public TextView setName { get; set; }

            public EditSetViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                platformImg = (ImageView)itemView.FindViewById(Resource.Id.editSet_platformImg_ImageView);
                setName = (TextView)itemView.FindViewById(Resource.Id.editSet_channelName_textView);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }

        }
    }

    
}