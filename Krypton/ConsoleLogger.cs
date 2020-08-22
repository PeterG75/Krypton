using System.Drawing;
using Colorful;
using Krypton.Core;

namespace Krypton
{
    public class ConsoleLogger : ILogger
    {
        public void Success(string message)
        {
            WriteLine(message, Color.Cyan, '+');
        }

        public void Warning(string message)
        {
            WriteLine(message, Color.Orange, '-');
        }

        public void Error(string message)
        {
            WriteLine(message, Color.Red, '!');
        }

        public void Info(string message)
        {
            WriteLine(message, Color.Gray, '*');
        }

        private void WriteLine(string message, Color color, char character)
        {
            Console.Write("[", Color.White);
            Console.Write(character, color);
            Console.Write("] ", Color.White);
            Console.WriteLine(message, color);
        }
    }
}