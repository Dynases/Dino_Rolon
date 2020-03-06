﻿using ENTITY.inv.Traspaso.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LTraspaso
    {
        protected ITraspaso iTraspaso;

        public LTraspaso()
        {
            iTraspaso = new RTraspaso();
        }

        #region Transacciones

        public bool Guardar(VTraspaso vTraspaso)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iTraspaso.Guardar(vTraspaso);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consulta

        public List<VTraspaso> Listar()
        {
            try
            {
                return this.iTraspaso.ListarTraspasos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
