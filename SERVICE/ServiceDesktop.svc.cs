using ENTITY.Cliente.View;
using LOGIC.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ENTITY.Libreria.View;
using ENTITY.Proveedor.View;
using ENTITY.Producto.View;
using ENTITY.inv.Almacen.View;
using ENTITY.reg.PrecioCategoria.View;
using ENTITY.reg.Precio.View;
using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using System.Data;
using ENTITY.com.Seleccion.View;
using ENTITY.com.Seleccion_01.View;
using ENTITY.inv.Transformacion.View;
using ENTITY.inv.Transformacion_01.View;
using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using ENTITY.inv.Sucursal.View;
using ENTITY.com.Seleccion.Report;
using ENTITY.inv.Transformacion.Report;
using ENTITY.inv.Traspaso.View;
using ENTITY.inv.TipoAlmacen.view;
using ENTITY.Plantilla;
using ENTITY.ven.view;

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
        #endregion
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
        #region Proveedor
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
        #region Producto
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
        public List<VProducto> ProductoListarXId(int id)
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

        #endregion

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

        #endregion

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
        #region Compra Ingreso
        public bool CompraIngreso_Guardar(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> detalle, ref int id, string usuario)
        {
            try
            {
                var result = new LCompraIngreso().Guardar(vCompraIngreso, detalle, ref id, usuario);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso> CmmpraIngresoListar()
        {
            try
            {
                var listResult = new LCompraIngreso().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngresoLista> CmmpraIngresoListarXId(int id)
        {
            try
            {
                var listResult = new LCompraIngreso().ListarXId(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public List<VCompraIngresoNota> CompraIngreso_NotaXId(int id)
        {

            try
            {
                var listResult = new LCompraIngreso().ListarNotaXId(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable CompraIngreso_ListarEncabezado()
        {
            try
            {
                var listResult = new LCompraIngreso().ListarEncabezado();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Seleccion

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
        public List<VSeleccionLista> Seleccion_Lista()
        {
            try
            {
                var listResult = new LSeleccion().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion
        #region Seleccion_01
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

        #region Transaformacion
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
        #region Transaformacion_01

        public bool TransformacionGuardar(VTransformacion vSeleccion, List<VTransformacion_01> detalle, ref int Id)
        {
            try
            {
                var result = new LTransformacion().Guardar(vSeleccion, detalle, ref Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
        #region Compra
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

        public List<VCompra_01_Lista> Compra_01_Lista()
        {
            try
            {
                var listResult = new LCompra_01().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

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

        public List<VPlantilla> PlantillaListar(UTILITY.Enum.ENConceptoPlantilla concepto)
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

        #region Ventas

        public bool VentaGuardar(VVenta vVenta, ref int id)
        {
            try
            {
                return new LVenta().Guardar(vVenta, ref id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VVenta> VentasListar()
        {
            try
            {
                return new LVenta().Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VentaDetalleGuardar(List<VVenta_01> lista, int VentaId, int AlmacenId)
        {
            try
            {
                return new LVenta_01().Guardar(lista, VentaId, AlmacenId);
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
    }
}