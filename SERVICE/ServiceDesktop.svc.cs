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
using ENTITY.inv.Sucursal.View;
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

        #endregion
        #region Precio Categoria

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

        public List<VCompraIngreso_01> CmmpraIngreso_01ListarXId2(int IdGrupo2)
        {
            try
            {
                var listResult = new LCompraIngreso_01().ListarXId2(IdGrupo2);
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

        public bool Seleccion_Guardar(VSeleccion vSeleccion, List<VSeleccion_01> detalle, ref int id, string usuario)
        {
            try
            {
                var result = new LSeleccion().Guardar(vSeleccion, detalle, ref id, usuario);
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
        public List<VSeleccion_01_Lista> Seleccion_01_ListarXId_Vacio(int IdCompraIngreso_01)
        {
            try
            {
                var listResult = new LSeleccion_01().ListarXId_Vacio(IdCompraIngreso_01);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Seleccion
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


        #endregion
        #region Seleccion_01
        public List<VTransformacion_01_Lista> Transformacion_01_Lista()
        {
            try
            {
                var listResult = new LTransformacion_01().Listar();
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

    }
}
