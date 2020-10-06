using ENTITY.DiSoft.Personal;
using LOGIC.Class.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE
{
    public partial class Service1 : IServiceDesktop
    {
        #region Consulta
        public List<VPersonalCombo> PersonalCombo()
        {
            try
            {
                var listResult = new LPersonal().PersonalCombo();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}