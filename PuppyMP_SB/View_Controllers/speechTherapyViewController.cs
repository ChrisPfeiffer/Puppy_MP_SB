using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Xml;
using SharedPCL;
using System.Collections.Generic;

namespace PuppyMP_SB
{
	public partial class speechTherapyViewController : UIViewController
	{
		//static bool UserInterfaceIdiomIsPhone {
		//get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		//}

		private Dictionary<String, List<Pair>> pairLists = new Dictionary<string, List<Pair>> ();

		public speechTherapyViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			//create an xmlreader to pass so it can be parsed
			XmlReader reader = XmlReader.Create("Shared_Content/Pairs.xml");

			imageParser parser = new imageParser ();

			pairLists = parser.parsePairs (reader);

			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject Sender)
		{
			base.PrepareForSegue (segue, Sender);
			//based on segue, pass appropriate list to either position choice or image displayer


			if (segue.Identifier == "initial_consonant") {

				var initialVC = segue.DestinationViewController as imagePgViewController;
				initialVC.setPairList (this, pairLists ["Initial Consonant Deletion"]);
			} else if (segue.Identifier == "final_consonant") {
				var finalVC = segue.DestinationViewController as imagePgViewController;
				finalVC.setPairList (this, pairLists ["Final Consonant Deletion"]);
			} else if (segue.Identifier == "cluster_reduction") {
				var clusterVC = segue.DestinationViewController as PositionViewController;
				clusterVC.setPairList (this, pairLists ["Cluster Reduction"]);
			} else if (segue.Identifier == "stopping") {
				var stopVC = segue.DestinationViewController as PositionViewController;
				stopVC.setPairList (this, pairLists ["Stopping"]);
			} else if (segue.Identifier == "syllable_deletion") {
				var sylDelVC = segue.DestinationViewController as imagePgViewController;
				sylDelVC.setPairList (this, pairLists ["Syllable Deletion"]);
			} else if (segue.Identifier == "fronting") {
				var frontVC = segue.DestinationViewController as PositionViewController;
				frontVC.setPairList (this, pairLists ["Fronting"]);
			}
		}

		#endregion
	}
}

