using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Datalayer
{
	

    public class DataLayer
	{
        string connection_string = "server=localhost;user=root;database=theistest;port=3306;password=secret";
        MySqlConnection connection;


        public DataLayer()
		{

		}

		public void TestingObject()
		{
			Console.WriteLine("hul igennem");
		}

		public void OpenConnection()
		{
            connection = new MySqlConnection(connection_string);

            try
            {
                Console.WriteLine("connecting");
                connection.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to connect {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            Console.WriteLine("Closing connection");
            connection.Close();
        }

        public void ReadRecords()
        {
            Console.WriteLine("reading records");
            // (peron(id, name, age)

            // create query 
            string sql = "SELECT * FROM person";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            // excecute query and return result in object (don't create object with constructor!)
            MySqlDataReader reader = cmd.ExecuteReader();

            // Advances the MySqlDataReader to the next record - returns false, when no more
            // returns an array, with index corresponding to columns

            while (reader.Read())
            {
                Console.WriteLine($"name {reader[1]}, age {reader[2]}");
            }

            // While the MySqlDataReader is in use, the associated MySqlConnection is busy serving the MySqlDataReader,
            // and no other operations can be performed on the MySqlConnection other than closing it.
            // This is the case until the Close() method of the MySqlDataReader is called.

            reader.Close();
        }
	}
}

