using ENTITY.DiSoft.Personal;
using REPOSITORY.Clase.DiSoft;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class.DiSoft
{
   public class LPersonal
    {
        protected IPersonal iPersonal;
        public LPersonal()
        {
            iPersonal = new RPersonal();
        }
        public List<VPersonalCombo> PersonalCombo()
        {
            try
            {
                return iPersonal.PersonalCombo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
