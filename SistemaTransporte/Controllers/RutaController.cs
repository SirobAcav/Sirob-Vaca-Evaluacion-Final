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
    public class RutaController
    {
        public static List<RutaModel> Listar()
        {
            var lista = new List<RutaModel>();
            var tabla = Cargar();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new RutaModel
                {
                    Id = Convert.ToInt32(fila["id"]),
                    Nombre = fila["nombre"].ToString(),
                    Kilometraje = Convert.ToDecimal(fila["kilometraje"])
                });
            }
            return lista;
        }

        public static bool ExisteNombre(string nombre, int idExcluir = 0)
        {
            try
            {
                var con = cls_conexion.Conectar();
                var sql = "SELECT COUNT(*) FROM rutas WHERE nombre = @nombre AND estado = 1";
                if (idExcluir > 0)
                {
                    sql += " AND id != @id";
                }
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                if (idExcluir > 0)
                    cmd.Parameters.AddWithValue("@id", idExcluir);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Guardar(RutaModel ruta)
        {
            try
            {
                if (ExisteNombre(ruta.Nombre))
                    throw new Exception("Ya existe una ruta con ese nombre.");

                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("INSERT INTO rutas (nombre, kilometraje) VALUES (@nombre, @kilometraje)", con);
                cmd.Parameters.AddWithValue("@nombre", ruta.Nombre);
                cmd.Parameters.AddWithValue("@kilometraje", ruta.Kilometraje);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Modificar(RutaModel ruta)
        {
            try
            {
                if (ExisteNombre(ruta.Nombre, ruta.Id))
                    throw new Exception("Ya existe una ruta con ese nombre.");

                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("UPDATE rutas SET nombre=@nombre, kilometraje=@kilometraje WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@nombre", ruta.Nombre);
                cmd.Parameters.AddWithValue("@kilometraje", ruta.Kilometraje);
                cmd.Parameters.AddWithValue("@id", ruta.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void Eliminar(int id)
        {
            try
            {
                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("UPDATE rutas SET estado=0 WHERE id=@id", con);
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
                var cmd = new MySqlCommand("SELECT * FROM rutas WHERE estado=1", con);
                var da = new MySqlDataAdapter(cmd);
                da.Fill(tabla);
            }
            catch (Exception ex) { throw ex; }
            return tabla;
        }
    }
}
