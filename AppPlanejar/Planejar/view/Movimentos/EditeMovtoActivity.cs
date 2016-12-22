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
using SharedPlanejar.Models;
using Controls;
using Android.Support.V7.App;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using com.dinizdesenvolve.planejar.view.ColorPicker;
using Android.Webkit;
using Android.Content.Res;
using System.IO;
using com.dinizdesenvolve.planejar.view.Categorias;
using appCompact = Android.Support.V7.Widget.Toolbar;
using com.dinizdesenvolve.planejar.view.Categorias.Metas;
using Utils;
using Android.Text;
using Android.Widget;
using System.Globalization;

namespace com.dinizdesenvolve.planejar.view.Movimentos
{
    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class EditeMovtoActivity : ActionBarActivity
    {
        private Lancamento mLanc;
        private ControleMovimento controle;
        private List<Categoria> categorias;
        private List<Item> itens;
        private List<Item> contas;
        private int catId;

      

        private RadioButton MovDesp;
        private RadioButton MovRec;

        private AutoCompleteTextView MovItem;
        private ImageButton MovFindItem;
        public List<MyData> SpinnerDataItens;

        private Spinner MovCategoria;
        private List<MyData> SpinnerDataCat;

        private EditText MovValor;
        private TextView MovData;
        private TextView MovDataPagto;
        private Switch MovPagto;

        private Spinner MovConta;
        private List<MyData> SpinnerDataConta;


        private appCompact mToolbar;
        string dateformat = "dd/MM/yyyy";
        private IFormatProvider mProvider = CultureInfo.InvariantCulture;

        /*DATE PICKER ******************************************************************/
       

        private void DatePickerListnerVecto(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            mLanc.dt_vcto = e.Date;
            MovData.Text = e.Date.ToString(dateformat);
        }

        private void DatePickerListnerPagto(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            mLanc.dt_pgto = e.Date;
            MovDataPagto.Text = e.Date.ToString(dateformat);
         
        }
        /******************************************************DATE PICKER */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityLancamento);

            if (new ControleCategoria().Consultar().Count() == 0) {

                new SimpleAlert(this, "Erro", "Não existem categorias cadastradas. Candastre-as antes de continuar.");
                Finish();
            }

            if (new ControleItem().GetItens("C").Count() == 0)
            {
                new SimpleAlert(this, "Erro","Não existem contas cadastradas. Candastre-as antes de continuar.");
                Finish();

            }


            int lancId = Intent.GetIntExtra("lancId", 0);

            mToolbar = FindViewById<appCompact>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            //SupportActionBar.Title = "Categorias";

            controle = new ControleMovimento();

            if (lancId != 0)
            {

                mLanc = controle.getLanc(lancId);

            }
            else
            {
                mLanc = new Lancamento();
                mLanc.dt_cad = DateTime.Now.Date;
                mLanc.dt_vcto = DateTime.Now.Date;
                mLanc.tipo = "D";
                mLanc.status = 0;
                
                
            }

            //Inicia campos e eventos 
            MovDesp = FindViewById<RadioButton>(Resource.Id.MovDespesa);
            MovDesp.CheckedChange += (o, e) =>
            {
                RadioButton rb = (RadioButton)o;
                if (e.IsChecked)
                {
                    if (rb.Id == Resource.Id.MovDespesa)
                    {
                        mLanc.tipo = "D";
                    }
                    else
                    {
                        mLanc.tipo = "R";

                    }
                }
                carregaSpinnerCategoria();
            };

            MovRec = FindViewById<RadioButton>(Resource.Id.movReceita);
            MovRec.CheckedChange += (o, e) =>
            {
                RadioButton rb = (RadioButton)o;
                if (e.IsChecked)
                {
                    if (rb.Id == Resource.Id.MovDespesa)
                    {
                        mLanc.tipo = "D";
                    }
                    else
                    {
                        mLanc.tipo = "R";

                    }
                }
                carregaSpinnerCategoria();
            };

            MovData = FindViewById<TextView>(Resource.Id.MovData);
           
            
            MovData.Click += (o,e)=> {

                Dialog dg = new DatePickerDialog(this, DatePickerListnerVecto, mLanc.dt_vcto.Year, mLanc.dt_vcto.Month -1 , mLanc.dt_vcto.Day);
                dg.Show();


            };

            MovCategoria = FindViewById<Spinner>(Resource.Id.MovCategoria);

            MovItem =  FindViewById<AutoCompleteTextView> (Resource.Id.MovItem);
            itens = new ControleItem().GetItens("N");
            //AdapterCustomItem adapter = new AdapterCustomItem(this,itens);

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                 Android.Resource.Layout.SimpleDropDownItem1Line, itens.Select(a => a.descricao).ToArray());
           
            MovItem.Adapter = adapter;
            
            MovItem.ItemSelected += (o, e) => {

                var lastMov = itens.Where(a => a.descricao.Equals(e.ToString())).FirstOrDefault().getLastMov();

                if (lastMov != null)
                {
                    if (string.IsNullOrEmpty(MovValor.Text))
                    {

                        mLanc.valor = lastMov.GetValorTit();
                    }

                    if (mLanc.cat_id == 0)
                    {
                        mLanc.cat_id = lastMov.cat_id;
                    }
                }
            };

            /*MovFindItem = FindViewById<ImageButton>(Resource.Id.MovFindItem);
            MovFindItem.Click += (ob, er) => {

                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogItem mDialogItem = new DialogItem(catId, mLanc.tipo);
                mDialogItem.Show(transaction, "Dialog Fragment");

                mDialogItem.mOnItemSelectArgs += (o, e) =>
                {
                    mLanc.item_id = e.item.id;
                    MovItem.Text = e.item.descricao;

                    carregaSpinnerCategoria();

                };
            };
            */
            MovValor = FindViewById<EditText>(Resource.Id.MovValor);
            MovConta = FindViewById<Spinner>(Resource.Id.MovConta);
            MovPagto = FindViewById<Switch>(Resource.Id.MovPagto);
            MovPagto.CheckedChange += (o, e) => {
                if (e.IsChecked)
                {
                    mLanc.status = 1;
                    MovDataPagto.Visibility = ViewStates.Visible;
                    mLanc.dt_pgto = DateTime.Now;
                    MovDataPagto.Text = ((DateTime)mLanc.dt_pgto).ToString(dateformat);
                }
                else {
                    mLanc.status = 0;
                    MovDataPagto.Visibility = ViewStates.Gone;
                    MovDataPagto.Text = null;
                }
            };
            MovDataPagto = FindViewById<TextView>(Resource.Id.MovDataPagto);

            MovDataPagto.Click += (o, e) => {

                DateTime dt = DateTime.Now;
                if (mLanc.dt_pgto != null) {
                    dt = (DateTime) mLanc.dt_pgto;
                }
                Dialog dg = new DatePickerDialog(this, DatePickerListnerPagto, dt.Year, dt.Month-1, dt.Day);
                dg.Show();
                
            };

            carregaCampos();
        }

        private void esdd(AdapterView arg1, Android.Views.View arg2, int arg3, long arg4)
        {
            throw new NotImplementedException();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_edit_cat, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void spinnerCat_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string s = spinner.GetItemAtPosition(e.Position).ToString();
            var l = SpinnerDataCat.Where(a => a.keyValues.Equals(s));
            var id = l.FirstOrDefault().getKeyInt();

            catId = id;
        }

        private void spinnerCta_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string s = spinner.GetItemAtPosition(e.Position).ToString();
            var l = SpinnerDataConta.Where(a => a.keyValues.Equals(s));
            var id = l.FirstOrDefault().getKeyInt();

            mLanc.itdebt_id = id;
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

        private void Atualizar()
        {
            if (MovValor.Text == null) {
                new SimpleAlert(this, "Alerta", "CAMPO 'Valor' é obrigatório.");
                MovValor.Focusable = true;
                return;
            }

            if (MovItem.Text == null)
            {
                new SimpleAlert(this, "Alerta", "CAMPO 'Item' é obrigatório.");
                MovItem.Focusable = true;
                return;
            }

            mLanc.cat_id = catId;

            try
            {
                Item it = itens.Where(a => a.descricao.ToUpper().Equals(MovItem.Text.ToUpper())).FirstOrDefault();

                if (it == null)
                {

                    Android.Support.V7.App.AlertDialog.Builder mDialog;
                    mDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                    mDialog.SetTitle("Alerta");
                    mDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    mDialog.SetMessage("O item informado não existe. Deseja cria-ló?");
                    mDialog.SetCancelable(false);

                    mDialog.SetPositiveButton("Sim", (o, e) =>
                    {
                        try
                        {
                            new ControleItem().Atualizar(new Item { tipo = "N", ativo = 1, descricao = MovItem.Text });

                            itens = new ControleItem().GetItens("N");

                            Atualizar();
                        }
                        catch (Exception er)
                        {
                            new SimpleAlert(this, "Erro", "Erro ao incluir item " + er.Message);
                        }


                    });

                    mDialog.SetNegativeButton("Não", (o, e) =>
                    {
                        MovItem.Focusable = true;
                        return;
                    });

                    mDialog.Create();
                    mDialog.Show();

                    return;

                }
                else {
                    mLanc.item_id = it.id;
                }
            }
            catch (Exception e) {
                new SimpleAlert(this, "Erro", "Erro ao consultar item " + e.Message);
            }

            try
            {
                mLanc.valor = Convert.ToDecimal(MovValor.Text);
            }
            catch (Exception e)
            {
                new SimpleAlert(this, "Alerta", "CAMPO 'Valor' :" + e.Message);
                return;
            }
            mLanc.dt_vcto = DateTime.ParseExact(MovData.Text, dateformat, mProvider);

            if (mLanc.status == 1)
            {
                mLanc.dt_pgto = DateTime.ParseExact(MovDataPagto.Text, dateformat, mProvider);
            }
            else
            {
                mLanc.dt_pgto = null;
                mLanc.vlr_pgto = 0;
            }
            try
            {

                controle.Atualizar(mLanc);

                Finish();
            }
            catch (Exception e) {
                new SimpleAlert(this, "Erro", e.Message);
            }
        }

        private void Deletar()
        {

            if (mLanc.id != 0)
            {

                controle.deleta(mLanc);

            }

            Finish();
        }

        private void carregaCampos()
        {

            if (mLanc.tipo == "D")
            {
                MovDesp.Selected = true;
                MovRec.Selected = false;
            }
            else
            {
                MovDesp.Selected = false;
                MovRec.Selected = true;
            }

            MovData.Text = mLanc.getDtVecto();

            carregaSpinnerCategoria();

            try
            {
                MovItem.Text = mLanc.getItem().descricao;
            }
            catch (Exception e)
            {
                MovItem.Text = "";
            }
            

            if (mLanc.GetValorTit() > 0)
            {
                MovValor.Text = mLanc.GetValorTit().ToString("#,###,###,##0.00");
            }
            else {
                MovValor.Text = null;
            }

            MovPagto.Checked = mLanc.status == 1;

            carregaSpinnerConta();

            if (mLanc.id > 0) {
                MovValor.Focusable = true;
            }

        }

        public void carregaSpinnerCategoria()
        {

            SpinnerDataCat = new List<MyData>();
            var ctCat = new ControleCategoria();
            categorias = new List<Categoria>();

            MovDesp.RefreshDrawableState();
            categorias = ctCat.Consultar().Where(a => a.getTipo(0) == mLanc.tipo).ToList();
            catId = categorias.FirstOrDefault().id;
            foreach (Categoria item in categorias)
            {
                SpinnerDataCat.Add(new MyData(Convert.ToString(item.id), item.descricao));
            }


            MovCategoria.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCat_ItemSelected);

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem
                , categorias.Select(a => a.descricao).ToArray());

            MovCategoria.Adapter = adapter;

            adapter.NotifyDataSetChanged();
            //seta combo box com o valor correto
            try
            {
                if (mLanc.cat_id != 0)
                {
                    for (int x = 0; x < SpinnerDataCat.Count(); x++)
                    {
                        if (SpinnerDataCat[x].getKeyInt() == mLanc.cat_id)
                        {
                            MovCategoria.SetSelection(x);
                            catId = mLanc.cat_id;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MovCategoria.SetSelection(0);
            }

        }


        public void carregaSpinnerConta()
        {

            SpinnerDataConta = new List<MyData>();

            try
            {
                MovConta.DestroyDrawingCache();
            }
            catch (Exception e)
            {

            }

            contas = new ControleCategoria().getContas();
            foreach (Item it in contas)
            {
                SpinnerDataConta.Add(new MyData(Convert.ToString(it.id), it.descricao));
            }

            MovConta.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCta_ItemSelected);

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem
                , contas.Select(a => a.descricao).ToArray());

            MovConta.Adapter = adapter;
            adapter.NotifyDataSetChanged();

            //seta combo box com o valor correto
            if (mLanc.itdebt_id != 0)
            {
                for (int x = 0; x < SpinnerDataConta.Count(); x++)
                {
                    if (SpinnerDataConta[x].getKeyInt() == mLanc.itdebt_id)
                    {
                        MovConta.SetSelection(x);
                    }
                }
            }
            //            MovConta.NotifyAll();

        }
    }
}