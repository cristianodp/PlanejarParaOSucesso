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

namespace com.dinizdesenvolve.planejar.view.Itens
{

    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class ItensFrag : Android.Support.V4.App.Fragment
    {
        private List<Item> mAdapterList;
        // private List<MyData> SpinnerData;
        private AdapterListaItens mAdapter;
        //  private Spinner mItCatCategoria;
        private ListView mListView;
        private ControleItem controle;
        private Context mContext;
        private Android.Views.View mView;



        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            controle = new ControleItem();
            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            Android.Views.View view = inflater.Inflate(Resource.Layout.FragItens, container, false);
            mContext = container.Context;
            mView = view;

            mListView = view.FindViewById<ListView>(Resource.Id.ItCatlistView);
            mListView.ItemClick += (o, e) =>
            {
                if (mAdapterList[e.Position].id > 0)
                {
                    Intent intent = new Intent(mContext, typeof(EditeItensActivity));
                    intent.PutExtra("itemId", mAdapterList[e.Position].id);
                    StartActivity(intent);
                }
            };
            var CatBTAdd = mView.FindViewById<FloatingActionButton>(Resource.Id.itCatBTAdd);
            CatBTAdd.Click += (o, e) => {
                Intent intent = new Intent(mContext, typeof(EditeItensActivity));
                StartActivity(intent);
            };
            return view;
        }


        public override void OnResume()
        {

            mAdapterList = controle.GetItens("N");

            mAdapter = new AdapterListaItens(mContext, mAdapterList);

            mListView.Adapter = mAdapter;

            base.OnResume();

        }




    }
        
     
}