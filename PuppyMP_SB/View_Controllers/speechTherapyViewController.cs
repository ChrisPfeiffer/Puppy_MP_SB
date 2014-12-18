﻿using System;
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
		//main in app purchasing object
		InAppPurchaseManager iap;
		//observers
		NSObject succeededObserver, requestObserver,failedObserver, priceObserver, noInterNetObserver;
		//product keys
		NSString NSProdID = new NSString("Fronting"); NSString keyCR = new NSString("com.chrispcodes.pmp.clusterreduction");
		NSString keyFCD = new NSString("com.chrispcodes.pmp.finalconsonantdeletion"); NSString keyStopping = new NSString("com.chrispcodes.pmp.stopping");
		NSString keyGliding = new NSString ("com.chrispcodes.pmp.gliding"); NSString keyMulti = new NSString("com.chrispcodes.pmp.multi");
		NSString keySD = new NSString("com.chrispcodes.pmp.stridencydeletion"); NSString keyPre = new NSString("com.chrispcodes.pmp.prevocalic");
		NSString keyPost = new NSString("com.chrispcodes.pmp.postvocalic");
		//product objects
		SKProduct frontingProduct = new SKProduct(); SKProduct crProduct = new SKProduct(); SKProduct fcdProduct = new SKProduct();
		SKProduct stoppingProduct = new SKProduct(); SKProduct glidingProduct = new SKProduct(); SKProduct multiProduct = new SKProduct(); SKProduct stridencyProduct = new SKProduct();
		SKProduct preProduct = new SKProduct(); SKProduct postProduct = new SKProduct();

		//event handlers
		EventHandler handlerFronting, handlerCR, handlerFCD, handlerStopping, handlerGliding, handlerMulti, handlerStridency, handlerPre, handlerPost;

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

			//initialize buttons, buttons will be disabled until prices load or the purchase is checked
			btnFronting.Enabled = false; btnCluster.Enabled = false; btnFinalConsonant.Enabled = false;
			btnStopping.Enabled = false; btnGliding.Enabled = false; btnCluster.Enabled = false; btnStridency.Enabled = false; btnPreVoicing.Enabled = false;
			btnPostVoicing.Enabled = false; btnMulti.Enabled = false;


			handlerFronting = (s, e) => iap.PurchaseProduct (frontingProduct);
			handlerCR = (s, e) => iap.PurchaseProduct (crProduct); 
			handlerFCD=(s,e)=>iap.PurchaseProduct(fcdProduct);  handlerStopping=(s,e)=>iap.PurchaseProduct(stoppingProduct); 
			handlerGliding=(s,e)=>iap.PurchaseProduct(glidingProduct);  handlerMulti=(s,e)=>iap.PurchaseProduct(multiProduct);  
			handlerPre=(s,e)=>iap.PurchaseProduct(preProduct);  handlerPost=(s,e)=>iap.PurchaseProduct(postProduct); handlerStridency=(s,e)=>iap.PurchaseProduct(stridencyProduct);

			buttonCheck ();

			//style the buttons
			styleButton (this.btnPreVoicing); styleButton (btnMulti); styleButton (btnFinalConsonant);
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
				//assign product ID's
				frontingProduct = (SKProduct) info.ObjectForKey(NSProdID); fcdProduct=(SKProduct) info.ObjectForKey(keyFCD); stoppingProduct = (SKProduct) info.ObjectForKey(keyStopping);
				glidingProduct = (SKProduct) info.ObjectForKey(keyGliding); multiProduct = (SKProduct) info.ObjectForKey(keyMulti); stridencyProduct = (SKProduct) info.ObjectForKey(keySD);
				preProduct = (SKProduct) info.ObjectForKey(keyPre); postProduct = (SKProduct) info.ObjectForKey(keyPost); crProduct = (SKProduct) (SKProduct) info.ObjectForKey(keyCR);

				//set button titles
				if(!iapHelpers.hasPurchased("fronting"))
				{
					this.btnFronting.SetTitle("Fronting - "+frontingProduct.Price.ToString(), UIControlState.Normal);
					this.btnFronting.Enabled = true;
					this.btnFronting.TouchUpInside+=handlerFronting;
				}

				if(!iapHelpers.hasPurchased("cluster_reduction"))
				{
					this.btnCluster.SetTitle("Cluster Reduction - " +  crProduct.Price.ToString(), UIControlState.Normal);
					this.btnCluster.Enabled = true;
					this.btnCluster.TouchUpInside+=handlerCR;
				}
				if(!iapHelpers.hasPurchased("stopping"))
				{
					this.btnStopping.SetTitle("Stopping - " + stoppingProduct.Price.ToString(), UIControlState.Normal);
					this.btnStopping.Enabled = true;
					this.btnStopping.TouchUpInside+=handlerStopping;
				}
				if(!iapHelpers.hasPurchased("postvocalic"))
				{
					this.btnPostVoicing.SetTitle("Post-Vocalic Voicing - " + postProduct.Price.ToString(), UIControlState.Normal);
					this.btnPostVoicing.Enabled = true;
					this.btnPostVoicing.TouchUpInside +=handlerPost;
				}
				if(!iapHelpers.hasPurchased("gliding"))
				{
					this.btnGliding.SetTitle("Gliding = " + glidingProduct.Price.ToString(), UIControlState.Normal);
					this.btnGliding.Enabled = true;
					this.btnGliding.TouchUpInside+=handlerGliding;
				}
				if(!iapHelpers.hasPurchased("prevocalic"))
				{
					this.btnPreVoicing.SetTitle("Pre-Vocalic Voicing - " + preProduct.Price.ToString(),UIControlState.Normal);
					this.btnPreVoicing.Enabled = true;
					this.btnPreVoicing.TouchUpInside +=handlerPre;
				}
				if(!iapHelpers.hasPurchased("stridency_deletion"))
				{
					this.btnStridency.SetTitle("Stridency Deletion - " + stridencyProduct.Price.ToString(), UIControlState.Normal);
					this.btnStridency.Enabled = true;
					this.btnStridency.TouchUpInside +=handlerStridency;
				}
				if(!iapHelpers.hasPurchased("multi"))
				{
					this.btnMulti.SetTitle("Multi-Syllabalic Words - " + multiProduct.Price.ToString(), UIControlState.Normal);
					this.btnMulti.Enabled = true;
					this.btnMulti.TouchUpInside+=handlerMulti;
				}
				if(!iapHelpers.hasPurchased("final_consonant"))
				{
					this.btnFinalConsonant.SetTitle("Final Consonant Deletion - " + fcdProduct.Price.ToString(), UIControlState.Normal);
					this.btnFinalConsonant.Enabled = true;
					this.btnFinalConsonant.TouchUpInside+=handlerFCD;
				}



				

				Console.WriteLine("here");
			});

			succeededObserver = NSNotificationCenter.DefaultCenter.AddObserver (InAppPurchaseManager.InAppPurchaseManagerTransactionSucceededNotification, 
				(notification) => {
					// update the buttons after a successful purchase
					buttonCheck();
				});
			requestObserver = NSNotificationCenter.DefaultCenter.AddObserver (InAppPurchaseManager.InAppPurchaseManagerRequestFailedNotification, 
				(notification) => {

					UIAlertView alert = new UIAlertView("App Store Error", "There was a problem connecting to the App Store.  Feel free to utilize items which have already been purchased",null,"OK",null);
					alert.Show();
				});

			failedObserver = NSNotificationCenter.DefaultCenter.AddObserver(InAppPurchaseManager.InAppPurchaseManagerTransactionFailedNotification, (notification)=>{
				UIAlertView alert2 = new UIAlertView("Transaction Failed","The purchase was unsuccessful.  You will not be charged.  Please make sure you are properly connected to the App Store and try again",null,"OK",null);
				alert2.Show();
			});

			noInterNetObserver = NSNotificationCenter.DefaultCenter.AddObserver (InAppPurchaseManager.NoInternetNotification, (notification) => {
				UIAlertView noNetAlert = new UIAlertView("Connection Lost", "There seems to be an issue with your internet connection.  Please try purchase again with a better connection",null,"OK",null);
				noNetAlert.Show();
			});
					
			iap = new InAppPurchaseManager ();
			List<String> allProdStrings = iapHelpers.getAllKeys ();
			iap.RequestProductData (allProdStrings);
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

		private void styleButton(UIButton btn)
		{
			btn.BackgroundColor = UIColor.FromRGB (0, 122, 255);
			btn.SetTitleColor(UIColor.White,UIControlState.Normal);
			btn.Layer.BorderWidth = 1.0f;
			btn.Layer.BorderColor = UIColor.White.CGColor;
			btn.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
		}


		private void buttonCheck()
		{
			if (iapHelpers.hasPurchased ("fronting")) {
				btnFronting.Enabled = true;
				btnFronting.TouchUpInside -= handlerFronting;
				btnFronting.SetTitle ("Fronting",UIControlState.Normal);

			} 
			if (iapHelpers.hasPurchased ("cluster_reduction")) {
				btnCluster.Enabled = true;
				btnCluster.TouchUpInside -= handlerCR;
				btnCluster.SetTitle ("Cluster Reduction",UIControlState.Normal);
			}
			if (iapHelpers.hasPurchased ("stopping")) {
				btnStopping.Enabled = true;
				btnStopping.TouchUpInside -= handlerStopping;
				btnStopping.SetTitle ("Stopping",UIControlState.Normal);
			}
			if (iapHelpers.hasPurchased ("gliding")) {
				btnGliding.Enabled = true;
				btnGliding.TouchUpInside -= handlerGliding;
				btnGliding.SetTitle ("Gliding",UIControlState.Normal);
			}
			if (iapHelpers.hasPurchased ("multi")) {
				btnMulti.Enabled = true;
				btnMulti.TouchUpInside -= handlerMulti;
				btnMulti.SetTitle ("Multi-Syllabalic Words", UIControlState.Normal);
			}
			if (iapHelpers.hasPurchased ("stridency_deletion")) {
				btnStridency.Enabled = true;
				btnStridency.TouchUpInside -= handlerStridency;
				btnStridency.SetTitle ("Stridency Deletion", UIControlState.Normal);
			}
			if (iapHelpers.hasPurchased ("prevocalic")) {
				btnPreVoicing.Enabled = true;
				btnPreVoicing.TouchUpInside -= handlerPre;
				btnPreVoicing.SetTitle ("Pre-Vocalic Voicing", UIControlState.Normal);
			}
			if (iapHelpers.hasPurchased ("postvocalic")) {
				btnPostVoicing.Enabled = true;
				btnPostVoicing.TouchUpInside -= handlerPost;
				btnPostVoicing.SetTitle ("Pre-Vocalic Voicing", UIControlState.Normal);
			}
		}
			

		public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
		{
			//in order for this to work, all segue names must match product names.


			if (segueIdentifier == "initial_consonant") {
				return true;
			}

			if (iapHelpers.hasPurchased (segueIdentifier)) {
					return true;
				}

			return false;
		}
		#endregion
	}
}

