using System.Collections.Generic;
using Android.Content;
using SharedPlanejar.Models;
using Android.Widget;
using Android.Views;
using System;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;

namespace com.dinizdesenvolve.planejar.view.Categorias.Metas
{
    public class AdapterListaMetCat : BaseAdapter<Meta>
    {
        private Context mContext;
        private List<Meta> mList;


        public AdapterListaMetCat(Context context, List<Meta> list)
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

        public override Meta this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_meta_cat_list_view, null, false);
            }

            TextView tximg = row.FindViewById<TextView>(Resource.Id.rowListMetCatImg);
            TextView txini = row.FindViewById<TextView>(Resource.Id.rowListMetCatInicio);
            TextView txfim = row.FindViewById<TextView>(Resource.Id.rowListMetCatFim);
            TextView txvlr = row.FindViewById<TextView>(Resource.Id.rowListMetCatValor);
            TextView txrea = row.FindViewById<TextView>(Resource.Id.rowListMetCatRealizado);

            if (mList[position].ativo == 1){
                tximg.SetBackgroundColor(new Color(Resource.Color.colorAtivo));
            }
            else{
                tximg.SetBackgroundColor(new Color(Resource.Color.colorInativo));
            }
            txini.Text = mList[position].getDtInicio();
            txfim.Text = mList[position].getDtFinal();
            txvlr.Text = mList[position].valor.ToString("#,###,###,##0.00");
            txrea.Text = mList[position].getRealizado().ToString("#,###,###,##0.00");


            return row;
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
