using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MODEL
{
    public partial class ModeloF2 : Form
    {
        #region VARIABLES 
        //VM= VARIABLE DE MODELO
        public static bool VM_Nuevo;
        public static string MUsuario;
        public static string _NombreProg;
        #endregion
        #region CONFIGURACION DEL FORMULARIO
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
        #endregion
        public ModeloF2()
        {//Componentes que pueden mover el formulario
            InitializeComponent();
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModeloF2_MouseMove);
            this.PanelSuperior.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModeloF2_MouseMove);           
            this.PanelSuperior.MouseMove += new MouseEventHandler(ModeloF2_MouseMove);           
            this.LblSubtitulo.MouseMove += new MouseEventHandler(ModeloF2_MouseMove);
            this.PanelInferior.MouseMove += new MouseEventHandler(ModeloF2_MouseMove);            
            this.PanelMenu.MouseMove += new MouseEventHandler(ModeloF2_MouseMove);           
            this.reflectionLabelLogo.MouseMove += new MouseEventHandler(ModeloF2_MouseMove);    
            MP_Habilitar();
           
        }
        public void MP_IniciarTodo()
        {
            TxtNombreUsu.Text = MUsuario;
            TxtNombreUsu.ReadOnly = true;
            //this.SuperTabBuscar.Visible = false;
        }
        private void ModeloF2_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void MP_Habilitar()
        {          
           
           
            BtnGenerar.Enabled = true;
            PanelNavegacion.Enabled = true;            
            BtnExportar.Enabled = true;
        }
        private void ModeloF2_Load(object sender, EventArgs e)
        {
            MP_IniciarTodo();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
