using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using Mysqlx.Crud;
using System.Xml.Linq;

namespace Datalayer
{
    public class DataLayer
    {
        private readonly string _connectionString =
            "server=localhost;user=root;database=theistest;port=3306;password=secret";

        MySqlConnection connection;


        public DataLayer()
        {
        }


        public void OpenConnection()
        {
            connection = new MySqlConnection(_connectionString);
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

        // ExecuteReader to GET information
        public void ReadRecords()
        {
            Console.WriteLine("reading records");
            // (person(id, name, age)

            // create query 
            string sql = "SELECT * FROM person";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            // execute query and return result in object (don't create object with constructor!)
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
        
        // ExecuteNonQuery to insert, update, and delete data.
        public void InsertInDatabase(string name, int age)
        {
            Console.WriteLine($"Inserting: {name} aged {age} in database");
            string sql = $"INSERT INTO person(Name, Age) VALUES ('{name}', {age});";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }


        public void SeedDatabase()
        {
            Console.WriteLine("Seeding database");
            InitializeDatabaseTable();
            InsertThreeRandomPersons();
        }

        private void InsertThreeRandomPersons()
        {
            Console.WriteLine("Inserting three random persons");
            Dictionary<string, int> persons = new Dictionary<string, int>()
            {
                { "Torben", 67 },
                { "Birthe", 42 },
                { "Hr. Jensen", 12 }
            };

            foreach (var person in persons)
            {
                InsertInDatabase(person.Key, person.Value);
            }
        }

        public void InitializeDatabaseTable()
        {
            Console.WriteLine("Drop and create new table");
            ArrayList sqlStatements = new ArrayList();
            sqlStatements.Add("DROP TABLE IF EXISTS person;");
            sqlStatements.Add(
                "CREATE TABLE person (ID int NOT NULL AUTO_INCREMENT, Name varchar(255), Age int, PRIMARY KEY (ID));");

            foreach (string sqlStatement in sqlStatements)
            {
                var cmd = new MySqlCommand(sqlStatement, connection);
                cmd.ExecuteNonQuery();
            }
        }

        // use the ExecuteScalar method to return a single value
        public long CountPersons()
        {
            string sql = "SELECT COUNT(*) FROM person;";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            return (long) cmd.ExecuteScalar();
        }

        // https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-parameters.html
        public void InsertWithPreparedStatements(string name, int age)
        {
            Console.WriteLine($"Inserting: {name} aged {age} in database using prepared statement");
            string sql = "INSERT INTO person(Name, Age) VALUES (@name, @age);";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.ExecuteNonQuery();
        }
    }
}