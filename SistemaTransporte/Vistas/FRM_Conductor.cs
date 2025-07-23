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
    public partial class FRM_Conductor : Form
    {
        int idSeleccionado = 0;

        public FRM_Conductor()
        {
            InitializeComponent();
            CargarConductores();
            cmbLicencia.Items.AddRange(new string[] { "A", "B", "C", "D", "E" });
        }

        private void CargarConductores()
        {
            dgvConductores.DataSource = ConductorController.Cargar();
            dgvConductores.ClearSelection();
            LimpiarCampos();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCedula.Text = "";
            cmbLicencia.SelectedIndex = -1;
            idSeleccionado = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string cedula = txtCedula.Text.Trim();
            string licencia = cmbLicencia.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cedula) || string.IsNullOrWhiteSpace(licencia))
            {
                MessageBox.Show("Debe completar todos los campos.");
                return;
            }

            if (!new string[] { "A", "B", "C", "D", "E" }.Contains(licencia))
            {
                MessageBox.Show("Licencia no válida. Ingrese A, B, C, D o E.");
                return;
            }

            if (ConductorController.ExisteCedula(cedula))
            {
                MessageBox.Show("La cédula ya existe. Ingrese otra o modifique el conductor existente.");
                return;
            }

            var conductor = new ConductorModel()
            {
                Nombre = nombre,
                Cedula = cedula,
                Licencia = licencia
            };

            try
            {
                ConductorController.Guardar(conductor);
                MessageBox.Show("Conductor guardado.");
                CargarConductores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un conductor para modificar.");
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string cedula = txtCedula.Text.Trim();
            string licencia = cmbLicencia.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cedula) || string.IsNullOrWhiteSpace(licencia))
            {
                MessageBox.Show("Debe completar todos los campos.");
                return;
            }

            if (!new string[] { "A", "B", "C", "D", "E" }.Contains(licencia))
            {
                MessageBox.Show("Licencia no válida.");
                return;
            }

            if (ConductorController.ExisteCedula(cedula, idSeleccionado))
            {
                MessageBox.Show("Ya existe otro conductor con esa cédula.");
                return;
            }

            var conductor = new ConductorModel()
            {
                Id = idSeleccionado,
                Nombre = nombre,
                Cedula = cedula,
                Licencia = licencia
            };

            try
            {
                ConductorController.Modificar(conductor);
                MessageBox.Show("Conductor modificado.");
                CargarConductores();
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
                MessageBox.Show("Seleccione un conductor para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Seguro que desea eliminar este conductor?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    ConductorController.Eliminar(idSeleccionado);
                    MessageBox.Show("Conductor eliminado.");
                    CargarConductores();
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

        private void dgvConductores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvConductores.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtCedula.Text = fila.Cells["Cedula"].Value.ToString();
                cmbLicencia.Text = fila.Cells["licencia"].Value.ToString();

                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }
    }
}
