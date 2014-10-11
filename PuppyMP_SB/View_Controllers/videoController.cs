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

namespace PuppyMP_SB
{
	public partial class videoController : UIViewController
	{
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
			weAreRecording = false;
			btnRecord.SetTitle ("Start Recording!", UIControlState.Normal);
			initializeRecorder();
			createPreview ();
			configureOutput ();

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

			UIView cameraView = new UIView ();
			cameraView.Layer.AddSublayer (previewlayer);

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

				deleteVideo ();

				AVCaptureFileOutputRecordingDelegate avDel = new AVCaptureFileOutputRecordingDelegate ();
				output.StartRecordingToOutputFile (videoLocation, avDel);
				btnRecord.SetTitle ("Done!", UIControlState.Normal);
				weAreRecording = true;
			} 
			else {
				output.StopRecording ();
				weAreRecording = false;
				btnRecord.SetTitle ("Start Recording!", UIControlState.Normal);
			}
		}

		private void deleteVideo()
		{
			NSFileManager manager = new NSFileManager ();
			NSError error = new NSError ();

			//delete the file if it is already there
			if (manager.FileExists (videoPath)) {
				manager.Remove (videoPath, out error);
			}
		}

		public override void ViewWillDisappear(bool animated)
		{
			session.StopRunning ();
			this.btnRecord.TouchUpInside -= startStopPushed;

			foreach (var view in this.View.Subviews) {
				view.RemoveFromSuperview ();
			}

			base.ViewWillDisappear (animated);
		}

	}
}

