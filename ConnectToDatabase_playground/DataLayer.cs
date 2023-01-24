﻿using System;
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
            Console.WriteLine("inserting in database");
            string sql = $"INSERT INTO person(Name, Age) VALUES ('{name}', {age});";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }


        public void SeedDatabase()
        {
            Console.WriteLine("Seeding database");
            ArrayList sqlStatements = new ArrayList();
            sqlStatements.Add("DROP TABLE IF EXISTS person;");
            sqlStatements.Add("CREATE TABLE person (ID int NOT NULL AUTO_INCREMENT, Name varchar(255), Age int, PRIMARY KEY (ID));");

            foreach (string sqlStatement in sqlStatements)
            {
                Console.WriteLine($"running {sqlStatement}");
                var cmd = new MySqlCommand(sqlStatement, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(string name, int age)
        {
        }
    }
}