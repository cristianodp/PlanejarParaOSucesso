package md571e82ea8ed9253b0b112b003bd95d2d7;


public class ParChart
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getNum1:()I:__export__\n" +
			"";
		mono.android.Runtime.register ("Planejar.view.Categorias.ParChart, Planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ParChart.class, __md_methods);
	}


	public ParChart () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ParChart.class)
			mono.android.TypeManager.Activate ("Planejar.view.Categorias.ParChart, Planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ParChart (int p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ParChart.class)
			mono.android.TypeManager.Activate ("Planejar.view.Categorias.ParChart, Planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}

	@android.webkit.JavascriptInterface

	public int getNum1 ()
	{
		return n_getNum1 ();
	}

	private native int n_getNum1 ();

	java.util.ArrayList refList;
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
