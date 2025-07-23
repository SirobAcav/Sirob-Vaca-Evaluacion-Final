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
    public partial class FRM_Autobus : Form
    {
        int idSeleccionado = 0;

        public FRM_Autobus()
        {
            InitializeComponent();
            CargarAutobuses();
        }

        private void CargarAutobuses()
        {
            dgvAutobuses.DataSource = AutobusController.Cargar();
            dgvAutobuses.ClearSelection();
            LimpiarCampos();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void LimpiarCampos()
        {
            txtPlaca.Text = "";
            txtModelo.Text = "";
            idSeleccionado = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) || string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("Debe ingresar placa y modelo.");
                return;
            }

            if (AutobusController.ExistePlaca(txtPlaca.Text.Trim()))
            {
                MessageBox.Show("La placa ya existe. Ingrese otra.");
                return;
            }

            var bus = new AutobusModel()
            {
                Placa = txtPlaca.Text.Trim(),
                Modelo = txtModelo.Text.Trim()
            };

            try
            {
                AutobusController.Guardar(bus);
                MessageBox.Show("Autobús guardado.");
                CargarAutobuses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvAutobuses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvAutobuses.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                txtPlaca.Text = fila.Cells["placa"].Value.ToString();
                txtModelo.Text = fila.Cells["modelo"].Value.ToString();

                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un autobús para modificar.");
                return;
            }

            if (AutobusController.ExistePlaca(txtPlaca.Text.Trim(), idSeleccionado))
            {
                MessageBox.Show("Ya existe otro autobús con esa placa.");
                return;
            }

            var bus = new AutobusModel()
            {
                Id = idSeleccionado,
                Placa = txtPlaca.Text.Trim(),
                Modelo = txtModelo.Text.Trim()
            };

            try
            {
                AutobusController.Modificar(bus);
                MessageBox.Show("Autobús modificado.");
                CargarAutobuses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un autobús para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar autobús?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    AutobusController.Eliminar(idSeleccionado);
                    MessageBox.Show("Autobús eliminado.");
                    CargarAutobuses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
