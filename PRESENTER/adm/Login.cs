using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using UTILITY.Global;
using UTILITY.Enum;
using UTILITY.Enum.EnEstaticos;
using ENTITY.Libreria.View;
using Janus.Windows.GridEX.EditControls;
using MetroFramework.Controls;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using ENTITY.Rol.View;
using System.IO;
using PRESENTER.frm;
using MODEL;
using System.Collections;


namespace PRESENTER.adm
{
    public partial class Login : Form
    {
        Principal principal = new Principal();
        #region Manejo de eventos
        public Login()
        {
            InitializeComponent();
            txtUsuario.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {

                string usu = txtUsuario.Text.ToUpper();
                string pass = txtPassword.Text;
                

                if (txtUsuario.Text == "")
                {
                    ToastNotification.Show(this, "No Puede Dejar Nombre en Blanco..!!!".ToUpper(), PRESENTER.Properties.Resources.WARNING, 1000, eToastGlowColor.Red, eToastPosition.BottomLeft);
                    return;
                }
                if (txtPassword.Text == "")
                {
                    ToastNotification.Show(this, "No Puede Dejar Contraseña en Blanco..!!!".ToUpper(), PRESENTER.Properties.Resources.WARNING, 1000, eToastGlowColor.Red, eToastPosition.BottomLeft);
                    return;
                }
                
               var ListaUsuario = new ServiceDesktop.ServiceDesktopClient().ValidarUsuario(usu, pass).ToList();
               

                if (ListaUsuario.Count > 0)
                {
                    var ListaUsu = ListaUsuario.First();
                    if (ListaUsuario.Count > 0)
                    {
                        UTGlobal.Usuario = txtUsuario.Text;
                        UTGlobal.UsuarioId = Convert.ToInt32(ListaUsu.IdUsuario);
                        UTGlobal.UsuarioRol = Convert.ToInt32(ListaUsu.IdRol);

                        // Asignando el nombre de usuario y rol  a las variables                        
                        MODEL.ModeloF1.MUsuario= UTGlobal.Usuario;

                        Limpiar();
                        this.Hide();

                        if (UTGlobal.Usuario == "DEFAULT")
                        {                            
                            principal.SideNav1.Enabled = false;                           
                        }          

                        
                        else
                        {
                            MP_CargarPrivilegios();
                            principal.SideNav1.Enabled = true;
                            principal.Show();
                            
                        }
                    }
                }
                else
                    ToastNotification.Show(this, "Codigo de Usuario y Password Incorrecto..!!!".ToUpper(), PRESENTER.Properties.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter);

            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }

        }
        #endregion

        #region Metodos Privados
        private void MP_CargarPrivilegios()
        {
            try
            {
                var listaTabs = new List<DevComponents.DotNetBar.Metro.MetroTilePanel>();
                //List<DevComponents.DotNetBar.Metro.MetroTilePanel> listaTabs = new List<DevComponents.DotNetBar.Metro.MetroTilePanel>();
                
                listaTabs.Add(principal.MetroTilePanel1);
                listaTabs.Add(principal.metroTilePanel2);
                listaTabs.Add(principal.MetroTilePanel6);
                listaTabs.Add(principal.MetroTilePanel7);
                listaTabs.Add(principal.metroTilePanel8);
                

                int idRolUsu = UTGlobal.UsuarioRol;

                var dtModulos = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.ROL),
                Convert.ToInt32(ENEstaticosOrden.ROL_ORDEN)).ToList();

                List<string> listFormsModulo = new List<string>();
                              
                    for (var i = 0; i <= dtModulos.Count - 1; i++)
                    {               
                                       
                        var dtDetRol = new ServiceDesktop.ServiceDesktopClient().ListarDetalle(idRolUsu, dtModulos[i].IdLibreria).ToList();
                        listFormsModulo = new List<string>();

                        if (dtDetRol.Count > 0)
                        {
                            // cargo los nombres de los programas(botones) del modulo
                            foreach (var fila in dtDetRol)
                                listFormsModulo.Add(fila.NombreProg);
                            // recorro el modulo(tab) que corresponde
                            foreach (DevComponents.DotNetBar.BaseItem _item in listaTabs[i].Items)
                            {
                                if ((_item) is DevComponents.DotNetBar.Metro.MetroTileItem)
                                {
                                    var Metro_ = (DevComponents.DotNetBar.Metro.MetroTileItem)_item;
                                    if (listFormsModulo.Contains(Metro_.Name))
                                    {
                                        string Texto = Metro_.Text;
                                        string TTexto = Metro_.TitleText;
                                        int f = listFormsModulo.IndexOf(Metro_.Name);
                                        if (Texto == "")
                                            Metro_.TitleText = dtDetRol[f].Descripcion;
                                        else
                                            Metro_.Text = dtDetRol[f].Descripcion;

                                        if (dtDetRol[f].Show == true | dtDetRol[f].Add == true | dtDetRol[f].Mod == true | dtDetRol[f].Del == true)
                                            Metro_.Visible = true;
                                        else
                                            Metro_.Visible = false;
                                    }
                                    else
                                        Metro_.Visible = false;
                                }
                                else
                                // recorro todos los elementos del sub grupo
                                if (_item is ItemContainer)
                                {
                                    foreach (var _subItem in _item.SubItems)
                                    {
                                        MetroTileItem _subBtn = (MetroTileItem)_subItem;
                                        if (listFormsModulo.Contains(_subBtn.Name))
                                        {
                                            string Texto = _subBtn.Text;
                                            string TTexto = _subBtn.TitleText;
                                            int f = listFormsModulo.IndexOf(_subBtn.Name);
                                            if (Texto == "")
                                                _subBtn.TitleText = dtDetRol[f].Descripcion;
                                            else
                                                _subBtn.Text = dtDetRol[f].Descripcion;

                                            if (dtDetRol[f].Show == true | dtDetRol[f].Add == true | dtDetRol[f].Mod == true | dtDetRol[f].Del == true)
                                                _subBtn.Visible = true;
                                            else
                                                _subBtn.Visible = false;
                                        }
                                        else
                                            _subBtn.Visible = false;
                                    }
                                }
                            }
                        }
                        else //no exiten formulario registrados en el modulo pero igual hay que ocultar los botones y los subbotones que tenga

                            foreach (DevComponents.DotNetBar.BaseItem _item in listaTabs[i].Items)
                            {
                                if ((_item) is DevComponents.DotNetBar.Metro.MetroTileItem)
                                {
                                    var Metro_ = (DevComponents.DotNetBar.Metro.MetroTileItem)_item;
                                    Metro_.Visible = false;
                                }
                                else
                                   // recorro todos los elementos del sub grupo
                                   if (_item is ItemContainer)
                                {
                                    foreach (var _subItem in _item.SubItems)
                                    {
                                        MetroTileItem _subBtn = (MetroTileItem)_subItem;
                                        _subBtn.Visible = false;
                                    }
                                }
                            }
                    }              

                // refrescar el formulario
                this.Refresh();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
            
        }

        private void Limpiar()
        {
            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtUsuario.Focus();
        }
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

        }
        #endregion

    }
}
