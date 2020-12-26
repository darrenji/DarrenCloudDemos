using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    public static class ConsoleInput
    {
        public static bool TryReadDecimal(string prompt, out Decimal value)
        {
            value = default(Decimal);

            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    return false;
                }

                try
                {
                    value = Convert.ToDecimal(input);
                    return true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("The input is not a valid decimal.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The input is not a valid decimal.");
                }
            }
        }
    }
}
