using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace MatrizNxN
{
    class Program
    {
        private static readonly Random _random = new Random();
        public static int ValorMaximo = 6;

        static int n = 3;

        static void Main(string[] args)
        {
            var matriz = new int[n][];
            for (var i = 0; i < n; i++) matriz[i] = new int[n];

            BackTrack(matriz);
        }

        private static int c = 0;
        private static void BackTrack(int[][] matriz, Point? inicio = null)
        {
            var coordenadas = inicio ?? new Point(-1, -1);

            var backtrack = Null;
            while (true)
            {
                var siguiente = Siguiente(coordenadas);
                if (siguiente == Null)
                {
                    if (backtrack == Null)
                        backtrack = coordenadas;
                    backtrack = Anterior(backtrack);
                    if (backtrack == Null)
                    {
                        backtrack = new Point(n-1,n-1);
                        c++;

                    }
                    coordenadas = backtrack;
                    continue;
                }
          
                var posibleSolicion = GenerarSolucion(siguiente, matriz);
                matriz.PrintArray(coordenadas, backtrack);
                var solucion = posibleSolicion;
                if (posibleSolicion == SinSolucion)
                {
                    coordenadas = Anterior(backtrack);
                    solucion = _random.Next(1, ValorMaximo);

                    if (matriz[siguiente.Y][siguiente.X] > 0 && backtrack == coordenadas)
                    {
                        backtrack = Anterior(backtrack);
                        continue;
                    }
                }

                

                matriz[siguiente.Y][siguiente.X] = solucion;
                matriz.PrintArray(siguiente, backtrack);
                Console.WriteLine("Intentos: " + c);
                Thread.Sleep(100);

                if (matriz.SumasIguales())
                    break;
 
                coordenadas = siguiente;
            }
        }
        

        private static int GenerarSolucion(Point coordenadas, int[][] matriz)
        {
            var arr = new List<int[]>();
            arr.AddRange(matriz.Select(g => (int[]) g.Clone()).ToList());

            for (var i = arr[coordenadas.Y][coordenadas.X]; i <= ValorMaximo; i++)
            {
                if (arr[coordenadas.Y][coordenadas.X] == i)
                    continue;
                arr[coordenadas.Y][coordenadas.X] = i;
                arr.PrintArray(coordenadas, Null, currentColor: ConsoleColor.Yellow);
                if (ValidarSumas(coordenadas, arr))
                {
                    return i;
                }
            }

         
            arr[coordenadas.Y][coordenadas.X] = -0;
            arr.PrintArray(coordenadas, Null, currentColor:ConsoleColor.Red);

            return SinSolucion;
        }

        private static bool ValidarSumas(Point coordenadas, List<int[]> arr)
        {
            var sumaFila = arr[coordenadas.Y].Sum();
            var sumaColumna = arr.Sum(g => g[coordenadas.X]);
            return sumaColumna == sumaFila;
        }

        public static int SinSolucion = -1337;

        private static Point Siguiente(Point punto)
        {
            if (punto.X == -1 && punto.Y == -1)
                return new Point(0, 0);
            if (punto.X + 1 < n)
                return new Point(punto.X + 1, punto.Y);
            if (punto.Y + 1 < n)
                return new Point(0, punto.Y + 1);

            return Null;
        }

        private static Point Anterior(Point punto)
        {
            if (punto.IsEmpty)
                return new Point(-1, 0);

            if (punto.X > 0)
                return new Point(punto.X - 1, punto.Y);
            if (punto.X <= 0)
                return punto.Y == 0 ? Null : new Point(n, punto.Y - 1);
            return Null;
        }

        private static readonly Point Null = new Point(-2, -2);
        private static readonly Point Ultimo = new Point(-3, -3);
    }
}