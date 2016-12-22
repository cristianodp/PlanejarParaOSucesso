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

namespace com.dinizdesenvolve.planejar.view.Itens
{
    [Activity(Label = "Planejar", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class EditeItensActivity : ActionBarActivity
    {
        private Item mItem;
        private ControleItem controle;

        private Button ItCatBtOK;
        private Button ItCatBtCanc;

        private EditText ItCatEdTxtDesc;
        private ToggleButton ItCatTgBtAtivo;
        
        private appCompactToobar mToolbar;
        //private int CatId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityCatItemEdite);
          //  CatId = Intent.GetIntExtra("CatId", 0);
            int ItCatId = Intent.GetIntExtra("itemId", 0);

            mToolbar = FindViewById<appCompactToobar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            //SupportActionBar.Title = "Categorias";

            controle = new ControleItem();

            if (ItCatId != 0)
            {

                mItem = controle.GetItem(ItCatId);

            }
            else
            {
                mItem = new Item();
                mItem.ativo = 1;
                mItem.tipo = "N";
                //mItem.cat_id = CatId;
                mItem.cor = Resources.GetColor(Resource.Color.colorAtivo);

            }

            ItCatEdTxtDesc = FindViewById<EditText>(Resource.Id.ItCatEdTxtDesc);
            //            CatTxtVwCor = FindViewById<TextView>(Resource.Id.CatTxtVwCor);


            ItCatTgBtAtivo = FindViewById<ToggleButton>(Resource.Id.ItCatTgBtAtivo);
            ItCatTgBtAtivo.Click += (o, e) => {
                // Perform action on clicks
                if (ItCatTgBtAtivo.Checked)
                    mItem.ativo = 1;
                else
                    mItem.ativo = 0;
            };

            carregaCampos();
        }

        private void atualizaItemCategoria()
        {
            mItem.descricao = ItCatEdTxtDesc.Text;

            controle.Atualizar(mItem);

            Finish();
        }

        private void delataItemCategoria()
        {

            if (mItem.id != 0){
                controle.Delete(mItem);
            }

            Finish();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_edit_cat, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void carregaCampos()
        {

            ItCatEdTxtDesc.Text = mItem.descricao;

            ItCatTgBtAtivo.Checked = (mItem.ativo == 1);
            ItCatTgBtAtivo.RefreshDrawableState();

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

                        delataItemCategoria();
                    }
                    break;
                case (Resource.Id.menu_edcat_Salvar):
                    {
                        atualizaItemCategoria();
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