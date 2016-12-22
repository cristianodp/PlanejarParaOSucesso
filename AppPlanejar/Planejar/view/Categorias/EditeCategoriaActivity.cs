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
using com.dinizdesenvolve.planejar.view.Itens;

namespace com.dinizdesenvolve.planejar.view.Categorias
{
    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class EditeCategoriaActivity : ActionBarActivity
    {
        private Categoria mCategoria;
        private ControleCategoria controle;

        private Button CatBtItens;
        private Button CatBtMetas;
        private Button CatBtOK;
        private Button CatBtCanc;
        private Spinner CatcBoxTipoCat;
        private ToggleButton CatTgBtAtivo;
        private EditText CatEdTxtDesc;
        private TextView CatTxtVwCor;
        private Spinner CatSPColor;
        private WebView CatChart;

        private List<int> mPaletColors;

        private appCompactToobar mToolbar;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityCatEdite);
            int CatId = Intent.GetIntExtra("catId", 0);

            mToolbar = FindViewById<appCompactToobar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            //SupportActionBar.Title = "Categorias";

            controle = new ControleCategoria();

            if (CatId != 0)
            {

                mCategoria = controle.getCategoria(CatId);

            }
            else {
                mCategoria = new Categoria();
                mCategoria.ativo = 1;
                mCategoria.visivel = 1;
                mCategoria.setTipo("Receita");
                mCategoria.cor = Resource.Color.paletCor01;

            }

            CatEdTxtDesc = FindViewById<EditText>(Resource.Id.CatEdTxtDesc);
            //            CatTxtVwCor = FindViewById<TextView>(Resource.Id.CatTxtVwCor);



            CatcBoxTipoCat = FindViewById<Spinner>(Resource.Id.CatcBoxTipoCat);
            CatcBoxTipoCat.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.tipoCategoria, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            CatcBoxTipoCat.Adapter = adapter;

            CatTgBtAtivo = FindViewById<ToggleButton>(Resource.Id.CatTgBtAtivo);
            CatTgBtAtivo.Click += (o, e) => {
                // Perform action on clicks
                if (CatTgBtAtivo.Checked)
                    mCategoria.ativo = 1;
                else
                    mCategoria.ativo = 0;
            };
            
            CatChart = FindViewById<WebView>(Resource.Id.CategoriaChart);

            
            CatSPColor = FindViewById<Spinner>(Resource.Id.CatSPColor);
            CatSPColor.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCor_ItemSelected);

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
            CatSPColor.Adapter = adapterColor;
            carregaCampos();
        }

        private void atualizaCategoria()
        {
            mCategoria.descricao = CatEdTxtDesc.Text;

            controle.Atualizar(mCategoria);

            Finish();
        }

        private void delataCategoria() {

            if (mCategoria.id != 0) {

                controle.deleta(mCategoria);

            }
            
            Finish();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_categoria, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mCategoria.setTipo(spinner.GetItemAtPosition(e.Position).ToString());

        }

        private void spinnerCor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var item = spinner.GetItemAtPosition(e.Position);
            int it = Convert.ToInt32(item);
            mCategoria.cor = it;

        }


        private void carregaCampos() {


            CatEdTxtDesc.Text = mCategoria.descricao;
            /*
                        if (mCategoria.cor != null || mCategoria.cor != 0) {

                            changeColorCategoria(mCategoria.cor);
                        }
                        */
            //seta combo box com o valor correto
            if (this.mCategoria.getTipo(0) != null)
            {
                String[] tiposCat = Resources.GetStringArray(Resource.Array.tipoCategoria);
                for (int x = 0; x < tiposCat.Count(); x++)
                {
                    if (tiposCat[x].Equals(this.mCategoria.getTipo(1)))
                    {
                        CatcBoxTipoCat.SetSelection(x);
                    }
                }
            }

            if (this.mCategoria.cor != 0)
            {

                for (int x = 0; x < mPaletColors.Count(); x++)
                {
                    if (mPaletColors[x].Equals(this.mCategoria.cor))
                    {
                        CatSPColor.SetSelection(x);
                    }
                }
            }

            CatTgBtAtivo.Checked = (mCategoria.ativo == 1);
            CatTgBtAtivo.RefreshDrawableState();
            CarregaGrafico();

        }


        private void CarregaGrafico()
        {
            WebChart mWebChart = new WebChart("Histórico de Saldo");

            List<string> lables = new List<string>();
            List<Nullable<int>> valores = new List<Nullable<int>>();

           /* foreach (var lanc in mConta.getMovitos()
                .Where(a => a.status == 1 && (a.GetDataTit() >= DateTime.Now.AddDays(-15) && a.GetDataTit() <= DateTime.Now.AddDays(15)))
                .OrderBy(b => b.dt_pgto))
            {

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
            }*/

            //CatChart = FindViewById<WebView>(Resource.Id.CtaChart);
            mWebChart.loadChart(CatChart);
        }

        private void changeColorCategoria(int selectedColor)
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

            mCategoria.cor = selectedColor;

            CatTxtVwCor.Background = shape;
            CatTxtVwCor.Invalidate();

        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case (Resource.Id.menu_cat_canc):
                    Finish();
                    break;
                case (Resource.Id.menu_cat_deleta): {

                        delataCategoria();
                    }
                    break;
                case (Resource.Id.menu_cat_Salvar): {
                        atualizaCategoria();
                    }
                    break;
               /*
                case (Resource.Id.menu_cat_itens):
                    {
                        Intent intent = new Intent(this, typeof(ItensCatActivity));
                        intent.PutExtra("CatId", mCategoria.id);
                        StartActivity(intent);
                    }
                    break;
                    
                case (Resource.Id.menu_cat_metas):
                    {

                        Intent intent = new Intent(this, typeof(MetasCatActivity));
                        intent.PutExtra("CatId", mCategoria.id);
                        StartActivity(intent);
                    }
                    break;*/
                case (Android.Resource.Id.Home):
                    {
                        Finish();
                    }
                    break;


            }



            return base.OnOptionsItemSelected(item);
        }

    }
}