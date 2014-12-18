using System;
using MonoTouch.StoreKit;
using MonoTouch.Foundation;
using System.Collections.Generic;


namespace speechTherapy
{
	public class iapHelpers
	{
		public iapHelpers ()
		{

		}

		public static void reflectPurchase(String product)
		{
			//todo: add full logic here.
			Dictionary<String, String> prods = getAllProductIDs ();
			NSUserDefaults.StandardUserDefaults.SetBool (true, prods[product]);
			NSUserDefaults.StandardUserDefaults.Synchronize ();
		}

		public static bool hasPurchased(String product)
		{
			var key = new NSString (product);
			return NSUserDefaults.StandardUserDefaults.BoolForKey (key);
		}

		public static Dictionary<String, String> getAllProductIDs()
		{
			Dictionary<String, String> prodIDs = new Dictionary<String, String> ();

			prodIDs.Add ("com.chrispcodes.pmp.clusterreduction", "cluster_reduction");
			prodIDs.Add ("Fronting", "fronting");
			prodIDs.Add ("com.chrispcodes.pmp.stopping", "stopping");
			prodIDs.Add ("com.chrispcodes.pmp.gliding", "gliding");
			prodIDs.Add ("com.chrispcodes.pmp.multi", "multi");
			prodIDs.Add ("com.chrispcodes.pmp.stridencydeletion", "stridency_deletion");
			prodIDs.Add ("com.chrispcodes.pmp.prevocalic", "prevocalic");
			prodIDs.Add ("com.chrispcodes.pmp.postvocalic", "postvocalic");
			prodIDs.Add ("com.chrispcodes.pmp.finalconsonantdeletion", "final_consonant");

			return prodIDs;

		}

		public static List<String> getAllKeys()
		{
			Dictionary<String, String> dict = new Dictionary<String, String> ();
			List<String> allKeys = new List<String> ();

			dict = getAllProductIDs ();

			foreach (String key in dict.Keys) {
				allKeys.Add (key);
			}

			return allKeys;
		}
}
}
