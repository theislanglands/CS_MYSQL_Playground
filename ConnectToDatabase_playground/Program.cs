using System;
using System.Data;
using Datalayer;

// https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html


internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Testing my database");

        DataLayer dataLayer = new DataLayer();


        dataLayer.OpenConnection();

        dataLayer.SeedDatabase();
        //dataLayer.InsertInDatabase("Thomas", 12);
        //dataLayer.InsertInDatabase("Hans", 42);

        dataLayer.ReadRecords();

        dataLayer.CloseConnection();
        
        Console.WriteLine("All worked");
        
    }
}