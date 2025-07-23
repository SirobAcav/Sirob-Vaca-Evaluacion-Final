using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTransporte.Conexion
{
    public class cls_conexion
    {
        public static MySqlConnection Conectar()
        {
            string cadena = "server=localhost;database=sistema_transporte;uid=root;pwd=;";
            var conexion = new MySqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}
