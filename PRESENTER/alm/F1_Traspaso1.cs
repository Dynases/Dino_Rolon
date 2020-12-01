using DevComponents.DotNetBar;
using ENTITY.inv.TI001.VIew;
using ENTITY.inv.Traspaso.View;
using ENTITY.Libreria.View;
using ENTITY.Producto.View;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using MODEL;
using PRESENTER.frm;
using PRESENTER.Properties;
using PRESENTER.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY;
using UTILITY.Enum;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Traspaso1 : ModeloF1
    {
        public F1_Traspaso1()
        {
            InitializeComponent();
            this.MP_CargarEncabezado();
            this.MP_InHabilitar();
            this.MP_CargarAlmacenes();
            this.MP_AsignarPermisos();            
            this.LblTitulo.Text = "TRASPASO";
            this.Text = "TRASPASO";
            superTabControl1.SelectedPanel = PanelContenidoRegistro;
            BtnHabilitar.Visible = true;
            btnTransportadoPor.Visible = false;
        }
        #region Variables   
        private static List<VTraspaso> LTraspaso;
        private static List<VTraspaso_01> LTraspasoDetalle;
        private static VProductoListaStock vProductos = new VProductoListaStock();
        private static int index;
        private bool _Limpiar = false;
        #endregion
        #region Metodos Privados
        private void MP_CargarAlmacenes()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var libreria = servicio.LibreriaListarCombo((int)ENEstaticosGrupo.TRASPASO, 
                                                               (int)ENEstaticosOrden.TRASPASO_TRASPASADOPOR).ToList();
                    var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                    UTGlobal.MG_ArmarComboAlmacen(Cb_Destino, almacenes);
                    UTGlobal.MG_ArmarComboAlmacen(Cb_Origen, almacenes);
                    UTGlobal.MG_ArmarCombo(Cb_TransportePor, libreria);
                }               
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
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
        private void MP_InHabilitar()
        {
            this.Tb_UsuarioEnvio.ReadOnly = true;
            this.Tb_UsuarioRecibe.ReadOnly = true;
            this.Tb_Observacion.ReadOnly = true;
            this.Cb_Destino.ReadOnly = true;
            this.Cb_Origen.ReadOnly = true;
            this.Sw_Estado.IsReadOnly = true;
            this.tb_TotalUnidad.IsInputReadOnly = true;
            this.Tb_Total.IsInputReadOnly = true;
            this.Tb_FechaEnvio.Enabled = false;
            this.Tb_FechaDestino.Enabled = false;
            Cb_TransportePor.ReadOnly = true;
            BtnHabilitar.Enabled = true;
            this.Tb_Id.ReadOnly = true;
            _Limpiar = false;
            //Panel_Productos.Visible = false;
        }

        private void MP_Habilitar()
        {
            this.Tb_Observacion.ReadOnly = false;
            this.Cb_Destino.ReadOnly = false;
            this.Cb_Origen.ReadOnly = false;
            this.Tb_FechaEnvio.Enabled = false;
            Tb_FechaDestino.Enabled = true;
            BtnHabilitar.Enabled = false;
            Cb_TransportePor.ReadOnly = false;
            Sw_Estado.IsReadOnly = Sw_Estado.Value ? false : true;
        }
       
        private void MP_InHabilitarProducto()
        {
            try
            {
                Panel_Productos.Visible = false;
                Panel_Productos.Height = 30;                
                Dgv_Detalle.Select();
                Dgv_Detalle.Col = 7;
                Dgv_Detalle.Row = Dgv_Detalle.RowCount - 1;
                vProductos = new VProductoListaStock();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }       

        private void MP_CargarEncabezado()
        {           
            try
            {
                LTraspaso = new ServiceDesktop.ServiceDesktopClient().TraerTraspasos(UTGlobal.UsuarioId).ToList();               
                MP_ArmarEncabezado();
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_MostrarRegistro(int index)
        {
            try
            {
                if (index < Dgv_GBuscador.RowCount - 1)
                {
                    var traspaso = LTraspaso[index];

                    Tb_Id.Text = traspaso.Id.ToString();
                    Cb_Destino.Value = traspaso.IdAlmacenDestino;
                    Cb_Origen.Value = traspaso.IdAlmacenOrigen;
                    Tb_UsuarioEnvio.Text = traspaso.Usuario;
                    Tb_FechaEnvio.Value = traspaso.Fecha;
                    Tb_FechaDestino.Value = traspaso.Fecha;
                    Sw_Estado.Value = traspaso.EstadoEnvio == 1 ? true : false;
                    Tb_Observacion.Text = traspaso.Observaciones;
                    Cb_TransportePor.Value = traspaso.TransportadoPor;
                    this.MP_CargarDetalleRegistro(traspaso.Id, 1);
                    this.MP_ObtenerCalculo();
                    this.LblPaginacion.Text = (index + 1) + "/" + Convert.ToString(Dgv_GBuscador.RowCount - 1) + " Traspaso";
                }
                   
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
           
        }

        private void MP_CargarDetalleRegistro(int id, int EsPlantilla)
        {
            try
            {
                if (EsPlantilla == 1)
                {
                    LTraspasoDetalle = new ServiceDesktop.ServiceDesktopClient().TraerTraspasos_01(id).ToList();
                }
                else
                    LTraspasoDetalle = new ServiceDesktop.ServiceDesktopClient().TraerTraspasos_01Vacio(id).ToList();
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ArmarLotes(List<VTI001> listaLotes)
        {
            Dgv_Producto.DataSource = listaLotes;
            Dgv_Producto.RetrieveStructure();
            Dgv_Producto.AlternatingColors = true;

            Dgv_Producto.RootTable.Columns["IdAlmacen"].Visible = false;
            Dgv_Producto.RootTable.Columns["IdProducto"].Visible = false;
            Dgv_Producto.RootTable.Columns["Unidad"].Visible = false;

            Dgv_Producto.RootTable.Columns["Lote"].Caption = "Lote";
            Dgv_Producto.RootTable.Columns["Lote"].Width = 150;
            Dgv_Producto.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Producto.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
            Dgv_Producto.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Producto.RootTable.Columns["Lote"].Visible = true;

            Dgv_Producto.RootTable.Columns["FechaVen"].Caption = "FechaVen";
            Dgv_Producto.RootTable.Columns["FechaVen"].Width = 150;
            Dgv_Producto.RootTable.Columns["FechaVen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Producto.RootTable.Columns["FechaVen"].CellStyle.FontSize = 9;
            Dgv_Producto.RootTable.Columns["FechaVen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Producto.RootTable.Columns["FechaVen"].Visible = true;

            Dgv_Producto.RootTable.Columns["Cantidad"].Caption = "Stock";
            Dgv_Producto.RootTable.Columns["Cantidad"].Width = 150;
            Dgv_Producto.RootTable.Columns["Cantidad"].FormatString = "0.00";
            Dgv_Producto.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Producto.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
            Dgv_Producto.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Producto.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_Producto.RootTable.Columns["id"].Visible = false;

            Dgv_Producto.GroupByBoxVisible = false;
            Dgv_Producto.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            Dgv_Producto.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Producto.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));

        }
        private void MP_ArmarDetalle()
        {
            Dgv_Detalle.DataSource = LTraspasoDetalle;
            Dgv_Detalle.RetrieveStructure();
            Dgv_Detalle.AlternatingColors = true;

            Dgv_Detalle.RootTable.Columns["Id"].Visible = false;
            Dgv_Detalle.RootTable.Columns["IdTraspaso"].Visible = false;
            Dgv_Detalle.RootTable.Columns["IdProducto"].Visible = false;
            Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;

            Dgv_Detalle.RootTable.Columns["CodigoProducto"].Caption = "CODIGO";
            Dgv_Detalle.RootTable.Columns["CodigoProducto"].Width = 80;
            Dgv_Detalle.RootTable.Columns["CodigoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Detalle.RootTable.Columns["CodigoProducto"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Producto"].Caption = "PRODUCTO";
            Dgv_Detalle.RootTable.Columns["Producto"].Width = 240;
            Dgv_Detalle.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Detalle.RootTable.Columns["Producto"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Unidad"].Caption = "UN.";
            Dgv_Detalle.RootTable.Columns["Unidad"].Width = 80;
            Dgv_Detalle.RootTable.Columns["Unidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Unidad"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Unidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Unidad"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Cantidad"].Caption = "CANTIDAD";
            Dgv_Detalle.RootTable.Columns["Cantidad"].Width = 130;
            Dgv_Detalle.RootTable.Columns["Cantidad"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Contenido"].Caption = "CONT.";
            Dgv_Detalle.RootTable.Columns["Contenido"].Width = 80;
            Dgv_Detalle.RootTable.Columns["Contenido"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["Contenido"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Contenido"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Contenido"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Contenido"].Visible = true;

            Dgv_Detalle.RootTable.Columns["TotalContenido"].Caption = "TOTAL UN.";
            Dgv_Detalle.RootTable.Columns["TotalContenido"].Width = 100;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["TotalContenido"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Precio"].Caption = "Precio";
            Dgv_Detalle.RootTable.Columns["Precio"].Width = 100;
            Dgv_Detalle.RootTable.Columns["Precio"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["Precio"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Precio"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Precio"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Precio"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Total"].Caption = "TOTAL";
            Dgv_Detalle.RootTable.Columns["Total"].Width = 130;
            Dgv_Detalle.RootTable.Columns["Total"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Total"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Total"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Lote"].Caption = "LOTE";
            Dgv_Detalle.RootTable.Columns["Lote"].Width = 100;
            Dgv_Detalle.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Lote"].Visible = true;

            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].Caption = "FechaVencimiento";
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].Width = 100;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Stock"].Visible = false;

            Dgv_Detalle.RootTable.Columns["Delete"].Caption = "ELIMINAR";
            Dgv_Detalle.RootTable.Columns["Delete"].Width = 60;
            Dgv_Detalle.RootTable.Columns["Delete"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Delete"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Delete"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Delete"].Visible = true;
            Dgv_Detalle.RootTable.Columns["Delete"].Image = Resources.delete;
            Dgv_Detalle.RootTable.Columns["Delete"].EditType = EditType.NoEdit;

            Dgv_Detalle.GroupByBoxVisible = false;
            Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
            Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
        }
        private void MP_Limpiar()
        {
            this.Tb_Id.Clear();
            this.Tb_UsuarioEnvio.Text = UTGlobal.Usuario;
            this.Tb_Observacion.Clear();        
            this.Tb_FechaEnvio.Value = DateTime.Today;
            this.Tb_FechaDestino.Value = DateTime.Today;
            this.Sw_Estado.Value = true;
            if (_Limpiar == false)            {
                
                UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Origen);
                UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Destino);
            }
            index = 0;
            LTraspasoDetalle = new List<VTraspaso_01>();
            this.Dgv_Detalle.DataSource = null;           
            this.MP_AddFila();
        }    
        private void MP_ArmarEncabezado()
        {
            try
            {               
                Dgv_GBuscador.DataSource = LTraspaso;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;
                    
                Dgv_GBuscador.RootTable.Columns["Id"].Visible = false;       
                Dgv_GBuscador.RootTable.Columns["IdAlmacenOrigen"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["AlamacenOrigen"].Caption = "Almacen Origen";
                Dgv_GBuscador.RootTable.Columns["AlamacenOrigen"].Width = 250;
                Dgv_GBuscador.RootTable.Columns["AlamacenOrigen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["AlamacenOrigen"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["AlamacenOrigen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["AlamacenOrigen"].Visible = true;               

                Dgv_GBuscador.RootTable.Columns["IdAlmacenDestino"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["AlmacenDestino"].Caption = "Almacen Destino";
                Dgv_GBuscador.RootTable.Columns["AlmacenDestino"].Width = 250;
                Dgv_GBuscador.RootTable.Columns["AlmacenDestino"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["AlmacenDestino"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["AlmacenDestino"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["AlmacenDestino"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["UsuarioEnvio"].Caption = "Usuario Envio";
                Dgv_GBuscador.RootTable.Columns["UsuarioEnvio"].Width = 120;
                Dgv_GBuscador.RootTable.Columns["UsuarioEnvio"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["UsuarioEnvio"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["UsuarioEnvio"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["UsuarioEnvio"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["UsuarioRecepcion"].Caption = "Usuario Recep.";
                Dgv_GBuscador.RootTable.Columns["UsuarioRecepcion"].Width = 110;
                Dgv_GBuscador.RootTable.Columns["UsuarioRecepcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["UsuarioRecepcion"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["UsuarioRecepcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["UsuarioRecepcion"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["FechaEnvio"].Caption = "Fecha Env.";
                Dgv_GBuscador.RootTable.Columns["FechaEnvio"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["FechaEnvio"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaEnvio"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaEnvio"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaEnvio"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Caption = "Fecha Recep.";
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Observaciones"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["EstadoEnvio"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["EstadoEnvioDescripcion"].Caption = "Estado Env.";
                Dgv_GBuscador.RootTable.Columns["EstadoEnvioDescripcion"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["EstadoEnvioDescripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["EstadoEnvioDescripcion"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["EstadoEnvioDescripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["EstadoEnvioDescripcion"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].Caption = "Total Uni.";
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].AggregateFunction = AggregateFunction.Sum;
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["TotalUnidad"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Total"].Caption = "Total Bs.";
                Dgv_GBuscador.RootTable.Columns["Total"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["Total"].AggregateFunction = AggregateFunction.Sum;
                Dgv_GBuscador.RootTable.Columns["Total"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["Total"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

                //Habilitar filtradores
                Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                Dgv_GBuscador.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                //Diseno
                Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
                Dgv_GBuscador.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
                Dgv_GBuscador.AlternatingColors = true;
                //Dgv_GBuscador.RecordNavigator = true;
                Dgv_GBuscador.GroupByBoxVisible = false;
                //Totalizadoe
                Dgv_GBuscador.TotalRow = InheritableBoolean.True;
                Dgv_GBuscador.TotalRowFormatStyle.FontBold = TriState.True;
                Dgv_GBuscador.TotalRowFormatStyle.BackColor = Color.Gold;
                Dgv_GBuscador.TotalRowPosition = TotalRowPosition.BottomScrollable;
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
                Double saldo,  precioCosto;
                int idProducto, stock, contenido; 
               // string Producto, codPrducto, unidadVenta;
                saldo = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value);
                stock = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Stock"].Value);              
                precioCosto = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Precio"].Value);
                contenido = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Contenido"].Value);
                if (saldo > stock)
                {
                    Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value = 1;
                    Dgv_Detalle.CurrentRow.Cells["Contenido"].Value = contenido;
                    Dgv_Detalle.CurrentRow.Cells["TotalContenido"].Value = contenido;                   
                    Dgv_Detalle.CurrentRow.Cells["Total"].Value = precioCosto;
                    throw new Exception("No existe stock para algun producto");
                }
                else
                {
                    IngresarCantidad(saldo, precioCosto,contenido);
                }
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void IngresarCantidad(double cantidad, double precioCosto, int contenido)
        {
            var totalUnidad = cantidad * contenido;         
            var subTotalCosto = cantidad * precioCosto;          
            Dgv_Detalle.CurrentRow.Cells["TotalContenido"].Value = totalUnidad;
            Dgv_Detalle.CurrentRow.Cells["Total"].Value = subTotalCosto;
        }       
        private void MP_ObtenerCalculo()
        {
            try
            {
                Dgv_Detalle.UpdateData();               
                Tb_Total.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
                tb_TotalUnidad.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["TotalContenido"], AggregateFunction.Sum));
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarProducto(List<VProductoListaStock> lProductosConStock)
        {
            try
            {
                Dgv_Producto.DataSource = lProductosConStock;
                Dgv_Producto.RetrieveStructure();
                Dgv_Producto.AlternatingColors = true;
                Dgv_Producto.RootTable.Columns["IdProducto"].Visible = false;
                Dgv_Producto.RootTable.Columns["IdAlmacen"].Visible = false;
                Dgv_Producto.RootTable.Columns["IdCategoriaPrecio"].Visible = false;
                Dgv_Producto.RootTable.Columns["PrecioMinVenta"].Visible = false;
                Dgv_Producto.RootTable.Columns["PrecioMaxVenta"].Visible = false;
                Dgv_Producto.RootTable.Columns["EsLote"].Visible = false;
                Dgv_Producto.RootTable.Columns["Contenido"].Visible = false;
                Dgv_Producto.RootTable.Columns["TipoProducto"].Visible = false;
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].Visible = false;
                Dgv_Producto.RootTable.Columns["PrecioVenta"].Visible = false;
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].Visible = false;

                Dgv_Producto.RootTable.Columns["CodigoProducto"].Caption = "Codigo";
                Dgv_Producto.RootTable.Columns["CodigoProducto"].Width = 80;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].Visible = true;

                Dgv_Producto.RootTable.Columns["Producto"].Caption = "Producto";
                Dgv_Producto.RootTable.Columns["Producto"].Width = 180;
                Dgv_Producto.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Producto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Producto"].Visible = true;

                Dgv_Producto.RootTable.Columns["Division"].Caption = "División";
                Dgv_Producto.RootTable.Columns["Division"].Width = 100;
                Dgv_Producto.RootTable.Columns["Division"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Division"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Division"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Division"].Visible = true;

                Dgv_Producto.RootTable.Columns["UnidadVenta"].Caption = "UN.";
                Dgv_Producto.RootTable.Columns["UnidadVenta"].Width = 60;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].Visible = true;

                Dgv_Producto.RootTable.Columns["PrecioCosto"].Caption = "P. Costo";
                Dgv_Producto.RootTable.Columns["PrecioCosto"].Width = 100;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].FormatString = "0.00";
                Dgv_Producto.RootTable.Columns["PrecioCosto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].Visible = true;

                Dgv_Producto.RootTable.Columns["Stock"].Caption = "Stock";
                Dgv_Producto.RootTable.Columns["Stock"].Width = 100;
                Dgv_Producto.RootTable.Columns["Stock"].FormatString = "0.00";
                Dgv_Producto.RootTable.Columns["Stock"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Stock"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Stock"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Producto.RootTable.Columns["Stock"].Visible = true;

                //Habilitar filtradores
                Dgv_Producto.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_Producto.FilterMode = FilterMode.Automatic;
                Dgv_Producto.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                Dgv_Producto.GroupByBoxVisible = false;
                Dgv_Producto.VisualStyle = VisualStyle.Office2007;
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

                GPanel_Producto.Text = "SELECCIONE PRODUCTOS";
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var almacen =  servicio.AlmacenListar().ToList() .Find(a => a.Id == Convert.ToInt32(Cb_Origen.Value));
                    var lProductosConStock = servicio.ListarProductosStock(almacen.SucursalId, almacen.Id, (int)ENCategoriaPrecio.COSTO).ToList();
                    if (lProductosConStock == null)
                        lProductosConStock = new List<VProductoListaStock>();
                    MP_CargarProducto(lProductosConStock);
                    MP_HabilitarProducto();
                } 

               
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
                Panel_Productos.Visible = true;
                Panel_Productos.Height = 350;
                Panel_Productos.Width = 650;
                GPanel_Producto.Visible = true;
                Dgv_Producto.Focus();
                Dgv_Producto.MoveTo(Dgv_Producto.FilterRow);
                Dgv_Producto.Col = 5;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        //REVISAR
        private void MP_VerificarSeleccion(string columna)
        {
            try
            {
                if (Dgv_Detalle.Col == Dgv_Detalle.RootTable.Columns[columna].Index)
                {
                    if (Dgv_Detalle.GetValue("Producto").ToString() != string.Empty && Dgv_Detalle.GetValue("IdProducto").ToString() != string.Empty)
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
                if (Dgv_Detalle.RowCount > 1)
                {
                    Dgv_Detalle.UpdateData();
                    int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                    int idDetalle = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Id"].Value);
                    if (estado == (int)ENEstado.NUEVO)
                    {
                        //Elimina
                        LTraspasoDetalle = ((List<VTraspaso_01>)Dgv_Detalle.DataSource).ToList();
                        var lista = LTraspasoDetalle.Where(t => t.Id == idDetalle).FirstOrDefault();
                        LTraspasoDetalle.Remove(lista);
                        MP_ArmarDetalle();  
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO || estado == (int)ENEstado.MODIFICAR)
                        {
                            Dgv_Detalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.ELIMINAR;
                            Dgv_Detalle.UpdateData();
                            Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
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
        private void MP_Filtrar(int tipo)
        {
            MP_CargarEncabezado();
            if (Dgv_GBuscador.RowCount > 0)
            {
                index = 0;
                MP_MostrarRegistro(tipo == 1 ? index : Dgv_GBuscador.Row);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }
        private void MP_AddFila()
        {
            try
            {       
                int idDetalle = ((List<VTraspaso_01>)Dgv_Detalle.DataSource) == null ? 0 : ((List<VTraspaso_01>)Dgv_Detalle.DataSource).Max(c => c.Id);
                int posicion = ((List<VTraspaso_01>)Dgv_Detalle.DataSource) == null ? 0 : ((List<VTraspaso_01>)Dgv_Detalle.DataSource).Count;
                VTraspaso_01 nuevo = new VTraspaso_01()
                {
                    Id = idDetalle + 1,
                    IdTraspaso = 0,
                    IdProducto = 0,
                    Estado = 0,
                    CodigoProducto = "",
                    Producto = "",
                    Unidad = "",
                    Cantidad = 1,
                    Contenido = 0,
                    TotalContenido = 0,
                    Precio = 0,
                    Total = 0,
                    Lote = UTGlobal.lote,
                    FechaVencimiento = UTGlobal.fechaVencimiento,
                    Stock = 0,
                    Delete = null
                };
                LTraspasoDetalle.Insert(posicion, nuevo);
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private static void MP_IngresarProductoDetalle(int idDetalle, int idLote, List<VTI001> lLotes)
        {
            if (lLotes.Count != 0)
            {
                foreach (var vDetalleVenta in LTraspasoDetalle)
                {
                    if (vProductos.IdProducto == vDetalleVenta.IdProducto &&
                        lLotes.FirstOrDefault(a => a.id == idLote).Lote == vDetalleVenta.Lote &&
                        lLotes.FirstOrDefault(a => a.id == idLote).FechaVen == vDetalleVenta.FechaVencimiento)
                    {
                        throw new Exception("El producto ya existe en el detalle");
                    }
                    if (vDetalleVenta.Id == idDetalle)
                    {
                        vDetalleVenta.IdProducto = vProductos.IdProducto;
                        vDetalleVenta.CodigoProducto = vProductos.CodigoProducto;
                        vDetalleVenta.Producto = vProductos.Producto;
                        vDetalleVenta.Unidad = vProductos.UnidadVenta;
                        vDetalleVenta.Cantidad = 1;
                        vDetalleVenta.Contenido = Convert.ToDecimal(vProductos.Contenido);
                        vDetalleVenta.TotalContenido = Convert.ToDecimal(vProductos.Contenido);            
                        vDetalleVenta.Precio = vProductos.PrecioCosto;
                        vDetalleVenta.Total = vProductos.PrecioCosto;
                        vDetalleVenta.Lote = lLotes.FirstOrDefault(a => a.id == idLote).Lote;
                        vDetalleVenta.FechaVencimiento = lLotes.FirstOrDefault(a => a.id == idLote).FechaVen;
                        //Revisar
                        vDetalleVenta.Stock = lLotes.FirstOrDefault(a => a.id == idLote).Cantidad;
                        break;
                    }
                }
                vProductos = new VProductoListaStock();
            }
        }

        private void MP_ActualizarLote(ref List<VTI001> Lotes, int idProducto)
        {
            foreach (var fila in Lotes)
            {
                var sumaCantidad = LTraspasoDetalle.Where(a => a.Lote.Equals(fila.Lote) &&
                                                              a.FechaVencimiento == fila.FechaVen &&
                                                              a.IdProducto == idProducto &&
                                                              a.Estado == 0).Sum(a => a.Cantidad);
                fila.Cantidad = fila.Cantidad - sumaCantidad;
            }
        }
       
        #endregion

        #region Eventos
        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }

        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                Dgv_Detalle.UpdateData();
                int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                {
                    MP_CalcularFila();
                }
                else
                {
                    if (estado == (int)ENEstado.GUARDADO)
                    {
                        MP_CalcularFila();
                        Dgv_Detalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.MODIFICAR;
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (Cb_Origen.ReadOnly == false)
                {
                    if (e.Column.Index == Dgv_Detalle.RootTable.Columns["Cantidad"].Index)
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

        private void Dgv_Detalle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cb_Origen.Enabled == true)
                {
                    if (Convert.ToInt32(Cb_Origen.Value) == Convert.ToInt32(Cb_Destino.Value))
                    {
                        throw new Exception("Almacen origen y destido deben ser distinto");
                    }
                    if (e.KeyData == Keys.Enter)
                    {
                        MP_VerificarSeleccion("CodigoProducto");
                        MP_VerificarSeleccion("Producto");
                        MP_VerificarSeleccion("Cantidad");
                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter &&
                        Dgv_Detalle.Row >= 0)
                    {

                        int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
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
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Dgv_Producto_EditingCell(object sender, EditingCellEventArgs e)
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

        private void Dgv_Producto_KeyDown(object sender, KeyEventArgs e)
        
        {
            try
            {
                if (Cb_Origen.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter && Dgv_Producto.Row > -1)
                    {
                        int idLote = 0;
                        var idDetalle = Convert.ToInt32(Dgv_Detalle.GetValue("id"));              
                        if (idDetalle > 0)
                        {
                            if (vProductos.IdProducto == 0)
                            {

                                var idProducto = Convert.ToInt32(Dgv_Producto.GetValue("IdProducto"));
                                //Obtiene el producto
                                vProductos = ((List<VProductoListaStock>)Dgv_Producto.DataSource)
                                                                               .Where(p => p.IdProducto == idProducto)
                                                                               .FirstOrDefault();
                                if (vProductos.Stock <= 0)
                                {
                                    var producto = vProductos.Producto;
                                    vProductos = new VProductoListaStock();
                                    throw new Exception("El producto " + producto + "no cuenta con STOCK disponible.");
                                }

                                var InventarioLotes = new ServiceDesktop.ServiceDesktopClient()
                                                                         .TraerInventarioLotes(vProductos.IdProducto,
                                                                                                Convert.ToInt32(Cb_Origen.Value)).ToList();
                                //Veridica si se mostrara la seleccion de lotes
                                if (vProductos.EsLote == 2)
                                {
                                    idLote = InventarioLotes.FirstOrDefault().id;
                                    if (idLote == 0)
                                    {
                                        vProductos = new VProductoListaStock();
                                        throw new Exception("El producto no tiene un lote relacionado");
                                    }
                                    MP_IngresarProductoDetalle(idDetalle, idLote, InventarioLotes);
                                    MP_ArmarDetalle();
                                    MP_InHabilitarProducto();
                                    MP_ObtenerCalculo();
                                    return;
                                }
                                //Actualiza el stock de lotes
                                MP_ActualizarLote(ref InventarioLotes, idProducto);
                                MP_ArmarLotes(InventarioLotes);
                                GPanel_Producto.Text = vProductos.Producto;
                            }
                            else
                            {                                
                                idLote = Convert.ToInt32(Dgv_Producto.GetValue("id"));
                                var lLotes = ((List<VTI001>)Dgv_Producto.DataSource);
                                MP_IngresarProductoDetalle(idDetalle, idLote, lLotes);
                                MP_ArmarDetalle();
                                MP_InHabilitarProducto();
                            }
                        }
                        MP_ObtenerCalculo();

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

        private void BtnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                base.MH_Inhanbilitar();
                this.MP_Habilitar();
                this.MP_CargarDetalleRegistro(Convert.ToInt32(Tb_Id.Text), 2);
                this.MP_ObtenerCalculo();
                this.LblPaginacion.Text = (index + 1) + "/" + Dgv_GBuscador.RowCount.ToString() + " Ventas";
                Tb_Id.Text = "";
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
                index = 0;
                this.MP_MostrarRegistro(index);
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
                if (index > 0)
                {
                    index -= 1;
                    this.MP_MostrarRegistro(index);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (index < LTraspaso.Count - 1)
            {
                index += 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                index = LTraspaso.Count - 1;
                this.MP_MostrarRegistro(index);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Sw_Estado_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Tb_Observacion.ReadOnly == false)
                {
                    if (Sw_Estado.Value == false)
                    {
                        if (Tb_Id.Text != string.Empty)
                        {
                            Tb_UsuarioRecibe.Text = UTGlobal.Usuario;
                            Tb_FechaDestino.Enabled = true;
                            Tb_FechaDestino.Value = DateTime.Today;
                            Tb_FechaEnvio.Enabled = false;
                        }
                    }
                    else
                    {
                        Tb_UsuarioRecibe.Text = "";
                        Tb_FechaDestino.Enabled = false;
                        Tb_FechaDestino.Value = DateTime.Today;
                        Tb_FechaEnvio.Enabled = true;
                    }
                }
            }                
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion

        #region Metodos Heredados
        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
            this.MP_Limpiar();
            Sw_Estado.IsReadOnly = true;
        }
        public override void MH_Modificar()
        {
            if (Sw_Estado.Value == false)
            {
                throw new Exception("Traspaso con recepcion no se peude modificar");
            }
            MP_Habilitar();
            Cb_Origen.ReadOnly = true;
        }
        public override void MH_Salir()
        {            
            this.MP_CargarAlmacenes();
            MP_InHabilitar();
            MP_Filtrar(1);
        }
        public void MP_LimpiarColor()
        {
            Cb_Destino.BackColor = Color.White;
            Cb_Origen.BackColor = Color.White;
            Tb_UsuarioEnvio.BackColor = Color.White;
        }
        public override bool MH_Validar()
        {
            var flag = true;

            if (Cb_Destino.SelectedIndex == -1)
            {
                Cb_Destino.BackColor = Color.Red;
                flag = true;
            }
            else
                Cb_Destino.BackColor = Color.White;
            if (Cb_Origen.SelectedIndex == -1)
            {
                Cb_Origen.BackColor = Color.Red;
                flag = true;
            }
            else
                Cb_Origen.BackColor = Color.White;

            if (Tb_UsuarioEnvio.Text == string.Empty)
            {
                Tb_UsuarioEnvio.BackColor = Color.Red;
                flag = true;
            }
            else
                Tb_UsuarioEnvio.BackColor = Color.White;
            if (this.Dgv_Detalle.RowCount == 0)
            {
                this.MP_MostrarMensajeError("El detalle se encuentra vacio no se puede Guardar");
                return flag;
            }
            flag = false;
            return flag;
        }

        public override bool MH_NuevoRegistro()
        {
            try
            {
                string mensaje = "";
                var VTraspaso = new VTraspaso
                {
                    Estado = (int)ENEstado.GUARDADO,
                    IdAlmacenOrigen = Convert.ToInt32(this.Cb_Origen.Value),
                    IdAlmacenDestino = Convert.ToInt32(this.Cb_Destino.Value),
                    FechaEnvio = Tb_FechaEnvio.Value,
                    FechaRecepcion = Tb_FechaDestino.Value,
                    Observaciones = this.Tb_Observacion.Text,
                    UsuarioEnvio = this.Tb_UsuarioEnvio.Text,
                    UsuarioRecepcion = this.Tb_UsuarioRecibe.Text,
                    EstadoEnvio = Sw_Estado.Value ? 1 : 2,
                    TotalUnidad = Convert.ToDecimal(tb_TotalUnidad.Value),
                    Total = Convert.ToDecimal(Tb_Total.Value),                   
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                    TransportadoPor = Convert.ToInt32(Cb_TransportePor.Value)
                };
                int id = Tb_Id.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Id.Text);
                int idAux = id;
                var detalle = ((List<VTraspaso_01>)Dgv_Detalle.DataSource).ToArray<VTraspaso_01>();
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                try
                {
                    if (new ServiceDesktop.ServiceDesktopClient().GuardarTraspaso(VTraspaso, detalle, ref id, ref LMensaje))
                    {
                        MP_Reporte(id);
                        _Limpiar = true;
                        if (idAux == 0)//Registar
                        {
                            Cb_Origen.Focus();
                            this.MP_Filtrar(1);
                            this.MP_Limpiar();
                           
                        }
                        else
                        {                           
                            this.MP_Filtrar(2);
                            this.MP_InHabilitar();                         
                            MH_Habilitar();//El menu                           
                        }
                        mensaje = id == 0 ? mensaje = GLMensaje.Nuevo_Exito(LblTitulo.Text, id.ToString()) :
                                         GLMensaje.Modificar_Exito(LblTitulo.Text, id.ToString());
                        MP_MostrarMensajeExito(mensaje);
                        return true;
                    }
                    else
                    {
                        mensaje = GLMensaje.Registro_Error(LblTitulo.Text);
                        this.MP_MostrarMensajeError(mensaje);
                        return false;
                    }
                }
                
                catch (Exception ex)
                {
                    this.MP_MostrarMensajeError(ex.Message);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }

        public override bool MH_Eliminar()
        {
            try
            {
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
                    new ServiceDesktop.ServiceDesktopClient().EliminarTraspaso(Convert.ToInt32(Tb_Id.Text));
                    MP_Filtrar(1);
                    MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(LblTitulo.Text, Tb_Id.Text));
                    resul = true;
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        #endregion

        private void Cb_TransportePor_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_TransportePor, btnTransportadoPor);
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

        private void btnTransportadoPor_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.TRASPASO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.TRASPASO_TRASPASADOPOR));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.TRASPASO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.TRASPASO_TRASPASADOPOR),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_TransportePor.Text == "" ? "" : Cb_TransportePor.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_TransportePor,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.TRASPASO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.TRASPASO_TRASPASADOPOR)).ToList());
                    Cb_TransportePor.SelectedIndex = ((List<VLibreria>)Cb_TransportePor.DataSource).Count() - 1;
                }               
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (Cb_Origen.ReadOnly == true)
            {
                MP_Reporte(Convert.ToInt32(Tb_Id.Text));
            }
        }
        private void MP_Reporte(int idTraspaso)
        {

            try
            {
                if (idTraspaso == 0)
                {
                    throw new Exception("No existen registros");
                }
                if (UTGlobal.visualizador != null)
                {
                    UTGlobal.visualizador.Close();
                }
                UTGlobal.visualizador = new Visualizador();
                var lista = new ServiceDesktop.ServiceDesktopClient().ReporteTraspaso(idTraspaso).ToList();
                if (lista != null)
                {
                    var ObjetoReport = new RTraspasoTicket();
                    ObjetoReport.SetDataSource(lista);
                    UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
                    ObjetoReport.SetParameterValue("Titulo", "TRASPASO");
                    UTGlobal.visualizador.ShowDialog();
                    UTGlobal.visualizador.BringToFront();
                }
                else
                    throw new Exception("No se encontraron registros");


            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
    }
}
