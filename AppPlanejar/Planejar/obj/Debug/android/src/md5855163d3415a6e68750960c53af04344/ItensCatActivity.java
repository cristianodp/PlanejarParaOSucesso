package md5855163d3415a6e68750960c53af04344;


public class ItensCatActivity
	extends android.support.v7.app.ActionBarActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"";
		mono.android.Runtime.register ("com.dinizdesenvolve.planejar.view.Itens.ItensCatActivity, com.dinizdesenvolve.planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ItensCatActivity.class, __md_methods);
	}


	public ItensCatActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ItensCatActivity.class)
			mono.android.TypeManager.Activate ("com.dinizdesenvolve.planejar.view.Itens.ItensCatActivity, com.dinizdesenvolve.planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
