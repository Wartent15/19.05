using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _19._05
{
    internal class Program
    {
        static SQLiteConnection connection; // инициализация conection
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=mydb_19_05.db;Version=3;";

            connection = new SQLiteConnection(connectionString);
            connection.ConnectionString = connectionString;

            //string query = "CREATE TABLE Vegetables_and_Fruits(Id INT AUTO_INCREMENT, Name NVARCHAR(30), Type NVARCHAR(30), Color NVARCHAR(30), Calorie INT);";

            // команда создана
            //SQLiteCommand command = new SQLiteCommand(query, connection); // два параметра: запрос и подключение   

            //connection.Open();
            //command.ExecuteNonQuery();
            //connection.Close();

            while (true)
            {
                Console.WriteLine("Выберете операцию: \n" +
                    "1. Добавить овощи \n" +
                    "2. Удалить овощи \n" +
                    "3. Показать все овощи \n" +
                    "4. Показать минимальную калорийность\n" +
                    "5. Показать максимальную калорийность\n" +
                    "6. Показать среднюю калорийность\n");

                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Введите id");
                        int idAdd = int.Parse(Console.ReadLine());

                        Console.WriteLine("Введите name");
                        string name = Console.ReadLine();

                        Console.WriteLine("Введите type");
                        string type = Console.ReadLine();

                        Console.WriteLine("Введите color");
                        string color = Console.ReadLine();

                        Console.WriteLine("Введите calorie");
                        int calorie = Console.Read();

                        AddVegetables(idAdd, name, type, color, calorie);
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.WriteLine("Введите id");
                        int idDelete = int.Parse(Console.ReadLine());

                        DeleteVegetables(idDelete);
                        Console.WriteLine();
                        break;

                    case "3":
                        ShowAllVegetables();
                        Console.WriteLine();
                        break;

                    case "4":
                        ShowMinCalorie();
                        Console.WriteLine();
                        break;
                    case "5":
                        ShowMaxCalorie();
                        Console.WriteLine();
                        break;

                    case "6":
                        ShowABSCalorie();
                        Console.WriteLine();
                        break;
                }


            }
        }

        public static void AddVegetables(int id, string name, string type, string color, int calorie)
        {
            string query = $"INSERT INTO Vegetables_and_Fruits VALUES ({id}, '{name}', '{type}', '{color}','{calorie}')";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();
            command.ExecuteScalar();
            connection.Close();
        }

        public static void DeleteVegetables(int id)
        {
            string query = $"DELETE FROM Vegetables_and_Fruits where id = {id}";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public static void ShowAllVegetables()
        {
            string query = $"SELECT * FROM Vegetables_and_Fruits";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            int line = 0;

            while (reader.Read())
            {
                if (line == 0)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + " ");
                    }
                    Console.WriteLine();
                }
                if (line >= 0)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + " ");
                    }
                    Console.WriteLine();
                }
                line++;
            }

            reader.Close();

            connection.Close();
        }

        public static void ShowMinCalorie()
        {
            string query = $"SELECT Calorie FROM Vegetables_and_Fruits";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();

            Int64 minCalorie = 100000000000;
            int line = 0;

            while (reader.Read())
            {
                if (line >= 0)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if(minCalorie > reader.GetInt64(i))
                        {
                            minCalorie = reader.GetInt64(i);
                        }
                    }
                }
                line++;
            }
            Console.WriteLine($"Минимальное количество калорий: {minCalorie}");

            reader.Close();

            connection.Close();
        }

        public static void ShowMaxCalorie()
        {
            string query = $"SELECT Calorie FROM Vegetables_and_Fruits";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();

            Int64 maxCalorie = 0;
            int line = 0;

            while (reader.Read())
            {
                if (line >= 0)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (maxCalorie < reader.GetInt64(i))
                        {
                            maxCalorie = reader.GetInt64(i);
                        }
                    }
                }
                line++;
            }
            Console.WriteLine($"Максимальное количество калорий: {maxCalorie}");

            reader.Close();

            connection.Close();
        }   
        public static void ShowABSCalorie()
        {
            string query = $"SELECT Calorie FROM Vegetables_and_Fruits";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();

            Int64 Calorie = 0;
            int count_vegetables = 0;
            int line = 0;

            while (reader.Read())
            {
                if (line >= 0)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Calorie += reader.GetInt64(i);
                        count_vegetables ++;
                    }
                }
                line++;
            }
            Console.WriteLine($"Средняя калорийность: {Calorie / count_vegetables}");

            reader.Close();

            connection.Close();
        }
    }
}
