using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.reg.PrecioCategoria.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
    public class RPrecioCategoria : BaseConexion, IPrecioCategoria
    {
        #region Transaccion
        public bool Guardar(VPrecioCategoria vPrecioCat, ref int id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var idAux = id;
                    PrecioCat precioCat;
                    if (id > 0)
                    {
                        precioCat = db.PrecioCat.Where(a => a.Id == idAux).FirstOrDefault();
                        if (precioCat == null)
                            throw new Exception("No existe la categoria con id " + idAux);
                    }
                    else
                    {
                        precioCat = new PrecioCat();
                        db.PrecioCat.Add(precioCat);
                    }
                    precioCat.Cod = vPrecioCat.Cod;
                    precioCat.Descrip = vPrecioCat.Descrip;
                    precioCat.Margen = vPrecioCat.Margen;
                    precioCat.Tipo = vPrecioCat.Tipo;
                    precioCat.Fecha = vPrecioCat.Fecha;
                    precioCat.Hora = vPrecioCat.Hora;
                    precioCat.Usuario = vPrecioCat.Usuario;
                    db.SaveChanges();
                    id = precioCat.Id;
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
        public List<VPrecioCategoria> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.PrecioCat
                                      select new VPrecioCategoria
                                      {
                                          Id = a.Id,
                                          Cod = a.Cod,
                                          Descrip = a.Descrip,
                                          Tipo = a.Tipo,
                                          NombreTipo = a.Tipo == 1 ? "Venta" : "Compra",
                                          Margen = a.Margen,
                                          Estado = (int)ENEstado.GUARDADO,
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
