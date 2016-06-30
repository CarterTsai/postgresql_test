using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;


namespace postgresql_test
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var connectString = "Host=192.168.99.100;Username=admin;Password=testtest;Database=mrbs";
			using (NpgsqlConnection connection = new NpgsqlConnection(connectString)) 
			{
				connection.Open();
				using (var cmd = new NpgsqlCommand())
				{
					try
					{
						string data = "1c95c52e-f597-d796-ec74-5fe72e32b196,def800a2-550e-6fb0-a440-bef65dd127da";
						cmd.Connection = connection;
						cmd.CommandText = "select * from \"TEST\" where \"UUID\" = ANY(string_to_array(@id,',')::uuid[])";
						cmd.Parameters.Add("@id", NpgsqlDbType.Text);
						//cmd.Prepare();
						cmd.Parameters[0].Value = data;

						var result = cmd.ExecuteReader();

						if (result.HasRows)
						{
							result.Read();
							Console.WriteLine(result.GetString(1));
							Console.WriteLine(result.GetString(2));
						}
					} catch(Exception ex) {
						Console.WriteLine(ex.Message);
					}
				}
			}
		}
	}
}
