using DevComponents.DotNetBar;
using ENTITY.Plantilla;
using ENTITY.ven.view;
using Janus.Windows.GridEX;
using PRESENTER.frm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.ven
{
    public partial class F1_Ventas : MODEL.ModeloF1
    {
        public F1_Ventas()
        {
            InitializeComponent();
            this.MP_InHabilitar();
            this.MP_CargarAlmacenes();
        }

        #region Variables de Entorno

        //public static List<Producto> detalleProductos;
        private static List<VVenta> listaVentas;
        private static List<VPlantilla> listaPlantillas;
        private static int index;
        private static int plantillaIndex;

        #endregion

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_InHabilitar()
        {
            this.lblId.Visible = false;
            this.lblIdCliente.Visible = false;
            this.Dt_FechaVenta.Enabled = false;
            this.Tb_Cod.Enabled = false;
            this.Cb_Origen.Enabled = false;
            this.Tb_Usuario.ReadOnly = true;
            this.TbCliente.ReadOnly = true;
            this.sw_estado.Enabled = false;
            this.Sw_TipoVenta.Enabled = false;
            this.TbEncVenta.ReadOnly = true;
            this.TbEncEntrega.ReadOnly = true;
            this.TbEncTransporte.ReadOnly = true;
            this.TbEncRecepcion.ReadOnly = true;
            this.Tb_Observaciones.ReadOnly = true;
            this.TbEncPrVenta.ReadOnly = true;
        }

        private void MP_Habilitar()
        {
            this.lblFechaRegistro.Visible = false;
            this.Dt_FechaVenta.Enabled = true;
            this.Cb_Origen.Enabled = true;
            this.TbCliente.ReadOnly = false;
            this.sw_estado.Enabled = true;
            this.Sw_TipoVenta.Enabled = true;
            this.TbEncVenta.ReadOnly = false;
            this.TbEncEntrega.ReadOnly = false;
            this.TbEncTransporte.ReadOnly = false;
            this.TbEncRecepcion.ReadOnly = false;
            this.Tb_Observaciones.ReadOnly = false;
            this.TbEncPrVenta.ReadOnly = false;
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Origen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }



        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_InHabilitar();
        }

        #endregion

        #region Eventos

        private void F1_Ventas_Load(object sender, EventArgs e)
        {

        }

        private void TbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (TbCliente.ReadOnly.Equals(false))
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                {
                    var lista = new ServiceDesktop.ServiceDesktopClient().ClienteListarEncabezado();
                    List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "COD", tamano = 50 },
                        new GLCelda() { campo = "IdSpyre", visible = true, titulo = "CÓDIGO SPYRE", tamano = 80 },
                        new GLCelda() { campo = "Descrip", visible = true, titulo = "Nombre y Apellido", tamano = 150 },
                        new GLCelda() { campo = "RazonSo", visible = false, titulo = "Razon Social", tamano = 100 },
                        new GLCelda() { campo = "Nit", visible = true, titulo = "NIT", tamano = 100 },
                        new GLCelda() { campo = "Direcc", visible = true, titulo = "Direccion", tamano = 150 },
                        new GLCelda() { campo = "b.Descrip", visible = true, titulo = "Ciudad", tamano = 120 },
                        new GLCelda() { campo = "Factur", visible = true, titulo = "Facturacion", tamano = 100 }
                    };

                    Efecto efecto = new Efecto();
                    efecto.Tipo = 3;
                    efecto.Tabla = lista;
                    efecto.SelectCol = 2;
                    efecto.listaCelda = listEstCeldas;
                    efecto.Alto = 50;
                    efecto.Ancho = 350;
                    efecto.Context = "SELECCIONE UN CLIENTE";
                    efecto.ShowDialog();

                    var bandera = false;
                    bandera = efecto.Band;
                    if (bandera)
                    {
                        GridEXRow Row = efecto.Row;
                        lblIdCliente.Text = Row.Cells[0].Value.ToString();
                        TbCliente.Text = Row.Cells[2].Value.ToString();
                    }
                }
            }
        }



        #endregion       
    }
}
