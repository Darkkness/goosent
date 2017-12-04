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
    class SelectSetArrayAdapter : BaseAdapter
    {
        private List<ChannelsSet> _setsList;
        ChannelsSet _currentSet;
        private Context mContext;

        public SelectSetArrayAdapter(Context context, ChannelsSet currentSet, ChannelsSetsList channelsSet)
        {
            mContext = context;
            _currentSet = currentSet;
            _setsList = channelsSet.SetsList;
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
                view = LayoutInflater.From(mContext).Inflate(Resource.Layout.SelectSetListViewRow, null, false);
            }

            TextView setName = (TextView)view.FindViewById(Resource.Id.selectSet_setName_textView);

            setName.Text = _setsList[position].Name;

            return view;
        }
    }
}