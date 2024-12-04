using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Для поддержки цветного вывода
        Console.Title = "Поиск уникальных элементов";

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Добро пожаловать в программу поиска уникальных элементов! ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("Доступные команды:");
            Console.WriteLine("1. Введите 'help' для вывода инструкции.");
            Console.WriteLine("2. Введите 'example' для вывода примера правильного ввода.");
            Console.WriteLine("3. Введите элементы массива через запятую без пробелов.");
            Console.WriteLine("-----------------------------------------------------------");

            Console.Write("Введите команду или элементы массива: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "help")
            {
                ShowHelp();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                continue;
            }
            else if (input.ToLower() == "example")
            {
                ShowExample();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                continue;
            }

            // Проверка корректности ввода
            if (!IsValidInput(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: Некорректный ввод. Пожалуйста, введите числа через запятую без пробелов.");
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для повторного ввода...");
                Console.ReadKey();
                continue;
            }

            // Преобразуем введенную строку в массив целых чисел
            int[] nums = input.Split(',')
                              .Select(int.Parse)
                              .ToArray();

            // Проверка условия задачи
            if (!IsValidArray(nums))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: В массиве должны быть ровно два элемента, которые появляются только один раз, а все остальные — ровно дважды.");
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для повторного ввода...");
                Console.ReadKey();
                continue;
            }

            // Вызов функции для поиска уникальных элементов
            int[] result = FindUniqueNumbers(nums);

            // Вывод результата
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Результат: [{result[0]}, {result[1]}]");
            Console.ResetColor();

            Console.WriteLine("Нажмите любую клавишу для повторного ввода или Esc для выхода...");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }

    static void ShowHelp()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║ Инструкция:                                                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine("1. Введите элементы массива через запятую без пробелов.");
        Console.WriteLine("2. Убедитесь, что ровно два элемента появляются только один раз, а все остальные — ровно дважды.");
        Console.WriteLine("-----------------------------------------------------------");
    }

    static void ShowExample()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║ Пример правильного ввода:                                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine("Для получения результата [3, 5], введите: 1,2,1,3,2,5");
        Console.WriteLine("-----------------------------------------------------------");
    }

    static bool IsValidInput(string input)
    {
        // Проверка на наличие запятых и отсутствие пробелов
        return input.All(c => c == ',' || char.IsDigit(c));
    }

    static bool IsValidArray(int[] nums)
    {
        // Проверка условия задачи
        var counts = nums.GroupBy(n => n)
                         .Select(g => new { Number = g.Key, Count = g.Count() })
                         .ToList();

        int uniqueCount = counts.Count(c => c.Count == 1);
        int doubleCount = counts.Count(c => c.Count == 2);

        return uniqueCount == 2 && doubleCount == (nums.Length - 2) / 2;
    }

    static int[] FindUniqueNumbers(int[] nums)
    {
        // Шаг 1: Найти XOR всех элементов
        int xorResult = 0;
        foreach (int num in nums)
        {
            xorResult ^= num;
        }

        // Шаг 2: Найти любой бит, который отличается в двух уникальных элементах
        // Мы используем самый младший установленный бит
        int diffBit = xorResult & -xorResult;

        // Шаг 3: Разделить массив на две группы и найти уникальные элементы в каждой группе
        int num1 = 0, num2 = 0;
        foreach (int num in nums)
        {
            if ((num & diffBit) != 0)
            {
                num1 ^= num;
            }
            else
            {
                num2 ^= num;
            }
        }

        return new int[] { num1, num2 };
    }
}