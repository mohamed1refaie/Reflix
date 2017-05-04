﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
	// Holds the current login session
	/*
	 * This is a global class to hold the current logged in user data
	 * to use it across the whole program for the current session
	 */
	class Session
	{
		public static int userID { get; set; }
		public static string userFirstName { get; set; }
	}

	class SQL
	{
		static public void ChangeQuery(string query)
		{
			SqlConnection sqlConnection = new SqlConnection($"Data Source = {Environment.MachineName}\\SQLEXPRESS; Initial Catalog = Reflix; Integrated Security = True");
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = sqlConnection;
			sqlConnection.Open();

			cmd.CommandText = query;
			cmd.ExecuteNonQuery();

			sqlConnection.Close();
		}
		static public List<Dictionary<string, object>> ReadQuery(string query)
		{
			SqlConnection sqlConnection = new SqlConnection($"Data Source = {Environment.MachineName}\\SQLEXPRESS; Initial Catalog = Reflix; Integrated Security = True");

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = sqlConnection;
			sqlConnection.Open();

			cmd.CommandText = query;
			SqlDataReader reader = cmd.ExecuteReader();

			// Dictionary is like C++ map
			// List is a dynamic array (like vector)
			List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

			// Read rows in the reader
			while (reader.Read())
			{
				// Dict to hold the row
				Dictionary<string, object> dict = new Dictionary<string, object>();

				// Add each column to the dict
				for (int lp = 0; lp < reader.FieldCount; lp++)
				{
					dict.Add(reader.GetName(lp), reader.GetValue(lp));
				}

				// Add the dict (row) to the list
				results.Add(dict);
			}

			sqlConnection.Close();
			return results;
		}
	}
}