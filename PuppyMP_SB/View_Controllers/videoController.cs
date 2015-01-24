using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoTouch.CoreVideo;
using MonoTouch.AVFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreMedia;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace speechTherapy
{
	public partial class videoController : UIViewController
	{
		//recording variables
		Boolean weAreRecording;
		AVCaptureMovieFileOutput output;
		AVCaptureDevice device;
		AVCaptureDevice audioDevice;
		AVCaptureDeviceInput input;
		AVCaptureDeviceInput audioInput;
		AVCaptureSession session;
		AVCaptureVideoPreviewLayer previewlayer;
		NSUrl videoLocation;
		String videoPath;
		UIView cameraView;

		Boolean _playNow; //used to determine which button was pushed to get here.

		//player variables

		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;
		NSUrl url;
		Boolean _isPlaying;
		NSObject _notificationHandle;
		UIView _videoView;

		public UIViewController Delegate{ get; set; }

		public videoController (IntPtr handle) : base (handle)
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
			// Perform any additional setup after loading the view, typically from a nib.

			initializeRecorder();
			configureOutput ();
			createPreview ();

			//If the play video button was pushed just show the latest recording right 


			if (_playNow) {

				initializeVideo ();
				startPlaying ();
			} else {

				weAreRecording = false;
				btnRecord.SetTitle ("Start Recording!", UIControlState.Normal);
				this.btnPlayVideo.Hidden = true;
				showPreview ();

			}

			//a recording hasn't been done yet, hide the button
			this.btnPlayVideo.TouchUpInside += playVideo;

		}
		private void initializeRecorder()
		{
			//Initialize sessions
			session = new AVCaptureSession ();

			//get inputs and add them to the session.

			try{
				//add front facing camera
				foreach(AVCaptureDevice capdevice in AVCaptureDevice.Devices)
				{
					if(capdevice.HasMediaType(AVMediaType.Video))
					{
						if(capdevice.Position == AVCaptureDevicePosition.Front)
						{
							device = capdevice;
						}
					}
				}

				//device = AVCaptureDevice.DefaultDeviceWithMediaType (AVMediaType.Video);
				input = AVCaptureDeviceInput.FromDevice(device);
				session.AddInput(input);

				//add audio capture device
				audioDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Audio);
				audioInput = AVCaptureDeviceInput.FromDevice(audioDevice);
				session.AddInput(audioInput);
			}
			catch(Exception ex) {
				return;
			}
		}

		private void createPreview()
		{
			previewlayer = new AVCaptureVideoPreviewLayer (session);
			previewlayer.Frame = this.View.Bounds;

			this.cameraView = new UIView ();

			this.cameraView.Layer.AddSublayer (previewlayer);

		}

		private void showPreview()
		{
			this.View.AddSubview (cameraView);
			this.View.SendSubviewToBack (cameraView);

		}

		private void configureOutput()
		{
			output = new AVCaptureMovieFileOutput ();

			long totalSeconds = 10000;
			Int32 preferredTimeScale = 30;
			CMTime maxDuration = new CMTime (totalSeconds, preferredTimeScale);
			output.MinFreeDiskSpaceLimit = 1024 * 1024;
			output.MaxRecordedDuration = maxDuration;

			if (session.CanAddOutput (output)) {
				session.AddOutput (output);
			}

			session.SessionPreset = AVCaptureSession.PresetMedium;

			//configure output location
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var library = System.IO.Path.Combine (documents, "..", "Library");
			videoPath = System.IO.Path.Combine (library, "sweetMovieFilm.mov");

			videoLocation = new NSUrl (videoPath, false);

			session.StartRunning ();
			this.btnRecord.TouchUpInside += startStopPushed;
		}

		private void startStopPushed (Object sender, EventArgs ea)
		{
			if (!weAreRecording) {

				deleteVideo (videoPath);

				AVCaptureFileOutputRecordingDelegate avDel = new AVCaptureFileOutputRecordingDelegate ();
				output.StartRecordingToOutputFile (videoLocation, avDel);
				btnRecord.SetImage (UIImage.FromFile("btn_done_recording.png"), UIControlState.Normal);
				btnPlayVideo.Hidden = true;

				weAreRecording = true;
			} 
			else {
				output.StopRecording ();
				weAreRecording = false;
				btnRecord.SetImage (UIImage.FromFile("btn_Start_recording.png"), UIControlState.Normal);
				btnPlayVideo.Hidden = false;
			}
		}

		public static Boolean vidExists(string vidCheck)
		{
			NSFileManager mgr = new NSFileManager ();
			if (mgr.FileExists (vidCheck)) {
				return true;
			} 

			return false;


		}

		public static void deleteVideo(string vidToDelete)
		{
			NSFileManager manager = new NSFileManager ();
			NSError error = new NSError ();

			//delete the file if it is already there
			if (manager.FileExists (vidToDelete)) {
				manager.Remove (vidToDelete, out error);
			}
		}


		public override void ViewWillDisappear(bool animated)
		{
			session.StopRunning ();
			this.btnRecord.TouchUpInside -= startStopPushed;
			clearSubViews ();

			base.ViewWillDisappear (animated);
		}

		//play video functionality

		private void playVideo(Object sender, EventArgs ea)
		{

			initializeVideo ();
			startPlaying ();

		}

		private void startPlaying ()
		{
			//first, remove recording subview
			//if this doesn't work it's because we went straight to player and now cameraview exists yet.
			try{
			this.cameraView.RemoveFromSuperview ();
			}
			catch(Exception) {
				//swallow
			}

			View.Layer.AddSublayer (_playerLayer);
			_videoView = new UIView ();
			this.View.AddSubview (_videoView);
			//this.View.SendSubviewToBack (_videoView);
			_videoView.Layer.AddSublayer (_playerLayer);


			_isPlaying = true;
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

			_notificationHandle = NSNotificationCenter.DefaultCenter.AddObserver (AVPlayerItem.DidPlayToEndTimeNotification, VideoDonePlaying);


			_playerLayer = AVPlayerLayer.FromPlayer (_player);
			_playerLayer.Frame = View.Frame;

		}

		private void VideoDonePlaying(NSNotification notification)
		{

			_videoView.RemoveFromSuperview ();

			this.View.AddSubview (this.cameraView);
			this.View.SendSubviewToBack (this.cameraView);

			this.btnRecord.Hidden = false;
			Console.WriteLine ("finished Playing");	
		}

		private void clearSubViews()
		{

			foreach (var view in this.View.Subviews) {
				view.RemoveFromSuperview ();
			} 			
		}

		public void setPlayNow(UIViewController vc, Boolean playNow)
		{
			Delegate = vc;

			_playNow = playNow;

		}



	}
}

