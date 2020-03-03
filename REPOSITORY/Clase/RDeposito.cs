using ENTITY.inv.Deposito;
using ENTITY.inv.Sucursal.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RDeposito : BaseConexion, IDeposito
    {
        public List<VDepositoCombo> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from d in db.Deposito
                                      select new VDepositoCombo
                                      {
                                          Id = d.Id,
                                          Descripcion = d.Descrip
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VDepositoLista> ListarDepositos()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Deposito.Select(d => new VDepositoLista
                    {
                        Descripcion = d.Descrip,
                        Direccion = d.Direcc,
                        Id = d.Id,
                        Telefono = d.Telef
                    }).ToList();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VSucursalLista> ListarSucursalXDepositoId(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Sucursal
                        .Where(s => s.Deposito.Id == Id)
                        .Select(s => new VSucursalLista
                        {
                            Id = s.Id,
                            Descripcion = s.Descrip,
                            Direccion = s.Direcc,
                            Telefono = s.Telef,
                            Deposito = s.Deposito.Descrip
                        })
                        .ToList();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
