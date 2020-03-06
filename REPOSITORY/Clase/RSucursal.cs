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
                        .Where(s => s.Sucursal.Id == Id)
                        .Select(s => new VAlmacenLista
                        {
                            Id = s.Id,
                            Descripcion = s.Descrip,
                            Direccion = s.Direcc,
                            Telefono = s.Telef,
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
        public bool Guardar(VSucursal vDeposito)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var deposito = new Sucursal
                    {
                        Descrip = vDeposito.Descripcion,
                        Direcc = vDeposito.Direccion,
                        Fecha = DateTime.Now,
                        Hora = DateTime.Now.ToShortTimeString(),
                        Latit = vDeposito.Latitud,
                        Longi = vDeposito.Longitud,
                        Telef = vDeposito.Telefono,
                        Usuario = vDeposito.Usuario,
                        Estado = vDeposito.Estado,
                        Id = vDeposito.Id,
                        Imagen = vDeposito.Imagen
                    };

                    db.Sucursal.Add(deposito);
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
