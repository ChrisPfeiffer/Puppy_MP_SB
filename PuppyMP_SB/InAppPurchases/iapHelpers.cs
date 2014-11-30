using System;
using MonoTouch.StoreKit;
using MonoTouch.Foundation;



namespace speechTherapy
{
	public class iapHelpers
	{
		public iapHelpers ()
		{

		}

		public static void reflectPurchase(String product)
		{
			var key = new NSString (product);
			NSUserDefaults.StandardUserDefaults.SetBool (true, key);
			NSUserDefaults.StandardUserDefaults.Synchronize ();
		}

		public static bool hasPurchased(String product)
		{
			var key = new NSString (product);
			return NSUserDefaults.StandardUserDefaults.BoolForKey (key);
		}

		//this method should be removed.  For testing purposes only.
		public static void resetPurchasing()
		{
			//resets.  tests restoring/re-doing transactions.
			NSUserDefaults.StandardUserDefaults.SetBool (false, "Fronting");
		}
	}
}

