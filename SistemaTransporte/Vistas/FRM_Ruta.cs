using SistemaTransporte.Controllers;
using SistemaTransporte.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTransporte.Vistas
{
    public partial class FRM_Ruta : Form
    {
        private int idSeleccionado = -1;

        public FRM_Ruta()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            dgvRutas.DataSource = RutaController.Cargar();
            dgvRutas.ClearSelection();
            idSeleccionado = -1;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                RutaModel ruta = new RutaModel
                {
                    Nombre = txtNombre.Text.Trim(),
                    Kilometraje = (decimal)double.Parse(txtKilometraje.Text)
                };

                RutaController.Guardar(ruta);
                MessageBox.Show("Ruta guardada correctamente.");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void dgvRutas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idSeleccionado = Convert.ToInt32(dgvRutas.Rows[e.RowIndex].Cells["id"].Value);
                txtNombre.Text = dgvRutas.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
                txtKilometraje.Text = dgvRutas.Rows[e.RowIndex].Cells["kilometraje"].Value.ToString();
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                RutaModel ruta = new RutaModel
                {
                    Id = idSeleccionado,
                    Nombre = txtNombre.Text.Trim(),
                    Kilometraje = (decimal)double.Parse(txtKilometraje.Text)
                };

                RutaController.Modificar(ruta);
                MessageBox.Show("Ruta modificada correctamente.");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                RutaController.Eliminar(idSeleccionado);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
