using System;
using Microsoft.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
namespace WeirdProject
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            const string connectionString = "Data Source=MyDatabase.db";
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            string operation;
            for (; ; )
            {
                Console.WriteLine("\n Benvenuto nel tuo password manager personale! uwu \t \n Inserisci il tipo di operazione che desideri effettuare: \n \t S - Search \n \t R - Remove \n \t A - Add \n \t Q - Quit");
                operation = Console.ReadLine();
                switch (operation)
                {
                    case "a": addValue(connection); break;
                    case "q":
                        char conf;
                        Console.WriteLine("Are you sure you want to quit? Y/N");
                        conf = Console.ReadKey().KeyChar;
                        if (conf == 'y' || conf == 'Y')
                        {
                            Console.WriteLine("Thanks and see you next time!");
                            Environment.Exit(1);
                        }
                        else if (conf == 'n' || conf == 'N')
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\n Input not valid, getting back to the main menu! \n");
                            continue;
                        }
                        break;
                    case "s":
                        Console.WriteLine("What are you searching for? (Service name) \n Please use the keyword AllServices to show all the currently available records");
                        searchValue(connection, Console.ReadLine());
                        break;
                    case "r":
                        Console.WriteLine("Please, remember that removing records from the database is ID-based and you need to know the exact ID number to avoid deleting the wrong record and losing your password! \n To get the exact record number, you can search all the records for the service you're interested in or just list all the records and find the right one in the search feature. \n");
                        Console.WriteLine("Insert record ID: ");
                        int index;
                        index = int.Parse(Console.ReadLine());
                        removeValue(connection, index);
                        break;
                    default:
                        Console.WriteLine("Invalid input! Please provide a valid option \n");
                        break;
                }
            }
        }
        static void addValue(SqliteConnection connection)
        {
            Console.WriteLine("\n Welcome! \n \n Which service do you want to add a password for?");
            string service;
            string password = "";
            service = Console.ReadLine();
            Console.WriteLine("\n Nice! \n \n Now, do you want a randomly generated password or write your own? \n \n \t 1 - Randomly Generated \n \t 2 - I'll add my own");
            int randomornot = int.Parse(Console.ReadLine());
            switch (randomornot)
            {
                case 1:
                    password = Randomness.RandomPassword();
                    break;
                case 2:
                    Console.WriteLine($"\n Alright, what password are you gonna use for the service {service}?");
                    password = Console.ReadLine();
                    break;
            }
            Console.WriteLine("\n Alright, I'm adding the password to the database");
            SqliteCommand command = new SqliteCommand($"INSERT INTO whatdoesthefoxsay(Service, Password) values('{service}', '{password}')", connection);
            command.ExecuteNonQuery();
        }
        public class DatabaseRow
        {
            public int ID;
            public string Service;
            public string Password;
        }
        static void searchValue(SqliteConnection connection, string query)
        {
            List<DatabaseRow> records = new List<DatabaseRow>();
            SqliteCommand command = new SqliteCommand($"SELECT * FROM whatdoesthefoxsay WHERE Service='{query}'", connection);
            if (query == "AllServices")
            {
                command.CommandText = "SELECT * FROM whatdoesthefoxsay";
            }
            SqliteDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                DatabaseRow record = new DatabaseRow();
                record.ID = r.GetInt32(0);
                record.Service = r.GetString(1);
                record.Password = r.GetString(2);
                records.Add(record);
            }
            Console.WriteLine($"Found {records.Count} records! \n ");
            foreach (DatabaseRow row in records)
            {
                Console.WriteLine($"ID: {row.ID} Service: {row.Service} Password: {row.Password} \n ");
            }
        }
        static void removeValue(SqliteConnection connection, int RecordID)
        {
            SqliteCommand command = new SqliteCommand($"DELETE FROM whatdoesthefoxsay WHERE RecordID = '{RecordID}'", connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Successfully removed the desired record! \n");
        }

    }
}