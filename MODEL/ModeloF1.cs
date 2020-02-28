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

namespace MODEL
{
    public partial class ModeloF1 : Form
    {
        #region VARIABLES 
        //VM= VARIABLE DE MODELO
        public static bool VM_Nuevo;
        #endregion
        #region CONFIGURACION DEL FORMULARIO
        public ModeloF1()
        {
            InitializeComponent();
            //Componentes que pueden mover el formulario
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModeloF1_MouseMove);
            this.PanelSuperior.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModeloF1_MouseMove);
            this.PictureBoxLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModeloF1_MouseMove);
            this.PanelSuperior.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.superTabControl1.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.LblSubtitulo.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.PanelInferior.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.PanelContenidoRegistro.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.PanelMenu.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.PanelContenidoBuscar.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            this.reflectionLabelLogo.MouseMove += new MouseEventHandler(ModeloF1_MouseMove);
            MP_Habilitar();
        }
        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;

        // Declaraciones del API 
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        // 
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void ModeloF1_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        #endregion
        #region EVENTOS DEL FORMULARIO
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void MFlyoutUsuario_PrepareContent(object sender, EventArgs e)
        {
            if (sender == BubbleBarUsuario && BtnGrabar.Enabled == false)
            {
                //MFlyoutUsuario.BorderColor = Color.FromArgb(HC0, 0, 0);
                MFlyoutUsuario.Content = PanelUsuario;
            }
        }
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            VM_Nuevo = true;
            MP_Inhabilitar();
            MH_Nuevo();
        }
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            VM_Nuevo = false;
            MP_Inhabilitar();
            MH_Modificar();
        }
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            MH_Eliminar();
        }
        private void ModeloF1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (MH_Validar() == true)
                return;                     
            MH_NuevoRegistro();
            if (VM_Nuevo)
                MH_Habilitar();
            else
                MH_Inhanbilitar();
  
        }
        private void BtnAtras_Click(object sender, EventArgs e)
        {
            if (BtnGrabar.Enabled == true)
            {
                MP_Habilitar();
                MH_Salir();
            }
            else
                this.Close();

        }
        #endregion
        #region METODOS PRIVADOS 
        public void MP_IniciarTodo()
        {
            TxtNombreUsu.Text = UTGlobal.Usuario;
            TxtNombreUsu.ReadOnly = true;
            this.SuperTabBuscar.Visible = false;
        }
        private void MP_Inhabilitar()
        {
            BtnNuevo.Enabled = false;
            BtnModificar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnGrabar.Enabled = true;            
            BtnImprimir.Enabled = false;
            BtnExportar.Enabled = false;
            PanelNavegacion.Enabled = false;
            SuperTabBuscar.Enabled = false;
        }
        private void MP_Habilitar()
        {
            BtnNuevo.Enabled = true;
            BtnModificar.Enabled = true;
            BtnEliminar.Enabled = true;
            BtnGrabar.Enabled = false;           
            BtnImprimir.Enabled = true;
            PanelNavegacion.Enabled = true;
            SuperTabBuscar.Enabled = true;
            BtnExportar.Enabled = true;
        }
        private void MP_Moverenfoque()
        {
            SendKeys.Send("{TAB}");
        }
        #endregion
        #region METODOS HEREDADOS (OVERRIDABLES)
        public virtual bool MH_NuevoRegistro() { return true; }
        public virtual void MH_Nuevo() { }
        public virtual void MH_Modificar() { }
        public virtual bool MH_Grabar() { return true; }
        public virtual bool MH_Validar() { return true; }
        public virtual bool MH_Eliminar() { return true; }
        public virtual void MH_Habilitar() {
            MP_Habilitar();
        }
        public virtual void MH_Inhanbilitar() {
           
        }
        public virtual void MH_Salir() { }
        public virtual void MH_Limpiar() { }

        #endregion


    } 
}
