// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace TableViewTest
{
	[Register ("vc_ManageVeggies")]
	partial class vc_ManageVeggies
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnDone { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblVeggies { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblFeedback { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnNewVeggie { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnEditVeggie { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnDeleteVeggie { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnDone != null) {
				btnDone.Dispose ();
				btnDone = null;
			}

			if (tblVeggies != null) {
				tblVeggies.Dispose ();
				tblVeggies = null;
			}

			if (lblFeedback != null) {
				lblFeedback.Dispose ();
				lblFeedback = null;
			}

			if (btnNewVeggie != null) {
				btnNewVeggie.Dispose ();
				btnNewVeggie = null;
			}

			if (btnEditVeggie != null) {
				btnEditVeggie.Dispose ();
				btnEditVeggie = null;
			}

			if (btnDeleteVeggie != null) {
				btnDeleteVeggie.Dispose ();
				btnDeleteVeggie = null;
			}
		}
	}
}
