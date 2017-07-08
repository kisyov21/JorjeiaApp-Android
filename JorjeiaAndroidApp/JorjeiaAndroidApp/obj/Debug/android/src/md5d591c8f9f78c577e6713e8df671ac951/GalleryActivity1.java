package md5d591c8f9f78c577e6713e8df671ac951;


public class GalleryActivity1
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("JorjeiaAndroidApp.GalleryActivity1, JorjeiaAndroidApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GalleryActivity1.class, __md_methods);
	}


	public GalleryActivity1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GalleryActivity1.class)
			mono.android.TypeManager.Activate ("JorjeiaAndroidApp.GalleryActivity1, JorjeiaAndroidApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
