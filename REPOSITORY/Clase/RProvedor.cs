using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Proveedor.View;
using DATA.EntityDataModel.DiAvi;
using System.Data;

namespace REPOSITORY.Clase
{
    public class RProvedor : BaseConexion, IProveedor
    {
        public bool Guardar(VProveedor vProveedor, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Proveed proveedor;
                    if (id > 0)
                    {
                        proveedor = db.Proveed.Where(a => a.Id == idAux).FirstOrDefault();
                        if (proveedor == null)
                            throw new Exception("No existe el proveedor con id " + idAux);
                    }
                    else
                    {
                        proveedor = new Proveed();
                        db.Proveed.Add(proveedor);
                    }                        
                    
                    proveedor.IdSpyre = vProveedor.IdSpyre;
                    proveedor.Estado = vProveedor.Estado;
                    proveedor.Descrip = vProveedor.Descripcion;
                    proveedor.Ciudad = vProveedor.Ciudad;
                    proveedor.Tipo = vProveedor.Tipo;
                    proveedor.TipoProve = vProveedor.TipoProveeedor;
                    proveedor.Direcc = vProveedor.Direccion;
                    proveedor.Contacto = vProveedor.Contacto;
                    proveedor.Telfon = vProveedor.Telefono;
                    proveedor.Email = vProveedor.Email;
                    proveedor.Contacto2 = vProveedor.Contacto2;
                    proveedor.Telfon2 = vProveedor.Telefono2;
                    proveedor.Email2 = vProveedor.Email2;
                    proveedor.Latitud = vProveedor.Latitud;
                    proveedor.Longit = vProveedor.Longittud;
                    proveedor.Imagen = vProveedor.Imagen;          
                    proveedor.Fecha = vProveedor.Fecha;
                    proveedor.Hora = vProveedor.Hora;
                    proveedor.Usuario = vProveedor.Usuario;
                    
                    db.SaveChanges();
                    id = proveedor.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VProveedorLista> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Proveed
                                      join c in db.Libreria on
                                       new
                                       {
                                           Grupo = 2,
                                           Orden = 1,
                                           Libreria = a.Ciudad
                                       }
                                       equals
                                       new
                                       {
                                           Grupo = c.IdGrupo,
                                           Orden = c.IdOrden,
                                           Libreria = c.IdLibrer
                                       }
                                      select new VProveedorLista                                      
                                      {
                                          Id = a.Id,
                                          Descripcion = a.Descrip,
                                          Contacto = a.Contacto,
                                          Ciudad = a.Ciudad,
                                          NombreCiudad =c.Descrip,                                    
                                          Telefono = a.Telfon,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado()
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = @"SELECT 
                                        A.Id,A.Descrip,A.Contacto,A.Ciudad,B.Descrip AS CiudadNombre,a.Telfon, 
	                                    c.EdadSeman AS EdadSemana
                                    FROM
                                        COM.Proveed a
                                        JOIN ADM.Libreria b ON B.IdGrupo = 2 AND B.IdOrden = 1 AND B.IdLibrer = A.Ciudad
                                        JOIN COM.Proveed_01 c ON c.IdProveed = a.Id
                                    WHERE
                                        C.Id = (select MAX(x.Id) from COM.Proveed_01 x WHERE X.IdProveed = A.Id)
                                    GROUP BY C.EdadSeman, A.Id,A.Descrip,A.Ciudad,B.Descrip, A.Contacto,A.Telfon";
                return  tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VProveedor> ListarXId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Proveed
                                      where a.Id.Equals(id)
                                      select new VProveedor
                                      {
                                          Id = a.Id,
                                          IdSpyre = a.IdSpyre,
                                          Descripcion = a.Descrip,
                                          Ciudad = a.Ciudad,
                                          Tipo = a.Tipo,
                                          TipoProveeedor = a.TipoProve,
                                          Direccion = a.Direcc,
                                          Contacto = a.Contacto,
                                          Telefono = a.Telfon,
                                          Email = a.Email,
                                          Contacto2 = a.Contacto2,
                                          Telefono2 = a.Telfon2,
                                          Email2 = a.Email2,
                                          Latitud = a.Latitud,
                                          Longittud = a.Longit,
                                          Imagen = a.Imagen,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
