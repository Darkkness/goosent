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

namespace Goosent
{
    public class FragmentEditSets : Android.Support.V4.App.Fragment
    {

        private View view;

        public static FragmentChat getInstance()
        {
            Bundle args = new Bundle();
            FragmentChat cFragment = new FragmentChat();
            cFragment.Arguments = args;

            return cFragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.FragmentEditSetLayout, container, false);
            return view;
        }
    }
}