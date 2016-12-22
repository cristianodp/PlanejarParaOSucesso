using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class Imagens { 

        public static ShapeDrawable customDrawableCircle(int Color)
        {
            ShapeDrawable mDrawable;
            int x = 10;
            int y = 10;
            int width = 300;
            int height = 50;
           
            mDrawable = new ShapeDrawable(new OvalShape());
            //ColorDrawable.
            /* mDrawable.Paint.Color = (new DrawColor);
            mDrawable.setBounds(x, y, x + width, y + height);*/
            return mDrawable;
        }
    }
}
