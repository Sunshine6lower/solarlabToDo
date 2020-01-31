using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                ShowAll();
                Console.WriteLine("\n1. Добавить задание" +
                                  "\n2. Изменить задание или его статус" +
                                  "\n3. Удалить задание" +
                                  "\n9. Настройки" +
                                  "\n0. Выход");
                Task userTask = null;
                string userChoice;
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
                        if (DB.DBContext.Tasks.ToList().Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                        }
                        else
                        {
                            while (userTask == null)
                            {
                                Console.WriteLine("Выберите номер задания:");
                                userChoice = Console.ReadLine();
                                int userChoiceId;
                                
                                if(!Int32.TryParse(userChoice, out userChoiceId) || (userTask = DB.DBContext.Tasks.FirstOrDefault(t=>t.Id==userChoiceId)) == null)
                                    Console.WriteLine("Введено некорректное значение номера");
                            }
                            
                            Show(userTask);

                            Console.WriteLine("1. Изменить задание\n" +
                                              "2. Изменить дэдлайн\n" +
                                              "3. Изменить статус выполнения");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("Введите задание:");
                                    userTask.Name = Console.ReadLine();
                                    break;
                                case "2":
                                    Console.WriteLine("Введите новую дату дэдлайна в формате DD.MM.YYYY hh:mm:");
                                    userTask.Deadline = DateTime.Parse(Console.ReadLine());
                                    break;
                                case "3":
                                    userTask.IsDone = !userTask.IsDone;
                                    break;
                                default:
                                    Console.WriteLine("Недопустимый ввод");
                                    break;
                            }
                        }
                        break;
                    case "3":
                        if (DB.DBContext.Tasks.ToList().Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                        }

                        while (userTask == null)
                        {
                            Console.WriteLine("Выберите номер задания:");
                            userChoice = Console.ReadLine();
                            int userChoiceId;
                                
                            if(!Int32.TryParse(userChoice, out userChoiceId) || (userTask = DB.DBContext.Tasks.FirstOrDefault(t=>t.Id==userChoiceId)) == null)
                                Console.WriteLine("Введено некорректное значение номера");
                        }
                            
                        Show(userTask);

                        Console.WriteLine("Вы уверены, что хотите удалить задание? [Д/н]");

                        userChoice = Console.ReadLine();
                        
                        if (string.Equals(userChoice, "д", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(userChoice, "y", StringComparison.OrdinalIgnoreCase))
                        {
                            DB.DBContext.Tasks.Remove(userTask);
                        }
                        break;
                    case "9":
                        Console.WriteLine("1. Изменить хост БД");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                DB.ChangeDB();
                                break;
                            default:
                                Console.WriteLine("Проверьте ввод.");
                                break;
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Проверьте ввод.");
                        break;
                }

                DB.DBContext.SaveChanges();
            }
        }

        private static void ShowAll()
        {
            Show(DB.DBContext.Tasks.ToList());
        }

        private static void Show(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            
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
                    Console.ForegroundColor = ConsoleColor.Red;

                if (t.IsDone)
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("|{0, -" + idLength + "}|{1, -" + nameLength + "}|{2, -" + dateLength + "}|    {3}    |", t.Id, t.Name, t.Deadline.ToString("g"), isD);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void Show(Task task)
        {
            var tasks = new List<Task>();
            tasks.Add(task);
            Show(tasks);
        }
        
        private static string Indent(int count)
        {
            return "".PadLeft(count);
        }
    }
}