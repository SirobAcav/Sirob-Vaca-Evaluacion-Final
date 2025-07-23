using MySql.Data.MySqlClient;
using SistemaTransporte.Conexion;
using SistemaTransporte.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTransporte.Controllers
{
    public class AsignacionController
    {
        public static void Guardar(AsignacionModel asignacion)
        {
            try
            {
                var con = cls_conexion.Conectar();
                
                // Validar si hay solapamientos
                if (ExisteConflicto(con, asignacion))
                    throw new Exception("Existe un conflicto de horario con el conductor, autobús o ruta.");

                var cmd = new MySqlCommand(@"INSERT INTO asignaciones 
        (id_conductor, id_autobus, id_ruta, fecha_asignacion, hora_inicio, hora_fin) 
        VALUES (@id_conductor, @id_autobus, @id_ruta, @fecha, @hora_inicio, @hora_fin)", con);

                cmd.Parameters.AddWithValue("@id_conductor", asignacion.IdConductor);
                cmd.Parameters.AddWithValue("@id_autobus", asignacion.IdAutobus);
                cmd.Parameters.AddWithValue("@id_ruta", asignacion.IdRuta);
                cmd.Parameters.AddWithValue("@fecha", asignacion.FechaAsignacion);
                cmd.Parameters.AddWithValue("@hora_inicio", asignacion.HoraInicio);
                cmd.Parameters.AddWithValue("@hora_fin", asignacion.HoraFin);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la asignación: " + ex.Message);
            }
        }

        public static void Actualizar(AsignacionModel asignacion)
        {
            try
            {
                var con = cls_conexion.Conectar();

                // Validar conflictos excluyendo el propio ID
                if (ExisteConflicto(con, asignacion, true))
                    throw new Exception("Existe un conflicto de horario con el conductor, autobús o ruta.");

                var cmd = new MySqlCommand(@"UPDATE asignaciones 
            SET id_conductor = @id_conductor, id_autobus = @id_autobus, id_ruta = @id_ruta, 
                fecha_asignacion = @fecha, hora_inicio = @hora_inicio, hora_fin = @hora_fin
            WHERE id = @id", con);

                cmd.Parameters.AddWithValue("@id", asignacion.Id);
                cmd.Parameters.AddWithValue("@id_conductor", asignacion.IdConductor);
                cmd.Parameters.AddWithValue("@id_autobus", asignacion.IdAutobus);
                cmd.Parameters.AddWithValue("@id_ruta", asignacion.IdRuta);
                cmd.Parameters.AddWithValue("@fecha", asignacion.FechaAsignacion);
                cmd.Parameters.AddWithValue("@hora_inicio", asignacion.HoraInicio);
                cmd.Parameters.AddWithValue("@hora_fin", asignacion.HoraFin);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la asignación: " + ex.Message);
            }
        }

        public static void Eliminar(int id)
        {
            try
            {
                var con = cls_conexion.Conectar();
                var cmd = new MySqlCommand("DELETE FROM asignaciones WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la asignación: " + ex.Message);
            }
        }

        public static DataTable Cargar()
        {
            var tabla = new DataTable();
            try
            {
                var con = cls_conexion.Conectar();
                string query = @"SELECT a.id, c.nombre AS conductor, au.placa AS autobus, r.nombre AS ruta, 
                        a.fecha_asignacion, a.hora_inicio, a.hora_fin
                        FROM asignaciones a
                        INNER JOIN conductores c ON a.id_conductor = c.id
                        INNER JOIN autobuses au ON a.id_autobus = au.id
                        INNER JOIN rutas r ON a.id_ruta = r.id";
                var cmd = new MySqlCommand(query, con);
                var da = new MySqlDataAdapter(cmd);
                da.Fill(tabla);
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar asignaciones: " + ex.Message);
            }
            return tabla;
        }

        private static bool ExisteConflicto(MySqlConnection con, AsignacionModel asignacion, bool esActualizacion = false)
        {
            string filtroId = esActualizacion ? "AND id <> @id" : "";

            string query = @"
                SELECT COUNT(*) FROM asignaciones
                WHERE fecha_asignacion = @fecha
                AND (
                    (id_conductor = @id_conductor AND ((hora_inicio <= @hora_fin AND hora_fin >= @hora_inicio))) OR
                    (id_autobus = @id_autobus AND ((hora_inicio <= @hora_fin AND hora_fin >= @hora_inicio))) OR
                    (id_ruta = @id_ruta AND ((hora_inicio <= @hora_fin AND hora_fin >= @hora_inicio)))
                ) " + filtroId;

            var cmd = new MySqlCommand(query, con);
            if (esActualizacion)
                cmd.Parameters.AddWithValue("@id", asignacion.Id);

            cmd.Parameters.AddWithValue("@fecha", asignacion.FechaAsignacion);
            cmd.Parameters.AddWithValue("@id_conductor", asignacion.IdConductor);
            cmd.Parameters.AddWithValue("@id_autobus", asignacion.IdAutobus);
            cmd.Parameters.AddWithValue("@id_ruta", asignacion.IdRuta);
            cmd.Parameters.AddWithValue("@hora_inicio", asignacion.HoraInicio);
            cmd.Parameters.AddWithValue("@hora_fin", asignacion.HoraFin);

            var resultado = Convert.ToInt32(cmd.ExecuteScalar());
            return resultado > 0;
        }
    }
}
