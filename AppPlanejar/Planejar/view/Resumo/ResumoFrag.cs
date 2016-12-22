using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Android.Views;
using Android.Webkit;
using com.refractored.fab;
using Controls;
using com.dinizdesenvolve.planejar.view.Movimentos;
using Utils;

namespace com.dinizdesenvolve.planejar.view.Resumo
{
    public class ResumoFrag : Android.Support.V4.App.Fragment
    {

        private TextView ResumoReceita;
        private TextView ResumoDespesa;
        private TextView ResumoSaldo;
        private WebView resumoChart;
        private FloatingActionButton ResumoBTAdd;
        private Context mContext;
        private ControleCategoria controle;



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
            //return inflater.
            Android.Views.View view = inflater.Inflate(Resource.Layout.FragResumo, container, false);

            mContext = view.Context;
            ResumoReceita = view.FindViewById<TextView>(Resource.Id.resumoReceita);
            ResumoDespesa = view.FindViewById<TextView>(Resource.Id.ResumoDespesa);
            ResumoSaldo = view.FindViewById<TextView>(Resource.Id.ResumoSaldo);
            resumoChart = view.FindViewById<WebView>(Resource.Id.resumoChart);
            ResumoBTAdd = view.FindViewById<FloatingActionButton>(Resource.Id.ResumoBTAdd);

            ResumoBTAdd.Click += (o, e) =>
            {

                if (new ControleCategoria().Consultar().Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem categorias cadastradas. Candastre-as antes de continuar.");
                }
                else if (new ControleItem().GetItens("C").Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem contas cadastradas. Candastre-as antes de continuar.");

                }
                else if (new ControleCategoria().Consultar().Where(a => a.getTipo(2).Equals("R")).Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem categorias tipo receita cadastradas. Candastre-as antes de continuar.");

                }
                else if (new ControleCategoria().Consultar().Where(a => a.getTipo(2).Equals("D")).Count() == 0)
                {
                    new SimpleAlert(mContext, "Erro", "Não existem categorias tipo despesa cadastradas. Candastre-as antes de continuar.");
                }
                else
                {
                    Intent intent = new Intent(mContext, typeof(EditeMovtoActivity));
                    StartActivity(intent);
                }



            };

            

            return view;




        }

        public override void OnResume()
        {
            base.OnResume();
            CarregaTela();
        }

        public void CarregaTela() {

            var totalReceitas = controle.getContas().Sum(a => a.getReceitas());

            ResumoReceita.Text = totalReceitas.ToString("R$ #,###,###,##0.00");

            var totalDespesas = controle.getContas().Sum(a => a.getDespesas());

            ResumoDespesa.Text = totalDespesas.ToString("R$ #,###,###,##0.00");
            var contas = controle.getContas();
            var totalSaldoAnt = contas.Sum(a => a.getSaldoAnterior() );

            var saldo = totalReceitas + totalSaldoAnt - totalDespesas;

            ResumoSaldo.Text = saldo.ToString("R$ #,###,###,##0.00");

            WebChart mWebChart = new WebChart("Histórico de Saldo");

            List<string> lables = new List<string>();
            List<Nullable<int>> valores = new List<Nullable<int>>();

            var movtos = new ControleMovimento().getMovimentos()
                .Where(a => a.status == 1
                        && a.GetDataTit().Date.Month == DateTime.Now.Month
                        && a.GetDataTit().Date.Year == DateTime.Now.Year);

            var dataValues = movtos.Where(a => a.tipo.Equals("D"))
                .GroupBy(g => g.getCategoria().descricao )
                .Select(g => new {
                    cat = g.Key,
                    valor = g.Sum(a => a.vlr_pgto) }).ToList();


/*            dataValues = dataValues
               .GroupBy(g => g.cat.id)
               .Select(g => new {
                   cat = g.Key,
                   valor = g.Sum(a => a.valor)
               }).ToList();
               */
            foreach (var it in dataValues)
            {

                lables.Add(it.cat);
                valores.Add(Convert.ToInt16(it.valor));
            }

            if (valores.Count > 0)
            {

                mWebChart.dataChart.AddChartData("doughnut"
                , lables.ToArray()
                , null
                , valores.ToArray()
                );
            }

           //hart = FindViewById<WebView>(Resource.Id.CtaChart);
            mWebChart.loadChart(resumoChart);


        }

    }
}