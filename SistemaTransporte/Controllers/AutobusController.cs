using MySql.Data.MySqlClient;
using SistemaTransporte.Conexion;
using SistemaTransporte.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTransporte.Controllers
{
    public class AutobusController
    {
        public static List<AutobusModel> Listar()
        {
            var lista = new List<AutobusModel>();
            var tabla = Cargar();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new AutobusModel
                {
                    Id = Convert.ToInt32(fila["id"]),
                    Placa = fila["placa"].ToString(),
                    Modelo = fila["modelo"].ToString()
                });
            }
            return lista;
        }

        public static void Guardar(AutobusModel bus)
        {
            try
            {
                if (ExistePlaca(bus.Placa))
                    throw new Exception("Ya existe un autobús con esa placa.");

                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("INSERT INTO autobuses (placa, modelo) VALUES (@placa, @modelo)", con);
                cmd.Parameters.AddWithValue("@placa", bus.Placa);
                cmd.Parameters.AddWithValue("@modelo", bus.Modelo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Modificar(AutobusModel bus)
        {
            try
            {
                if (ExistePlaca(bus.Placa, bus.Id))
                    throw new Exception("Ya existe otro autobús con esa placa.");

                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("UPDATE autobuses SET placa=@placa, modelo=@modelo WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@placa", bus.Placa);
                cmd.Parameters.AddWithValue("@modelo", bus.Modelo);
                cmd.Parameters.AddWithValue("@id", bus.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Eliminar(int id)
        {
            try
            {
                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("UPDATE autobuses SET estado=0 WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static DataTable Cargar()
        {
            var tabla = new DataTable();
            try
            {
                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("SELECT * FROM autobuses WHERE estado=1", con);
                var da = new MySqlDataAdapter(cmd);
                da.Fill(tabla);
            }
            catch (Exception ex) { throw ex; }
            return tabla;
        }

        public static bool ExistePlaca(string placa)
        {
            var con = cls_conexion.Conectar();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM autobuses WHERE placa=@placa AND estado=1", con);
            cmd.Parameters.AddWithValue("@placa", placa);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public static bool ExistePlaca(string placa, int id)
        {
            var con = cls_conexion.Conectar();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM autobuses WHERE placa=@placa AND id<>@id AND estado=1", con);
            cmd.Parameters.AddWithValue("@placa", placa);
            cmd.Parameters.AddWithValue("@id", id);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}
