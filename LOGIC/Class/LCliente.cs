using ENTITY.adm.ValidacioinPrograma;
using ENTITY.Cliente.View;
using REPOSITORY.Clase;
using REPOSITORY.Clase.DiSoft;
using REPOSITORY.Interface;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace LOGIC.Class
{
    public class LCliente
    {
        protected ICliente iCliente;
        protected IClienteD iClienteD;
        protected IZonaD iZonaD;

        public LCliente()
        {
            iCliente = new RCliente();
            iClienteD = new RClienteD();
            iZonaD = new RZonaD();
        }

        #region Consultas
      
        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VCliente> ListarCliente(int id)
        {
            try
            {
                
                return iCliente.ListarCliente(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal TraerLimiteCredito(int idCliente)
        {
            try
            {
                return iCliente.TraerLimiteCredito(idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VCliente> Listar()
        {
            try
            {
                var lista = iCliente.Listar();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VClienteLista> ListarClientes()
        {
            try
            {
                List<VClienteLista> lista = new List<VClienteLista>();
                lista = iCliente.ListarClientes();
                //Anade la Zona desde el disoft
                lista = iZonaD.ListarClienteAdicionarZona(lista);
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado()
        {
            try
            {
                return iCliente.ListarEncabezado();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VClienteCombo> TraerClienteCombo()       
        {
            try
            {
                var lista = iCliente.TraerClienteCombo();    
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region Transacciones
        public bool Guardar(VCliente vcliente, ref int idCliente)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    bool result = false;
                     result = iCliente.Guardar(vcliente, ref idCliente);
                    //Guarda en el cliente 
                     result = iClienteD.Guardar(vcliente, ref idCliente);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCliente vcliente, int idCliente)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    bool result = false;
                    result = iCliente.Modificar(vcliente, idCliente);
                    //Guarda en el cliente 
                    result = iClienteD.Guardar(vcliente,ref idCliente);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdCliente)
        {
            try
            {
                FValidacionPrograma validacionPrograma = new FValidacionPrograma();
                validacionPrograma.tablaOrigen = "REG.CLIENTE";
                List<string> mensaje = new List<string>();
                if (new LValidacionPrograma().ValidadrEliminacion(IdCliente, validacionPrograma, ref mensaje, false))
                {
                    using (var scope = new TransactionScope())
                    {
                        bool result = false;
                        result = iCliente.Eliminar(IdCliente);
                        //ELIMINA EL CLIENTE DE DISOFT
                        result = iClienteD.Eliminar(IdCliente);
                        scope.Complete();
                        return result;
                    }
                }
                return false;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool ExisteEnVenta(int idCliente)
        {
            try
            {
                return iCliente.ExisteEnVenta(idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EsClienteCredito(int idCliente)
        {
            try
            {
                return iCliente.EsClienteCredito(idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}
