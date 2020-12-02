using ENTITY.inv.Concepto.View;
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
using Ent = ENTITY.inv.Concepto.View.VConcepto;

namespace PRESENTER.reg
{
    public partial class F1_Concepto : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "CONCEPTO";
        int _MPos = 0;
        Ent _concepto = new Ent();
        #endregion
        public F1_Concepto()
        {
            InitializeComponent();
            BtnImprimir.Visible = false;
        }
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                VConceptosBindingSource.DataSource = _concepto;
                this.Text = _NombreFormulario;
                LblTitulo.Text = _NombreFormulario;              
                MP_InHabilitar();
                MP_AsignarPermisos();
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

        private void MP_ArmarGrillas()
        {
            MP_ArmarGrillaListado();      
        }       
       
        private void MP_ArmarGrillaListado()
        {
            try
            {
                List<VConcepto> ListaCompleta = new ServiceDesktop.ServiceDesktopClient().concepto().ToList(); //go-dev revizar
                Dgv_GBuscador.ConfigInicialVinculado(ListaCompleta, "Ajuste");

                Dgv_GBuscador.ColAL(nameof(EntList.Id), "Código", 100);
                Dgv_GBuscador.ColAL(nameof(EntList.Fecha), "Fecha", 100);
                Dgv_GBuscador.ColAL(nameof(EntList.NConcepto), "Concepto", 150);
                Dgv_GBuscador.ColAL(nameof(EntList.Obs), "Observación", 600);
                Dgv_GBuscador.ColAL(nameof(EntList.NAlmacen), "Alamacen", 200);

                Dgv_GBuscador.ConfigFinalBasica();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        public void MP_ArmarGrillaDetalle()
        {
            try
            {
                dgjDetalle.ConfigInicialVinculado<List<EntDet>>(_detalles, "Detalle");

                dgjDetalle.ColNoVisible(nameof(EntDet.IdView));
                dgjDetalle.ColNoVisible(nameof(EntDet.Id));
                dgjDetalle.ColNoVisible(nameof(EntDet.IdAjuste));
                dgjDetalle.ColNoVisible(nameof(EntDet.IdProducto));
                dgjDetalle.ColNoVisible(nameof(EntDet.Estado));
                dgjDetalle.ColNoVisible(nameof(EntDet.Stock)); ;

                dgjDetalle.ColAL(nameof(EntDet.CodProducto), "Código", 80);
                dgjDetalle.ColAL(nameof(EntDet.NProducto), "Producto", 200);
                dgjDetalle.ColAC(nameof(EntDet.Unidad), "UN", 70);
                dgjDetalle.ColARNro(nameof(EntDet.Cantidad), "Cantidad", 110, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Contenido), "Contenido", 110, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.TotalContenido), "TotalContenido", 110, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Precio), "P. Unitario", 100, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Total), "Total", 100, "0.00");
                dgjDetalle.ColAL(nameof(EntDet.Lote), "Lote", 130);
                dgjDetalle.ColAL(nameof(EntDet.FechaVen), "Fecha ven", 130);

                dgjDetalle.ColIcon("Eliminar", "Eliminar", new Bitmap(Properties.Resources.delete, new Size(20, 20)));

                dgjDetalle.ConfigFinalDetalle();

                var filtroEliminado = new GridEXFilterCondition(dgjDetalle.RootTable.Columns[nameof(EntDet.Estado)], ConditionOperator.NotEqual, (int)ENEstado.ELIMINAR);
                dgjDetalle.RootTable.ApplyFilter(filtroEliminado);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_ArmarGrillaBusquedaProducto()
        {
            try
            {
                dgjProducto.ConfigInicialVinculado<List<VProductoListaStock>>(_productos, "Producto");

                dgjProducto.ColNoVisible(nameof(VProductoListaStock.IdProducto));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.IdAlmacen));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.IdCategoriaPrecio));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.PrecioVenta));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.CategoriaPrecio));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.TipoProducto));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.CategoriaProducto));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.EsLote));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.Contenido));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.PrecioMaxVenta));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.PrecioMinVenta));

                dgjProducto.ColAL(nameof(VProductoListaStock.CodigoProducto), "Código", 80);
                dgjProducto.ColAL(nameof(VProductoListaStock.Producto), "Producto", 180);
                dgjProducto.ColAL(nameof(VProductoListaStock.Division), "División", 100);
                dgjProducto.ColAL(nameof(VProductoListaStock.UnidadVenta), "UN.", 60);

                dgjProducto.ColARNro(nameof(VProductoListaStock.PrecioCosto), "P. Costo", 100, "0.00");
                dgjProducto.ColARNro(nameof(VProductoListaStock.Stock), "Stock", 100, "0.00");
                dgjProducto.ConfigFinalBasica();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ArmarGrillaBusquedaLotes()
        {
            try
            {
                dgjProducto.ConfigInicialVinculado<List<VTI001>>(_lotes, "Lotes");

                dgjProducto.ColNoVisible(nameof(VTI001.IdProducto));
                dgjProducto.ColNoVisible(nameof(VTI001.IdAlmacen));
                dgjProducto.ColNoVisible(nameof(VTI001.Unidad));
                dgjProducto.ColNoVisible(nameof(VTI001.id));

                dgjProducto.ColAL(nameof(VTI001.Lote), "Lote", 150);
                dgjProducto.ColAL(nameof(VTI001.FechaVen), "Fecha Ven.", 150);
                dgjProducto.ColAL(nameof(VTI001.Cantidad), "Stock", 150);

                dgjProducto.ConfigFinalBasica();

                dgjProducto.GroupByBoxVisible = false;
                dgjProducto.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
                var filtroEliminado = new GridEXFilterCondition(dgjProducto.RootTable.Columns[nameof(EntDet.Estado)], ConditionOperator.NotEqual, (int)ENEstado.ELIMINAR);
                dgjProducto.RootTable.ApplyFilter(filtroEliminado);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarListado()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var listado = servicio.Ajuste_Lista().ToList();
                    if (listado == null)
                        listado = new List<EntList>();
                    _listado.Clear();
                    _listado.AddRange(listado);
                    Dgv_GBuscador.Refetch();
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
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var detalles = servicio.AjusteDetalle_Lista(id).ToList();
                    if (detalles == null)
                        detalles = new List<EntDet>();
                    _detalles.Clear();
                    _detalles.AddRange(detalles);
                    dgjDetalle.Refetch();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_CargarProductos()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {

                    var almacen = servicio.AlmacenListar().ToList().Find(a => a.Id == Convert.ToInt32(cbAlmacen.Value));
                    var productos = servicio.ListarProductosStock(almacen.SucursalId, almacen.Id, (int)ENCategoriaPrecio.COSTO).ToList();
                    if (productos == null)
                        productos = new List<VProductoListaStock>();
                    _productos.Clear();
                    _productos.AddRange(productos);
                    MP_ArmarGrillaBusquedaProducto();
                    // dgjProducto.Refetch();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Habilitar()
        {
            dtiFecha.IsInputReadOnly = false;
            dtiFecha.ButtonDropDown.Enabled = true;
            cbConcepto.ReadOnly = false;
            cbAlmacen.ReadOnly = false;
            tbObs.ReadOnly = false;
            cbCategoriaPrecio.ReadOnly = false;
            dgjDetalle.Enabled = true;
        }

        private void MP_InHabilitar()
        {
            tbCodigo.ReadOnly = true;
            dtiFecha.IsInputReadOnly = true;
            dtiFecha.ButtonDropDown.Enabled = false;
            cbConcepto.ReadOnly = true;
            cbAlmacen.ReadOnly = true;
            tbObs.ReadOnly = true;
            cbCategoriaPrecio.ReadOnly = true;
            dgjDetalle.Enabled = true;
        }

        private void MP_Limpiar()
        {
            try
            {
                var conceptos = (List<VConceptoCombo>)cbConcepto.DataSource;
                var almacenes = (List<VAlmacenCombo>)cbAlmacen.DataSource;
                var categoriaPrecio = (List<VPrecioCategoria>)cbCategoriaPrecio.DataSource;

                _concepto.Id = 0;
                _concepto.Fecha = DateTime.Today;
                _concepto.IdConcepto = conceptos.First().Id;
                _concepto.IdAlmacen = almacenes.First().IdLibreria;
                _concepto.Obs = string.Empty;
                vAjusteBindingSource.ResetCurrentItem();

                _detalles = _detalles.Where(t => t.IdView == "0").ToList();
                dgjDetalle.Refetch();
                MP_ArmarGrillaDetalle();
                //_detalles.RemoveRange(0, _detalles.Count);
                //dgjDetalle.Refetch();

                MP_LimpiarColor();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }

        public void MP_LimpiarColor()
        {
            cbConcepto.BackColor = Color.White;
            cbAlmacen.BackColor = Color.White;
            cbCategoriaPrecio.BackColor = Color.White;
            tbObs.BackColor = Color.White;
        }

        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_GBuscador.Row = _Pos;
                if (_Pos > -1)
                {
                    var ajuste = (EntList)Dgv_GBuscador.GetRow(_Pos).DataRow;

                    var conceptos = (List<VConceptoCombo>)cbConcepto.DataSource;
                    var almacenes = (List<VAlmacenCombo>)cbAlmacen.DataSource;
                    var categoriaPrecio = (List<VPrecioCategoria>)cbCategoriaPrecio.DataSource;
                    _concepto.Id = ajuste.Id;
                    _concepto.Fecha = ajuste.Fecha;
                    _concepto.IdConcepto = conceptos.First(a => a.Descripcion == ajuste.NConcepto).Id;
                    _concepto.IdAlmacen = almacenes.First(a => a.Descripcion == ajuste.NAlmacen).IdLibreria;
                    _concepto.Obs = ajuste.Obs;
                    vAjusteBindingSource.ResetCurrentItem();
                    MP_CargarDetalle(ajuste.Id);
                    MP_ObtenerCalculo();
                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Filtrar(int tipo)
        {
            MP_CargarListado();
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = tipo == 1 ? 0 : Dgv_GBuscador.Row;
                MP_MostrarRegistro(_MPos);
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

        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_AddFila()
        {
            try
            {
                var itemVacio = _detalles.Where(a => a.Cantidad == 0 && a.NProducto == string.Empty).Count();
                if (itemVacio > 0 && _detalles.Count > 0)
                {
                    return;
                }
                var fechaVencimiento = Convert.ToDateTime("2017-01-01");
                var nuevo = new EntDet()
                {
                    Id = 0,
                    IdAjuste = 0,
                    IdProducto = 0,
                    CodProducto = string.Empty,
                    NProducto = string.Empty,
                    Unidad = string.Empty,
                    Cantidad = 0,
                    Precio = 0,
                    Total = 0,
                    Lote = "20170101",
                    FechaVen = fechaVencimiento,
                    Estado = (int)ENEstado.NUEVO,
                    Stock = 0
                };
                _detalles.Add(nuevo);
                dgjDetalle.Refetch();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_InHabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = false;
                GPanel_Producto.Height = 30;
                dgjDetalle.Select();
                dgjDetalle.Col = dgjDetalle.RootTable.Columns[nameof(EntDet.Cantidad)].Index;
                dgjDetalle.Row = dgjDetalle.RowCount - 1;
                _producto = new VProductoListaStock();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_InsertarProducto()
        {
            try
            {
                GPanel_Producto.Text = "PRODUCTOS DE COMERCIALES";
                MP_CargarProductos();
                MP_HabilitarProducto();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_HabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = true;
                GPanel_Producto.Height = 350;
                dgjProducto.Focus();
                dgjProducto.MoveTo(dgjProducto.FilterRow);
                dgjProducto.Col = dgjProducto.RootTable.Columns[nameof(VProductoListaStock.Producto)].Index;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_VerificarSeleccion(string columna)
        {
            try
            {
                if (dgjDetalle.Col == dgjDetalle.RootTable.Columns[columna].Index)
                {
                    if (dgjDetalle.GetValue("NProducto").ToString() != string.Empty && dgjDetalle.GetValue("IdProducto").ToString() != string.Empty)
                    {
                        MP_AddFila();
                        MP_InsertarProducto();
                    }
                    else
                        throw new Exception("Seleccione un producto");
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_EliminarFila()
        {
            try
            {
                if (dgjDetalle.RowCount > 1)
                {
                    dgjDetalle.UpdateData();
                    int estado = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Estado"].Value);
                    string idView = dgjDetalle.CelValorIdView();
                    if (estado == (int)ENEstado.NUEVO)
                    {
                        _detalles = _detalles.Where(t => t.IdView != idView).ToList();
                        dgjDetalle.Refetch();
                        MP_ArmarGrillaDetalle();
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO || estado == (int)ENEstado.MODIFICAR)
                        {
                            dgjDetalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.ELIMINAR;
                            dgjDetalle.UpdateData();
                            dgjDetalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(dgjDetalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
                        }
                    }
                }
                else
                    throw new Exception("El detalle no puede estar vacio");
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
                decimal cantidad = dgjDetalle.CelValor<decimal>(nameof(EntDet.Cantidad));
                decimal precio = dgjDetalle.CelValor<decimal>(nameof(EntDet.Precio));
                decimal total = cantidad * precio;


                var idView = dgjDetalle.CelValorIdView();
                var item = _detalles.Where(a => a.IdView == idView).First();
                item.Cantidad = cantidad;
                item.Total = total;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion
    }
}
