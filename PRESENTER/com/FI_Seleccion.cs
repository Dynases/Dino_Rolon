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
                LblTitulo.Text = _NombreFormulario;
                MP_InicioArmarCombo();
                MP_CargarAlmacenes();
                btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
                Tb_Merma.Value = 0;
               
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
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacen, almacenes);
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
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Seleccion_Lista().ToList();
                var lista = (from a in ListaCompleta                     
                            select new { a.Id,a.IdCompraIng, a.Granja, a.Proveedor, a.FechaEntrega, a.FechaRecepcion,a.Fecha,a.Hora,a.Usuario}).ToList();
                if (lista.Count() > 0)
                {
                    Dgv_GBuscador.DataSource = lista;
                    Dgv_GBuscador.RetrieveStructure();
                    Dgv_GBuscador.AlternatingColors = true;

                    Dgv_GBuscador.RootTable.Columns["id"].Caption = "Nota Seleccion";
                    Dgv_GBuscador.RootTable.Columns["id"].Width = 140;
                    Dgv_GBuscador.RootTable.Columns["id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["id"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["id"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["IdCompraIng"].Caption = "Nota Recepcion";
                    Dgv_GBuscador.RootTable.Columns["IdCompraIng"].Width = 140;
                    Dgv_GBuscador.RootTable.Columns["IdCompraIng"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["IdCompraIng"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["IdCompraIng"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["IdCompraIng"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["Granja"].Caption = "Nota Granja";
                    Dgv_GBuscador.RootTable.Columns["Granja"].Width = 140;
                    Dgv_GBuscador.RootTable.Columns["Granja"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Granja"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Granja"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Granja"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["Proveedor"].Caption = "Proveedor";
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].Width = 250;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].Visible = true;

                    
                    Dgv_GBuscador.RootTable.Columns["FechaEntrega"].Caption = "Fecha Entrega";
                    Dgv_GBuscador.RootTable.Columns["FechaEntrega"].Width = 200;
                    Dgv_GBuscador.RootTable.Columns["FechaEntrega"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["FechaEntrega"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["FechaEntrega"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["FechaEntrega"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Caption = "FechaRecepcion";
                    Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Width = 200;
                    Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["FechaRecepcion"].Visible = true;                  

                    Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;                    
                    Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;                    
                    Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

                    //Habilitar filtradores
                    Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                    Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_GBuscador.GroupByBoxVisible = false;
                    Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
                }
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
                    Dgv_Detalle.RootTable.Columns["Precio"].FormatString = "0.00";
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
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void MP_CargarDetalle2(int id)
        {
            try
            {
              
                seleccion_01 = new ServiceDesktop.ServiceDesktopClient().Seleccion_01_Lista().Where(a => a.IdSeleccion == id).ToList();
                ArmarDetalle(seleccion_01);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void ArmarDetalle(List<VSeleccion_01_Lista> lresult)
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
                Dgv_Seleccion.RootTable.Columns["Producto"].Width = 150;
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
                Dgv_Seleccion.RootTable.Columns["Precio"].FormatString = "0.00";
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
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }       
        private void MP_InicioArmarCombo()
        {
            //Carga las librerias al combobox desde una lista
            UTGlobal.MG_ArmarCombo(Cb_Tipo,
                                   new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                 Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2)).ToList());
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
            //Tb_IdCompraIngreso.ReadOnly = false;
            //Tb_NUmGranja.ReadOnly = false;
            //Tb_Placa.ReadOnly = false;
            //Cb_Tipo.ReadOnly = true;
            //tb_Proveedor.ReadOnly = false;           
            //Tb_Edad.ReadOnly = false;           
            //Tb_FechaEnt.Value = DateTime.Now;
            //Tb_FechaRec.Value = DateTime.Now;
            tb_FechaSeleccion.IsInputReadOnly = false;
            Tb_IdCompraIngreso.ReadOnly = false;
            Dgv_Detalle.Enabled = true;
            Dgv_Seleccion.Enabled = true;

        }
        private void MP_InHabilitar()
        {
            Tb_FechaEnt.IsInputReadOnly = true;
            tb_FechaSeleccion.IsInputReadOnly = true;
            Tb_FechaRec.IsInputReadOnly = true;
            Tb_Id.ReadOnly = true;
            Tb_IdCompraIngreso.ReadOnly = true;
            Tb_NUmGranja.ReadOnly = true;
            Tb_Placa.ReadOnly = true;
            Tb_Edad.ReadOnly = true;
            tb_Proveedor.ReadOnly = true;
            _Limpiar = false;          
            Dgv_Detalle.Enabled = false;
            Dgv_Seleccion.Enabled = false;

        }
        private void MP_Limpiar()
        {
            try
            {
                Tb_Id.Clear();
                Tb_IdCompraIngreso.Clear();
                Tb_NUmGranja.Clear();
                Tb_Placa.Clear();
                Tb_Edad.Clear();
                Tb_FechaEnt.Value = DateTime.Now;
                Tb_FechaRec.Value = DateTime.Now;
                tb_FechaSeleccion.Value = DateTime.Now;
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo(Cb_Tipo);                    
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

                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
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
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Seleccion_Lista();
                    var lista = (from a in ListaCompleta
                                 where a.Id.Equals(_idOriginal)
                                 select new { a.Id, a.IdCompraIng, a.Granja,a.FechaReg,  a.FechaEntrega, a.FechaRecepcion, a.Placa, a.Proveedor, a.Tipo, a.Edad }).FirstOrDefault();
                    Tb_Id.Text = lista.Id.ToString();
                    Tb_NUmGranja.Text = lista.Granja.ToString();
                    Tb_IdCompraIngreso.Text = lista.IdCompraIng.ToString();
                    Tb_FechaEnt.Value = lista.FechaEntrega;
                    Tb_FechaRec.Value = lista.FechaRecepcion;
                    Tb_Placa.Text = lista.Placa == "" ? "" : lista.Placa;
                    tb_Proveedor.Text = lista.Proveedor.ToString();
                    Cb_Tipo.Value = (int)lista.Tipo;
                    Tb_Edad.Text = lista.Edad.ToString();
                    tb_FechaSeleccion.Value = lista.FechaReg;
                    var LTipoCompra = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_NotaXId(lista.IdCompraIng).ToList();
                    _TipoCompra = (from a in LTipoCompra
                                   select new { a.TipoCompra }).First().TipoCompra;
                    btn_Seleccionar.Visible = _TipoCompra == 1 ? true : false;
                    MP_CargarDetalle(Convert.ToInt32(Tb_IdCompraIngreso.Text));
                    MP_CargarDetalle2(Convert.ToInt32(Tb_Id.Text));
                    MP_ObtenerCalculo();
                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
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
               // decimal Precio = 0;
                //if (Tb_IdCompraIngreso.Text != "")
                //{                    
                //   var lresult = new ServiceDesktop.ServiceDesktopClient().CmmpraIngreso_01ListarXId(Convert.ToInt32(Tb_IdCompraIngreso.Text)).ToList();
                //   Precio = lresult.Select(c => c.PrecioCost).Sum() / lresult.Where(c => c.PrecioCost > 0).Select(d => d.PrecioCost).Count();
                //}           
                //Totales de recepcion
                Tb_Recep_TCantidad.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Cantidad"], AggregateFunction.Sum));
                Tb_Recep_Total.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
                Tb_Recep_TPrecio.Value = Tb_Recep_Total.Value / Tb_Recep_TCantidad.Value;

                //Totales de seleccion
                Tb_TCantidad.Value = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Cantidad"], AggregateFunction.Sum));
                Tb_Total.Value = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Total"], AggregateFunction.Sum));
                //Tb_Selecc_TPrecio.Value = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Precio"], AggregateFunction.Sum)) / Dgv_Seleccion.RowCount;
                //Merma              
                Tb_TPrecio.Value = Tb_Total.Value / Tb_TCantidad.Value;
                Tb_Merma.Value = Tb_Recep_TCantidad.Value - Tb_TCantidad.Value;
                tb_MermaPorc.Value = (Tb_Merma.Value * 100 )/ Tb_Recep_TCantidad.Value;
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

        #endregion
        #region Eventos     

        private void Dgv_GBuscador_SelectionChanged_1(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }

        private void Dgv_Detalle_EditingCell_1(object sender, EditingCellEventArgs e)
        {           
            e.Cancel = true;            
        }

        private void Dgv_Seleccion_EditingCell(object sender, EditingCellEventArgs e)
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
        private void Dgv_Seleccion_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                Dgv_Seleccion.UpdateData();
                int estado = Convert.ToInt32(Dgv_Seleccion.CurrentRow.Cells["Estado"].Value);
                //if (estado == (int)ENEstado.COMPLETADO)
                //{
                //    throw new Exception("PRODUCTO COMPLETADO NO SE PUEDE  MODIFICAR");
                //}
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

        private void Tb_IdCompraIngreso_KeyDown(object sender, KeyEventArgs e)
        {

            if (Tb_IdCompraIngreso.ReadOnly == false)
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                {
                    var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ListarEncabezado();
                    List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 80 },
                        new GLCelda() { campo = "NumNota", visible = true, titulo = "NOTA DE GRANJA", tamano = 80 },
                        new GLCelda() { campo = "FechaEnt", visible = true, titulo = "FECHA ENTRADA", tamano = 80 },
                        new GLCelda() { campo = "FechaRec", visible = true, titulo = "FECHA RECEPCION", tamano = 80 },
                        new GLCelda() { campo = "Placa", visible = true, titulo = "PLACA", tamano = 120 },
                        new GLCelda() { campo = "IdProvee", visible = false, titulo = "IdProvee", tamano = 100 },
                        new GLCelda() { campo = "Proveedor", visible = true, titulo = "PROVEEDOR", tamano = 150 },
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
                        Tb_NUmGranja.Text = Row.Cells["NumNota"].Value.ToString();
                        Tb_FechaEnt.Value = Convert.ToDateTime(Row.Cells["FechaEnt"].Value);
                        Tb_FechaRec.Value = Convert.ToDateTime(Row.Cells["FechaRec"].Value);
                        Cb_Tipo.Value = Row.Cells["Tipo"].Value;
                        tb_Proveedor.Text = Row.Cells["Proveedor"].Value.ToString();
                        Tb_Placa.Text = Row.Cells["Placa"].Value.ToString();
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
        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = 0;
                Dgv_GBuscador.Row = _MPos;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_GBuscador.Row;
            if (_MPos > 0 && Dgv_GBuscador.RowCount > 0)
            {
                _MPos = _MPos - 1;
                Dgv_GBuscador.Row = _MPos;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_GBuscador.Row;
            if (_MPos < Dgv_GBuscador.RowCount - 1 && _MPos >= 0)
            {
                _MPos = Dgv_GBuscador.Row + 1;
                Dgv_GBuscador.Row = _MPos;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_GBuscador.Row;
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = Dgv_GBuscador.RowCount - 1;
                Dgv_GBuscador.Row = _MPos;
            }
        }
        private void btn_Seleccionar_Click(object sender, EventArgs e)
        {
            Dgv_Seleccion.UpdateData();
            Double totalSel, totalCant, precioProrateo, precioAvg;
            if (_TipoCompra == 1)
            {
                //merma = Tb_Recep_TCantidad.Value - Tb_TCantidad.Value;
                //mermaPorc = merma / Tb_Recep_TCantidad.Value;
                //Tb_Merma.Value = merma;
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
                        }
                    }
                }
                ArmarDetalle(seleccion_01);
                Dgv_Seleccion.UpdateData();
                totalSel = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Total"], AggregateFunction.Sum));
                totalCant = Convert.ToDouble(Dgv_Seleccion.GetTotal(Dgv_Seleccion.RootTable.Columns["Cantidad"], AggregateFunction.Sum));
                precioProrateo = (Tb_Recep_Total.Value - totalSel) / totalCant;

                foreach (var filaRep in detRecion)
                {
                    foreach (var filaSel in seleccion_01)
                    {
                        if (filaRep.IdProducto == filaSel.IdProducto)
                        {
                            filaSel.Precio = (filaRep.Precio + Convert.ToDecimal(precioProrateo));
                            filaSel.Total = filaSel.Precio * filaRep.Cantidad;
                            
                        }
                    }
                }             
                ArmarDetalle(seleccion_01);
                MP_ObtenerCalculo();
             }
        }

        private void Dgv_GBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                superTabControl1.SelectedTabIndex = 0;
                tb_FechaSeleccion.Focus();
            }
        }

        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion
        #region Metodo heredados
        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
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
                Merma = Convert.ToDecimal(Tb_Merma.Value)
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
                    Tb_NUmGranja.Focus();
                    MP_CargarEncabezado();
                    MP_Limpiar();
                    _Limpiar = true;
                    mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                }
                else//Modificar
                {
                    MP_Filtrar(1);
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
        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();

        }
        public override void MH_Modificar()
        {
            MP_Habilitar();
            Tb_IdCompraIngreso.ReadOnly = true;

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
                if (Tb_NUmGranja.Text == "")
                {
                    Tb_NUmGranja.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Tb_NUmGranja.BackColor = Color.White;

                if (tb_Proveedor.Text == "")
                {
                    tb_Proveedor.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    tb_Proveedor.BackColor = Color.White;
                if (Cb_Tipo.SelectedIndex == -1)
                {
                    Cb_Tipo.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Tipo.BackColor = Color.White;
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }          
        }


        #endregion

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
        }

      
    }
}
