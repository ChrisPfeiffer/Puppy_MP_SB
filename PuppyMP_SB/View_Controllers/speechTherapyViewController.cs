using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.StoreKit;
using System.Xml;
using SharedPCL;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.CoreGraphics;


namespace speechTherapy
{
	public partial class speechTherapyViewController : UIViewController
	{
		//static bool UserInterfaceIdiomIsPhone {
		//get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		//}

		InAppPurchaseManager iap;
		NSObject priceObserver;
		NSString NSProdID = new NSString("Fronting");
		NSObject succeededObserver, failedObserver;
		SKProduct frontingProduct = new SKProduct();


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

			//style the buttons
			styleButton (this.btnPreVoicing); styleButton (btnDeaffrication); styleButton (btnFinalConsonant);
			styleButton (this.btnFronting); styleButton (btnInitialConsonant); styleButton (btnPostVoicing);
			styleButton (this.btnPreVoicing); styleButton (btnStopping); styleButton (btnStridency);
			styleButton (btnGliding); styleButton (btnCluster);


			//create an xmlreader to pass so it can be parsed
			XmlReader reader = XmlReader.Create("Shared_Content/Pairs2.xml");

			imageParser parser = new imageParser ();

			pairLists = parser.parsePairs (reader);



			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			priceObserver = NSNotificationCenter.DefaultCenter.AddObserver (InAppPurchaseManager.InAppPurchaseManagerProductsFetchedNotification, (notification) => {
				//the code that will run when product info is retrieved from apple
				var info = notification.UserInfo;
				frontingProduct = (SKProduct) info.ObjectForKey(NSProdID);

				this.buyFronting.SetTitle("Fronting - "+frontingProduct.Price.ToString(), UIControlState.Normal);

				Console.WriteLine("here");
			});

			updateButtons ();

			priceObserver = NSNotificationCenter.DefaultCenter.AddObserver (InAppPurchaseManager.InAppPurchaseManagerTransactionSucceededNotification, 
				(notification) => {
					// update the buttons after a successful purchase
					updateButtons ();
				});
					
			//event handlers for buttons
			buyFronting.TouchUpInside += (sender, e) => {iap.PurchaseProduct(frontingProduct);};


			//request the info, such as prices from apple server
			List<String> iapProducts = new List<String> (){"Fronting"};
			iap = new InAppPurchaseManager ();
			iap.RequestProductData (iapProducts);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			NSNotificationCenter.DefaultCenter.RemoveObserver (priceObserver);
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

		private void updateButtons()
		{
			//this.buyFronting.Hidden = iapHelpers.hasPurchased ("Fronting");
			//this.btnFronting.Hidden = !iapHelpers.hasPurchased ("Fronting");
		}


		private void styleButton(UIButton btn)
		{
			btn.BackgroundColor = UIColor.FromRGB (0, 122, 255);
			btn.SetTitleColor(UIColor.White,UIControlState.Normal);
			btn.Layer.BorderWidth = 1.0f;
			btn.Layer.BorderColor = UIColor.White.CGColor;
		}
		#endregion
	}
}

