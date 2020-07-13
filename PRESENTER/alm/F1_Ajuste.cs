using DevComponents.DotNetBar;
using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using ENTITY.inv.Ajuste.View;
using ENTITY.inv.Almacen.View;
using ENTITY.inv.Concepto;
using ENTITY.inv.Concepto.View;
using ENTITY.Producto.View;
using ENTITY.reg.PrecioCategoria.View;
using Janus.Windows.GridEX;
using MODEL;
using PRESENTER.frm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;
using UTILITY.MetodosExtencion;
using Ent = ENTITY.inv.Ajuste.View.VAjuste;
using EntDet = ENTITY.inv.Ajuste.View.VAjusteDetalle;
using EntList = ENTITY.inv.Ajuste.View.VAjusteLista;

namespace PRESENTER.alm
{
    public partial class F1_Ajuste : ModeloF1
    {
        #region Variables
        string _NombreFormulario = "AJSUTE";
        int _MPos = 0;
        Ent _ajuste = new Ent();
        List<EntDet> _detalles = new List<EntDet>();
        List<EntList> _listado = new List<EntList>();
        List<VProductoLista> _productos = new List<VProductoLista>();
        #endregion
        public F1_Ajuste()
        {
            InitializeComponent();
        }
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                vAjusteBindingSource.DataSource = _ajuste;
                LblTitulo.Text = _NombreFormulario;
                MP_ArmarGrillas();
                MP_ArmarCombos();
                MP_InHabilitar();
                MP_AsignarPermisos();
                MP_Limpiar(); //go-dev revisar
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

        public void MP_ArmarCombos()
        {
            MP_CargarConcepto();
            MP_CargarAlmacenes();
            MP_CargarCategoriaPrecio();
        }

        private void MP_ArmarGrillas()
        {
            MP_ArmarGrillaListado();
            MP_ArmarGrillaDetalle();
            MP_ArmarGrillaBusquedaProducto();
        }

        public void MP_CargarConcepto()
        {
            try
            {
                var conceptos = new ServiceDesktop.ServiceDesktopClient().Concepto_ListaComboAjuste().ToList();
                UTGlobal.MG_ArmarComboConcepto(cbConcepto, conceptos);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(cbAlmacen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        public void MP_CargarCategoriaPrecio()
        {
            try
            {
                var categoriasPrecio = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
                UTGlobal.MG_ArmarCombo_CatPrecio(cbCategoriaPrecio, categoriasPrecio);
                cbCategoriaPrecio.SelecionarPrimero();
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_ArmarGrillaListado()
        {
            try
            {
                List<EntList> ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Ajuste_Lista().ToList(); //go-dev revizar
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

                dgjDetalle.ColAL(nameof(EntDet.CodProducto), "Código", 100);
                dgjDetalle.ColAL(nameof(EntDet.NProducto), "Producto", 150);
                dgjDetalle.ColAC(nameof(EntDet.Unidad), "Código", 80);
                dgjDetalle.ColARNro(nameof(EntDet.Cantidad), "Cantidad", 100, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Precio), "P. Unitario", 100, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Total), "Total", 100, "0.00");
                dgjDetalle.ColAL(nameof(EntDet.Lote), "Lote", 150);
                dgjDetalle.ColAL(nameof(EntDet.FechaVen), "Fecha ven", 100);

                dgjDetalle.ColIcon("Eliminar", string.Empty, new Bitmap(Properties.Resources.delete, new Size(15, 15)));

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
                dgjProducto.ConfigInicialVinculado<List<VProductoLista>>(_productos, "Producto");

                dgjProducto.ColNoVisible(nameof(VProductoLista.Id));
                dgjProducto.ColNoVisible(nameof(VProductoLista.Tipo));
                dgjProducto.ColNoVisible(nameof(VProductoLista.Usuario));
                dgjProducto.ColNoVisible(nameof(VProductoLista.Hora));
                dgjProducto.ColNoVisible(nameof(VProductoLista.Fecha));

                dgjProducto.ColAL(nameof(VProductoLista.Codigo), "Código", 100);
                dgjProducto.ColAL(nameof(VProductoLista.Descripcion), "Descripción", 150);
                dgjProducto.ColAL(nameof(VProductoLista.Grupo1), "División", 120);
                dgjProducto.ColAL(nameof(VProductoLista.Grupo1), "Tipo", 120);
                dgjProducto.ColAL(nameof(VProductoLista.Grupo3), "Categoria", 120);

                dgjProducto.ConfigFinalBasica();
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
                    var productos = servicio.ProductoListar().Where(p => p.Tipo.Equals(1)).ToList();
                    if (productos == null)
                        productos = new List<VProductoLista>();
                    _productos.Clear();
                    _productos.AddRange(productos);
                    dgjProducto.Refetch();
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
                _ajuste.Id = 0;
                _ajuste.Fecha = DateTime.Today;
                _ajuste.IdConcepto = conceptos.First().Id;
                _ajuste.IdAlmacen = almacenes.First().IdLibreria;
                _ajuste.Obs = string.Empty;
                _detalles.Clear();
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
                    _ajuste.Id = ajuste.Id;
                    _ajuste.Fecha = ajuste.Fecha;
                    _ajuste.IdConcepto = conceptos.First(a => a.Descripcion == ajuste.NConcepto).Id;
                    _ajuste.IdAlmacen = almacenes.First(a => a.Descripcion == ajuste.NAlmacen).IdLibreria;
                    _ajuste.Obs = ajuste.Obs;

                    MP_CargarDetalle(ajuste.Id);

                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Filtrar(int tipo) //go-dev revisar
        {
            MP_CargarListado();
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
                dgjDetalle.Select();
                dgjDetalle.Col = dgjDetalle.RootTable.Columns[nameof(EntDet.Cantidad)].Index;
                dgjDetalle.Row = dgjDetalle.RowCount - 1;
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
                dgjProducto.Focus();
                dgjProducto.MoveTo(dgjProducto.FilterRow);
                dgjProducto.Col = dgjProducto.RootTable.Columns[nameof(VProductoLista.Descripcion)].Index;
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
                        MP_HabilitarProducto();
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
                    int idDetalle = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Id"].Value);
                    if (estado == (int)ENEstado.NUEVO)
                    {
                        _detalles = ((List<VAjusteDetalle>)dgjDetalle.DataSource).ToList();
                        var lista = _detalles.Where(t => t.Id == idDetalle).FirstOrDefault();
                        _detalles.Remove(lista);
                        //MP_ArmarDetalle(); //go-dev
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

        #region Eventos
        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }

        private void dgjDetalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (tbObs.ReadOnly == false)
                {
                    if (e.Column.Index == dgjDetalle.RootTable.Columns[nameof(EntDet.Cantidad)].Index)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void dgjDetalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                var idView = dgjDetalle.CelValorIdView();
                var item = _detalles.Where(a => a.IdView == idView).First();
                if (item.Estado == (int)ENEstado.GUARDADO)
                {
                    item.Estado = (int)ENEstado.MODIFICAR;
                }

                MP_CalcularFila();

                MP_VerificarSeleccion("NProducto");
                MP_VerificarSeleccion("Cantidad");
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void dgjDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (tbObs.ReadOnly == false)
                {
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter && dgjDetalle.Row >= 0
                        && dgjDetalle.Col == dgjDetalle.RootTable.Columns["NProducto"].Index)
                    {
                        int estado = Convert.ToInt32(dgjDetalle.CurrentRow.Cells["Estado"].Value);
                        if (estado == (int)ENEstado.NUEVO)
                        {

                            MP_InsertarProducto();
                        }
                        else
                        {
                            string mensaje = "-No se puede cambiar de producto " + "\n" +
                                             "-Modifique la cantidad";
                            throw new Exception(mensaje);
                        }

                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        //Eliminar FIla.
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void dgjProducto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (tbObs.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter && dgjProducto.Row > -1)
                    {
                        string idView = dgjDetalle.CelValorIdView();
                        using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                        {
                            var idProducto = dgjProducto.CelValor<int>(nameof(VProductoLista.Id));
                            var producto = servicio.ProductoListarXId(idProducto);
                            var unidadVenta = servicio.LibreriaListarCombo((int)ENEstaticosGrupo.PRODUCTO, (int)ENEstaticosOrden.PRODUCTO_UN_VENTA)
                                .Where(l => l.IdLibreria == producto.UniVenta)
                                .Select(l => l.Descripcion).FirstOrDefault();
                            var precio = servicio.PrecioProductoListar((int)cbAlmacen.Value)
                                .Where(p => p.IdProducto == idProducto && p.IdPrecioCat == ((VPrecioCategoria)cbCategoriaPrecio.SelectedItem).Id)
                                .Select(p => p.Precio).FirstOrDefault();

                            var fila = _detalles.Where(a => a.IdView == idView).First();
                            fila.IdProducto = producto.Id;
                            fila.CodProducto = producto.IdProd;
                            fila.NProducto = producto.Descripcion;
                            fila.Unidad = unidadVenta;
                            fila.Cantidad = 0;
                            fila.Precio = precio;
                            fila.Total = 0;
                            if (fila.Estado == (int)ENEstado.GUARDADO || fila.Estado == (int)ENEstado.MODIFICAR)
                            {
                                fila.Estado = (int)ENEstado.MODIFICAR;
                            }
                            dgjDetalle.Refetch();
                            MP_InHabilitarProducto();
                        }
                    }
                    if (e.KeyData == Keys.Escape)
                    {
                        MP_InHabilitarProducto();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = 0;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (_MPos > 0 && Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = _MPos - 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (_MPos < Dgv_GBuscador.RowCount - 1 && _MPos >= 0)
                {
                    _MPos = Dgv_GBuscador.Row + 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = Dgv_GBuscador.RowCount - 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion

        #region Metodo heredados
        private void MP_VerificarExistenciaLote()
        {
            int IdCompra = Convert.ToInt32(tbCodigo.Text);
            var compra_01 = new ServiceDesktop.ServiceDesktopClient().Compra_01_Lista(IdCompra).ToList();
            var detalle = ((List<VCompra_01>)dgjDetalle.DataSource).ToList();
            foreach (var item in detalle)
            {
                if (new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ExisteEnSeleccion(IdCompra))
                {
                    throw new Exception("La compra esta asociado a una Seleccion.");
                }
                var producto = compra_01.Where(p => p.IdProducto == item.IdProducto).FirstOrDefault();
                if (item.Lote == producto.Lote && item.FechaVen == producto.FechaVen)
                {

                }
            }
        }

        public override bool MH_NuevoRegistro()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var result = servicio.Ajuste_Guardar(_ajuste, _detalles.Where(a => a.NProducto != string.Empty).ToArray(), TxtNombreUsu.Text);
                }
                return true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return true;
            }
        }

        public override bool MH_Eliminar()
        {
            try
            {
                int IdCompra = Convert.ToInt32(tbCodigo.Text);
                Efecto efecto = new Efecto();
                efecto.Tipo = 2;
                efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
                efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
                efecto.ShowDialog();
                bool resul = false;
                if (efecto.Band)
                {
                    List<string> Mensaje = new List<string>();
                    var LMensaje = Mensaje.ToArray();
                    resul = new ServiceDesktop.ServiceDesktopClient().CompraModificarEstado(IdCompra, (int)ENEstado.ELIMINAR, ref LMensaje);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, tbCodigo.Text));
                    }
                    else
                    {
                        //Obtiene los codigos de productos sin stock
                        var mensajeLista = LMensaje.ToList();
                        if (mensajeLista.Count > 0)
                        {
                            var mensaje = "";
                            foreach (var item in mensajeLista)
                            {
                                mensaje = mensaje + "- " + item + "\n";
                            }
                            MP_MostrarMensajeError(mensaje);
                            return false;
                        }
                        else
                        {
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, tbCodigo.Text));
                        }
                    }
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }

        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();
            MP_AddFila();
        }

        public override void MH_Modificar()
        {
            MP_Habilitar();
            MP_AddFila();
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
                if (cbConcepto.SelectedIndex == -1)
                {
                    cbConcepto.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cbConcepto.BackColor = Color.White;
                if (cbAlmacen.SelectedIndex == -1)
                {
                    cbAlmacen.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cbAlmacen.BackColor = Color.White;
                if (cbCategoriaPrecio.SelectedIndex == -1)
                {
                    cbCategoriaPrecio.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cbCategoriaPrecio.BackColor = Color.White;
                if (_detalles.Where(a => a.NProducto != string.Empty).Count() == 0)
                {
                    _Error = true;
                    throw new Exception("El detalle se encuentra vacio");
                }
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }
        }
        #endregion

        private void Dgv_Producto_EditingCell(object sender, EditingCellEventArgs e)
        {

        }

        private void Dgv_Producto_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
