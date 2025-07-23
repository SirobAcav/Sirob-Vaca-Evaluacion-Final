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
    public class ConductorController
    {
        private static readonly List<string> LicenciasValidas = new List<string> { "A", "B", "C", "D", "E" };
        public static List<ConductorModel> Listar()
        {
            var lista = new List<ConductorModel>();
            var tabla = Cargar();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new ConductorModel
                {
                    Id = Convert.ToInt32(fila["id"]),
                    Nombre = fila["nombre"].ToString(),
                    Licencia = fila["licencia"].ToString(),
                    Cedula = fila["cedula"].ToString()
                });
            }
            return lista;
        }

        public static void Guardar(ConductorModel conductor)
        {
            try
            {
                if (!LicenciasValidas.Contains(conductor.Licencia))
                    throw new Exception("Licencia no válida. Use: A, B, C, D o E.");

                if (ExisteCedula(conductor.Cedula))
                    throw new Exception("Ya existe un conductor con esa cédula.");

                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("INSERT INTO conductores (nombre, licencia, cedula) VALUES (@nombre, @licencia, @cedula)", con);
                cmd.Parameters.AddWithValue("@nombre", conductor.Nombre);
                cmd.Parameters.AddWithValue("@licencia", conductor.Licencia);
                cmd.Parameters.AddWithValue("@cedula", conductor.Cedula);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Modificar(ConductorModel conductor)
        {
            try
            {
                if (!LicenciasValidas.Contains(conductor.Licencia))
                    throw new Exception("Licencia no válida. Use: A, B, C, D o E.");

                if (ExisteCedula(conductor.Cedula, conductor.Id))
                    throw new Exception("Ya existe un conductor con esa cédula.");

                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("UPDATE conductores SET nombre=@nombre, licencia=@licencia, cedula=@cedula WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@nombre", conductor.Nombre);
                cmd.Parameters.AddWithValue("@licencia", conductor.Licencia);
                cmd.Parameters.AddWithValue("@cedula", conductor.Cedula);
                cmd.Parameters.AddWithValue("@id", conductor.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Eliminar(int id)
        {
            try
            {
                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("UPDATE conductores SET estado=0 WHERE id=@id", con);
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
                var cmd = new MySqlCommand("SELECT * FROM conductores WHERE estado=1", con);
                var da = new MySqlDataAdapter(cmd);
                da.Fill(tabla);
            }
            catch (Exception ex) { throw ex; }
            return tabla;
        }
        public static bool ExisteCedula(string cedula, int? idExcluir = null)
        {
            try
            {
                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM conductores WHERE cedula=@cedula AND estado=1" + (idExcluir.HasValue ? " AND id<>@id" : ""), con);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                if (idExcluir.HasValue)
                    cmd.Parameters.AddWithValue("@id", idExcluir.Value);
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
