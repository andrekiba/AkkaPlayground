using System;
using System.Drawing;
using Console = Colorful.Console;

namespace AkkaPlayground
{
    public static class ColorConsole
    {
        public static void WriteWhite(string message)
        {
            Console.WriteLine(message, Color.White);
        }

        public static void WriteRed(string message)
        {
            Console.WriteLine(message, Color.Red);
        }

        public static void WriteGreen(string message)
        {
            Console.WriteLine(message, Color.MediumSeaGreen);
        }

        public static void WriteOrange(string message)
        {
            Console.WriteLine(message, Color.DarkOrange);
        }

        public static void WriteViolet(string message)
        {
            Console.WriteLine(message, Color.BlueViolet);
        }
    }
}
