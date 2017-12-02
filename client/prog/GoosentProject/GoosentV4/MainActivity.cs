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
        
        ActionBarDrawerToggle mDrawerToggle;
        Android.Support.V7.App.ActionBar actionBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitToolbar();
            InitNavigationView();
            InitTabs();
        }

        private void InitToolbar()
        {
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.app_name);
            toolbar.InflateMenu(Resource.Menu.menu);
            
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
            } else
            {
                Console.WriteLine("Not keeping screen awake");
                Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
        }

        private void InitTabs()
    {
        viewPager = (ViewPager)FindViewById(Resource.Id.view_pager);
        TabsPagerFragmentAdapter adapter = new TabsPagerFragmentAdapter(SupportFragmentManager);
        viewPager.Adapter = adapter;

        TabLayout tabLayout = (TabLayout)FindViewById(Resource.Id.tab_layout);
        tabLayout.SetupWithViewPager(viewPager);

        tabLayout.GetTabAt(1).SetIcon(Resource.Drawable.ic_tab_chat);
        tabLayout.GetTabAt(0).SetIcon(Resource.Drawable.ic_tab_select_set);
        tabLayout.GetTabAt(2).SetIcon(Resource.Drawable.ic_tab_edit_set);
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
}
}

