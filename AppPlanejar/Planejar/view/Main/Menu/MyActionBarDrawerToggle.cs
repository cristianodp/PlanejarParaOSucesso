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
using Android.Support.V7.App;
using Android.Support.V4.Widget;

using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;

namespace com.dinizdesenvolve.planejar.Views.Main
{
    public class MyActionBarDrawerToggle : SupportActionBarDrawerToggle
    {
        private ActionBarActivity mHostActivity;
        private int mOpendedResource;
        private int mClosedResource;
        public MyActionBarDrawerToggle(ActionBarActivity host, DrawerLayout drawerLayout, int opendedResource, int closedResource ): base (host, drawerLayout,opendedResource,closedResource)
        {
            mHostActivity = host;
            mOpendedResource = opendedResource;
            mClosedResource = closedResource;
        }

        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            base.OnDrawerOpened(drawerView);
        }

        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}