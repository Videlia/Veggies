using System;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MyDataLayer
{

	// DataField class is used to create data for each data field in a table to be created, updated, or deleted.
	public class DataField
	{
	
		public string FieldName { get; set; }

		public string FieldValue { get; set; }

		public string FieldType { get; set; }
	}

	public class DataLayer
	{	
		//declare variables used by all DataLayer methods; 
		private static string dbFileName = "myFunDatabase.db3";
		private static SqliteConnection connection;

		public DataLayer ()
		{
		}

		public static int CountRecords (string tableName)
		{
			int count = 0;
			string sql = "SELECT count(*) FROM " + tableName;
			count = DataLayer.ExecuteScalar_Int16 (sql);
			Console.WriteLine ("\n DataLayer.CountRecords: " + count.ToString () + "records found in the datatable.");


			return count;

		}
		public static void ExecuteNonQuery (string sql)
		{
			SqliteCommand sqlCommand = new SqliteCommand ();

			try {
				// create and open connection
				connection = GetConnection ();

				using (connection) {
					connection.Open ();
					Console.WriteLine ("\n DataLayer.ExecuteNonQuery: connection opened. Connection state is " + connection.State);
						
					// create and execute command
					sqlCommand.CommandText = sql;
					sqlCommand.Connection = connection;
					sqlCommand.ExecuteNonQuery ();
					connection.Close ();
					Console.WriteLine ("\n DataLayer.ExecuteNonQuery: Connection was closed. Connection state is " + connection.State);

				}
				connection.Close ();

				Console.WriteLine ("DataLayer.ExecuteNonQuery: NonQuery Executed: " + sql);

			} catch (Exception ex) {
				Console.WriteLine ("\n >>> DataLayer.ExecuteNonQuery Exception: " + ex.ToString ());
				Console.WriteLine ("\n >>> SQL statement: " + sql);
			}

		}

		public static Int16 ExecuteScalar_Int16 (string sql)
		{
			int result = 0;
			SqliteCommand sqlCommand = new SqliteCommand ();

			try {
				connection = GetConnection ();
				connection.Open ();

				sqlCommand.CommandText = sql;
				sqlCommand.Connection = connection;
				result = Convert.ToInt16 (sqlCommand.ExecuteScalar ());

				connection.Close ();

			} catch (Exception ex) {
				Console.WriteLine ("\n >>>DataLayer.ExecuteScalar_Int Exception: " + ex.ToString ());
			}

			return Convert.ToInt16 (result);
		}

		public static string ExecuteScalar_String (string sql)
		{
			string result = string.Empty;
			SqliteCommand sqlCommand = new SqliteCommand ();

			try {
				connection = GetConnection ();
				connection.Open ();

				sqlCommand.CommandText = sql;
				sqlCommand.Connection = connection;
				result = Convert.ToString (sqlCommand.ExecuteScalar ());

				connection.Close ();

			} catch (Exception ex) {
				Console.WriteLine ("\n >>>DataLayer.ExecuteScalar_Int Exception: " + ex.ToString ());
			}

			return result;
		}

		public void CreateDatabaseFolder ()
		{

		}


		public string InsertRecord (string tableName, DataField[] dataFields)
		{
			string sql = string.Empty;
			string fieldNameList = string.Empty;
			string fieldValueList = string.Empty;
			string response = string.Empty;
			SqliteCommand sqlCommand = new SqliteCommand ();

			connection = GetConnection ();

			try {

				connection.Open ();

				// Begin transaction for multiple updates (improves efficiency)
				sqlCommand = new SqliteCommand ("BEGIN", connection);
				sqlCommand.ExecuteNonQuery ();

				// begin individual inserts. I will nest this later to accommodate batch updates.l
				sql = "INSERT INTO " + tableName + " (";
				sqlCommand = new SqliteCommand ();
				sqlCommand.Connection = connection; 

				foreach (DataField dataField in dataFields) {
					fieldNameList += dataField.FieldName + ", ";
					fieldValueList += dataField.FieldValue + ", ";
					sqlCommand.Parameters.AddWithValue ("@" + dataField.FieldName, dataField.FieldValue);
				}

				// remove extra commas at the end of the lists, and append to the sql statement
				sql += fieldNameList.Substring (0, fieldNameList.Length - 2);
				sql += ") VALUES (";
				sql += fieldValueList.Substring (0, fieldValueList.Length - 2);
				sql += ")";

				Console.WriteLine (sql); 

				// load and run sql insert statement.
				sqlCommand.CommandText = sql;

				sqlCommand.ExecuteNonQuery ();

				sqlCommand = new SqliteCommand ("END", connection);
				sqlCommand.ExecuteNonQuery ();

				connection.Close ();

				response = "Success";
			} catch (Exception ex) {
				response = ">>> DataLayer.InsertRecord Error: " + ex.ToString ();
			}

			// End batch commmand and close connection to the database.

			Console.WriteLine ("\n DataLayer.InsertRecord Response to be returned: " + response);
			return response;
		}

		public static List<string> GetStringList (string sql)
		{
			SqliteDataReader dataReader;
			SqliteCommand sqlCommand = new SqliteCommand ();
			List<string> values = new List<string> ();

			int i = 0; //used to increment through  records.
			int count = 0; 

			connection = GetConnection ();

			try {

				using (connection) {
					connection.Open ();

					Console.WriteLine ("\n DataLayer.GetStringList: sql statement: " + sql);
					sqlCommand.CommandText = sql;


					sqlCommand.Connection = connection;

					dataReader = sqlCommand.ExecuteReader ();	
					if (dataReader.HasRows) {
						Console.WriteLine ("\n DataLayer.GetStringList: dataReader has" + " rows. ");
						while (dataReader.Read ()) {
							values.Add (dataReader [0].ToString ());
							Console.WriteLine ("\n DataLayer.GetStringList: value added to list: " + dataReader [0].ToString ());
							i++;
						}
						count = i + 1;
						Console.WriteLine ("\n DataLayer.GetStringList: dataReader has" + count + " rows. ");

						Console.WriteLine ("\n DataLayer.GetStringList: successfully populated value string array from data reader");

						connection.Close ();
					}
				}
			} catch (Exception ex) {
				Console.WriteLine ("\n >>> DataLayer.GetStringList  error: " + ex.ToString ());

			}

			return values;
		
		}

		public void GetSchema ()
		{


		}

		public static void DeleteDatabase ()
		{
			// Declare variables 
			var path = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			string db; // full name and path of database file.

			// set initial values;
			path = Path.Combine (path, "..", "Library");
			db = Path.Combine (path, dbFileName);
			bool databaseExists = File.Exists (db);
			Console.WriteLine ("\n DataLayer.GetConnection: Getting ready to delete database if it exists.");

			if (databaseExists) {
				try {
					File.Delete (db);
					Console.WriteLine ("\n DataLayer.DeleteDatabase: database deleted: " + dbFileName);
				} catch (Exception ex) {
					Console.WriteLine ("\n >>> DataLayer.DeleteDatabase: Unable to delete database: " + dbFileName);
					Console.WriteLine ("\n >>> DataLayer.DeleteDatabase: Cause of error: " + ex.ToString ());

				}

			}

		}

		static SqliteConnection GetConnection ()
		{
			// Declare variables 
			var path = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			string db; // full name and path of database file.
			var conn = new SqliteConnection ();

			// set initial values;
			path = Path.Combine (path, "..", "Library");
			db = Path.Combine (path, dbFileName);
			bool databaseExists = File.Exists (db);
			Console.WriteLine ("\n DataLayer.GetConnection: Getting ready to create database if it does not exist.");

			if (!databaseExists) {

				CreateDatabase (db);
			}

			conn = new SqliteConnection ("\n Data Source=" + db);
			return conn;

		}


		static void CreateDatabase (string db)
		{
			// Declare variables and set initial values
			var conn = new SqliteConnection ();
			bool databaseExists = File.Exists (db);
			;

			// create database if it does not exist
			Console.WriteLine ("\n DataLayer.GetConnection: Getting ready to create database if it does not exist.");

			if (!databaseExists) {
				try {
					SqliteConnection.CreateFile (db);
					Console.WriteLine ("\n DataLayer.GetConnection: Database created successfully.");
				} catch (Exception ex) {
					Console.WriteLine ("\n >>> DataLayer.GetConnection: Error creating database: " + ex.ToString ());
				}

				if (!File.Exists (db)) {

					Console.WriteLine ("\n >>> DataLayer.GetConnection: Database NOT created.");
				}
			

				conn = new SqliteConnection ("Data Source=" + db);

				var commands = new[] {

            		"CREATE TABLE tVeggies (VeggieName ntext, Description ntext, Yummy int)",
					"INSERT INTO tVeggies (VeggieName, Description, Yummy) VALUES ('Carrots', 'An orange root', 0)",
					"INSERT INTO tVeggies (VeggieName, Description, Yummy) VALUES ('Spinach', 'Green leafy veggie', 1)",
					"INSERT INTO tVeggies (VeggieName, Description, Yummy) VALUES ('Onion', 'Round with layers', 1)",
            	};

				foreach (var cmd in commands) {
				
					using (var c = conn.CreateCommand()) {

						c.CommandText = cmd;
						//c.CommandType = CommandType.Text; //this code does not work.
						conn.Open ();
						c.ExecuteNonQuery ();
						conn.Close ();
						Console.WriteLine ("\n DataLayer.GetConnection: command executed: " + cmd.ToString ());
					}
				}
			} 
		}


		public static System.Data.DataTable GetData (string sql)
		{
			// declare variables and set initial values
			SqliteCommand command = new SqliteCommand ();
			System.Data.DataTable dataTable = new System.Data.DataTable ();
			SqliteDataAdapter adapter = new SqliteDataAdapter ();
			System.Data.DataRow iRow;

			//open connection and retrieve desired data.
			try {
				connection = GetConnection ();
				using (connection) {

					Console.WriteLine ("\n DataLayer.GetData: sql statement: " + sql);
					command.CommandText = sql;
					command.Connection = connection;
					adapter = new SqliteDataAdapter (command);

					connection.Open ();
					adapter.Fill (dataTable);

					// check to see if we got some data.
					Console.WriteLine ("\n DataLayer.GetData: retrieved dataTable with " + dataTable.Rows.Count.ToString () + " rows.");
					for (int i = 0; i< dataTable.Rows.Count; i++) {
						iRow = dataTable.Rows [i];
					}

					Console.WriteLine ("\n DataLayer.GetData: successfully populated data table");

					connection.Close ();
				}

			} catch (Exception ex) {
				Console.WriteLine ("\n >>> DataLayer.GetData  error: " + ex.ToString ());

			}

			return dataTable;

		}

	}
}

