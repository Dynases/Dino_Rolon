using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTILITY.Enum.EnEstaticos
{
    public enum ENEstaticosOrden
    {
        //****CLIENTE
        CIUDAD_CLIENTE = 1,
        FACTURACION_CLIENTE =2,
        //****PROVEEDOR
        PROVEEDOR_CIUDAD = 1,
        PROVEEDOR_TIPO = 2,
        PROVEEDOR_TIPO_ALOJAMIENTO = 3,
        PROVEEDOR_LINEA_GENETICA = 4,
        //****PRODUCTO
        PRODUCTO_GRUPO1 = 1,
        PRODUCTO_GRUPO2 = 2,
        PRODUCTO_GRUPO3 = 3,
        PRODUCTO_GRUPO4 = 4,
        PRODUCTO_GRUPO5 = 5,
        PRODUCTO_UN_VENTA = 6,
        PRODUCTO_UN_PESO = 7,
        //****COMPRA INGRESO  
        COMPRA_INGRESO_PLACA = 1,
        COMPRA_INGRESO_CANTIDAD_CAJAS = 2,
        COMPRA_INGRESO_CANTIDAD_GRUPOS = 3,
        COMPRA_INGRESO_RECIBIDO = 4,
        //****VENTA
        VENTA_ENC_PREVENTA = 1,
        VENTA_ENC_VENTA = 2,
        VENTA_ENC_TRASPORTE = 3,
        VENTA_ENC_RECEPCION= 4,
        //****ROL
        ROL_ORDEN = 1
    }
}
