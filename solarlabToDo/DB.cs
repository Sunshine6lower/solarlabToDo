using System;

namespace solarlabToDo
{
    public class DB
    {
        public static string Host { get; set; }
        public static string Port { get; set; }
        public static string DatabaseName { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }

        private static ApplicationContext _applicationContext;

        internal static void InitDB()
        {
            Host = "ec2-54-228-243-238.eu-west-1.compute.amazonaws.com";
            Port = "5432";
            DatabaseName = "de26dnl0958fo7";
            Username = "cdxodghvvdaznj";
            Password = "e1e0d3c60b90c826f72c3aec0aefcaff0d12ed2ede93fc23867fb557fc73f5fc";

            try
            {
                _applicationContext = new ApplicationContext();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Не удалось подключиться к БД. Проверьте доступ к интернету. Подключиться к другой БД? [Д/н]");
                string userChoice = Console.ReadLine();
                if (string.Equals(userChoice, "д", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(userChoice, "y", StringComparison.OrdinalIgnoreCase))
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
                    InitDB();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private static string IsEmptyString(string String, string Default)
        {
            if (string.IsNullOrWhiteSpace(String))
                return Default;
            return String;
        }
    }
}