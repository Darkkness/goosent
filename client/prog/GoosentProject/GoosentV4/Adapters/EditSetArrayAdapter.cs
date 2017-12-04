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

namespace Goosent.Adapters
{
    class EditSetArrayAdapter : BaseAdapter
    {
        private List<Channel> _setsList;
        private Context mContext;

        public EditSetArrayAdapter(Context context, ChannelsSet channelsSet)
        {
            mContext = context;
            _setsList = channelsSet.Channels;
        }

        public override int Count
        {
            get { return _setsList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(mContext).Inflate(Resource.Layout.EditSetListViewRow, null, false);
            }

            ImageView platformImg = (ImageView)view.FindViewById(Resource.Id.editSet_platformImg_ImageView);
            TextView setName = (TextView)view.FindViewById(Resource.Id.editSet_channelName_textView);

            platformImg.SetImageDrawable(mContext.Resources.GetDrawable(DATA.platformsImagesResourceIds[_setsList[position].Platform]));
            setName.Text = _setsList[position].Name;

            return view;
        }
    }
}