using DATA.EntityDataModel.DiSoft;
using ENTITY.DiSoft.Libreria;
using ENTITY.Libreria.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase.DiSoft
{
   public class RLibreriaD : BaseConexion2, ILibreriaD
    {
        #region Consultas
        #endregion
        #region Transacciones
        public bool Guardar(VLibreriaLista vLibreria)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    switch (vLibreria.IdOrden)
                    {
                        case (int)ENEstaticosOrden.PRODUCTO_GRUPO1:
                            vLibreria.IdOrden = 101;
                            break;
                        case (int)ENEstaticosOrden.PRODUCTO_GRUPO2:
                            vLibreria.IdOrden = 102;
                            break;
                        case (int)ENEstaticosOrden.PRODUCTO_GRUPO3:
                            vLibreria.IdOrden = 103;
                            break;
                        case (int)ENEstaticosOrden.PRODUCTO_GRUPO4:
                            vLibreria.IdOrden = 104;
                            break;
                        case (int)ENEstaticosOrden.PRODUCTO_GRUPO5:
                            vLibreria.IdOrden = 105;
                            break;

                        case (int)ENEstaticosOrden.PRODUCTO_UN_VENTA:
                            vLibreria.IdOrden = 106;
                            break;
                    }
                    var libreria = new TC0051();
                    libreria.cecon = vLibreria.IdOrden;
                    libreria.cenum = vLibreria.IdLibrer;
                    libreria.cedesc = vLibreria.Descrip;
                    libreria.cefact = vLibreria.Fecha;
                    libreria.cehact = vLibreria.Hora;
                    libreria.ceuact = vLibreria.Usuario;
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
