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
using Controls;
using Utils;

namespace com.dinizdesenvolve.planejar.Views.Usuario
{
    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class CadUsuarioActivity : Activity
    {
        private ControlUsuario control; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CadUsuario);
            // Create your application here
            control = new ControlUsuario();
            Button button = FindViewById<Button>(Resource.Id.CadUsuarioBTsalvar);

            button.Click += delegate {

                EditText nome = FindViewById<EditText>(Resource.Id.CadUsuarioNome);
                EditText email = FindViewById<EditText>(Resource.Id.CadUsuarioEmail);
                EditText senha = FindViewById<EditText>(Resource.Id.CadUsuarioSenha);

                try { 
                    control.addUsuario(nome.Text, email.Text, senha.Text);

                    this.Finish();
                }
                catch(Exception ex )
                {
                    new SimpleAlert(this, "Erro", ex.Message);

                }
               


            };
        }
    }
}