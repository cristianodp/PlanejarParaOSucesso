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

namespace com.dinizdesenvolve.planejar.view.Contas
{
    public class AdapterListaCta : BaseAdapter<Item>
    {
        List<Item> mList;
        Context mContext;

        public AdapterListaCta(Context context, List<Item> list)
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

        public override Item this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_conta_list_view, null, false);
            }
            
            TextView img = row.FindViewById<TextView>(Resource.Id.rowListItCtaImg);
            TextView textVdesc = row.FindViewById<TextView>(Resource.Id.rowListItCtaTitulo1);
            TextView textVtipo = row.FindViewById<TextView>(Resource.Id.rowListItCtaSubTitulo);
            TextView textVValor = row.FindViewById<TextView>(Resource.Id.rowListItCtaValor); 

            int cor = mList[position].cor;
            string descricao = mList[position].descricao;
            string tipo = mList[position].getTipoCta(1);
            decimal valor = mList[position].getSaldo();

            img.SetBackgroundColor(new Color(cor));

            //changeColorCategoria(cor, img);
            textVdesc.Text = descricao;
            textVtipo.Text = tipo;
            textVValor.Text = valor.ToString("R$ #,###,###,##0.00");

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
