using ENTITY.DiSoft.Zona;
using REPOSITORY.Clase.DiSoft;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class.DiSoft
{
    public class LZona
    {
        protected IZonaD iZona;
        public LZona()
        {
            iZona = new RZonaD();
        }
        public List<VZona> Listar()
        {
            try
            {
                return iZona.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
