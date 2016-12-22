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
using Utils;
using com.refractored.fab;
using com.dinizdesenvolve.planejar.view.Categorias.Metas;

namespace com.dinizdesenvolve.planejar.view.Movimentos
{
    public class MovimentosFrag : Android.Support.V4.App.Fragment
    {
        private List<Lancamento> mAdapterList;
        private AdapterListaMov mAdapter;
        private DateTime mDtMesAno;
        private TextView mTxtMesAno;
        private TextView mMovTotal;
        private ListView mListView;
        private ControleMovimento controle;
        private Context mContext;
        private Android.Views.View mView;
        private String mask = "MMMM yyyy";
        private ImageButton btNext;
        private ImageButton btPrev;
        private FloatingActionButton btAdd; 

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mDtMesAno = DateTime.Now;
            controle = new ControleMovimento();
            // Create your fragment here
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            Android.Views.View view = inflater.Inflate(Resource.Layout.FragMovimentos, container, false);
            mContext = container.Context;
            mView = view;

            mTxtMesAno = mView.FindViewById<TextView>(Resource.Id.MovMesAno);

            mTxtMesAno.Text = mDtMesAno.ToString(mask);

            mListView = mView.FindViewById<ListView>(Resource.Id.MovList);
            mListView.ItemClick += mListViewClick;
            mListView.ItemLongClick += mListViewLingClick;

            mMovTotal = mView.FindViewById<TextView>(Resource.Id.movTotal);

            AdicionaEventos();

            return view;
        }


        private void mListViewLingClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            Intent intent = new Intent(mContext, typeof(EditeMovtoActivity));
            intent.PutExtra("lancId", mAdapterList[e.Position].id);
            StartActivity(intent);
        }

        private void mListViewClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(mContext, typeof(EditeMovtoActivity));
            intent.PutExtra("lancId", mAdapterList[e.Position].id);
            StartActivity(intent);
        }

        public override void OnResume()
        {
            carregaLista();
            //AdicionaEventos();

            base.OnResume();
        }


        private void AdicionaEventos()
        {
            btAdd = mView.FindViewById<FloatingActionButton>(Resource.Id.MovBTAdd);
            btAdd.Click += (o,e)=> {
                if (new ControleCategoria().Consultar().Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem categorias cadastradas. Candastre-as antes de continuar.");
                }
                else if (new ControleItem().GetItens("C").Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem contas cadastradas. Candastre-as antes de continuar.");

                } else if (new ControleCategoria().Consultar().Where(a => a.getTipo(2).Equals("R")).Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem categorias tipo receita cadastradas. Candastre-as antes de continuar.");

                } else if (new ControleCategoria().Consultar().Where(a => a.getTipo(2).Equals("D")).Count() == 0) {
                    new SimpleAlert(mContext, "Erro", "Não existem categorias tipo despesa cadastradas. Candastre-as antes de continuar.");
                }else
                {
                    Intent intent = new Intent(mContext, typeof(EditeMovtoActivity));
                    StartActivity(intent);
                }
            };

            btNext = mView.FindViewById<ImageButton>(Resource.Id.MovDataNext);

            btNext.Click += (o, e) => {
                mDtMesAno = mDtMesAno.AddMonths(1);
                RefrashDate();
            };

            btPrev = mView.FindViewById<ImageButton>(Resource.Id.MovDataPrev);
            btPrev.Click += (o, e) => {
                mDtMesAno = mDtMesAno.AddMonths(-1);
                
                RefrashDate();
                
            };

        }

        private void RefrashDate()
        {
            mTxtMesAno.Text = mDtMesAno.ToString(mask);
            try
            {
                carregaLista();
            }
            catch (Exception e)
            {

                string atr = e.Message;
                // Log.v(e);
            };

        }

        private void carregaLista()
        {


            mAdapterList = controle.Consultar(mDtMesAno).OrderBy(a => a.GetDataTit()).ToList();

            mAdapter = new AdapterListaMov(mContext, mAdapterList);
            
            mListView.Adapter = mAdapter;
            
            var tot_rec = mAdapterList.Where(x => x.getCategoria().getTipo(0).Equals("R")).Sum(a => a.GetValorTit());
            var tot_des = mAdapterList.Where(x => x.getCategoria().getTipo(0).Equals("D")).Sum(a => a.GetValorTit());

            var tot_geral = tot_rec - tot_des;

            tot_rec = mAdapterList.Where(x => x.getCategoria().getTipo(0).Equals("R")).Sum(y => y.vlr_pgto);
            tot_des = mAdapterList.Where(x => x.getCategoria().getTipo(0).Equals("D")).Sum(a => a.vlr_pgto);

            var tot_pgto = tot_rec - tot_des;

            mMovTotal.Text = tot_pgto.ToString("R$ #,###,###,##0.00")+" / "+tot_geral.ToString("R$ #,###,###,##0.00");


        }


    }
}