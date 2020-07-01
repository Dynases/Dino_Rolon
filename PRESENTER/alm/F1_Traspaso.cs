using DevComponents.DotNetBar;
using ENTITY.inv.Traspaso.View;
using ENTITY.Plantilla;
using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
            lblEstadoTraspaso.Text = "";
            MP_AsignarPermisos();
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
            this.lblIdPlantilla.Visible = false;
            this.lblEstadoTraspasoValue.Visible = false;

            this.btnEstado.Visible = true;
            this.lblEstadoTraspaso.Visible = true;
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

            this.btnEstado.Visible = false;
            this.lblEstadoTraspaso.Visible = false;
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
            this.Dgv_ProductosInventario.DataSource = null;
            this.Dgv_DetalleNuevo.DataSource = null;
            this.Tb_Observaciones.Text = "";
            this.Tb_UsuarioEnvio.Text = "";
            this.Tb_UsuarioRecibe.Text = "";

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
            Cb_Destino.Value = traspaso.IdDestino;
            Cb_Origen.Value = traspaso.AlmacenOrigen;
            Tb_UsuarioEnvio.Text = traspaso.Usuario;
            lblFechaEnvio.Text = traspaso.Fecha.ToShortDateString();
            lblFechaRecepcion.Text = traspaso.Fecha.ToShortDateString();
            lblEstadoTraspasoValue.Text = traspaso.Estado.ToString();
            Tb_Observaciones.Text = traspaso.Observaciones;

            switch (traspaso.Estado)
            {
                case 1:
                    btnEstado.BackColor = Color.Red;
                    lblEstadoTraspaso.Text = "Enviado - Pendiente";
                    break;
                case 2:
                    btnEstado.BackColor = Color.Yellow;
                    lblEstadoTraspaso.Text = "Envio inmediato";
                    break;
                case 3:
                    btnEstado.BackColor = Color.Green;
                    lblEstadoTraspaso.Text = "Recepcionado";
                    break;
                default:
                    break;
            }

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

                    Dgv_DetalleTraspaso.RootTable.Columns[1].Key = "Estado";
                    Dgv_DetalleTraspaso.RootTable.Columns[1].Visible = false;

                    Dgv_DetalleTraspaso.RootTable.Columns[2].Key = "TraspasoId";
                    Dgv_DetalleTraspaso.RootTable.Columns[2].Caption = "COD";
                    Dgv_DetalleTraspaso.RootTable.Columns[2].Width = 80;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].Visible = true;
                    Dgv_DetalleTraspaso.RootTable.Columns[2].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[3].Key = "ProductoId";
                    Dgv_DetalleTraspaso.RootTable.Columns[3].Caption = "COD PROD";
                    Dgv_DetalleTraspaso.RootTable.Columns[3].Width = 100;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].Visible = true;
                    Dgv_DetalleTraspaso.RootTable.Columns[3].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[4].Key = "Detalle";
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Caption = "PRODUCTO";
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Width = 200;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].Visible = true;
                    Dgv_DetalleTraspaso.RootTable.Columns[4].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[5].Key = "Cantidad";
                    Dgv_DetalleTraspaso.RootTable.Columns[5].Caption = "CANTIDAD";
                    Dgv_DetalleTraspaso.RootTable.Columns[5].Width = 120;
                    Dgv_DetalleTraspaso.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_DetalleTraspaso.RootTable.Columns[5].Visible = true;
                    Dgv_DetalleTraspaso.RootTable.Columns[5].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[6].Key = "Lote";
                    Dgv_DetalleTraspaso.RootTable.Columns[6].Caption = "Lote";
                    Dgv_DetalleTraspaso.RootTable.Columns[6].Width = 250;
                    Dgv_DetalleTraspaso.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[6].Visible = false;
                    Dgv_DetalleTraspaso.RootTable.Columns[6].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[7].Key = "Fecha";
                    Dgv_DetalleTraspaso.RootTable.Columns[7].Caption = "Fecha";
                    Dgv_DetalleTraspaso.RootTable.Columns[7].Width = 250;
                    Dgv_DetalleTraspaso.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[7].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[7].Visible = false;
                    Dgv_DetalleTraspaso.RootTable.Columns[7].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[8].Key = "Unidad";
                    Dgv_DetalleTraspaso.RootTable.Columns[8].Caption = "Unidad";
                    Dgv_DetalleTraspaso.RootTable.Columns[8].Width = 150;
                    Dgv_DetalleTraspaso.RootTable.Columns[8].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[8].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[8].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[8].Visible = true;
                    Dgv_DetalleTraspaso.RootTable.Columns[8].EditType = EditType.NoEdit;

                    Dgv_DetalleTraspaso.RootTable.Columns[9].Key = "Marca";
                    Dgv_DetalleTraspaso.RootTable.Columns[9].Caption = "Marca";
                    Dgv_DetalleTraspaso.RootTable.Columns[9].Width = 150;
                    Dgv_DetalleTraspaso.RootTable.Columns[9].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleTraspaso.RootTable.Columns[9].CellStyle.FontSize = 8;
                    Dgv_DetalleTraspaso.RootTable.Columns[9].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleTraspaso.RootTable.Columns[9].Visible = true;
                    Dgv_DetalleTraspaso.RootTable.Columns[9].EditType = EditType.NoEdit;

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

        private void MP_GuardarDetalleTraspaso(VTraspaso traspaso, int idTI2)
        {
            var listaDetalle = new List<VTraspaso_01>();
            var mensaje = "";
            foreach (var i in Dgv_DetalleNuevo.GetRows())
            {
                var detalle = new VTraspaso_01
                {
                    Cantidad = Convert.ToInt32(i.Cells[4].Value),
                    FechaVencimiento = DateTime.Now,
                    Lote = "",
                    ProductoId = Convert.ToInt32(i.Cells[0].Value),
                    TraspasoId = traspaso.Id,
                    Estado = 1,
                    Marca = i.Cells[2].Value.ToString(),
                    Unidad = i.Cells[3].Value.ToString()
                };

                listaDetalle.Add(detalle);
            }

            if (!new ServiceDesktop.ServiceDesktopClient().TraspasoDetalleGuardar(listaDetalle.ToArray(),
                traspaso.Id, Convert.ToInt32(Cb_Origen.Value), idTI2))
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

                    Dgv_ProductosInventario.RootTable.Columns[1].Key = "ProductoId";
                    Dgv_ProductosInventario.RootTable.Columns[1].Caption = "COD";
                    Dgv_ProductosInventario.RootTable.Columns[1].Width = 60;
                    Dgv_ProductosInventario.RootTable.Columns[1].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[1].EditType = EditType.NoEdit;

                    Dgv_ProductosInventario.RootTable.Columns[2].Key = "AlmacenId";
                    Dgv_ProductosInventario.RootTable.Columns[2].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[3].Key = "Descripcion";
                    Dgv_ProductosInventario.RootTable.Columns[3].Caption = "Descripcion";
                    Dgv_ProductosInventario.RootTable.Columns[3].Width = 120;
                    Dgv_ProductosInventario.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[3].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[3].EditType = EditType.NoEdit;

                    Dgv_ProductosInventario.RootTable.Columns[4].Key = "Saldo";
                    Dgv_ProductosInventario.RootTable.Columns[4].Caption = "Saldo";
                    Dgv_ProductosInventario.RootTable.Columns[4].Width = 80;
                    Dgv_ProductosInventario.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_ProductosInventario.RootTable.Columns[4].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[4].EditType = EditType.NoEdit;

                    Dgv_ProductosInventario.RootTable.Columns[5].Key = "División";
                    Dgv_ProductosInventario.RootTable.Columns[5].Caption = "División";
                    Dgv_ProductosInventario.RootTable.Columns[5].Width = 110;
                    Dgv_ProductosInventario.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[5].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[5].EditType = EditType.NoEdit;

                    Dgv_ProductosInventario.RootTable.Columns[6].Key = "Marca";
                    Dgv_ProductosInventario.RootTable.Columns[6].Caption = "Marca";
                    Dgv_ProductosInventario.RootTable.Columns[6].Width = 110;
                    Dgv_ProductosInventario.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[6].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[6].EditType = EditType.NoEdit;

                    Dgv_ProductosInventario.RootTable.Columns[7].Key = "Categorías";
                    Dgv_ProductosInventario.RootTable.Columns[7].Caption = "Categoría";
                    Dgv_ProductosInventario.RootTable.Columns[7].Width = 110;
                    Dgv_ProductosInventario.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[7].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[7].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[7].EditType = EditType.NoEdit;

                    Dgv_ProductosInventario.RootTable.Columns[8].Key = "Unidad";
                    Dgv_ProductosInventario.RootTable.Columns[8].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[9].Key = "UnidadVentaDisplay";
                    Dgv_ProductosInventario.RootTable.Columns[9].Caption = "Unidad";
                    Dgv_ProductosInventario.RootTable.Columns[9].Width = 80;
                    Dgv_ProductosInventario.RootTable.Columns[9].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[9].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[9].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[9].Visible = true;
                    Dgv_ProductosInventario.RootTable.Columns[9].EditType = EditType.NoEdit;

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

        private bool MP_ValidarExistenciaSeleccionado(GridEXRow row)
        {
            var response = false;

            if (Convert.ToInt32(row.Cells[4].Value) <= 0)
            {
                response = true;
                this.MP_MostrarMensajeError("No puede seleccionar un producto con STOCK EN CERO");
            }

            return response;
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

        private void MP_CargarPlantillas(int AlmacenOrigen, int AlmacenDestino)
        {
            try
            {
                plantillaIndex = 0;
                listaPlantillas = new ServiceDesktop.ServiceDesktopClient()
                                                    .PlantillaListar(ENConceptoPlantilla.Traspaso)
                                                    .Where(p => p.IdAlmacen == AlmacenOrigen && p.IdAlmacenDestino == AlmacenDestino)
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
            var vPlantilla = new VPlantilla
            {
                Concepto = Convert.ToInt32(ENConceptoPlantilla.Traspaso),
                IdAlmacen = Convert.ToInt32(Cb_Origen.Value),
                IdAlmacenDestino = Convert.ToInt32(Cb_Destino.Value),
                Nombre = Tb_NombrePlantilla.Text,
            };

            try
            {
                int id = 0;
                if (new ServiceDesktop.ServiceDesktopClient().PlantillaGuardar(vPlantilla, ref id))
                {
                    vPlantilla.Id = id;
                    return vPlantilla;
                }
                else
                {
                    return null;
                }
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
                        Cantidad = Convert.ToInt32(i.Cells[4].Value),
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
            this.MP_CargarInventario();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_Reiniciar();
            this.MP_CargarAlmacenes();
            this.MP_CargarListaTraspasos();
        }

        public override bool MH_NuevoRegistro()
        {
            var Vtraspaso = new VTraspaso
            {
                Concepto = 11,
                IdDestino = Convert.ToInt32(Cb_Destino.Value),
                Estado = 1,
                Fecha = DateTime.Now,
                Hora = DateTime.Now.ToShortTimeString(),
                Observaciones = Tb_Observaciones.Text,
                AlmacenOrigen = Convert.ToInt32(Cb_Origen.Value),
                Usuario = UTGlobal.Usuario
            };

            var mensaje = "";

            try
            {
                int id = 0;
                int idTI2 = 0;
                if (new ServiceDesktop.ServiceDesktopClient().TraspasoGuardar(Vtraspaso, ref idTI2, ref id))
                {
                    Vtraspaso.Id = id;
                    this.MP_GuardarDetalleTraspaso(Vtraspaso, idTI2);

                    this.MP_Reiniciar();
                    this.MP_CargarAlmacenes();
                    this.MP_CargarListaTraspasos();

                    mensaje = GLMensaje.Modificar_Exito("TRASPASOS", Vtraspaso.Id.ToString());
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

        public void MP_CargarInventario()
        {
            try
            {
                if (Cb_Origen.Value != null && Cb_Destino.Value != null)
                {
                    int AlmacenOrigenId;
                    int AlmacenDestinoId;
                    if (int.TryParse(Cb_Origen.Value.ToString(), out AlmacenOrigenId) &&
                        int.TryParse(Cb_Destino.Value.ToString(), out AlmacenDestinoId))
                    {
                        this.MP_CargarProductosPorAlmacenOrigen(AlmacenOrigenId);
                        this.MP_CargarPlantillas(AlmacenOrigenId, AlmacenDestinoId);
                    }
                }
            }
            catch
            {
                this.MP_MostrarMensajeError("Ocurrió un error inesperado, por favor intente cerrar la ventana actual y pruebe nuevamente");
            }
        }

        #endregion

        //===============
        #region Eventos

        private void F1_Traspaso_Load(object sender, EventArgs e)
        {
            LblTitulo.Text = "TRASPASOS";
            btnMax.Visible = false;
            this.MP_CargarPlantillas(Convert.ToInt32(Cb_Origen.Value), Convert.ToInt32(Cb_Destino.Value));
        }

        private void Dgv_ProductosInventario_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_ProductosInventario_Click(object sender, EventArgs e)
        {
            if (!this.Cb_Origen.Value.ToString().Equals(this.Cb_Destino.Value.ToString()))
            {
                var row = Dgv_ProductosInventario.CurrentRow;

                if (row.Cells != null && row.Cells[0].Value != null)
                {
                    if (!this.MP_ExisteItemEnLista(row.Cells[1].Value.ToString()) &&
                        !this.MP_ValidarExistenciaSeleccionado(row))
                    {
                        detalleProductos.Add(new Producto
                        {
                            Id = row.Cells[1].Value.ToString(),
                            Descripcion = row.Cells[3].Value.ToString(),
                            Marca = row.Cells[6].Value.ToString(),
                            Unidad = row.Cells[9].Value.ToString(),
                            Cantidad = ""
                        });

                        Dgv_DetalleNuevo.DataSource = detalleProductos;
                        Dgv_DetalleNuevo.RetrieveStructure();
                        Dgv_DetalleNuevo.AlternatingColors = true;

                        Dgv_DetalleNuevo.RootTable.Columns[0].Key = "Id";
                        Dgv_DetalleNuevo.RootTable.Columns[0].Caption = "COD";
                        Dgv_DetalleNuevo.RootTable.Columns[0].Width = 50;
                        Dgv_DetalleNuevo.RootTable.Columns[0].Visible = true;
                        Dgv_DetalleNuevo.RootTable.Columns[0].EditType = EditType.NoEdit;

                        Dgv_DetalleNuevo.RootTable.Columns[1].Key = "Descripcion";
                        Dgv_DetalleNuevo.RootTable.Columns[1].Caption = "Descripcion";
                        Dgv_DetalleNuevo.RootTable.Columns[1].Width = 150;
                        Dgv_DetalleNuevo.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_DetalleNuevo.RootTable.Columns[1].CellStyle.FontSize = 8;
                        Dgv_DetalleNuevo.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_DetalleNuevo.RootTable.Columns[1].Visible = true;
                        Dgv_DetalleNuevo.RootTable.Columns[1].EditType = EditType.NoEdit;

                        Dgv_DetalleNuevo.RootTable.Columns[2].Key = "Marca";
                        Dgv_DetalleNuevo.RootTable.Columns[2].Caption = "Marca";
                        Dgv_DetalleNuevo.RootTable.Columns[2].Width = 100;
                        Dgv_DetalleNuevo.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_DetalleNuevo.RootTable.Columns[2].CellStyle.FontSize = 8;
                        Dgv_DetalleNuevo.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_DetalleNuevo.RootTable.Columns[2].Visible = true;
                        Dgv_DetalleNuevo.RootTable.Columns[2].EditType = EditType.NoEdit;

                        Dgv_DetalleNuevo.RootTable.Columns[3].Key = "Unidad";
                        Dgv_DetalleNuevo.RootTable.Columns[3].Caption = "Unidad";
                        Dgv_DetalleNuevo.RootTable.Columns[3].Width = 100;
                        Dgv_DetalleNuevo.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_DetalleNuevo.RootTable.Columns[3].CellStyle.FontSize = 8;
                        Dgv_DetalleNuevo.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_DetalleNuevo.RootTable.Columns[3].Visible = true;
                        Dgv_DetalleNuevo.RootTable.Columns[3].EditType = EditType.NoEdit;

                        Dgv_DetalleNuevo.RootTable.Columns[4].Key = "Cantidad";
                        Dgv_DetalleNuevo.RootTable.Columns[4].Caption = "Cantidad";
                        Dgv_DetalleNuevo.RootTable.Columns[4].Width = 100;
                        Dgv_DetalleNuevo.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_DetalleNuevo.RootTable.Columns[4].CellStyle.FontSize = 8;
                        Dgv_DetalleNuevo.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                        Dgv_DetalleNuevo.RootTable.Columns[4].Visible = true;

                        Dgv_DetalleNuevo.GroupByBoxVisible = false;
                        Dgv_DetalleNuevo.VisualStyle = VisualStyle.Office2007;
                    }
                }
            }
            else
            {
                this.MP_MostrarMensajeError("Los Almacenes de Origen y Destino no pueden ser iguales");
            }
        }

        private void Cb_Origen_ValueChanged(object sender, EventArgs e)
        {
            this.MP_CargarInventario();
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

        private void btnGuardarPlantilla_Click(object sender, EventArgs e)
        {
            try
            {

                if (Dgv_DetalleNuevo.RowCount > 0)
                {
                    if (!string.IsNullOrEmpty(Tb_NombrePlantilla.Text))
                    {
                        this.MP_GuardarDetallePlantilla(this.MP_GuardarPlantilla());
                        var mensaje = GLMensaje.Modificar_Exito("TRASPASOS", "Plantilla ");
                        ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
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
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (Tb_UsuarioEnvio.Text.Equals(UTGlobal.Usuario) &&
                lblEstadoTraspasoValue.Text.Equals("1"))
            {
                if (MessageBoxEx.Show("¿Está seguro de recepcionar y confirmar el traspaso de almacen?", "TRASPASO",
                   System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        if (new ServiceDesktop.ServiceDesktopClient().TraspasoConfirmarRecepcion(Convert.ToInt32(lblId.Text), UTGlobal.Usuario))
                        {
                            this.MP_Reiniciar();
                            this.MP_CargarAlmacenes();
                            this.MP_CargarListaTraspasos();

                            ToastNotification.Show(this, "Traspaso recepcionado, inventario actualizado",
                                PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico,
                                eToastGlowColor.Green, eToastPosition.TopCenter);
                        }
                        else
                        {
                            this.MP_MostrarMensajeError("Ocurrio un error al recepcionar el traspaso. " +
                                "porfavor cierra la pantalla e intente nuevamente");
                        }
                    }
                    catch (Exception ex)
                    {
                        this.MP_MostrarMensajeError(ex.Message);
                    }
                }
            }
        }

        #endregion        

        public class Producto
        {
            public string Id { get; set; } //0

            public string Descripcion { get; set; } //1

            public string Marca { get; set; } //2

            public string Unidad { get; set; } //3

            public string Cantidad { get; set; } //4
        }
    }
}

