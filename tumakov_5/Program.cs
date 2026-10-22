using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tumakov_5
{
    internal class Program
    {
        public static readonly Random random = new Random(); //пишем так, чтобы random = new Random() присваился только один раз,
                                                             //для более разных значений в матрицах в упражнении 6.2

        static void Main(string[] args)
        {
            Task6_1();
            Task6_2();
            Task6_3();
            Homework6_1();
            Homework6_2();
            Homework6_3();
        }

        #region Упражнение 6.1

        public static void Task6_1()
        {
            Console.WriteLine("Упражнение 6.1 - Посчитать количество гласных и согласных в файле");

            string filePath = "..\\..\\Files\\textFile.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Ошибка: файла не существует");
                return;
            }

            var readedText = File.ReadAllLines(filePath);

            char[] vowelLetters = { 'у', 'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю', 'ё' };
            char[] consonantLetters = { 'й', 'ц', 'к', 'н', 'г', 'ш', 'щ', 'з', 'х', 'ъ', 'ф', 'в', 'п', 'р', 'л', 'д', 'ж', 'ч', 'с', 'м', 'т', 'ь', 'б' };

            var res = CountLetters(readedText, vowelLetters, consonantLetters);

            Console.WriteLine($"Количество гласных в строках файла: {res.CountVowels}\nКоличество согласных в строках файла: {res.CountConsonants}");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод, который вычисляет количество гласных и согласных букв в строках файла
        /// </summary>
        /// <param name="letters">Строки из данного нам файла</param>
        /// <param name="vowels">Массив гласных букв</param>
        /// <param name="consonants">Массив согласных букв</param>
        /// <returns>Два числа: количество гласных и согласных букв</returns>
        public static (int CountVowels, int CountConsonants) CountLetters(string[] letters, char[] vowels, char[] consonants)
        {
            int countVowels = 0;
            int countConsonants = 0;

            foreach (string letter in letters)
            {
                foreach (char ch in letter.ToLower())
                {
                    if (vowels.Contains(ch))
                    {
                        countVowels++;
                    }
                    else if (consonants.Contains(ch))
                    {
                        countConsonants++;
                    }
                }
            }

            return (countVowels, countConsonants);
        }

        #endregion

        #region Упражнение 6.2

        public static void Task6_2()
        {
            Console.WriteLine("Упражнение 6.2 - Написать программу, реализующую умножение двух матриц");
            Console.WriteLine("Введите количество строк и столбцов в первой матрице (через Enter): ");

            if (!int.TryParse(Console.ReadLine(), out var row1) || !int.TryParse(Console.ReadLine(), out var column1))
            {
                Console.WriteLine("Ошибка: вы ввели не число");
                return;
            }

            Console.WriteLine("Введите количество строк и столбцов во второй матрице (через Enter): ");

            if (!int.TryParse(Console.ReadLine(), out var row2) || !int.TryParse(Console.ReadLine(), out var column2))
            {
                Console.WriteLine("Ошибка: вы ввели не число");
                return;
            }

            if (column1 != row2)
            {
                Console.WriteLine("Ошибка: матрицы нельзя умножить из-за невыполнения правила умножения");
                return;
            }

            int[,] matrix1 = RandomMatrix(row1, column1);
            int[,] matrix2 = RandomMatrix(row2, column2);
            int[,] resultMatrix = MatrixMultiplication(matrix1, matrix2);

            Console.WriteLine("Первая матрица: ");
            PrintMatrix(matrix1);
            Console.WriteLine("Вторая матрица: ");
            PrintMatrix(matrix2);
            Console.WriteLine("Результат умножения двух матриц:");
            PrintMatrix(resultMatrix);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод для создания матрицы со случайными числами
        /// </summary>
        /// <param name="row">Число строк в матрице</param>
        /// <param name="column">Число столбцов в матрице</param>
        /// <returns>Матрица, заполненная случайными значениями</returns>
        public static int[,] RandomMatrix(int row, int column)
        {
            int[,] matrix = new int[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = random.Next(-20, 31);
                }
            }

            return matrix;
        }

        /// <summary>
        ///Метод, который выводит матрицу на консоль в виде таблицы
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно вывести на консоль</param>
        public static void PrintMatrix(int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод, который выполняет умножение двух матриц
        /// </summary>
        /// <param name="matrix1">Первая матрица</param>
        /// <param name="matrix2">Вторая матрица</param>
        /// <returns>Новая матрица, которая является результатом умножения</returns>
        public static int[,] MatrixMultiplication(int[,] matrix1, int[,] matrix2)
        {
            int row1 = matrix1.GetLength(0);
            int column1 = matrix1.GetLength(1);
            int row2 = matrix2.GetLength(0);
            int column2 = matrix2.GetLength(1);
            int[,] resultMatrix = new int[row1, column2];

            for (int i = 0; i < row1; i++)
            {
                for (int j = 0; j < column2; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < column1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }

                    resultMatrix[i, j] = sum;
                }
            }

            return resultMatrix;
        }

        #endregion

        #region Упражнение 6.3

        public static void Task6_3()
        {
            Console.WriteLine("Упражнение 6.3 - Написать программу для вычисления средней температуры за год");

            int[,] temperature = RandomMatrix(12, 30);

            double[] averageTemperatures = AverageTemperatures(temperature);
            Array.Sort(averageTemperatures);

            Console.WriteLine("Упорядоченный массив средних температур: ");

            foreach (var temp in averageTemperatures)
            {
                Console.WriteLine($"{temp:F2}");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Метод, который вычисляет средние температуры за год
        /// </summary>
        /// <param name="temperature">Двумерный массив температур со строками в виде месяцев и со столбцами в виде дней</param>
        /// <returns>Массив, состоящий из 12 элементов, который содержит в себе средние температуры для каждого месяца</returns>
        public static double[] AverageTemperatures(int[,] temperature)
        {
            int monthsCount = temperature.GetLength(0);
            int daysCount = temperature.GetLength(1);

            double[] resultArray = new double[monthsCount];

            for (int i = 0; i < monthsCount; i++)
            {
                for (int j = 0; j < daysCount; j++)
                {
                    resultArray[i] += temperature[i, j];
                }

                resultArray[i] /= daysCount;
            }

            return resultArray;
        }

        #endregion

        #region Домашнее задание 6.1

        public static void Homework6_1()
        {
            Console.WriteLine("Домашнее задание 6.1 - Выполнить упражнение 6.1 с помощью List<T>");

            string filePath = "..\\..\\Files\\textFile.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Ошибка: файла не существует");
                return;
            }

            var readedText = new List<string>(File.ReadAllLines(filePath));

            var vowelLetters = new List<char> { 'у', 'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю', 'ё' };
            var consonantLetters = new List<char> { 'й', 'ц', 'к', 'н', 'г', 'ш', 'щ', 'з', 'х', 'ъ', 'ф', 'в', 'п', 'р', 'л', 'д', 'ж', 'ч', 'с', 'м', 'т', 'ь', 'б' };

            var res = CountLetters(readedText, vowelLetters, consonantLetters);

            Console.WriteLine($"Количество гласных в строках файла: {res.CountVowels}\nКоличество согласных в строках файла: {res.CountConsonants}");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод считает количество гласных и согласных в строках файла с помощью коллекции List
        /// </summary>
        /// <param name="letters">Список строк</param>
        /// <param name="vowels">Список гласных букв</param>
        /// <param name="consonants">Список согласных букв</param>
        /// <returns>Кортеж, содержащий количество гласных и согласных букв</returns>
        public static (int CountVowels, int CountConsonants) CountLetters(List<string> letters, List<char> vowels, List<char> consonants)
        {
            int countVowels = 0;
            int countConsonants = 0;

            foreach (string letter in letters)
            {
                foreach (char ch in letter.ToLower())
                {
                    if (vowels.Contains(ch))
                    {
                        countVowels++;
                    }
                    else if (consonants.Contains(ch))
                    {
                        countConsonants++;
                    }
                }
            }

            return (countVowels, countConsonants);
        }

        #endregion

        #region Домашнее задание 6.2

        public static void Homework6_2()
        {
            Console.WriteLine("Домашнее задание 6.2 - Выполнить упражнение 6.2 с помощью LinkedList<LinkedList<T>>");
            Console.WriteLine("Введите количество строк и столбцов в первой матрице: ");

            if (!int.TryParse(Console.ReadLine(), out var row1) || !int.TryParse(Console.ReadLine(), out var column1))
            {
                Console.WriteLine("Ошибка: вы ввели не число");
                return;
            }

            Console.WriteLine("Введите количество строк и столбцов во второй матрице: ");

            if (!int.TryParse(Console.ReadLine(), out var row2) || !int.TryParse(Console.ReadLine(), out var column2))
            {
                Console.WriteLine("Ошибка: вы ввели не число");
                return;
            }

            if (column1 != row2)
            {
                Console.WriteLine("Ошибка: матрицы нельзя умножить из-за невыполнения правила умножения");
                return;
            }

            var matrix1 = Matrix(row1, column1);
            var matrix2 = Matrix(row2, column2);
            var resultMatrix = MatrixMultiplication(matrix1, matrix2);

            Console.WriteLine("Первая матрица: ");
            PrintMatrix(matrix1);
            Console.WriteLine("Вторая матрица: ");
            PrintMatrix(matrix2);
            Console.WriteLine("Результат умножения двух матриц:");
            PrintMatrix(resultMatrix);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод для создания матрицы со случайными числами через LinkedList
        /// </summary>
        /// <param name="row">Число строк в матрице</param>
        /// <param name="column">Число столбцов в матрице</param>
        /// <returns>Матрица, заполненная случайными значениями</returns>
        public static LinkedList<LinkedList<int>> Matrix(int row, int column)
        {
            LinkedList<LinkedList<int>> matrix = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < row; i++)
            {
                var rowInMatrix = new LinkedList<int>();

                for (int j = 0; j < column; j++)
                {
                    rowInMatrix.AddLast(random.Next(-20, 31));
                }

                matrix.AddLast(rowInMatrix);
            }

            return matrix;
        }

        /// <summary>
        /// Метод, который выводит матрицу на консоль в виде таблицы
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно вывести на консоль</param>
        public static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var number in row)
                {
                    Console.Write(number + " ");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод, который выполняет умножение двух матриц (с использованием LinkedList)
        /// </summary>
        /// <param name="matrix1">Первая матрица</param>
        /// <param name="matrix2">Вторая матрица</param>
        /// <returns>Новая матрица, которая является результатом умножения</returns>
        public static LinkedList<LinkedList<int>> MatrixMultiplication(LinkedList<LinkedList<int>> matrix1, LinkedList<LinkedList<int>> matrix2)
        {
            int row1 = matrix1.Count;
            int column1 = matrix1.First.Value.Count;
            int column2 = matrix2.First.Value.Count;

            var resultMatrix = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < row1; i++)
            {
                var rowInMatrix = new LinkedList<int>();

                for (int j = 0; j < column2; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < column1; k++)
                    {
                        sum += matrix1.ElementAt(i).ElementAt(k) * matrix2.ElementAt(k).ElementAt(j);
                    }

                    rowInMatrix.AddLast(sum);
                }

                resultMatrix.AddLast(rowInMatrix);
            }
            
            return resultMatrix;
        }

        #endregion

        #region Домашнее задание 6.3

        public static void Homework6_3()
        {
            Console.WriteLine("Домашнее задание 6.3 - Написать упражнение 6.3 с использованием Dictionary");

            var temperatures = RandomTemperatures();

            double[] averageTemperatures = AverageTemperatures(temperatures);
            Array.Sort(averageTemperatures);

            Console.WriteLine("Упорядоченный массив средних температур: ");

            foreach (var temp in averageTemperatures)
            {
                Console.WriteLine($"{temp:F2}");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Метод, который создаёт словарь с температурами по дням каждого месяца
        /// </summary>
        /// <returns>Словарь, где ключом является название месяца, а значением - массив месячных температур</returns>
        public static Dictionary<string, int[]> RandomTemperatures()
        {
            var months = new string[]
            {
                "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь"
            };

            var temperatures = new Dictionary<string, int[]>();

            foreach (var month in months)
            {
                var days = new int[30];

                for (int i = 0; i < days.Length; i++)
                {
                    days[i] = random.Next(-20, 31);
                }

                temperatures.Add(month, days);
            }

            return temperatures;
        }

        /// <summary>
        /// Метод, который вычисляет средние температуры за год
        /// </summary>
        /// <param name="temperatures">Словарь температур, где ключ - название месяца, а значение - массив температур по дням</param>
        /// <returns>Массив из 12 элементов, который содержит в себе средние температуры каждого месяца</returns>
        public static double[] AverageTemperatures(Dictionary<string, int[]> temperatures)
        {
            double[] resultArray = new double[12];
            int count = 0;

            foreach (var pair in temperatures)
            {
                int sum = 0;

                foreach (var temp in pair.Value)
                {
                    sum += temp;
                }

                resultArray[count] = (double)sum / pair.Value.Length;
                count++;
            }

            return resultArray;
        }

        #endregion
    }
}