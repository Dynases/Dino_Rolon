using DevComponents.DotNetBar;
using ENTITY.inv.Traspaso.View;
using ENTITY.Plantilla;
using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Enum;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Traspaso : MODEL.ModeloF1
    {
        public F1_Traspaso()
        {
            InitializeComponent();
            this.MP_InHabilitar();
            this.MP_CargarAlmacenes();
            this.MP_CargarListaTraspasos();
        }

        //===============
        #region Variables de entorno

        public static List<Producto> detalleProductos;
        private static List<VTraspaso> listaTraspasos;
        private static List<VPlantilla> listaPlantillas;
        private static int index;
        private static int plantillaIndex;

        #endregion

        //===============
        #region Metodos Privados      

        private void MP_InHabilitar()
        {
            this.Tb_UsuarioEnvio.ReadOnly = true;
            this.Tb_UsuarioRecibe.ReadOnly = true;
            this.Tb_Observaciones.ReadOnly = true;

            this.Cb_Destino.Enabled = false;
            this.Cb_Origen.Enabled = false;

            this.Dgv_DetalleTraspaso.Visible = true;
            this.panelDerecha.Visible = false;
            this.panelIzquierda.Visible = false;

            this.GPanel_Detalles.Text = "DETALLE DE TRASPASO";

            this.lblId.Visible = false;
            this.panelNavegacionPlantilla.Visible = false;
        }

        private void MP_Habilitar()
        {
            //this.Tb_UsuarioEnvio.ReadOnly = false;
            //this.Tb_UsuarioRecibe.ReadOnly = false;

            this.Tb_UsuarioEnvio.Text = UTGlobal.Usuario;
            this.lblFechaEnvio.Text = DateTime.Now.ToString();

            this.Tb_Observaciones.ReadOnly = false;

            this.Cb_Destino.Enabled = true;
            this.Cb_Origen.Enabled = true;

            this.Dgv_DetalleTraspaso.Visible = false;
            this.panelDerecha.Visible = true;
            this.panelIzquierda.Visible = true;

            this.GPanel_Detalles.Text = "SELECCIONE LOS PRODUCTOS PARA EL TRASPASO";

            this.lblId.Visible = false;
            this.panelNavegacionPlantilla.Visible = true;
        }

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Destino, almacenes);
                UTGlobal.MG_ArmarComboAlmacen(Cb_Origen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Reiniciar()
        {
            Dgv_ProductosInventario.DataSource = null;
            Tb_Observaciones.Text = "";
            Tb_UsuarioEnvio.Text = "";
            Tb_UsuarioRecibe.Text = "";

            this.MP_InHabilitar();
        }

        private void MP_CargarListaTraspasos()
        {
            index = 0;
            try
            {
                listaTraspasos = new ServiceDesktop.ServiceDesktopClient().TraspasosListar().ToList();
                if (listaTraspasos != null && listaTraspasos.Count > 0)
                {
                    this.MP_MostrarRegistro(index);
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_MostrarRegistro(int index)
        {
            var traspaso = listaTraspasos[index];
            lblId.Text = traspaso.Id.ToString();
            Cb_Destino.Value = traspaso.Destino;
            Cb_Origen.Value = traspaso.Origen;
            Tb_UsuarioEnvio.Text = traspaso.Usuario;
            lblFechaEnvio.Text = traspaso.Fecha.ToShortDateString();
            lblFechaRecepcion.Text = traspaso.Fecha.ToShortDateString();

            this.MP_CargarDetalleRegistro(traspaso.Id);

            this.LblPaginacion.Text = (index + 1) + "/" + listaTraspasos.Count;
        }

        private void MP_CargarDetalleRegistro(int id)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().TraspasoDetalleListar(id).ToList();

                if (result.Count > 0)
                {
                    Dgv_DetalleTraspaso.DataSource = result;
                    Dgv_DetalleTraspaso.RetrieveStructure();
                    Dgv_DetalleTraspaso.AlternatingColors = true;

                    Dgv_DetalleTraspaso.RootTable.Columns[0].Key = "Id";
                    Dgv_DetalleTraspaso.RootTable.Columns[0].Caption = "Id";
                    Dgv_DetalleTraspaso.RootTable.Columns[0].Visible = false;

                    Dgv_DetalleTraspaso.RootTable.Columns[1].Key = "TraspasoId";
                    Dgv_DetalleTraspaso.RootTable.Columns[1].Caption = "TraspasoId";
                    Dgv_DetalleTraspaso.RootTable.Columns[1].Width = 250;
                    Dgv_DetalleTraspaso.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[1].Visible = true;

                    Dgv_DetalleTraspaso.RootTable.Columns[2].Key = "ProductoId";
                    Dgv_DetalleTraspaso.RootTable.Columns[2].Caption = "ProductoId";
                    Dgv_DetalleTraspaso.RootTable.Columns[2].Width = 250;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].Visible = true;

                    Dgv_DetalleTraspaso.RootTable.Columns[3].Key = "Cantidad";
                    Dgv_DetalleTraspaso.RootTable.Columns[3].Caption = "Cantidad";
                    Dgv_DetalleTraspaso.RootTable.Columns[3].Width = 150;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].Visible = true;

                    Dgv_DetalleTraspaso.RootTable.Columns[4].Key = "Lote";
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Caption = "Lote";
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Width = 250;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Visible = true;

                    Dgv_DetalleTraspaso.RootTable.Columns[4].Key = "Fecha";
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Caption = "Fecha";
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Width = 250;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Visible = true;

                    //Habilitar filtradores
                    Dgv_DetalleTraspaso.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_DetalleTraspaso.FilterMode = FilterMode.Automatic;
                    Dgv_DetalleTraspaso.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_DetalleTraspaso.GroupByBoxVisible = false;
                    Dgv_DetalleTraspaso.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_GuardarDetalleTraspaso(VTraspaso traspaso)
        {
            var listaDetalle = new List<VTraspaso_01>();
            var mensaje = "";
            foreach (var i in Dgv_DetalleNuevo.GetRows())
            {
                var detalle = new VTraspaso_01
                {
                    Cantidad = Convert.ToInt32(i.Cells[3].Value),
                    Fecha = DateTime.Now,
                    Lote = "",
                    ProductoId = Convert.ToInt32(i.Cells[0].Value),
                    TraspasoId = traspaso.Id
                };

                listaDetalle.Add(detalle);
            }

            if (!new ServiceDesktop.ServiceDesktopClient().TraspasoDetalleGuardar(listaDetalle.ToArray(), traspaso.Id))
            {
                mensaje = GLMensaje.Registro_Error("TRASPASOS");
                this.MP_MostrarMensajeError(mensaje);
            }
        }

        private void MP_CargarProductosPorAlmacenOrigen(int AlmacenOrigenId)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ListarInventarioXAlmacenId(AlmacenOrigenId).ToList();
                Dgv_ProductosInventario.DataSource = result;
                if (result.Count > 0)
                {
                    Dgv_ProductosInventario.RetrieveStructure();
                    Dgv_ProductosInventario.AlternatingColors = true;

                    Dgv_ProductosInventario.RootTable.Columns[0].Key = "InventarioId";
                    Dgv_ProductosInventario.RootTable.Columns[0].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[1].Key = "AlmacenId";
                    Dgv_ProductosInventario.RootTable.Columns[1].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[2].Key = "Descripcion";
                    Dgv_ProductosInventario.RootTable.Columns[2].Caption = "Descripcion";
                    Dgv_ProductosInventario.RootTable.Columns[2].Width = 150;
                    Dgv_ProductosInventario.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[2].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[3].Key = "Saldo";
                    Dgv_ProductosInventario.RootTable.Columns[3].Caption = "Saldo";
                    Dgv_ProductosInventario.RootTable.Columns[3].Width = 80;
                    Dgv_ProductosInventario.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[3].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[4].Key = "División";
                    Dgv_ProductosInventario.RootTable.Columns[4].Caption = "División";
                    Dgv_ProductosInventario.RootTable.Columns[4].Width = 110;
                    Dgv_ProductosInventario.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[4].Visible = true;


                    Dgv_ProductosInventario.RootTable.Columns[5].Key = "Marca";
                    Dgv_ProductosInventario.RootTable.Columns[5].Caption = "Marca";
                    Dgv_ProductosInventario.RootTable.Columns[5].Width = 110;
                    Dgv_ProductosInventario.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[5].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[6].Key = "Categorías";
                    Dgv_ProductosInventario.RootTable.Columns[6].Caption = "Categoría";
                    Dgv_ProductosInventario.RootTable.Columns[6].Width = 120;
                    Dgv_ProductosInventario.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[6].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[7].Key = "Unidad";
                    Dgv_ProductosInventario.RootTable.Columns[7].Caption = "Unidad";
                    Dgv_ProductosInventario.RootTable.Columns[7].Width = 80;
                    Dgv_ProductosInventario.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[7].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[7].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[8].Key = "UnidadVentaDisplay";
                    Dgv_ProductosInventario.RootTable.Columns[8].Visible = false;

                    //Habilitar filtradores
                    Dgv_ProductosInventario.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_ProductosInventario.FilterMode = FilterMode.Automatic;
                    Dgv_ProductosInventario.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_ProductosInventario.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_ProductosInventario.GroupByBoxVisible = false;
                    Dgv_ProductosInventario.VisualStyle = VisualStyle.Office2007;
                }

                detalleProductos = new List<Producto>();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool MP_ExisteItemEnLista(string valorId)
        {
            var response = false;

            foreach (var i in Dgv_DetalleNuevo.GetRows())
            {
                if (i.Cells[0].Value.Equals(valorId))
                {
                    response = true;
                }
            }

            return response;
        }

        private void MP_CargarPlantillas()
        {
            try
            {
                plantillaIndex = 0;
                listaPlantillas = new ServiceDesktop.ServiceDesktopClient().PlantillaListar()
                                    .Where(p => p.Concepto.Equals(ENConceptoPlantilla.Traspaso))
                                    .ToList();

                lblPlantillaCount.Text = plantillaIndex.ToString() + " / " + listaPlantillas.Count.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private VPlantilla MP_GuardarPlantilla()
        {
            var plantilla = new VPlantilla
            {
                Concepto = Convert.ToInt32(ENConceptoPlantilla.Traspaso),
                IdAlmacen = Convert.ToInt32(Cb_Origen.Value),
                IdAlmacenDestino = Convert.ToInt32(Cb_Destino.Value),
                Nombre = Tb_NombrePlantilla.Text,
            };

            try
            {
                if (new ServiceDesktop.ServiceDesktopClient().PlantillaGuardar(plantilla))
                { return plantilla; }
                else
                { return null; }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void MP_GuardarDetallePlantilla(VPlantilla plantilla)
        {
            var mensaje = "";
            if (plantilla != null)
            {
                var listaDetalle = new List<VPlantilla01>();
                foreach (var i in Dgv_DetalleNuevo.GetRows())
                {
                    var detalle = new VPlantilla01
                    {
                        Cantidad = Convert.ToInt32(i.Cells[3].Value),
                        IdPlantilla = plantilla.Id,
                        IdProducto = Convert.ToInt32(i.Cells[0].Value),
                        Precio = 0
                    };

                    listaDetalle.Add(detalle);
                }

                if (!new ServiceDesktop.ServiceDesktopClient().PlantillaDetalleGuardar(listaDetalle.ToArray(), plantilla.Id))
                {
                    mensaje = GLMensaje.Registro_Error("TRASPASOS");
                    this.MP_MostrarMensajeError(mensaje);
                }
            }
            else
            {
                mensaje = GLMensaje.Registro_Error("TRASPASOS");
                this.MP_MostrarMensajeError(mensaje);
            }
        }

        #endregion

        //===============
        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_Reiniciar();
        }

        public override bool MH_NuevoRegistro()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            var random = new Random(seed);
            var value = random.Next(0, 1000000);

            var traspaso = new VTraspaso
            {
                Concepto = 11,
                Destino = Convert.ToInt32(Cb_Destino.Value),
                Estado = 11,
                Fecha = DateTime.Now,
                Hora = DateTime.Now.ToShortTimeString(),
                Observaciones = Tb_Observaciones.Text,
                Origen = Convert.ToInt32(Cb_Origen.Value),
                Usuario = UTGlobal.Usuario,
                Id = value
            };

            var mensaje = "";

            try
            {
                if (new ServiceDesktop.ServiceDesktopClient().TraspasoGuardar(traspaso))
                {
                    this.MP_GuardarDetalleTraspaso(traspaso);
                    mensaje = GLMensaje.Modificar_Exito("TRASPASOS", traspaso.Id.ToString());
                    this.MP_InHabilitar();
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                    return true;
                }
                else
                {
                    mensaje = GLMensaje.Registro_Error("TRASPASOS");
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

        public override bool MH_Validar()
        {
            return false;
        }

        #endregion

        //===============
        #region Eventos

        private void F1_Traspaso_Load(object sender, EventArgs e)
        {
            LblTitulo.Text = "TRASPASOS";
            btnMax.Visible = false;
            this.MP_CargarPlantillas();
        }

        private void Dgv_ProductosInventario_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_ProductosInventario_Click(object sender, EventArgs e)
        {
            var row = Dgv_ProductosInventario.CurrentRow;

            if (row.Cells != null && row.Cells[0].Value != null)
            {
                if (!this.MP_ExisteItemEnLista(row.Cells[0].Value.ToString()))
                {
                    detalleProductos.Add(new Producto
                    {
                        Id = row.Cells[0].Value.ToString(),
                        Descripcion = row.Cells[2].Value.ToString(),
                        Unidad = row.Cells[7].Value.ToString(),
                        Cantidad = ""
                    });

                    Dgv_DetalleNuevo.DataSource = detalleProductos;
                    Dgv_DetalleNuevo.RetrieveStructure();
                    Dgv_DetalleNuevo.AlternatingColors = true;

                    Dgv_DetalleNuevo.RootTable.Columns[0].Key = "id";
                    Dgv_DetalleNuevo.RootTable.Columns[0].Visible = false;

                    Dgv_DetalleNuevo.RootTable.Columns[1].Key = "Descripcion";
                    Dgv_DetalleNuevo.RootTable.Columns[1].Caption = "Descripcion";
                    Dgv_DetalleNuevo.RootTable.Columns[1].Width = 150;
                    Dgv_DetalleNuevo.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleNuevo.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_DetalleNuevo.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleNuevo.RootTable.Columns[1].Visible = true;
                    Dgv_DetalleNuevo.RootTable.Columns[1].Selectable = false;

                    Dgv_DetalleNuevo.RootTable.Columns[2].Key = "Unidad";
                    Dgv_DetalleNuevo.RootTable.Columns[2].Caption = "Unidad";
                    Dgv_DetalleNuevo.RootTable.Columns[2].Width = 150;
                    Dgv_DetalleNuevo.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleNuevo.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_DetalleNuevo.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleNuevo.RootTable.Columns[2].Visible = true;
                    Dgv_DetalleNuevo.RootTable.Columns[2].Selectable = false;

                    Dgv_DetalleNuevo.RootTable.Columns[3].Key = "Cantidad";
                    Dgv_DetalleNuevo.RootTable.Columns[3].Caption = "Cantidad";
                    Dgv_DetalleNuevo.RootTable.Columns[3].Width = 150;
                    Dgv_DetalleNuevo.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleNuevo.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_DetalleNuevo.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleNuevo.RootTable.Columns[3].Visible = true;

                    //Habilitar filtradores
                    //Dgv_DetalleNuevo.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    ////Dgv_DetalleNuevo.FilterMode = FilterMode.Automatic;
                    ////Dgv_DetalleNuevo.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    ////Dgv_DetalleNuevo.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_DetalleNuevo.GroupByBoxVisible = false;
                    Dgv_DetalleNuevo.VisualStyle = VisualStyle.Office2007;
                }
            }
        }

        private void Cb_Origen_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Cb_Origen.Value != null)
                {
                    int AlmacenOrigenId;
                    if (int.TryParse(Cb_Origen.Value.ToString(), out AlmacenOrigenId))
                    {
                        this.MP_CargarProductosPorAlmacenOrigen(AlmacenOrigenId);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError("Ocurrió un error inesperado, por favor intente cerrar la ventana actual y pruebe nuevamente");
            }
        }

        private void btnAgregarPlantilla_Click(object sender, EventArgs e)
        {
            if (Dgv_DetalleNuevo.RowCount > 0)
            {
                if (!string.IsNullOrEmpty(Tb_NombrePlantilla.Text))
                {
                    this.MP_GuardarDetallePlantilla(this.MP_GuardarPlantilla());
                }
                else
                {
                    this.MP_MostrarMensajeError("Debe ingresar un nombre a la plantilla");
                    Tb_NombrePlantilla.BackColor = Color.Red;
                }
            }
            else
            {
                this.MP_MostrarMensajeError("Debe ingresar un item para agregar una nueva plantilla");
            }
        }

        private void Tb_NombrePlantilla_TextChanged(object sender, EventArgs e)
        {
            if (Tb_NombrePlantilla.BackColor.Equals(Color.Red))
            {
                Tb_NombrePlantilla.BackColor = Color.White;
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            index = 0;
            this.MP_MostrarRegistro(index);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index -= 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (index < listaTraspasos.Count - 1)
            {
                index += 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            index = listaTraspasos.Count - 1;
            this.MP_MostrarRegistro(index);
        }

        #endregion        

        public class Producto
        {
            public string Id { get; set; }

            public string Descripcion { get; set; }

            public string Unidad { get; set; }

            public string Cantidad { get; set; }
        }
    }
}

