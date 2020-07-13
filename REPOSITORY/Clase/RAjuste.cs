using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Ajuste.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
   public class RAjuste : BaseConexion, IAjuste
    {
        #region Ajuste
        #region Transacciones
        public void Guardar(VAjuste ajuste, ref int id, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    TI002 data;
                    if (id > 0)
                    {
                        data = db.TI002.Where(a => a.ibid == idAux).FirstOrDefault();
                        if (data == null)
                            throw new Exception("No existe el ajuste con id " + idAux);
                    }
                    else
                    {
                        data = new TI002();
                        db.TI002.Add(data);
                    }
                    data.ibid = db.TI002.Select(a => a.ibid).DefaultIfEmpty(0).Max() + 1;
                    data.ibfdoc = ajuste.Fecha;
                    data.ibconcep = ajuste.IdConcepto;
                    data.ibobs = ajuste.Obs;
                    data.ibalm = ajuste.IdAlmacen;
                    data.ibdepdest = 0;
                    data.ididdestino = 0;
                    data.ibiddc = 0;
                    data.ibfact = DateTime.Today;
                    data.ibhact = DateTime.Now.ToString("HH:mm");
                    data.ibuact = usuario;
                    db.SaveChanges();
                    id = data.ibid;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar(int IdAjuste)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var ajuste = db.TI002.Where(c => c.ibid.Equals(IdAjuste)).FirstOrDefault();                  
                    db.TI002.Remove(ajuste);                 
                    db.SaveChanges();               
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        private static readonly Expression<Func<TI002, VAjusteLista>> CONVERT_LIST_VALUE = (item) => new VAjusteLista()
        {
            Id = item.ibid,
            Fecha = item.ibfdoc,
            NConcepto = item.TCI001.cpdesc,
            Obs = item.ibobs,
            NAlmacen = item.Almacen.Descrip + " | Sucursal : " + item.Almacen.Sucursal.Descrip,         
        };

        public List<VAjusteLista> Lista()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TI002
                        .Where(a => a.ibconcep == (int)ENConcepto.INGRESO || a.ibconcep == (int)ENConcepto.SALIDA)
                        .OrderByDescending(a => a.ibid);
                    return query.Select(CONVERT_LIST_VALUE).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VAjuste ObtenerPorId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TI002
                        .Where(a => a.ibid == id)
                        .Select(a => new VAjuste
                        {
                            Id = a.ibid,
                            Fecha = a.ibfdoc,
                            IdConcepto = a.ibconcep,
                            IdAlmacen = a.ibalm,
                            Obs = a.ibobs,                        
                        }).FirstOrDefault();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        #region AjusteDetalle
        #region Transacciones
        public void GuardarDetalle(List<VAjusteDetalle> detalle, int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TI0021 data;
                    TI0021A data2;
                    foreach (var item in detalle)
                    {
                        switch (item.Estado)
                        {
                            case (int)ENEstado.NUEVO:
                                data = new TI0021();
                                data2 = new TI0021A();

                                data2.Precio = item.Precio;
                                data2.TI0021 = data;
                                data2.Unidad = item.Unidad;
                                data2.Contenido = item.Contenido;

                                data.icid = db.TI0021.Select(a => a.icid).DefaultIfEmpty(0).Max() + 1;
                                data.icibid = id;
                                data.iccprod = item.IdProducto;
                                data.iccant = item.Cantidad;
                                data.iclot = item.Lote;
                                data.icfvenc = item.FechaVen;

                                db.TI0021.Add(data);
                                db.TI0021A.Add(data2);
                                break;
                            case (int)ENEstado.MODIFICAR:
                                data = db.TI0021.Where(a => a.icid == item.Id).FirstOrDefault();
                                data2 = data.TI0021A;

                                data2.Precio = item.Precio;
                                data2.TI0021 = data;
                                data2.Unidad = item.Unidad;
                                data2.Contenido = item.Contenido;

                                data.iccprod = item.IdProducto;
                                data.iccant = item.Cantidad;
                                data.iclot = item.Lote;
                                data.icfvenc = item.FechaVen;
                                break;
                            case (int)ENEstado.ELIMINAR:
                                data = db.TI0021.Where(a => a.icid == item.Id).FirstOrDefault();
                                data2 = data.TI0021A;

                                db.TI0021A.Remove(data2);
                                db.TI0021.Remove(data);
                                break;
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        public List<VAjusteDetalle> ListaDetalle(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.TI0021
                                      join b in db.Producto on a.iccprod equals b.Id
                                      join c in db.Libreria on b.UniVen equals c.IdLibrer
                                      where c.IdGrupo.Equals((int)ENEstaticosGrupo.PRODUCTO)
                                      && c.IdOrden.Equals((int)ENEstaticosOrden.PRODUCTO_UN_VENTA)
                                      && a.iccprod == id
                                      select (new VAjusteDetalle()
                                      {
                                          Id = a.icid,
                                          IdAjuste = a.icibid,
                                          IdProducto = b.Id,
                                          CodProducto = b.IdProd,
                                          NProducto = b.Descrip,
                                          Unidad = c.Descrip,
                                          Cantidad = a.iccant,
                                          Contenido = a.TI0021A.Contenido,
                                          TotalContenido = (a.TI0021A.IdTI0021 * a.TI0021A.Contenido),                                          
                                          Precio = a.TI0021A.Precio,
                                          Total = (a.iccant * a.TI0021A.Precio),
                                          Lote = a.iclot,
                                          FechaVen = a.icfvenc
                                      })).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion
    }
}
