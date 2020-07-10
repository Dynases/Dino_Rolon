using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
using MODEL;
using PRESENTER.frm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Enum.EnCarpetas;
using UTILITY.Global;

namespace PRESENTER.com.Reporte
{
    public partial class F2_ExportarExcel : ModeloF2
    {
        public F2_ExportarExcel()
        {
            InitializeComponent();
            BtnGenerar.Visible = false;
            BtnExportar.Visible = true;
            Cb_Estado.SelectedIndex = 0;
        }
        private void GPanel_Criterio_Click(object sender, EventArgs e)
        {

        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fechaDesde = null;
                DateTime? fechaHasta = null;
                fechaDesde = Dt_FechaDesde.Checked ? Dt_FechaDesde.Value.Date : fechaDesde;
                fechaHasta = Dt_FechaHasta.Checked ? Dt_FechaHasta.Value.Date : fechaHasta;

                Dgv_GBuscador.DataSource = null;
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ReporteHistoricoSeleccion(fechaDesde, fechaHasta);
                Dgv_GBuscador.DataSource = ListaCompleta;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;
                var resultado = false;
                switch (Cb_Estado.SelectedIndex)
                {
                    case 0:
                        UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Reporte, ENSubCarpetas.ReportesSeleccion);
                        string ubicacion = Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Reporte, ENSubCarpetas.ReportesSeleccion);
                        resultado = MP_HistoricoSeleccion(ubicacion, ENArchivoNombre.SeleccionHistorio);
                        break;
                }              
                if (resultado)
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
        #region Metodos
        private bool MP_HistoricoSeleccion(string ubicacion, string nombreArchivo)
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
                            var data = fila.Cells[columna.Key].Value.ToString();
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
        #endregion

    }
}
