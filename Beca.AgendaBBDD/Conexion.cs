using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beca.AgendaBBDD
{
    public class Conexion
    {
        SqlConnection cnx;

        public Conexion()
        {
            try
            {
                cnx = new SqlConnection("Data Source=WINAPXWCXOVEEYF\\SQLEXPRESS;Initial Catalog=Agenda;");
                cnx.Open();
                Console.WriteLine("Éxito al conectar.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al conectar. " + ex.Message);
            }
        }
    }
}
