
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Cliente.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum;
using ENTITY.Telefono.View;
using UTILITY.Enum.EnEstaticos;
using System.Data.Entity;

namespace REPOSITORY.Clase
{

    public class RCliente : BaseConexion, ICliente
    {
        public bool Guardar(VCliente vcliente, ref int idCLiente)
        {
            try
            {                
                using (var db = GetEsquema())
                {
                    var cliente = new Cliente();
                    cliente.IdSpyre = vcliente.IdSpyre;
                    cliente.Estado = vcliente.Estado;
                    cliente.Descrip = vcliente.Descripcion;
                    cliente.RazonSo = vcliente.RazonSocial;
                    cliente.Nit = vcliente.Nit;
                    cliente.TipoCli = vcliente.TipoCliente;
                    cliente.Direcc = vcliente.Direcccion;
                    cliente.Contac1 = vcliente.Contacto1;
                    cliente.Contac2 = vcliente.Contacto2;
                    cliente.Telfo1 = vcliente.Telfono1;
                    cliente.Telfo2 = vcliente.Telfono2;
                    cliente.Email1 = vcliente.Email1;
                    cliente.Email2 = vcliente.Email2;
                    cliente.Ciudad = vcliente.Ciudad;
                    cliente.Factur = vcliente.Facturacion;
                    cliente.Longit = vcliente.Longittud;
                    cliente.Latitud = vcliente.Latitud;
                    cliente.Imagen = vcliente.Imagen;
                    cliente.Fecha = vcliente.Fecha;
                    cliente.Hora = vcliente.Hora;
                    cliente.Usuario = vcliente.Usuario;
                    db.Cliente.Add(cliente);
                    db.SaveChanges();
                    idCLiente = cliente.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCliente vcliente, int idCLiente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var cliente = db.Cliente.FirstOrDefault(b => b.Id == idCLiente);
                    cliente.IdSpyre = vcliente.IdSpyre;
                    cliente.Estado = vcliente.Estado;
                    cliente.Descrip = vcliente.Descripcion;
                    cliente.RazonSo = vcliente.RazonSocial;
                    cliente.Nit = vcliente.Nit;
                    cliente.TipoCli = vcliente.TipoCliente;
                    cliente.Direcc = vcliente.Direcccion;
                    cliente.Contac1 = vcliente.Contacto1;
                    cliente.Contac2 = vcliente.Contacto2;
                    cliente.Telfo1 = vcliente.Telfono1;
                    cliente.Telfo2 = vcliente.Telfono2;
                    cliente.Email1 = vcliente.Email1;
                    cliente.Email2 = vcliente.Email2;
                    cliente.Ciudad = vcliente.Ciudad;
                    cliente.Factur = vcliente.Facturacion;
                    cliente.Longit = vcliente.Longittud;
                    cliente.Latitud = vcliente.Latitud;
                    cliente.Imagen = vcliente.Imagen;
                    cliente.Fecha = vcliente.Fecha;
                    cliente.Hora = vcliente.Hora;
                    cliente.Usuario = vcliente.Usuario;
                    db.Cliente.Attach(cliente);
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                    idCLiente = cliente.Id;
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
                    var cliente = db.Cliente.FirstOrDefault(b => b.Id == IdCliente);
                    db.Cliente.Remove(cliente);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //         using (var db = GetEsquema())
        //                {
        //                    var cliente = new Cliente();
        //    cliente.IdSpyre = vcliente.IdSpyre;
        //                    cliente.Estado = vcliente.Estado;
        //                    cliente.Descrip = vcliente.Descripcion;
        //                    cliente.RazonSo = vcliente.RazonSocial;
        //                    cliente.Nit = vcliente.Nit;
        //                    cliente.TipoCli = vcliente.TipoCliente;
        //                    cliente.Nit = vcliente.Nit;
        //                    cliente.Nit = vcliente.Nit;
        //                    foreach (var vtelefono in vcliente.Telefonos)
        //                    {
        //                        var telefono = new Telefono
        //                        {
        //                            Descripcion = vtelefono.Descripcion,
        //                            Tipo = vtelefono.Tipo
        //                        };
        //    cliente.Telefono.Add(telefono);
        //                    }
        //db.Cliente.Add(cliente);
        //                    db.SaveChanges();
        //                    idCLiente = cliente.Id;
        //                    return true;
        //                }
        //public List<VCliente> Listar(string Nombre)
        //{
        //    try
        //    {
        //        using (var db= GetEsquema())
        //        {
        //            var listResult = (from a in db.Cliente
        //                              where a.Nombre.Contains(Nombre)
        //                              select new VCliente
        //                              {
        //                                  Id = a.Id,
        //                                  Nombre = a.Nombre,
        //                                  Apellido = a.Apellido,
        //                                  Direccion = a.Direccion,
        //                                  //Telefonos = a.Telefono.Where(b=>b.Tipo==(int)ENTipoTelefono.Celular).FirstOrDefault() != null ? a.Telefono.FirstOrDefault().Descripcion : string.Empty
        //                                  Telefonos = (from b in a.Telefono
        //                                               select new VTelefono
        //                                               {
        //                                                   Id = b.Id,
        //                                                   Descripcion = b.Descripcion,
        //                                                   Tipo = b.Tipo,
        //                                               }).ToList()
        //                              }).ToList();
        //            return listResult;
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        public List<VCliente> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Cliente
                                      select new VCliente
                                      {
                                          Id = a.Id,
                                          IdSpyre = a.IdSpyre,
                                          Descripcion = a.Descrip,
                                          RazonSocial = a.RazonSo,
                                          TipoCliente = a.TipoCli,
                                          Nit = a.Nit,
                                          Direcccion = a.Direcc,
                                          Contacto1 = a.Contac1,
                                          Contacto2 = a.Contac2,
                                          Telfono1 = a.Telfo1,
                                          Telfono2= a.Telfo2,
                                          Email1= a.Email1,
                                          Email2 = a.Email2,
                                          Ciudad = a.Ciudad,
                                          Facturacion = a.Factur
                                      }).ToList();
                    return listResult;
                }
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
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Cliente
                                      join c in db.Libreria on 
                                        new {
                                            Grupo = 1,
                                            Orden = 1,                                         
                                            Libreria =a.Ciudad
                                        }
                                        equals
                                        new {
                                            Grupo = c.IdGrupo,
                                            Orden = c.IdOrden,
                                            Libreria = c.IdLibrer
                                        }                                    
                                      select new VClienteLista
                                      {
                                          Id = a.Id,                                          
                                          Descripcion = a.Descrip,
                                          RazonSocial = a.RazonSo, 
                                          Ciudad =a.Ciudad,
                                          NombreCiudad = c.Descrip,
                                          Contacto1 = a.Contac1,
                                          Contacto2 = a.Contac2,                                    
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCliente> ListarCliente(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Cliente
                                      where a.Id.Equals(id)
                                      select new VCliente
                                      {
                                          Id = a.Id,
                                          IdSpyre = a.IdSpyre,
                                          Descripcion = a.Descrip,
                                          RazonSocial = a.RazonSo,
                                          TipoCliente = a.TipoCli,
                                          Nit = a.Nit,
                                          Direcccion = a.Direcc,
                                          Contacto1 = a.Contac1,
                                          Contacto2 = a.Contac2,
                                          Telfono1 = a.Telfo1,
                                          Telfono2 = a.Telfo2,
                                          Email1 = a.Email1,
                                          Email2 = a.Email2,
                                          Ciudad = a.Ciudad,
                                          Facturacion = a.Factur,
                                          Latitud = a.Latitud,
                                          Longittud = a.Longit,
                                          Imagen = a.Imagen
                                      }).ToList();
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