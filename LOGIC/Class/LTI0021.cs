using ENTITY.inv.TI0021.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
    public class LTI0021
    {
        protected ITI0021 iTi0021;
        public LTI0021()
        {
            iTi0021 = new RTI0021();
        }
       
    }
}
