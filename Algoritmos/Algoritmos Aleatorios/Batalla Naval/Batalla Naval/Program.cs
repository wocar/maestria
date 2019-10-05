using System;
using System.Linq;
using System.Threading;


namespace Batalla_Naval
{
    class Program
    {
        private const int TamañoX = 140;
        private const int TamañoY = 5;


        private const int Barcos = 5;
        private const int TamañoMaxBarco = 5;
        private const int TamañoMinBarco = 2;

        static void Main(string[] args)
        {
            GenerarCuadricula();
            GenerarBarcos();
            Console.WriteLine("Barcos generados: ");
            var original = _cuadricula.Select(a => a.ToArray()).ToArray();
            ;
            ImprimirBarcos();
            Console.WriteLine("Presione X para comenzar");
            Console.ReadLine();
            Jugar();
        }


        private static void Jugar()
        {
            var resultados = new int[TamañoY][];
            for (var i = 0; i < resultados.Length; i++)
            {
                resultados[i] = new int[TamañoX];
            }

            var ultimo = (x: 0, y: 0, status: Miss, direccion: Direccion.Aleatoria, missCount: 0);

            while (true)
            {
                var x = ultimo.x;
                var y = ultimo.y;
                var direccion = ultimo.direccion;

       
                if (direccion == Direccion.Aleatoria)
                {
                    (x, y) = GenerarCoordenadas();
                    direccion = Direccion.Aleatoria;
                }


                var resultado = Atacar(x, y) ? Hit : Miss;
                resultados[y][x] = resultado;
                ultimo = (x, y, Hit, direccion, resultado == Miss ? ultimo.missCount++ : 0);


                Console.Clear();
                //ImprimirBarcos();
                ImprimirIntentos(resultados);
                //Thread.Sleep(10);
                if (VerificarGanar(resultados))
                    break;
            }
        }

        private static Direccion InvertirDireccion(Direccion direccion)
        {
            return direccion == Direccion.Horizontal ? Direccion.Vertical : Direccion.Horizontal;
        }

        private static bool ObtenerDireccion(int x, int y, ref Direccion direccion)
        {
            if (direccion == Direccion.Horizontal)
            {
                if (x + 1 >= TamañoX)
                {
                    direccion = Direccion.Vertical;
                }
                else
                {
                    return true;
                }
            }

            if (direccion == Direccion.Vertical)
            {
                if (y + 1 >= TamañoY)
                {
                    direccion = Direccion.Horizontal;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private static bool VerificarGanar(int[][] resultados)
        {
            for (int i = 0; i < _cuadricula.Length; i++)
            {
                for (int j = 0; j < _cuadricula[i].Length; j++)
                {
                    if (_cuadricula[i][j] >= 1)
                        if (resultados[i][j] != Hit)
                            return false;
                }
            }

            return true;
        }

        private static bool Atacar(int x, int y)
        {
            return _cuadricula[y][x] >= 1;
        }


        private static readonly Random Random = new Random();

        private static void GenerarBarcos()
        {
            for (var barco = 1; barco <= Barcos; barco++)
            {
                var intentos = 0;
                while (true)
                {
                    var (x, y) = GenerarCoordenadas();
                    var direccion = Random.Next(0, 2) == 1 ? Direccion.Horizontal : Direccion.Vertical;
                    var tamaño = Random.Next(TamañoMinBarco, TamañoMaxBarco);
                    if (!PuedeGenerarse(x, y, tamaño, direccion))
                    {
                        intentos++;
                        if (intentos >= 100)
                            throw new Exception("Imposible generar los barcos dados los parametros");
                        continue;
                    }

                    GenerarBarco(x, y, tamaño, direccion, barco);
                    break;
                }
            }
        }

        private const int Hit = -2;
        private const int Miss = -1;

        static void ImprimirIntentos(int[][] resultados)
        {
            Console.WriteLine("");

            foreach (var linea in resultados)
            {
                foreach (var i in linea)
                {
                    var c = i switch
                    {
                        Miss => "0",
                        Hit => "x",
                        _ => "-"
                    };

                    Console.Write(c);
                }

                Console.WriteLine("");
            }

            Console.WriteLine("");
        }

        static void ImprimirBarcos()
        {
            Console.WriteLine("");

            foreach (var linea in _cuadricula)
            {
                foreach (var i in linea)
                {
                    Console.Write(i == 0 ? "x" : $"{i}");
                }

                Console.WriteLine("");
            }

            Console.WriteLine("");
        }

        private static void GenerarBarco(int x, int y, int tamaño, Direccion direccion, int nBarco)
        {
            for (var i = 0; i < tamaño; i++)
            {
                switch (direccion)
                {
                    case Direccion.Vertical:
                        _cuadricula[y + i][x] = nBarco;
                        _cuadricula[y - i][x] = nBarco;
                        break;
                    case Direccion.Horizontal:
                        _cuadricula[y][x + i] = nBarco;
                        _cuadricula[y][x - i] = nBarco;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direccion), direccion, null);
                }
            }
        }

        private static bool PuedeGenerarse(int x, int y, int tamaño, Direccion direccion)
        {
            switch (direccion)
            {
                case Direccion.Horizontal when x + tamaño >= TamañoX:
                case Direccion.Horizontal when x - tamaño < 0:
                case Direccion.Vertical when y + (tamaño) >= TamañoY:
                case Direccion.Vertical when y - tamaño < 0:
                    return false;
            }

            for (var i = 0; i < tamaño; i++)
            {
                switch (direccion)
                {
                    case Direccion.Vertical when
                        _cuadricula[y + i][x] != 0:
                    case Direccion.Vertical when
                        _cuadricula[y - i][x] != 0:
                        return false;
                    case Direccion.Horizontal when
                        _cuadricula[y][x + i] != 0:
                    case Direccion.Horizontal when
                        _cuadricula[y][x - i] != 0:
                        return false;
                }
            }

            return true;
        }

        private static (int centroX, int centroY) GenerarCoordenadas()
        {
            var centroX = Random.Next(0, TamañoX);
            var centroY = Random.Next(0, TamañoY);
            return (centroX, centroY);
        }

        private static int[][] _cuadricula;

        private static void GenerarCuadricula()
        {
            _cuadricula = new int[TamañoY][];
            for (int i = 0; i < TamañoY; i++)
            {
                _cuadricula[i] = new int[TamañoX];
            }
        }
    }

    enum Direccion
    {
        Vertical,
        Horizontal,
        Aleatoria
    }
}