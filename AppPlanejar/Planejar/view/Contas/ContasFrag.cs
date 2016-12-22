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
using SharedPlanejar.Models;
using Controls;
using com.refractored.fab;

namespace com.dinizdesenvolve.planejar.view.Contas
{
    public class ContasFrag : Android.Support.V4.App.Fragment
    {
        private List<Item> mAdapterList;
        private AdapterListaCta mAdapter;
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

            Android.Views.View view = inflater.Inflate(Resource.Layout.FragContas, container, false);
            mContext = container.Context;
            mView = view;

            mListView = mView.FindViewById<ListView>(Resource.Id.CtalistView);
            mListView.ItemClick += (o, e) =>
            {
                if (mAdapterList[e.Position].id > 0)
                {
                    Intent intent = new Intent(mContext, typeof(EditeContaActivity));
                    intent.PutExtra("itemId", mAdapterList[e.Position].id);
                    StartActivity(intent);
                }
            };
            var CatBTAdd = mView.FindViewById<FloatingActionButton>(Resource.Id.CtaBTAdd);
            CatBTAdd.Click += (o, e) => {
                Intent intent = new Intent(mContext, typeof(EditeContaActivity));
                StartActivity(intent);
            };
            return view;
        }


        public override void OnResume()
        {

            mAdapterList = controle.getContas();

            mAdapter = new AdapterListaCta(mContext, mAdapterList);

            mListView.Adapter = mAdapter;

            base.OnResume();
            
        }

   
      

    }
}