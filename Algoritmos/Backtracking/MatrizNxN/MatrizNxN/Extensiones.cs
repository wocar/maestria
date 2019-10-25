using System;
using System.Drawing;
using System.Linq;

namespace MatrizNxN
{
    public static class Extensiones
    {
        public static bool SumasIguales(this int[][] matriz)
        {
            var sumas = matriz.Select(t => t.Sum()).ToList(); //filas
            sumas.AddRange(matriz.Select((t1, columna) => matriz.Sum(t => t[columna]))); // columnas
            if (sumas.Any(g => g == 0)) return false;
            return sumas.Distinct().Count() == 1;
        }
        public static void PrintArray(this int[][] matriz, Point current, Point backtrack)
        {
            Console.Clear();
            for (var index = 0; index < matriz.Length; index++)
            {
                var t = matriz[index];
                for (var j = 0; j < matriz.Length; j++)
                {
                    var point = new Point(j, index);
                    if (point == current && current != backtrack)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (point == backtrack)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(t[j] + "\t");
                    Console.ResetColor();
                }

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(t.Sum());

                Console.ResetColor();

                Console.WriteLine();
            }

            for (var i = 0; i < matriz.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(matriz.Sum(g => g[i]));
                Console.ResetColor();
                Console.Write("\t");
            }
        }
    }
    
}