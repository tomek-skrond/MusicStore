namespace MusicStore.Extensions.Chainer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography;
    using System.Windows.Input;
    public sealed class ConsoleChainer
    {
        public ConsoleChainer Clear()
        {
            Console.Clear();
            return this;
        }

        public ConsoleChainer BreakLine()
        {
            Console.WriteLine();
            return this;
        }

        public ConsoleChainer PressToContinue()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            return this;
        }

        public ConsoleChainer SetTitle(string title)
        {
            Console.Title = title;
            return this;
        }

        public ConsoleChainer RetrieveInput(string text, out string input)
        {
            Console.Write(text);

            input = Console.ReadLine();

            return this;
        }


        public ConsoleChainer DisplayTextInRow(string text, string format = null)
        {
            Console.Write(format ?? $"{{0}}", text);
            return this;
        }

        public ConsoleChainer DisplayTextInColumn(string text, string format = null)
        {
            Console.WriteLine(format ?? $"{{0}}", text);
            return this;
        }

        public ConsoleChainer DisplayTextInRow(IEnumerable<string> text, string format = null)
        {
            foreach (var message in text)
                Console.Write(format ?? $"{{0}}", message);

            return this;
        }

        public ConsoleChainer DisplayTextInColumn(IEnumerable<string> text, string format = null)
        {
            foreach (var message in text)
                Console.WriteLine(format ?? $"{{0}}", message);

            return this;
        }
      
        public ConsoleChainer DisplayTextInColumnLoop(int howMany,string msg,string format = null)
        {
            var i = 0;
            while (i < howMany)
            {
                Console.WriteLine(msg);
                i++;
            }
            return this;
        }
        public ConsoleChainer DisplayTextInRowLoop(int howMany, string text,string format = null)
        {
            var i = 0;
            while (i < howMany)
            {
                foreach (var message in text)
                    Console.Write(format ?? $"{{0}}", message);
                i++;
            }
            return this;
        }
        
        public T ParseInput<T>(ref string text, Validator<T> validator) where T : struct
        {
            if (validator.Invoke(text, true, out var value))
                return value;


            DisplayTextInColumn($"\nProvided parameter did not match any of the values. Will proceed with default value.");
            PressToContinue();
            return default;
        }
        public bool ParseInput(ref string text, BoolValidator validator)
        {
            if (validator.Invoke(text, out var value))
                return value;


            DisplayTextInColumn($"\nProvided parameter did not match any of the values. Will proceed with default value.");
            PressToContinue();
            return default;
        }

        public delegate bool Validator<T>(string? value, bool ignoreCase, out T enumValue);
        public delegate bool BoolValidator(string? value, out bool result);
    }
}