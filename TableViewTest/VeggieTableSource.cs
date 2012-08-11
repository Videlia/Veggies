using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TableViewTest
{
	public class VeggieTableSource: UITableViewSource
	{
		//List<string> veggieNames;

		public VeggieTableSource ()
		{
			Veggies.GetVeggieNames ();
		}

				#region implemented abstract members of MonoTouch.UIKit.UITableViewSource
		public override int RowsInSection (UITableView tableview, int section)
		{
			//return veggieNames.Count;
			return Veggies.VeggieNames.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cell");

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, "cell");

				var text = string.Format ("{0}", Veggies.VeggieNames [indexPath.Row]);
				cell.TextLabel.Text = text;

			}


			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Veggies.SelectedVeggie = Veggies.VeggieNames [indexPath.Row].ToString ();

		}
		#endregion


	}
}


