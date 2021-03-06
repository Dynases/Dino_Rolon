﻿using DATA.EntityDataModel.DiSoft;
using ENTITY.Cliente.View;
using ENTITY.DiSoft.Zona;
using REPOSITORY.Base;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase.DiSoft
{
    public class RClienteD : BaseConexion2,IClienteD
    {
        public bool Guardar(VCliente vcliente, ref int idCliente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TC004 cliente;
                    TC004B credito;
                    var aux = idCliente;
                    cliente = db.TC004.Where(a => a.ccnumi == aux).FirstOrDefault();
                            
                    if (cliente == null)
                    {
                        cliente = new TC004();
                        db.TC004.Add(cliente);
                        cliente.ccnumi = idCliente;
                    }
                    if (vcliente.TipoCliente == 2)
                    {
                        credito = db.TC004B.Where(c => c.ccbnumi == aux).FirstOrDefault();
                        if (credito == null)
                        {
                            credito = new TC004B();
                            db.TC004B.Add(credito);
                        }
                        //Guarda elcredito
                        credito.ccbnumi = cliente.ccnumi;
                        credito.ccbzona = cliente.cczona;
                        credito.ccbtcre = vcliente.TipoCliente;
                        credito.ccLimite = vcliente.TotalCred;
                        credito.ccDias = vcliente.Dias;
                        cliente.cccod = vcliente.IdSpyre;
                    }
                    cliente.ccdesc = vcliente.Descripcion;
                    cliente.cczona = vcliente.Ciudad; 
                    cliente.ccdct = 1; // PARA QUE SIRVE
                    cliente.ccdctnum ="0"; //  IGUAL AQUI
                    cliente.ccdirec = vcliente.Direcccion;
                    cliente.cctelf1 = vcliente.Telfono1;
                    cliente.cctelf2 = vcliente.Telfono2;
                    cliente.cccat = vcliente.IdCategoria; // CATEGORIA 
                    cliente.ccest = vcliente.Estado;
                    cliente.cclongi = vcliente.Longittud;
                    cliente.cclat = vcliente.Latitud;
                    cliente.ccprconsu = -1; //PREGUNTAR QUE ES?
                    cliente.cceven = false;
                    cliente.ccobs = "";
                    cliente.ccfnac = DateTime.Today;
                    cliente.ccnomfac = vcliente.RazonSocial;
                    cliente.ccnit = vcliente.Nit;
                    cliente.ccultped = DateTime.Today;
                    cliente.ccfecing = DateTime.Today;
                    cliente.ccultvent = DateTime.Today;
                    cliente.ccrecven = 1; //para que es esto
                    cliente.ccsupven = 3;// nose
                    cliente.ccpreven = 2;// nose
                    cliente.ccemail = vcliente.Email1;
                    cliente.ccref = "";
                    cliente.ccpass = "";
                    cliente.cchact = vcliente.Hora;
                    cliente.ccfact = vcliente.Fecha;
                    cliente.ccuact = vcliente.Usuario;
                    idCliente = cliente.ccnumi;

                    db.SaveChanges();
                    return true;
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
                using (var db = GetEsquema())
                {
                    var validarPedido = db.TO001.Where(a => a.oaccli == IdCliente).Count();
                    if (validarPedido > 0)
                    {
                        throw new Exception("El cliente esta relacionado en "+ validarPedido.ToString() +" transacciones de Pedidos");
                    }
                    var cliente = db.TC004.Where(a => a.ccnumi == IdCliente).FirstOrDefault();
                    var credito = db.TC004B.Where(c => c.ccbnumi == IdCliente).FirstOrDefault();
                    var tC0041 = db.TC0041.Where(c => c.chnumi == IdCliente).FirstOrDefault();
                    var tC0042 = db.TC0042.Where(c => c.cpcli == IdCliente).FirstOrDefault();
                    if (cliente != null)
                    {
                        db.TC004.Remove(cliente);
                    }
                    if (credito != null)
                    {
                        db.TC004B.Remove(credito);
                    }
                    if (tC0041 != null)
                    {
                        db.TC0041.Remove(tC0041);
                    }
                    if (tC0042 != null)
                    {
                        db.TC0042.Remove(tC0042);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Consulta
        public Decimal? saldoPendienteCredito(int clienteId)
        {

            try
            {
                using (var db = GetEsquema())
                {                  
                    decimal? saldoPendiente =  db.App_Listado_CobrosPendientes
                                                .Where(x => x.ClienteId == clienteId)
                                                .Sum(x => x.pendiente);
                    saldoPendiente = saldoPendiente == null ? 0 : saldoPendiente;
                    return saldoPendiente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
