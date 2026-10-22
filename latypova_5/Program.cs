using System;
using System.Collections.Generic;
using System.IO;
using latypova_5.Structs;
using latypova_5.Files;

namespace latypova_5
{
    internal class Program
    {
        public static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
        }

        #region Задание 1

        public static void Task1()
        {
            Console.WriteLine("Задание 1 - Перемешать 32 пары картинок");

            string filePath = "..\\..\\Images";

            if (!Directory.Exists(filePath))
            {
                Console.WriteLine("Ошибка: папки с картинками не существует");
                return;
            }

            string[] files = Directory.GetFiles(filePath);

            if (files.Length != 32)
            {
                Console.WriteLine("Ошибка: в папке не найдено 32 картинок");
                return;
            }

            var pictures = new List<string>(64);

            foreach (var file in files)
            {
                pictures.Add(file);
                pictures.Add(file);
            }

            Console.WriteLine("Исходный список: ");
            PrintPictures(pictures);

            MixList(pictures);

            Console.WriteLine("Перемешанный список: ");
            PrintPictures(pictures);

            Console.ReadKey();
        }

        /// <summary>
        /// Метод, который выводит номера и название картинок (тоже их номера) списка на консоль
        /// </summary>
        /// <param name="pictures">Список с картинками для вывода</param>
        public static void PrintPictures(List<string> pictures)
        {
            for (int i = 0; i < pictures.Count; i++)
            {
                Console.WriteLine($"{i+1}: {Path.GetFileName(pictures[i])}");
            }
        }

        /// <summary>
        /// Метод, который случайным образом перемешивает исходный список
        /// </summary>
        /// <param name="pictures">Список, который нужно перемешать</param>
        public static void MixList(List<string> pictures)
        {
            int number1 = pictures.Count;

            while(number1 > 1)
            {
                int number2 = random.Next(number1--);

                (pictures[number1], pictures[number2]) = (pictures[number2], pictures[number1]);
            }
        }

        #endregion

        #region Задание 2

        public static void Task2()
        {
            Console.WriteLine("Задание 2 - Взаимодействие со студентом");

            string filePath = "..\\..\\Files\\Students.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Ошибка: файла не существует");
                return;
            }

            var students = StudentsFromFile(filePath);

            if (students == null || students.Count == 0)
            {
                Console.WriteLine("Ошибка: в файле нет студентов");
                return;
            }

            Console.WriteLine("Исходный список студентов: ");
            PrintDictionary(students);

            string input;

            do
            {
                Console.WriteLine("Введите команду (Новый студент, Удалить, Сортировать, Выйти): ");
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "новый студент":
                        NewStudent(students);

                        Console.WriteLine("Текущий список студентов: ");
                        PrintDictionary(students);

                        break;

                    case "удалить":
                        if (RemoveStudent(students))
                        {
                            Console.WriteLine("Текущий список студентов: ");
                            PrintDictionary(students);
                        }
                        break;

                    case "сортировать":
                        SortScore(students);
                        break;

                    case "выйти":
                        break;

                    default:
                        Console.WriteLine("Ошибка: неверная команда");
                        break;
                }
            }
            while (input != "выйти");

            Console.ReadKey();
        }

        /// <summary>
        /// Читает данные о студентах из файла и создаёт словарь, где ключ - Guid, а значение - структура Student
        /// </summary>
        /// <param name="filePath">Переменная, содержащая в себе путь к файлу со студентами</param>
        /// <returns>Словарь студентов или null, если данные нарушены</returns>
        public static Dictionary<Guid, Student> StudentsFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var students = new Dictionary<Guid, Student>();

            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(';');

                if (parts.Length != 5 || !int.TryParse(parts[2], out var birthYear) || !int.TryParse(parts[4], out var score))
                {
                    Console.WriteLine($"Ошибка: в {i+1} строке файла у студента неправильно введены данные");
                    return null;
                }

                students.Add(Guid.NewGuid(), new Student(parts[0], parts[1], birthYear, parts[3], score));
            }

            return students;
        }

        /// <summary>
        /// Выводит на экран список студентов из словаря
        /// </summary>
        /// <param name="students">Словарь со студентами</param>
        public static void PrintDictionary(Dictionary<Guid, Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Key} - {student.Value.PrintStudent()}");
            }
        }

        /// <summary>
        /// Метод, который создает нового студента, запрашивая его данные. Добавляется в словарь
        /// </summary>
        /// <param name="students">Словарь, в который нужно добавить студента</param>
        public static void NewStudent(Dictionary<Guid, Student> students)
        {
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();

            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();

            Console.WriteLine("Введите год рождения: ");
            if (!int.TryParse(Console.ReadLine(), out var birthYear))
            {
                Console.WriteLine("Ошибка: вы ввели не число");
                return;
            }

            Console.WriteLine("Введите экзамен: ");
            string exam = Console.ReadLine();

            Console.WriteLine("Введите баллы: ");
            if (!int.TryParse(Console.ReadLine(), out var score))
            {
                Console.WriteLine("Ошибка: вы ввели не число");
                return;
            }

            var newKey = Guid.NewGuid();

            students.Add(newKey, new Student(surname, name, birthYear, exam, score));

            Console.WriteLine($"Студент с ID: {newKey} добавлен");
        }

        /// <summary>
        /// Метод, который ищет студента по фамилии и имени и удаляет его из словаря
        /// </summary>
        /// <param name="students">Словарь со студентами</param>
        /// <returns>True, если студент был найден и удален, иначе False</returns>
        public static bool RemoveStudent(Dictionary<Guid, Student> students)
        {
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();

            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();

            foreach (var student in students)
            {
                if (student.Value.Surname == surname && student.Value.Name == name)
                {
                    students.Remove(student.Key);
                    Console.WriteLine("Указанный студент удален");

                    return true;
                }
            }

            Console.WriteLine("Ошибка: студента с такой фамилией и именем нет в файле");

            return false;
        }

        /// <summary>
        /// Сортирует студентов по возрастанию их баллов, выводит отсортированный список на консоль
        /// </summary>
        /// <param name="students">Словарь со студентами</param>
        public static void SortScore(Dictionary<Guid, Student> students)
        {
            var list = new List<KeyValuePair<Guid, Student>>(students);

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (list[j].Value.Score > list[j + 1].Value.Score)
                    {
                        var copy = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = copy;
                    }
                }
            }
            
            Console.WriteLine("Студенты, отсортированные по баллам: ");

            foreach (var student in list)
            {
                Console.WriteLine($"{student.Key} - {student.Value.Surname} {student.Value.Name}, {student.Value.BirthYear}г., экзамен - {student.Value.Exam}, баллы - {student.Value.Score}");
            }
        }

        #endregion

        #region Задание 3

        public static void Task3()
        {
            Console.WriteLine("Задание 3 - Бабули, болезни и больницы");

            var hospitals = CreateHospitals();
            var grandmas = Grandmas();

            if (grandmas.Count == 0)
            {
                Console.WriteLine("Бабуль больше нет");
                return;
            }

            var queueGrandmas = Distribution(grandmas, hospitals);

            PrintGrandmas(queueGrandmas);
            PrintHospitals(hospitals);

            Console.ReadKey();
        }

        /// <summary>
        /// Создаёт и возвращает стек с больницами, их лечимыми болезнями, вместимостью
        /// </summary>
        /// <returns>Стек объектов Hospital</returns>
        public static Stack<Hospital> CreateHospitals()
        {
            var hospitals = new Stack<Hospital>();

            hospitals.Push(new Hospital("Городская клиническая больница №7", new List<string> { "диабет", "астма", "пневмония", "инсульт" }, 15));
            hospitals.Push(new Hospital("Республиканская клиническая больница", new List<string> { "язва желудка", "острый панкреатит", "отслоение сетчатки", "диабет" }, 25));
            hospitals.Push(new Hospital("Хирургическая клиника", new List<string> { "артроз", "мочекаменная болезнь", "инсульт" }, 18));

            return hospitals;
        }

        /// <summary>
        /// Запрашивает у бабули её имя, позволяет прервать ввод командой "Выйти"
        /// </summary>
        /// <returns>Введенное имя или null (если пользователь решил выйти)</returns>
        public static string GrandmaName()
        {
            string input;

            do
            {
                Console.WriteLine("Введите имя бабушки (или введите 'Выйти' для завершения): ");
                input = Console.ReadLine();

                if (input.ToLower() == "выйти")
                {
                    return null;
                }
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: вы не ввели имя");
                }
            }
            while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        /// <summary>
        /// Запрашивает возраст бабушки до тех пор, пока ввод не будет корректным
        /// </summary>
        /// <returns>Возраст бабушки</returns>
        public static byte GrandmaAge()
        {
            byte age;

            Console.WriteLine("Введите возраст: ");

            while (!byte.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Ошибка: введите возраст числом");
            }

            return age;
        }

        /// <summary>
        /// Запрашивает список болезней и лекарств бабушки, метод завершается по команде "Выйти"
        /// </summary>
        /// <returns>Список объектов Disease</returns>
        public static List<Disease> GrandmaDiseases()
        {
            var diseases = new List<Disease>();

            Console.WriteLine("Введите болезни и лекарства бабушки через точку с запятой.\nЕсли болезней нет, введите 'Выйти': ");

            string input;

            do
            {
                input = Console.ReadLine();

                if (input.ToLower() != "выйти")
                {
                    string[] parts = input.Split(';');

                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Ошибка: введенная строка не соответствует условиям, надо ввести так: 'болезнь;лекарство'");
                    }
                    else
                    {
                        diseases.Add(new Disease(parts[0], parts[1]));
                    }
                }
            }
            while (input.ToLower() != "выйти");

            return diseases;
        }

        /// <summary>
        /// Метод, которй управляет созданием бабуль. Запрашивает данные до тех пор, пока пользователь не завершит ввод, и добавляет в очередь
        /// </summary>
        /// <returns>Очередь бабуль</returns>
        public static Queue<Grandma> Grandmas()
        {
            var grandmas = new Queue<Grandma>();

            string name = GrandmaName();

            while (name != null)
            {
                byte age = GrandmaAge();
                var diseases = GrandmaDiseases();

                grandmas.Enqueue(new Grandma(name, age, diseases));

                Console.WriteLine("Бабка добавлена в очередь");

                name = GrandmaName();
            }

            return grandmas;
        }

        /// <summary>
        /// Метод, который ищет самую подходящую больницу для конкретной бабушки с учётом её болезней и заполненности больницы
        /// </summary>
        /// <param name="grandma">Объект бабули</param>
        /// <param name="hospitals">Стек возможных подходящих больниц</param>
        /// <returns>Самая подходящая больница или null, если она чересчур заполнена</returns>
        public static Hospital FindHospital(Grandma grandma, Stack<Hospital> hospitals)
        {
            Hospital bestHospital = null;

            foreach (Hospital hospital in hospitals)
            {
                if (hospital.CountNow >= hospital.Capacity)
                {
                    continue;
                }
                if (grandma.Diseases.Count == 0)
                {
                    return hospital;
                }
                if (PercentOfHospital(grandma, hospital) > 0.5)
                {
                    if (bestHospital == null || hospital.CountNow < bestHospital.CountNow)
                    {
                        bestHospital = hospital;
                    }
                }
            }

            return bestHospital;
        }

        /// <summary>
        /// Метод, который уже распределяет бабушек по больницам и обновляет данные по загруженностям в них
        /// </summary>
        /// <param name="grandmas">Очередь бабуль для распределения по больницам</param>
        /// <param name="hospitals">Стек больниц</param>
        /// <returns>Новая очередь бабуль, обновляется NameOfHospital - то есть, их нахождение после распределения</returns>
        public static Queue<Grandma> Distribution(Queue<Grandma> grandmas, Stack<Hospital> hospitals)
        {
            var queueGrandma = new Queue<Grandma>();

            while (grandmas.Count > 0)
            {
                Grandma grandma = grandmas.Dequeue();
                Hospital hospital = FindHospital(grandma, hospitals);

                if (hospital != null)
                {
                    hospital.CountNow++;
                    grandma.NameOfHospital = hospital.Name;
                }

                queueGrandma.Enqueue(grandma);
            }

            return queueGrandma;
        }

        /// <summary>
        /// Вычисляет процент болезней бабушки, который лечит больница
        /// </summary>
        /// <param name="grandma">Объект бабули</param>
        /// <param name="hospital">Объект больницы</param>
        /// <returns>Дробное число от 0.0 до 1.0</returns>
        public static double PercentOfHospital(Grandma grandma, Hospital hospital)
        {
            if (grandma.Diseases.Count == 0)
            {
                return 0;
            }

            int percent = 0;

            foreach (Disease disease in grandma.Diseases)
            {
                if (hospital.ListOfDiseases.Contains(disease.Name.ToLower()))
                {
                    percent++;
                }
            }

            return (double)percent / grandma.Diseases.Count;
        }

        /// <summary>
        /// Выводит на экран итоговый список всех бабуль с информацией о них, о болезнях и о местах лечения
        /// </summary>
        /// <param name="grandmas">Очередь бабуль</param>
        public static void PrintGrandmas(Queue<Grandma> grandmas)
        {
            Console.WriteLine("Список всех бабуль: ");

            foreach (Grandma grandma in grandmas)
            {
                Console.WriteLine($"{grandma.Name}, {grandma.Age} лет, болезни: {PrintDiseases(grandma)}, где лечится: {grandma.NameOfHospital} ");
            }
        }

        /// <summary>
        /// Метод, который формирует строку со списком болезней и лекарств бабули. Используется для красивого вывода метода PrintGrandmas
        /// </summary>
        /// <param name="grandma">Объект бабули</param>
        /// <returns>Отформатированная строка с болезнями и лекарствами</returns>
        public static string PrintDiseases(Grandma grandma)
        {
            if (grandma.Diseases.Count == 0)
            {
                return "Зашла только спросить";
            }

            var parts = new List<string>();
            
            foreach (Disease disease in grandma.Diseases)
            {
                parts.Add($"{disease.Name} (лекарство: {disease.MedicationName})");            
            }

            return string.Join("; ", parts);
        }

        /// <summary>
        /// Выводит на консоль список всех больниц, их специализацию (что лечат), количество занятых мест и загруженность в процентах
        /// </summary>
        /// <param name="hospitals">Стек больниц</param>
        public static void PrintHospitals(Stack<Hospital> hospitals)
        {
            Console.WriteLine("Список имеющихся больниц: ");

            foreach (Hospital hospital in hospitals)
            {
                double percent = (double)hospital.CountNow / hospital.Capacity * 100;

                Console.WriteLine($"{hospital.Name}, лечит: {string.Join(("; "), hospital.ListOfDiseases)}, " +
                    $"сейчас там {hospital.CountNow} человек, больница заполнена на {percent:F0}%");
            }
        }

        #endregion
    }
}
