using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
using UTILITY.Global;
using UTILITY.Enum;
using UTILITY.Enum.EnEstaticos;
using ENTITY.Libreria.View;
using Janus.Windows.GridEX.EditControls;
using MetroFramework.Controls;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using ENTITY.Usuario.View;
using System.IO;
using PRESENTER.frm;

namespace PRESENTER.adm
{
    public partial class F1_Usuarios : MODEL.ModeloF1
    {
        #region Variables

        string _NombreFormulario = "USUARIOS";
        int _idOriginal = 0;
        int _MPos;
        bool _Limpiar = false;
        
        #endregion

        #region Manejo de eventos
        public F1_Usuarios()
        {
            InitializeComponent();
            MP_Iniciar();
            superTabControl1.SelectedTabIndex = 0;
        }
        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }
        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_GBuscador_DoubleClick(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row > -1)
            {
                superTabControl1.SelectedTabIndex = 0;
            }
        }

        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (BtnGrabar.Enabled)
            {
                if (e.Column.Key == "Acceso")
                {
                    e.Cancel = false;
                }
                else
                    e.Cancel = true;

            }
            else
                e.Cancel = true;
        }

        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            int estado = Convert.ToInt32(Dgv_Detalle.GetValue("Estado"));
            if (estado == 1)
                Dgv_Detalle.SetValue("Estado", 2);
            
        }
        private void btnPrimero_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = 0;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (_MPos > 0 && Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = _MPos - 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (_MPos < Dgv_GBuscador.RowCount - 1 && _MPos >= 0)
                {
                    _MPos = Dgv_GBuscador.Row + 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = Dgv_GBuscador.RowCount - 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                var ListaDetalle = ((List<VUsuario_01>)Dgv_Detalle.DataSource).ToList();
               
                foreach (var fila in ListaDetalle)
                {
                    fila.Acceso = true;

                    if (fila.Estado == 1)
                    {
                        fila.Estado = 2;
                    }
                }
                Dgv_Detalle.DataSource = ListaDetalle;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion


        #region Metodos Privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                MP_CargarComboRol(cbRol);
                MP_CargarEncabezado();
                MP_InHabilitar();
                MP_AsignarPermisos();

            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }

        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

        }
        private void MP_Limpiar()
        {
            try
            {
                tbIdUsuario.Clear();
                tbUsuario.Clear();
                tbContraseña.Clear();
                cbRol.SelectedIndex = -1;
                swEstado.Value = true;                

                MP_CargarGridDetalle(-1);

                tbUsuario.Focus();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_Habilitar()
        {    
            tbUsuario.ReadOnly = false;
            tbContraseña.ReadOnly = false;
            cbRol.ReadOnly = false;
            swEstado.IsReadOnly = false;
            Dgv_Detalle.Enabled = true;
            btnSeleccionarTodos.Enabled = true;

        }
        private void MP_InHabilitar()
        {
            tbIdUsuario.ReadOnly = true;
            tbUsuario.ReadOnly = true;
            tbContraseña.ReadOnly = true;
            cbRol.ReadOnly = true;
            swEstado.IsReadOnly = true;
            btnSeleccionarTodos.Enabled = false;
            //Dgv_Detalle.Enabled = false;        
            
            
            _Limpiar = false;
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarEncabezado();
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = 0;
                MP_MostrarRegistro(tipo == 1 ? _MPos : Dgv_GBuscador.RowCount - 1);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }
        private void MP_CargarComboRol(MultiColumnCombo combo)
        {
            try
            {
                var rol = new ServiceDesktop.ServiceDesktopClient().ListarRol().ToList();
                
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("idRol").Width = 70;
                combo.DropDownList.Columns[0].Caption = "Código";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Rol1").Width = 220;
                combo.DropDownList.Columns[1].Caption = "Rol";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "idRol";
                combo.DisplayMember = "Rol1";
                combo.DropDownList.DataSource = rol;
                combo.DropDownList.Refresh();
                //combo.Font= new Font(this.Font, FontStyle.Bold);
               
                combo.Value = 1;
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
            
        private void MP_CargarEncabezado()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ListarUsuario().ToList();
                Dgv_GBuscador.DataSource = ListaCompleta;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns["IdUsuario"].Caption = "IdUsuario";
                Dgv_GBuscador.RootTable.Columns["IdUsuario"].Width = 100;                     
                Dgv_GBuscador.RootTable.Columns["IdUsuario"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["User"].Caption = "Usuario";
                Dgv_GBuscador.RootTable.Columns["User"].Width = 350;
                Dgv_GBuscador.RootTable.Columns["User"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;           
                Dgv_GBuscador.RootTable.Columns["User"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Password"].Caption = "Password";
                Dgv_GBuscador.RootTable.Columns["Password"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["Password"].Visible = false;              
              
                Dgv_GBuscador.RootTable.Columns["IdRol"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["Fecha"].Caption = "Fecha";
                Dgv_GBuscador.RootTable.Columns["Fecha"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Hora"].Caption = "Hora";
                Dgv_GBuscador.RootTable.Columns["Hora"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["Hora"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Usuario1"].Caption = "Usuario";
                Dgv_GBuscador.RootTable.Columns["Usuario1"].Width = 200;
                Dgv_GBuscador.RootTable.Columns["Usuario1"].Visible = true;

                //Habilitar filtradores
                Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                Dgv_GBuscador.GroupByBoxVisible = false;
                Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarGridDetalle(int idUsuario)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ListarDetalleUsuario_01(idUsuario).ToList();

                Dgv_Detalle.DataSource = result;
                Dgv_Detalle.RetrieveStructure();
                Dgv_Detalle.AlternatingColors = true;

                if (result.Count > 0)
                {
         
                    Dgv_Detalle.RootTable.Columns["IdUsuario_01"].Visible = false;
                
                    Dgv_Detalle.RootTable.Columns["IdUsuario"].Visible = false;
                 
                    Dgv_Detalle.RootTable.Columns["IdAlmacen"].Caption = "IdAlmacen";
                    Dgv_Detalle.RootTable.Columns["IdAlmacen"].Width = 120;
                    Dgv_Detalle.RootTable.Columns["IdAlmacen"].Visible = true;
           
                    Dgv_Detalle.RootTable.Columns["DescripAlm"].Caption = "Almacen";
                    Dgv_Detalle.RootTable.Columns["DescripAlm"].Width = 480;
                    Dgv_Detalle.RootTable.Columns["DescripAlm"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["IdSuc"].Key = "IdSuc";
                    Dgv_Detalle.RootTable.Columns["IdSuc"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["DescripSuc"].Caption = "Sucursal";
                    Dgv_Detalle.RootTable.Columns["DescripSuc"].Width = 250;
                    Dgv_Detalle.RootTable.Columns["DescripSuc"].Visible = true;
                  
                    
                    Dgv_Detalle.RootTable.Columns["Acceso"].Visible = true;
                    Dgv_Detalle.RootTable.Columns["Acceso"].Caption = "Acceso";
                    //Dgv_Detalle.RootTable.Columns["Estado"].ShowRowSelector = true;
                    

                    Dgv_Detalle.RootTable.Columns["Estado"].Caption = "Estado";
                    Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;
                    

                    Dgv_Detalle.RootTable.Columns["Fecha"].Key = "Fecha";
                    Dgv_Detalle.RootTable.Columns["Fecha"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Hora"].Key = "Hora";
                    Dgv_Detalle.RootTable.Columns["Hora"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Usuario"].Key = "Usuario";
                    Dgv_Detalle.RootTable.Columns["Usuario"].Visible = false;


                    //Dgv_Detalle.RootTable.Columns["Usuario"].Visible = false;


                    //Habilitar filtradores
                    Dgv_Detalle.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Detalle.SelectionMode = SelectionMode.SingleSelection;
                    Dgv_Detalle.FilterMode = FilterMode.Automatic;
                    Dgv_Detalle.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_Detalle.RowHeaders = InheritableBoolean.True;
                    Dgv_Detalle.GroupByBoxVisible = false;
                    Dgv_Detalle.VisualStyle = VisualStyle.Office2007;                   
                   
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }        

        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_GBuscador.Row = _Pos;
                _idOriginal = (int)Dgv_GBuscador.GetValue("IdUsuario");
                if (_idOriginal != 0)
                {
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ListarUsuario().Where(A => A.IdUsuario == _idOriginal).ToList();
                    var Lista = ListaCompleta.First();

                    tbIdUsuario.Text = Lista.IdUsuario.ToString();
                    tbUsuario.Text = Lista.User;
                    tbContraseña.Text = Lista.Password;
                    cbRol.Value = Lista.IdRol;
                    swEstado.Value = Convert.ToBoolean(Lista.Estado);
                   

                    MP_CargarGridDetalle(Convert.ToInt32(tbIdUsuario.Text));
                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_AsignarPermisos()
        {
            try
            {
                DataTable dtRolUsu = new ServiceDesktop.ServiceDesktopClient().AsignarPermisos((UTGlobal.UsuarioRol).ToString(), _NombreProg);
                //DataTable dtRolUsu = new ServiceDesktop.ServiceDesktopClient().AsignarPermisos("1", "Metro_Usuarios");
                if (dtRolUsu.Rows.Count > 0)
                {
                    bool show = Convert.ToBoolean(dtRolUsu.Rows[0]["Show"]);
                    bool add = Convert.ToBoolean(dtRolUsu.Rows[0]["Add"]);
                    bool modif = Convert.ToBoolean(dtRolUsu.Rows[0]["Mod"]);
                    bool del = Convert.ToBoolean(dtRolUsu.Rows[0]["Del"]);

                    if (add == false)
                        BtnNuevo.Visible = false;
                    if (modif == false)
                        BtnModificar.Visible = false;
                    if (del == false)
                        BtnEliminar.Visible = false;
                }


            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion

        #region Métodos Heredados
        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();
        }

        public override bool MH_Validar()
        {
            bool _Error = false;
            MEP.Clear();
            try
            {
                if (tbUsuario.Text == "")
                {                                     
                    _Error = true;
                    MEP.SetError(tbUsuario, "ingrese el usuario!".ToUpper());                    
                }
                else
                    tbUsuario.BackColor = Color.White;

                if (tbContraseña.Text == "")
                {
                    _Error = true;
                    MEP.SetError(tbContraseña, "ingrese la contraseña!".ToUpper());
                }
                else
                    tbContraseña.BackColor = Color.White;

                if (cbRol.SelectedIndex < 0)
                {
                    _Error = true;
                    MEP.SetError(tbContraseña, "seleccione un rol!".ToUpper());
                }
                else
                    tbContraseña.BackColor = Color.White;


                var ListaDetalle = ((List<VUsuario_01>)Dgv_Detalle.DataSource).ToList();                        
                int seleccionados = ListaDetalle.Where(x => x.Acceso == true).ToList().Count;
               
                if (seleccionados == 0)
                {
                    ToastNotification.Show(this, "Seleccione al menos un almacen!".ToUpper(),
                                          PRESENTER.Properties.Resources.WARNING,
                                         (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
                    _Error = true;
                }
               

                MHighlighterFocus.UpdateHighlights();
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error = true;
            }
        }

        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {
                string mensaje = "";
                VUsuario vUsuario = new VUsuario()
                {
                    User=tbUsuario.Text,
                    Password=tbContraseña.Text,
                    IdRol= Convert.ToInt32(cbRol.Value),
                    Estado= Convert.ToInt32(swEstado.Value),
                    Usuario1 = UTGlobal.Usuario,

                };
                int idUsuario = tbIdUsuario.Text == string.Empty ? 0 : Convert.ToInt32(tbIdUsuario.Text);                
                int idAux = idUsuario;

                //List<VUsuario_01> List1 = (List<VUsuario_01>)Dgv_Detalle.DataSource;
                //List<VUsuario_01> List2 = new List<VUsuario_01>();               

                //for (int i = 0; i <= List1.Count - 1; i += 1)
                //{                   
                //    if (List1[i].Acceso == true)
                //        List2.Add(List1[i]);                    
                //}                                
                //var detalle = ((List<VUsuario_01>)List2).ToArray<VUsuario_01>();

                var detalle = ((List<VUsuario_01>)Dgv_Detalle.DataSource).ToArray<VUsuario_01>();
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                resultado = new ServiceDesktop.ServiceDesktopClient().UsuarioGuardar(vUsuario, detalle, ref idUsuario, UTGlobal.Usuario);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        tbUsuario.Focus();
                        MP_Filtrar(1);
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, idUsuario.ToString());
                    }
                    else//Modificar
                    {
                        MP_Filtrar(2);
                        MP_InHabilitar();//El formulario
                        _Limpiar = true;
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, idUsuario.ToString());
                        MH_Habilitar();//El menu                   
                    }
                }

                if (resultado)
                {

                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                {
                    mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return resultado;
            }
        }
        public override void MH_Modificar()
        {
            MP_Habilitar();
        }

        public override bool MH_Eliminar()
        {
            try
            {
                int IdUsuario = Convert.ToInt32(tbIdUsuario.Text);
                Efecto efecto = new Efecto();
                efecto.Tipo = 2;
                efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
                efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
                efecto.ShowDialog();
                bool resul = false;
                if (efecto.Band)
                {
                    List<string> Mensaje = new List<string>();
                    var LMensaje = Mensaje.ToArray();
                    var detalle = ((List<VUsuario_01>)Dgv_Detalle.DataSource).ToArray<VUsuario_01>();
                    resul = new ServiceDesktop.ServiceDesktopClient().UsuarioEliminar(IdUsuario, detalle);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, tbIdUsuario.Text));
                    }
                    else
                    {
                        MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, tbIdUsuario.Text));
                    }
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(1);
        }

        #endregion

       
    }
}
