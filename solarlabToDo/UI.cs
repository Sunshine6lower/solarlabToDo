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
            
            using (ApplicationContext db = new ApplicationContext())
            {
                Task task1 = new Task {Deadline = new DateTime(2020, 2, 20), IsDone = false, Name = "birthday"};
                Task task2 = new Task {Deadline = new DateTime(2020, 3, 8), IsDone = true, Name = "woman"};


                db.Tasks.Add(task1);
                db.Tasks.Add(task2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");
 
                // получаем объекты из бд и выводим на консоль
                var tasks = db.Tasks.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Task u in tasks)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Deadline}, {u.IsDone}");
                }
            }
            Console.Read();
//
//            while (true)
//            {
//                Console.WriteLine();
//            }
        }
    }
}