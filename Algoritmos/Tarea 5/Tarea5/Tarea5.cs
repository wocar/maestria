using System;
using System.Collections.Generic;
using System.Linq;

namespace Tarea5
{
    public class Tarea5
    {
        private static readonly int[] Denominaciones = {500, 200, 100, 50};

        public static Dictionary<int, int> CacularBilletes(int monto)
        {
            var denominaciones = Denominaciones.OrderByDescending(i => i).ToList();

            var resultados = new Dictionary<int, int>();
            
            foreach (var denominacion in denominaciones)
            {
                if(monto == 0) break;
                var cantidad = monto / denominacion;
                if(cantidad == 0) continue;
                resultados[denominacion] = cantidad;
                monto -= cantidad * denominacion;
            }

            if (monto != 0)
                throw new Exception("No hay cambio exacto");
            return resultados;
        }
    }
}