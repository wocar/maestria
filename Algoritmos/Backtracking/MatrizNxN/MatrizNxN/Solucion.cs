using System.Drawing;

namespace MatrizNxN
{
    struct Solucion
    {
        public Solucion(Point coordenadas, int valor)
        {
            Coordenadas = coordenadas;
            Valor = valor;
        }

        public Point Coordenadas { get; }
        public int Valor;
    }
}