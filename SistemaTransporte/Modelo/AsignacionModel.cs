using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTransporte.Modelo
{
    public class AsignacionModel
    {
        public int Id { get; set; }
        public int IdConductor { get; set; }
        public int IdAutobus { get; set; }
        public int IdRuta { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }
}
