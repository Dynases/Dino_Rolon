using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Almacen.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RAlmacen : BaseConexion, IAlmacen
    {
        public bool Guardar(VAlmacen vAlmacen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var almacen = new Almacen
                    {
                        Descrip = vAlmacen.Descripcion,
                        Direcc = vAlmacen.Direccion,
                        Fecha = DateTime.Now,
                        Hora = DateTime.Now.ToShortTimeString(),
                        Latit = vAlmacen.Latitud,
                        Longi = vAlmacen.Longitud,
                        Telef = vAlmacen.Telefono,
                        Usuario = vAlmacen.Usuario,
                        IdSuc = vAlmacen.IdSucursal,
                        TipoAlmacen = vAlmacen.TipoAlmacenId,
                        Imagen = vAlmacen.Imagen,
                        Encargado = vAlmacen.Encargado
                    };

                    db.Almacen.Add(almacen);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAlmacenCombo> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Almacen
                                      select new VAlmacenCombo
                                      {
                                          IdLibreria = a.Id,
                                          Descripcion = a.Descrip + " | Sucursal : " + a.Sucursal.Descrip
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAlmacenLista> ListarAlmacenes()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Almacen.Select(al => new VAlmacenLista
                    {
                        Id = al.Id,
                        Descripcion = al.Descrip,
                        Sucursal = al.Sucursal.Descrip,
                        SucursalId = al.Sucursal.Id,
                        Direccion = al.Direcc,
                        Telefono = al.Telef,
                        Encargado = al.Encargado,
                        TipoAlmacen = al.TipoAlmacen1.TipoAlmacen1,
                        TipoAlmacenId = al.TipoAlmacen1.Id
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
