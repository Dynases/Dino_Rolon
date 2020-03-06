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
        public bool Guardar(VTipoAlmacen vtipoAlmacen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var tipoAlmacen = new TipoAlmacen
                    {
                        Descripcion = vtipoAlmacen.Descripcion,
                        Id = vtipoAlmacen.Id,
                        TipoAlmacen1 = vtipoAlmacen.Nombre
                    };

                    db.TipoAlmacen.Add(tipoAlmacen);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
                                 Nombre = tp.TipoAlmacen1
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

                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
