package md59f6415ffda7f5b80022979c2248095ba;


public class WebAppInterface
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getTitle:(I)Ljava/lang/String;:__export__\n" +
			"n_getValue:(Ljava/lang/String;)D:__export__\n" +
			"n_getValueJson:()Ljava/lang/String;:__export__\n" +
			"";
		mono.android.Runtime.register ("com.dinizdesenvolve.planejar.view.WebAppInterface, com.dinizdesenvolve.planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WebAppInterface.class, __md_methods);
	}


	public WebAppInterface () throws java.lang.Throwable
	{
		super ();
		if (getClass () == WebAppInterface.class)
			mono.android.TypeManager.Activate ("com.dinizdesenvolve.planejar.view.WebAppInterface, com.dinizdesenvolve.planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public WebAppInterface (java.lang.String p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == WebAppInterface.class)
			mono.android.TypeManager.Activate ("com.dinizdesenvolve.planejar.view.WebAppInterface, com.dinizdesenvolve.planejar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}

	@android.webkit.JavascriptInterface

	public java.lang.String getTitle (int p0)
	{
		return n_getTitle (p0);
	}

	private native java.lang.String n_getTitle (int p0);

	@android.webkit.JavascriptInterface

	public double getValue (java.lang.String p0)
	{
		return n_getValue (p0);
	}

	private native double n_getValue (java.lang.String p0);

	@android.webkit.JavascriptInterface

	public java.lang.String getValueJson ()
	{
		return n_getValueJson ();
	}

	private native java.lang.String n_getValueJson ();

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
