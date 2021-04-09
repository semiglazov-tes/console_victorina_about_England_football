using System;
using System.IO;
using System.Linq;

namespace ConsoleBot
{
    class Program 
    {
        static void Main(string[] args)
        {
            var questionsAndAnswersArrayData = File.ReadAllLines("VictorinaQuestions.txt");
            var questionsAndAnswersList = questionsAndAnswersArrayData
                .Select(s => s.Split("/"))
                .Select(s => (s[0], s[1]))
                .ToList();
            var trueAnswers = 0;
            var random = new Random();
            var count = questionsAndAnswersList.Count;
            while (true)
            {
                if (count<1)
                {
                    count = questionsAndAnswersList.Count;
                }
                var index = random.Next(questionsAndAnswersList.Count - 1);
                var questionAndAnswer = questionsAndAnswersList[index];
                questionsAndAnswersList.RemoveAt(index);
                questionsAndAnswersList.Add(questionAndAnswer);
                count -= 1;
                var opened = 0;
                while (opened < questionAndAnswer.Item2.Length)
                {
                    var promptForAnswer = questionAndAnswer.Item2
                   .Substring(0, opened)
                   .PadRight(questionAndAnswer.Item2.Length, '_');
                    Console.WriteLine(questionAndAnswer.Item1);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(promptForAnswer);
                    Console.ResetColor();
                    var tryAnswer = Console.ReadLine();
                    if (tryAnswer != null && tryAnswer.Trim().ToLowerInvariant() == questionAndAnswer.Item2.Trim().ToLowerInvariant())
                    {
                        trueAnswers += 1;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Правильно! Это-{questionAndAnswer.Item2}");
                        Console.ResetColor();
                        Console.WriteLine($"Колличество правильных ответов:{trueAnswers}");
                        opened = -1;
                        break;
                    }
                    else
                    {
                        opened += 1;
                    }
                }
                if (opened != -1)
                {
                    Console.WriteLine($"Никто не угадал! Это был-{questionAndAnswer.Item2}");
                }
            }
               
        }
    }
}
