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
	[Register ("imagePgViewController")]
	partial class imagePgViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView leftImageOutlet { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel leftLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView rightImageOutlet { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel rightLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton theNextButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnNext { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPlayAudio { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPlayVideo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRecordAudio { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnNext != null) {
				btnNext.Dispose ();
				btnNext = null;
			}
			if (btnPlayAudio != null) {
				btnPlayAudio.Dispose ();
				btnPlayAudio = null;
			}
			if (btnPlayVideo != null) {
				btnPlayVideo.Dispose ();
				btnPlayVideo = null;
			}
			if (btnRecordAudio != null) {
				btnRecordAudio.Dispose ();
				btnRecordAudio = null;
			}
		}
	}
}
