using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

using MonoTouch.CoreVideo;
using MonoTouch.CoreMedia;
using MonoTouch.AVFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreFoundation;
using System.Runtime.InteropServices;



namespace PuppyMP_SB
{
	public partial class videoPlayController : UIViewController
	{
		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;
		NSUrl url;
		Boolean isPlaying;


		public videoPlayController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			initializeVideo ();
			playVideo ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void playVideo()
		{
			View.Layer.AddSublayer (_playerLayer);
			UIView cameraView = new UIView ();
			this.View.AddSubview (cameraView);
			this.View.SendSubviewToBack (cameraView);
			cameraView.Layer.AddSublayer (_playerLayer);

			isPlaying = true;
			_player.Play ();

		}

		private void initializeVideo()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var library = System.IO.Path.Combine (documents, "..", "Library");
			var urlpath = System.IO.Path.Combine (library, "sweetMovieFilm.mov");

			url = new NSUrl (urlpath, false);

			_asset = AVAsset.FromUrl(url);
			_playerItem = new AVPlayerItem (_asset);
			_player = new AVPlayer (_playerItem);

			_playerLayer = AVPlayerLayer.FromPlayer (_player);
			_playerLayer.Frame = View.Frame;


		}

		public override void ViewWillDisappear (bool animated)
		{
			foreach (var view in this.View.Subviews) {

				view.RemoveFromSuperview ();
			}

			base.ViewWillDisappear (animated);
		}
	}
}

