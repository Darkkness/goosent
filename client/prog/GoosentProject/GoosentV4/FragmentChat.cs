using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Support.V4;

namespace Goosent
{
    public class FragmentChat : BaseTabFragment
    {

        public static FragmentChat getInstance(Context context)
        {
            Bundle args = new Bundle();
            FragmentChat cFragment = new FragmentChat();
            cFragment.Arguments = args;
            cFragment.SetContext(context);
            cFragment.SetTitle(context.GetString(Resource.String.tab_item_chat));

            return cFragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.FragmentChatLayout, container, false);
            return view;
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }

  
    }
}