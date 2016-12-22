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

namespace com.dinizdesenvolve.planejar.view.Categorias
{
    public class CategoriasFrag : Android.Support.V4.App.Fragment
    {
        private List<Categoria> mAdapterList;
        private AdapterListaCat mAdapter;
        private ListView mListView;
        private ControleCategoria controle;
        private Context mContext;
        private Android.Views.View mView;
  

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            controle = new ControleCategoria();
            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            Android.Views.View view = inflater.Inflate(Resource.Layout.FragCategorias, container, false);
            mContext = container.Context;
            mView = view;

            mListView = mView.FindViewById<ListView>(Resource.Id.CatlistView);
            mListView.ItemClick += mListViewClick;
            // mListView.ItemLongClick += mListViewLingClick;
            AdicionaEventos();
            return view;
        }

        

        private async void mListViewClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(mContext, typeof(EditeCategoriaActivity));
            intent.PutExtra("catId", mAdapterList[e.Position].id);
            StartActivity(intent);
        }

        public override void OnResume()
        {

            carregaLista();
            
                        
            base.OnResume();


        }

        private void AdicionaEventos() {
            var CatBTAdd = mView.FindViewById<FloatingActionButton>(Resource.Id.CatBTAdd);
            CatBTAdd.Click += async (o,e) => {
                Intent intent = new Intent(mContext, typeof(EditeCategoriaActivity));
                StartActivity(intent);
            };
        }
        private void carregaLista()
        {
            mAdapterList = controle.Consultar();
           
            mAdapter = new AdapterListaCat(mContext, mAdapterList);
            
            mListView.Adapter = mAdapter;

          

        }

    }
}