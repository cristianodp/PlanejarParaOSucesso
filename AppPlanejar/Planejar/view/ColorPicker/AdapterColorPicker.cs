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
using Java.Lang;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Content.Res;

namespace com.dinizdesenvolve.planejar.view.ColorPicker
{
    class AdapterColorPicker : BaseAdapter<int>
    {
        private List<int> mPaletColors;
        private Context mContext;

        public AdapterColorPicker(Context context,List<int> PaletColors)
        {

            mContext = context;

            mPaletColors = PaletColors;
             

        }

        public override int Count
        {
            get
            {
                return mPaletColors.Count;
            }
        }

        public override int this[int position]
        {
            get
            {
                return mPaletColors[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.row_color_picker, null, false);
            }

            TextView img = row.FindViewById<TextView>(Resource.Id.row_color);
            
            int cor = mPaletColors[position];

            ShapeDrawable mDrawable;
            int x = 10;
            int y = 10;
            int width = 300;
            int height = 50;

            mDrawable = new ShapeDrawable(new OvalShape());
            mDrawable.Paint.Color = new Color(cor);
            mDrawable.SetBounds(x, y, x + width, y + height);

            ShapeDrawable shape = mDrawable;


            img.Background = shape;
            img.Invalidate();


            return row;
        }

        
    }
}

