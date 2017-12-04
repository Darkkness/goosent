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
using Android.Support.V7.Widget;

namespace Goosent
{
    public class FragmentEditSets : BaseTabFragment
    {
        RecyclerView editSetRecyclerView;
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
            editSetRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.editSet_recyclerView);
            editSetAdapter = new Adapters.EditSetArrayAdapter(context, _currentSet);
            editSetRecyclerView.SetLayoutManager(new LinearLayoutManager(context));
            editSetAdapter.ItemClick += EditSetAdapter_ItemClick;
            editSetRecyclerView.SetAdapter(editSetAdapter);


            return view;

        }

        private void EditSetAdapter_ItemClick(object sender, int e)
        {
            Toast.MakeText(context, "Item clicked" + e.ToString(), ToastLength.Short).Show();
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