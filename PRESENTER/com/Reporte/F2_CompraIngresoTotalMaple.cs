using DevComponents.DotNetBar;
using ENTITY.com.CompraIngreso.Filter;
using Microsoft.Reporting.WinForms;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;

namespace PRESENTER.com.Reporte
{
    public partial class F2_CompraIngresoTotalMaple : ModeloF2
    {
        public F2_CompraIngresoTotalMaple()
        {
            InitializeComponent();
            Rpt_Reporte.LocalReport.EnableExternalImages = true;
            Rpt_Reporte.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            Rpt_Reporte.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            Rpt_Reporte.ZoomPercent = 100;
        }
        #region Metodos Privados
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_CargarAlmacenes()
        {
            try
            {
              var  _almacens = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                UTGlobal.MG_ArmarComboAlmacen(cbAlmacen, _almacens);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_Habilitar()
        {
            Cb_Estado.SelectedIndex = 0;
            Dt_FechaDesde.Checked = false;
            Dt_FechaHasta.Checked = true;
            Rpt_Reporte.Visible = false;
            cb_NumGranja.Value = 0;
            cb_Proveedor.Value = 0;
            Cb_Tipo.Value = 0;
            cbAlmacen.Value = 0;
        }
        private void MP_InicioArmarCombo()
        {
            try
            {
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarMultiComboCompraIngreso2(cb_NumGranja,
                                                 new ServiceDesktop.ServiceDesktopClient().TraerCompraIngresoCombo(UTGlobal.UsuarioId).ToList());
                UTGlobal.MG_ArmarComboProveedores(cb_Proveedor,
                                                new ServiceDesktop.ServiceDesktopClient().TraerProveedoresEdadSemana().ToList(), ENEstado.CARGARPRIMERFILA);
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarComboConPrimerFila(Cb_Tipo,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2)).ToList());
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion

        private void F2_CompraIngresoTotalMaple_Load(object sender, EventArgs e)
        {
            MP_InicioArmarCombo();
            MP_Habilitar();
            MP_CargarAlmacenes();
       
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                int estado = Cb_Estado.SelectedIndex == 2 ? (int)ENEstado.TODOS :
                                                                  (Cb_Estado.SelectedIndex == 0 ? (int)ENEstado.GUARDADO : (int)ENEstado.COMPLETADO);
                DateTime? fechaDesde = null;
                DateTime? fechaHasta = null;
                FCompraIngreso fcompraingreso = new FCompraIngreso()
                {
                    Id = Convert.ToInt32(cb_NumGranja.Value),
                    IdProveedor = Convert.ToInt32(cb_Proveedor.Value),
                    TipoCategoria = Convert.ToInt32(Cb_Tipo.Value),
                    fechaDesde = Dt_FechaDesde.Checked ? Dt_FechaDesde.Value.Date : fechaDesde,
                    fechaHasta = Dt_FechaHasta.Checked ? Dt_FechaHasta.Value.Date : fechaHasta,
                    IdAlmacen = Convert.ToInt32(cbAlmacen.Value),
                    estadoCompra = estado
                };
                DataTable compraIngreso = new DataTable();
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    compraIngreso = servicio.ReporteTotalMaple(fcompraingreso);
                }            
                if (compraIngreso.Rows.Count != 0)
                {
                    Rpt_Reporte.LocalReport.DataSources.Clear();
                    Rpt_Reporte.ProcessingMode = ProcessingMode.Local;
                    Rpt_Reporte.LocalReport.ReportEmbeddedResource = "PRESENTER.Report.ReportViewer.CompraIngresoTotalMaple.rdlc";

                    List<ReportParameter> lParametros = new List<ReportParameter> { };
                    lParametros.Add(new ReportParameter("fechaDesde", Dt_FechaDesde.Checked ? fcompraingreso.fechaDesde.Value.ToShortDateString() : "-----"));
                    lParametros.Add(new ReportParameter("fechaHasta", Dt_FechaHasta.Checked ? fcompraingreso.fechaHasta.Value.ToShortDateString() : "-----"));
                    lParametros.Add(new ReportParameter("estado", Cb_Estado.Text));
                    lParametros.Add(new ReportParameter("NumGranja", cb_NumGranja.Text));
                    lParametros.Add(new ReportParameter("Tipo", Cb_Tipo.Text));
                    lParametros.Add(new ReportParameter("Proveedor", cb_Proveedor.Text));
                    lParametros.Add(new ReportParameter("Almacen", cbAlmacen.Text));
                    Rpt_Reporte.LocalReport.DataSources.Add(new ReportDataSource("CompraIngresoTotalMaple", compraIngreso));
                    Rpt_Reporte.LocalReport.SetParameters(lParametros);
                    Rpt_Reporte.RefreshReport();
                    Rpt_Reporte.Visible = true;
                    LblPaginacion.Text = compraIngreso.Rows.Count.ToString();
                }
                else
                    throw new Exception("No se encontraron registros con el filtro especificado.");
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void BtnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
