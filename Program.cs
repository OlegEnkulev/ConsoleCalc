using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        static double a, b, result;
        static string oper;
        static int iter = 1;
        static bool isOperCorrect = true;
        static bool isABCorrect = true;

        static List<CalcLib.CalcHistory> History = new List<CalcLib.CalcHistory>();

        static int func;
        static bool isFuncCorrect;

        static string saveDir = Environment.CurrentDirectory + "/save.txt";
        static int loadedHistory = 0;

        static void Main(string[] args)
        {
            FileInfo saveHistory = new FileInfo(saveDir);
            if (!saveHistory.Exists)
            {
                saveHistory.Create();
            }

            StreamReader sr = new StreamReader(saveDir);
            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] lineList = line.Split(':');

                History.Add(new CalcLib.CalcHistory(Convert.ToDouble(lineList[0]), lineList[1], Convert.ToDouble(lineList[2]), Convert.ToDouble(lineList[3])));
                loadedHistory++;
            }
            sr.Close();

            Console.WriteLine("Загружено " + loadedHistory + " записей истории.");

            while (true)
            {
                Console.WriteLine("Выберите Функцмю:");
                Console.WriteLine("1. Посчитать");
                Console.WriteLine("2. Вывести историю");

                isFuncCorrect= true;

                try
                {
                    func = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Функция выбрана неправильно!");
                    Console.Clear();
                    isFuncCorrect = false;
                }

                if (isFuncCorrect)
                {
                    switch(func)
                    {
                        case 1:
                            CalculationsView();
                            break;
                        case 2:
                            HistoryView();
                            break;
                        default:
                            Console.WriteLine("Функция выбрана неправильно!");
                            Console.Clear();
                            isFuncCorrect = false;
                            break;
                    }
                }
            }
        }

        static void HistoryView()
        {
            Console.Clear();

            Console.WriteLine("История:");

            for(int i = 0; i < History.Count; i++)
            {
                CalcLib.CalcHistory history = History[i];

                Console.WriteLine(i + 1 + ". " + history.a + " " + history.oper + " " + history.b + " = " + history.result);
            }

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        static void CalculationsView()
        {
            Console.Clear();

            Console.WriteLine("Калькулятор: Итерация - " + Convert.ToString(iter));

            Console.WriteLine("Введите a: ");
            try
            {
                a = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Переменная указана не верно!");
                isABCorrect = false;
            }

            if (isABCorrect)
            {
                Console.WriteLine("Введите b: ");
                try
                {
                    b = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Переменная указана не верно!");
                    isABCorrect = false;
                }
            }

            if (isABCorrect)
            {
                Console.WriteLine("Введите Операнд ( +, -, *, /): ");
                oper = Console.ReadLine();

                switch (oper)
                {
                    case "+":
                        result = CalcLib.Calculations.Summ(a,b);
                        break;
                    case "-":
                        result = CalcLib.Calculations.Minus(a, b);
                        break;
                    case "*":
                        result = CalcLib.Calculations.Multi(a, b);
                        break;
                    case "/":
                        result = CalcLib.Calculations.Delit(a, b);
                        break;
                    default:
                        Console.WriteLine("Операнд указан не верно!");
                        isOperCorrect = false;
                        break;
                }

                if (isOperCorrect)
                {
                    Console.WriteLine(Convert.ToString(a) + " " + oper + " " + Convert.ToString(b) + " = " + Convert.ToString(result));
                }
            }

            if (isOperCorrect && isABCorrect)
            {
                History.Add(new CalcLib.CalcHistory(a, oper, b, result));
                string saveString = "\n" + a + ":" + oper + ":" + b + ":" + result;
                File.AppendAllText(saveDir, saveString);
            }

            isABCorrect = true;
            isOperCorrect = true;
            iter++;

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
