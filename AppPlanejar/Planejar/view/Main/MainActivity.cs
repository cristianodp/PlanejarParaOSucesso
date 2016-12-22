using Android.App;
using Android.Widget;
using Android.OS;
using Controls;
using Android.Content;
using com.dinizdesenvolve.planejar.Views.Usuario;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using com.dinizdesenvolve.planejar.Views.Main;
using Android.Support.V4.Widget;
using Android.Views;
using System.Collections.Generic;
using com.dinizdesenvolve.planejar.Views.Main.Menu;
using com.dinizdesenvolve.planejar.view.Resumo;
using SupportFragment = Android.Support.V4.App.Fragment;
using com.dinizdesenvolve.planejar.view.Contas;
using com.dinizdesenvolve.planejar.view.Movimentos;
using com.dinizdesenvolve.planejar.view.Categorias;
using com.dinizdesenvolve.planejar.view.Planejamento;
using SharedPlanejar.Utils;
using Java.Lang;
using com.dinizdesenvolve.planejar.view.Itens;


namespace com.dinizdesenvolve.planejar.View
{

    [Activity(Label = "Planejar", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : ActionBarActivity
    {
        private ControlPrincipal controle;
        private int aux = 0;

        private SupportToolbar myToolBar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;

        private AdapterMenu mLeftAdapter;
        private List<string> mLeftDataSet;

        private FrameLayout mFragmentContainer;
        private SupportFragment mCurrentFragment;

        private ResumoFrag mResumoFrag;
        private ContasFrag mContasFrag;
        private MovimentosFrag mMovimentosFrag;
        private CategoriasFrag mCategoriasFrag;
        private PlanejamentoFrag mPlanejamentoFrag;
        private ItensFrag mItensFrag;
        

        private Stack<SupportFragment> mStackFragments;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

           SetContentView(Resource.Layout.Main);
          
           controle = new ControlPrincipal();
           myToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
           mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
           mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
           mFragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);

            SetSupportActionBar(myToolBar);

            mLeftDataSet = new List<string>();

            mLeftDataSet.AddRange(Resources.GetStringArray(Resource.Array.listaMenu));

            mLeftAdapter = new AdapterMenu(this, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            mLeftDrawer.ItemClick += mListViewClick;

            mDrawerToggle = new MyActionBarDrawerToggle(this,
                mDrawerLayout,
                Resource.String.openDrawer,
                Resource.String.closeDrawer);

            mDrawerLayout.SetDrawerListener(mDrawerToggle);

            //SupportActionBar.SetIcon(Resource.Drawable.Icon);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayUseLogoEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            mDrawerToggle.SyncState();
            
            carregaFragment();
            


        }

        private void carregaFragment()
        {
            //mFragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);

            if (mResumoFrag != null) {
                return;
            }

            mResumoFrag = new ResumoFrag();
            mContasFrag = new ContasFrag();
            mMovimentosFrag = new MovimentosFrag();
            mCategoriasFrag = new CategoriasFrag();
            mPlanejamentoFrag = new PlanejamentoFrag();
            mItensFrag = new ItensFrag();

            mStackFragments = new Stack<SupportFragment>();
            var trans = SupportFragmentManager.BeginTransaction();

            trans.Add(Resource.Id.fragmentContainer, mContasFrag, "Contas");
            trans.Hide(mContasFrag);

            trans.Add(Resource.Id.fragmentContainer, mMovimentosFrag, "Movimentos");
            trans.Hide(mMovimentosFrag);

            trans.Add(Resource.Id.fragmentContainer, mCategoriasFrag, "Categorias");
            trans.Hide(mCategoriasFrag);

            trans.Add(Resource.Id.fragmentContainer, mItensFrag, "Itens");
            trans.Hide(mItensFrag);

            /*trans.Add(Resource.Id.fragmentContainer, mPlanejamentoFrag, "Planejamento");
             trans.Hide(mPlanejamentoFrag);
            */
            trans.Add(Resource.Id.fragmentContainer, mResumoFrag, "Resumo");
            trans.Commit();

            mCurrentFragment = mResumoFrag;

        }

        private void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = SupportFragmentManager.BeginTransaction();

            //trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out);

            fragment.View.BringToFront();
            mCurrentFragment.View.BringToFront();

            trans.Hide(mCurrentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            mStackFragments.Push(mCurrentFragment);
            trans.Commit();

            mCurrentFragment = fragment;
        }

        private void mListViewClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (mLeftDataSet[e.Position].ToString().Equals("Resumo")){
                ShowFragment(mResumoFrag);
            }

            if (mLeftDataSet[e.Position].ToString().Equals("Contas")){
                ShowFragment(mContasFrag);
            }

            if (mLeftDataSet[e.Position].ToString().Equals("Movimentos"))
            {
                ShowFragment(mMovimentosFrag);
            }

            if (mLeftDataSet[e.Position].ToString().Equals("Categorias"))
            {
                ShowFragment(mCategoriasFrag);
            }

            if (mLeftDataSet[e.Position].ToString().Equals("Itens"))
            {
                ShowFragment(mItensFrag);
            }


            /*  if (mLeftDataSet[e.Position].ToString().Equals("Planejamento"))
              {
                  ShowFragment(mPlanejamentoFrag);
              }
              */

            //mDrawerToggle.OnDrawerClosed(mDrawerLayout);

            mDrawerLayout.CloseDrawer(mLeftDrawer);


        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!controle.isLogado()  && (aux ==1))
            {
                Finish();

            }else if (!controle.isLogado() && (aux == 0))
            {
                aux = 1;
                var intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }

            


        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }



       
    };

}