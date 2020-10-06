using ENTITY.DiSoft.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SERVICE
{
    public partial interface IServiceDesktop
    {
        /**********   Personal ******************/
       
        #region Transacciones
        #endregion
        #region Consulta
        [OperationContract]
        List<VPersonalCombo> PersonalCombo();
        #endregion
    }
}