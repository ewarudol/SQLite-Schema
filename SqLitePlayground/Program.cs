using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqLitePlayground {
    class Program {

        public static void CreateDb(string name) {
            SQLiteConnection.CreateFile(name + ".sqlite");
        }

        public static SQLiteConnection OpenConnection() {
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=Books.sqlite;Version=3;");
            dbConnection.Open();
            return dbConnection;
        }

        public static int ExecuteQueryNoResult(string query, SQLiteConnection dbConnection) {
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            int numRows = command.ExecuteNonQuery();
            return numRows;
        }

        public static void ExecuteQueryAndDisplay(string query, SQLiteConnection dbConnection) {
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<string> columns = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++) 
                columns.Add(reader.GetName(i));

            while (reader.Read())
                columns.ForEach(x => { Console.WriteLine(reader[x]); });
        }

        static void Main(string[] args) {

            //### 1. Get SqLite from Nuget

            //### 2. For the first time create database
            //CreateDb("Authors");

            //### 3. Open Connection
            SQLiteConnection dbConnection = OpenConnection();

            //### 4. Create table with a simple query execution  
            //ExecuteQueryNoResult("CREATE TABLE books (id INT, name VARCHAR(20))", dbConnection);

            //### 5. Execute and get rows number  
            //ExecuteQueryNoResult("insert into books (id, name) values(1, 'Snow White')", dbConnection);

            //### 6. Execute and get reader to select data
            ExecuteQueryAndDisplay("select * from books", dbConnection);

            dbConnection.Cancel();
            Console.ReadKey();
        }
    }
}
