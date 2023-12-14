

using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        
        Func<int, int, int> suma = (a, b) => a + b;

        int CalcularSuma(int a, int b)
        {
            return SumaLocal();

            int SumaLocal()
            {
                return a + b;
            }
        }

    }
}

