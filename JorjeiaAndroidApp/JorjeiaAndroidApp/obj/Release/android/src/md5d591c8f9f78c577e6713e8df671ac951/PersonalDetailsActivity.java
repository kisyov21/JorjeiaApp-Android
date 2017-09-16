package md5d591c8f9f78c577e6713e8df671ac951;


public class PersonalDetailsActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"";
		mono.android.Runtime.register ("JorjeiaAndroidApp.PersonalDetailsActivity, JorjeiaAndroidApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PersonalDetailsActivity.class, __md_methods);
	}


	public PersonalDetailsActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PersonalDetailsActivity.class)
			mono.android.TypeManager.Activate ("JorjeiaAndroidApp.PersonalDetailsActivity, JorjeiaAndroidApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();

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
