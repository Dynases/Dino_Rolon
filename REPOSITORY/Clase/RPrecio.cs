using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.reg.Precio.View;
using System.Data;
using DATA.EntityDataModel.DiAvi;
using System.Data.Entity;

namespace REPOSITORY.Clase
{
    public class RPrecio : BaseConexion, IPrecio
    {
        #region Transacciones
        public bool Nuevo(VPrecioLista vPrecio,  int idAlmacen, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var precio = new Precio();
                    precio.IdSucursal = idAlmacen;
                    precio.IdProduc = vPrecio.IdProducto;
                    precio.IdPrecioCat = vPrecio.IdPrecioCat;
                    precio.Precio1 = vPrecio.Precio;
                    precio.Oferta =0;
                    precio.Observacion = "";
                    precio.Fecha = DateTime.Now.Date;
                    precio.Hora = DateTime.Now.ToString("hh:mm");
                    precio.Usuario = usuario;
                    db.Precio.Add(precio);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VPrecioLista vPrecio, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var precio = db.Precio.FirstOrDefault(b => b.Id == vPrecio.Id);
                    precio.IdPrecioCat = vPrecio.IdPrecioCat;
                    precio.Precio1 = vPrecio.Precio;                    
                    precio.Usuario = usuario;
                    db.Precio.Attach(precio);
                    db.Entry(precio).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VPrecioLista> ListarProductoPrecio(int idAlmacen)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Precio
                                      join pro in db.Producto on
                                        new { idpro = a.IdProduc, idAlm = a.IdSucursal }
                                         equals new { idpro = pro.Id, idAlm = idAlmacen }
                                      join cat in db.PrecioCat on
                                           new { idCat = a.IdPrecioCat }
                                         equals new { idCat = cat.Id }
                                      select new VPrecioLista
                                      {
                                          Id = a.Id,
                                          IdProducto = a.IdProduc,
                                          IdPrecioCat = a.IdPrecioCat,
                                          COd = cat.Cod,
                                          Precio = a.Precio1,
                                          Estado = 1
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarProductoPrecio2(int idSucursal)
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = string.Format(@"SELECT
	                                                a.Id,a.IdProduc as IdProducto,a.IdPrecioCat,c.Cod,a.Precio, 1 as Estado
                                                FROM 
	                                                REG.Precio a JOIN
	                                                REG.Producto b ON a.IdProduc = b.Id  AND A.IdSucursal = {0} JOIN
	                                                REG.PrecioCat c ON c.Id = a.IdPrecioCat 
                                                GROUP BY a.Id,a.IdProduc,a.IdPrecioCat,c.Cod,a.Precio
                                                union
                                                SELECT 
	                                                0 Id,c.Id as IdProducto,a.Id as IdPrecioCat, a.Cod, 
				                                                ISNULL((SELECT ((e.Precio * CAST(a.Margen/100.0 as decimal))+ e.Precio) as Precio 
				                                                FROM 
					                                                REG.Precio e
				                                                WHERE
					                                                e.IdProduc = c.IdProducto and e.IdPrecioCat = 1 and e.IdSucursal = {0} ),0), 3 as Estado  
                                                FROM 
	                                                REG.PrecioCat a, REG.Producto c
                                                WHERE
	                                                a.Id NOT IN(SELECT b.IdPrecioCat 
				                                                FROM 
					                                                REG.Precio b join
					                                                REG.Producto e ON b.IdProduc = c.Id AND b.IdSucursal = {0} )
                                                GROUP BY c.Id,a.Id,a.Cod,a.Margen,c.IdProducto ", idSucursal);
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}
