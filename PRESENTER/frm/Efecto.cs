using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.frm
{
    public partial class Efecto : Form
    {
        public Efecto()
        {
            InitializeComponent();
        }

        private void Efecto_Load(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Maximized;
            switch (Tipo)
            {
                case 1:
                    MP_MostrarMensaje();
                    break;
                case 2:
                    MP_MostrarMensajeDelete();
                    break;
                case 3:
                    MP_MostrarFrmAyuda();
                    break;
            }
        }
        #region Variables
        public string archivo;
        public bool Band = false;
        public string Header = "";
        public int Tipo = 0;
        public string Context = "";
        public DataTable Tabla;
        public List<GLCelda> listaCelda = new List<GLCelda>();
        public int Alto;
        public int Ancho;
        public Janus.Windows.GridEX.GridEXRow Row;
        public int SelectCol = -1;
        #endregion
        #region Metodos

        void MP_MostrarMensaje()
        {
            Bitmap blah = PRESENTER.Properties.Resources.CUESTION;
            Icon ico = Icon.FromHandle(blah.GetHicon());
            if (MessageBox.Show(Context, Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Band = true;
                this.Close();
            }
            else
            {
                Band = false;
                this.Close();
            }
        }
        void MP_MostrarMensajeDelete()
        {
            TaskDialogInfo info = new TaskDialogInfo(Context, eTaskDialogIcon.Delete, "ADVERTENCIA", Header, eTaskDialogButton.Yes | eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Default);
            eTaskDialogResult resultado = TaskDialog.Show(info);
            if (resultado == eTaskDialogResult.Yes)
            {
                string mensajeError = "";
                Band = true;
                this.Close();
            }
            else
            {
                Band = true;
                this.Close();
            }
        }
        void MP_MostrarFrmAyuda()
        {
            MODEL.ModeloAyuda frmAyuda = new MODEL.ModeloAyuda(Alto, Ancho, Tabla, Context.ToUpper(), listaCelda);
            if (SelectCol >= 0)
            {
                frmAyuda.Columna = SelectCol;
                frmAyuda.MP_Seleccionar();
            }
            frmAyuda.ShowDialog();
            if (frmAyuda.seleccionado)
            {
                Row = frmAyuda.filaSelect;
                Band = true;
                this.Close();
            }
            else
            {
                Band = false;
                this.Close();
            }
        }
        #endregion
    }
}
