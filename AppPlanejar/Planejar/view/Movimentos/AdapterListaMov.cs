using System.Collections.Generic;
using Android.Content;
using SharedPlanejar.Models;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;
using Android.Views;
using System.Linq;

namespace com.dinizdesenvolve.planejar.view.Movimentos
{
    public class AdapterListaMov : BaseAdapter<Lancamento>
    {
        private Context mContext;
        private List<Lancamento> mList;


        public AdapterListaMov(Context context, List<Lancamento> list)
        {
            this.mContext = context;
            var lista = list;
            this.mList = lista;
        }


        public override int Count
        {
            get
            {
                return mList.Count;
            }
        }

        public override Lancamento this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_mov_list_view, null, false);
            }

            TextView row_mov_img = row.FindViewById<TextView>(Resource.Id.row_mov_img);
            TextView row_mov_valor = row.FindViewById<TextView>(Resource.Id.row_mov_valor);
            TextView row_mov_data = row.FindViewById<TextView>(Resource.Id.row_mov_data);
            TextView row_mov_item = row.FindViewById<TextView>(Resource.Id.row_mov_item);
            TextView row_mov_pago = row.FindViewById<TextView>(Resource.Id.row_mov_pago);

            row_mov_img.SetBackgroundColor(new Color(mList[position].getCategoria().cor));
            row_mov_item.Text = mList[position].getItem().descricao;
            row_mov_valor.Text = mList[position].GetValorTit().ToString("R$ #,###,###,##0.00");
            row_mov_data.Text = mList[position].GetDataTit().ToString("dd/MM/yyyy");
            if (mList[position].status == 1)
            {
                row_mov_pago.Text = "Pago";
            }
            else
            {
                row_mov_pago.Text = "Em aberto";
            }

            if (mList[position].getCategoria().getTipo(0).Equals("R"))
            {
                row_mov_valor.Text = mList[position].GetValorTit().ToString("R$ #,###,###,##0.00");
                row_mov_valor.SetTextColor(new Color(Resource.Color.paletCor05));
                //row_mov_img.SetImageResource(Resource.Drawable.ic_plus);
            }
            else
            {
                row_mov_valor.Text = mList[position].GetValorTit().ToString(" - R$ #,###,###,##0.00");
                row_mov_valor.SetTextColor(new Color(Resource.Color.paletCor01));
            }

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