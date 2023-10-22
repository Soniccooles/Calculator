using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Programm1;

public class Programm // Класс программ
{
    static void Main(string[] args) //Функция главная
    {
        while (true)
        {
            string input = Console.ReadLine().Replace(" ", "");
            Console.WriteLine(string.Join(' ', WriteOperands(input)));
            Console.WriteLine(string.Join(' ', WriteNumbers(input)));
            Console.WriteLine(WriteResult(WriteOperands(input), WriteNumbers(input)));
        }
    }

    static List<char> WriteOperands(string input)
    {
        var operands = new List<char>();
        foreach (var element in input)
        {
            if ((element == '.') || (element == ','))
                continue;
            if (!Char.IsDigit(element))
                operands.Add(element);
        }
        return operands;
    }
    static List<float> WriteNumbers(string input)
    {
        List<float> numberList = new List<float>();
        string number = "";

        foreach (var element in input + " ")
        {
            if (Char.IsDigit(element) || (element == ',')) number += element;

            else
            {
                numberList.Add(float.Parse(number));
                number = "";
            }
        }
        return numberList;
    }
    
    public static float WriteResult(List<char> operands, List<float> numberList)
    {
        while (operands.Count > 0)
        {
            int operandsIndex = -1;
            if (operands.Contains('/') || operands.Contains('*'))
            {
                int multiplyIndex = operands.IndexOf('*');
                int divideIndex = operands.IndexOf('/');
                operandsIndex = divideIndex != -1 && multiplyIndex != -1 
                    ? multiplyIndex > divideIndex
                        ? divideIndex
                        : multiplyIndex
                    : divideIndex == -1
                        ? multiplyIndex
                        : divideIndex;
            }
            if (operandsIndex == -1)
            {
                operandsIndex = 0;
            }
       
            char op = operands[operandsIndex];
            float num1 = numberList[operandsIndex];
            float num2 = numberList[operandsIndex + 1];
            float result = Calculate(op, num1, num2);
            numberList.RemoveAt(operandsIndex + 1);
            numberList.RemoveAt(operandsIndex);
            operands.RemoveAt(operandsIndex);
            numberList.Insert(operandsIndex, result);
        }
        return numberList[0];
    }    
    static float Calculate(char op, float num1, float num2)
    {
        switch (op)
        {
            case '+': return num1 + num2;
            case '-': return num1 - num2;
            case '*': return num1 * num2;
            case '/': return num1 / num2;
        }
        throw new Exception("I don't know this operation!");
    }
}