using DATA.EntityDataModel.DiAvi;
using ENTITY.Plantilla;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RPlantilla01 : BaseConexion, IPlantilla_01
    {
        #region Transaccion

        public bool Guardar(List<VPlantilla01> lista, int PlantillaId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var i in lista)
                    {
                        var detalle = new Plantilla_01
                        {
                            Cantidad = i.Cantidad,
                            IdPlantilla = PlantillaId,
                            IdProducto = i.IdProducto,
                            Precio = i.Precio,
                        };

                        db.Plantilla_01.Add(detalle);
                    }

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

        #region Consulta

        public List<VPlantilla01> ListarDetallePlantilla(int PlantillaId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.Plantilla_01.Where(p => p.IdPlantilla.Equals(PlantillaId))
                                          .Select(p => new VPlantilla01
                                          {
                                              Cantidad = p.Cantidad.Value,
                                              DescripcionProducto = p.Producto.Descrip,
                                              Id = p.Id,
                                              IdPlantilla = p.IdPlantilla,
                                              IdProducto = p.IdProducto,
                                              Precio = p.Precio.Value
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
