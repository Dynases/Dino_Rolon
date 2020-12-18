using DATA.EntityDataModel.DiSoft;
using ENTITY.DiSoft.Pedido.View;
using REPOSITORY.Base;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase.DiSoft
{
    public class RPedidoD : BaseConexion2, IPedidoD
    {
        #region Pedido
        #region Transacciones
        public void Guardar(VPedidoD pedido, ref int id, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    TO001 data;
                 
                    if (id > 0)
                    {
                        data = db.TO001.Where(a => a.oanumi == idAux).FirstOrDefault();
                        if (data == null)
                            throw new Exception("No existe el pedido con id " + idAux);
                    }
                    else
                    {
                        data = new TO001();
                        db.TO001.Add(data);
                        data.oanumi = db.TO001.Select(a => a.oanumi).DefaultIfEmpty(0).Max() + 1;
                    }
                    data.oahora = DateTime.Now.ToString("HH:mm"); 
                    data.oafdoc = pedido.FechaReg;
                    data.oaccli = pedido.ClienteId;
                    data.oazona = db.TC004.Where(x => x.ccnumi == pedido.ClienteId).FirstOrDefault().cczona;
                    data.oarepa = pedido.VendedorId;
                    data.oaobs = pedido.Observacion;
                    data.oaobs2 = "";
                    data.oaest = (int)ENEstado.GUARDADO;
                    data.oaap = (int)ENEstado.GUARDADO;
                    data.oapg = 0;
                    data.oafact = DateTime.Today;
                    data.oahact = DateTime.Now.ToString("HH:mm");
                    data.oauact = usuario;                   
                    db.SaveChanges();
                    id = data.oanumi;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GuardarPedidoDirecto(int id, int RepartidorId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TO001C data;
                    data = new TO001C();
                    data.oacoanumi = id;
                    data.oaccbnumi = RepartidorId;
                    data.oacfdoc = DateTime.Today;
                    db.TO001C.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ModificarEstadoPedido(int id, int EstadoPedido)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TO001 data;
                    data = db.TO001.Where(a => a.oanumi == id).FirstOrDefault();
                    if (data == null)
                        throw new Exception("No existe el pedido con id " + id);
                    data.oaest = EstadoPedido;               
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GuardarExtencionPedido(int id, int VendedorId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TO001A data;
                    data = new TO001A();
                    data.oaato1numi = id;
                    data.oaanumiprev = VendedorId;
                    data.oaaentrega = 2;
                    data.oaacaja = 0;
                    db.TO001A.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar(int PedidoId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    //Cambia de estaodo
                    var pedido = db.TO001.Where(c => c.oanumi.Equals(PedidoId)).FirstOrDefault();
                    var credito = db.TO001A1.Where(c => c.ognumi.Equals(PedidoId)).FirstOrDefault();
                    if (pedido == null)
                        throw new Exception("No existe el pedido con id " + PedidoId);
                    if (credito != null)
                    {
                        db.TO001A1.Remove(credito);
                    }
                    pedido.oaap = 2;         
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas     
        public VPedidoD ObtenerPorId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TO001
                        .Where(a => a.oanumi == id)
                        .Select(a => new VPedidoD
                        {
                            Id = a.oanumi,
                            FechaReg = a.oafdoc.Value ,
                            HoraReg = a.oahora,
                            ClienteId = a.oaccli.Value,
                            ZonaId = a.oazona.Value,
                            VendedorId = a.oarepa.Value,
                            Observacion = a.oaobs,
                            Observacion2 = a.oaobs2,
                            EstadoPedido = a.oaest.Value,
                            Estado = a.oaap.Value                            
                }).FirstOrDefault();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion
        #region PedidoDetalle
        #region Transacciones
        public void GuardarDetalle(List<VPedidoProductoD> detalle, int id, int tipoPedido)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TO0011 data;
                    TO001A1 data2;
                    foreach (var item in detalle)
                    {
                        var idProducto = Convert.ToInt32(item.ProductoId);
                        data = db.TO0011.Where(a => a.obnumi == id && a.obcprod == item.ProductoId).FirstOrDefault();
                        if (data != null)
                        {
                            db.TO0011.Remove(data);
                        }

                        data = new TO0011();
                        data.obnumi = id;
                        data.obcprod = item.ProductoId;
                        data.obpcant = item.Cantidad;
                        data.obpbase = item.Precio;
                        data.obptot = item.SubTotal;
                        data.obdesc = item.Descuento;
                        data.obtotal = item.Total;
                        data.obfamilia = item.Familia;
                        data.obcampo1 = 0;
                        data.obcampo2 = 0;
                        db.TO0011.Add(data);
                        db.SaveChanges();                       
                    }
                    //Guardar credito del pedido
                    if (tipoPedido == 2)
                    {
                        data2 = new TO001A1();
                        data2.ognumi = id;
                        data2.ogcred = detalle.Sum(x=> x.Total);
                        db.TO001A1.Add(data2);
                        db.SaveChanges();
                    }                  
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        
        #endregion
        #endregion
    }
}
