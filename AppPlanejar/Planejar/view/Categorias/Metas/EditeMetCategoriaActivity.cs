using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SharedPlanejar.Models;
using Controls;
using Android.Support.V7.App;
using appCompactToobar = Android.Support.V7.Widget.Toolbar;
using Utils;

namespace com.dinizdesenvolve.planejar.view.Categorias.Metas
{

    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class EditeMetCategoriaActivity : ActionBarActivity
    {
        private Meta mMeta;
        private ControleCategoria controle;

        private Button ItCatBtOK;
        private Button ItCatBtCanc;

        private TextView metDtIni;
        private TextView metDtfim;
        private EditText metDtValor;
        
        private Switch   metCatTgBtAtivo;
      

        private appCompactToobar mToolbar;
        private int CatId;
        private String dateformat = "dd/MM/yyyy";

        private Dialog mDialog;

        /*DATE PICKER ******************************************************************/
       

        private void DatePickerListnerInicio(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            mMeta.dt_inicio = e.Date;
            metDtIni.Text = mMeta.dt_inicio.ToString(dateformat);
        }

        private void DatePickerListnerFinal(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            mMeta.dt_final = e.Date;

            metDtfim.Text = mMeta.dt_final.ToString(dateformat);

        }
        /******************************************************DATE PICKER */

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityCatMetaEdite);
            CatId = Intent.GetIntExtra("CatId", 0);
            int MetCatId = Intent.GetIntExtra("metCatId", 0);

            mToolbar = FindViewById<appCompactToobar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            //SupportActionBar.Title = "Categorias";

            controle = new ControleCategoria();

            if (MetCatId != 0)
            {

                mMeta = controle.GetMeta(MetCatId);

            }
            else
            {
                mMeta = new Meta();
                mMeta.ativo = 1;
                mMeta.cat_id = CatId;

                DateTime dtNow = DateTime.Now;

                mMeta.dt_inicio = new DateTime(dtNow.Year, dtNow.Month, 1);
                mMeta.dt_final = new DateTime(dtNow.Year, dtNow.Month, DateTime.DaysInMonth(dtNow.Year, dtNow.Month));

            }

            metDtIni = FindViewById<TextView>(Resource.Id.MetCatInicio);
            metDtfim = FindViewById<TextView>(Resource.Id.MetCatFim);
            metDtValor = FindViewById<EditText>(Resource.Id.MetCatValor);


            metDtIni.Click += (e,o) => {

                Dialog dg = new DatePickerDialog(this, DatePickerListnerInicio, mMeta.dt_inicio.Year, mMeta.dt_inicio.Month-1, mMeta.dt_inicio.Day);
                dg.Show();
                //ShowDialog(DIALOG_ID_INICIO);

            };
            

            metDtfim.Click += (e, o) => {

                Dialog dg = new DatePickerDialog(this, DatePickerListnerFinal, mMeta.dt_final.Year, mMeta.dt_final.Month-1, mMeta.dt_final.Day);
                dg.Show();

            };


            metCatTgBtAtivo = FindViewById<Switch>(Resource.Id.MatCatAtivo);
            metCatTgBtAtivo.Click += (o, e) => {
                // Perform action on clicks
                if (metCatTgBtAtivo.Checked)
                    mMeta.ativo = 1;
                else
                    mMeta.ativo = 0;
            };

            carregaCampos();
        }

       

        private void atualizaItemCategoria()
        {
            try
            {
                mMeta.dt_inicio = DateTime.ParseExact(metDtIni.Text, dateformat, System.Globalization.CultureInfo.InvariantCulture);
                mMeta.dt_final = DateTime.ParseExact(metDtfim.Text, dateformat, System.Globalization.CultureInfo.InvariantCulture);
                mMeta.valor = Convert.ToDecimal(metDtValor.Text);

                controle.AtualizaMeta(mMeta);

                Finish();
            }
            catch (Exception e) {
                new SimpleAlert(this, "Erro", e.Message);
            }
        }

        private void delataMetaCategoria()
        {

            if (mMeta.id != 0)
            {
                controle.delelaMeta(mMeta);
            }

            Finish();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_edit_cat, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void carregaCampos()
        {

            metDtIni.Text = mMeta.getDtInicio();
            metDtfim.Text = mMeta.getDtFinal();
            if (mMeta.valor != 0)
            {
                metDtValor.Text = mMeta.valor.ToString("#,###,###,##0.00");
            }
            else {
                metDtValor.Text = null;
            }
            metCatTgBtAtivo.Checked = mMeta.ativo==1;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case (Resource.Id.menu_edcat_canc):
                    Finish();
                    break;
                case (Resource.Id.menu_edcat_deleta):
                    {

                        delataMetaCategoria();
                    }
                    break;
                case (Resource.Id.menu_edcat_Salvar):
                    {
                        atualizaItemCategoria();
                    }
                    break;
                case (Android.Resource.Id.Home):
                    Finish();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}