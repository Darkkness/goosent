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

namespace Goosent
{
    [Activity(Label = "Goosent", MainLauncher = true)]
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

        DBHandler dbHandler;

        //Temp
        List<string> chatsInSet = new List<string>();

        // Все что касается сетов
        private ChannelsSet _selectedSet;
        private ChannelsSetsList _setsList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            InitChannelsSets();
            InitToolbar();
            InitNavigationView();
            InitTabs();
            InitFAB();
            
            

            //dbHandler = new DBHandler(this);
            
        }

        private void InitChannelsSets()
        {
            _setsList = new ChannelsSetsList();
            InitTestingSets();
        }

        private void InitTestingSets()
        {
            var set1 = new ChannelsSet("Тестовый сет 1");
            set1.AddChannel(new Channel("stopgameru", Resources.GetStringArray(Resource.Array.platformList)[0]));
            set1.AddChannel(new Channel("arthas", Resources.GetStringArray(Resource.Array.platformList)[0]));
            set1.AddChannel(new Channel("riotgames", Resources.GetStringArray(Resource.Array.platformList)[0]));
            _setsList.AddSet(set1);
            var set2 = new ChannelsSet("Тестовый сет 2");
            set2.AddChannel(new Channel("esltv_cs", Resources.GetStringArray(Resource.Array.platformList)[0]));
            set2.AddChannel(new Channel("nightblue3", Resources.GetStringArray(Resource.Array.platformList)[0]));
            set2.AddChannel(new Channel("imaqtpie", Resources.GetStringArray(Resource.Array.platformList)[0]));
            _setsList.AddSet(set2);
            var set3 = new ChannelsSet("Тестовый сет 3");
            set3.AddChannel(new Channel("lirikk", Resources.GetStringArray(Resource.Array.platformList)[0]));
            set3.AddChannel(new Channel("lirik", Resources.GetStringArray(Resource.Array.platformList)[0]));
            set3.AddChannel(new Channel("meclipse", Resources.GetStringArray(Resource.Array.platformList)[0]));
            _setsList.AddSet(set3);
            _selectedSet = _setsList.SetsList[0];
        }

        private void InitFAB()
        {
            fab = (FloatingActionButton)FindViewById(Resource.Id.floatingActionButton);
            fab.Enabled = false;
            fab.Alpha = 0.0f;

            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Fab was pressed", ToastLength.Short).Show();
        }

        private void InitToolbar()
        {
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.app_name);
            toolbar.InflateMenu(Resource.Menu.menu);
            InitSpinner();
        }

        private void InitSpinner()
        {
            spinner = (Spinner)FindViewById(Resource.Id.spinner);

            ChatsSpinnerArrayAdapter spinnerAdapter = new ChatsSpinnerArrayAdapter(this, _selectedSet.Channels);
            spinner.Adapter = spinnerAdapter;
            
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
            tabLayout.GetTabAt(1).SetIcon(Resource.Drawable.ic_tab_select_set);
            tabLayout.GetTabAt(2).SetIcon(Resource.Drawable.ic_tab_edit_set);
        }

        // Выполняется каждый раз, когда меняется страница в ViewPager
        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            // Показать/скрыть спиннер при смене страницы
            switch (e.Position)
            {
                case 0:
                    spinner.Alpha = 1.0f;
                    fab.Alpha = 0.0f;
                    fab.Enabled = false;
                    spinner.Enabled = true;
                    break;

                case 2:

                    break;


                default:
                    spinner.Alpha = 0.0f;
                    fab.Alpha = 1.0f;
                    fab.Enabled = true;
                    spinner.Enabled = false;
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

        public ChannelsSet SelectedSet
        {
            get { return _selectedSet; }
        }

        public ChannelsSetsList SetsList
        {
            get { return _setsList; }
        }

        public void SetSelectedSet(ChannelsSet set)
        {
            _selectedSet = set;
        }
    }
}

