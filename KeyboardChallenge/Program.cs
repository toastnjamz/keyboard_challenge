using System;
using System.Linq;
using System.IO;

namespace Keyboard.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            // How can you make this array nullable? Nullable<char[]>?
            char[] keyboard =
            {
                'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
                'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', '\0',
                'z', 'x', 'c', 'v', 'b', 'n', 'm', '\0', '\0', '\0'

            };

            Console.WriteLine("Original keyboard:");
            Print(keyboard);

            var inputText = File.ReadAllText(@"input.txt");
            string[] inputArray = inputText.Split(',');
            foreach (string input in inputArray)
            {
                if (input.Contains("L"))
                {
                    int leftDistance = (int)char.GetNumericValue(input[1]);
                    LeftShift(ref keyboard, leftDistance);
                    Print(keyboard);

                }
                else if (input.Contains("R"))
                {
                    int rightDistance = (int)char.GetNumericValue(input[1]);
                    RightShift(ref keyboard, rightDistance);
                    Print(keyboard);

                }
                else if (input.Contains("V"))
                {
                    VFlip(ref keyboard);
                    Print(keyboard);
                }
                else
                {
                    HFlip(ref keyboard);
                    Print(keyboard);
                }
            }
        }

        // Prints a visual keyboard to the screen (as pictured above)
        public static void Print(char[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 10 || i == 20)
                {
                    Console.WriteLine();
                    Console.Write(array[i]);
                }
                else
                {
                    Console.Write(array[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        // Skips n elements, then appends them to the end of the array
        private static char[] LeftShift(ref char[] keyboard, int distance)
        {
            Console.WriteLine("Left-shifted keyboard by {0}:", distance);
            keyboard = keyboard.Skip(distance).Concat(keyboard.Take(distance)).ToArray();
            return keyboard;
        }

        // Skips the last n elements, then appends all but the last n elements
        private static char[] RightShift(ref char[] keyboard, int distance)
        {
            Console.WriteLine("Right-shifted keyboard by {0}:", distance);
            keyboard = keyboard.Skip(keyboard.Length - distance).Concat(keyboard.Take(keyboard.Length - distance)).ToArray();
            return keyboard;
        }

        // Skips all but the last row, then appends the middle and the top row (hardcoded and crappy)
        // Is there anyway to use TakeWhile() for selecting a range of indexes in an array?
        private static char[] VFlip(ref char[] keyboard)
        {
            Console.WriteLine("Vertically-flipped keyboard:");
            keyboard = keyboard.Skip(20).Concat(keyboard.Skip(10).Take(10)).Concat(keyboard.Take(10)).ToArray();
            return keyboard;
        }

        // Especially crappy and hardcoded - I should have used a 10x3 array
        private static char[] HFlip(ref char[] keyboard)
        {
            Console.WriteLine("Horizontally-flipped keyboard:");
            keyboard = keyboard.Skip(5).Take(5).Concat(keyboard.Skip(4).Take(1)).Concat(keyboard.Take(4))
                       .Concat(keyboard.Skip(15).Take(5)).Concat(keyboard.Skip(14).Take(1)).Concat(keyboard.Skip(10).Take(4))
                       .Concat(keyboard.Skip(25).Take(5)).Concat(keyboard.Skip(24).Take(1)).Concat(keyboard.Skip(20).Take(4)) 
                       .ToArray();
            return keyboard;
        }
    }
}

