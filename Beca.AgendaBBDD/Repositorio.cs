using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beca.AgendaBBDD
{
    internal class Repositorio
    {
        /// <summary>
        /// Método para Obtener los contactos
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerContactos()
        {
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adaptador = new SqlDataAdapter();

            try
            {
                comando.CommandText = "SELECT * FROM CONTACTOS";
                comando.Connection = conexion.cnx;
                adaptador.SelectCommand = comando;
                conexion.cnx.Open();
                adaptador.Fill(ds);
                conexion.cnx.Close();

                return ds;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return ds;
        }

        /// <summary>
        /// Método para crear un contacto.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static  bool CrearContacto(Contacto c)
        {
            bool todoCorrecto = false;
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = "set dateformat dmy; INSERT INTO CONTACTOS VALUES ('" + c.nombre + "', '" + c.fechaNacimiento + "', '" + c.telefono + "', '" + c.observaciones + "')";
                
                comando.Connection = conexion.cnx;
                conexion.cnx.Open();
                comando.ExecuteNonQuery();
                conexion.cnx.Close();

                todoCorrecto = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                todoCorrecto = false;
            }

            return todoCorrecto;
        }

        /// <summary>
        /// Método para eliminar un contacto.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool EliminarContacto(Contacto c)
        {
            bool todoCorrecto = false;
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = "DELETE FROM CONTACTOS WHERE ID = '" + c.Id + "'";

                comando.Connection = conexion.cnx;
                conexion.cnx.Open();
                comando.ExecuteNonQuery();
                conexion.cnx.Close();

                todoCorrecto = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                todoCorrecto = false;
            }

            return todoCorrecto;
        }

        /// <summary>
        /// Método para modificar un contacto.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool ModificarContacto(Contacto c)
        {
            bool todoCorrecto = false;
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = "set dateformat dmy; UPDATE CONTACTOS SET NOMBRE = '" + c.nombre + "', FECHANACIMIENTO= '" + c.fechaNacimiento + "', " + 
                    "TELEFONO = '" + c.telefono + "', OBSERVACIONES = '" + c.observaciones + "' " +
                    " WHERE ID = '" + c.Id + "'";

                comando.Connection = conexion.cnx;
                conexion.cnx.Open();
                comando.ExecuteNonQuery();
                conexion.cnx.Close();

                todoCorrecto = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                todoCorrecto = false;
            }

            return todoCorrecto;
        }
    }
}
