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

namespace com.dinizdesenvolve.planejar.Views.Usuario
{
    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class LoginActivity : Activity
    {
        private ControlUsuario control;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityLogin);

            control = new ControlUsuario();
            // Create your application here
            Button btnEntrar = FindViewById<Button>(Resource.Id.loginEntrar);

            btnEntrar.Click += delegate {

                EditText email = FindViewById<EditText>(Resource.Id.loginEmail);
                EditText senha = FindViewById<EditText>(Resource.Id.loginSenha);
                              

                if (control.login(email.Text, senha.Text))
                {
                    Finish();
                }
                else
                {
                    Toast msg = Toast.MakeText(this, "Usuário ou senha inválidos.",ToastLength.Long);
                    msg.Show();
                };

                


            };

            

        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!control.ExistUsr())
            {
                AlertDialog.Builder mDialog;
                mDialog = new AlertDialog.Builder(this);
                mDialog.SetTitle("Alerta");
                mDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                mDialog.SetMessage("Não existe usuários cadastrados deseja cadastra agora?");
                mDialog.SetCancelable(false);

                mDialog.SetPositiveButton("Sim", (o, e) =>
                {
                    var intent = new Intent(this, typeof(CadUsuarioActivity));
                    StartActivity(intent);
                });

                mDialog.SetNegativeButton("Não", (o, e) =>
                {
                    Finish();
                });

                mDialog.Create();
                mDialog.Show();
            }
            else {
                if (new ControlPrincipal().isLogado())
                {
                    Finish();

                }
            }
           
        }
        
    }
}