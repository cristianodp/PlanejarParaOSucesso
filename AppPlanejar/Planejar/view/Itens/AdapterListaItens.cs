using System.Collections.Generic;
using Android.Content;
using SharedPlanejar.Models;
using Android.Widget;
using Android.Views;
using System;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;

namespace com.dinizdesenvolve.planejar.view.Itens
{
    internal class AdapterListaItens : BaseAdapter<Item>
    {
        private Context mContext;
        private List<Item> mList;
        

        public AdapterListaItens(Context context, List<Item> list) 
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_item_cat_list_view, null, false);
            }

            TextView img = row.FindViewById<TextView>(Resource.Id.rowListItCatImg);
            TextView textVdesc = row.FindViewById<TextView>(Resource.Id.rowListItCatTitulo);

            int ativo = mList[position].ativo;
            string descricao = mList[position].descricao;
            string tipo = mList[position].tipo;
            int corAtivo = mContext.Resources.GetColor(Resource.Color.colorAtivo);
            int corInativo = mContext.Resources.GetColor(Resource.Color.colorInativo);
            if (ativo == 1)            {
                img.SetBackgroundColor(new Color(corAtivo));
            }
            else {
                img.SetBackgroundColor(new Color(corInativo));
            }
           
            textVdesc.Text = descricao;
      

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
