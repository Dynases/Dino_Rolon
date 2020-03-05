﻿using DATA.EntityDataModel.DiAvi;
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
                        Id = vAlmacen.Id,
                        Imagen = vAlmacen.Imagen
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
                                          Descripcion = a.Descrip + " | " + a.Sucursal.Descrip
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
                        Direccion = al.Direcc,
                        Telefono = al.Telef,
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
