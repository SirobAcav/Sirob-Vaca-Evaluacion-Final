using SistemaTransporte.Controllers;
using SistemaTransporte.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTransporte.Vistas
{
    public partial class FRM_Asignacion : Form
    {
        private int idSeleccionado = -1;

        public FRM_Asignacion()
        {
            InitializeComponent();
            CargarCombos();
            CargarTabla();
            InicializarControles();
        }

        private void InicializarControles()
        {
            dtpFecha.MinDate = DateTime.Today;
            maskedHoraInicio.Mask = "00:00";
            maskedHoraFin.Mask = "00:00";
            maskedHoraInicio.ValidatingType = typeof(DateTime);
            maskedHoraFin.ValidatingType = typeof(DateTime);

            cmbConductor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAutobus.DropDownStyle = ComboBoxStyle.DropDown;
            cmbRuta.DropDownStyle = ComboBoxStyle.DropDown;

            cmbConductor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbConductor.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbAutobus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAutobus.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbRuta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbRuta.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void CargarCombos()
        {
            cmbConductor.DataSource = ConductorController.Listar();
            cmbConductor.DisplayMember = "nombre";
            cmbConductor.ValueMember = "id";

            cmbAutobus.DataSource = AutobusController.Listar();
            cmbAutobus.DisplayMember = "placa";
            cmbAutobus.ValueMember = "id";

            cmbRuta.DataSource = RutaController.Listar();
            cmbRuta.DisplayMember = "nombre";
            cmbRuta.ValueMember = "id";
        }

        private void CargarTabla()
        {
            dgvAsignaciones.DataSource = AsignacionController.Cargar();
        }

        private bool ValidarHoras()
        {
            if (!maskedHoraInicio.MaskFull || !maskedHoraFin.MaskFull)
            {
                MessageBox.Show("Debe ingresar ambas horas en formato válido (HH:mm).");
                return false;
            }
            
            if (!TimeSpan.TryParse(maskedHoraInicio.Text, out TimeSpan horaInicio) ||
                !TimeSpan.TryParse(maskedHoraFin.Text, out TimeSpan horaFin))
            {
                MessageBox.Show("Formato de hora incorrecto. Use HH:mm.");
                return false;
            }

            if (dtpFecha.Value.Date == DateTime.Today)
            {
                TimeSpan horaActual = DateTime.Now.TimeOfDay;
                if (horaInicio < horaActual)
                {
                    MessageBox.Show("La hora de inicio no puede ser anterior a la hora actual.");
                    return false;
                }

                if (horaFin < horaActual)
                {
                    MessageBox.Show("La hora de fin no puede ser anterior a la hora actual.");
                    return false;
                }
            }

            if (horaFin <= horaInicio)
            {
                MessageBox.Show("La hora de fin debe ser mayor que la hora de inicio.");
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarHoras())
                    return;

                var nueva = new AsignacionModel
                {
                    IdConductor = Convert.ToInt32(cmbConductor.SelectedValue),
                    IdAutobus = Convert.ToInt32(cmbAutobus.SelectedValue),
                    IdRuta = Convert.ToInt32(cmbRuta.SelectedValue),
                    FechaAsignacion = dtpFecha.Value.Date,
                    HoraInicio = maskedHoraInicio.Text,
                    HoraFin = maskedHoraFin.Text
                };

                AsignacionController.Guardar(nueva);
                MessageBox.Show("Asignación guardada correctamente.");
                CargarTabla();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void dgvAutobuses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idSeleccionado = Convert.ToInt32(dgvAsignaciones.Rows[e.RowIndex].Cells["id"].Value);
                cmbConductor.Text = dgvAsignaciones.Rows[e.RowIndex].Cells["conductor"].Value.ToString();
                cmbAutobus.Text = dgvAsignaciones.Rows[e.RowIndex].Cells["autobus"].Value.ToString();
                cmbRuta.Text = dgvAsignaciones.Rows[e.RowIndex].Cells["ruta"].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(dgvAsignaciones.Rows[e.RowIndex].Cells["fecha_asignacion"].Value);
                maskedHoraInicio.Text = dgvAsignaciones.Rows[e.RowIndex].Cells["hora_inicio"].Value.ToString();
                maskedHoraFin.Text = dgvAsignaciones.Rows[e.RowIndex].Cells["hora_fin"].Value.ToString();
                btnModificar.Enabled = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado != -1)
                {
                    var confirm = MessageBox.Show("¿Seguro que desea eliminar esta asignación?", "Confirmar", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        //
                        int id = Convert.ToInt32(dgvAsignaciones.CurrentRow.Cells["id"].Value);
                        AsignacionController.Eliminar(id);
                        MessageBox.Show("Asignación eliminada.");
                        CargarTabla();
                        Limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una fila para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void Limpiar()
        {
            cmbConductor.SelectedIndex = 0;
            cmbAutobus.SelectedIndex = 0;
            cmbRuta.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Today;
            maskedHoraInicio.Clear();
            maskedHoraFin.Clear();
            idSeleccionado = -1;
            btnModificar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == -1)
                {
                    MessageBox.Show("Debe seleccionar una asignación para modificar.");
                    return;
                }

                if (!ValidarHoras())
                    return;

                var modificada = new AsignacionModel
                {
                    //
                    Id = Convert.ToInt32(dgvAsignaciones.CurrentRow.Cells["id"].Value),
                    IdConductor = Convert.ToInt32(cmbConductor.SelectedValue),
                    IdAutobus = Convert.ToInt32(cmbAutobus.SelectedValue),
                    IdRuta = Convert.ToInt32(cmbRuta.SelectedValue),
                    FechaAsignacion = dtpFecha.Value.Date,
                    HoraInicio = maskedHoraInicio.Text,
                    HoraFin = maskedHoraFin.Text
                };

                AsignacionController.Actualizar(modificada);
                MessageBox.Show("Asignación modificada correctamente.");
                CargarTabla();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
