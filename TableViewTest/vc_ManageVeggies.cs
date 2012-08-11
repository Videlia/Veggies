
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TableViewTest
{
	public partial class vc_ManageVeggies : UIViewController
	{
		public vc_ManageVeggies () : base ("vc_ManageVeggies", null)
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
			
			// connect table to source.
			VeggieTableSource source = new VeggieTableSource ();

			Console.WriteLine ("\n vc_ManageVeggies.ViewDidLoad: " + Veggies.CountVeggies ().ToString () + " veggies found! :-)");

			if (Veggies.CountVeggies () > 0) {

				this.tblVeggies.Source = source;
			}
				
			btnNewVeggie.TouchUpInside += delegate {

				Veggies.SelectedVeggie = string.Empty;

				vc_VeggieAddEdit newScreen = new vc_VeggieAddEdit ();
				try {
					this.NavigationController.PushViewController (newScreen, false);
				} catch (Exception ex) {
					Console.WriteLine ("btnNewVeggie error: " + ex.ToString ());
				}
			};


			btnEditVeggie.TouchUpInside += delegate {

				if (Veggies.SelectedVeggie == string.Empty) {
					UIAlertView alert = new UIAlertView ("Oops!", "Please select a veggie to edit.", null, "OK");
					alert.Show ();
				} else {
					vc_VeggieAddEdit newScreen = new vc_VeggieAddEdit ();
					this.NavigationController.PushViewController (newScreen, false);
				}
			};


			btnDeleteVeggie.TouchUpInside += delegate {

				Veggies.DeleteVeggie (Veggies.SelectedVeggie);

				// update VeggieNames list and reload tableview data.
				//Veggies.RefreshVeggieList ();
				Veggies.VeggieNames.Remove (Veggies.SelectedVeggie);
				tblVeggies.ReloadData ();

			};

			btnDone.TouchUpInside += delegate {
			
				vc_MainMenu newScreen = new vc_MainMenu ();
				NavigationController.PushViewController (newScreen, false);
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

		public void RefreshVeggieTable ()
		{

	

		}
	}
}

