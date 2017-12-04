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

using System.Threading.Tasks;

namespace Goosent
{
    public class FragmentEditSets : BaseTabFragment
    {
        ListView editSetListView;
        private Adapters.EditSetArrayAdapter editSetAdapter;
        public ChannelsSet _currentSet;

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

            _currentSet = ((MainActivity)Activity).SelectedSet;
            editSetListView = (ListView)view.FindViewById(Resource.Id.editSet_listView);
            editSetAdapter = new Adapters.EditSetArrayAdapter(context, _currentSet);
            editSetListView.Adapter = editSetAdapter;
            StartConstantEditSetViewUpdating();

            return view;

        }

        private async Task StartConstantEditSetViewUpdating()
        {
            while (true)
            {
                Activity.RunOnUiThread(() =>
                {
                    UpdateEditSetListView();
                });


                await Task.Delay(TimeSpan.FromSeconds(0.3));
            }
        }

        void UpdateEditSetListView()
        {
            Console.WriteLine("From EditFragment: " + _currentSet.Name);
            _currentSet = ((MainActivity)Activity).SelectedSet;
            editSetAdapter.NotifyDataSetChanged();
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }

    }
}