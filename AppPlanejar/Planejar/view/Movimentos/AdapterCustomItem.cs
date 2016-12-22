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
using SharedPlanejar.Models;
using Android.Graphics.Drawables;

namespace com.dinizdesenvolve.planejar.view.Movimentos
{
    class AdapterCustomItem : BaseAdapter<Item>
    {
        private Context mContext;
        private List<Item> mList;


        public AdapterCustomItem(Context context, List<Item> list)
        {
            this.mContext = context;
            this.mList = list;
        }


        public override int Count
        {
            get
            {
                return mList.Count;
            }
        }

        public override Item this[int position]
        {
            get
            {
                return this.mList[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_dialog_item_list_view, null, false);
            }

            TextView titulo = row.FindViewById<TextView>(Resource.Id.row_list_dialog_titulo);
            TextView subtitulo = row.FindViewById<TextView>(Resource.Id.row_list_dialog_subtitulo);

            titulo.Text = mList[position].descricao;
            //subtitulo.Text = mList[position].getCategoria().descricao;

            return row;
        }



    }

}