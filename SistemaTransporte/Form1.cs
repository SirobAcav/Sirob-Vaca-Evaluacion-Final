using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTransporte
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConductor_Click(object sender, EventArgs e)
        {
            var FRM_Conductor = new Vistas.FRM_Conductor();
            this.Hide();
            FRM_Conductor.ShowDialog();
            this.Show();
        }

        private void btnAutobus_Click(object sender, EventArgs e)
        {
            var FRM_Autobus = new Vistas.FRM_Autobus();
            this.Hide();
            FRM_Autobus.ShowDialog();
            this.Show();
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            var FRM_Ruta = new Vistas.FRM_Ruta();
            this.Hide();
            FRM_Ruta.ShowDialog();
            this.Show();
        }

        private void btnAsignacion_Click(object sender, EventArgs e)
        {
            var FRM_Asignacion = new Vistas.FRM_Asignacion();
            this.Hide();
            FRM_Asignacion.ShowDialog();
            this.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
