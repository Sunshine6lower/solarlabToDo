using System;
using System.Linq;

namespace solarlabToDo
{
    public class UI
    {
        internal static void StartUI()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("=                                               =");
            Console.WriteLine("=     Приложение \"Планировщик\" Леханов М.Е.     =");
            Console.WriteLine("=                                               =");
            Console.WriteLine("=================================================");
            
            while (true)
            {
                Show();
                Console.WriteLine("\n1. Добавить задание" +
                                  "\n2. Изменить задание или его статус" +
                                  "\n3. Удалить задание" +
                                  "\n9. Настройки" +
                                  "\n0. Выход");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Введите задание:");
                        string _name = Console.ReadLine();
                        Console.WriteLine("Введите дату дэдлайна в формате DD.MM.YYYY hh:mm");
                        string _deadline = Console.ReadLine();
                        DB.AddToDB(new Task {Deadline = DateTime.Parse(_deadline), Name = _name, IsDone = false});
                        break;
                    case "2":
                        //TODO;
                        break;
                    case "3":
                        //TODO;
                        break;
                    case "9":
                        //TODO;
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Проверьте ввод.");
                        break;
                }
            }
        }
        
        public static void Show()
        {
            var tasks = DB.DBContext.Tasks.ToList();

            int idLength = (int)Math.Log10(tasks[tasks.Count - 1].Id) + 1;
            int nameLength = 8;
            int doneLength = 9;
            int dateLength = 16;

            const char done = '+';
            const char notDone = ' ';

            string[] headers = {"№", "Название", "Дата", "Выполнено"};
            foreach (Task t in tasks)
            {
                if (nameLength < t.Name.Length)
                    nameLength = t.Name.Length;
            }

            int master = idLength + nameLength + doneLength + dateLength + 5;
            Console.WriteLine();
            Console.WriteLine("|" + Indent((master-13)/2) + "Список дел:" + Indent((master-13)/2) + "|");
            Console.WriteLine("|{0, -" + idLength + "}|{1, -" + nameLength + "}|{2, -" + dateLength + "}|{3}|", headers[0], headers[1], headers[2], headers[3]);

            foreach (Task t in tasks)
            {
                char isD = t.IsDone ? done : notDone;
                if (t.Deadline.CompareTo(DateTime.Now) < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine("|{0, -" + idLength + "}|{1, -" + nameLength + "}|{2, -" + dateLength + "}|    {3}    |", t.Id, t.Name, t.Deadline.ToString("g"), isD);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        
        private static string Indent(int count)
        {
            return "".PadLeft(count);
        }
    }
}