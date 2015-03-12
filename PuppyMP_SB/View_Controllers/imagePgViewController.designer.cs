// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace speechTherapy
{
	[Register ("imagePgViewController")]
	partial class imagePgViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView leftImageOutlet { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView rightImageOutlet { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton theNextButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLeft { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnMulti { get; set; }

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
		UIButton btnPrevious { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRecordAudio { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRecordVideo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRight { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton micImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView multiImage { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnLeft != null) {
				btnLeft.Dispose ();
				btnLeft = null;
			}
			if (btnMulti != null) {
				btnMulti.Dispose ();
				btnMulti = null;
			}
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
			if (btnPrevious != null) {
				btnPrevious.Dispose ();
				btnPrevious = null;
			}
			if (btnRecordAudio != null) {
				btnRecordAudio.Dispose ();
				btnRecordAudio = null;
			}
			if (btnRecordVideo != null) {
				btnRecordVideo.Dispose ();
				btnRecordVideo = null;
			}
			if (btnRight != null) {
				btnRight.Dispose ();
				btnRight = null;
			}
			if (micImage != null) {
				micImage.Dispose ();
				micImage = null;
			}
			if (multiImage != null) {
				multiImage.Dispose ();
				multiImage = null;
			}
		}
	}
}
