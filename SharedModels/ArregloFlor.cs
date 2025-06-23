using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class ArregloFlor
    {
        public int IdArreglo { get; set; }
        public int IdFlor { get; set; }
        public int Cantidad { get; set; }

        public Flor? Flor { get; set; }
    }

}
