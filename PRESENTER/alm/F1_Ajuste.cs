using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using UTILITY.Global;
using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using UTILITY.Enum.EnEstado;
using PRESENTER.frm;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Enum;
using ENTITY.inv.Ajuste.View;

namespace PRESENTER.alm
{
    public partial class F1_Ajuste : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "AJSUTE";
        bool _Limpiar = false;
        int _idOriginal = 0;
        int _MPos = 0;
        int _IdProveedor = 0;
        List<VAjusteDetalle> ListaDetalle = new List<VAjusteDetalle>();
        #endregion

        public F1_Ajuste()
        {
            InitializeComponent();
            MP_Iniciar();
            superTabControl1.SelectedTabIndex = 0;
        }

        private void F1_Compra_Load(object sender, EventArgs e)
        {
            this.Name = _NombreFormulario;
        }
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                MP_CargarAlmacenes();
                //btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
                MP_AsignarPermisos();
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
        public void MP_LimpiarColor()
        {         
            cbAlmacen.BackColor = Color.White;          
        }
        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(cbAlmacen, almacenes);
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
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Compra_Lista().ToList();
                Dgv_GBuscador.DataSource = ListaCompleta;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns["Id"].Caption = "Id";
                Dgv_GBuscador.RootTable.Columns["Id"].Width = 250;
                Dgv_GBuscador.RootTable.Columns["Id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Id"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["Id"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["IdAlmacen"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["IdProvee"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["Proveedor"].Caption = "Proveedor";
                Dgv_GBuscador.RootTable.Columns["Proveedor"].Width = 350;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["FechaDoc"].Caption = "Fecha";
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].Width = 220;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["TipoVenta"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["NombreTipo"].Caption = "Tipo";
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].Width = 350;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Descu"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["Total"].Caption = "Total";
                Dgv_GBuscador.RootTable.Columns["Total"].Width = 250;
                Dgv_GBuscador.RootTable.Columns["Total"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["Total"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["FechaVen"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Factura"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Recibo"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["TipoFactura"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Observ"].Visible = false;

                //Habilitar filtradores
                Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                Dgv_GBuscador.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_GBuscador.GroupByBoxVisible = false;
                Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarDetalle(int id)
        {
            try
            {
                //ListaDetalle = new ServiceDesktop.ServiceDesktopClient().Compra_01_Lista(id).ToList();
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_ArmarDetalle()
        {
            try
            {
                dgjDetalle.DataSource = ListaDetalle;
                dgjDetalle.RetrieveStructure();
                dgjDetalle.AlternatingColors = true;

                dgjDetalle.RootTable.Columns["id"].Visible = false;
                dgjDetalle.RootTable.Columns["IdCompra"].Visible = false;
                dgjDetalle.RootTable.Columns["IdProducto"].Visible = false;
                dgjDetalle.RootTable.Columns["Estado"].Visible = false;

                dgjDetalle.RootTable.Columns["Producto"].Caption = "PRODUCTO";
                dgjDetalle.RootTable.Columns["Producto"].Width = 200;
                dgjDetalle.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjDetalle.RootTable.Columns["Producto"].Visible = true;

                dgjDetalle.RootTable.Columns["Cantidad"].Caption = "CANTIDAD";
                dgjDetalle.RootTable.Columns["Cantidad"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["Cantidad"].Width = 150;
                dgjDetalle.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["Cantidad"].Visible = true;

                dgjDetalle.RootTable.Columns["Unidad"].Caption = " UN.";
                dgjDetalle.RootTable.Columns["Unidad"].Width = 90;
                dgjDetalle.RootTable.Columns["Unidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Unidad"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Unidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjDetalle.RootTable.Columns["Unidad"].Visible = true;

                dgjDetalle.RootTable.Columns["Costo"].Caption = "PRECIO U.";
                dgjDetalle.RootTable.Columns["Costo"].Width = 90;
                dgjDetalle.RootTable.Columns["Costo"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["Costo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Costo"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Costo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["Costo"].Visible = true;

                dgjDetalle.RootTable.Columns["Total"].Caption = "SUBTOTAL";
                dgjDetalle.RootTable.Columns["Total"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["Total"].Width = 150;
                dgjDetalle.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Total"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["Total"].Visible = true;

                dgjDetalle.RootTable.Columns["Lote"].Caption = "Lote";
                //Dgv_Detalle.RootTable.Columns["Lote"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["Lote"].Width = 120;
                dgjDetalle.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["Lote"].Visible = true;

                dgjDetalle.RootTable.Columns["FechaVen"].Caption = "Fecha Ven.";
                //Dgv_Detalle.RootTable.Columns["Lote"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["FechaVen"].Width = 120;
                dgjDetalle.RootTable.Columns["FechaVen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["FechaVen"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["FechaVen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["FechaVen"].Visible = true;

                //Dgv_Detalle.RootTable.Columns["Lote"].Visible = false;
                //Dgv_Detalle.RootTable.Columns["FechaVen"].Visible = false;

                dgjDetalle.RootTable.Columns["Utilidad"].Caption = "UTILIDAD(%)";
                dgjDetalle.RootTable.Columns["Utilidad"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["Utilidad"].Width = 150;
                dgjDetalle.RootTable.Columns["Utilidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Utilidad"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Utilidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["Utilidad"].Visible = true;

                dgjDetalle.RootTable.Columns["Porcent"].Caption = "PRECIO VENTA";
                dgjDetalle.RootTable.Columns["Porcent"].Width = 150;
                dgjDetalle.RootTable.Columns["Porcent"].FormatString = "0.00";
                dgjDetalle.RootTable.Columns["Porcent"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjDetalle.RootTable.Columns["Porcent"].CellStyle.FontSize = 9;
                dgjDetalle.RootTable.Columns["Porcent"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                dgjDetalle.RootTable.Columns["Porcent"].Visible = true;

                dgjDetalle.GroupByBoxVisible = false;
                dgjDetalle.VisualStyle = VisualStyle.Office2007;
                dgjDetalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(dgjDetalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_CargarProducto(List<ENTITY.Producto.View.VProductoLista> result)
        {
            try
            {
                dgjProducto.DataSource = result;
                dgjProducto.RetrieveStructure();
                dgjProducto.AlternatingColors = true;
                dgjProducto.RootTable.Columns["id"].Visible = false;

                dgjProducto.RootTable.Columns["Codigo"].Caption = "Codigo";
                dgjProducto.RootTable.Columns["Codigo"].Width = 100;
                dgjProducto.RootTable.Columns["Codigo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjProducto.RootTable.Columns["Codigo"].CellStyle.FontSize = 8;
                dgjProducto.RootTable.Columns["Codigo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjProducto.RootTable.Columns["Codigo"].Visible = true;

                dgjProducto.RootTable.Columns["Descripcion"].Caption = "Descripcion";
                dgjProducto.RootTable.Columns["Descripcion"].Width = 150;
                dgjProducto.RootTable.Columns["Descripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjProducto.RootTable.Columns["Descripcion"].CellStyle.FontSize = 8;
                dgjProducto.RootTable.Columns["Descripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjProducto.RootTable.Columns["Descripcion"].Visible = true;

                dgjProducto.RootTable.Columns["Grupo1"].Caption = "División";
                dgjProducto.RootTable.Columns["Grupo1"].Width = 120;
                dgjProducto.RootTable.Columns["Grupo1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjProducto.RootTable.Columns["Grupo1"].CellStyle.FontSize = 8;
                dgjProducto.RootTable.Columns["Grupo1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjProducto.RootTable.Columns["Grupo1"].Visible = true;

                dgjProducto.RootTable.Columns["Grupo2"].Caption = "Tipo";
                dgjProducto.RootTable.Columns["Grupo2"].Width = 120;
                dgjProducto.RootTable.Columns["Grupo2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjProducto.RootTable.Columns["Grupo2"].CellStyle.FontSize = 8;
                dgjProducto.RootTable.Columns["Grupo2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjProducto.RootTable.Columns["Grupo2"].Visible = true;


                dgjProducto.RootTable.Columns["Grupo3"].Caption = "CategorIas";
                dgjProducto.RootTable.Columns["Grupo3"].Width = 120;
                dgjProducto.RootTable.Columns["Grupo3"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                dgjProducto.RootTable.Columns["Grupo3"].CellStyle.FontSize = 8;
                dgjProducto.RootTable.Columns["Grupo3"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                dgjProducto.RootTable.Columns["Grupo3"].Visible = true;

                dgjProducto.RootTable.Columns["Tipo"].Visible = false;
                dgjProducto.RootTable.Columns["Usuario"].Visible = false;
                dgjProducto.RootTable.Columns["Hora"].Visible = false;
                dgjProducto.RootTable.Columns["Fecha"].Visible = false;

                //Habilitar filtradores
                dgjProducto.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                dgjProducto.FilterMode = FilterMode.Automatic;
                dgjProducto.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                dgjProducto.GroupByBoxVisible = false;
                dgjProducto.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_InicioArmarCombo()
        {
            UTGlobal.MG_ArmarComboSucursal(cbAlmacen,
                                              new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());
           
        }
        private void MP_SeleccionarButtonCombo(MultiColumnCombo combo, ButtonX btn)
        {
            try
            {
                if (combo.SelectedIndex < 0 && combo.Text != string.Empty)
                {
                    btn.Visible = true;
                }
                else
                {
                    btn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void MP_Habilitar()
        {
            //Tb_Id.ReadOnly = false;
            dtiFecha.IsInputReadOnly = false;
            tbObs.ReadOnly = false;
            cbAlmacen.ReadOnly = false;  
            dgjDetalle.Enabled = true;
        }
        private void MP_InHabilitar()
        {
            tbCodigo.ReadOnly = true;
            dtiFecha.IsInputReadOnly = true;
            tbObs.ReadOnly = true;
            cbAlmacen.ReadOnly = true;
            dgjDetalle.Enabled = true;
            _Limpiar = false;
        }
        private void MP_Limpiar()
        {
            try
            {
                tbCodigo.Clear();
                dtiFecha.Value = DateTime.Today;
                tbObs.Clear();              
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo_Almacen(cbAlmacen);
                }
                dgjDetalle.DataSource = null;
                ListaDetalle.Clear();
                MP_LimpiarColor();
                MP_AddFila();
                // ((DataTable)Dgv_Detalle.DataSource).Clear();
                //  Dgv_Detalle.DataSource = null;
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
                _idOriginal = (int)Dgv_GBuscador.GetValue("id");
                if (_idOriginal != 0)
                {
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Compra_Lista().Where(A => A.Id == _idOriginal).ToList();
                    var Lista = ListaCompleta.First();
                    tbCodigo.Text = Lista.Id.ToString();
                    cbAlmacen.Value = Lista.IdAlmacen;
                    _IdProveedor = Lista.IdProvee;
                    dtiFecha.Value = Lista.FechaDoc;             
                    tbObs.Text = Lista.Observ;
                    MP_CargarDetalle(Convert.ToInt32(tbCodigo.Text));

                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
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
   
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_AddFila()
        {
            try
            {
                var fechaVencimiento = Convert.ToDateTime("2017-01-01");
                int idDetalle = ((List<VCompra_01>)dgjDetalle.DataSource) == null ? 0 : ((List<VCompra_01>)dgjDetalle.DataSource).Max(c => c.Id);
                int posicion = ((List<VCompra_01>)dgjDetalle.DataSource) == null ? 0 : ((List<VCompra_01>)dgjDetalle.DataSource).Count;
                VCompra_01 nuevo = new VCompra_01()
                {
                    Id = idDetalle + 1,
                    IdCompra = 0,
                    Estado = 0,
                    IdProducto = 0,
                    Producto = "",
                    Unidad = "",                  
                    Cantidad = 0,
                    Costo = 0,
                    Total = 0,
                    Lote= "20170101",
                    FechaVen = fechaVencimiento,
                    Utilidad = 0,
                    Porcent = 0
                };
                //ListaDetalle.Insert(posicion, nuevo);
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InHabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = false;
               //GPanel_Producto.Height = 30;
                dgjDetalle.Select();
                dgjDetalle.Col = 5;
                dgjDetalle.Row = dgjDetalle.RowCount - 1;               
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InsertarProducto()
        {
            try
            {
                List<ENTITY.Producto.View.VProductoLista> result;
                //Productos materia prima
                GPanel_Producto.Text = "PRODUCTOS DE COMERCIALES";
                result = new ServiceDesktop.ServiceDesktopClient().ProductoListar().Where(p => p.Tipo.Equals(1)).ToList();
                MP_CargarProducto(result);
                MP_HabilitarProducto();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_HabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = true;
               // GPanel_Producto.Height = 350; 
                dgjProducto.Focus();
                dgjProducto.MoveTo(dgjProducto.FilterRow);
                dgjProducto.Col = 3;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_VerificarSeleccion(string columna)
        {
            try
            {
                if (dgjDetalle.Col == dgjDetalle.RootTable.Columns[columna].Index)
                {
                    if (dgjDetalle.GetValue("Producto").ToString() != string.Empty && dgjDetalle.GetValue("IdProducto").ToString() != string.Empty)
                    {
                        MP_AddFila();
                        MP_HabilitarProducto();
                        MP_InsertarProducto();
                    }
                    else
                        throw new Exception("Seleccione un producto");
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_EliminarFila()
        {
            try
            {
                if (dgjDetalle.RowCount > 1)
                {
                    dgjDetalle.UpdateData();
                    int estado = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Estado"].Value);
                    int idDetalle = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Id"].Value);
                    if (estado == (int)ENEstado.NUEVO)
                    {
                        //Elimina
                        //ListaDetalle = ((List<VCompra_01>)dgjDetalle.DataSource).ToList();
                        //var lista = ListaDetalle.Where(t => t.Id == idDetalle).FirstOrDefault();
                        //ListaDetalle.Remove(lista);
                        //MP_ArmarDetalle();
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO || estado == (int)ENEstado.MODIFICAR)
                        {
                            dgjDetalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.ELIMINAR;
                            dgjDetalle.UpdateData();
                            dgjDetalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(dgjDetalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
                        }

                    }
                }
                else
                    throw new Exception("El detalle no puede estar vacio");
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion
        #region Eventos
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
        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (tbObs.ReadOnly == false)
                {
                    if (e.Column.Index == dgjDetalle.RootTable.Columns["Cantidad"].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns["Utilidad"].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns["Costo"].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns["Porcent"].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns["Lote"].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns["FechaVen"].Index)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                dgjDetalle.UpdateData();
                int estado = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Estado"].Value);
                if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                {
                    MP_CalcularFila();
                }
                else
                {
                    if (estado == (int)ENEstado.GUARDADO)
                    {
                        MP_CalcularFila();
                        dgjDetalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.MODIFICAR;
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CalcularFila()
        {
            try
            {
                Double cantidad, precioCompra, total, utilidad, precioVenta, porcentaje;
                cantidad = Convert.ToDouble(dgjDetalle.CurrentRow.Cells["Cantidad"].Value);
                precioCompra = Convert.ToDouble(dgjDetalle.CurrentRow.Cells["Costo"].Value);
                total = cantidad * precioCompra;
                dgjDetalle.CurrentRow.Cells["Total"].Value = total;
                if (dgjDetalle.Col == dgjDetalle.RootTable.Columns["Porcent"].Index)
                {
                    precioVenta = Convert.ToDouble(dgjDetalle.CurrentRow.Cells["Porcent"].Value);
                    utilidad = ((precioVenta - precioCompra) * 100) / precioCompra;
                    dgjDetalle.CurrentRow.Cells["utilidad"].Value = utilidad;
                }
                if (dgjDetalle.Col == dgjDetalle.RootTable.Columns["Utilidad"].Index)
                {
                    utilidad = Convert.ToDouble(dgjDetalle.CurrentRow.Cells["Utilidad"].Value);
                    porcentaje = (precioCompra + ((precioCompra)) * (utilidad / 100));
                    dgjDetalle.CurrentRow.Cells["Porcent"].Value = porcentaje;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Producto_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (tbObs.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        var idDetalle = Convert.ToInt32(dgjDetalle.GetValue("id"));
                        if (idDetalle > 0)
                        {
                            var idProducto = Convert.ToInt32(dgjProducto.GetValue("Id"));
                            var Lista = new ServiceDesktop.ServiceDesktopClient().ProductoListarXId(idProducto);
                            var UnidadVenta = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo((int)ENEstaticosGrupo.PRODUCTO,
                                                                                                             (int)ENEstaticosOrden.PRODUCTO_UN_VENTA)
                                                                                                             .Where(l => l.IdLibreria == Lista.UniVenta)
                                                                                                             .Select(l => l.Descripcion)
                                                                                                             .FirstOrDefault();
                            var IdSucursal = new ServiceDesktop.ServiceDesktopClient().AlmacenListar()
                                                                                    .Where(a => a.Id == Convert.ToInt32(cbAlmacen.Value))
                                                                                    .Select(a => a.SucursalId).FirstOrDefault();
                            var PrecioCompra = new ServiceDesktop.ServiceDesktopClient().PrecioProductoListar(IdSucursal).Where(
                                                                                    p => p.IdProducto == idProducto
                                                                                    && p.IdPrecioCat == (int)ENCategoriaPrecio.COSTO).Select(
                                                                                    p => p.Precio).FirstOrDefault();
                            var PrecioVenta = new ServiceDesktop.ServiceDesktopClient().PrecioProductoListar(IdSucursal).Where(
                                                                          p => p.IdProducto == idProducto
                                                                          && p.IdPrecioCat == (int)ENCategoriaPrecio.VENTA).Select(
                                                                          p => p.Precio).FirstOrDefault();


                            //ListaDetalle = (List<VCompra_01>)dgjDetalle.DataSource;
                            //foreach (var fila in ListaDetalle)
                            //{
                            //    if (idProducto == fila.IdProducto)
                            //    {
                            //        throw new Exception("El producto ya fue seleccionado");
                            //    }
                            //    if (fila.Id == idDetalle)
                            //    {
                            //        fila.IdProducto = Lista.Id;
                            //        fila.Producto = Lista.Descripcion;
                            //        fila.Unidad = UnidadVenta;
                            //        fila.Cantidad = 1;
                            //        fila.Costo = PrecioCompra;
                            //        fila.Porcent = PrecioVenta;
                            //        fila.Utilidad = (((PrecioVenta - PrecioCompra) * 100) / PrecioCompra);
                            //        fila.Total = PrecioCompra;
                            //    }
                            //}
                            MP_ArmarDetalle();
                            MP_InHabilitarProducto();
                        }
                    }
                    if (e.KeyData == Keys.Escape)
                    {
                        MP_InHabilitarProducto();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Producto_EditingCell_1(object sender, EditingCellEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Detalle_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (tbObs.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        MP_VerificarSeleccion("Producto");
                        MP_VerificarSeleccion("Cantidad");
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter && dgjDetalle.Row >= 0
                        && dgjDetalle.Col == dgjDetalle.RootTable.Columns["Producto"].Index)
                    {
                        int estado = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Estado"].Value);
                        if (estado == (int)ENEstado.NUEVO)
                        {

                            MP_InsertarProducto();
                        }
                        else
                        {
                            string mensaje = "-No se puede cambiar de producto " + "\n" +
                                             "-Modifique la cantidad";
                            throw new Exception(mensaje);
                        }

                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        //Eliminar FIla.
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
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
        #endregion
        #region Metodo heredados
        private void MP_VerificarExistenciaLote()
        {
            int IdCompra = Convert.ToInt32(tbCodigo.Text);
            var compra_01 = new ServiceDesktop.ServiceDesktopClient().Compra_01_Lista(IdCompra).ToList();
            var detalle = ((List<VCompra_01>)dgjDetalle.DataSource).ToList();
            foreach (var item in detalle)
            {
                if (new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ExisteEnSeleccion(IdCompra))
                {
                    throw new Exception("La compra esta asociado a una Seleccion.");
                }
                var producto = compra_01.Where(p=> p.IdProducto == item.IdProducto).FirstOrDefault();
                if (item.Lote == producto.Lote && item.FechaVen == producto.FechaVen)
                {

                }
            }
           
        }
        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {
                string mensaje = "";
                VCompra CompraIngreso = new VCompra()
                {
                    IdAlmacen = Convert.ToInt32(cbAlmacen.Value),
                    IdProvee = _IdProveedor,
                    Estado = (int)ENEstado.GUARDADO,
                    FechaDoc = dtiFecha.Value,
                    Observ = tbObs.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                int id = tbCodigo.Text == string.Empty ? 0 : Convert.ToInt32(tbCodigo.Text);
                int idAux = id;
                var detalle = ((List<VCompra_01>)dgjDetalle.DataSource).ToArray<VCompra_01>();
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                resultado = new ServiceDesktop.ServiceDesktopClient().CompraGuardar(CompraIngreso, detalle, ref id, ref LMensaje, TxtNombreUsu.Text);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {                     
                        MP_Filtrar(1);
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                    }
                    else//Modificar
                    {
                        MP_Filtrar(2);
                        MP_InHabilitar();//El formulario
                        _Limpiar = true;
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                        MH_Habilitar();//El menu                   
                    }
                }
                //Resultado
                if (resultado)
                {

                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                {
                    //Obtiene los productos sin stock
                    var mensajeLista = LMensaje.ToList();
                    if (mensajeLista.Count > 0)
                    {
                        var mensajes = "";
                        foreach (var item in mensajeLista)
                        {
                            mensajes = mensajes + "- " + item + "\n";
                        }
                        MP_MostrarMensajeError(mensajes);
                        return false;
                    }
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
        public override bool MH_Eliminar()
        {
            try
            {
                int IdCompra= Convert.ToInt32(tbCodigo.Text);             
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
                    resul = new ServiceDesktop.ServiceDesktopClient().CompraModificarEstado(IdCompra, (int)ENEstado.ELIMINAR, ref LMensaje);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, tbCodigo.Text));
                    }
                    else
                    {
                        //Obtiene los codigos de productos sin stock
                        var mensajeLista = LMensaje.ToList();
                        if (mensajeLista.Count > 0)
                        {
                            var mensaje = "";
                            foreach (var item in mensajeLista)
                            {
                                mensaje = mensaje + "- " + item + "\n";
                            }
                            MP_MostrarMensajeError(mensaje);
                            return false;
                        }
                        else
                        {
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, tbCodigo.Text));
                        }
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
        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();

        }        
        public override void MH_Modificar()
        {
            
            MP_Habilitar();
        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(1);
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {
                if (cbAlmacen.SelectedIndex == -1)
                {
                    cbAlmacen.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cbAlmacen.BackColor = Color.White;
                if (((List<VCompra_01>)dgjDetalle.DataSource).Count() == 0)
                {
                    _Error = true;
                    throw new Exception("El detalle se encuentra vacio");
                }
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }
        }
        #endregion
    }
}
