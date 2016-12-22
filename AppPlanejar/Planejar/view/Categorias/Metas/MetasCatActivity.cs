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
using com.refractored.fab;
using com.dinizdesenvolve.planejar.Views.Usuario;
using Android.Support.V7.App;
using appCompactToobar = Android.Support.V7.Widget.Toolbar;
using Utils;
using com.dinizdesenvolve.planejar.view.Categorias.Metas;

namespace com.dinizdesenvolve.planejar.view.Categorias
{

    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MetasCatActivity : ActionBarActivity
    {
        private List<Meta> mAdapterList;
        private List<MyData> SpinnerData;
        private AdapterListaMetCat mAdapter;
        private Spinner mItCatCategoria;
        private ListView mListView;
        private ControleCategoria controle;
        private List<Categoria> categorias;
        private int CatId;
        private appCompactToobar mToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityCatMeta);

            CatId = Intent.GetIntExtra("CatId", 0);
            controle = new ControleCategoria();


            mToolbar = FindViewById<appCompactToobar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);


            carregaSpinner();

            mListView = FindViewById<ListView>(Resource.Id.MetCatlistView);
            mListView.ItemClick += mListViewClick;
            mListView.ItemLongClick += mListViewLingClick;

            AdicionaEventos();

        }

        public void carregaSpinner()
        {



            SpinnerData = new List<MyData>();

            categorias = controle.Consultar();
            foreach (Categoria item in categorias)
            {
                SpinnerData.Add(new MyData(item.id.ToString(), item.descricao));
            }


            mItCatCategoria = FindViewById<Spinner>(Resource.Id.MetCatCategoria);
            mItCatCategoria.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerItCat_ItemSelected);

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem
                , categorias.Select(a => a.descricao).ToArray());

            mItCatCategoria.Adapter = adapter;

            //seta combo box com o valor correto
            if (CatId != 0)
            {
                for (int x = 0; x < SpinnerData.Count(); x++)
                {
                    if (SpinnerData[x].getKeyInt() == CatId)
                    {
                        mItCatCategoria.SetSelection(x);
                    }
                }
            }


        }

        private void spinnerItCat_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            String selected = spinner.GetItemAtPosition(e.Position).ToString();
            CatId = SpinnerData.Where(a => a.getValue().Equals(selected)).FirstOrDefault().getKeyInt();

            carregaLista();
        }

        private void mListViewLingClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            Intent intent = new Intent(this, typeof(EditeMetCategoriaActivity));
            intent.PutExtra("CatId", mAdapterList[e.Position].cat_id);
            intent.PutExtra("metCatId", mAdapterList[e.Position].id);
            StartActivity(intent);
        }

        private void mListViewClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditeMetCategoriaActivity));
            intent.PutExtra("CatId", mAdapterList[e.Position].cat_id);
            intent.PutExtra("metCatId", mAdapterList[e.Position].id);
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            carregaLista();
            

            base.OnResume();
        }

        private void AdicionaEventos()
        {
            var CatBTAdd = FindViewById<FloatingActionButton>(Resource.Id.itCatBTAdd);
            CatBTAdd.Click += (o, e) => {
                Intent intent = new Intent(this, typeof(EditeMetCategoriaActivity));
                intent.PutExtra("CatId", CatId);
                StartActivity(intent);
            };
        }
        private void carregaLista()
        {
            mAdapterList = controle.GetMetas(CatId).ToList();

            mAdapter = new AdapterListaMetCat(this, mAdapterList);

            mListView.Adapter = mAdapter;
            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case (Android.Resource.Id.Home):
                    Finish();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}