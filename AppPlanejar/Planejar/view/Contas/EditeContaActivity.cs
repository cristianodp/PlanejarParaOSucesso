using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SharedPlanejar.Models;
using Controls;
using Android.Support.V7.App;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using com.dinizdesenvolve.planejar.view.ColorPicker;
using appCompactToobar = Android.Support.V7.Widget.Toolbar;
using Android.Webkit;
using Android.Content.Res;
using System.IO;
using com.dinizdesenvolve.planejar.view.Categorias;
using Utils;

namespace com.dinizdesenvolve.planejar.view.Contas
{
    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class EditeContaActivity : ActionBarActivity
    {
        private Item mConta;
        private ControleCategoria controle;


        private Button BtOK;
        private Button BtCanc;
        private Spinner cBoxTipo;
        private Switch BtAtivo;
        private EditText TxtDesc;
        private TextView TxtCor;
        private Spinner cBoxColor;
        private WebView Chart;

        private List<int> mPaletColors;

        private appCompactToobar mToolbar;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityContaEdite);
            int itemId = Intent.GetIntExtra("itemId", 0);

            mToolbar = FindViewById<appCompactToobar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            //SupportActionBar.Title = "Categorias";

            controle = new ControleCategoria();

            if (itemId != 0)
            {

                mConta = controle.getConta(itemId);

            }
            else
            {
                mConta = new Item();
                mConta.ativo = 1;
                mConta.tipo = "C";
                mConta.setTipoCta("Receita");
                mConta.cor = Resource.Color.paletCor01;

            }

            TxtDesc = FindViewById<EditText>(Resource.Id.CtaDesc);

       

            cBoxTipo = FindViewById<Spinner>(Resource.Id.CtaTipo);
            cBoxTipo.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tipoConta, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            cBoxTipo.Adapter = adapter;

            BtAtivo = FindViewById<Switch>(Resource.Id.CtaAtivo);
            BtAtivo.Click += (o, e) => {
                // Perform action on clicks
                if (BtAtivo.Checked)
                    mConta.ativo = 1;
                else
                    mConta.ativo = 0;
            };

            
            



            cBoxColor = FindViewById<Spinner>(Resource.Id.CtaCor);
            cBoxColor.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCor_ItemSelected);

            mPaletColors = new List<int>();
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor01));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor02));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor03));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor04));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor05));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor06));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor07));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor08));
            mPaletColors.Add(Resources.GetColor(Resource.Color.paletCor09));

            AdapterColorPicker adapterColor = new AdapterColorPicker(this, mPaletColors);
            cBoxColor.Adapter = adapterColor;
            carregaCampos();
        }

        private void Atualizar()
        {
            mConta.descricao = TxtDesc.Text;
            try
            {
                controle.AtualizaItem(mConta);
                Finish();
            }
            catch (Exception ex)
            {
                new SimpleAlert(this, "Erro", ex.Message);
            }
            
        }

        private void Deletar()
        {

            if (mConta.id != 0)
            {
                try
                {
                    controle.delelaItens(mConta);
                    Finish();
                }
                catch (Exception ex)
                {
                    new SimpleAlert(this, "Erro", ex.Message);
                }
            }

           
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_edit_cat, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mConta.setTipoCta(spinner.GetItemAtPosition(e.Position).ToString());

        }

        private void spinnerCor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var item = spinner.GetItemAtPosition(e.Position);
            int it = Convert.ToInt32(item);
            mConta.cor = it;

        }


        private void carregaCampos()
        {


            TxtDesc.Text = mConta.descricao;
            /*
                        if (mCategoria.cor != null || mCategoria.cor != 0) {

                            changeColorCategoria(mCategoria.cor);
                        }
                        */
            //seta combo box com o valor correto
            if (this.mConta.getTipoCta(0) != null)
            {
                String[] tiposCat = Resources.GetStringArray(Resource.Array.tipoConta);
                for (int x = 0; x < tiposCat.Count(); x++)
                {
                    if (tiposCat[x].Equals(this.mConta.getTipoCta(1)))
                    {
                        cBoxTipo.SetSelection(x);
                    }
                }
            }

            if (this.mConta.cor != 0)
            {

                for (int x = 0; x < mPaletColors.Count(); x++)
                {
                    if (mPaletColors[x].Equals(this.mConta.cor))
                    {
                        cBoxColor.SetSelection(x);
                    }
                }
            }

            BtAtivo.Checked = (mConta.ativo == 1);
            BtAtivo.RefreshDrawableState();
            List<Lancamento> listMovItem = new ControleMovimento().GetMovsItem(mConta.id);

            //listMovItem.GroupBy(a=>a.GetDataTit().ToString("MMyyyy")).Select(a=>a.);

            WebChart mWebChart = new WebChart("Histórico de Saldo");

            List<string> lables = new List<string>();
            List<Nullable<int>> valores = new List<Nullable<int>>();

            foreach (var lanc in mConta.getMovitos()
                .Where(a => a.status == 1 && (a.GetDataTit() >= DateTime.Now.AddDays(-15) && a.GetDataTit() <= DateTime.Now.AddDays(15)))
                .OrderBy(b=>b.dt_pgto )){

                lables.Add(lanc.GetDataTit().ToString("dd/MM/yyyy"));
                DateTime dt = Convert.ToDateTime(lanc.dt_pgto);
                decimal saldo = lanc.getItemDebt().getSaldo(dt.Date);

                valores.Add(Convert.ToInt16(saldo));
            }
                       
            if (valores.Count > 0)
            {
                                              
                mWebChart.dataChart.AddChartData("spline"
                , lables.ToArray()
                , null
                , valores.ToArray()
                );
            }

            Chart = FindViewById<WebView>(Resource.Id.CtaChart);
            mWebChart.loadChart(Chart);
            /*(string pType bar, line, area, pie, column, spline, splineArea, doughnut 
                               , string pTitle
                               , string[] pLabels
                               , int?[] pValuesY
                               , int?[] pValuesX)
*/



        }


        private void changeColor(int selectedColor)
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

            mConta.cor = selectedColor;

            TxtCor.Background = shape;
            TxtCor.Invalidate();

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

                        Deletar();
                    }
                    break;
                case (Resource.Id.menu_edcat_Salvar):
                    {
                        Atualizar();
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