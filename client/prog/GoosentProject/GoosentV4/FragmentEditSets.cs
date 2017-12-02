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
    public class FragmentEditSets : BaseTabFragment
    {

        public static FragmentEditSets getInstance(Context context)
        {
            Bundle args = new Bundle();
            FragmentEditSets cFragment = new FragmentEditSets();
            cFragment.Arguments = args;
            cFragment.SetContext(context);
            cFragment.SetTitle(context.GetString(Resource.String.tab_item_edit_set));

            return cFragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.FragmentEditSetLayout, container, false);
            return view;
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }

    }
}