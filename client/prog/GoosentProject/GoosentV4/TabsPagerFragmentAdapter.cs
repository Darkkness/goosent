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
using Android.Support.V4.App;
using Java.Lang;

namespace Goosent
{
    class TabsPagerFragmentAdapter : FragmentPagerAdapter
    {
        private List<string> tabs = new List<string>();

        public TabsPagerFragmentAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            tabs.Add("Edit set");
            tabs.Add("Chat");
            tabs.Add("Select set");
        }

        public override int Count
        {
            get { return tabs.Count; }
        }

        // Show tab labels

        //public override ICharSequence GetPageTitleFormatted(int position)
        //{
        //    ICharSequence convertedString = new Java.Lang.String(tabs[position]);
        //    return convertedString;
        //}

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return FragmentChat.getInstance();
                    break;

                case 1:
                    return FragmentSelectSet.getInstance();
                    break;

                case 2:
                    return FragmentEditSets.getInstance();
                    break;

            }

            return null;
        }
    }
}