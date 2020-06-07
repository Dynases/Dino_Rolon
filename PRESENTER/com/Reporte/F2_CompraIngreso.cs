using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
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
using UTILITY.Global;

namespace PRESENTER.com.Reporte
{
    public partial class F2_CompraIngreso : ModeloF2
    {
        public F2_CompraIngreso()
        {
            InitializeComponent();
            Rpt_Reporte.LocalReport.EnableExternalImages = true;
            Rpt_Reporte.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            Rpt_Reporte.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            Rpt_Reporte.ZoomPercent = 100;
        }
        #region Eventos
        private void F2_CompraIngreso_Load(object sender, EventArgs e)
        {         
            //this.Rpt_Reporte.RefreshReport();
            MP_Habilitar();

        }
        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                int estado = 0;
                DateTime? fechaDesde = null;
                DateTime? fechaHasta = null;
                var compraIngreso = new ServiceDesktop.ServiceDesktopClient()
                                    .CompraIngresoReporte(fechaDesde = Dt_FechaDesde.Checked ? Dt_FechaDesde.Value.Date : fechaDesde,
                                                          fechaHasta = Dt_FechaHasta.Checked ? Dt_FechaHasta.Value.Date : fechaHasta,
                                                          estado = Cb_Estado.SelectedIndex == 2? (int)ENEstado.TODOS :
                                                                  (Cb_Estado.SelectedIndex == 0? (int)ENEstado.GUARDADO : (int)ENEstado.COMPLETADO));
                if (compraIngreso.Rows.Count != 0)
                {
                    Rpt_Reporte.LocalReport.DataSources.Clear();
                    Rpt_Reporte.ProcessingMode = ProcessingMode.Local;
                    Rpt_Reporte.LocalReport.ReportEmbeddedResource = "PRESENTER.Report.ReportViewer.CompraIngreso.rdlc";
                    
                    List<ReportParameter> lParametros = new List<ReportParameter>{};                    
                    lParametros.Add(new ReportParameter("fechaDesde", fechaDesde.HasValue ? fechaDesde.Value.ToShortDateString() : "-----"));
                    lParametros.Add(new ReportParameter("fechaHasta", fechaHasta.HasValue ? fechaHasta.Value.ToShortDateString() : "-----"));
                    lParametros.Add(new ReportParameter("estado", Cb_Estado.Text));
                    Rpt_Reporte.LocalReport.DataSources.Add(new ReportDataSource("CompraIngreso", compraIngreso));
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
        #endregion

        #region Metodos Privados
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_Habilitar()
        {
            Cb_Estado.SelectedIndex = 0;
            Dt_FechaDesde.Checked = false;
            Dt_FechaHasta.Checked = true;
            Rpt_Reporte.Visible = false;
        }
        #endregion


    }

}
