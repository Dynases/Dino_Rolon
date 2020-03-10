using DATA.EntityDataModel.DiAvi;
using ENTITY.Plantilla;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RPlantilla : BaseConexion, IPlantilla
    {
        #region Trasancciones

        public bool Guardar(VPlantilla vPlantilla)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var plantilla = new Plantilla
                    {
                        IdAlmacen = vPlantilla.IdAlmacen,
                        IdAlmacenDestino = vPlantilla.IdAlmacenDestino,
                        Concepto = vPlantilla.Concepto,
                        Nombre = vPlantilla.Nombre
                    };

                    db.Plantilla.Add(plantilla);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consultas

        public List<VPlantilla> Listar()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.Plantilla.Select(p => new VPlantilla
                    {
                        Concepto = p.Concepto,
                        Id = p.Id,
                        IdAlmacen = p.IdAlmacen,
                        IdAlmacenDestino = p.IdAlmacenDestino,
                        NombreAlmacen = p.Almacen.Descrip,
                        NombreAlmacenDestino = p.Almacen1.Descrip,
                        Nombre = p.Nombre
                    }).ToList();
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
