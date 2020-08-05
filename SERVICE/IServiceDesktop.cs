using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_02;
using ENTITY.com.Seleccion.View;
using ENTITY.com.Seleccion_01.View;
using ENTITY.DiSoft.Zona;
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
using ENTITY.Rol.View;
using ENTITY.Usuario.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using UTILITY.Enum;
using ENTITY.com.CompraIngreso_03.View;
using ENTITY.Cliente.View;
using ENTITY.com.CompraIngreso.Filter;
using ENTITY.com.Seleccion.Report;
using ENTITY.inv.TI001.VIew;

namespace SERVICE
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public partial interface IServiceDesktop
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        /********** INICIO ****************/
        #region Libreria
        #region Transacciones
        [OperationContract]
        bool LibreriaGuardar(VLibreriaLista vlibreria);
        [OperationContract]
        bool ModificarLibreria(List<VLibreriaLista> vlibreria, ref List<string> mensaje);

 
        #endregion
        #region Consultas
        [OperationContract]
        List<VLibreria> LibreriaListarCombo(int idGrupo, int idOrden);
        [OperationContract]
        List<VLibreria> TraerProgramas();
        [OperationContract]
        List<VLibreria> TraerCategorias(int idPrograma);
        [OperationContract]
        List<VLibreriaLista> TraerLibreriasXCategoria(int idGrupo, int idOrden);
        #endregion
        #endregion
        /********** ROL ****************/
        #region Rol

        [OperationContract]
        bool RolGuardar(VRol Rol, List<VRol_01> detalle, ref int IdRol, string usuario);

        [OperationContract]
        bool RolEliminar(int id, List<VRol_01> detalle);
        [OperationContract]
        List<VRol> ListarRol();

        [OperationContract]
        List<VRol_01> ListarDetalleRol_01(int idRol);

        [OperationContract]
        DataTable AsignarPermisos(string idRol, string NombreProg);

        [OperationContract]
        bool RolExisteEnUsuario(int IdRol);

        [OperationContract]
        List<VRol_01> ListarDetalle(int IdRol, int IdModulo);

        #endregion

        /********** USUARIO ****************/
        #region Usuario

        [OperationContract]
        bool UsuarioGuardar(VUsuario Rol, List<VUsuario_01> detalle, ref int IdUsuario, string usuario);

        [OperationContract]
        bool UsuarioEliminar(int id, List<VUsuario_01> detalle);

        [OperationContract]
        List<VUsuario> ListarUsuario();

        [OperationContract]
        List<VUsuario_01> ListarDetalleUsuario_01(int idUsuario);

        [OperationContract]
        List<VUsuario> ValidarUsuario(string user, string password);

        #endregion

        /********** CLIENTE ***************/
        #region Cliente
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        [OperationContract]
        List<VCliente> ClienteListar1(int id);
        /********** VARIOS REGISTROS ***********/
        [OperationContract]
        List<VCliente> ClienteListar();
        [OperationContract]
        List<VClienteLista> ClientesListar();

        [OperationContract]
        DataTable ClienteListarEncabezado();
        [OperationContract]
        List<VClienteCombo> TraerClienteCombo();

        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        [OperationContract]
        bool ClienteGuardar(VCliente cliente, ref int id);
        [OperationContract]
        bool ClienteModificar(VCliente cliente, int id);
        [OperationContract]
        bool ClienteEliminar(int id);
        #endregion
        #region Verificaciones
        [OperationContract]
        bool ExisteClienteEnVenta(int idCliente);
        #endregion
        #endregion
        /********** PROVEEDOR **************/

        #region Proveedor

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        [OperationContract]
        List<VProveedor> ProveedorListarXId(int id);

        /********** VARIOS REGISTROS ***********/
        [OperationContract]
        List<VProveedorLista> ProveedorListar();
        [OperationContract]
        DataTable ProveedorListarEncabezado();
        [OperationContract]
        List<VProveedorCombo> TraerProveedoresEdadSemana();
        /********** REPORTE ***********/
        #endregion

        #region Transacciones
        [OperationContract]
        bool ProveedorGuardar(VProveedor proveedor, List<VProveedor_01Lista> detalle, ref int id, string usuario);
        [OperationContract]
        bool EliminarProveedor(int id);
        #endregion

        #region Verificaciones
        [OperationContract]
        bool ExisteProveedorEnCompra(int idProveedor);
        [OperationContract]
        bool ExisteProveedorEnCompraIng(int idProveedor);
        #endregion

        #endregion

        #region Proveedor_01
        [OperationContract]
        List<VProveedor_01Lista> Proveedor_01ListarXId(int Id);
        #endregion 
        /********** PRODUCTO ****************/
        
        #region PRODUCTO
            #region Transacciones
                [OperationContract]
                bool ProductoGuardar(VProducto proveedor, ref int id);
                [OperationContract]
                bool ProductoModificar(VProducto proveedor, int id);
                [OperationContract]
                bool ProductoEliminar(int id);
            #endregion
            #region Consultas
                [OperationContract]
                VProducto ProductoListarXId(int id);

                [OperationContract]
                List<VProductoLista> ProductoListar();       

                [OperationContract]
                DataTable PrductoListarEncabezado(int IdSucursal, int IdAlmacen, int IdCategoriaPrecio);

                [OperationContract]
                List<VDetalleKardex> ListarDetalleKardex(System.DateTime inicio, System.DateTime fin, int IdAlmacen, int codProducto);
                [OperationContract]
                List<VProductoListaStock> ListarProductosStock(int IdSucursal, int IdAlmacen, int IdCategoriaPrecio);
        #endregion
        #region Verificaciones
        [OperationContract]
                bool ProductoExisteEnCompra(int id);
                [OperationContract]
                bool ProductoExisteEnCompraNormal(int id);
                [OperationContract]
                bool ProductoExisteEnVenta(int id);
                [OperationContract]
                bool ProductoExisteEnMovimiento(int id);
                [OperationContract]
                bool ProductoExisteEnTransformacion(int id);
                [OperationContract]
                bool ProductoExisteEnSeleccion(int id);
                [OperationContract]
                bool ProductoEsCategoriaSuper(int id);
        #endregion
        #endregion
        /********** ALMACEN *****************/

        #region Almacen

        [OperationContract]
        bool AlmacenGuardar(VAlmacen vAlmacen);

        [OperationContract]
        List<VAlmacenCombo> AlmacenListarCombo(int usuarioId);

        [OperationContract]
        List<VAlmacenLista> AlmacenListar();

        #endregion
        /********** TIPO DE ALMACEN *********/
      
        #region Tipo de Almacen

        [OperationContract]
        void TipoAlmacenGuardar(VTipoAlmacen vTipoAlmacen, ref int Id);

        [OperationContract]
        List<VTipoAlmacenCombo> TipoAlmacenListarCombo();

        [OperationContract]
        List<VTipoAlmacenListar> TipoAlmacenListar();

        #endregion
        /********** SUCURSAL ****************/
       
        #region Sucursal
        [OperationContract]
        List<VSucursalCombo> SucursalListarCombo();

        [OperationContract]
        List<VSucursalLista> SucursalListar();

        [OperationContract]
        List<VAlmacenLista> ListarAlmacenXSucursalId(int Id);

        [OperationContract]
        bool SucursalGuardar(VSucursal vSucursal);
        #endregion
        /********** TRASPASO ****************/

        #region Traspaso

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        [OperationContract]
        VTraspaso_01 TraerTraspass_01(int idDetalle);
        /********** VARIOS REGISTROS ***********/
        [OperationContract]
        List<VTraspaso> TraerTraspasos(int usuarioId);

        [OperationContract]
        List<VTraspaso_01> TraerTraspasos_01(int idTraspaso);

        [OperationContract]
        List<VTListaProducto> ListarInventarioXAlmacenId(int AlmacenId);
        [OperationContract]
        List<VTraspaso_01> TraerTraspasos_01Vacio(int idTraspaso);
        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        [OperationContract]
        bool GuardarTraspaso(VTraspaso vTraspaso, List<VTraspaso_01> detalle, ref int idTraspaso, ref List<string> lMensaje);

        [OperationContract]
        bool TraspasoDetalleGuardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId, int idTI2);

        [OperationContract]
        bool TraspasoConfirmarRecepcion(int traspasoId, string usuarioRecepcion);
        #endregion
        #region Verificaciones

        #endregion

        #endregion
        /********** PRECIO ******************/

        #region Precio Categoria
        [OperationContract]
        List<VPrecioCategoria> PrecioCategoriaListar();

        [OperationContract]
        bool precioCategoria_Guardar(VPrecioCategoria precioCat, ref int id);
        #endregion
        #region Precio 
        [OperationContract]
        List<VPrecioLista> PrecioProductoListar(int idSucursal);

        [OperationContract]
        DataTable PrecioProductoListar2(int idSucursal);
        [OperationContract]
        bool PrecioGuardar(List<VPrecioLista> vPrecio, int idSucural, string usuario);
        [OperationContract]
        bool PrecioNuevo(VPrecioLista vPrecio, int idSucural, string usuario);
        [OperationContract]
        bool PrecioModificar(VPrecioLista vPrecio, string usuario);
        #endregion
        /********** COMPRA INGRESO **********/

        #region COMPRA INGRESO
        #region Transacciones

        [OperationContract]
        bool GuardarCompraIngreso(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> vCompraIngreso_01,
                                            ref int idCompraIng, bool EsDevolucion, List<VCompraIngreso_03> vCompraIngreso_03,
                                           int totalMapleDetalle, int totalMapleDevolucion);
        [OperationContract]
        bool ModificarEstadoCompraIngreso(int IdCompraIng, int estado,  ref List<string>lMensaje,bool existeDevolucion);

        #endregion
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/   
        
       [OperationContract]
       VCompraIngresoLista TraerCompraIngreso(int id);

        /********** VARIOS REGISTROS ***********/

        [OperationContract]
        DataTable CompraIngresoBuscar(int estado,int usuarioId);

        [OperationContract]
        List<VCompraIngreso> TraerComprasIngreso(int usuarioId);

        [OperationContract]
        List<VCompraIngresoCombo> TraerCompraIngresoCombo(int usuarioId);

        [OperationContract]
        List<VCompraIngresoCombo> TraerCompraIngresoComboCompleto();

        /********** REPORTES ***********/

        [OperationContract]
         DataTable CompraIngresoReporte(FCompraIngreso fcompraIngreso);
        [OperationContract]
        DataTable ReporteCriterioCompraIngreso(FCompraIngreso fcompraIngreso);
        [OperationContract]
        DataTable ReporteCriterioCompraIngresoDevolucion(FCompraIngreso fcompraIngreso);
        [OperationContract]
        DataTable ReporteCriterioCompraIngresoResultado(FCompraIngreso fcompraIngreso);

        [OperationContract]
        List<VCompraIngresoNota> CompraIngreso_NotaXId(int id);
        [OperationContract]
        List<VCompraIngresoNota> CompraIngreso_DevolucionNotaXId(int id);
        [OperationContract]
        List<VCompraIngresoNota> CompraIngreso_ResultadoNotaXId(int id);
        #endregion
        #region Verficaciones
        [OperationContract]
                bool CompraIngreso_ExisteEnSeleccion(int id);
            #endregion
        #endregion
        #region Compra Ingreso_01
        [OperationContract]
        List<VCompraIngreso_01> CmmpraIngreso_01ListarXId(int id);
        [OperationContract]
        List<VCompraIngreso_01> CmmpraIngreso_01ListarXId2(int IdGrupo2, int idAlmacen);
        #endregion
        #region COMPRA Ingreso_02
        [OperationContract]
        bool CompraIngreso_02_Guardar(VCompraIngreso_02 Lista);

        [OperationContract]
        List<VCompraIngreso_02> CompraIngreso_02_Listar();
        [OperationContract]
        DataTable CompraIngreso_02_ListarTabla();
        #endregion
        #region COMPRA Ingreso_03      
        [OperationContract]
        List<VCompraIngreso_03> TraerDevolucionCompraIngreso_03(int id);
        [OperationContract]
        List<VCompraIngreso_03> TraerDevolucionTipoProductoCompraIngreso_03(int IdGrupo2, int idAlmacen);
        #endregion

        /********** SELECCIÓN ***************/

        #region SELECCION
        #region Transacciones
        [OperationContract]
                bool Seleccion_Guardar(VSeleccion vSeleccion, List<VSeleccion_01_Lista> detalle_Seleccion, List<VSeleccion_01_Lista> detalle_Ingreso, ref int id);
                [OperationContract]
                bool Seleccion_ModificarEstado(int IdSeleccion, int estado, ref List<string> lMensaje);
        #endregion
        #region Consulta
        [OperationContract]
         List<VSeleccionEncabezado> TraerSelecciones(int usuarioId);
        [OperationContract]
        VSeleccionLista TraerSeleccion(int idSeleccion);
        [OperationContract]
        List<RSeleccionNota> NotaSeleccion(int idSeleccion);
        /********** REPORTE ***********/
        [OperationContract]       
        DataTable ReporteHistoricoSeleccion(DateTime? fechaDesde, DateTime? fechaHasta);
        #endregion
        #endregion
        #region Seleccion_01
        /******** VALOR/REGISTRO ÚNICO *********/
        [OperationContract]
        List<VSeleccion_01_Lista> TraerSeleccion_01(int idSeleccion);
        /********** VARIOS REGISTROS ***********/
        [OperationContract]
        List<VSeleccion_01_Lista> Seleccion_01_Lista();
        [OperationContract]
        List<VSeleccion_01_Lista> Seleccion_01_ListarXId_CompraIng_01(int IdCompraInreso_01, int tipo);
        [OperationContract]
        List<VSeleccion_01_Lista> Seleccion_01_ListarXId_CompraIng_01_XSeleccion(int IdCompraInreso_01);
        #endregion

        /********** TRANSFORMACIÓN **********/
        
        #region TRANSFORMACION
        #region Trnasacciones
        [OperationContract]
        bool TransformacionGuardar(VTransformacion vSeleccion, List<VTransformacion_01> detalle, ref int Id, ref List<string> lMensaje);

                [OperationContract]
                bool Transformacion_ModificarEstado(int IdTransformacion, int estado, ref List<string> lMensaje);
        #endregion
        #region Consulta
        [OperationContract]
                List<VTransformacion> Transformacion_Lista(int usuarioId);
                [OperationContract]
                List<VTransformacionReport> TransformacionIngreso(int id);
                [OperationContract]
                List<VTransformacionReport> TransformacionSalida(int id);
            #endregion
        #endregion
        #region Transformacion_01
        [OperationContract]
        List<VTransformacion_01> Transformacion_01_Lista(int idTransformacion);
        [OperationContract]
        VTransformacion_01 Transformacion_01_TraerFilaProducto(int IdProducto, int idProducto_Mat);
        #endregion

        /********** COMPRA ******************/
       
        #region Compra
        #region Transacciones
        [OperationContract]
        bool CompraGuardar(VCompra vCompra, List<VCompra_01> detalle, ref int IdCompra, ref List<string> lMensaje, string usuario);
        [OperationContract]
        bool CompraModificarEstado(int IdCompra, int estado, ref List<string> lMensaje);
        #endregion
        #region Consulta
        [OperationContract]
        List<VCompraLista> Compra_Lista(int usuarioId);
        #endregion
        #region Verificaciones
        [OperationContract]
        bool CompraExisteEnLoteEnUsoVenta_01(int IdProducto, string lote, DateTime? fechaVen);
        #endregion
        #endregion
        #region Compra_01
        [OperationContract]
        List<VCompra_01> Compra_01_Lista(int IdCompra);
        #endregion

        /********** PLANTILLA ****************/
       
        #region Plantilla

        [OperationContract]
        bool PlantillaGuardar(VPlantilla VPlantilla, ref int id);

        [OperationContract]
        List<VPlantilla> PlantillaListar(ENConceptoPlantilla concepto);

        [OperationContract]
        bool PlantillaDetalleGuardar(List<VPlantilla01> lista, int PlantillaId);

        [OperationContract]
        List<VPlantilla01> PlantillaListarDetallePlantilla(int PlantillaId);
        #endregion

        /********** VENTA ********************/

        #region Venta
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        [OperationContract]
        VVenta TraerVenta(int idVenta);
        /********** VARIOS REGISTROS ***********/
        [OperationContract]
        List<VVenta_01> VentaDetalleListar(int VentaId);

        [OperationContract]
        List<VVenta> TraerVentas(int usuarioId);

        [OperationContract]
        List<VVenta_01> TraerDetalleVentaVacio(int VentaId);
        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        [OperationContract]
        bool VentaGuardar(VVenta vVenta, List<VVenta_01> detalle, ref int IdVenta, ref List<string> lMensaje);
        [OperationContract]
        bool VentaModificarEstado(int IdVenta, int estado, ref List<string> lMensaje);
        #endregion
        #region Verificaciones
        #endregion
        #endregion

        /********** ZONA ********************/
        #region ZONA
        #region Consulta
        [OperationContract]
        List<VZona> ZonaListar();
        #endregion
        #endregion

        #region Invetario TI001
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        [OperationContract]
        List<VTI001> TraerInventarioLotes(int IdProducto, int idAlmacen);
        /********** REPORTE ***********/
        #endregion
        #region Transacciones

        #endregion
        #region Verificaciones

        #endregion
        #endregion
    }
    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}