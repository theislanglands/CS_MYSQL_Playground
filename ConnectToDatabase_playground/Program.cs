using System;
using System.Data;
using Datalayer;


internal class Program
{
    private static void Main(string[] args)
    {
        DataLayer dataLayer = new DataLayer();

        Console.WriteLine("Testing my database");


        dataLayer.TestingObject();

        dataLayer.OpenConnection();

        
        dataLayer.ReadRecords();

        dataLayer.CloseConnection();
        
        Console.WriteLine("All worked");
        
    }
}