using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Java.Lang;
using Android.Views;
using com.dinizdesenvolve.planejar.View;

namespace com.dinizdesenvolve.planejar.Views.Main.Menu
{
    public class AdapterMenu : BaseAdapter<string>
    {
        private List<string> menuList;
        private Context mContext;
        private MainActivity mainActivity;
        private AdapterMenu mLeftAdapter;

        public AdapterMenu(Context context, List<string> lista) {

            this.mContext = context;
            this.menuList = lista;

        }

        public AdapterMenu(MainActivity mainActivity, AdapterMenu mLeftAdapter)
        {
            this.mainActivity = mainActivity;
            this.mLeftAdapter = mLeftAdapter;
        }

        public override string this[int position]
        {
            get
            {
               return this.menuList[position];
            }
        }

        public override int Count
        {
            get{ return menuList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View row = convertView;

            if (row == null) {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_menu_list_view,null,false);
            }

            TextView itemMenu = row.FindViewById<TextView>(Resource.Id.menuItem);
            itemMenu.Text = this.menuList[position];

            return row;

        }
    }
}