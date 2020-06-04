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
using ENTITY.Rol.View;
using System.IO;
using PRESENTER.frm;


namespace PRESENTER.adm
{
    public partial class F1_Roles : ModeloF1
    {
        #region Variables

        string _NombreFormulario = "ROLES";       
        int _idOriginal = 0;
        int _MPos;
        bool _Limpiar = false;
        

        #endregion

        #region Manejo de eventos

        public F1_Roles()
        {
            InitializeComponent();
            MP_Iniciar();
            superTabControl1.SelectedTabIndex = 0;

        }
        private void Dgv_Modulos_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_Modulos.Focus())
            {
                MP_SeleccionarModulo();
            }           
        }
        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (BtnGrabar.Enabled)
            {
                if (e.Column.Key == "Show" || e.Column.Key == "Add" || e.Column.Key == "Mod" || e.Column.Key == "Del")
                {
                    e.Cancel = false;                    
                }
                else
                    e.Cancel = true;
            }
        }

        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            int estado = Convert.ToInt32(Dgv_Detalle.GetValue("Estado"));
            if (estado == 1)
                Dgv_Detalle.SetValue("Estado", 2);
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

        private void SELECCIONARTODOSSHOWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MP_SeleccionarTodos("Show");
        }
        private void SELECCIONARTODOSADDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MP_SeleccionarTodos("Add");
        }

        private void SELECCIONARTODOSEDITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MP_SeleccionarTodos("Mod");
        }

        private void SELECCIONARTODOSDELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MP_SeleccionarTodos("Del");
        }

        private void Dgv_GBuscador_DoubleClick(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row > -1)
            {
                superTabControl1.SelectedTabIndex = 0;
            }
        }
        

        #endregion

        #region Metodos Privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;                
                MP_CargarModulos();
                MP_CargarEncabezado();
                MP_InHabilitar();
                MP_AsignarPermisos();

            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }

        }
        void MP_SeleccionarModulo()
        {
            try
            {
                if (Dgv_Detalle.RowCount > 0)
                {
                    string numiModulo = Dgv_Modulos.GetValue("IdLibreria").ToString();
                    string desc = Dgv_Modulos.GetValue("Descripcion").ToString();
                    Dgv_Detalle.RemoveFilters();
                    Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["IdModulo"], Janus.Windows.GridEX.ConditionOperator.Equal, numiModulo));
                    GroupPanel2.Text = "privilegios del modulo ".ToUpper() + desc;
                }
                else
                    MP_MostrarMensajeError("No existe datos para mostrar, agregué nuevo rol");

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

        private void MP_CargarModulos()
        {
            try
            {
                
                var result = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.ROL),
                                                                                           Convert.ToInt32(ENEstaticosOrden.ROL_ORDEN)).ToList();
                Dgv_Modulos.DataSource = result;
                if (result.Count > 0)
                {
                    Dgv_Modulos.RetrieveStructure();
                    Dgv_Modulos.AlternatingColors = true;

                    Dgv_Modulos.RootTable.Columns["IdLibreria"].Key = "IdLibreria";
                    Dgv_Modulos.RootTable.Columns["IdLibreria"].Caption = "Id";                  
                    Dgv_Modulos.RootTable.Columns["IdLibreria"].Visible = false;              


                    Dgv_Modulos.RootTable.Columns["Descripcion"].Key = "Descripcion";
                    Dgv_Modulos.RootTable.Columns["Descripcion"].Caption = "MÓDULO";
                    Dgv_Modulos.RootTable.Columns["Descripcion"].Width = 230;                 
                    Dgv_Modulos.RootTable.Columns["Descripcion"].CellStyle.FontSize = 9;
                    Dgv_Modulos.RootTable.Columns["Descripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Modulos.RootTable.Columns["Descripcion"].Visible = true;

                   

                    //Habilitar filtradores
                    Dgv_Modulos.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Modulos.SelectionMode = SelectionMode.SingleSelection;
                    Dgv_Modulos.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;                   
                    Dgv_Modulos.RowHeaders = InheritableBoolean.True;                    
                    Dgv_Modulos.GroupByBoxVisible = false;
                    Dgv_Modulos.VisualStyle = VisualStyle.Office2007;                   
            
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarGridDetalle(int idRol)
        {
            try
            {

                var result = new ServiceDesktop.ServiceDesktopClient().ListarDetalleRol_01(idRol).ToList();               
                
                Dgv_Detalle.DataSource = result;
                Dgv_Detalle.RetrieveStructure();
                Dgv_Detalle.AlternatingColors = true;

                if (result.Count > 0)
                {

                    Dgv_Detalle.RootTable.Columns["IdRol_01"].Key = "IdRol_01";                  
                    Dgv_Detalle.RootTable.Columns["IdRol_01"].Visible = false;


                    Dgv_Detalle.RootTable.Columns["IdRol"].Key = "IdRol";         
                    Dgv_Detalle.RootTable.Columns["IdRol"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["IdPrograma"].Key = "IdPrograma";
                    Dgv_Detalle.RootTable.Columns["IdPrograma"].Caption = "IdPrograma";                   
                    Dgv_Detalle.RootTable.Columns["IdPrograma"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["NombreProg"].Key = "NombreProg";
                    Dgv_Detalle.RootTable.Columns["NombreProg"].Caption = "Nombre";
                    Dgv_Detalle.RootTable.Columns["NombreProg"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Descripcion"].Key = "Descripcion";
                    Dgv_Detalle.RootTable.Columns["Descripcion"].Caption = "Programa";
                    Dgv_Detalle.RootTable.Columns["Descripcion"].Width = 330;
                    Dgv_Detalle.RootTable.Columns["Descripcion"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["IdModulo"].Key = "IdModulo";
                    Dgv_Detalle.RootTable.Columns["IdModulo"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Estado"].Key = "Estado";
                    Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;                   

                    Dgv_Detalle.RootTable.Columns["Show"].Key = "Show";
                    Dgv_Detalle.RootTable.Columns["Show"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Add"].Key = "Add";
                    Dgv_Detalle.RootTable.Columns["Add"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Mod"].Key = "Mod";
                    Dgv_Detalle.RootTable.Columns["Mod"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Del"].Key = "Del";
                    Dgv_Detalle.RootTable.Columns["Del"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Fecha"].Key = "Fecha";
                    Dgv_Detalle.RootTable.Columns["Fecha"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Hora"].Key = "Hora";
                    Dgv_Detalle.RootTable.Columns["Hora"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Usuario"].Key = "Usuario";
                    Dgv_Detalle.RootTable.Columns["Usuario"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Rol"].Key = "Rol";
                    Dgv_Detalle.RootTable.Columns["Rol"].Visible = false;


                    //Habilitar filtradores
                    Dgv_Detalle.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Detalle.SelectionMode = SelectionMode.SingleSelection;
                    Dgv_Detalle.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_Detalle.RowHeaders = InheritableBoolean.True;
                    Dgv_Detalle.GroupByBoxVisible = false;
                    Dgv_Detalle.VisualStyle = VisualStyle.Office2007;

                    //Filtro por la primera fila del modulo
                    MP_SeleccionarModulo();            

                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        
        private void MP_Habilitar()
        {            
            tbRol.ReadOnly = false;            
            Dgv_Detalle.Enabled = true;

            Dgv_Modulos.ContextMenuStrip = msModulos;

        }
        private void MP_InHabilitar()
        {
            tbIdRol.ReadOnly = true;
            tbRol.ReadOnly = true;
            Dgv_Detalle.Enabled = false;
            _Limpiar = false;
        }
        private void MP_Limpiar()
        {
            try
            {
                tbIdRol.Clear();
                tbRol.Clear();
                tbRol.Focus();

                MP_CargarGridDetalle(-1);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

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
        private void MP_CargarEncabezado()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ListarRol().ToList();
                Dgv_GBuscador.DataSource = ListaCompleta;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns["IdRol"].Caption = "Id";
                Dgv_GBuscador.RootTable.Columns["IdRol"].Width = 100;
                //Dgv_GBuscador.RootTable.Columns["IdRol"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                //Dgv_GBuscador.RootTable.Columns["IdRol"].CellStyle.FontSize = 8;             
                Dgv_GBuscador.RootTable.Columns["IdRol"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Rol1"].Caption = "Rol";
                Dgv_GBuscador.RootTable.Columns["Rol1"].Width = 450;
                Dgv_GBuscador.RootTable.Columns["Rol1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;             
                Dgv_GBuscador.RootTable.Columns["Rol1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Rol1"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Fecha"].Caption = "Fecha";
                Dgv_GBuscador.RootTable.Columns["Fecha"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Hora"].Caption = "Hora";
                Dgv_GBuscador.RootTable.Columns["Fecha"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["Hora"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Usuario"].Caption = "Usuario";
                Dgv_GBuscador.RootTable.Columns["Usuario"].Width = 200;
                Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = true;

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
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_GBuscador.Row = _Pos;
                _idOriginal = (int)Dgv_GBuscador.GetValue("IdRol");
                if (_idOriginal != 0)
                {
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ListarRol().Where(A => A.IdRol == _idOriginal).ToList();
                    var Lista = ListaCompleta.First();
                    tbIdRol.Text = Lista.IdRol.ToString();
                    tbRol.Text = Lista.Rol1;

                    MP_CargarGridDetalle(Convert.ToInt32(tbIdRol.Text));                
                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_SeleccionarTodos(string columna)
        {
            try
            {
                var ListaDetalle = ((List<VRol_01>)Dgv_Detalle.DataSource).ToList();
                int numiModulo = Convert.ToInt32(Dgv_Modulos.GetValue("IdLibreria"));

                foreach (var fila in ListaDetalle)
                {
                    if (fila.IdModulo == numiModulo)
                    {
                        if (columna == "Show")
                        {
                            fila.Show = true;
                        }
                        if (columna == "Add")
                        {
                            fila.Add = true;
                        }
                        if (columna == "Mod")
                        {
                            fila.Mod = true;
                        }
                        if (columna == "Del")
                        {
                            fila.Del = true;
                        }

                        if (fila.Estado == 1)
                        {
                            fila.Estado = 2;
                        }
                    }
                }
                Dgv_Detalle.DataSource = ListaDetalle;
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
                if (tbRol.Text == "")
                {
                    //tbRol.BackColor = Color.Red;                    
                    _Error = true;
                    MEP.SetError(tbRol, "ingrese la descripcion del rol!".ToUpper());
                    //ToastNotification.Show(this, "ingrese la descripción del rol!".ToUpper(), 
                    //                        PRESENTER.Properties.Resources.WARNING,
                    //                        (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

                }
                else 
                    tbRol.BackColor = Color.White;


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
                VRol vRol = new VRol()
                {
                    Rol1 = tbRol.Text,                    
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("HH:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                int idRol = tbIdRol.Text == string.Empty ? 0 : Convert.ToInt32(tbIdRol.Text);              
                int idAux = idRol;
                var detalle = ((List<VRol_01>)Dgv_Detalle.DataSource).ToArray<VRol_01>();
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                resultado = new ServiceDesktop.ServiceDesktopClient().RolGuardar(vRol, detalle, ref idRol, UTGlobal.Usuario);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        tbRol.Focus();
                        MP_Filtrar(1);
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, idRol.ToString());
                    }
                    else//Modificar
                    {
                        MP_Filtrar(2);
                        MP_InHabilitar();//El formulario
                        _Limpiar = true;
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, idRol.ToString());
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
                int IdRol = Convert.ToInt32(tbIdRol.Text);
               
                List<string> lMensaje = new List<string>();
                if (new ServiceDesktop.ServiceDesktopClient().RolExisteEnUsuario(IdRol))
                {
                    lMensaje.Add("El Rol esta asociado a un Usuario, no puede eliminarlo. Modifique o elimine primero el Usuario.");
                }
                if (lMensaje.Count > 0)
                {
                    var mensaje = "";
                    foreach (var item in lMensaje)
                    {
                        mensaje = mensaje + "- " + item + "\n";
                    }
                    MP_MostrarMensajeError(mensaje);
                    return false;
                }

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
                    var detalle = ((List<VRol_01>)Dgv_Detalle.DataSource).ToArray<VRol_01>();
                    resul = new ServiceDesktop.ServiceDesktopClient().RolEliminar(IdRol,detalle);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, tbIdRol.Text));
                    }
                    else
                    {                   
                        MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, tbIdRol.Text));                       
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
