using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Tarea5.Tests
{
    public class Tests
    {
        [Test]
        public void Debe_Calcular_500()
        {
            var resultado = Tarea5.CacularBilletes(500);
            resultado.Select(g => g.Key * g.Value).DefaultIfEmpty(0).Sum().ShouldBe(500);
            
            resultado.Count.ShouldBe(1);
            
        }
        [Test]
        public void Debe_Calcular_750()
        {
            var resultado = Tarea5.CacularBilletes(750).ToArray();
            resultado.Select(g => g.Key * g.Value).DefaultIfEmpty(0).Sum().ShouldBe(750);
            
            resultado.Length.ShouldBe(3);
            resultado[0].Key.ShouldBe(500);
            resultado[1].Key.ShouldBe(200);
            resultado[2].Key.ShouldBe(50);
            
        }
        [Test]
        public void Debe_Tirar_Excepcion_Cuando_No_Hay_Cantidad_Exacta()
        {
            Should.Throw<Exception>(() => Tarea5.CacularBilletes(40));


        }
    }
}