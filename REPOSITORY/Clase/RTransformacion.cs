using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.inv.Transformacion.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;
using ENTITY.inv.Transformacion.Report;
using ENTITY.com.Seleccion.Report;

namespace REPOSITORY.Clase
{
    public class RTransformacion : BaseConexion, ITransformacion
    {
        #region TRANSACCIONES
        public bool Guardar(VTransformacion vTransformacion, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Transformacion transformacion;
                    if (id > 0)
                    {
                        transformacion = db.Transformacion.Where(a => a.Id == idAux).FirstOrDefault();
                        if (transformacion == null)
                            throw new Exception("No existe la transformacion con id " + idAux);
                    }
                    else
                    {
                        transformacion = new Transformacion();
                        db.Transformacion.Add(transformacion);
                    }
                    transformacion.IdSucSalida = vTransformacion.IdSucSalida;
                    transformacion.IdSucIngreso = vTransformacion.IdSucIngreso;
                    transformacion.Estado = (int)ENEstado.GUARDADO;
                    transformacion.Observ = vTransformacion.Observ;          
                    transformacion.Fecha = vTransformacion.Fecha;
                    transformacion.Hora = vTransformacion.Hora;
                    transformacion.Usuario = vTransformacion.Usuario;                    
                    db.SaveChanges();
                    id = transformacion.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region CONSULTAS
        public List<VTransformacion> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Transformacion
                                      join b in db.Sucursal on a.IdSucIngreso equals b.Id
                                      join c in db.Sucursal on a.IdSucSalida equals c.Id
                                      select new VTransformacion
                                      {
                                          Id = a.Id,
                                          IdSucSalida = a.IdSucSalida,
                                          Sucursal2 = c.Descrip,
                                          IdSucIngreso = a.IdSucIngreso,
                                          Sucursal1 = b.Descrip,
                                          Observ = a.Observ,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> ListarIngreso(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_TransformacionIngreso
                                      where a.Id.Equals(Id)
                                      select new VTransformacionReport
                                      {
                                          Id = a.Id,
                                          SucursalIngreso = a.SucursalIngreso,
                                          SucursalSalida = a.SucursalSalida,
                                          IdProducto = a.IdProducto,
                                          Descrip = a.Descrip,
                                          Observ = a.Observ,
                                          TotalProd = a.TotalProd,
                                          Fecha = a.Fecha
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> ListarSalida(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_TransformacionSalida
                                      where a.Id.Equals(Id)
                                      select new VTransformacionReport
                                      {
                                          //Id = a.Id,
                                          //SucursalIngreso = a.SucursalIngreso,
                                          //SucursalSalida = a.SucursalSalida,
                                          //IdProducto = a.IdProducto_Mat,
                                          //Descripcion = a.Descrip,
                                          //Observ = a.Observ,
                                          //Total = a.Total,
                                          //Fecha = a.Fecha
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
