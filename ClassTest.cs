using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1
{
    public class ClassTest
    {
        public int LenghtMax(string content)
        {
            string[] words = content.Split();
            int maxLength = words.Select(x => x.Length).Max();
            return maxLength;
        }
        public int LenghtString(string content)
        {
            int lenght = content.Length;
            return lenght;
        }

        public int Summ(int a, int b)
        {
            int summ;
            summ = a + b;
            return summ;
        }

        public int Division(int a, int b)
        {
            int division;
            division = a / b;
            return division;
        }

        public static bool NotEqueal(int a, int b)
        {
            if (a != b)
            {
                return false;
            }
            else return true;
        }

        public int Subtraction(int a, int b)
        {
            int substraction;
            substraction = a - b;
            return substraction;
        }

        public int Multiplication(int a, int b)
        {
            int multiplication;
            multiplication = a * b;
            return multiplication;
        }

        public static int KakayaTochka(string content) //когда ставиться первая точка
        {
            ClassTest classTest = new ClassTest();
            int lenghtString = classTest.LenghtString(content);
            int index = 0;
            for (int i=0; i<lenghtString; i++)
            {
                if (content[i].ToString() == ".")
                {
                    index = i + 1;
                    break;
                }
            }
            return index;
        }
    }
}
