using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Almacen.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UTILITY.Global;

namespace REPOSITORY.Clase
{
    public class RAlmacen : BaseConexion, IAlmacen
    {
        #region Transacciones
        public bool Guardar(VAlmacen vAlmacen, ref int Id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var aux = Id;
                    Almacen almacen;
                    if (aux == 0)
                    {
                        almacen = new Almacen();
                        db.Almacen.Add(almacen);
                    }
                    else
                    {
                        almacen = db.Almacen.Where(a => a.Id == aux).FirstOrDefault();
                    }
                    almacen.Descrip = vAlmacen.Descripcion;
                    almacen.Direcc = vAlmacen.Direccion;
                    almacen.Fecha = DateTime.Now;
                    almacen.Hora = DateTime.Now.ToShortTimeString();
                    almacen.Latit = vAlmacen.Latitud;
                    almacen.Longi = vAlmacen.Longitud;
                    almacen.Telef = vAlmacen.Telefono;
                    almacen.Usuario = vAlmacen.Usuario;
                    almacen.IdSuc = vAlmacen.IdSucursal;
                    almacen.TipoAlmacen = vAlmacen.TipoAlmacenId;
                    almacen.Imagen = vAlmacen.Imagen;
                    almacen.Encargado = vAlmacen.Encargado;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar(int Id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var aux = Id;
                    Almacen almacen = db.Almacen.Where(a => a.Id == aux).FirstOrDefault();
                    if (almacen == null)
                    {
                        throw new Exception("No se encontro el almacen");
                    }
                    //Eliminar TI001
                    var inventario = db.TI001.Where(a => a.icalm == aux).ToList();
                    foreach (var inv in inventario)
                    {
                        db.TI001.Remove(inv);
                    }
                    db.Almacen.Remove(almacen);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Transacciones

        public List<VAlmacenCombo> Listar(int usuarioId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from b in db.Usuario
                                      join c in db.Usuario_01 on b.IdUsuario equals c.IdUsuario
                                      join a in db.Almacen on c.IdAlmacen equals a.Id
                                      where b.IdUsuario == usuarioId && c.Acceso == true
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
        #endregion



    }
}
