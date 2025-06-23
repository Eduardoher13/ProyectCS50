using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class ArregloAccesorio
    {
        public int IdArreglo { get; set; }
        public int IdAccesorio { get; set; }
        public int Cantidad { get; set; }

        public Accesorio? Accesorio { get; set; }
    }

}
