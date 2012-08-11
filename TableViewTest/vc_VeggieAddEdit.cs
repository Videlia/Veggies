
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TableViewTest
{
	public partial class vc_VeggieAddEdit : UIViewController
	{
		public vc_VeggieAddEdit () : base ("vc_VeggieAddEdit", null)
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
			int isYummy = Convert.ToByte (boolYummy.On);

			// populate fields
			txtVeggieName.Text = Veggies.SelectedVeggie;

			// make sure keyboard disappears when user presses <Enter>
			this.txtVeggieName.ShouldReturn += (UITextField) =>
			{
				UITextField.ResignFirstResponder ();
				return true;
			};

			this.txtDescription.ShouldReturn += (UITextField) =>
			{
				UITextField.ResignFirstResponder ();
				return true;
			};



			// wire up buttons
			btnSave.TouchUpInside += delegate {

				// create new veggie if there is no selected veggie to update.
				if (Veggies.SelectedVeggie == string.Empty) {
					Veggies.CreateVeggie (txtVeggieName.Text, isYummy, txtDescription.Text);
					vc_ManageVeggies newScreen = new vc_ManageVeggies ();
					NavigationController.PushViewController (newScreen, false);
				}
				// else update selected veggie
				else {
					Veggies.UpdateVeggie (txtVeggieName.Text, isYummy, txtDescription.Text);
					vc_ManageVeggies newScreen = new vc_ManageVeggies ();
					NavigationController.PushViewController (newScreen, false);
				}

				// update VeggieNames list so tableview data will be updated.
				Veggies.RefreshVeggieList ();

			};
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

