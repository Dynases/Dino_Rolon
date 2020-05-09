using DATA.EntityDataModel.DiSoft;
using ENTITY.DiSoft.Libreria;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase.DiSoft
{
   public class RLibreriaD : BaseConexion2, ILibreriaD
    {
        #region Consultas
        #endregion
        #region Transacciones
        public bool Guardar(VLibreriaD vLibreria)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var libreria = new TC0051();
                    libreria.cecon = vLibreria.cecon;
                    libreria.cenum = vLibreria.cenum;
                    libreria.cedesc = vLibreria.cedesc;
                    libreria.cefact = vLibreria.cefact;
                    libreria.cehact = vLibreria.cehact;
                    libreria.ceuact = vLibreria.ceuact;
                    db.TC0051.Add(libreria);
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
        #region Metodos
        #endregion


    }
}
