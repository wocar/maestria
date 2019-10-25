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


        static void Main(string[] args)
        {
            var n = 4;
            var matriz = new int[n][];
            for (var i = 0; i < n; i++) matriz[i] = new int[n];
            var soluciones = new LinkedList<Solucion>();

            var coordenadas = new Point(-1, -1);
            var direccion = true;

            var backtrack = new Point(matriz.Length - 1, matriz.Length - 1);
            while (true)
            {
                var siguiente = Siguiente(coordenadas, matriz);

                if (siguiente == Null)
                {
                    backtrack = Anterior(backtrack, matriz);
                    if (backtrack == Null)
                    {
                        backtrack = new Point(matriz.Length - 1, matriz.Length - 1);
                        coordenadas = new Point(-1, -1);
                        continue;
                    }

                    coordenadas = backtrack;
                    continue;
                }


                var solucion = GenerarSolucion(siguiente, matriz);
                matriz[siguiente.Y][siguiente.X] = solucion.Valor;
                matriz.PrintArray(siguiente, backtrack);
                Thread.Sleep(5000);

                if (matriz.SumasIguales())
                    break;

                coordenadas = siguiente;
            }
        }


        private static Solucion GenerarSolucion(Point coordenadas, IReadOnlyList<int[]> matriz)
        {
            var arr = new List<int[]>();
            arr.AddRange(matriz.Select(g => (int[]) g.Clone()).ToList());

            for (int i = 1; i <= 4; i++)
            {
                arr[coordenadas.Y][coordenadas.X] = i;
                var sumaFila = arr[coordenadas.Y].Sum();
                var sumaColumna = arr.Sum(g => g[coordenadas.X]);
                if (sumaFila == sumaColumna)
                {
                    return new Solucion(coordenadas, i);
                }
            }

            return new Solucion(coordenadas, SinSolucion);

            //return new Solucion(coordenadas, _random.Next(1, 4));
        }

        public static int SinSolucion = -1337;

        private static Point Siguiente(Point punto, int[][] matriz)
        {
            if (punto.X == -1 && punto.Y == -1)
                return new Point(0, 0);
            if (punto.X + 1 < matriz.Length)
                return new Point(punto.X + 1, punto.Y);
            if (punto.Y + 1 < matriz.Length)
                return new Point(0, punto.Y + 1);

            return Null;
        }
        private static Point Anterior(Point punto, int[][] matriz)
        {
            if (punto.IsEmpty)
                return Null;
            if (punto.X > 0)
                return new Point(punto.X - 1, punto.Y);
            if (punto.X <= 0)
                return new Point(matriz.Length, punto.Y - 1);
            return Null;
        }
        private static readonly Point Null = new Point(-2, -2);

    }
}