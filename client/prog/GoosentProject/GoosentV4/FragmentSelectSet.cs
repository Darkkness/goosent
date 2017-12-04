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
    public class FragmentSelectSet : BaseTabFragment
    {
        ListView selectSetListView;
        Adapters.SelectSetArrayAdapter selectSetAdapter;
        ChannelsSet _currentSet;
        ChannelsSetsList _setsList;

        public static FragmentSelectSet getInstance(Context context)
        {
            Bundle args = new Bundle();
            FragmentSelectSet cFragment = new FragmentSelectSet();
            cFragment.Arguments = args;
            cFragment.SetContext(context);
            cFragment.SetTitle(context.GetString(Resource.String.tab_item_select_set));

            return cFragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.FragmentSelectSetLayout, container, false);
            _currentSet = ((MainActivity)Activity).SelectedSet;
            _setsList = ((MainActivity)Activity).SetsList;
            selectSetListView = (ListView)view.FindViewById(Resource.Id.selectSet_listView);
            selectSetAdapter = new Adapters.SelectSetArrayAdapter(context, _currentSet, _setsList);
            selectSetListView.Adapter = selectSetAdapter;
            selectSetListView.ChoiceMode = ChoiceMode.Single;
            selectSetListView.ItemClick += SelectSetListView_ItemClick;
            StartConstantSelectSetViewUpdating();


            return view;
        }

        private void SelectSetListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            e.View.Selected = true;
            ((MainActivity)Activity).SetSelectedSet(_setsList.SetsList[selectSetListView.CheckedItemPosition]);
            Console.WriteLine(((MainActivity)Activity).SelectedSet.Name);
        }

        private async Task StartConstantSelectSetViewUpdating()
        {
            while (true)
            {
                Activity.RunOnUiThread(() =>
                {
                    UpdateSelectSetListView();
                });

                
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            }
        }

        void UpdateSelectSetListView()
        {
            selectSetAdapter.NotifyDataSetChanged();
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }

    }
}