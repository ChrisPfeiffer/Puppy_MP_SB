using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Xml;
using SharedPCL;
using System.Collections.Generic;
using System.Linq;

namespace speechTherapy
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
			XmlReader reader = XmlReader.Create("Shared_Content/Pairs2.xml");

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
			List<Pair> listToPass = new List<Pair>();
			listToPass = pairLists[segue.Identifier];

			var pairsWithPositions = listToPass.Where (p => p.position != null);

			if (pairsWithPositions.Count() == listToPass.Count) {
				//pass it to the positioncontroller
				var posVC = segue.DestinationViewController as PositionViewController;
				posVC.setPairList (this,listToPass);
			} else {

				var imageVC = segue.DestinationViewController as imagePgViewController;
				imageVC.setPairList (this,listToPass);
			}


		}

		#endregion
	}
}

