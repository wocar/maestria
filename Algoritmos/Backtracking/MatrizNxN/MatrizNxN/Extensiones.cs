using System;
using System.Collections.Generic;
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
        public static void PrintArray(this IReadOnlyList<int[]> matriz, Point current, Point backtrack, ConsoleColor currentColor = ConsoleColor.Blue)
        {
            Console.Clear();
            for (var index = 0; index < matriz.Count; index++)
            {
                var t = matriz[index];
                for (var j = 0; j < matriz.Count; j++)
                {
                    var point = new Point(j, index);
                    if (point == current && current != backtrack)
                    {
                        Console.BackgroundColor = currentColor;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (point == backtrack)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
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

            for (var i = 0; i < matriz.Count; i++)
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