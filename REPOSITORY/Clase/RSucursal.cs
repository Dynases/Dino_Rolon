using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Almacen.View;
using ENTITY.inv.Sucursal.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RSucursal : BaseConexion, ISucursal
    {
        public List<VSucursalCombo> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from d in db.Sucursal
                                      select new VSucursalCombo
                                      {
                                          Id = d.Id,
                                          Descripcion = d.Descrip
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSucursalLista> ListarSucursales()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Sucursal.Select(d => new VSucursalLista
                    {
                        Descripcion = d.Descrip,
                        Direccion = d.Direcc,
                        Id = d.Id,
                        Telefono = d.Telef
                    }).ToList();

                    return listResult;
                }
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
                using (var db = GetEsquema())
                {
                    var listResult = db.Almacen
                        .Include("TipoAlmacen1")
                        .Where(a => a.Sucursal.Id == Id)
                        .Select(a => new VAlmacenLista
                        {
                            Id = a.Id,
                            Descripcion = a.Descrip,
                            Direccion = a.Direcc,
                            Telefono = a.Telef,
                            Encargado = a.Encargado,
                            Sucursal = a.Sucursal.Descrip,
                            SucursalId = a.IdSuc,
                            TipoAlmacen = a.TipoAlmacen1.TipoAlmacen1,
                            TipoAlmacenId = a.TipoAlmacen.Value
                        })
                        .ToList();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Guardar(VSucursal vSucursal)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var sucursal = new Sucursal
                    {
                        Descrip = vSucursal.Descripcion,
                        Direcc = vSucursal.Direccion,
                        Fecha = DateTime.Now,
                        Hora = DateTime.Now.ToShortTimeString(),
                        Latit = vSucursal.Latitud,
                        Longi = vSucursal.Longitud,
                        Telef = vSucursal.Telefono,
                        Usuario = vSucursal.Usuario,
                        Estado = vSucursal.Estado,
                        Imagen = vSucursal.Imagen
                    };

                    db.Sucursal.Add(sucursal);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
