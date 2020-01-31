using System;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace solarlabToDo
{
    public class DB
    {
        public static string Host { get; set; } = "ec2-54-228-243-238.eu-west-1.compute.amazonaws.com";
        public static string Port { get; set; } = "5432";
        public static string DatabaseName { get; set; } = "de26dnl0958fo7";
        public static string Username { get; set; } = "cdxodghvvdaznj";
        public static string Password { get; set; } = "e1e0d3c60b90c826f72c3aec0aefcaff0d12ed2ede93fc23867fb557fc73f5fc";

        public static ApplicationContext DBContext;

        internal static void InitDB()
        {
            try
            {
                DBContext = new ApplicationContext();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Не удалось подключиться к БД. Проверьте доступ к интернету. Подключиться к другой БД? [Д/н]");
                string userChoice = Console.ReadLine();
                if (string.Equals(userChoice, "д", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(userChoice, "y", StringComparison.OrdinalIgnoreCase))
                {
                    ChangeDB();
                    InitDB();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        internal static void ChangeDB()
        {
            Console.WriteLine("Введите хост: [localhost]");
            Host = IsEmptyString(Console.ReadLine(), "localhost");
            Console.WriteLine("Введите порт: [5432]");
            Port = IsEmptyString(Console.ReadLine(), "5432");
            Console.WriteLine("Введите название БД: [postgres]");
            Port = IsEmptyString(Console.ReadLine(), "postgres");
            Console.WriteLine("Введите имя пользователя: [postgres]");
            Port = IsEmptyString(Console.ReadLine(), "postgres");
            Console.WriteLine("Введите пароль: []");
            Port = IsEmptyString(Console.ReadLine(), "");
        }

        internal static void AddToDB(Task task)
        {
            DBContext.Tasks.Add(task);
            DBContext.SaveChanges();
        }

        private static string IsEmptyString(string String, string Default)
        {
            if (string.IsNullOrWhiteSpace(String))
                return Default;
            return String;
        }

        
    }
}