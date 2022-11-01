using System.Data;

namespace Beca.AgendaBBDD
{
    public partial class frmAgenda : Form
    {
        public frmAgenda()
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
            CargarYConfigurarGrid();

        }

        /// <summary>
        /// Evento KeyPress para controlar que no se puedan escribir caracteres en el tel�fono.
        /// </summary>
        /// <param name="sender">Objeto llamante.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void txtNumPersonas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
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
            bool correcto = false;
            DialogResult respuesta = MessageBox.Show("�Est� seguro de que desea eliminar el registro?", "Confirmaci�n", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                Contacto c = ObtenerInformacion();
                correcto = Repositorio.EliminarContacto(c);

                // Si todo ha ido bien, mostramos un mensaje
                if (correcto)
                {
                    MessageBox.Show("la acci�n se ha realizado correctamente.");
                    // Cambiamos el modo a lectura
                    ModoPantallaLectura();
                    // Una vez hemos hecho la acci�n, recargamos el grid:
                    CargarYConfigurarGrid();
                }

            }

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
            
            bool correcto = false;

            if (InformacionObligatoriaCumplimentada())
            {
                // Rellenamos la entidad con la informaci�n
                Contacto c = ObtenerInformacion();

                switch (modoEdicion)
                {
                    case ModoEdicion.crear:
                        correcto = Repositorio.CrearContacto(c);
                        break;
                    case ModoEdicion.modificar:
                        correcto = Repositorio.ModificarContacto(c);
                        break;
                }

                // Si todo ha ido bien, mostramos un mensaje
                if (correcto)
                {
                    MessageBox.Show("la acci�n se ha realizado correctamente.");
                    modoEdicion = ModoEdicion.lectura;
                    // Cambiamos el modo a lectura
                    ModoPantallaLectura();
                    // Una vez hemos hecho la acci�n, recargamos el grid:
                    CargarYConfigurarGrid();
                }

            }
            else
            {
                MessageBox.Show("Los campos Nombre reserva y Numero de personas son obligatorios.");
            }

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
                modoEdicion = ModoEdicion.lectura;
                ModoPantallaLectura();

                // Si tenemos una fila seleccionada en el grid:
                if(dgvListado.SelectedRows.Count == 1)
                {
                    // Recargamos su informaci�n en el formulario
                    CargarInfoFilaSeleccionadaFormulario(dgvListado.SelectedRows[0]);
                }
            }
        }

        /// <summary>
        /// Evento click de la celda.
        /// Cuando se hace click en una celda se selecciona la fila entera.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvListado.Rows[e.RowIndex].Selected = true;
        }

        /// <summary>
        /// Evento para cuando una fila cambia de estado en el grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvListado_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            // Para cualquier operaci�n que no sea que ha cambiado la selecci�n de la fila, nos salimos
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            // Obtenemos la fila seleccionada
            DataGridViewRow filaSeleccionada = e.Row;
            CargarInfoFilaSeleccionadaFormulario(filaSeleccionada);

        }

        #endregion

        #region M�todos

        /// <summary>
        /// M�todo para establecer el modo de la pantalla a edici�n.
        /// </summary>
        public void ModoPantallaEdicion()
        {
            // Limpiamos s�lo si es modo creaci�n, si es modo edici�n dejamos los valores:
            if (modoEdicion == ModoEdicion.crear)
            {
                txtNombrePersn.Text = "";
                dtpFecha.Value = DateTime.Now;
                txtNumPersonas.Text = "";
                txtId.Text = "";

            }

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            txtNombrePersn.Enabled = true;
            dtpFecha.Enabled = true;
            txtNumPersonas.Enabled = true;
            txtId.Enabled = false;

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
            txtNombrePersn.Text = "";
            dtpFecha.Value = DateTime.Now;
            txtNumPersonas.Text = "";
            txtId.Text = "";

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnA�adir.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;

            txtNombrePersn.Enabled = false;
            dtpFecha.Enabled = false;
            txtNumPersonas.Enabled = false;
            txtId.Enabled = false;

            dgvListado.Enabled = true;
        }

        /// <summary>
        /// M�todo para obtener la informaci�n del formulario.
        /// </summary>
        /// <returns>Contacto con la informaci�n del formulario.</returns>
        public Contacto ObtenerInformacion()
        {
            Contacto contacto = new Contacto();

            contacto.nombre = txtNombrePersn.Text;
            contacto.numPersonas = txtNumPersonas.Text;
            contacto.fecha = dtpFecha.Value;

            if (!String.IsNullOrEmpty(txtId.Text))
            {
                contacto.Id = Convert.ToInt32(txtId.Text);
            }

            return contacto;
        }

        /// <summary>
        /// M�todo que carga y configura el grid.
        /// </summary>
        /// <param name="Contactos">DataTable con la info de los contactos</param>
        public void CargarYConfigurarGrid()
        {
            DataSet ds = Repositorio.ObtenerContactos();
            dgvListado.DataSource = ds.Tables[0];

            // Tama�os columnas
            dgvListado.Columns["ID_Reserva"].Width = 40;
            dgvListado.Columns["Nombre_Reserva"].Width = 150;
            dgvListado.Columns["Fecha"].Width = 150;
            dgvListado.Columns["Num_Personas"].Width = 120;

            // Renombrado columnas
            dgvListado.Columns["Fecha"].HeaderText = "Fecha";

            // Formato fecha en espa�ol
            dgvListado.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Seleccionamos la primera fila del grid si existe
            SeleccionarPrimeraFilaGrid();

        }

        /// <summary>
        /// M�todo que selecciona la primera fila del grid si existe.
        /// </summary>
        public void SeleccionarPrimeraFilaGrid()
        {
            // Si hay alguna fila, seleccionamos la primera
            if (dgvListado.Rows.Count > 0)
            {
                dgvListado.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// M�todo que carga la informaci�n de la fila seleccionada en los controles.
        /// </summary>
        /// <param name="filaSeleccionada"></param>
        public void CargarInfoFilaSeleccionadaFormulario(DataGridViewRow filaSeleccionada)
        {
            txtId.Text = filaSeleccionada.Cells["Id_Reserva"].Value.ToString();
            txtNombrePersn.Text = filaSeleccionada.Cells["Nombre_Reserva"].Value.ToString();
            dtpFecha.Value = (DateTime)filaSeleccionada.Cells["Fecha"].Value;
            txtNumPersonas.Text = filaSeleccionada.Cells["Num_Personas"].Value.ToString();

        }

        /// <summary>
        /// Funci�n que nos indica si la informaci�n obligatoria est� cumplimentada o no.
        /// </summary>
        /// <returns></returns>
        public bool InformacionObligatoriaCumplimentada()
        {
            if(String.IsNullOrEmpty(txtNombrePersn.Text) || String.IsNullOrEmpty(txtNumPersonas.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        private void gbInfoContacto_Enter(object sender, EventArgs e)
        {

        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void lblTelefono_Click(object sender, EventArgs e)
        {

        }
    }
}