using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemaMochila
{
    class Program
    {
        private const int MaxCapacidad = 100;

        static readonly Random Random = new Random();

        //Introducir la mayor cantidad de items a una mochila
        static void Main(string[] args)
        {
            var mochila = new Mochila(MaxCapacidad);

            var items = Enumerable.Range( MaxCapacidad / 2, Random.Next(MaxCapacidad / 2,MaxCapacidad))
                .Select(f => new Item() {Tamaño = Random.Next(0, MaxCapacidad)})
                .ToList();

            for (int i = 0; i < items.Count; i++)
            {
               

                var itemActual = items[i];
                Item siguiente = null;

                if (i + 1 < items.Count)
                    siguiente = items[i + 1];
                
                var item = itemActual;
                
                if (siguiente != null && siguiente?.Tamaño <= itemActual.Tamaño)
                    item = siguiente;

                if (item.Tamaño > mochila.Capacidad)
                    continue;
                mochila.AgregarItem(item);
            }

            Console.WriteLine($"Total items: {items.Count} (tamaño: {items.CalcularTamaño()} )");

            Console.WriteLine(
                $"Utilizado: {mochila.Items.CalcularTamaño()} de {MaxCapacidad} ({mochila.Items.CalcularTamaño() / (decimal) MaxCapacidad:P2}) en {mochila.Items.Count} item(s)");
        }

        class Mochila
        {
            public Mochila(int capacidad)
            {
                Capacidad = capacidad;
            }

            public bool EstaLlena => Capacidad == 0;

            public int Capacidad { get; private set; }
            public List<Item> Items { get; set; } = new List<Item>();

            public void AgregarItem(Item item)
            {
                if (item.Tamaño <= Capacidad)
                {
                    Capacidad -= item.Tamaño;
                    Items.Add(item);
                }
                else
                {
                    throw new Exception("Este item ya no cabe en la mochila");
                }
            }
        }
    }

    public class Item
    {
        public int Tamaño { get; set; }
    }

    public static class Extensiones
    {
        public static int CalcularTamaño(this IEnumerable<Item> items)
        {
            return items.Select(g => g.Tamaño).DefaultIfEmpty(0).Sum();
        }
    }
}