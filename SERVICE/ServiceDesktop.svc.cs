using ENTITY.Cliente.View;
using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.Seleccion.View;
using ENTITY.com.Seleccion_01.View;
using ENTITY.inv.Almacen.View;
using ENTITY.inv.Sucursal.View;
using ENTITY.inv.TipoAlmacen.view;
using ENTITY.inv.Transformacion.Report;
using ENTITY.inv.Transformacion.View;
using ENTITY.inv.Transformacion_01.View;
using ENTITY.inv.Traspaso.View;
using ENTITY.Libreria.View;
using ENTITY.Plantilla;
using ENTITY.Producto.View;
using ENTITY.Proveedor.View;
using ENTITY.reg.Precio.View;
using ENTITY.reg.PrecioCategoria.View;
using ENTITY.ven.view;
using LOGIC.Class;
using System;
using System.Collections.Generic;
using System.Data;
using ENTITY.com.CompraIngreso_02;
using UTILITY.Enum;
using ENTITY.DiSoft.Zona;
using LOGIC.Class.DiSoft;
using ENTITY.Rol.View;
using ENTITY.Usuario.View;
using ENTITY.com.CompraIngreso_03.View;
using ENTITY.com.CompraIngreso.Filter;
using ENTITY.com.Seleccion.Report;

namespace SERVICE
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IServiceDesktop
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        /********** ROL ***************/
        #region Rol 

        public bool RolGuardar(VRol vRol, List<VRol_01> detalle, ref int IdRol,  string usuario)
        {
            try
            {
                var listResult = new LRol().Guardar(vRol, detalle, ref IdRol,  usuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RolEliminar(int IdRol, List<VRol_01> detalle)
        {
            try
            {
                var listResult = new LRol().EliminarRol(IdRol, detalle);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VRol> ListarRol()
        {
            try
            {
                var listResult = new LRol().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VRol_01> ListarDetalleRol_01(int IdRol)
        {
            try
            {
                var listResult = new LRol_01().Listar(IdRol);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable AsignarPermisos(string idRol, string NombreProg)
        {
            try
            {
                DataTable dt  = new LRol().AsignarPermisos(idRol, NombreProg);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RolExisteEnUsuario(int IdRol)
        {
            try
            {
                var listResult = new LRol().ExisteEnUsuario(IdRol);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VRol_01> ListarDetalle(int IdRol, int IdModulo)
        {
            try
            {
                var listResult = new LRol_01().ListaDetalle(IdRol, IdModulo);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /********** USUARIO ***************/
        #region Usuario 

        public bool UsuarioGuardar(VUsuario vUsuario, List<VUsuario_01> detalle, ref int IdUsuario, string usuario)
        {
            try
            {
                var listResult = new LUsuario().Guardar(vUsuario, detalle, ref IdUsuario, usuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UsuarioEliminar(int IdUsuario, List<VUsuario_01> detalle)
        {
            try
            {
                var listResult = new LUsuario().EliminarUsuario(IdUsuario, detalle);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VUsuario> ListarUsuario()
        {
            try
            {
                var listResult = new LUsuario().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VUsuario_01> ListarDetalleUsuario_01(int IdUsuario)
        {
            try
            {
                var listResult = new LUsuario_01().Listar(IdUsuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VUsuario> ValidarUsuario(string user, string password)
        {
            try
            {
                var listResult = new LUsuario().ValidarUsuario(user,password);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /********** CLIENTE ***************/
        #region Cliente
        public List<VCliente> ClienteListar()
        {
            try
            {
                var listResult = new LCliente().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ClienteGuardar(VCliente cliente, ref int id)
        {
            try
            {
                var result = new LCliente().Guardar(cliente, ref id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   
        public bool ClienteModificar(VCliente cliente, int id)
        {
            try
            {
                var result = new LCliente().Modificar(cliente, id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ClienteEliminar(int id)
        {
            try
            {
                var result = new LCliente().Eliminar(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCliente> ClienteListar1(int id)
        {
            try
            {
                var listResult = new LCliente().ListarCliente(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VClienteLista> ClientesListar()
        {
            try
            {
                var listResult = new LCliente().ListarClientes();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ClienteListarEncabezado()
        {
            try
            {
                var listResult = new LCliente().ListarEncabezado();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteClienteEnVenta(int id)
        {
            try
            {
                var listResult = new LCliente().ExisteEnVenta(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /********** LIBRERIA ****************/

        #region Libreria
        public List<VLibreria> LibreriaListarCombo(int idGrupo, int idOrden)
        {
            try
            {
                var listResult = new LLibreria().Listar(idGrupo, idOrden);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool LibreriaGuardar(VLibreriaLista vlibreria)
        {
            try
            {
                var result = new LLibreria().Guardar(vlibreria);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** PROVEEDOR **************/

        #region Proveedor

        #region Consulta

        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VProveedor> ProveedorListarXId(int id)
        {
            try
            {
                var listResult = new LProveedor().ListarXId(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/
        public List<VProveedorLista> ProveedorListar()
        {
            try
            {
                var listResult = new LProveedor().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ProveedorListarEncabezado()
        {
            try
            {
                var listResult = new LProveedor().ListarEncabezado();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VProveedorCombo> TraerProveedoresEdadSemana()
        {
            try
            {
                var listResult = new LProveedor().TraerProveedoresEdadSemana();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** REPORTE ***********/
        #endregion

        #region Transacciones
        public bool ProveedorGuardar(VProveedor proveedor, List<VProveedor_01Lista> detalle, ref int id, string usuario)
        {
            try
            {
                var result = new LProveedor().Guardar(proveedor, detalle, ref id, usuario);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarProveedor(int id)
        {
            try
            {
                var listResult = new LProveedor().Eliminar(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Verificaciones
        public bool ExisteProveedorEnCompra(int idProveedor)
        {
            try
            {
                var listResult = new LProveedor().ExisteEnCompra(idProveedor);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteProveedorEnCompraIng(int idProveedor)
{
            try
            {
                var listResult = new LProveedor().ExisteEnCompraIng(idProveedor);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion
        #region Proveedor_01
        public List<VProveedor_01Lista> Proveedor_01ListarXId(int id)
        {
            try
            {
                var listResult = new LProveedor_01().ListarXId(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /********** PRODUCTO ****************/

        #region PRODUCTO
        #region Transacciones
        public bool ProductoGuardar(VProducto Producto, ref int id)
        {
            try
            {
                var result = new LProducto().Guardar(Producto, ref id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ProductoModificar(VProducto Producto, int id)
        {
            try
            {
                var result = new LProducto().Modificar(Producto, id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ProductoEliminar(int id)
        {
            try
            {
                var result = new LProducto().Eliminar(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        public VProducto ProductoListarXId(int id)
        {
            try
            {
                var listResult = new LProducto().ListarXId(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VProductoLista> ProductoListar()
        {
            try
            {
                var listResult = new LProducto().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable PrductoListarEncabezado(int IdSucursal, int IdAlmacen,
                                          int IdCategoriaPrecio)
        {
            try
            {
                var listResult = new LProducto().ListarEncabezado(IdSucursal, IdAlmacen, IdCategoriaPrecio);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        public List<VProductoListaStock> ListarProductosStock(int IdSucursal, int IdAlmacen, int IdCategoriaPrecio)
        {
            try
            {
                return new LProducto().ListarProductosStock(IdSucursal, IdAlmacen, IdCategoriaPrecio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /************** REPORTES ***************/
        public List<VDetalleKardex> ListarDetalleKardex(DateTime inicio, DateTime fin, int IdAlmacen)
        {
            try
            {
                return new LProducto().ListarDetalleKardex(inicio, fin, IdAlmacen);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verficaciones
        public bool ProductoExisteEnCompra(int id)
        {
            try
            {
                var listResult = new LProducto().ExisteEnCompra(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ProductoExisteEnCompraNormal(int id)
        {
            try
            {
                var listResult = new LProducto().ExisteEnCompraNormal(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ProductoExisteEnVenta(int id)
        {
            try
            {
                var listResult = new LProducto().ExisteEnVenta(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ProductoExisteEnMovimiento(int id)
        {
            try
            {
                var listResult = new LProducto().ExisteEnMovimiento(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ProductoExisteEnTransformacion(int id)
        {
            try
            {
                var listResult = new LProducto().ExisteEnTransformacion(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ProductoExisteEnSeleccion(int id)
        {
            try
            {
                var listResult = new LProducto().ExisteEnSeleccion(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ProductoEsCategoriaSuper(int id)
        {
            try
            {
                var listResult = new LProducto().EsCategoriaSuper(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        /********** ALMACEN *****************/

        #region Almacen        

        public bool AlmacenGuardar(VAlmacen vAlmacen)
        {
            try
            {
                var result = new LAlmacen().Guardar(vAlmacen);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAlmacenCombo> AlmacenListarCombo()
        {
            try
            {
                var listResult = new LAlmacen().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAlmacenLista> AlmacenListar()
        {
            try
            {
                return new LAlmacen().ListarAlmacenes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** TIPO DE ALMACEN *********/

        #region TipoAlmacen

        public bool TipoAlmacenGuardar(VTipoAlmacen vTipoAlmacen)
        {
            try
            {
                var result = new LTipoAlmacen().Guardar(vTipoAlmacen);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTipoAlmacenCombo> TipoAlmacenListarCombo()
        {
            try
            {
                var result = new LTipoAlmacen().ListarCombo();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTipoAlmacenListar> TipoAlmacenListar()
        {
            try
            {
                var result = new LTipoAlmacen().Listar();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** SUCURSAL ****************/

        #region Sucursal

        public List<VSucursalCombo> SucursalListarCombo()
        {
            try
            {
                var listResult = new LSucursal().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VSucursalLista> SucursalListar()
        {
            try
            {
                return new LSucursal().ListarSucursales();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAlmacenLista> ListarAlmacenXSucursalId(int Id)
        {
            try
            {
                return new LSucursal().ListarAlmacenXSucursalId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SucursalGuardar(VSucursal vSucursal)
        {
            try
            {
                return new LSucursal().Guardar(vSucursal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** TRASPASO ****************/

        #region Traspasos

        public bool TraspasoGuardar(VTraspaso vTraspaso, ref int idTI2, ref int id)
        {
            try
            {
                return new LTraspaso().Guardar(vTraspaso, ref idTI2, ref id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TraspasoDetalleGuardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId, int idTI2)
        {
            try
            {
                return new LTraspaso_01().Guardar(lista, TraspasoId, almacenId, idTI2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTraspaso> TraspasosListar()
        {
            try
            {
                return new LTraspaso().Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTraspaso_01> TraspasoDetalleListar(int TraspasoId)
        {
            try
            {
                return new LTraspaso_01().ListarDetalleTraspaso(TraspasoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTListaProducto> ListarInventarioXAlmacenId(int AlmacenId)
        {
            try
            {
                return new LTraspaso().ListarInventarioXAlmacenId(AlmacenId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TraspasoConfirmarRecepcion(int traspasoId, string usuarioRecepcion)
        {
            try
            {
                return new LTraspaso().ConfirmarRecepcion(traspasoId, usuarioRecepcion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** PRECIO ******************/

        #region Precio Categoria

        public bool precioCategoria_Guardar(VPrecioCategoria precioCat, ref int id)
        {
            try
            {
                var result = new LPrecioCategoria().Guardar(precioCat, ref id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VPrecioCategoria> PrecioCategoriaListar()
        {
            try
            {
                var listResult = new LPrecioCategoria().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region Precio 

        public bool PrecioNuevo(VPrecioLista vPrecio, int idSucural, string usuario)
        {
            try
            {
                var listResult = new LPrecio().Nuevo(vPrecio, idSucural, usuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PrecioModificar(VPrecioLista vPrecio, string usuario)
        {
            try
            {
                var listResult = new LPrecio().Modificar(vPrecio, usuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool PrecioGuardar(List<VPrecioLista> vPrecio, int idSucural, string usuario)
        {
            try
            {
                var listResult = new LPrecio().Guardar(vPrecio, idSucural, usuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VPrecioLista> PrecioProductoListar(int idSucursal)
        {
            try
            {
                var listResult = new LPrecio().ListarProductoPrecio(idSucursal);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable PrecioProductoListar2(int idSucursal)
        {
            try
            {
                var listResult = new LPrecio().ListarProductoPrecio2(idSucursal);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** COMPRA INGRESO **********/

        #region Compra Ingreso
        #region Transacciones
        public bool GuardarCompraIngreso(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> vCompraIngreso_01, 
                                            ref int idCompraIng, bool EsDevolucion, List<VCompraIngreso_03> vCompraIngreso_03,
                                           int totalMapleDetalle, int totalMapleDevolucion)
        {
            try
            {
                var result = new LCompraIngreso().Guardar(vCompraIngreso, vCompraIngreso_01, ref idCompraIng, EsDevolucion, vCompraIngreso_03, totalMapleDetalle, totalMapleDevolucion);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstadoCompraIngreso(int IdCompraIng, int estado, ref List<string> lMensaje, bool existeDevolucion)
        {
            try
            {
                var listResult = new LCompraIngreso().ModificarEstado(IdCompraIng, estado, ref lMensaje,existeDevolucion);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VCompraIngreso> TraerComprasIngreso()
        {
            try
            {
                var listResult = new LCompraIngreso().TraerComprasIngreso();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public VCompraIngresoLista TraerCompraIngreso(int id)
        {
            try
            {
                var listResult = new LCompraIngreso().TraerCompraIngreso(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Obtiene CompraIngreso0_01 = Detalle de compra
        public List<VCompraIngreso_01> CmmpraIngreso_01ListarXId(int id)
        {
            try
            {
                var listResult = new LCompraIngreso_01().ListarXId(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngreso_01> CmmpraIngreso_01ListarXId2(int IdGrupo2, int idAlmacen)
        {
            try
            {
                var listResult = new LCompraIngreso_01().ListarXId2(IdGrupo2, idAlmacen);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Obtiene CompraIngreso0_03 = Devoluciones
        public List<VCompraIngreso_03> TraerDevolucionCompraIngreso_03(int id)
        {
            try
            {
                var listResult = new LCompraIngreso_03().TraerDevoluciones(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngreso_03> TraerDevolucionTipoProductoCompraIngreso_03(int IdGrupo2, int idAlmacen)
        {
            try
            {
                var listResult = new LCompraIngreso_03().TraerDevolucionesTipoProducto(IdGrupo2, idAlmacen);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoCombo> TraerCompraIngresoCombo()
        {

            try
            {
                var listResult = new LCompraIngreso().TraerCompraIngresoCombo();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoCombo> TraerCompraIngresoComboCompleto()
        {

            try
            {
                var listResult = new LCompraIngreso().TraerCompraIngresoComboCompleto();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTES ***********/
        public List<VCompraIngresoNota> CompraIngreso_NotaXId(int id)
        {

            try
            {
                var listResult = new LCompraIngreso().NotaCompraIngreso(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoNota> CompraIngreso_DevolucionNotaXId(int id)
        {

            try
            {
                var listResult = new LCompraIngreso().NotaCompraIngresoDevolucion(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoNota> CompraIngreso_ResultadoNotaXId(int id)
        {

            try
            {
                var listResult = new LCompraIngreso().NotaCompraIngresoResultado(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable CompraIngresoBuscar(int estado)
        {
            try
            {
                var listResult = new LCompraIngreso().BuscarCompraIngreso(estado);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public DataTable CompraIngresoReporte(FCompraIngreso fcompraIngreso)  
        {
            try
            {
                var listResult = new LCompraIngreso().ReporteCompraIngreso(fcompraIngreso);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngreso(FCompraIngreso fcompraIngreso)
        {
            try
            {
                var listResult = new LCompraIngreso().ReporteCriterioCompraIngreso(fcompraIngreso);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngresoDevolucion(FCompraIngreso fcompraIngreso)
        {
            try
            {
                var listResult = new LCompraIngreso().ReporteCriterioCompraIngresoDevolucion(fcompraIngreso);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngresoResultado(FCompraIngreso fcompraIngreso)
        {
            try
            {
                var listResult = new LCompraIngreso().ReporteCriterioCompraIngresoResultado(fcompraIngreso);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool CompraIngreso_ExisteEnSeleccion(int id)
        {
            try
            {
                var listResult = new LCompraIngreso().ExisteEnSeleccion(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        #endregion


        #endregion
        #region Compra Ingreso_02
        public bool CompraIngreso_02_Guardar(VCompraIngreso_02 Lista)
        {
            try
            {
                var result = new LCompraIngreso_02().Guardar(Lista);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso_02> CompraIngreso_02_Listar()
        {
            try
            {
                var listResult = new LCompraIngreso_02().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable CompraIngreso_02_ListarTabla()
        {
            try
            {
                var listResult = new LCompraIngreso_02().ListarTabla();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /********** SELECCIÓN ***************/

        #region Seleccion
        #region Transacciones
        public bool Seleccion_ModificarEstado(int IdSeleccion, int estado,ref List<string> lMensaje)
        {
            try
            {
                var result = new LSeleccion().ModificarEstado(IdSeleccion, estado,ref lMensaje);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Seleccion_Guardar(VSeleccion vSeleccion, List<VSeleccion_01_Lista> detalle_Seleccion, List<VSeleccion_01_Lista> detalle_Ingreso, ref int id)
        {
            try
            {
                var result = new LSeleccion().Guardar(vSeleccion, detalle_Seleccion, detalle_Ingreso, ref id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
    
        public VSeleccionLista TraerSeleccion(int idSeleccion)
        {
            try
            {
                var listResult = new LSeleccion().TraerSeleccion(idSeleccion);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<RSeleccionNota> NotaSeleccion(int idSeleccion)
        { 
            try
            {
                var listResult = new LSeleccion().NotaSeleccion(idSeleccion);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        /********** VARIOS REGISTROS ***********/
        public List<VSeleccionEncabezado> TraerSelecciones()
        {
            try
            {
                var listResult = new LSeleccion().TraerSelecciones();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        #endregion
        #endregion
        #region Seleccion_01
        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VSeleccion_01_Lista> TraerSeleccion_01(int idSeleccion)
        {
            try
            {
                var listResult = new LSeleccion_01().TraerSeleccion_01(idSeleccion);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VSeleccion_01_Lista> Seleccion_01_Lista()
        {
            try
            {
                var listResult = new LSeleccion_01().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSeleccion_01_Lista> Seleccion_01_ListarXId_CompraIng_01(int IdCompraInreso_01, int tipo)
        {
            try
            {
                var listResult = new LSeleccion_01().ListarXId_CompraIng(IdCompraInreso_01, tipo);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VSeleccion_01_Lista> Seleccion_01_ListarXId_CompraIng_01_XSeleccion(int IdCompraInreso_01)
        {
            try
            {
                var listResult = new LSeleccion_01().ListarXId_CompraIng_XSeleccion(IdCompraInreso_01);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /********** TRANSFORMACIÓN **********/

        #region TRANSFORMACION
        #region Transacciones
        public bool TransformacionGuardar(VTransformacion vSeleccion, List<VTransformacion_01> detalle, ref int Id, ref List<string> lMensaje)
        {
            try
            {
                var result = new LTransformacion().Guardar(vSeleccion, detalle, ref Id, ref lMensaje);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Transformacion_ModificarEstado(int IdTransformacion, int estado, ref List<string> lMensaje)
        {
            try
            {
                var result = new LTransformacion().ModificarEstado(IdTransformacion, estado,ref lMensaje);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VTransformacion> Transformacion_Lista()
        {
            try
            {
                var listResult = new LTransformacion().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> TransformacionIngreso(int id)
        {
            try
            {
                var listResult = new LTransformacion().ListarIngreso(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTransformacionReport> TransformacionSalida(int id)
        {
            try
            {
                var listResult = new LTransformacion().ListarSalida(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion




        #endregion
        #region Transaformacion_01

        

        public VTransformacion_01 Transformacion_01_TraerFilaProducto(int IdProducto, int idProducto_Mat)
        {
            try
            {
                var listResult = new LTransformacion_01().TraerFilaProducto(IdProducto, idProducto_Mat);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTransformacion_01> Transformacion_01_Lista(int idTransformacion)
        {
            try
            {
                var listResult = new LTransformacion_01().Listar(idTransformacion);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** COMPRA ******************/

        #region Compra
        #region Transacciones
        public bool CompraGuardar(VCompra vCompra, List<VCompra_01> detalle, ref int IdCompra, ref List<string> lMensaje, string usuario)
        {
            try
            {
                var listResult = new LCompra().Guardar(vCompra, detalle, ref IdCompra, ref lMensaje, usuario);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CompraModificarEstado(int IdCompra, int estado, ref List<string> lMensaje)
        {
            try
            {
                var listResult = new LCompra().ModificarEstado(IdCompra, estado, ref lMensaje);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VCompraLista> Compra_Lista()
        {

            try
            {
                var listResult = new LCompra().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompra_01> Compra_01_Lista(int IdCompra)
        {
            try
            {
                var listResult = new LCompra_01().Listar(IdCompra);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones

        public bool CompraExisteEnLoteEnUsoVenta_01(int IdProducto, string lote, DateTime? fechaVen)
        {
            try
            {
                var listResult = new LCompra_01().ExisteEnLoteEnUsoVenta_01(IdProducto, lote, fechaVen);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #endregion

        /********** PLANTILLA ****************/

        #region Plantilla

        public bool PlantillaGuardar(VPlantilla VPlantilla, ref int id)
        {
            try
            {
                return new LPlantilla().Guardar(VPlantilla, ref id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VPlantilla> PlantillaListar(ENConceptoPlantilla concepto)
        {
            try
            {
                var response = new LPlantilla().Listar(concepto);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlantillaDetalleGuardar(List<VPlantilla01> lista, int PlantillaId)
        {
            try
            {
                return new LPlantilla_01().Guardar(lista, PlantillaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VPlantilla01> PlantillaListarDetallePlantilla(int PlantillaId)
        {
            try
            {
                return new LPlantilla_01().ListarDetallePlantilla(PlantillaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** VENTA ********************/

        #region Ventas

        public bool VentaGuardar(VVenta vVenta, List<VVenta_01> detalle, ref int IdVenta, ref List<string> lMensaje)
        {
            try
            {
                var listResult = new LVenta().Guardar(vVenta, detalle, ref IdVenta, ref lMensaje);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VentaModificarEstado(int IdVenta, int estado, ref List<string> lMensaje)
        {
            try
            {
                var listResult = new LVenta().ModificarEstado(IdVenta, estado, ref lMensaje);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public VVenta TraerVenta(int idVenta)
        {
            try
            {
                return new LVenta().TraerVenta(idVenta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VVenta> TraerVentas()
        {
            try
            {
                return new LVenta().TraerVentas();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VVenta_01> VentaDetalleListar(int VentaId)
        {
            try
            {
                return new LVenta_01().ListarDetalle(VentaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /********** ZONA ********************/
        #region ZONA
        #region Consulta
        public List<VZona> ZonaListar()
        {
            try
            {
                return new LZona().Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion
        #endregion

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        /********** REPORTE ***********/
        #endregion
        #region Transacciones

        #endregion
        #region Verificaciones

        #endregion
    }
}