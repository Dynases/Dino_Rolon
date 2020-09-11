using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.TipoAlmacen.view;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTipoAlmacen : BaseConexion, ITipoAlmacen
    {
        #region Transacciones
        public void Guardar(VTipoAlmacen vtipoAlmacen, ref int Id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var aux = Id;
                    TipoAlmacen tipoAlmacen;
                    if (aux == 0)
                    {
                        tipoAlmacen = new TipoAlmacen();
                        db.TipoAlmacen.Add(tipoAlmacen);
                    }
                    else
                    {
                        tipoAlmacen = db.TipoAlmacen.Where(a => a.Id == aux).FirstOrDefault();
                    }
                    tipoAlmacen.Descripcion = vtipoAlmacen.Descripcion;
                    tipoAlmacen.TipoAlmacen1 = vtipoAlmacen.Nombre;
                    tipoAlmacen.TraspasoDirecto = vtipoAlmacen.TraspasoDirecto;
                    Id = tipoAlmacen.Id;
                    db.SaveChanges();
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
                    TipoAlmacen tipoAlmacen = db.TipoAlmacen.Where(a => a.Id == aux).FirstOrDefault();
                    if (tipoAlmacen == null)
                    {
                        throw new Exception("No se encontro el tipo de almacen");
                    }
                    db.TipoAlmacen.Remove(tipoAlmacen);
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
        public List<VTipoAlmacenCombo> ListarCombo()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.TipoAlmacen
                             .Select(tp => new VTipoAlmacenCombo
                             {
                                 Id = tp.Id,
                                 TipoAlmacen = tp.TipoAlmacen1
                             })
                             .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTipoAlmacenListar> Listar()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.TipoAlmacen
                        .Select(tp => new VTipoAlmacenListar
                        {
                            Id = tp.Id,
                            Nombre = tp.TipoAlmacen1,
                            Descripcion = tp.Descripcion,
                            TraspasoDirecto = tp.TraspasoDirecto
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones

        #endregion



    }
}
