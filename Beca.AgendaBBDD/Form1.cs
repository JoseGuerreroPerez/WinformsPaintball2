namespace Beca.AgendaBBDD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Enumerado

        public enum ModoEdicion
        {
            lectura,
            crear,
            modificar
        }

        public ModoEdicion modoEdicion;

        #endregion

        #region Eventos

        /// <summary>
        /// Evento Load del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            modoEdicion = ModoEdicion.lectura;
            ModoPantallaLectura();

            // Cargamos los items de bbdd.

        }

        /// <summary>
        /// Evento KeyPress
        /// </summary>
        /// <param name="sender">Objeto llamante.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento del bot�n a�adir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnA�adir_Click(object sender, EventArgs e)
        {
            modoEdicion = ModoEdicion.crear;
            ModoPantallaEdicion();

        }

        /// <summary>
        /// Evento del bot�n eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento del bot�n Modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            modoEdicion = ModoEdicion.modificar;
            ModoPantallaEdicion();

        }

        /// <summary>
        /// Evento click del bot�n guardar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Contacto c = ObtenerInformacion();

        }

        /// <summary>
        /// Evento click del bot�n Cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("�Est� seguro de que desea salir de la edici�n?", "Confirmaci�n", MessageBoxButtons.YesNo);

            if(respuesta == DialogResult.Yes)
            {
                ModoPantallaLectura();
            }
        }

        #endregion

        #region M�todos

        /// <summary>
        /// M�todo para establecer el modo de la pantalla a edici�n.
        /// </summary>
        public void ModoPantallaEdicion()
        {
            txtNombre.Text = "";
            dtpFechaNacimiento.Value = DateTime.Now;
            txtTelefono.Text = "";
            txtObservaciones.Text = "";

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            txtNombre.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            txtObservaciones.Enabled = true;

            btnA�adir.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            dgvListado.Enabled = false;
        }

        /// <summary>
        /// M�todo para establecer el modo de la pantalla a edici�n.
        /// </summary>
        public void ModoPantallaLectura()
        {
            txtNombre.Text = "";
            dtpFechaNacimiento.Value = DateTime.Now;
            txtTelefono.Text = "";
            txtObservaciones.Text = "";

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            txtNombre.Enabled = false;
            dtpFechaNacimiento.Enabled = false;
            txtObservaciones.Enabled = false;

            btnA�adir.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;

            dgvListado.Enabled = true;
        }

        /// <summary>
        /// M�todo para obtener la informaci�n del formulario.
        /// </summary>
        /// <returns>Contacto con la informaci�n del formulario.</returns>
        public Contacto ObtenerInformacion()
        {
            Contacto contacto = new Contacto();

            contacto.nombre = txtNombre.Text;
            contacto.telefono = txtTelefono.Text;
            contacto.observaciones = txtObservaciones.Text;
            contacto.fechaNacimiento = dtpFechaNacimiento.Value;

            return contacto;
        }


        #endregion


    }
}