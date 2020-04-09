using DATA.EntityDataModel.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Base
{
    public class BaseConexion2 : BaseConexionPrincipal<BDDistBHF_RolonEntities>
    {
        protected override BDDistBHF_RolonEntities GetEsquema()
        {
            return new BDDistBHF_RolonEntities();
        }
        private static BaseConexion2 _instancia;
        public static BaseConexion2 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new BaseConexion2();
                }
                return _instancia;
            }
        }
    }
}
