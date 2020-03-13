using DATA.EntityDataModel.DiAvi;
using ENTITY.Plantilla;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RPlantilla : BaseConexion, IPlantilla
    {
        #region Trasancciones

        public bool Guardar(VPlantilla vPlantilla, ref int id)
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
                    id = plantilla.Id;
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

        public List<VPlantilla> Listar(ENConceptoPlantilla concepto)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listresult = db.Plantilla
                                         .Where(p => p.Concepto == Convert.ToInt32(concepto))
                                         .Select(p => new VPlantilla
                                         {
                                             Concepto = p.Concepto,
                                             Id = p.Id,
                                             IdAlmacen = p.IdAlmacen,
                                             IdAlmacenDestino = p.IdAlmacenDestino,
                                             NombreAlmacen = p.Almacen.Descrip,
                                             NombreAlmacenDestino = p.Almacen1.Descrip,
                                             Nombre = p.Nombre
                                         }).ToList();

                    return listresult;
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
