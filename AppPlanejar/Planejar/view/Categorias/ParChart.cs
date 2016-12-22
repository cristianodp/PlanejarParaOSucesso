using Android.Webkit;
using Java.Interop;
using Java.Lang;

namespace com.dinizdesenvolve.planejar.view.Categorias
{
    internal class ParChart : Java.Lang.Object
    {
        private int num;
        public ParChart(int num)
        {
            this.num = num;
        }

        [Export]
        [JavascriptInterface]
        public int getNum1()
        {
            return num;
        }
    };
}