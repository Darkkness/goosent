using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using Android.Support.V7.AppCompat;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Graphics.Drawables;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Goosent.Adapters;
using Android.Views.Animations;
using Android.Content.Res;
using Android.Graphics;
using System.IO;

namespace Goosent
{
    [Activity(Label = "Goosent", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        private ViewPager viewPager;
        private Android.Support.V7.Widget.Toolbar toolbar;
        private Android.Support.V4.Widget.DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private CompoundButton switchActionView;
        private FloatingActionButton fab;
        private TabLayout tabLayout;
        private TabsPagerFragmentAdapter fragmentPagerAdapter;

        ActionBarDrawerToggle mDrawerToggle;
        Android.Support.V7.App.ActionBar actionBar;
        Spinner spinner;
        ChatsSpinnerArrayAdapter newSpinnerAdapter;

        DBHandler dbHandler;

        //Temp
        List<string> chatsInSet = new List<string>();

        // Все что касается сетов
        private int SELECTED_SET_INDEX;
        private ChannelsSetsList SETS_LIST;
        private Channel SELECTED_CHANNEL;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            DeleteDatabase("goosentDB");
            dbHandler = new DBHandler(this);
            

            SetContentView(Resource.Layout.Main);
            InitChannelsSets();
            InitToolbar();
            InitNavigationView();
            InitTabs();
            InitFAB();

            SaveSetsAndChannelsToDB();
            dbHandler.GetTableAsString("channels_table");
            dbHandler.GetTableAsString("sets_table");
            dbHandler.GetChannel(0);

            //dbHandler = new DBHandler(this);

        }

        private void InitChannelsSets()
        {
            SETS_LIST = new ChannelsSetsList();
            InitTestingSets();
        }

        private void InitTestingSets()
        {
            var set1 = new ChannelsSet("Тестовый сет 1");
            set1.AddChannel(new Channel("stopgameru", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("arthas", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("riotgames", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("hrthrthtr", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("hrthrthrth", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("jytuykjduy", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("liulkyj", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("bvcbcvb", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("ytyiuyuyo", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("bvxdg", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("xcvv", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("bfvbc", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("jyturt", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set1.AddChannel(new Channel("vghrt", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            SETS_LIST.AddSet(set1);
            var set2 = new ChannelsSet("Тестовый сет 2");
            set2.AddChannel(new Channel("esltv_cs", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set2.AddChannel(new Channel("nightblue3", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set2.AddChannel(new Channel("imaqtpie", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            SETS_LIST.AddSet(set2);
            var set3 = new ChannelsSet("Тестовый сет 3");
            set3.AddChannel(new Channel("lirikk", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set3.AddChannel(new Channel("lirik", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            set3.AddChannel(new Channel("meclipse", Resources.GetStringArray(Resource.Array.avalible_steaming_platforms)[0]));
            SETS_LIST.AddSet(set3);
            SELECTED_SET_INDEX = 0;
        }

        private void InitFAB()
        {
            fab = (FloatingActionButton)FindViewById(Resource.Id.floatingActionButton);
            fab.Hide();

            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Fragments.AddSetDialogFragment addSetFragmentDialogFragment = Fragments.AddSetDialogFragment.GetInstance();
            addSetFragmentDialogFragment.Show(FragmentManager, "add set dialog");
        }

        private void InitToolbar()
        {
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            toolbar.SetTitle(Resource.String.app_name);
            toolbar.InflateMenu(Resource.Menu.menu);
            InitActionBarSpinner();
        }

        private void SaveSetsAndChannelsToDB()
        {
            foreach (ChannelsSet set in SETS_LIST.SetsList)
            {
                dbHandler.AddSet(set);
            }
        }

        private void InitActionBarSpinner()
        {
            spinner = (Spinner)FindViewById(Resource.Id.spinner);

            newSpinnerAdapter = new ChatsSpinnerArrayAdapter(this, SetsList.SetsList[SELECTED_SET_INDEX].Channels);
            spinner.Adapter = newSpinnerAdapter;
            spinner.ItemSelected += Spinner_ItemSelected;
            
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SetSelectedChannel(SelectedSet.Channels[e.Position]);
        }

        public void SetSelectedChannel(Channel channel)
        {
            SELECTED_CHANNEL = channel;
            Toast.MakeText(this, "Selected channel: " + SELECTED_CHANNEL.Name, ToastLength.Short).Show();
        }

        public void UpdateSpinnerContent()
        {
            newSpinnerAdapter = new ChatsSpinnerArrayAdapter(this, SetsList.SetsList[SELECTED_SET_INDEX].Channels);
            if (spinner.Adapter != newSpinnerAdapter)
            {
                spinner.Adapter = newSpinnerAdapter;
            }

            newSpinnerAdapter.NotifyDataSetChanged();
        }

        private void InitNavigationView()
        {
            drawerLayout = (Android.Support.V4.Widget.DrawerLayout)FindViewById(Resource.Id.drawer_layout);
            navigationView = (NavigationView)FindViewById(Resource.Id.navigation_view);
            IMenuItem switchItem = navigationView.Menu.FindItem(Resource.Id.nav_menu_item_keep_awake);
            switchActionView = (CompoundButton)MenuItemCompat.GetActionView(switchItem);
            switchActionView.CheckedChange += SwitchView_CheckedChange;
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_menu_item_keep_awake:
                    switchActionView.Checked = !switchActionView.Checked;
                    break;
                default:
                    break;
            }
        }

        private void SwitchView_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                // Keep screen awake
                Console.WriteLine("Keeping screen awake");
                Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            }
            else
            {
                Console.WriteLine("Not keeping screen awake");
                Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
        }

        private void InitTabs()
        {
            viewPager = (ViewPager)FindViewById(Resource.Id.view_pager);
            fragmentPagerAdapter = new TabsPagerFragmentAdapter(SupportFragmentManager, ApplicationContext);
            viewPager.Adapter = fragmentPagerAdapter;
            viewPager.PageSelected += ViewPager_PageSelected;
            tabLayout = (TabLayout)FindViewById(Resource.Id.tab_layout);
            tabLayout.SetupWithViewPager(viewPager);

            tabLayout.GetTabAt(0).SetIcon(Resource.Drawable.ic_tab_chat);
            tabLayout.GetTabAt(1).SetIcon(Resource.Drawable.ic_tab_edit_set);
        }

        // Выполняется каждый раз, когда меняется страница в ViewPager
        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            // Показать/скрыть спиннер при смене страницы
            switch (e.Position)
            {
                case 0:
                    spinner.Visibility = ViewStates.Visible;
                    SupportActionBar.SetTitle(Resource.String.tab_item_chat_actionbar);
                    fab.Hide();
                    break;

                case 1:
                    spinner.Visibility = ViewStates.Gone;
                    SupportActionBar.SetTitle(Resource.String.tab_item_select_set);
                    fab.Show();
                    break;

                case 2:
                    spinner.Visibility = ViewStates.Gone;
                    SupportActionBar.SetTitle(Resource.String.tab_item_edit_set);
                    fab.Show();
                    break;

                default:
                    spinner.Visibility = ViewStates.Gone;
                    break;
            }
        }

        void AddTab(TabLayout tabLayout, string label, Drawable icon, bool iconOnly = false)
        {
            var newTab = tabLayout.NewTab();
            if (!iconOnly)
            {
                newTab.SetText(label);
            }
            newTab.SetIcon(icon);
            tabLayout.AddTab(newTab);
        }


        public int SelectedSetIndex
        {
            get { return SELECTED_SET_INDEX; }
            set { SELECTED_SET_INDEX = value; UpdateSpinnerContent(); }
        }

        public ChannelsSet SelectedSet
        {
            get { return SetsList.SetsList[SELECTED_SET_INDEX]; }
        }

        public ChannelsSetsList SetsList
        {
            get { return SETS_LIST; }
        }

        public void AddSet(ChannelsSet set)
        {
            SetsList.AddSet(set);
        }

        public void AddChannel(int setIndex, Channel channel)
        {
            SetsList.SetsList[setIndex].AddChannel(channel);
            ((FragmentEditSets)fragmentPagerAdapter.tabs[1]).UpdateEditSetListView();
        }

        public bool IsSetExist(string name)
        {
            foreach (ChannelsSet set in SetsList.SetsList)
            {
                if (set.Name == name)
                {

                    return true;
                }
            }

            return false;
        }


        void SetupDB()
        {
            string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "goosentDB.db");
        }
    }
}

