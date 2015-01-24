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

namespace speechTherapy
{
	public class AudioHandler
	{

		AVAudioRecorder recorder;
		NSUrl url;
		NSDictionary settings;
		AVAudioSession session;
		AVAudioPlayer player;

		public AudioHandler ()
		{
			getSettings ();
		}

		private void getSettings()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var library = System.IO.Path.Combine (documents, "..", "Library");
			var urlpath = System.IO.Path.Combine (library, "audioRecording.wav");

			session = AVAudioSession.SharedInstance ();
			session.SetCategory (AVAudioSessionCategory.PlayAndRecord);

			url = new NSUrl (urlpath, false);


			NSFileManager manager = new NSFileManager ();
			NSError error = new NSError (new NSString ("world"), 1);

			//if there is a file at the save location, delete it so we can save there
			if (manager.FileExists (urlpath)) {
				Console.WriteLine ("Deleting File");
				manager.Remove (urlpath, out error);
				Console.WriteLine ("Deleted File");
			}

			NSObject[] values = new NSObject[]
			{
				NSNumber.FromFloat (44100.0f), //Sample Rate
				NSNumber.FromInt32 ((int)MonoTouch.AudioToolbox.AudioFormatType.LinearPCM), //AVFormat
				NSNumber.FromInt32 (2), //Channels
				NSNumber.FromInt32 (16), //PCMBitDepth
				NSNumber.FromBoolean (false), //IsBigEndianKey
				NSNumber.FromBoolean (false) //IsFloatKey
			};

			NSObject[] keys = new NSObject[] {
				AVAudioSettings.AVSampleRateKey,
				AVAudioSettings.AVFormatIDKey,
				AVAudioSettings.AVNumberOfChannelsKey,
				AVAudioSettings.AVLinearPCMBitDepthKey,
				AVAudioSettings.AVLinearPCMIsBigEndianKey,
				AVAudioSettings.AVLinearPCMIsFloatKey
			};

			settings = NSDictionary.FromObjectsAndKeys (values, keys);

			recorder = AVAudioRecorder.ToUrl (url, settings, out error);


		}

		public void startrecording()
		{

			session.SetActive(true);
			recorder.PrepareToRecord ();
			recorder.Record ();
		}

		public void stopRecording()
		{
			this.recorder.Stop ();
			session.SetActive (false);
		}

		public void playAudio()
		{
			session.SetActive (true);

			player = AVAudioPlayer.FromUrl (url);

			AVAudioPlayerDelegate avDel = new AVAudioPlayerDelegate ();
			player.Delegate = avDel;

			player.Play ();
	
		}

		public void playAudio(string path)
		{
			var newURL = new NSUrl ("Audio_Files/Hello.m4a");
			session.SetActive (true);

			player = AVAudioPlayer.FromUrl (newURL);

			AVAudioPlayerDelegate avDel = new AVAudioPlayerDelegate ();
			player.Delegate = avDel;

			player.Play ();
		}



	}
}

