using DevComponents.DotNetBar;
using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_02;
using ENTITY.com.CompraIngreso_03.View;
using ENTITY.Libreria.View;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using PRESENTER.frm;
using PRESENTER.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UTILITY;
using UTILITY.Enum.EnCarpetas;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;

namespace PRESENTER.com
{
    public partial class F1_CompraIngreso : MODEL.ModeloF1
    {
        public F1_CompraIngreso()
        {
            InitializeComponent();
            MP_Iniciar();
            superTabControl1.SelectedTabIndex = 0;
        }
        #region Variables
        string _NombreFormulario = "COMPRA INGRESO";
        bool _Limpiar = false;
        int _idOriginal = 0;
        int _idProveedor = 10;
        int _MPos = 0;
        bool _Nuevo = false; //Especifica si es un nuevo registro o modificacion
        int _IdCompraIngresoPrecioAntiguo = 0;
        int totalMapleDetalle = 0;
        int totalMapleDevolucion = 0;
        #endregion
        #region Eventos      
        private void Dgv_GBuscador_DoubleClick(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row > -1)
            {
                superTabControl1.SelectedTabIndex = 0;
            }
        }
        private void Cb_Tipo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Cb_Tipo.Enabled == true)
                {
                    MP_CargarDetalle(Convert.ToInt32(Cb_Tipo.Value), 2);
                    MP_CargarDevolucion(Convert.ToInt32(Cb_Tipo.Value), 2);
                    MP_CargarResultado(Convert.ToInt32(Cb_Tipo.Value), 2);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Devolucion_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (Tb_NUmGranja.ReadOnly == false)
            {
                if (e.Column.Index == Dgv_Devolucion.RootTable.Columns[3].Index ||
                    e.Column.Index == Dgv_Devolucion.RootTable.Columns[4].Index ||
                    e.Column.Index == Dgv_Devolucion.RootTable.Columns[5].Index ||
                    e.Column.Index == Dgv_Devolucion.RootTable.Columns[6].Index ||
                    e.Column.Index == Dgv_Devolucion.RootTable.Columns[8].Index)
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

        private void Dgv_Resultado_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (Tb_NUmGranja.ReadOnly == false)
            {
                if (e.Column.Index == Dgv_Resultado.RootTable.Columns[3].Index ||
                    e.Column.Index == Dgv_Resultado.RootTable.Columns[4].Index ||
                    e.Column.Index == Dgv_Resultado.RootTable.Columns[5].Index ||
                    e.Column.Index == Dgv_Resultado.RootTable.Columns[6].Index ||
                    e.Column.Index == Dgv_Resultado.RootTable.Columns[8].Index)
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
        private void cb_Proveedor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Tb_FechaEnt.Enabled == true)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        if (Cb_Placa.SelectedIndex != -1)
                        {
                            var lista = new ServiceDesktop.ServiceDesktopClient().TraerProveedoresEdadSemana().Where(a => a.Id.Equals(Convert.ToInt32(cb_Proveedor.Value))).FirstOrDefault();
                            if (lista == null)
                            {
                                throw new Exception("No se encontro un proveedor");
                            }
                            _idProveedor = Convert.ToInt32(cb_Proveedor.Value);
                            Tb_Edad.Text = lista.EdadSemana;
                            Cb_Tipo.Focus();
                        }
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                    {
                        var lista = new ServiceDesktop.ServiceDesktopClient().ProveedorListarEncabezado();
                        List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 80 },
                        new GLCelda() { campo = "Descrip", visible = true, titulo = "DESCRIPCION", tamano = 150 },
                        new GLCelda() { campo = "Contacto", visible = true, titulo = "CONTACTO", tamano = 100 },
                        new GLCelda() { campo = "Ciudad", visible = false, titulo = "IDCIUDAD", tamano = 80 },
                        new GLCelda() { campo = "CiudadNombre", visible = true, titulo = "CIUDAD", tamano = 120 },
                        new GLCelda() { campo = "Telfon", visible = true, titulo = "TELEFONO", tamano = 100 },
                        new GLCelda() { campo = "EdadSemana", visible = true, titulo = "EDAD EN SEMANAS", tamano = 100 }
                    };
                        Efecto efecto = new Efecto();
                        efecto.Tipo = 3;
                        efecto.Tabla = lista;
                        efecto.SelectCol = 2;
                        efecto.listaCelda = listEstCeldas;
                        efecto.Alto = 50;
                        efecto.Ancho = 300;
                        efecto.Context = "SELECCIONE UN PROVEEDOR";
                        efecto.ShowDialog();
                        bool bandera = false;
                        bandera = efecto.Band;
                        if (bandera)
                        {
                            Janus.Windows.GridEX.GridEXRow Row = efecto.Row;
                            _idProveedor = Convert.ToInt32(Row.Cells["Id"].Value);
                            cb_Proveedor.Value = _idProveedor;
                            Tb_Edad.Text = Row.Cells["EdadSemana"].Value.ToString();
                            Cb_Tipo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void cb_Recibido_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(cb_Recibido, btnRecibido);
        }
        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }
        private void Sw_Devolucion_ValueChanged(object sender, EventArgs e)
        {
            Tap_Devolucion.Visible = Sw_Devolucion.Value ? false : true;
            Tab_Resultado.Visible = Sw_Devolucion.Value ? false : true;
            if (Dgv_Devolucion.RowCount == 0)
            {
                MP_CargarDevolucion(Convert.ToInt32(Cb_Tipo.Value), 2);
                MP_CargarResultado(Convert.ToInt32(Cb_Tipo.Value), 2);
            }
        }
        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (Tb_NUmGranja.ReadOnly == false)
            {
                if (e.Column.Index == Dgv_Detalle.RootTable.Columns[3].Index ||
                    e.Column.Index == Dgv_Detalle.RootTable.Columns[4].Index ||
                    e.Column.Index == Dgv_Detalle.RootTable.Columns[5].Index ||
                    e.Column.Index == Dgv_Detalle.RootTable.Columns[6].Index ||
                    e.Column.Index == Dgv_Detalle.RootTable.Columns[8].Index)
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
        private void Tb_TotalEnviado_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Tb_TotalVendido.Value = Tb_TotalEnviado.Value - Tb_TotalFisico.Value;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tb_Entregado.Text == string.Empty)
                {
                    throw new Exception("Debe introducir el campo ENTREGADO POR");
                }
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_PLACA));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_PLACA),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Placa.Text == "" ? "" : Cb_Placa.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Placa,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_PLACA)).ToList());
                    Cb_Placa.SelectedIndex = ((List<VLibreria>)Cb_Placa.DataSource).Count() - 1;
                }
                MP_RegistrarEntregaPlaca();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private bool MP_RegistrarEntregaPlaca()
        {
            bool result;
            VCompraIngreso_02 lista = new VCompraIngreso_02()
            {
                IdLibreria = Convert.ToInt32((Cb_Placa.Value)), //(int)ENEstaticosOrden.COMPRA_INGRESO_PLACA,
                Descripcion = Tb_Entregado.Text,
            };
            return result = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_02_Guardar(lista);
        }
        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (ValidarCantidad())
                {
                    Dgv_Detalle.UpdateData();
                    int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells[10].Value);                   
                    if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                    {
                        CalcularFila();
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO)
                        {
                            CalcularFila();
                            Dgv_Detalle.CurrentRow.Cells[10].Value = (int)ENEstado.MODIFICAR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Devolucion_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (ValidarCantidad())
                {
                    Dgv_Devolucion.UpdateData();
                    int estado = Convert.ToInt32(Dgv_Devolucion.CurrentRow.Cells[10].Value);
                    if (estado == (int)ENEstado.COMPLETADO)
                    {
                        throw new Exception("PRODUCTO COMPLETADO NO SE PUEDE  MODIFICAR");
                    }
                    if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                    {
                        CalcularFilaDevolucion();
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO)
                        {
                            CalcularFilaDevolucion();
                            Dgv_Devolucion.CurrentRow.Cells[10].Value = (int)ENEstado.MODIFICAR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private bool ValidarCantidad()
        {
            try
            {
                bool resultado = true;
                if (Tb_CantidadCajas.Value <= 0)
                {
                    resultado = false;
                    Tb_CantidadCajas.Focus();
                    throw new Exception("Debe introducir cantidades de conversión para Cajas");
                }
                if (Tb_CantidadGrupos.Value <= 0)
                {
                    resultado = false;
                    Tb_CantidadGrupos.Focus();
                    throw new Exception("Debe introducir cantidades de conversión para Grupos");
                }
                return resultado;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        private void CalcularFila()
        {
            var idProducto = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells[1].Value);
            var esCategoriaSuper = new ServiceDesktop.ServiceDesktopClient().ProductoEsCategoriaSuper(idProducto);
            //Valor conenido de 1 maple,  excepcion unica  Categoria Super = 15 unidades - 1 = Maple
            var valorContenidoDeMaple = esCategoriaSuper ? 15 : 30;

            Double caja, grupo, maple, cantidad, totalCantidadDetalle, precio, total;

            caja = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells[3].Value) * (Tb_CantidadCajas.Value * valorContenidoDeMaple);
            grupo = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells[4].Value) * (10 * valorContenidoDeMaple);
            maple = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells[5].Value) * valorContenidoDeMaple;
            cantidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells[6].Value);
            totalCantidadDetalle = caja + grupo + maple + cantidad;
            Dgv_Detalle.CurrentRow.Cells[7].Value = totalCantidadDetalle;
            precio = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells[8].Value);
            total = totalCantidadDetalle * precio;
            Dgv_Detalle.CurrentRow.Cells[9].Value = total;

            if (!Sw_Devolucion.Value)//Con devolucion        
            {
                var totalCantidadDevolucion = ((List<VCompraIngreso_03>)Dgv_Devolucion.DataSource).FirstOrDefault(a => a.IdProduc == idProducto).TotalCant;
                if (totalCantidadDetalle >= (double)totalCantidadDevolucion)
                {
                    var lResultado = ((List<VCompraIngreso_01>)Dgv_Resultado.DataSource).ToList();
                    var totalCantidad = (decimal)(totalCantidadDetalle - Convert.ToDouble(totalCantidadDevolucion));

                    lResultado.FirstOrDefault(a => a.IdProduc == idProducto).
                                                              TotalCant = totalCantidad;

                    lResultado.FirstOrDefault(a => a.IdProduc == idProducto).
                                                              Total = totalCantidad * (decimal)precio;
                    MP_ArmarDetalleResultado(lResultado);
                }
                else
                {
                    throw new Exception("La cantidad de devolución debe ser menor o igual a la cantidad del detalle");
                }
            }
            MP_ObtenerCalculo();
        }
        private void CalcularFilaDevolucion()
        {
            var idProducto = Convert.ToInt32(Dgv_Devolucion.CurrentRow.Cells[1].Value);
            var esCategoriaSuper = new ServiceDesktop.ServiceDesktopClient().ProductoEsCategoriaSuper(idProducto);
            //Valor conenido de 1 maple,  excepcion unica  Categoria Super = 15 unidades | 1 = Maple
            var valorContenidoDeMaple = esCategoriaSuper ? 15 : 30;

            Double caja, grupo, maple, cantidad, totalCantidadDevolucion, precio, total;
            caja = Convert.ToDouble(Dgv_Devolucion.CurrentRow.Cells[3].Value) * (Tb_CantidadCajas.Value * valorContenidoDeMaple);
            grupo = Convert.ToDouble(Dgv_Devolucion.CurrentRow.Cells[4].Value) * (10 * valorContenidoDeMaple);
            maple = Convert.ToDouble(Dgv_Devolucion.CurrentRow.Cells[5].Value) * valorContenidoDeMaple;
            cantidad = Convert.ToDouble(Dgv_Devolucion.CurrentRow.Cells[6].Value);
            totalCantidadDevolucion = caja + grupo + maple + cantidad;
            Dgv_Devolucion.CurrentRow.Cells[7].Value = totalCantidadDevolucion;
            precio = Convert.ToDouble(Dgv_Devolucion.CurrentRow.Cells[8].Value);
            total = totalCantidadDevolucion * precio;
            Dgv_Devolucion.CurrentRow.Cells[9].Value = total;

            if (!Sw_Devolucion.Value)//Con devolucion        
            {
                var totalCantidadDetalle = ((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).FirstOrDefault(a => a.IdProduc == idProducto).TotalCant;
                if ((double)totalCantidadDetalle >= totalCantidadDevolucion)
                {
                    var lResultado = ((List<VCompraIngreso_01>)Dgv_Resultado.DataSource).ToList();
                    var cantidadTotal = (decimal)(Convert.ToDouble(totalCantidadDetalle) - totalCantidadDevolucion);

                    lResultado.FirstOrDefault(a => a.IdProduc == idProducto).TotalCant = cantidadTotal;
                    lResultado.FirstOrDefault(a => a.IdProduc == idProducto).Total = cantidadTotal * (decimal)precio;
                    MP_ArmarDetalleResultado(lResultado);
                }
                else
                {
                    throw new Exception("La cantidad de devolución debe ser menor o igual a la cantidad del detalle");
                }
            }
            MP_ObtenerCalculo();
        }

        private void Dgv_Detalle_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
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
            catch (Exception)
            {
                MP_MostrarMensajeError(GLMensaje.ErrorNoControlado);
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
            catch (Exception)
            {
                MP_MostrarMensajeError(GLMensaje.ErrorNoControlado);
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
            catch (Exception)
            {
                MP_MostrarMensajeError(GLMensaje.ErrorNoControlado);
            }          
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = Dgv_GBuscador.RowCount - 2;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception)
            {
                MP_MostrarMensajeError(GLMensaje.ErrorNoControlado);
            }
            
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                MP_ReporteCompraIngreso(Convert.ToInt32(Tb_Cod.Text));
                var auxExisteDevolucion = Sw_Devolucion.Value ? false : true;
                MP_ImprimirNotaDevolcion(Convert.ToInt32(Tb_Cod.Text), auxExisteDevolucion);
            }
            catch (Exception)
            {
                MP_MostrarMensajeError(GLMensaje.ErrorNoControlado);
            }
           
        }
        private void Cb_Placa_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Tb_FechaEnt.Enabled == true)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        if (Cb_Placa.SelectedIndex != -1)
                        {
                            var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_02_Listar().Where(a => a.IdLibreria.Equals(Convert.ToInt32(Cb_Placa.Value))).FirstOrDefault();
                            if (lista == null)
                            {
                                throw new Exception("No se encontro la placa");
                            }
                            Tb_Entregado.Text = lista.Descripcion;
                        }
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                    {
                        var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_02_ListarTabla();
                        List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 80 },
                        new GLCelda() { campo = "IdLibreria", visible = false, titulo = "IdLibreria", tamano = 150 },
                        new GLCelda() { campo = "Libreria", visible = true, titulo = "Placa", tamano = 150 },
                        new GLCelda() { campo = "Descripcion", visible = true, titulo = "Recibido", tamano = 200 },
                    };
                        Efecto efecto = new Efecto();
                        efecto.Tipo = 3;
                        efecto.Tabla = lista;
                        efecto.SelectCol = 2;
                        efecto.listaCelda = listEstCeldas;
                        efecto.Alto = 50;
                        efecto.Ancho = 100;
                        efecto.Context = "SELECCIONE UNA PLACA";
                        efecto.ShowDialog();
                        bool bandera = false;
                        bandera = efecto.Band;
                        if (bandera)
                        {
                            Janus.Windows.GridEX.GridEXRow Row = efecto.Row;
                            Cb_Placa.Value = Convert.ToInt32(Row.Cells["IdLibreria"].Value);
                            Tb_Entregado.Text = Row.Cells["Descripcion"].Value.ToString();
                            cb_Proveedor.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Cb_Placa_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Placa, btnFacturacion);
        }
        private void Tb_CantidadCajas_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MP_RearmarDetalleSegunCantidad();
                MP_RearmarDetalleSegunCantidadDevolucion();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Tb_CantidadGrupos_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            _Nuevo = true;
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            _Nuevo = false;
        }

        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        private void Tb_IdCompraIngreso_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Tb_CompraIngresoPrecioAntoguo.ReadOnly == false)
                {
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                    {
                        var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngresoBuscar((int)ENEstado.TODOS, UTGlobal.UsuarioId);
                        List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 60 },
                        new GLCelda() { campo = "NumNota", visible = true, titulo = "N.GRANJA", tamano = 100 },
                        new GLCelda() { campo = "FechaEnt", visible = true, titulo = "FECHA ENT.", tamano = 100 },
                        new GLCelda() { campo = "FechaRec", visible = true, titulo = "FECHA REC.", tamano = 100 },
                        new GLCelda() { campo = "Placa", visible = true, titulo = "PLACA", tamano = 130 },
                        new GLCelda() { campo = "IdProvee", visible = false, titulo = "IdProvee", tamano = 100 },
                        new GLCelda() { campo = "Proveedor", visible = true, titulo = "PROVEEDOR", tamano = 240 },
                        new GLCelda() { campo = "Tipo", visible = false, titulo = "Tipo", tamano = 100 },
                        new GLCelda() { campo = "EdadSemana", visible = false, titulo = "EDAD SEMANA", tamano = 100 },
                        new GLCelda() { campo = "IdAlmacen", visible = false, titulo = "IdAlmacen", tamano = 100 },
                        new GLCelda() { campo = "TipoCompra", visible = false, titulo = "TipoCompra", tamano = 100 }
                    };
                        Efecto efecto = new Efecto();
                        efecto.Tipo = 3;
                        efecto.Tabla = lista;
                        efecto.SelectCol = 2;
                        efecto.listaCelda = listEstCeldas;
                        efecto.Alto = 50;
                        efecto.Ancho = 350;
                        efecto.Context = "SELECCIONE UNA COMPRA";
                        efecto.ShowDialog();
                        bool bandera = false;
                        bandera = efecto.Band;
                        if (bandera)
                        {
                            Janus.Windows.GridEX.GridEXRow Row = efecto.Row;
                            _IdCompraIngresoPrecioAntiguo = Convert.ToInt32(Row.Cells["Id"].Value);
                            int tipoProducto = Convert.ToInt32(Row.Cells["Tipo"].Value);
                            if (!_Nuevo)
                            {
                                if (!MP_VerificarTipoProducto(tipoProducto))
                                {
                                    return;
                                }
                            }
                            Sw_Devolucion.Value = false;
                            Cb_Tipo.Value = tipoProducto;
                            MP_RearmarDetalleAntiguo(_IdCompraIngresoPrecioAntiguo);
                            Tb_CompraIngresoPrecioAntoguo.Text = Convert.ToDateTime(Row.Cells["FechaEnt"].Value).ToString() + "- Id: " + _IdCompraIngresoPrecioAntiguo;
                            MP_ObtenerCalculo();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().TraerComprasIngreso(UTGlobal.UsuarioId).ToList();
                if (result != null)
                {
                    var query = result.Where(a => a.FechaRec >= Dt_FechaDesde.Value && a.FechaRec <= Dt_FechaHasta.Value).ToList();
                    MP_ArmarEncabezado(query);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            MP_ExportarExcel();
        }
       
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            MP_CargarEncabezado();
        }
        private void BtnExportar2_Click(object sender, EventArgs e)
        {
            MP_ExportarExcel();
        }
        #endregion

        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                MP_InicioArmarCombo();
                MP_CargarAlmacenes();
                btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
                btnFacturacion.Visible = false;
                btnRecibido.Visible = false;
                BtnExportar.Visible = true;
                MP_AsignarPermisos();
                Dt_FechaDesde.Value = DateTime.Now.Date;
                Dt_FechaHasta.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
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
        private void MP_ExportarExcel()
        {
            UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Reporte, ENSubCarpetas.ReportesCompraIngreso);
            string ubicacion = Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Reporte, ENSubCarpetas.ReportesCompraIngreso);
            if (MP_ArmarExcel(ubicacion, ENArchivoNombre.CompraIngreso))
            {
                MP_MostrarMensajeExito(GLMensaje.ExportacionExitosa);
            }
            else
            {
                MP_MostrarMensajeError(GLMensaje.ExportacionErronea);
            }
        }
        private void MP_CargarEncabezado()
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().TraerComprasIngreso(UTGlobal.UsuarioId).ToList();
                MP_ArmarEncabezado(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private bool MP_ArmarExcel(string ubicacion, string nombreArchivo)
        {
            try
            {
                string archivo, linea;                       
                linea = "";
                archivo =nombreArchivo + DateTime.Now.Day + "." +
                        DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year + "." + DateTime.Now.Date.Hour + "." +
                        DateTime.Now.Date.Minute + "." + DateTime.Now.Date.Second + ".csv";
                archivo= Path.Combine(ubicacion, archivo);
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
                linea = linea.Substring(0,linea.Length - 1);
           
                escritor.WriteLine(linea);
                linea = "";
                foreach (GridEXRow fila in Dgv_GBuscador.GetRows())
                {
                    foreach (GridEXColumn columna in Dgv_GBuscador.RootTable.Columns)
                    {
                        if (columna.Visible)
                        {
                            string data;
                            if (columna.Key.ToString() == "FechaRec" || columna.Key.ToString()=="FechaEnt")
                            {
                                data = Convert.ToDateTime(fila.Cells[columna.Key].Value).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                data = fila.Cells[columna.Key].Value.ToString();
                            } 
                            data = data.Replace(";", ",");
                            linea = linea + data + ";";
                        }
                    }
                    linea = linea.Substring(0,linea.Length - 1);
                    escritor.WriteLine(linea);
                    linea = "";
                }
                escritor.Close();
                Efecto efecto = new Efecto();
                efecto.archivo = archivo;
                efecto.Tipo = 1;
                efecto.Context = "Su archivo ha sido Guardado en la ruta: " + archivo + " DESEA ABRIR EL ARCHIVO?";
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
        void MP_ArmarEncabezado(List<VCompraIngreso> lCompraIngreso)
        {
            Dgv_GBuscador.DataSource = lCompraIngreso;
            Dgv_GBuscador.RetrieveStructure();
            Dgv_GBuscador.AlternatingColors = true;

            Dgv_GBuscador.RootTable.Columns["id"].Caption = "Nota Recep.";
            Dgv_GBuscador.RootTable.Columns["id"].Width = 80;
            Dgv_GBuscador.RootTable.Columns["id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["id"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["id"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["NotaProveedor"].Caption = "Nota Provee.";
            Dgv_GBuscador.RootTable.Columns["NotaProveedor"].Width = 70;
            Dgv_GBuscador.RootTable.Columns["NotaProveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["NotaProveedor"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["NotaProveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["NotaProveedor"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["Proveedor"].Caption = "Proveedor";
            Dgv_GBuscador.RootTable.Columns["Proveedor"].Width = 120;
            Dgv_GBuscador.RootTable.Columns["Proveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_GBuscador.RootTable.Columns["Proveedor"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["FechaRec"].Caption = "Fecha Rec.";
            Dgv_GBuscador.RootTable.Columns["FechaRec"].Width = 75;
            Dgv_GBuscador.RootTable.Columns["FechaRec"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["FechaRec"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["FechaRec"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["FechaRec"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["FechaEnt"].Caption = "Fecha Ent.";
            Dgv_GBuscador.RootTable.Columns["FechaEnt"].Width = 75;
            Dgv_GBuscador.RootTable.Columns["FechaEnt"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["FechaEnt"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["FechaEnt"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["FechaEnt"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["Entregado"].Caption = "Entregado";
            Dgv_GBuscador.RootTable.Columns["Entregado"].Width = 120;
            Dgv_GBuscador.RootTable.Columns["Entregado"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Entregado"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["Entregado"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_GBuscador.RootTable.Columns["Entregado"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["PlacaDescripcion"].Caption = "Placa";
            Dgv_GBuscador.RootTable.Columns["PlacaDescripcion"].Width = 80;
            Dgv_GBuscador.RootTable.Columns["PlacaDescripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["PlacaDescripcion"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["PlacaDescripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_GBuscador.RootTable.Columns["PlacaDescripcion"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["TipoCategoria"].Caption = "Tipo";
            Dgv_GBuscador.RootTable.Columns["TipoCategoria"].Width = 80;
            Dgv_GBuscador.RootTable.Columns["TipoCategoria"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["TipoCategoria"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["TipoCategoria"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_GBuscador.RootTable.Columns["TipoCategoria"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["TipoCompra"].Caption = "Tipo Compra";
            Dgv_GBuscador.RootTable.Columns["TipoCompra"].Width = 90;
            Dgv_GBuscador.RootTable.Columns["TipoCompra"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["TipoCompra"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["TipoCompra"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["TipoCompra"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["Estado"].Caption = "Seleccionado";
            Dgv_GBuscador.RootTable.Columns["Estado"].Width = 90;
            Dgv_GBuscador.RootTable.Columns["Estado"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Estado"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["Estado"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Estado"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["Devolucion"].Caption = "Devolución";
            Dgv_GBuscador.RootTable.Columns["Devolucion"].Width = 80;
            Dgv_GBuscador.RootTable.Columns["Devolucion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Devolucion"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["Devolucion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Devolucion"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["TotalMaple"].Caption = "Total Maple";
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].FormatString = "0";
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].Width = 90;
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].AggregateFunction = AggregateFunction.Sum;
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_GBuscador.RootTable.Columns["TotalMaple"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].Caption = "Total Uni.";
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].FormatString = "0.00";
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].Width = 90;
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].AggregateFunction = AggregateFunction.Sum;
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_GBuscador.RootTable.Columns["TotalUnidades"].Visible = true;

            Dgv_GBuscador.RootTable.Columns["Total"].Caption = "Total";
            Dgv_GBuscador.RootTable.Columns["Total"].FormatString = "0.00";
            Dgv_GBuscador.RootTable.Columns["Total"].Width = 90;
            Dgv_GBuscador.RootTable.Columns["Total"].AggregateFunction = AggregateFunction.Sum;
            Dgv_GBuscador.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.FontSize = 8;
            Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_GBuscador.RootTable.Columns["Total"].Visible = false;


            Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;

            Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;

            Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

            //Habilitar filtradores
            Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            Dgv_GBuscador.FilterMode = FilterMode.Automatic;
            Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            Dgv_GBuscador.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
            //Diseno
            Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
            Dgv_GBuscador.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            Dgv_GBuscador.AlternatingColors = true;
            //Dgv_GBuscador.RecordNavigator = true;
            Dgv_GBuscador.GroupByBoxVisible = false;
            //Totalizadoe
            Dgv_GBuscador.TotalRow = InheritableBoolean.True;
            Dgv_GBuscador.TotalRowFormatStyle.FontBold = TriState.True;
            Dgv_GBuscador.TotalRowFormatStyle.BackColor = Color.Gold;
            Dgv_GBuscador.TotalRowPosition = TotalRowPosition.BottomScrollable;
        }
        private void MP_ReporteCompraIngreso(int idCompra)
        {
            if (Tb_Cod.ReadOnly == true)
            {
                try
                {
                    if (idCompra == 0)
                    {
                        throw new Exception("No existen registros");
                    }
                    if (UTGlobal.visualizador != null)
                    {
                        UTGlobal.visualizador.Close();
                    }
                    UTGlobal.visualizador = new Visualizador();
                    var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_NotaXId(idCompra);
                    if (lista != null)
                    {
                        var ObjetoReport = new RCompraIngreso();
                        ObjetoReport.SetDataSource(lista);
                        UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
                        ObjetoReport.SetParameterValue("Titulo", "RECEPCIÓN SELECCIÓN");
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
        }
        private void MP_ReporteNotaDevolucion(int idCompra)
        {
            if (Tb_Cod.ReadOnly == true)
            {
                try
                {
                    if (idCompra == 0)
                    {
                        throw new Exception("No existen registros");
                    }
                    if (UTGlobal.visualizador != null)
                    {
                        UTGlobal.visualizador.Close();
                    }
                    UTGlobal.visualizador = new Visualizador();
                    var lista = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_DevolucionNotaXId(idCompra);
                    if (lista != null)
                    {
                        var ObjetoReport = new RCompraIngreso_Devolucion();
                        ObjetoReport.SetDataSource(lista);
                        UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
                        UTGlobal.visualizador.ShowDialog();
                        UTGlobal.visualizador.BringToFront();
                    }
                    else
                        throw new Exception("No se encontraron registros");

                    if (UTGlobal.visualizador != null)
                    {
                        UTGlobal.visualizador.Close();
                    }
                    UTGlobal.visualizador = new Visualizador();
                    var listaResultado = new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ResultadoNotaXId(idCompra);
                    if (lista != null)
                    {
                        var ObjetoReport = new RCompraIngreso();
                        ObjetoReport.SetDataSource(listaResultado);
                        ObjetoReport.SetParameterValue("Titulo", "RESULTADO DE RECEPCIÓN");
                        UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
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
        }
        private void MP_CargarDetalle(int id, int tipo)
        {
            try
            {
                List<VCompraIngreso_01> lresult = new List<VCompraIngreso_01>();
                if (tipo == 1)
                {
                    //Consulta segun un Id de Ingreso
                    lresult = new ServiceDesktop.ServiceDesktopClient().CmmpraIngreso_01ListarXId(id).ToList();
                }
                else
                {
                    int ValorTipo = id;
                    //Consulta segun un Categoria 
                    lresult = new ServiceDesktop.ServiceDesktopClient().CmmpraIngreso_01ListarXId2(ValorTipo, Convert.ToInt32(Cb_Almacen.Value)).ToList();
                }

                MP_ArmarDetalle(lresult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }

        }
        private void MP_CargarDevolucion(int id, int tipo)
        {
            try
            {
                List<VCompraIngreso_03> lresult = new List<VCompraIngreso_03>();
                if (tipo == 1)
                {
                    //Consulta segun un Id de Ingreso
                    lresult = new ServiceDesktop.ServiceDesktopClient().TraerDevolucionCompraIngreso_03(id).ToList();
                }
                else
                {
                    int ValorTipo = id;
                    //Consulta segun un Categoria 
                    lresult = new ServiceDesktop.ServiceDesktopClient().TraerDevolucionTipoProductoCompraIngreso_03(ValorTipo, Convert.ToInt32(Cb_Almacen.Value)).ToList();
                }

                MP_ArmarDevolucion(lresult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }

        }
        private void MP_CargarResultado(int id, int tipo)
        {
            try
            {
                List<VCompraIngreso_01> lresult = new List<VCompraIngreso_01>();
                if (tipo == 1)
                {
                    //Consulta segun un Id de Ingreso
                    lresult = new ServiceDesktop.ServiceDesktopClient().CmmpraIngreso_01ListarXId(id).ToList();
                }
                else
                {
                    int ValorTipo = id;
                    //Consulta segun un Categoria 
                    lresult = new ServiceDesktop.ServiceDesktopClient().CmmpraIngreso_01ListarXId2(ValorTipo, Convert.ToInt32(Cb_Almacen.Value)).ToList();
                }
                MP_ArmarDetalleResultado(lresult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }

        }
        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void btnRecibido_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_RECIBIDO));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_RECIBIDO),
                    IdLibrer = idLibreria + 1,
                    Descrip = cb_Recibido.Text == "" ? "" : cb_Recibido.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(cb_Recibido,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_RECIBIDO)).ToList());
                    cb_Recibido.SelectedIndex = ((List<VLibreria>)cb_Recibido.DataSource).Count() - 1;
                }
                MP_RegistrarEntregaPlaca();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ArmarDetalle(List<VCompraIngreso_01> lresult)
        {
            try
            {
                //DataTable result = ListaATabla(lresult);
                Dgv_Detalle.DataSource = lresult;
                Dgv_Detalle.RetrieveStructure();
                Dgv_Detalle.AlternatingColors = true;

                Dgv_Detalle.RootTable.Columns[0].Key = "id";
                Dgv_Detalle.RootTable.Columns[0].Visible = false;

                Dgv_Detalle.RootTable.Columns[1].Key = "IdProduc";
                Dgv_Detalle.RootTable.Columns[1].Visible = false;

                Dgv_Detalle.RootTable.Columns[2].Key = "Producto";
                Dgv_Detalle.RootTable.Columns[2].Caption = "PRODUCTO";
                Dgv_Detalle.RootTable.Columns[2].Width = 130;
                Dgv_Detalle.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[2].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns[2].Visible = true;

                Dgv_Detalle.RootTable.Columns[3].Key = "Caja";
                Dgv_Detalle.RootTable.Columns[3].Caption = "CAJA";
                Dgv_Detalle.RootTable.Columns[3].FormatString = "0";
                Dgv_Detalle.RootTable.Columns[3].Width = 90;
                Dgv_Detalle.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[3].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[3].Visible = true;

                Dgv_Detalle.RootTable.Columns[4].Key = "Grupo";
                Dgv_Detalle.RootTable.Columns[4].Caption = "GRUPO";
                Dgv_Detalle.RootTable.Columns[4].FormatString = "0";
                Dgv_Detalle.RootTable.Columns[4].Width = 90;
                Dgv_Detalle.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[4].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[4].Visible = true;

                Dgv_Detalle.RootTable.Columns[5].Key = "Maple";
                Dgv_Detalle.RootTable.Columns[5].Caption = "MAPLE";
                Dgv_Detalle.RootTable.Columns[5].FormatString = "0";
                Dgv_Detalle.RootTable.Columns[5].Width = 90;
                Dgv_Detalle.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[5].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[5].Visible = true;

                Dgv_Detalle.RootTable.Columns[6].Key = "Cantidad";
                Dgv_Detalle.RootTable.Columns[6].Caption = "UNIDADES";
                Dgv_Detalle.RootTable.Columns[6].FormatString = "0";
                Dgv_Detalle.RootTable.Columns[6].Width = 90;
                Dgv_Detalle.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[6].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[6].Visible = true;

                Dgv_Detalle.RootTable.Columns[7].Key = "TotalCant";
                Dgv_Detalle.RootTable.Columns[7].Caption = "TOTAL U.";
                Dgv_Detalle.RootTable.Columns[7].FormatString = "0";
                Dgv_Detalle.RootTable.Columns[7].Width = 110;
                Dgv_Detalle.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[7].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[7].Visible = true;

                Dgv_Detalle.RootTable.Columns[8].Key = "PrecioCost";
                Dgv_Detalle.RootTable.Columns[8].Caption = "PRECIO";
                Dgv_Detalle.RootTable.Columns[8].FormatString = "0.0000";
                Dgv_Detalle.RootTable.Columns[8].Width = 90;
                Dgv_Detalle.RootTable.Columns[8].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[8].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[8].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[8].Visible = true;

                Dgv_Detalle.RootTable.Columns[9].Key = "Total";
                Dgv_Detalle.RootTable.Columns[9].Caption = "TOTAL BS";
                Dgv_Detalle.RootTable.Columns[9].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns[9].Width = 110;
                Dgv_Detalle.RootTable.Columns[9].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns[9].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns[9].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns[9].Visible = true;


                Dgv_Detalle.RootTable.Columns[10].Key = "Estado";
                Dgv_Detalle.RootTable.Columns[10].Visible = false;

                Dgv_Detalle.RootTable.Columns[11].Key = "TotalMaple";
                Dgv_Detalle.RootTable.Columns[11].Visible = false;

                Dgv_Detalle.RootTable.Columns[12].Key = "IdCompra";
                Dgv_Detalle.RootTable.Columns[12].Visible = false;

                //Habilitar filtradores              
                //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_Detalle.GroupByBoxVisible = false;
                Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_ArmarDetalleResultado(List<VCompraIngreso_01> lresult)
        {
            try
            {
                //DataTable result = ListaATabla(lresult);
                Dgv_Resultado.DataSource = lresult;
                Dgv_Resultado.RetrieveStructure();
                Dgv_Resultado.AlternatingColors = true;

                Dgv_Resultado.RootTable.Columns[0].Key = "id";
                Dgv_Resultado.RootTable.Columns[0].Visible = false;

                Dgv_Resultado.RootTable.Columns[1].Key = "IdProduc";
                Dgv_Resultado.RootTable.Columns[1].Visible = false;

                Dgv_Resultado.RootTable.Columns[2].Key = "Producto";
                Dgv_Resultado.RootTable.Columns[2].Caption = "PRODUCTO";
                Dgv_Resultado.RootTable.Columns[2].Width = 350;
                Dgv_Resultado.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[2].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Resultado.RootTable.Columns[2].Visible = true;

                Dgv_Resultado.RootTable.Columns[3].Key = "Caja";
                Dgv_Resultado.RootTable.Columns[3].Caption = "CAJA";
                Dgv_Resultado.RootTable.Columns[3].FormatString = "0";
                Dgv_Resultado.RootTable.Columns[3].Width = 100;
                Dgv_Resultado.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[3].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[3].Visible = false;

                Dgv_Resultado.RootTable.Columns[4].Key = "Grupo";
                Dgv_Resultado.RootTable.Columns[4].Caption = "GRUPO";
                Dgv_Resultado.RootTable.Columns[4].FormatString = "0";
                Dgv_Resultado.RootTable.Columns[4].Width = 100;
                Dgv_Resultado.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[4].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[4].Visible = false;

                Dgv_Resultado.RootTable.Columns[5].Key = "Maple";
                Dgv_Resultado.RootTable.Columns[5].Caption = "MAPLE";
                Dgv_Resultado.RootTable.Columns[5].FormatString = "0";
                Dgv_Resultado.RootTable.Columns[5].Width = 100;
                Dgv_Resultado.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[5].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[5].Visible = false;

                Dgv_Resultado.RootTable.Columns[6].Key = "Cantidad";
                Dgv_Resultado.RootTable.Columns[6].Caption = "UNIDADES";
                Dgv_Resultado.RootTable.Columns[6].FormatString = "0";
                Dgv_Resultado.RootTable.Columns[6].Width = 90;
                Dgv_Resultado.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[6].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[6].Visible = false;

                Dgv_Resultado.RootTable.Columns[7].Key = "TotalCant";
                Dgv_Resultado.RootTable.Columns[7].Caption = "TOTAL U.";
                Dgv_Resultado.RootTable.Columns[7].FormatString = "0";
                Dgv_Resultado.RootTable.Columns[7].Width = 150;
                Dgv_Resultado.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[7].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[7].Visible = true;

                Dgv_Resultado.RootTable.Columns[8].Key = "PrecioCost";
                Dgv_Resultado.RootTable.Columns[8].Caption = "PRECIO";
                Dgv_Resultado.RootTable.Columns[8].FormatString = "0.0000";
                Dgv_Resultado.RootTable.Columns[8].Width = 150;
                Dgv_Resultado.RootTable.Columns[8].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[8].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[8].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[8].Visible = true;

                Dgv_Resultado.RootTable.Columns[9].Key = "Total";
                Dgv_Resultado.RootTable.Columns[9].Caption = "TOTAL BS";
                Dgv_Resultado.RootTable.Columns[9].FormatString = "0.00";
                Dgv_Resultado.RootTable.Columns[9].Width = 150;
                Dgv_Resultado.RootTable.Columns[9].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Resultado.RootTable.Columns[9].CellStyle.FontSize = 9;
                Dgv_Resultado.RootTable.Columns[9].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Resultado.RootTable.Columns[9].Visible = true;


                Dgv_Resultado.RootTable.Columns[10].Key = "Estado";
                Dgv_Resultado.RootTable.Columns[10].Visible = false;

                //Habilitar filtradores              
                //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_Resultado.GroupByBoxVisible = false;
                Dgv_Resultado.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }

        }

        private void MP_ArmarDevolucion(List<VCompraIngreso_03> lresult)
        {
            try
            {
                //DataTable result = ListaATabla(lresult);
                Dgv_Devolucion.DataSource = lresult;
                Dgv_Devolucion.RetrieveStructure();
                Dgv_Devolucion.AlternatingColors = true;

                Dgv_Devolucion.RootTable.Columns[0].Key = "id";
                Dgv_Devolucion.RootTable.Columns[0].Visible = false;

                Dgv_Devolucion.RootTable.Columns[1].Key = "IdProduc";
                Dgv_Devolucion.RootTable.Columns[1].Visible = false;

                Dgv_Devolucion.RootTable.Columns[2].Key = "Producto";
                Dgv_Devolucion.RootTable.Columns[2].Caption = "PRODUCTO";
                Dgv_Devolucion.RootTable.Columns[2].Width = 130;
                Dgv_Devolucion.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[2].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Devolucion.RootTable.Columns[2].Visible = true;

                Dgv_Devolucion.RootTable.Columns[3].Key = "Caja";
                Dgv_Devolucion.RootTable.Columns[3].Caption = "CAJA";
                Dgv_Devolucion.RootTable.Columns[3].FormatString = "0";
                Dgv_Devolucion.RootTable.Columns[3].Width = 90;
                Dgv_Devolucion.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[3].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[3].Visible = true;

                Dgv_Devolucion.RootTable.Columns[4].Key = "Grupo";
                Dgv_Devolucion.RootTable.Columns[4].Caption = "GRUPO";
                Dgv_Devolucion.RootTable.Columns[4].FormatString = "0";
                Dgv_Devolucion.RootTable.Columns[4].Width = 90;
                Dgv_Devolucion.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[4].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[4].Visible = true;

                Dgv_Devolucion.RootTable.Columns[5].Key = "Maple";
                Dgv_Devolucion.RootTable.Columns[5].Caption = "MAPLE";
                Dgv_Devolucion.RootTable.Columns[5].FormatString = "0";
                Dgv_Devolucion.RootTable.Columns[5].Width = 90;
                Dgv_Devolucion.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[5].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[5].Visible = true;

                Dgv_Devolucion.RootTable.Columns[6].Key = "Cantidad";
                Dgv_Devolucion.RootTable.Columns[6].Caption = "UNIDADES";
                Dgv_Devolucion.RootTable.Columns[6].FormatString = "0";
                Dgv_Devolucion.RootTable.Columns[6].Width = 90;
                Dgv_Devolucion.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[6].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[6].Visible = true;

                Dgv_Devolucion.RootTable.Columns[7].Key = "TotalCant";
                Dgv_Devolucion.RootTable.Columns[7].Caption = "TOTAL U.";
                Dgv_Devolucion.RootTable.Columns[7].FormatString = "0";
                Dgv_Devolucion.RootTable.Columns[7].Width = 110;
                Dgv_Devolucion.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[7].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[7].Visible = true;

                Dgv_Devolucion.RootTable.Columns[8].Key = "PrecioCost";
                Dgv_Devolucion.RootTable.Columns[8].Caption = "PRECIO";
                Dgv_Devolucion.RootTable.Columns[8].FormatString = "0.0000";
                Dgv_Devolucion.RootTable.Columns[8].Width = 90;
                Dgv_Devolucion.RootTable.Columns[8].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[8].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[8].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[8].Visible = true;

                Dgv_Devolucion.RootTable.Columns[9].Key = "Total";
                Dgv_Devolucion.RootTable.Columns[9].Caption = "TOTAL BS";
                Dgv_Devolucion.RootTable.Columns[9].FormatString = "0.00";
                Dgv_Devolucion.RootTable.Columns[9].Width = 110;
                Dgv_Devolucion.RootTable.Columns[9].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Devolucion.RootTable.Columns[9].CellStyle.FontSize = 9;
                Dgv_Devolucion.RootTable.Columns[9].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Devolucion.RootTable.Columns[9].Visible = true;


                Dgv_Devolucion.RootTable.Columns[10].Key = "Estado";
                Dgv_Devolucion.RootTable.Columns[10].Visible = false;

                Dgv_Devolucion.RootTable.Columns[11].Key = "TotalMaple";
                Dgv_Devolucion.RootTable.Columns[11].Visible = false;

                Dgv_Devolucion.RootTable.Columns[12].Key = "IdCompra";
                Dgv_Devolucion.RootTable.Columns[12].Visible = false;

                //Habilitar filtradores              
                //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_Devolucion.GroupByBoxVisible = false;
                Dgv_Devolucion.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_InicioArmarCombo()
        {
            try
            {
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarCombo(Cb_Tipo,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2)).ToList());
                //UTGlobal.MG_ArmarCombo(Cb_Placa,
                //                      new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                //                                                                                    Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_PLACA)).ToList());
                UTGlobal.MG_ArmarMultiComboPlaca(Cb_Placa,
                                     new ServiceDesktop.ServiceDesktopClient().CompraIngreso_02_Listar().ToList());

                UTGlobal.MG_ArmarComboProveedores(cb_Proveedor,
                                   new ServiceDesktop.ServiceDesktopClient().TraerProveedoresEdadSemana().ToList(), ENEstado.NOCARGARPRIMERFILA);


                UTGlobal.MG_ArmarCombo(cb_Recibido,
                                     new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.COMPRA_INGRESO),
                                                                                                   Convert.ToInt32(ENEstaticosOrden.COMPRA_INGRESO_RECIBIDO)).ToList());

            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
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
        public static DataTable ListaATabla(List<VCompraIngreso_01> lista)
        {
            DataTable tabla = new DataTable();
            //Crear la Estructura de la Tabla a partir de la Lista de Objetos
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                tabla.Columns.Add(propiedades[i].Name, propiedades[i].PropertyType);
            }
            //Llenar la Tabla desde la Lista de Objetos
            DataRow fila = null;
            for (int i = 0; i < lista.Count; i++)
            {
                propiedades = lista[i].GetType().GetProperties();
                fila = tabla.NewRow();
                for (int j = 0; j < propiedades.Length; j++)
                {
                    fila[j] = propiedades[j].GetValue(lista[i], null);
                }
                tabla.Rows.Add(fila);
            }
            return (tabla);
        }
        private void MP_Habilitar()
        {
            try
            {
                Tb_NUmGranja.ReadOnly = false;
                Cb_Placa.ReadOnly = false;
                Cb_Tipo.ReadOnly = false;
                Cb_Almacen.ReadOnly = false;
                cb_Proveedor.ReadOnly = false;
                Tb_Observacion.ReadOnly = false;
                Sw_Tipo.IsReadOnly = false;
                Tb_Entregado.ReadOnly = false;
                Tb_FechaEnt.Enabled = true;
                Tb_FechaRec.Enabled = true;
                cb_Recibido.ReadOnly = false;

                Dgv_Detalle.Enabled = true;
                Tb_CantidadCajas.IsInputReadOnly = false;
                Tb_CantidadGrupos.IsInputReadOnly = false;
                Tb_CompraIngresoPrecioAntoguo.ReadOnly = false;
                Sw_Devolucion.IsReadOnly = false;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InHabilitar()
        {
            try
            {
                Tb_Cod.ReadOnly = true;
                Tb_NUmGranja.ReadOnly = true;
                Cb_Placa.ReadOnly = true;
                Cb_Tipo.ReadOnly = true;
                Cb_Almacen.ReadOnly = true;
                cb_Proveedor.ReadOnly = true;
                Tb_Observacion.ReadOnly = true;
                Tb_Observacion.ReadOnly = true;
                Tb_Entregado.ReadOnly = true;
                Tb_Edad.ReadOnly = true;
                Sw_Tipo.IsReadOnly = true;
                Tb_FechaEnt.Enabled = false;
                Tb_FechaRec.Enabled = false;
                _Limpiar = false;
                cb_Recibido.ReadOnly = true;
                Dgv_Detalle.Enabled = false;
                Tb_TotalMaples.IsInputReadOnly = true;
                Tb_TotalVendido.IsInputReadOnly = true;
                Tb_TotalFisico.IsInputReadOnly = true;
                Tb_TPrecio.IsInputReadOnly = true;
                Tb_TSaldoTo.IsInputReadOnly = true;
                Tb_CantidadCajas.IsInputReadOnly = true;
                Tb_CantidadGrupos.IsInputReadOnly = true;
                Tb_CompraIngresoPrecioAntoguo.ReadOnly = true;
                Sw_Devolucion.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_Limpiar()
        {
            try
            {
                Tb_Cod.Clear();
                Tb_NUmGranja.Clear();
                Tb_Observacion.Clear();
                Tb_Observacion.Clear();
                Tb_Entregado.Clear();
                Tb_FechaEnt.Value = DateTime.Now.Date;
                Tb_FechaRec.Value = DateTime.Now.Date;
                Dt_FechaDesde.Value = DateTime.Now.Date;
                Dt_FechaHasta.Value = DateTime.Now.Date;
                Tb_Edad.Clear();
                Tb_CompraIngresoPrecioAntoguo.Clear();
                Sw_Tipo.Value = false;
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo(Cb_Tipo);
                    UTGlobal.MG_SeleccionarComboPlaca(Cb_Placa);
                    UTGlobal.MG_SeleccionarCombo(cb_Recibido);
                    UTGlobal.MG_SeleccionarComboProveedor(cb_Proveedor);
                    // UTGlobal.MG_SeleccionarCombo(Cb_Almacen);
                }
                MP_CargarDetalle(Convert.ToInt32(Cb_Tipo.Value), 2);
                MP_CargarDevolucion(Convert.ToInt32(Cb_Tipo.Value), 2);
                MP_CargarResultado(Convert.ToInt32(Cb_Tipo.Value), 2);
                Tb_TotalVendido.Value = 0;
                Tb_TotalEnviado.Value = 0;
                Tb_TotalMaples.Value = 0;
                Tb_TotalFisico.Value = 0;
                Tb_TPrecio.Value = 0;
                Tb_TSaldoTo.Value = 0;
                Tb_CantidadCajas.Value = 12;
                Tb_CantidadGrupos.Value = 11;
                MP_LimpiarColor();
                Sw_Devolucion.Value = true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }

        }
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                if (_Pos < Dgv_GBuscador.RowCount - 1)
                {
                    Dgv_GBuscador.Row = _Pos;
                    _idOriginal = (int)Dgv_GBuscador.GetValue("id");
                    if (_idOriginal != 0)
                    {
                        var registro = new ServiceDesktop.ServiceDesktopClient().TraerCompraIngreso(_idOriginal);
                        if (registro != null)
                        {
                            Tb_Cod.Text = registro.Id.ToString();
                            Cb_Almacen.Value = registro.IdAlmacen;
                            Tb_NUmGranja.Text = registro.NumNota.ToString();
                            Tb_FechaEnt.Value = registro.FechaEnt; //registro.FechaEnt;
                            Tb_FechaRec.Value = registro.FechaRec;
                            Cb_Placa.Value = registro.Placa;
                            _idProveedor = registro.IdProvee;
                            cb_Proveedor.Value = _idProveedor;
                            Tb_Observacion.Text = registro.Observacion;
                            Cb_Tipo.Value = registro.Tipo;
                            cb_Recibido.Value = registro.Recibido;
                            Tb_Edad.Text = registro.CantidadSemanas;
                            Tb_Entregado.Text = registro.Entregado;
                            Tb_TotalEnviado.Value = Convert.ToDouble(registro.TotalRecibido);
                            Tb_TotalVendido.Value = Convert.ToDouble(registro.TotalVendido);
                            Tb_TotalFisico.Value = Convert.ToDouble(registro.Total);
                            Sw_Tipo.Value = registro.TipoCompra == 1 ? true : false;
                            Tb_CantidadCajas.Value = Convert.ToDouble(registro.CantidadCaja);
                            Tb_CantidadGrupos.Value = Convert.ToDouble(registro.CantidadGrupo);
                            Tb_CompraIngresoPrecioAntoguo.Text = registro.CompraAntiguaFecha;
                            Tb_TotalMaples.Value = registro.TotalMaple;
                            MP_CargarDetalle(Convert.ToInt32(Tb_Cod.Text), 1);
                            MP_CargarDevolucion(Convert.ToInt32(Tb_Cod.Text), 1);
                            MP_CargarResultado(Convert.ToInt32(Tb_Cod.Text), 1);
                            Sw_Devolucion.Value = registro.Devolucion == 1 ? true : false;
                            MP_ObtenerCalculoResultado();
                            MP_ObtenerCalculo();
                        }
                        LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Convert.ToString(Dgv_GBuscador.RowCount - 1);
                    }
                }

            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ObtenerCalculoResultado()
        {
            if (!Sw_Devolucion.Value)
            {
                var lDetalle = ((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).ToList();
                var lDevolucion = ((List<VCompraIngreso_03>)Dgv_Devolucion.DataSource).ToList();
                var lResultado = ((List<VCompraIngreso_01>)Dgv_Resultado.DataSource).ToList();
                foreach (var fila1 in lDetalle)
                {
                    if (lDevolucion.Count(a => a.IdProduc == fila1.IdProduc) > 0)
                    {
                        var devolucion = lDevolucion.Where(a => a.IdProduc == fila1.IdProduc).FirstOrDefault();

                        if (lResultado.Count(a => a.IdProduc == fila1.IdProduc) > 0)
                        {
                            var resultado = lResultado.Where(a => a.IdProduc == fila1.IdProduc).FirstOrDefault();

                            resultado.TotalCant = fila1.TotalCant - devolucion.TotalCant;
                            resultado.Total = resultado.TotalCant * resultado.PrecioCost;

                            var index = lResultado.IndexOf(lResultado.FirstOrDefault(a => a.IdProduc == devolucion.IdProduc));
                            lResultado.RemoveAt(index);
                            lResultado.Insert(index, resultado);
                        }
                    }
                }
                MP_ArmarDetalleResultado(lResultado);
            }
        }
        private void MP_ObtenerCalculo()
        {
            try
            {
                Dgv_Detalle.UpdateData();
                double totalUnidadesDevolucion, sumaCajaDevolucion, sumaGrupoDevolucion, sumaMapleDevolucion, sumaUnidadesDevolucion,
                       totalCajaDevolucion, totalGrupoDevolucion;
                //double totalMapleDevolucion = 0;
                //Devolucion
                if (!Sw_Devolucion.Value)
                {
                    Tb_TotalFisico.Value = Convert.ToDouble(Dgv_Resultado.GetTotal(Dgv_Resultado.RootTable.Columns[7], AggregateFunction.Sum));
                    Tb_TSaldoTo.Value = Convert.ToDouble(Dgv_Resultado.GetTotal(Dgv_Resultado.RootTable.Columns[9], AggregateFunction.Sum));
                    Tb_TPrecio.Value = Tb_TSaldoTo.Value / Tb_TotalFisico.Value;
                    Tb_TotalEnviado.Value = Tb_TotalFisico.Value;

                    //Calculo de maples para devolucion
                    sumaCajaDevolucion = Convert.ToDouble(Dgv_Devolucion.GetTotal(Dgv_Devolucion.RootTable.Columns[3], AggregateFunction.Sum));
                    sumaGrupoDevolucion = Convert.ToDouble(Dgv_Devolucion.GetTotal(Dgv_Devolucion.RootTable.Columns[4], AggregateFunction.Sum));
                    sumaMapleDevolucion = Convert.ToDouble(Dgv_Devolucion.GetTotal(Dgv_Devolucion.RootTable.Columns[5], AggregateFunction.Sum));
                    sumaUnidadesDevolucion = Convert.ToDouble(Dgv_Devolucion.GetTotal(Dgv_Devolucion.RootTable.Columns[6], AggregateFunction.Sum));

                    totalCajaDevolucion = sumaCajaDevolucion * Tb_CantidadCajas.Value;
                    totalGrupoDevolucion = sumaGrupoDevolucion * Tb_CantidadGrupos.Value;
                    totalUnidadesDevolucion = sumaUnidadesDevolucion != 0 ?
                                             (sumaUnidadesDevolucion > 30 ? (sumaUnidadesDevolucion / 300) * Tb_CantidadGrupos.Value : 1) :
                                             0;
                    totalMapleDevolucion = Convert.ToInt32(totalCajaDevolucion + totalGrupoDevolucion + sumaMapleDevolucion + totalUnidadesDevolucion);
                }
                else
                {
                    Tb_TotalFisico.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns[7], AggregateFunction.Sum));
                    Tb_TSaldoTo.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns[9], AggregateFunction.Sum));
                    Tb_TPrecio.Value = Tb_TSaldoTo.Value / Tb_TotalFisico.Value;
                    Tb_TotalEnviado.Value = Tb_TotalFisico.Value;
                }
                //Calculo de MAPLES

                var sumaCaja = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns[3], AggregateFunction.Sum));
                var sumaGrupo = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns[4], AggregateFunction.Sum));
                var sumaMaple = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns[5], AggregateFunction.Sum));
                var sumaUnidades = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns[6], AggregateFunction.Sum));

                var totalCaja = sumaCaja * Tb_CantidadCajas.Value;
                var totalGrupo = sumaGrupo * Tb_CantidadGrupos.Value;
                var totalUnidades = sumaUnidades != 0 ?
                                   (sumaUnidades > 30 ? (sumaUnidades / 300) * Tb_CantidadGrupos.Value : 1) :
                                   0;
                totalMapleDetalle = Convert.ToInt32(totalCaja + totalGrupo + sumaMaple + totalUnidades);
                //TotalMaple
                Tb_TotalMaples.Value = Sw_Devolucion.Value ? totalMapleDetalle : totalMapleDetalle - totalMapleDevolucion;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_Filtrar(int tipo)
        {
            try
            {
                MP_CargarEncabezado();
                if (Dgv_GBuscador.RowCount > 0)
                {
                    //_MPos = 0;
                    MP_MostrarRegistro(tipo == 1 ? 0 : Dgv_GBuscador.Row);
                    MP_LimpiarColor();
                }
                else
                {
                    MP_Limpiar();
                    LblPaginacion.Text = "0/0";
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
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
        private void MP_RearmarDetalleSegunCantidad()
        {
            if (Dgv_Detalle.RowCount > 0)
            {
                Dgv_Detalle.Update();
                var Detalle = ((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).ToList();
                foreach (var fila in Detalle)
                {
                    if (fila.Grupo != 0 || fila.Caja != 0 || fila.Maple != 0 || fila.Cantidad != 0)
                    {
                        var esCategoriaSuper = new ServiceDesktop.ServiceDesktopClient().ProductoEsCategoriaSuper(fila.IdProduc);

                        //Valor conenido de 1 maple,  excepcion unica  Categoria Super = 15 unidades en 1 Maple
                        var valorContenidoDeMaple = esCategoriaSuper ? 15 : 30;

                        fila.TotalCant = (fila.Caja * (Convert.ToInt32(Tb_CantidadCajas.Value * valorContenidoDeMaple))) + (fila.Grupo * (10 * valorContenidoDeMaple)) + (fila.Maple * valorContenidoDeMaple) + fila.Cantidad;
                        fila.Total = fila.TotalCant * fila.PrecioCost;
                        if (fila.Estado == (int)ENEstado.GUARDADO)
                        {
                            fila.Estado = (int)ENEstado.MODIFICAR;
                        }
                    }
                }
                MP_ArmarDetalle(Detalle);
                MP_ObtenerCalculo();
            }
        }
        private void MP_RearmarDetalleSegunCantidadDevolucion()
        {
            if (Dgv_Devolucion.RowCount > 0)
            {
                Dgv_Devolucion.Update();
                var Detalle = ((List<VCompraIngreso_03>)Dgv_Devolucion.DataSource).ToList();
                foreach (var fila in Detalle)
                {
                    if (fila.Grupo != 0 || fila.Caja != 0 || fila.Maple != 0 || fila.Cantidad != 0)
                    {
                        var esCategoriaSuper = new ServiceDesktop.ServiceDesktopClient().ProductoEsCategoriaSuper(fila.IdProduc);

                        //Valor conenido de 1 maple,  excepcion unica  Categoria Super = 15 unidades en 1 Maple
                        var valorContenidoDeMaple = esCategoriaSuper ? 15 : 30;

                        fila.TotalCant = (fila.Caja * (Convert.ToInt32(Tb_CantidadCajas.Value * valorContenidoDeMaple))) + (fila.Grupo * (10 * valorContenidoDeMaple)) + (fila.Maple * valorContenidoDeMaple) + fila.Cantidad;
                        fila.Total = fila.TotalCant * fila.PrecioCost;
                        if (fila.Estado == (int)ENEstado.GUARDADO)
                        {
                            fila.Estado = (int)ENEstado.MODIFICAR;
                        }
                    }
                }
                MP_ArmarDevolucion(Detalle);
                //MP_ObtenerCalculo();
            }
        }
        private void MP_RearmarDetalleAntiguo(int IdCompraingreso)
        {
            try
            {
                //Consulta segun un Id de Ingreso
                var lCompraIngresoDetallePrecio = new ServiceDesktop.ServiceDesktopClient().CmmpraIngreso_01ListarXId(IdCompraingreso).ToList();
                var lCompraIngresoDetalle = ((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).ToList();
                foreach (var vCompraIngresoDetalle in lCompraIngresoDetalle)
                {
                    foreach (var vCompraIngresoDetallePrecio in lCompraIngresoDetallePrecio)
                    {
                        if (vCompraIngresoDetalle.IdProduc == vCompraIngresoDetallePrecio.IdProduc)
                        {
                            vCompraIngresoDetalle.PrecioCost = vCompraIngresoDetallePrecio.PrecioCost;
                            vCompraIngresoDetalle.Total = vCompraIngresoDetalle.PrecioCost * vCompraIngresoDetalle.TotalCant;
                            vCompraIngresoDetalle.Estado = (int)ENEstado.MODIFICAR;
                            break;
                        }
                    }
                }
                MP_ArmarDetalle(lCompraIngresoDetalle);
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private bool MP_VerificarTipoProducto(int tipoProducto)
        {
            try
            {
                if (Convert.ToInt32(Cb_Tipo.Value) != tipoProducto)
                {
                    throw new Exception("El tipo de producto es distinto, seleccione otra compra");
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

        #region Metodo heredados
        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {
                string mensaje = "";
                VCompraIngresoLista CompraIngreso = new VCompraIngresoLista()
                {
                    IdAlmacen = Convert.ToInt32(Cb_Almacen.Value),
                    IdProvee = _idProveedor,
                    estado = (int)ENEstado.GUARDADO,
                    NumNota = Tb_NUmGranja.Text,
                    FechaEnt = Tb_FechaEnt.Value,
                    FechaRec = Tb_FechaRec.Value,
                    Placa = Convert.ToInt32(Cb_Placa.Value),
                    CantidadSemanas = Tb_Edad.Text,
                    Tipo = Convert.ToInt32(Cb_Tipo.Value),
                    Observacion = Tb_Observacion.Text,
                    Entregado = Tb_Entregado.Text,
                    Recibido = Convert.ToInt32(cb_Recibido.Value),
                    TotalRecibido = Convert.ToDecimal(Tb_TotalEnviado.Value),
                    TotalVendido = Convert.ToDecimal(Tb_TotalVendido.Value),
                    TipoCompra = Sw_Tipo.Value == true ? 1 : 2,
                    Total = Convert.ToDecimal(Tb_TSaldoTo.Value),
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                    CantidadCaja = Convert.ToInt32(Tb_CantidadCajas.Value),
                    CantidadGrupo = Convert.ToInt32(Tb_CantidadGrupos.Value),
                    CompraAntiguaFecha = Tb_CompraIngresoPrecioAntoguo.Text,
                    TotalMaple = Convert.ToInt32(Tb_TotalMaples.Value),
                    Devolucion = Sw_Devolucion.Value == true ? 1 : 2,
                    TotalUnidades = Convert.ToInt32(Tb_TotalFisico.Value),
                };
                var auxImprimirDevolucion = Sw_Devolucion.Value ? false : true;
                int id = Tb_Cod.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Cod.Text);
                int idAux = id;
                var vCompraIngreso_01 = ((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).ToArray<VCompraIngreso_01>();
                var vCompraIngreso_03 = ((List<VCompraIngreso_03>)Dgv_Devolucion.DataSource).ToArray<VCompraIngreso_03>();
                resultado = new ServiceDesktop.ServiceDesktopClient().GuardarCompraIngreso(CompraIngreso, vCompraIngreso_01, ref id,
                                                                                            Sw_Devolucion.Value, vCompraIngreso_03, totalMapleDetalle, totalMapleDevolucion);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        MP_ReporteCompraIngreso(id);
                        MP_ImprimirNotaDevolcion(id, auxImprimirDevolucion);
                        Tb_NUmGranja.Focus();
                        MP_CargarEncabezado();
                        MP_Filtrar(1);
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                    }
                    else//Modificar
                    {
                        MP_ReporteCompraIngreso(id);
                        MP_ImprimirNotaDevolcion(id, auxImprimirDevolucion);
                        MP_Filtrar(2);
                        MP_InHabilitar();//El formulario
                        _Limpiar = true;
                       // MH_Habilitar();//El menu  
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                                       
                    }
                }
                //Resultado
                if (resultado)
                {
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                {
                    mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return resultado;
            }
        }
        private void MP_ImprimirNotaDevolcion(int id, bool auxImprimirDevolucion)
        {
            try
            {
                if (auxImprimirDevolucion)
                {
                    if (MP_DeseaImprimir())
                    {
                        MP_ReporteNotaDevolucion(id);
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        public bool MP_DeseaImprimir()
        {
            Efecto efecto = new Efecto();
            efecto.Tipo = 1;
            efecto.Context = GLMensaje.Pregunta_Imprimir.ToUpper() + "LA NOTA DE DEVOLUCIÓN?";
            efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
            efecto.ShowDialog();
            var resultado = efecto.Band;
            return resultado;
        }
        public override bool MH_Eliminar()
        {
            try
            {
                int idCompra = Convert.ToInt32(Tb_Cod.Text);
                if (new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ExisteEnSeleccion(idCompra))
                {
                    MP_MostrarMensajeError("La compra esta asociado a una Seleccion.");
                    return false;
                }
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
                    resul = new ServiceDesktop.ServiceDesktopClient().ModificarEstadoCompraIngreso(idCompra, (int)ENEstado.ELIMINAR, ref LMensaje, Sw_Devolucion.Value);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, idCompra.ToString()));
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
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, idCompra.ToString()));
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

        }
        public override void MH_Modificar()
        {
            int IdSeleccion = Convert.ToInt32(Tb_Cod.Text);
            if (new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ExisteEnSeleccion(IdSeleccion))
            {
                throw new Exception("La compra esta asociado a una Seleccion.");
            }
            MP_Habilitar();
        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(1);

        }
        public void MP_LimpiarColor()
        {
            Tb_NUmGranja.BackColor = Color.White;
            cb_Proveedor.BackColor = Color.White;
            Cb_Tipo.BackColor = Color.White;
            Cb_Almacen.BackColor = Color.White;
            Cb_Placa.BackColor = Color.White;
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {
                if (Tb_NUmGranja.Text == "")
                {
                    Tb_NUmGranja.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Tb_NUmGranja.BackColor = Color.White;
                if (Tb_Edad.Text == string.Empty)
                {
                    _Error = true;
                    throw new Exception("Debe seleccionar un proveedor Con la tecla Enter");
                }

                if (cb_Proveedor.SelectedIndex == -1)
                {
                    cb_Proveedor.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cb_Proveedor.BackColor = Color.White;
                if (Cb_Tipo.SelectedIndex == -1)
                {
                    Cb_Tipo.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Tipo.BackColor = Color.White;
                if (Cb_Almacen.SelectedIndex == -1)
                {
                    Cb_Almacen.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen.BackColor = Color.White;
                if (Cb_Placa.SelectedIndex == -1)
                {
                    Cb_Placa.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen.BackColor = Color.White;
                if (cb_Recibido.SelectedIndex == -1)
                {
                    _Error = true;
                    throw new Exception("Debe seleccionar Entregado Por");
                }
                if (Tb_Entregado.Text == string.Empty)
                {
                    _Error = true;
                    throw new Exception("Debe seleccionar un chofer");
                }
                if (((List<VCompraIngreso_01>)Dgv_Detalle.DataSource).Count() == 0)
                {
                    _Error = true;
                    throw new Exception("El detalle se encuentra vacio no se puede Guardar");
                }
                if (Tb_TotalFisico.Value == 0)
                {
                    _Error = true;
                    throw new Exception("Detalle de compra en 0");
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

       
    }
}
