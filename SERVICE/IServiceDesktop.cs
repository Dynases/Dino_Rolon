using ENTITY.Cliente.View;
using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.Seleccion.View;
using ENTITY.com.Seleccion_01.View;
using ENTITY.inv.Deposito;
using ENTITY.inv.Sucursal.View;
using ENTITY.inv.Transformacion.View;
using ENTITY.inv.Transformacion_01.View;
using ENTITY.Libreria.View;
using ENTITY.Producto.View;
using ENTITY.Proveedor.View;
using ENTITY.reg.Precio.View;
using ENTITY.reg.PrecioCategoria.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SERVICE
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceDesktop
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        ///**********INICIO**************************
        #region Libreria
        [OperationContract]
        List<VLibreria> LibreriaListarCombo(int idGrupo, int idOrden);

        [OperationContract]
        bool LibreriaGuardar(VLibreriaLista vlibreria);
        #endregion
        ///**********CLIENTE*************************
        
        #region Cliente

        [OperationContract]
        List<VCliente> ClienteListar();

        [OperationContract]
        bool ClienteGuardar(VCliente cliente, ref int id);
        [OperationContract]
        bool ClienteModificar(VCliente cliente, int id);
        [OperationContract]
        bool ClienteEliminar(int id);

        [OperationContract]
        List<VCliente> ClienteListar1(int id);

        [OperationContract]
        List<VClienteLista> ClientesListar();


        #endregion
        ///**********PROVEEDOR***********************
        ///
        #region Proveedor
        [OperationContract]
        bool ProveedorGuardar(VProveedor proveedor, List<VProveedor_01Lista> detalle, ref int id, string usuario);

        [OperationContract]
        List<VProveedor> ProveedorListarXId(int id);

        [OperationContract]
        List<VProveedorLista> ProveedorListar();
        [OperationContract]
        DataTable ProveedorListarEncabezado();
        #endregion

        #region Proveedor_01
        [OperationContract]
        List<VProveedor_01Lista> Proveedor_01ListarXId(int Id);
        #endregion
        ///**********PRODUCTO************************
        ///
        #region Producto
        [OperationContract]
        bool ProductoGuardar(VProducto proveedor, ref int id);
        [OperationContract]
        bool ProductoModificar(VProducto proveedor, int id);
        [OperationContract]
        bool ProductoEliminar(int id);

        [OperationContract]
        List<VProducto> ProductoListarXId(int id);

        [OperationContract]
        List<VProductoLista> ProductoListar();

        [OperationContract]
        bool ProductoExisteEnCompra(int id);
        #endregion
        ///**********SUCURSAL************************
        ///
        #region Sucursal

        [OperationContract]
        List<VSucursalCombo> SucursalListarCombo();

        [OperationContract]
        List<VSucursalLista> SucursalListar();

        [OperationContract]
        bool SucursalGuardar(VSucursal vSucursal);

        #endregion
        ///**********DEPOSITO************************
        ///
        #region Deposito
        [OperationContract]
        List<VDepositoCombo> DepositoListarCombo();
        [OperationContract]
        List<VDepositoLista> DepositoListar();
        [OperationContract]
        List<VSucursalLista> ListarSucursalXDepositoId(int Id);
        [OperationContract]
        bool DepositoGuardar(VDeposito vDeposito);
        #endregion
        ///**********PRECIO**************************
        ///
        #region Precio Categoria
        [OperationContract]
        List<VPrecioCategoria> PrecioCategoriaListar();
        #endregion
        #region Precio 
        [OperationContract]
        List<VPrecioLista> PrecioProductoListar(int idSucursal);
        #endregion
        ///**********COMPRA INGRESO******************
        ///
        #region Compra Ingreso 
        [OperationContract]
        List<VCompraIngreso> CmmpraIngresoListar();

        [OperationContract]
        List<VCompraIngresoLista> CmmpraIngresoListarXId(int id);

        [OperationContract]
        List<VCompraIngresoNota> CompraIngreso_NotaXId(int id);

        [OperationContract]
        bool CompraIngreso_Guardar(VCompraIngresoLista proveedor, List<VCompraIngreso_01> detalle, ref int id, string usuario);

        [OperationContract]
        DataTable CompraIngreso_ListarEncabezado();
        #endregion
        #region Compra Ingreso_01
        [OperationContract]
        List<VCompraIngreso_01> CmmpraIngreso_01ListarXId(int id);
        [OperationContract]
        List<VCompraIngreso_01> CmmpraIngreso_01ListarXId2(int IdGrupo2);
        #endregion
        ///**********SELECCION**********************
        ///
        #region Seleccion
        [OperationContract]
        List<VSeleccionLista> Seleccion_Lista();

        [OperationContract]
        bool Seleccion_Guardar(VSeleccion vSeleccion, List<VSeleccion_01_Lista> detalle_Seleccion, List<VSeleccion_01_Lista> detalle_Ingreso, ref int id);
        #endregion
        #region Seleccion_01
        [OperationContract]
        List<VSeleccion_01_Lista> Seleccion_01_Lista();
        [OperationContract]
        List<VSeleccion_01_Lista> Seleccion_01_ListarXId_CompraIng_01(int IdCompraInreso_01, int tipo);
        #endregion
        ///**********TRANSFORMACION******************
        ///
        #region Transformacion
        [OperationContract]
        List<VTransformacion> Transformacion_Lista();

        [OperationContract]
        bool TransformacionGuardar(VTransformacion vSeleccion, List<VTransformacion_01> detalle, ref int Id);

   
        #endregion
        #region Transformacion_01
        [OperationContract]
        List<VTransformacion_01> Transformacion_01_Lista(int idTransformacion);
        [OperationContract]
        VTransformacion_01 Transformacion_01_TraerFilaProducto( int IdProducto, int idProducto_Mat);
        #endregion
        ///**********COMPRA******************
        ///
        #region Compra
        [OperationContract]
        List<VCompraLista> Compra_Lista();
        #endregion
        #region Compra_01
        [OperationContract]
        List<VCompra_01_Lista> Compra_01_Lista();
        #endregion
        // TODO: agregue aquí sus operaciones de servicio
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