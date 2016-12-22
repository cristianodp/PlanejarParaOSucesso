using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using SharedPlanejar.Models;
using Controls;
using Utils;

namespace com.dinizdesenvolve.planejar.view.Movimentos
{
    public class OnItemSelectArgs : EventArgs
    {
        private Item mItem;

        public Item item {

            get { return mItem; }
            set { mItem = value; }
        }

        public OnItemSelectArgs(Item it) : base()
        {
            item = it;
        }
    }

    class DialogItem : DialogFragment
    {
        private List<Item> mItens;
        private List<Categoria> mCategorias;
        private List<MyData> SpinnerDataCat;
        private string mTipo;
        private Context mContext;
        private int catId;

        private Spinner mSpinnerCat;
        private Switch mSwitchTodos;
        private EditText Editfind;
        private ListView mListView;
        private Button mButtonOK;
        private AdapterCustomItem mAdapter;

        public event EventHandler<OnItemSelectArgs> mOnItemSelectArgs;

        public DialogItem(int cat_id, string tipo)
        {
            // this.mContext = context;
            this.catId = cat_id;
            this.mTipo = tipo;
        }


        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //base.OnCreateView(inflater, container, savedInstanceState);

            Android.Views.View view = inflater.Inflate(Resource.Layout.DialogFragItem, container, false);

            mContext = view.Context;

            mSpinnerCat = view.FindViewById<Spinner>(Resource.Id.DialogItemListCat);
            mSwitchTodos = view.FindViewById<Switch>(Resource.Id.DialogItemTodos);
            Editfind = view.FindViewById<EditText>(Resource.Id.DialogItemSearch);
            mListView = view.FindViewById<ListView>(Resource.Id.DialogItemList);
            mButtonOK = view.FindViewById<Button>(Resource.Id.DailogItemBtOk);
            mButtonOK.Click += delegate
            {
                Dismiss();
            };

            mItens = new List<Item>();
            mAdapter = new AdapterCustomItem(mContext, mItens);
            mListView.Adapter = mAdapter;
            mListView.ItemClick += mListViewLingClick;

            Editfind.KeyPress += mKeyPress; 
            
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            carregaSpinnerCategoria();
        }

        public void carregaSpinnerCategoria()
        {

            SpinnerDataCat = new List<MyData>();
            var ctCat = new ControleCategoria();
            
            mCategorias = new List<Categoria>();
            mCategorias = ctCat.Consultar();
           
            foreach (Categoria item in mCategorias)
            {
                SpinnerDataCat.Add(new MyData(Convert.ToString(item.id), item.descricao));
            }


            mSpinnerCat.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCat_ItemSelected);

            ArrayAdapter adapter = new ArrayAdapter(mContext, Android.Resource.Layout.SimpleSpinnerItem
                , mCategorias.Select(a => a.descricao).ToArray());

            mSpinnerCat.Adapter = adapter;

            //seta combo box com o valor correto
            try
            {
                if (catId != 0)
                {
                    for (int x = 0; x < SpinnerDataCat.Count(); x++)
                    {
                        if (SpinnerDataCat[x].getKeyInt() == catId)
                        {
                            mSpinnerCat.SetSelection(x);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                mSpinnerCat.SetSelection(0);
            }


        }

        private void spinnerCat_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string s = spinner.GetItemAtPosition(e.Position).ToString();
            var l = SpinnerDataCat.Where(a => a.keyValues.Equals(s));
            var id = l.FirstOrDefault().getKeyInt();

            catId = id;
        }

        private void mListViewLingClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Item it = mAdapter[e.Position];

            mOnItemSelectArgs.Invoke(mContext, new OnItemSelectArgs(it));
            this.Dismiss();
            
        }

        private void mKeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            e.Handled = false;

            EditText edit = (EditText) sender;

            String s = edit.Text;

            if (s.Length > 0) {
                mItens = new ControleItem().GetItens("N").Where(a=>a.descricao.ToUpper().Contains(s)).ToList();
                mAdapter = new AdapterCustomItem(mContext, mItens);
                mListView.Adapter = mAdapter;
            }
            
        }


        

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.Dialog_animation;
        }


    }
}