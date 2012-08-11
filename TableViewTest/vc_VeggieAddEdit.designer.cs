// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace TableViewTest
{
	[Register ("vc_VeggieAddEdit")]
	partial class vc_VeggieAddEdit
	{
		[Outlet]
		MonoTouch.UIKit.UITextField txtVeggieName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch boolYummy { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblHeader { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtVeggieName != null) {
				txtVeggieName.Dispose ();
				txtVeggieName = null;
			}

			if (boolYummy != null) {
				boolYummy.Dispose ();
				boolYummy = null;
			}

			if (txtDescription != null) {
				txtDescription.Dispose ();
				txtDescription = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (lblHeader != null) {
				lblHeader.Dispose ();
				lblHeader = null;
			}
		}
	}
}
