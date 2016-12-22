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
using SharedPlanejar.Models;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using com.dinizdesenvolve.planejar;

namespace com.dinizdesenvolve.planejar.view.Categorias
{
    public class AdapterListaCat : BaseAdapter<Categoria>
    {
        List<Categoria> mList;
        Context mContext;

        public AdapterListaCat(Context context, List<Categoria> list)
        {
            mList = list;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                return mList.Count;
            }
        }

        public override Categoria this[int position]
        {
            get
            {
                return this.mList[position];
            }
        }

     

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_categoria_list_view , null, false);
            }

            TextView img = row.FindViewById<TextView>(Resource.Id.rowListCatImg);
            TextView textVdesc = row.FindViewById<TextView>(Resource.Id.rowListCatTitulo);
            TextView textVtipo = row.FindViewById<TextView>(Resource.Id.rowListCatSubtitulo);

            int cor = mList[position].cor;
            string descricao = mList[position].descricao;
            string tipo = mList[position].getTipo(1);

            img.SetBackgroundColor(new Color(cor));

            //changeColorCategoria(cor, img);
            textVdesc.Text = descricao;
            textVtipo.Text = tipo;

            return row;
        }


        public override long GetItemId(int position)
        {
            return position;
        }


        private void changeColorCategoria(int selectedColor, TextView img)
        {

            ShapeDrawable mDrawable;
            int x = 10;
            int y = 10;
            int width = 300;
            int height = 50;

            mDrawable = new ShapeDrawable(new OvalShape());
            mDrawable.Paint.Color = new Color(selectedColor);
            mDrawable.SetBounds(x, y, x + width, y + height);

            ShapeDrawable shape = mDrawable;

            img.Background = shape;
            img.Invalidate();

        }
    }
}
