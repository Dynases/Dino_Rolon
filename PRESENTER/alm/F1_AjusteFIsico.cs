using DevComponents.DotNetBar;
using ENTITY.inv.Almacen.View;
using ENTITY.inv.Concepto.View;
using ENTITY.inv.TI001.VIew;
using ENTITY.Libreria.View;
using ENTITY.Producto.View;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using PRESENTER.frm;
using PRESENTER.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UTILITY;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;
using UTILITY.MetodosExtencion;
using Ent = ENTITY.inv.Ajuste.View.VAjusteFisico;
using EntDet = ENTITY.inv.Ajuste.View.VAjusteFisicoProducto;
using EntList = ENTITY.inv.Ajuste.View.VAjusteLista;
namespace PRESENTER.alm
{
    public partial class F1_AjusteFIsico : MODEL.ModeloF1
    {
        public F1_AjusteFIsico()
        {
            InitializeComponent();
            MP_Iniciar();
        }
        #region Variables
        string _NombreFormulario = "AJSUTE";
        int _MPos = 0;
        Ent _ajuste = new Ent();
        List<EntDet> _detalles = new List<EntDet>();
        List<EntList> _listado = new List<EntList>();
        List<VProductoListaStock> _productos = new List<VProductoListaStock>();
        VProductoListaStock _producto = new VProductoListaStock();
        List<VTI001> _lotes = new List<VTI001>();
        #endregion
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                vAjusteFisicoBindingSource.DataSource = _ajuste;
                this.Text = _NombreFormulario;
                LblTitulo.Text = _NombreFormulario;
                MP_ArmarCombos();
                MP_ArmarGrillas();
                MP_InHabilitar();
                MP_AsignarPermisos();
                btnTransportadoPorr.Visible = false;
                //MP_Limpiar(); //go-dev revisar
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
            //MP_CargarCategoriaPrecio();
            MP_CargarTransportadoPor();
        }

        private void MP_ArmarGrillas()
        {
            MP_ArmarGrillaListado();
            MP_ArmarGrillaDetalle();
            MP_ArmarGrillaBusquedaProducto();
        }
        public void MP_CargarCliente()
        {
            try
            {
                var conceptos = new ServiceDesktop.ServiceDesktopClient().TraerClienteCombo().ToList().ToList();
                UTGlobal.MG_ArmarComboClientes(cb_Cliente, conceptos, ENEstado.NOCARGARPRIMERFILA);
                cb_Cliente.SelecionarPrimero();
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        public void MP_CargarConcepto()
        {
            try
            {
                var conceptos = new ServiceDesktop.ServiceDesktopClient().Concepto_ListaComboAjuste().ToList();
                UTGlobal.MG_ArmarComboConcepto(cbConcepto, conceptos);
                cbConcepto.SelecionarPrimero();
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        public void MP_CargarTransportadoPor()
        {
            try
            {
                var trasportadoPor = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo((int)ENEstaticosGrupo.TRASPASO,
                                                            (int)ENEstaticosOrden.TRASPASO_TRASPASADOPOR).ToList();
                UTGlobal.MG_ArmarCombo(Cb_TransportePor, trasportadoPor);
                Cb_TransportePor.SelecionarPrimero();
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
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                UTGlobal.MG_ArmarComboAlmacen(cbAlmacen, almacenes);
                cbAlmacen.SelecionarPrimero();
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
                List<EntList> ListaCompleta = new ServiceDesktop.ServiceDesktopClient().AjusteFisico_Lista().ToList(); //go-dev revizar
                Dgv_GBuscador.ConfigInicialVinculado(ListaCompleta, "Ajuste");


                Dgv_GBuscador.ColNoVisible(nameof(EntList.IdCliente));
                Dgv_GBuscador.ColNoVisible(nameof(EntList.IdTransportadoPor));

                Dgv_GBuscador.ColAL(nameof(EntList.Id), "Código", 100);
                Dgv_GBuscador.ColAL(nameof(EntList.Fecha), "Fecha", 100);
                Dgv_GBuscador.ColAL(nameof(EntList.NConcepto), "Concepto", 300);
                Dgv_GBuscador.ColAL(nameof(EntList.Obs), "Observación", 150);
                Dgv_GBuscador.ColAL(nameof(EntList.NAlmacen), "Alamacen", 500);

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
                dgjDetalle.ColNoVisible(nameof(EntDet.Estado));
                dgjDetalle.ColNoVisible(nameof(EntDet.Precio));
                dgjDetalle.ColNoVisible(nameof(EntDet.IdProducto));
                dgjDetalle.ColNoVisible(nameof(EntDet.Lote));
                dgjDetalle.ColNoVisible(nameof(EntDet.FechaVen));

                dgjDetalle.ColAL(nameof(EntDet.CodProducto), "Código", 80);
                dgjDetalle.ColAL(nameof(EntDet.NProducto), "Producto", 150);
                dgjDetalle.ColARNro(nameof(EntDet.Saldo), "Saldo", 90, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Fisico), "Fisico", 90, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Diferencia), "Diferencia", 90, "0.00");
                dgjDetalle.ColARNro(nameof(EntDet.Total), "Total", 90, "0.00");


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
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.UnidadVenta));
                dgjProducto.ColNoVisible(nameof(VProductoListaStock.EsMateriaPrima));

                dgjProducto.ColAL(nameof(VProductoListaStock.CodigoProducto), "Código", 60);
                dgjProducto.ColAL(nameof(VProductoListaStock.Producto), "Producto", 150);
                dgjProducto.ColAL(nameof(VProductoListaStock.Division), "División", 70);                

                dgjProducto.ColARNro(nameof(VProductoListaStock.PrecioCosto), "P. Costo", 60, "0.00");
                dgjProducto.ColARNro(nameof(VProductoListaStock.Stock), "Stock", 90, "0.00");
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
                    var listado = servicio.AjusteFisico_Lista().ToList();
                    if (listado == null)
                        listado = new List<EntList>();
                    _listado.Clear();
                    _listado.AddRange(listado);
                    MP_ArmarGrillaListado();
                    //Dgv_GBuscador.Refetch();
                    //Dgv_GBuscador.Refresh();
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
                    var detalles = servicio.AjusteFisicoDetalle_Lista(id).ToList();
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
            //cbCategoriaPrecio.ReadOnly = false;
            dgjDetalle.Enabled = true;
            Cb_TransportePor.ReadOnly = false;
        }

        private void MP_InHabilitar()
        {
            tbCodigo.ReadOnly = true;
            dtiFecha.IsInputReadOnly = true;
            dtiFecha.ButtonDropDown.Enabled = false;
            cbConcepto.ReadOnly = true;
            cbAlmacen.ReadOnly = true;
            tbObs.ReadOnly = true;
            //cbCategoriaPrecio.ReadOnly = true;
            dgjDetalle.Enabled = true;
            Cb_TransportePor.ReadOnly = true;
        }

        private void MP_Limpiar()
        {
            try
            {
                var conceptos = (List<VConceptoCombo>)cbConcepto.DataSource;
                var almacenes = (List<VAlmacenCombo>)cbAlmacen.DataSource;
                //var categoriaPrecio = (List<VPrecioCategoria>)cbCategoriaPrecio.DataSource;
                var trasportadoPor = (List<VLibreria>)Cb_TransportePor.DataSource;                
                _ajuste.Id = 0;
                _ajuste.FechaReg = DateTime.Today;
                _ajuste.IdConcepto = conceptos.First().Id;
                _ajuste.IdAlmacen = almacenes.First().IdLibreria;
                _ajuste.Observacion = string.Empty;
                _ajuste.TransportadorPor = trasportadoPor.First().IdLibreria;
                _ajuste.IdCliente = 0;
                _ajuste.Estado = (int)ENEstado.GUARDADO;
                vAjusteFisicoBindingSource.ResetCurrentItem();
                MP_LimpiarDetalle();
                MP_LimpiarColor();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        public void MP_LimpiarDetalle()
        {
            _detalles = _detalles.Where(t => t.IdView == "0").ToList();
            dgjDetalle.Refetch();
            MP_ArmarGrillaDetalle();
        }
        public void MP_LimpiarColor()
        {
            cbConcepto.BackColor = Color.White;
            cbAlmacen.BackColor = Color.White;
            //cbCategoriaPrecio.BackColor = Color.White;
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
                    //var categoriaPrecio = (List<VPrecioCategoria>)cbCategoriaPrecio.DataSource;
                    _ajuste.Id = ajuste.Id;
                    _ajuste.FechaReg = ajuste.Fecha;
                    _ajuste.IdConcepto = conceptos.First(a => a.Descripcion == ajuste.NConcepto).Id;
                    _ajuste.IdAlmacen = almacenes.First(a => a.Descripcion == ajuste.NAlmacen).IdLibreria;                  
                    _ajuste.Observacion = ajuste.Obs;
                    _ajuste.TransportadorPor = ajuste.IdTransportadoPor;
                    _ajuste.IdCliente = ajuste.IdCliente;
                    vAjusteFisicoBindingSource.ResetCurrentItem();
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
                var itemVacio = _detalles.Where(a => a.Fisico == 0 && a.NProducto == string.Empty).Count();
                if (itemVacio > 0 && _detalles.Count > 0)
                {
                    return;
                }              
                var nuevo = new EntDet()
                {
                    Id = 0,
                    IdAjuste = 0,
                    IdProducto = 0,
                    CodProducto = string.Empty,
                    NProducto = string.Empty,               
                    Saldo = 0,
                    Fisico = 0,
                    Precio = 0,
                    Total = 0,
                    Lote = UTGlobal.lote,
                    FechaVen = UTGlobal.fechaVencimiento,
                    Estado = (int)ENEstado.NUEVO                  
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
                //MP_SeleccionarColumna();
                _producto = new VProductoListaStock();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_SeleccionarColumna()
        {
            dgjDetalle.Col = dgjDetalle.RootTable.Columns[nameof(EntDet.Fisico)].Index;
            dgjDetalle.Row = dgjDetalle.RowCount - 1;
            dgjDetalle.Select();
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
        private void MP_Reporte(int idAjuste)
        {

            try
            {
                if (idAjuste == 0)
                {
                    throw new Exception("No existen registros");
                }
                if (UTGlobal.visualizador != null)
                {
                    UTGlobal.visualizador.Close();
                }
                UTGlobal.visualizador = new Visualizador();
                var lista = new ServiceDesktop.ServiceDesktopClient().ReporteAjuste(idAjuste).ToList();
                if (lista != null)
                {
                    var ObjetoReport = new RAjusteTicket();
                    ObjetoReport.SetDataSource(lista);
                    UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
                    ObjetoReport.SetParameterValue("Titulo", "AJUSTE");
                    UTGlobal.visualizador.ShowDialog();
                    UTGlobal.visualizador.BringToFront();
                }
                else
                    throw new Exception("No se encontraron registros");


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
                    if (dgjDetalle.GetValue("NProducto").ToString() != string.Empty && dgjDetalle.GetValue("IdProducto").ToString() != string.Empty
                        && dgjDetalle.GetValue("Fisico").ToString() != string.Empty)
                    {
                        MP_AddFila();
                        MP_InsertarProducto();
                        dgjProducto.Select();
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
                decimal saldo = dgjDetalle.CelValor<decimal>(nameof(EntDet.Saldo));
                decimal fisico = dgjDetalle.CelValor<decimal>(nameof(EntDet.Fisico));
                decimal precio = dgjDetalle.CelValor<decimal>(nameof(EntDet.Precio));

                decimal diferencia = fisico - saldo;
                decimal total = fisico < saldo ? (diferencia * -1) * precio : diferencia * precio;

                var idView = dgjDetalle.CelValorIdView();
                var item = _detalles.Where(a => a.IdView == idView).First();
                item.Diferencia = diferencia;
                item.Total = total;
                dgjDetalle.UpdateData();
                //dgjDetalle.Refetch();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_SeleccionProductoPorConceptoIngreso()
        {
            try
            {
                int idProducto = dgjProducto.CelValor<int>(nameof(VProductoListaStock.IdProducto));
                _producto = _productos.Where(p => p.IdProducto == idProducto).FirstOrDefault();
                string idView = dgjDetalle.CelValorIdView();
                MP_IngresarProductoDetalleIngreso(idView);
                dgjDetalle.Refetch();
                //MP_InHabilitarProducto();
                MP_ObtenerCalculo();
                MP_SeleccionarColumna();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_SeleccionProductoPorConceptoSalida()
        {
            try
            {
                string idView = dgjDetalle.CelValorIdView();
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    int idLote = 0;
                    int idProducto = dgjProducto.CelValor<int>(nameof(VProductoListaStock.IdProducto));
                    if (_producto.IdProducto == 0)
                    {
                        _producto = _productos.Where(p => p.IdProducto == idProducto).FirstOrDefault();
                        if (_producto.Stock <= 0)
                        {
                            _producto = new VProductoListaStock();
                            throw new Exception("El producto " + _producto.Producto + "no cuenta con STOCK disponible.");
                        }
                        var lotes = servicio.TraerInventarioLotes(_producto.IdProducto,
                                                                  Convert.ToInt32(cbAlmacen.Value)).ToList();
                        if (_producto.EsLote == 2)
                        {
                            idLote = lotes.FirstOrDefault().id;
                            if (idLote == 0)
                            {
                                throw new Exception("El producto no tiene lote relacionado");
                            }
                            MP_IngresarProductoDetalleSalida(idView, idLote, lotes);                          
                            //MP_InHabilitarProducto();
                            MP_ObtenerCalculo();                           
                            dgjDetalle.Refetch();
                            MP_SeleccionarColumna();
                            return;
                        }
                        MP_ActualizarLote(ref lotes, idProducto);
                        _lotes.Clear();
                        _lotes.AddRange(lotes);
                        MP_ArmarGrillaBusquedaLotes();
                        GPanel_Producto.Text = _producto.Producto;                        
                    }
                    else
                    {
                        idLote = dgjProducto.CelValor<int>(nameof(VTI001.id));
                        var lLotes = (List<VTI001>)dgjProducto.DataSource;
                        MP_IngresarProductoDetalleSalida(idView, idLote, lLotes);
                        dgjDetalle.Refetch();
                        MP_CargarProductos();
                        MP_SeleccionarColumna();
                        //MP_InHabilitarProducto();
                    }
                }
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_ObtenerCalculo()
        {
            try
            {               
                Tb_Total.Value = (Double)_detalles.Sum(x => x.Total);
        
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ActualizarLote(ref List<VTI001> Lotes, int idProducto)
        {
            foreach (var fila in Lotes)
            {
                var sumaCantidad = _detalles.Where(a => a.Lote.Equals(fila.Lote) &&
                                                              a.FechaVen == fila.FechaVen &&
                                                              a.IdProducto == idProducto &&
                                                              a.Estado == 0).Sum(a => a.Fisico);
                fila.Cantidad = fila.Cantidad - sumaCantidad;
            }
        }
        private void MP_IngresarProductoDetalleSalida(string idView, int idLote, List<VTI001> lotes)
        {
            if (lotes.Count != 0)
            {
                foreach (var detallle in _detalles)
                {
                    if (_producto.IdProducto == detallle.IdProducto &&
                       lotes.FirstOrDefault(a => a.id == idLote).Lote == detallle.Lote &&
                       lotes.FirstOrDefault(a => a.id == idLote).FechaVen == detallle.FechaVen)
                    {
                        _producto = new VProductoListaStock();
                        throw new Exception("El producto ya existe en el detalle");
                    }
                }
                var fila = _detalles.Where(a => a.IdView == idView).First();
                fila.IdProducto = _producto.IdProducto;
                fila.CodProducto = _producto.CodigoProducto;
                fila.NProducto = _producto.Producto;
                fila.Saldo =_producto.Stock.Value;
                fila.Fisico = 0;
                fila.Diferencia = 0;
                fila.Precio = _producto.PrecioCosto;
                fila.Total =0;
                fila.Lote = lotes.FirstOrDefault(a => a.id == idLote).Lote;
                fila.FechaVen = lotes.FirstOrDefault(a => a.id == idLote).FechaVen;
            
            }
            _producto = new VProductoListaStock();
        }
        private void MP_IngresarProductoDetalleIngreso(string idView)
        {
            foreach (var detallle in _detalles)
            {
                if (_producto.IdProducto == detallle.IdProducto)
                {
                    _producto = new VProductoListaStock();
                    throw new Exception("El producto ya existe en el detalle");
                }
            }
            var fila = _detalles.Where(a => a.IdView == idView).First();
            fila.IdProducto = _producto.IdProducto;
            fila.CodProducto = _producto.CodigoProducto;
            fila.NProducto = _producto.Producto;
            fila.Saldo = _producto.Stock.Value; 
            fila.Fisico = 0;
            fila.Diferencia = 0;
            fila.Precio = _producto.PrecioCosto;
            fila.Total = 0;
            fila.Lote = UTGlobal.lote;
            fila.FechaVen = UTGlobal.fechaVencimiento;
          

            _producto = new VProductoListaStock();
        }
        private void MP_SeleccionarButtonCombo(MultiColumnCombo combo, ButtonX btn)
        {
            try
            {
                if (combo.SelectedIndex < 0 && combo.Text != string.Empty)
                {
                    btn.Visible = true;
                }
                else
                {
                    btn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void Dgv_Producto_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion
        #region Metodo heredados

        public override bool MH_NuevoRegistro()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var idAuxiliar = _ajuste.Id;
                    var idAjuste = servicio.AjusteFisico_Guardar(_ajuste, _detalles.Where(a => a.NProducto != string.Empty).ToArray(), TxtNombreUsu.Text);
                    MP_Reporte(idAjuste);
                    if (idAuxiliar == 0)
                    {
                        MP_Filtrar(1);
                        MP_Limpiar();                       
                    }
                    else
                    {
                        MP_Filtrar(2);
                        this.MP_InHabilitar();
                        MH_Habilitar();//El menu 
                    }
                    MP_InHabilitarProducto();
                    string mensaje = idAuxiliar == 0 ? GLMensaje.Nuevo_Exito("AJUSTE", idAjuste.ToString()) :
                                                       GLMensaje.Modificar_Exito("AJUSTE", idAjuste.ToString());
                    MP_MostrarMensajeExito(mensaje);
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
                    new ServiceDesktop.ServiceDesktopClient().AjusteFisico_Eliminar(IdCompra);
                    MP_Filtrar(1);
                    MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, tbCodigo.Text));
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
            MP_Filtrar(2);
            MP_InHabilitarProducto();
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
                //if (cbCategoriaPrecio.SelectedIndex == -1)
                //{
                //    cbCategoriaPrecio.BackColor = Color.Red;
                //    _Error = true;
                //}
                //else
                //    cbCategoriaPrecio.BackColor = Color.White;
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
        #region  Eventos


        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {

            try
            {
                _MPos = 0;
                this.MP_MostrarRegistro(_MPos);
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
                if (_MPos > 0)
                {
                    _MPos -= 1;
                    this.MP_MostrarRegistro(_MPos);
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
                if (_MPos < Dgv_GBuscador.RowCount - 1)
                {
                    _MPos += 1;
                    this.MP_MostrarRegistro(_MPos);
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
                _MPos = Dgv_GBuscador.RowCount - 1;
                this.MP_MostrarRegistro(_MPos);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void dgjDetalle_EditingCell(object sender, Janus.Windows.GridEX.EditingCellEventArgs e)
        {
            try
            {
                if (tbObs.ReadOnly == false)
                {
                    if (e.Column.Index == dgjDetalle.RootTable.Columns[nameof(EntDet.Fisico)].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns[nameof(EntDet.Lote)].Index ||
                        e.Column.Index == dgjDetalle.RootTable.Columns[nameof(EntDet.FechaVen)].Index)
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

        private void dgjDetalle_CellEdited(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
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
                MP_ObtenerCalculo();
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
                    if (e.KeyData == Keys.Enter)
                    {
                        MP_VerificarSeleccion("CodProducto");
                        MP_VerificarSeleccion("NProducto");
                        MP_VerificarSeleccion("Fisico");
                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter &&
                        dgjDetalle.Row >= 0)
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
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Cb_TransportePor_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_TransportePor, btnTransportadoPorr);
        }

        private void btnTransportadoPor_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.TRASPASO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.TRASPASO_TRASPASADOPOR));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.TRASPASO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.TRASPASO_TRASPASADOPOR),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_TransportePor.Text == "" ? "" : Cb_TransportePor.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_TransportePor,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.TRASPASO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.TRASPASO_TRASPASADOPOR)).ToList());
                    Cb_TransportePor.SelectedIndex = ((List<VLibreria>)Cb_TransportePor.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void cbConcepto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var conceptos = (List<VConceptoCombo>)cbConcepto.DataSource;
                if (conceptos != null && _ajuste.IdConcepto != 0)
                {
                    if (conceptos.Where(a => a.Id == Convert.ToInt32(cbConcepto.Value)).FirstOrDefault().AjusteCliente == 2)
                    {
                        cb_Cliente.Visible = true;
                        lblCliente.Visible = true;
                        MP_CargarCliente();
                    }
                    else
                    {
                        cb_Cliente.Visible = false;
                        lblCliente.Visible = false;
                    }                  
                }               
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }            
        }
        #endregion

        private void F1_AjusteFIsico_Load(object sender, EventArgs e)
        {

        }

        private void GPanel_Detalle_Click(object sender, EventArgs e)
        {

        }

        private void dgjProducto_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                e.Cancel = true;
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
                        var conceptos = (List<VConceptoCombo>)cbConcepto.DataSource;
                        if (conceptos != null && _ajuste.IdConcepto != 0)
                        {   
                            if (conceptos.Where(a => a.Id == Convert.ToInt32(cbConcepto.Value) && a.TipoMovimiento != (int)ENConcepto.CONCEPTO_TIPO_AJUSTE).Count() > 0)
                            {
                                MP_SeleccionProductoPorConceptoSalida();
                            }
                            else
                            {
                                MP_SeleccionProductoPorConceptoIngreso();
                            }                            
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

        private void cbAlmacen_ValueChanged(object sender, EventArgs e)
        {
            if (tbObs.ReadOnly == false)
            {
                MP_LimpiarDetalle();
                MP_AddFila();
                MP_InHabilitarProducto();
            }               
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (tbObs.ReadOnly == true)
            {
                MP_Reporte(_ajuste.Id);
            }
            
        }

        private void BtnAtras_Click(object sender, EventArgs e)
        {

        }
    }

}
