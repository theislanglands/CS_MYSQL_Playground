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
        
        // OPEN CONNECTION
        dataLayer.OpenConnection();
        
        // INITIALIZE AND FILL WITH THREE PERSONS
        dataLayer.SeedDatabase();
        
        // INSERT PERSON
        dataLayer.InsertInDatabase("Hansi", 42);
        
        // READING RECORDS
        dataLayer.ReadRecords();
        
        // COUNTING RECORDS
        var noOfRecords = dataLayer.CountPersons();
        Console.WriteLine($"Number of persons in database = {noOfRecords}");
        
        // INSERTING USING PREPARED STATEMENT FOR USER INPUT
        dataLayer.InitializeDatabaseTable();
        
        // GETTING USER INPUT
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter Age ");
        int age = Convert.ToInt32(Console.ReadLine());
        
        // INSERT USER DEFINED PERSON
        dataLayer.InsertWithPreparedStatements(name, age);
        dataLayer.ReadRecords();
        
        // CLOSING CONNECTION
        dataLayer.CloseConnection();
        Console.WriteLine("All worked");
    }

    private void InsertPerson()
    {
        //
        
    }
}