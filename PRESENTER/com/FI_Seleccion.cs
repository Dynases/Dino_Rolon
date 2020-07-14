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
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;
using ENTITY.com.Seleccion.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.Seleccion_01.View;
using PRESENTER.frm;
using UTILITY.Enum.EnEstado;
using UTILITY;
using PRESENTER.Report;
using System.Data.Entity.ModelConfiguration.Configuration;
using UTILITY.Enum.EnCarpetas;
using System.IO;
using System.Diagnostics;

namespace PRESENTER.com
{
    public partial class FI_Seleccion : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "SELECCION";
        bool _Limpiar = false;
        int _idOriginal = 0;  
        int _MPos = 0;
        int _TipoCompra = 0;
        List<VSeleccion_01_Lista> seleccion_01 = new List<VSeleccion_01_Lista>(); 
        #endregion
        public FI_Seleccion()
        {
            InitializeComponent();
        }

        private void FI_Seleccion_Load(object sender, EventArgs e)
        {
            this.Name = _NombreFormulario;
            superTabControl1.SelectedTabIndex = 0;
            MP_Iniciar();
        }
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                Tb_Merma.Value = 0;
                LblTitulo.Text = _NombreFormulario;
                MP_InicioArmarCombo();
                MP_CargarAlmacenes();
                btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
                MP_AsignarPermisos();
                BtnExportar.Visible = true;
                Dt_FechaDesde.Value = DateTime.Now.Date;
                Dt_FechaHasta.Value = DateTime.Now.Date;
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
        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ArmarEncabezado(List<VSeleccionEncabezado> vSeleccion)        
        {
            try
            {
                Dgv_GBuscador.DataSource = vSeleccion;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns["id"].Caption = "Nota Sel.";
                Dgv_GBuscador.RootTable.Columns["id"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["id"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["id"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["IdCompraIng"].Caption = "Nota Rec.";
                Dgv_GBuscador.RootTable.Columns["IdCompraIng"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["IdCompraIng"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["IdCompraIng"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["IdCompraIng"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["IdCompraIng"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Granja"].Caption = "Nota Granja";
                Dgv_GBuscador.RootTable.Columns["Granja"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["Granja"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Granja"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Granja"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Granja"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Proveedor"].Caption = "Proveedor";
                Dgv_GBuscador.RootTable.Columns["Proveedor"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].Visible = true;


                Dgv_GBuscador.RootTable.Columns["FechaReg"].Caption = "Fecha Sel.";
                Dgv_GBuscador.RootTable.Columns["FechaReg"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["FechaReg"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaReg"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaReg"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaReg"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Caption = "Fecha Rece.";
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["TipoCategoria"].Caption = "Tipo ";
                Dgv_GBuscador.RootTable.Columns["TipoCategoria"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["TipoCategoria"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["TipoCategoria"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["TipoCategoria"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["TipoCategoria"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Merma"].Caption = "Merma";
                Dgv_GBuscador.RootTable.Columns["Merma"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["Merma"].Width = 70;
                Dgv_GBuscador.RootTable.Columns["Merma"].AggregateFunction = AggregateFunction.Sum;
                Dgv_GBuscador.RootTable.Columns["Merma"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Merma"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Merma"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["Merma"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].Caption = "Total Rec.";
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].AggregateFunction = AggregateFunction.Sum;
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["TotalRecepcion"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Cantidad"].Caption = "Total Sel.";
                Dgv_GBuscador.RootTable.Columns["Cantidad"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["Cantidad"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["Cantidad"].AggregateFunction = AggregateFunction.Sum;
                Dgv_GBuscador.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Cantidad"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["Cantidad"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].Caption = "Merma%";
                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["MermaPorcentaje"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].Caption = "Manchado%";
                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].Width = 90;
                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["ManchadoPorcentaje"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].Caption = "Picado%";
                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].Width = 80;
                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["PicadoPorcentaje"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Dias"].Caption = "Dias";
                Dgv_GBuscador.RootTable.Columns["Dias"].FormatString = "0";
                Dgv_GBuscador.RootTable.Columns["Dias"].Width = 40;
                Dgv_GBuscador.RootTable.Columns["Dias"].AggregateFunction = AggregateFunction.Sum;
                Dgv_GBuscador.RootTable.Columns["Dias"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Dias"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Dias"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_GBuscador.RootTable.Columns["Dias"].Visible = true;

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

        private void MP_CargarEncabezado()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().TraerSelecciones().ToList();
                MP_ArmarEncabezado(ListaCompleta);
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
                var lresult = new ServiceDesktop.ServiceDesktopClient().Seleccion_01_ListarXId_CompraIng_01(id,2).ToList();
                if (lresult.Count() > 0)
                {
                    Dgv_Detalle.DataSource = lresult;
                    Dgv_Detalle.RetrieveStructure();
                    Dgv_Detalle.AlternatingColors = true;

                    Dgv_Detalle.RootTable.Columns["id"].Visible = false;
                    Dgv_Detalle.RootTable.Columns["IdSeleccion"].Visible = false;
                    Dgv_Detalle.RootTable.Columns["IdProducto"].Visible = false;
                    Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Producto"].Caption = "PRODUCTO";
                    Dgv_Detalle.RootTable.Columns["Producto"].Width = 140;
                    Dgv_Detalle.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Detalle.RootTable.Columns["Producto"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Cantidad"].Key = "Cantidad";
                    Dgv_Detalle.RootTable.Columns["Cantidad"].Caption = "CANT.";
                    Dgv_Detalle.RootTable.Columns["Cantidad"].FormatString = "0";
                    Dgv_Detalle.RootTable.Columns["Cantidad"].Width = 60;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Porcen"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Precio"].Caption = "PRECIO";
                    Dgv_Detalle.RootTable.Columns["Precio"].FormatString = "0.0000";
                    Dgv_Detalle.RootTable.Columns["Precio"].Width = 70;
                    Dgv_Detalle.RootTable.Columns["Precio"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Precio"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Precio"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Detalle.RootTable.Columns["Precio"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Total"].Caption = "TOTAL";
                    Dgv_Detalle.RootTable.Columns["Total"].FormatString = "0.00";
                    Dgv_Detalle.RootTable.Columns["Total"].Width = 90;
                    Dgv_Detalle.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Total"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Detalle.RootTable.Columns["Total"].Visible = true;

                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Detalle.GroupByBoxVisible = false;
                    Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
                }               
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarDetalle2(int id)
        {
            try
            {
              
                seleccion_01 = new ServiceDesktop.ServiceDesktopClient().TraerSeleccion_01(id).ToList();
                ArmarDetalle(seleccion_01);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void ArmarDetalle(List<VSeleccion_01_Lista> lresult)
        {
            try
            {
                if (lresult.Count() > 0)
                {
                    Dgv_Seleccion.DataSource = lresult;
                    Dgv_Seleccion.RetrieveStructure();
                    Dgv_Seleccion.AlternatingColors = true;

                    Dgv_Seleccion.RootTable.Columns["id"].Visible = false;
                    Dgv_Seleccion.RootTable.Columns["IdSeleccion"].Visible = false;
                    Dgv_Seleccion.RootTable.Columns["IdProducto"].Visible = false;
                    Dgv_Seleccion.RootTable.Columns["Estado"].Visible = false;

                    Dgv_Seleccion.RootTable.Columns["Producto"].Caption = "PRODUCTO";
                    Dgv_Seleccion.RootTable.Columns["Producto"].Width = 140;
                    Dgv_Seleccion.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Seleccion.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
                    Dgv_Seleccion.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Seleccion.RootTable.Columns["Producto"].Visible = true;

                    Dgv_Seleccion.RootTable.Columns["Cantidad"].Key = "Cantidad";
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].Caption = "CANT.";
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].FormatString = "0";
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].Width = 75;
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Seleccion.RootTable.Columns["Cantidad"].Visible = true;

                    Dgv_Seleccion.RootTable.Columns["Porcen"].Key = "Porcen";
                    Dgv_Seleccion.RootTable.Columns["Porcen"].Caption = "%";
                    Dgv_Seleccion.RootTable.Columns["Porcen"].FormatString = "0.00";
                    Dgv_Seleccion.RootTable.Columns["Porcen"].Width = 50;
                    Dgv_Seleccion.RootTable.Columns["Porcen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Seleccion.RootTable.Columns["Porcen"].CellStyle.FontSize = 9;
                    Dgv_Seleccion.RootTable.Columns["Porcen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Seleccion.RootTable.Columns["Porcen"].Visible = true;

                    Dgv_Seleccion.RootTable.Columns["Precio"].Caption = "PRECIO";
                    Dgv_Seleccion.RootTable.Columns["Precio"].FormatString = "0.0000";
                    Dgv_Seleccion.RootTable.Columns["Precio"].Width = 75;
                    Dgv_Seleccion.RootTable.Columns["Precio"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Seleccion.RootTable.Columns["Precio"].CellStyle.FontSize = 9;
                    Dgv_Seleccion.RootTable.Columns["Precio"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Seleccion.RootTable.Columns["Precio"].Visible = true;

                    Dgv_Seleccion.RootTable.Columns["Total"].Caption = "TOTAL";
                    Dgv_Seleccion.RootTable.Columns["Total"].FormatString = "0.00";
                    Dgv_Seleccion.RootTable.Columns["Total"].Width = 90;
                    Dgv_Seleccion.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Seleccion.RootTable.Columns["Total"].CellStyle.FontSize = 9;
                    Dgv_Seleccion.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Seleccion.RootTable.Columns["Total"].Visible = true;

                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Seleccion.GroupByBoxVisible = false;
                    Dgv_Seleccion.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void MP_CargarDetalle_Nuevo(int IdCompraIngreso_01)
        {
            try
            {
                //Consulta segun un Categoria
                //Consulta segun un Id de Ingreso

                if (_TipoCompra == 1)
                {
                    seleccion_01 = new ServiceDesktop.ServiceDesktopClient().Seleccion_01_ListarXId_CompraIng_01_XSeleccion(IdCompraIngreso_01).Where(a => !a.Producto.Contains("SIN SELECCION")).ToList();
                }
                else
                {
                    if (_TipoCompra == 2)
                    {
                        seleccion_01 = new ServiceDesktop.ServiceDesktopClient().Seleccion_01_ListarXId_CompraIng_01(IdCompraIngreso_01, 1).Where(a => !a.Producto.Contains("SIN SELECCION")).ToList();
                    }
                }              
                ArmarDetalle(seleccion_01);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }       
        private void MP_InicioArmarCombo()
        {
            try
            {
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarCombo(Cb_Tipo,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_Placa,
                                    new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                                                                                                  Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_PLACA)).ToList());
                UTGlobal.MG_ArmarMultiComboCompraIngreso(cb_NumGranja,
                                   new ServiceDesktop.ServiceDesktopClient().TraerCompraIngresoCombo().ToList());
            
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
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
                MP_MostrarMensajeError(ex.Message);
            }
        }
        public static DataTable ListaATabla(List<VSeleccionLista> lista)
        {
            DataTable tabla = new DataTable();
            //Crear la Estructura de la Tabla a partir de la Lista de Objetos
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                tabla.Columns.Add(propiedades[i].Name, propiedades[i].PropertyType);
            }
            //Llenar la Tabla desde la Lista de Objetos
            DataRow fila = null;
            for (int i = 0; i < lista.Count; i++)
            {
                propiedades = lista[i].GetType().GetProperties();
                fila = tabla.NewRow();
                for (int j = 0; j < propiedades.Length; j++)
                {
                    fila[j] = propiedades[j].GetValue(lista[i], null);
                }
                tabla.Rows.Add(fila);
            }
            return (tabla);
        }
        private void MP_Habilitar()
        {
            tb_FechaSeleccion.IsInputReadOnly = false;
            cb_NumGranja.Focus();
            //Tb_IdCompraIngreso.ReadOnly = false;
            cb_NumGranja.ReadOnly = false;
            Dgv_Detalle.Enabled = true;
            Dgv_Seleccion.Enabled = true;
            Cb_Almacen.ReadOnly = false;
        }
        private void MP_InHabilitar()
        {
            Tb_FechaEnt.IsInputReadOnly = true;
            tb_FechaSeleccion.IsInputReadOnly = true;
            Tb_FechaRec.IsInputReadOnly = true;
            Tb_Id.ReadOnly = true;
            cb_NumGranja.ReadOnly = true;
            Tb_IdCompraIngreso.ReadOnly = true;
            cb_NumGranja.ReadOnly = true;
            Cb_Placa.ReadOnly = true;
            Tb_Edad.ReadOnly = true;
            Cb_Tipo.ReadOnly = true;
            tb_Proveedor.ReadOnly = true;
            _Limpiar = false;          
            Dgv_Detalle.Enabled = false;
            Dgv_Seleccion.Enabled = false;
            Cb_Almacen.ReadOnly = true;
        }
        private void MP_Limpiar()
        {
            try
            {
                Tb_Id.Clear();
                Tb_IdCompraIngreso.Clear();                
                Tb_Edad.Clear();
                Tb_FechaEnt.Value = DateTime.Now.Date;
                Tb_FechaRec.Value = DateTime.Now.Date;
                tb_FechaSeleccion.Value = DateTime.Now;
                Dt_FechaDesde.Value= DateTime.Now.Date;
                Dt_FechaHasta.Value = DateTime.Now.Date;
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo(Cb_Tipo);
                    UTGlobal.MG_SeleccionarCombo(Cb_Placa);
                    UTGlobal.MG_SeleccionarComboCompraIngreso(cb_NumGranja);
                }
                //((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).Clear();
                Dgv_Detalle.DataSource = null;
                Dgv_Seleccion.DataSource = null;
                Tb_Recep_TCantidad.Value = 0;
                Tb_Recep_TPrecio.Value = 0;
                Tb_Recep_Total.Value = 0;
                Tb_TCantidad.Value = 0;
                Tb_TPrecio.Value = 0;
                Tb_Total.Value = 0;
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
                if (_Pos < Dgv_GBuscador.RowCount - 1)
                {
                    Dgv_GBuscador.Row = _Pos;
                    _idOriginal = (int)Dgv_GBuscador.GetValue("id");
                    if (_idOriginal != 0)
                    {
                        var lista = new ServiceDesktop.ServiceDesktopClient().TraerSeleccion(_idOriginal);

                        Tb_Id.Text = lista.Id.ToString();
                        cb_NumGranja.Value = lista.Id;
                        Tb_IdCompraIngreso.Text = lista.IdCompraIng.ToString();
                        Tb_FechaEnt.Value = lista.FechaEntrega;
                        Tb_FechaRec.Value = lista.FechaRecepcion;
                        Cb_Placa.Value = (int)lista.Placa;
                        tb_Proveedor.Text = lista.Proveedor.ToString();
                        Cb_Tipo.Value = (int)lista.Tipo;
                        Tb_Edad.Text = lista.Edad.ToString();
                        tb_FechaSeleccion.Value = lista.FechaReg;
                        Cb_Almacen.Value = lista.IdAlmacen;
                        Tb_Merma.Value = (double)lista.Merma;

                        _TipoCompra = new ServiceDesktop.ServiceDesktopClient().TraerCompraIngreso(lista.IdCompraIng).TipoCompra;
                        btn_Seleccionar.Visible = _TipoCompra == 1 ? true : false;
                        MP_CargarDetalle(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                        MP_CargarDetalle2(Convert.ToInt32(Tb_Id.Text));
                        MP_ObtenerCalculo();
                        LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Convert.ToString(Dgv_GBuscador.RowCount - 1);
                    }
                }                   
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }            
        }
        private void MP_ObtenerCalculo()
        {
            try
            {
                Dgv_Detalle.UpdateData();
                Dgv_Seleccion.UpdateData();                    
                //Totales de recepcion
                Tb_Recep_TCantidad.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Cantidad"], AggregateFunction.Sum));
                Tb_Recep_Total.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
                Tb_Recep_TPrecio.Value = Tb_Recep_Total.Value / Tb_Recep_TCantidad.Value;

                //Totales de seleccion
                Tb_TCantidad.Value = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Cantidad"], AggregateFunction.Sum));
                Tb_Total.Value = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Total"], AggregateFunction.Sum));
                //Tb_Selecc_TPrecio.Value = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Precio"], AggregateFunction.Sum)) / Dgv_Seleccion.RowCount;
                //Merma   
                if (Tb_Total.Value == 0)
                {
                    Tb_TPrecio.Value = 0;
                }
                else
                {
                    Tb_TPrecio.Value = Tb_Total.Value / Tb_TCantidad.Value;
                }
                
                Tb_Merma.Value = Tb_Recep_TCantidad.Value - Tb_TCantidad.Value;
                tb_MermaPorc.Value = (Tb_Merma.Value * 100 )/ Tb_Recep_TCantidad.Value;
                if (tb_MermaPorc.Value > 5)
                {
                    tb_MermaPorc.BackColor = Color.Red;
                    ToastNotification.Show(this, "Porcentaje de merma excede el límite permitido de 5%".ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                    tb_MermaPorc.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }                    
        }
        private void MP_Filtrar(int tipo)
        {
            try
            {
                MP_CargarEncabezado();
                if (Dgv_GBuscador.RowCount > 0)
                {
                    //_MPos = 0;
                    MP_MostrarRegistro(tipo == 1 ? 0 : _MPos);
                }
                else
                {
                    MP_Limpiar();
                    LblPaginacion.Text = "0/0";
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }
        private void MP_ExportarExcel()
        {
            try
            {
                UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Reporte, ENSubCarpetas.ReportesSeleccion);
                string ubicacion = Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Reporte, ENSubCarpetas.ReportesSeleccion);
                if (MP_ArmarExcel(ubicacion, ENArchivoNombre.Seleccion))
                {
                    MP_MostrarMensajeExito(GLMensaje.ExportacionExitosa);
                }
                else
                {
                    MP_MostrarMensajeError(GLMensaje.ExportacionErronea);
                }
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
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
        private bool MP_ArmarExcel(string ubicacion, string nombreArchivo)
        {
            try
            {
                string archivo, linea;
                linea = "";
                archivo = nombreArchivo + DateTime.Now.Day + "." +
                        DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year + "." + DateTime.Now.Date.Hour + "." +
                        DateTime.Now.Date.Minute + "." + DateTime.Now.Date.Second + ".csv";
                archivo = Path.Combine(ubicacion, archivo);
                File.Delete(archivo);
                Stream stream = File.OpenWrite(archivo);
                StreamWriter escritor = new StreamWriter(stream, Encoding.UTF8);
                foreach (GridEXColumn columna in Dgv_GBuscador.RootTable.Columns)
                {
                    if (columna.Visible)
                    {
                        linea = linea + columna.Caption + ";";
                    }
                }
                linea = linea.Substring(0, linea.Length - 1);

                escritor.WriteLine(linea);
                linea = "";
                foreach (GridEXRow fila in Dgv_GBuscador.GetRows())
                {
                    foreach (GridEXColumn columna in Dgv_GBuscador.RootTable.Columns)
                    {
                        if (columna.Visible)
                        {
                            string data;
                            if (columna.Key.ToString() == "FechaReg" || columna.Key.ToString() == "FechaRecepcion")
                            {
                                data = Convert.ToDateTime(fila.Cells[columna.Key].Value).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                data = fila.Cells[columna.Key].Value.ToString();
                            }
                            data = data.Replace(";", ",");
                            linea = linea + data + ";";
                        }
                    }
                    linea = linea.Substring(0, linea.Length - 1);
                    escritor.WriteLine(linea);
                    linea = "";
                }
                escritor.Close();
                Efecto efecto = new Efecto();
                efecto.archivo = archivo;
                efecto.Tipo = 1;
                efecto.Context = "Su archivo ha sido Guardado en la ruta: " + archivo + "DESEA ABRIR EL ARCHIVO?";
                efecto.Header = "PREGUNTA";
                efecto.ShowDialog();
                if (efecto.Band)
                {
                    Process.Start(archivo);
                }
                return true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        private void MP_ReporteSeleccion(int idSeleccion)
        {

            try
            {
                if (idSeleccion == 0)
                {
                    throw new Exception("No existen registros");
                }
                if (UTGlobal.visualizador != null)
                {
                    UTGlobal.visualizador.Close();
                }
                UTGlobal.visualizador = new Visualizador();
                var lista = new ServiceDesktop.ServiceDesktopClient().NotaSeleccion(idSeleccion).ToList();
                if (lista != null)
                {
                    var ObjetoReport = new RSeleccionNota();
                    ObjetoReport.SetDataSource(lista);
                    UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
                    ObjetoReport.SetParameterValue("Titulo", "CLASIFICACIÓN INGRESO A ALMACEN");
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
        #endregion
        #region Eventos     

        private void Dgv_GBuscador_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
                {
                    MP_MostrarRegistro(Dgv_GBuscador.Row);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }

        private void Dgv_Detalle_EditingCell_1(object sender, EditingCellEventArgs e)
        {           
            e.Cancel = true;            
        }

        private void Dgv_Seleccion_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (e.Column.Index == Dgv_Seleccion.RootTable.Columns["Cantidad"].Index)
                {
                    e.Cancel = false;
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
        private void Dgv_Seleccion_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                int estado = Convert.ToInt32(Dgv_Seleccion.CurrentRow.Cells["Estado"].Value);
                if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                {
                    MP_CalcularFila();
                }
                else
                {
                    if (estado == (int)ENEstado.GUARDADO)
                    {
                        MP_CalcularFila();
                        Dgv_Seleccion.CurrentRow.Cells["Estado"].Value = (int)ENEstado.MODIFICAR;
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
                Double cantidad, precio, total;
                cantidad = Convert.ToDouble(Dgv_Seleccion.CurrentRow.Cells["Cantidad"].Value);
                precio = Convert.ToDouble(Dgv_Seleccion.CurrentRow.Cells["Precio"].Value);
                total = cantidad * precio;
                Dgv_Seleccion.CurrentRow.Cells["Total"].Value = total;
                MP_ObtenerCalculo();
                if (cantidad > 0)
                {
                    Dgv_Seleccion.UpdateData();
                    foreach (var fila in seleccion_01)
                    {
                        fila.Porcen = (fila.Cantidad * 100) / Convert.ToDecimal(Tb_TCantidad.Value);
                    }
                    ArmarDetalle(seleccion_01);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }

        private void Tb_IdCompraIngreso_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (cb_NumGranja.ReadOnly == false)
                //{
                //    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                //    {
                //        var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngresoBuscar((int)ENEstado.COMPLETADO); 
                //        List<GLCelda> listEstCeldas = new List<GLCelda>
                //    {
                //        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 60 },
                //        new GLCelda() { campo = "NumNota", visible = true, titulo = "N.GRANJA", tamano = 100 },
                //        new GLCelda() { campo = "FechaEnt", visible = true, titulo = "FECHA ENT.", tamano = 100 },
                //        new GLCelda() { campo = "FechaRec", visible = true, titulo = "FECHA REC.", tamano = 100 },
                //        new GLCelda() { campo = "Placa", visible = true, titulo = "PLACA", tamano = 130 },
                //        new GLCelda() { campo = "IdProvee", visible = false, titulo = "IdProvee", tamano = 100 },
                //        new GLCelda() { campo = "Proveedor", visible = true, titulo = "PROVEEDOR", tamano = 240 },
                //        new GLCelda() { campo = "Tipo", visible = false, titulo = "Tipo", tamano = 100 },
                //        new GLCelda() { campo = "EdadSemana", visible = false, titulo = "EDAD SEMANA", tamano = 100 },
                //        new GLCelda() { campo = "IdAlmacen", visible = false, titulo = "IdAlmacen", tamano = 100 },
                //        new GLCelda() { campo = "TipoCompra", visible = false, titulo = "TipoCompra", tamano = 100 }
                //    };
                //        Efecto efecto = new Efecto();
                //        efecto.Tipo = 3;
                //        efecto.Tabla = lista;
                //        efecto.SelectCol = 2;
                //        efecto.listaCelda = listEstCeldas;
                //        efecto.Alto = 50;
                //        efecto.Ancho = 350;
                //        efecto.Context = "SELECCIONE UN INGRESO";
                //        efecto.ShowDialog();
                //        bool bandera = false;
                //        bandera = efecto.Band;
                //        if (bandera)
                //        {
                //            Janus.Windows.GridEX.GridEXRow Row = efecto.Row;
                //            Tb_IdCompraIngreso.Text = Row.Cells["Id"].Value.ToString();
                //            //Tb_NUmGranja.Text = Row.Cells["NumNota"].Value.ToString();
                //            cb_NumGranja.Value = Row.Cells["Id"].Value.ToString();
                //            Tb_FechaEnt.Value = Convert.ToDateTime(Row.Cells["FechaEnt"].Value);
                //            Tb_FechaRec.Value = Convert.ToDateTime(Row.Cells["FechaRec"].Value);
                //            Cb_Tipo.Value = Row.Cells["Tipo"].Value;
                //            tb_Proveedor.Text = Row.Cells["Proveedor"].Value.ToString();
                //            Cb_Placa.Value = Row.Cells["Placa"].Value;
                //            Tb_Edad.Text = Row.Cells["EdadSemana"].Value.ToString();
                //            Cb_Almacen.Value = Row.Cells["IdAlmacen"].Value;
                //            _TipoCompra = Convert.ToInt32(Row.Cells["TipoCompra"].Value);
                //            MP_CargarDetalle(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                //            MP_CargarDetalle_Nuevo(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                //            MP_ObtenerCalculo();
                //            btn_Seleccionar.Visible = _TipoCompra == 1 ? true : false;
                //        }
                //    }
                //}
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
                    _MPos = Dgv_GBuscador.RowCount - 2;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void btn_Seleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                Dgv_Seleccion.UpdateData();
                Double totalSel, totalCant, precioProrateo, precioAvg;
                if (_TipoCompra == 1)
                {                    
                    var detRecion = ((List<VSeleccion_01_Lista>)Dgv_Detalle.DataSource);
                    Dgv_Seleccion.UpdateData();
                    seleccion_01 = ((List<VSeleccion_01_Lista>)Dgv_Seleccion.DataSource);
                    foreach (var filaRep in detRecion)
                    {
                        foreach (var filaSel in seleccion_01)
                        {
                            if (filaRep.IdProducto == filaSel.IdProducto)
                            {
                                filaSel.Total = filaSel.Cantidad * filaRep.Precio;
                                break;
                            }
                        }
                    }
                    ArmarDetalle(seleccion_01);
                    Dgv_Seleccion.UpdateData();
                    totalSel = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Total"], AggregateFunction.Sum));
                    totalCant = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Cantidad"], AggregateFunction.Sum));
                    precioProrateo = (totalSel -Tb_Recep_Total.Value) / totalCant;
                    precioProrateo = precioProrateo <= 0 ? -1 * precioProrateo : precioProrateo;
                    seleccion_01 = ((List<VSeleccion_01_Lista>)Dgv_Seleccion.DataSource);
                    foreach (var filaRep in detRecion)
                    {
                        foreach (var filaSel in seleccion_01)
                        {
                            if (filaRep.IdProducto == filaSel.IdProducto)
                            {
                                filaSel.Precio = (filaRep.Precio + Convert.ToDecimal(precioProrateo));
                                filaSel.Total = filaSel.Precio * filaSel.Cantidad;
                                break;
                            }
                        }
                    }
                    ArmarDetalle(seleccion_01);
                    MP_ObtenerCalculo();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }

        private void Dgv_GBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyData == Keys.Enter)
            //    {
            //        superTabControl1.SelectedTabIndex = 0;
            //        tb_FechaSeleccion.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MP_MostrarMensajeError(ex.Message);
            //}           
        }

        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (cb_NumGranja.ReadOnly == true)
            {
                MP_ReporteSeleccion(Convert.ToInt32(Tb_Id.Text));
            }

        }

        private void Dgv_GBuscador_DoubleClick(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row > -1)
            {
                superTabControl1.SelectedTabIndex = 0;
            }
        }

        private void cb_NumGranja_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (cb_NumGranja.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        if (Cb_Placa.SelectedIndex != -1)
                        {
                            var lista = new ServiceDesktop.ServiceDesktopClient().TraerCompraIngresoCombo().Where(a => a.Id.Equals(Convert.ToInt32(cb_NumGranja.Value))).FirstOrDefault();
                            if (lista == null)
                            {
                                throw new Exception("No se encontro una compra");
                            }
                            var tCompraIngreso = new ServiceDesktop.ServiceDesktopClient().CompraIngresoBuscar((int)ENEstado.COMPLETADO);
                            foreach (DataRow rCompra in tCompraIngreso.Rows)
                            {
                                if (rCompra.Field<int>("Id") == Convert.ToInt32(cb_NumGranja.Value))
                                {
                                    Tb_IdCompraIngreso.Text = rCompra.Field<int>("Id").ToString();
                                    //Tb_NUmGranja.Text = rCompra.Field<string>("NumNota").ToString();
                                    //rCompra.Field<DateTime>("TipoCompra")
                                    cb_NumGranja.Value = rCompra.Field<int>("Id");
                                    Tb_FechaEnt.Value = rCompra.Field<DateTime>("FechaEnt");
                                    Tb_FechaRec.Value = rCompra.Field<DateTime>("FechaRec");
                                    Cb_Tipo.Value = rCompra.Field<int>("Tipo");
                                    tb_Proveedor.Text = rCompra.Field<string>("Proveedor").ToString();
                                    Cb_Placa.Value = rCompra.Field<int>("Placa");
                                    Tb_Edad.Text = rCompra.Field<string>("EdadSemana").ToString();
                                    Cb_Almacen.Value = rCompra.Field<int>("IdAlmacen");
                                    _TipoCompra = rCompra.Field<int>("TipoCompra");
                                    MP_CargarDetalle(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                                    MP_CargarDetalle_Nuevo(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                                    MP_ObtenerCalculo();
                                    btn_Seleccionar.Visible = _TipoCompra == 1 ? true : false;
                                }
                            }
                        }
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                    {
                        var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngresoBuscar((int)ENEstado.COMPLETADO);
                        List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 60 },
                        new GLCelda() { campo = "NumNota", visible = true, titulo = "N.GRANJA", tamano = 100 },
                        new GLCelda() { campo = "FechaEnt", visible = true, titulo = "FECHA ENT.", tamano = 100 },
                        new GLCelda() { campo = "FechaRec", visible = true, titulo = "FECHA REC.", tamano = 100 },
                        new GLCelda() { campo = "Placa", visible = true, titulo = "PLACA", tamano = 130 },
                        new GLCelda() { campo = "IdProvee", visible = false, titulo = "IdProvee", tamano = 100 },
                        new GLCelda() { campo = "Proveedor", visible = true, titulo = "PROVEEDOR", tamano = 240 },
                        new GLCelda() { campo = "Tipo", visible = false, titulo = "Tipo", tamano = 100 },
                        new GLCelda() { campo = "EdadSemana", visible = false, titulo = "EDAD SEMANA", tamano = 100 },
                        new GLCelda() { campo = "IdAlmacen", visible = false, titulo = "IdAlmacen", tamano = 100 },
                        new GLCelda() { campo = "TipoCompra", visible = false, titulo = "TipoCompra", tamano = 100 }
                    };
                        Efecto efecto = new Efecto();
                        efecto.Tipo = 3;
                        efecto.Tabla = lista;
                        efecto.SelectCol = 2;
                        efecto.listaCelda = listEstCeldas;
                        efecto.Alto = 50;
                        efecto.Ancho = 350;
                        efecto.Context = "SELECCIONE UN INGRESO";
                        efecto.ShowDialog();
                        bool bandera = false;
                        bandera = efecto.Band;
                        if (bandera)
                        {
                            Janus.Windows.GridEX.GridEXRow Row = efecto.Row;
                            Tb_IdCompraIngreso.Text = Row.Cells["Id"].Value.ToString();
                            cb_NumGranja.Value = Row.Cells["Id"].Value.ToString();
                            Tb_FechaEnt.Value = Convert.ToDateTime(Row.Cells["FechaEnt"].Value);
                            Tb_FechaRec.Value = Convert.ToDateTime(Row.Cells["FechaRec"].Value);
                            Cb_Tipo.Value = Row.Cells["Tipo"].Value;
                            tb_Proveedor.Text = Row.Cells["Proveedor"].Value.ToString();
                            Cb_Placa.Value = Row.Cells["Placa"].Value;
                            Tb_Edad.Text = Row.Cells["EdadSemana"].Value.ToString();
                            Cb_Almacen.Value = Row.Cells["IdAlmacen"].Value;
                            _TipoCompra = Convert.ToInt32(Row.Cells["TipoCompra"].Value);
                            MP_CargarDetalle(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                            MP_CargarDetalle_Nuevo(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                            MP_ObtenerCalculo();
                            btn_Seleccionar.Visible = _TipoCompra == 1 ? true : false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().TraerSelecciones().ToList();
                if (ListaCompleta != null)
                {
                    var query = ListaCompleta.Where(a => a.FechaReg >= Dt_FechaDesde.Value && a.FechaReg <= Dt_FechaHasta.Value).ToList();
                    MP_ArmarEncabezado(query);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            MP_ExportarExcel();
        }
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                MP_CargarEncabezado();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnExportar2_Click(object sender, EventArgs e)
        {
            MP_ExportarExcel();
        }
       
        #endregion
        #region Metodo heredados
        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {               
                string mensaje = "";

                VSeleccion vSeleccion = new VSeleccion()
                {
                    IdAlmacen = Convert.ToInt32(Cb_Almacen.Value),
                    IdCompraIng = Convert.ToInt32(Tb_IdCompraIngreso.Text),
                    Estado = (int)ENEstado.GUARDADO,
                    FechaReg = tb_FechaSeleccion.Value,
                    Cantidad = Convert.ToDecimal(Tb_TCantidad.Value),
                    Precio = Convert.ToDecimal(Tb_TPrecio.Value),
                    Total = Convert.ToDecimal(Tb_Total.Value),
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                    Merma = Convert.ToDecimal(Tb_Merma.Value),
                    MermaPorcentaje = Convert.ToDecimal(tb_MermaPorc.Value)
                };
                int id = Tb_Id.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Id.Text);
                int idAux = id;
                var detalle_Seleccion = ((List<VSeleccion_01_Lista>)Dgv_Seleccion.DataSource).ToArray<VSeleccion_01_Lista>();

                var detalle_Ingreso = ((List<VSeleccion_01_Lista>)Dgv_Detalle.DataSource).ToArray<VSeleccion_01_Lista>();
                resultado = new ServiceDesktop.ServiceDesktopClient().Seleccion_Guardar(vSeleccion, detalle_Seleccion, detalle_Ingreso, ref id);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        MP_ReporteSeleccion(id);
                        cb_NumGranja.Focus();
                        MP_Filtrar(1);                        
                        MP_Limpiar();
                        _Limpiar = true;
                        MP_InicioArmarCombo();
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                    }
                    else//Modificar
                    {
                        MP_ReporteSeleccion(id);
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
                int IdSeleccion = Convert.ToInt32(Tb_Id.Text);               
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
                    resul = new ServiceDesktop.ServiceDesktopClient().Seleccion_ModificarEstado(IdSeleccion, (int)ENEstado.ELIMINAR, ref LMensaje);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, Tb_Id.Text));
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
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, Tb_Id.Text));
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
            //Tb_IdCompraIngreso.ReadOnly = true;
            cb_NumGranja.ReadOnly = true;

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
                //if (cb_NumGranja.SelectedIndex == -1)
                //{
                //    cb_NumGranja.BackColor = Color.Red;
                //    _Error = true;
                //}
                //else
                //    cb_NumGranja.BackColor = Color.White;
                if (tb_Proveedor.Text == "")
                {
                    tb_Proveedor.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    tb_Proveedor.BackColor = Color.White;
                if (Tb_IdCompraIngreso.Text == "")
                {
                    Tb_IdCompraIngreso.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Tb_IdCompraIngreso.BackColor = Color.White;
                if (Cb_Tipo.SelectedIndex == -1)
                {
                    Cb_Tipo.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Tipo.BackColor = Color.White;
                if (Tb_TCantidad.Value == 0)
                {
                    _Error = true;
                    throw new Exception("Detalle de seleccion en 0");                    
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
