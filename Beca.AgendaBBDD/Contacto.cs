using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beca.AgendaBBDD
{
    public class Contacto
    {

        public int? Id { get; set; }

        public string nombre { get; set; }

        public DateTime fecha { get; set; }

        public string numPersonas { get; set; }

        //public bool premium { get; set; }
    }
}
