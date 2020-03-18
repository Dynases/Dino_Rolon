using ENTITY.inv.Almacen.View;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTILITY
{
    public partial class Visualizador2 : Form
    {

        public static DateTime Inicio;
        public static DateTime Fin;
        public static int IdAlmacen;
        public static List<VDetalleKardex> detalleKardex;

        public Visualizador2(int reporte)
        {
            InitializeComponent();

            switch (reporte)
            {
                case 1:
                    this.MostrarReporteKardexGeneral();
                    break;
                default:
                    break;
            }

        }

        private void Visualizador2_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        #region Metodos

        public void MostrarReporteKardexGeneral()
        {
            ReportesDataSet.DetalleKardexDataTable detalleKardexRows = new ReportesDataSet.DetalleKardexDataTable();

            foreach (var i in detalleKardex)
            {
                detalleKardexRows.AddDetalleKardexRow(i.Id,
                                                      i.Descripcion,
                                                      i.Unidad,
                                                      i.SaldoAnterior,
                                                      i.Entradas,
                                                      i.Salidas,
                                                      i.Saldo);
            }

            this.reportViewer1.LocalReport.Refresh();

            this.reportViewer1.LocalReport.DataSources.Clear();            

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "UTILITY.Reportes.KardexGeneral.rdlc";            

            ReportDataSource r = new ReportDataSource("DetalleKardex", (DataTable)detalleKardexRows);
            this.reportViewer1.LocalReport.DataSources.Add(r);

            //ReportDataSource f = new ReportDataSource("moves", (DataTable)Session["Moves"]);
            //ReportViewer1.LocalReport.DataSources.Add(f);

            //ReportDataSource d = new ReportDataSource("article", (DataTable)Session["Article"]);
            //ReportViewer1.LocalReport.DataSources.Add(d);

            this.reportViewer1.LocalReport.Refresh();

        }

        #endregion

    }
}
