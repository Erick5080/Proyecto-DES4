using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto1
{
    public partial class HistorialForm : Form
    {
        private readonly ConexionDB db;

        public HistorialForm(ConexionDB dbConnection)
        {
            InitializeComponent();
            this.db = dbConnection;
            this.Load += HistorialForm_Load; // Carga los datos al cargar el formulario
        }

        private void HistorialForm_Load(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                // Obtiene los registros de la base de datos
                DataTable historialData = db.ObtenerRegistros();

                // Asigna los datos al DataGridView
                dgvHistorial.DataSource = historialData;

                // Opcional: Ajustar el ancho de las columnas
                dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el historial. Detalle: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Si hay un error, el DataGridView estará vacío, lo cual es manejable.
            }
        }
    }
}