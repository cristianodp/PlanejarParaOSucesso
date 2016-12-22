using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    class SimpleAlert
    {
        private AlertDialog.Builder mDialog;

        public SimpleAlert(Context context,String titulo,String msg) {
            mDialog = new AlertDialog.Builder(context);

            mDialog.SetTitle(titulo);
            mDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            mDialog.SetMessage(msg);
            mDialog.SetCancelable(false);

            mDialog.SetPositiveButton("Ok", (o, e) => {

            });
            mDialog.Create();
            mDialog.Show();
        }
        
    }
}
