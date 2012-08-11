using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using MyDataLayer;

namespace TableViewTest
{
	public static class Veggies
	{
		public static List<string> VeggieNames;
		public static string SelectedVeggie;
		public static int NumberOfVeggies;

		public static int CountVeggies ()
		{
			NumberOfVeggies = DataLayer.CountRecords ("tVeggies");
			return NumberOfVeggies;
		}

		public static List<string> GetVeggieNames ()
		{
			//VeggieName ntext, Description ntext, Yummy int
			string sql = "Select VeggieName from tVeggies ORDER BY VeggieName";
			VeggieNames = DataLayer.GetStringList (sql);
			Console.WriteLine ("\n GetVeggieNames: " + VeggieNames.ToString ());
			return VeggieNames;
		}

		public static void CreateVeggie (string veggieName, int yummy, string description)
		{
			// 1. Declare variables and set initial values
			DataLayer dataLayer = new DataLayer ();
			DataField[] dataFields;

			DataField NameField = new DataField ();
			DataField YummyField = new DataField ();
			DataField DescriptionField = new DataField ();

			string sql = string.Empty;

			// unselect any other players because the new player will also become the selected player.
			MyDataLayer.DataLayer.ExecuteNonQuery (sql);

			//3. Create data field objects for each field to be created.
			NameField.FieldName = "VeggieName";
			NameField.FieldType = "string";
			NameField.FieldValue = '"' + veggieName + '"';

			YummyField.FieldName = "Yummy";
			YummyField.FieldType = "int";
			YummyField.FieldValue = yummy.ToString ();

			DescriptionField.FieldName = "Description";
			DescriptionField.FieldType = "string";
			DescriptionField.FieldValue = '"' + description + '"';

			dataFields = new DataField[] { NameField, YummyField, DescriptionField };

			// Insert Record into database.
			dataLayer.InsertRecord ("tVeggies", dataFields);

			RefreshVeggieList ();


		}

		public static void DeleteVeggie (string veggieName)
		{

			string sql = "DELETE FROM tVeggies WHERE VeggieName = '" + veggieName + "'";

			try {
				DataLayer.ExecuteNonQuery (sql);

			} catch (Exception ex) {
				Console.WriteLine ("Player.Delete error: " + ex.ToString ());
			}

			RefreshVeggieList ();

		}

		public static void UpdateVeggie (string veggieName, int yummy, string description)
		{
			// create variables and set initial values
			//DataLayer dataLayer = new DataLayer ();
			string sql = "UPDATE tVeggies SET VeggieName = '" + veggieName + "', yummy = " + yummy.ToString () + ", description = '" + description + "' where VeggieName = '" + Veggies.SelectedVeggie + "'";
			//string response = string.Empty; //this will hold a response such as "success" or the error message.

			Console.WriteLine ("Veggie.UpdateVeggie sql statement: " + sql);

			try {
				DataLayer.ExecuteNonQuery (sql);
				Console.WriteLine ("PVeggie.UpdateVeggie: veggie updated successfully.");

			} catch (Exception ex) {
				Console.WriteLine ("Veggie.UpdateVeggie: " + ex.ToString ());
			}

			RefreshVeggieList ();

		}

		// Update list of Veggie names
		// the TableView on the ManageVeggies view controller needs the VeggieList to be updated whenever veggie records are changed.
		//so the tableview will refresh properly.
		public static void RefreshVeggieList ()
		{

			string sql = "Select VeggieName from tVeggies ORDER BY VeggieName";
			VeggieNames = DataLayer.GetStringList (sql);
		}
	}
}

