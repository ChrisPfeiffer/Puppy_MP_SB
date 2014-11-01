// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace speechTherapy
{
	[Register ("videoController")]
	partial class videoController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPlayVideo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRecord { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnPlayVideo != null) {
				btnPlayVideo.Dispose ();
				btnPlayVideo = null;
			}
			if (btnRecord != null) {
				btnRecord.Dispose ();
				btnRecord = null;
			}
		}
	}
}
