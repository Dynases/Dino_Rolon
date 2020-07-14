using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
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
using UTILITY;
using UTILITY.Enum.EnCarpetas;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_ReporteKardex : MODEL.ModeloF1
    {
        public F1_ReporteKardex()
        {
            InitializeComponent();
            this.MP_CargarAlmacenes();
        }

        #region Variables de Entorno


        #endregion

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacenes, almacenes);

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion

        #region Eventos

        private void F1_ReporteKardex_Load(object sender, EventArgs e)
        {
            this.PanelContenidoBuscar.Visible = false;
            this.PanelMenu.Visible = false;
            this.SuperTabBuscar.Visible = false;
            this.TxtNombreUsu.Visible = false;
            this.SuperTalRegistro.Text = "REPORTES";
            //this.PanelInferior.Visible = false;
            this.btnMax.Visible = false;
        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void BtKardexGeneral_Click(object sender, EventArgs e)
        {
            //Visualizador2.Fin = Dt_FechaInicio.Value;
            //Visualizador2.Inicio = Dt_FechaFin.Value;
            //Visualizador2.detalleKardex = new ServiceDesktop.ServiceDesktopClient().ListarDetalleKardex(Dt_FechaInicio.Value, Dt_FechaFin.Value, Convert.ToInt32(Cb_Almacenes.Value), 0).ToList();
            //Visualizador2 frm = new Visualizador2(1);
            //frm.ShowDialog();
            var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ReporteHistoricoSeleccion(Dt_FechaInicio.Value, Dt_FechaFin.Value);
            Dgv_GBuscador.DataSource = ListaCompleta;
            Dgv_GBuscador.RetrieveStructure();
            Dgv_GBuscador.AlternatingColors = true;

            UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Reporte, ENSubCarpetas.ReportesSeleccion);
            string ubicacion = Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Reporte, ENSubCarpetas.ReportesSeleccion);
            if (MP_ExportarExcel(ubicacion, ENArchivoNombre.SeleccionHistorio))
            {
                MP_MostrarMensajeExito(GLMensaje.ExportacionExitosa);
            }
            else
            {
                MP_MostrarMensajeError(GLMensaje.ExportacionErronea);
            }
        }
        private bool MP_ExportarExcel(string ubicacion, string nombreArchivo)
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
