// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace TableViewTest
{
	[Register ("vc_MainMenu")]
	partial class vc_MainMenu
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnVeggies { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnVeggies != null) {
				btnVeggies.Dispose ();
				btnVeggies = null;
			}
		}
	}
}