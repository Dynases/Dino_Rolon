using DATA.EntityDataModel.DiAvi;
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
                        IdDepos = vSucursal.IdDeposito,
                        Id = vSucursal.Id,
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

        public List<VSucursalCombo> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Sucursal
                                      select new VSucursalCombo
                                      {
                                          IdLibreria = a.Id,
                                          Descripcion = a.Descrip + " | " + a.Deposito.Descrip
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
                    var listResult = db.Sucursal.Select(s => new VSucursalLista
                    {
                        Id = s.Id,
                        Descripcion = s.Descrip,
                        Deposito = s.Deposito.Descrip,
                        Direccion = s.Direcc,
                        Telefono = s.Telef,
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
