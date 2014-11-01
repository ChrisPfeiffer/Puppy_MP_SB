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

namespace PuppyMP_SB
{
	[Register ("videoPlayController")]
	partial class videoPlayController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnHome { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPlayAgain { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRecordAgain { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnHome != null) {
				btnHome.Dispose ();
				btnHome = null;
			}
			if (btnPlayAgain != null) {
				btnPlayAgain.Dispose ();
				btnPlayAgain = null;
			}
			if (btnRecordAgain != null) {
				btnRecordAgain.Dispose ();
				btnRecordAgain = null;
			}
		}
	}
}
