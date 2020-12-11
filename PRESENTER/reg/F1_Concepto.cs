using DevComponents.DotNetBar;
using ENTITY.inv.Concepto.View;
using PRESENTER.frm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;
using UTILITY.MetodosExtencion;
using Ent = ENTITY.inv.Concepto.View.VConcepto;
using EntList = ENTITY.inv.Concepto.View.VConceptoLista;
namespace PRESENTER.reg
{
    public partial class F1_Concepto : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "CONCEPTO";
        int _MPos = 0;
        Ent _concepto = new Ent();
        List<EntList> _listado = new List<EntList>();
        #endregion
        public F1_Concepto()
        {
            InitializeComponent();
            BtnImprimir.Visible = false;
            MP_Iniciar();
            SuperTabBuscar.Visible = false;
        }
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                VConceptosBindingSource.DataSource = _concepto;
                this.Text = _NombreFormulario;
                LblTitulo.Text = _NombreFormulario;              
                MP_InHabilitar();
                MP_AsignarPermisos();
                MP_ArmarGrillaListado();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_AsignarPermisos()
        {
            try
            {
                DataTable dtRolUsu = new ServiceDesktop.ServiceDesktopClient().AsignarPermisos((UTGlobal.UsuarioRol).ToString(), _NombreProg);
                if (dtRolUsu.Rows.Count > 0)
                {
                    bool show = Convert.ToBoolean(dtRolUsu.Rows[0]["Show"]);
                    bool add = Convert.ToBoolean(dtRolUsu.Rows[0]["Add"]);
                    bool modif = Convert.ToBoolean(dtRolUsu.Rows[0]["Mod"]);
                    bool del = Convert.ToBoolean(dtRolUsu.Rows[0]["Del"]);

                    if (add == false)
                        BtnNuevo.Visible = false;
                    if (modif == false)
                        BtnModificar.Visible = false;
                    if (del == false)
                        BtnEliminar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_ArmarGrillas()
        {
            MP_ArmarGrillaListado();      
        }       
       
        private void MP_ArmarGrillaListado()
        {
            try
            {
                List<EntList> ListaCompleta = new ServiceDesktop.ServiceDesktopClient().ObtenerListaConcepto().ToList(); //go-dev revizar
                Dgv_Buscador2.ConfigInicialVinculado(ListaCompleta, "Ajuste");

                Dgv_Buscador2.ColAL(nameof(EntList.Id), "Código", 100);
                Dgv_Buscador2.ColAL(nameof(EntList.Descripcion), "Descripcion", 250);
                Dgv_Buscador2.ColAL(nameof(EntList.TipoMovimiento), "Tipo Mov", 150);
                Dgv_Buscador2.ColAL(nameof(EntList.AjusteCliente), "Ajuste Cliente", 150);
                Dgv_Buscador2.ColAL(nameof(EntList.Estado), "Estado", 150);

                Dgv_Buscador2.ConfigFinalBasica();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

       
        private void MP_CargarListado()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var listado = servicio.ObtenerListaConcepto().ToList();
                    if (listado == null)
                        listado = new List<VConceptoLista>();
                    _listado.Clear();
                    _listado.AddRange(listado);
                    MP_ArmarGrillaListado();
                    //Dgv_Buscador2.Refetch();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }      
       

        private void MP_Habilitar()
        {
            Tb_Id.ReadOnly = false;
            Tb_Observacion.ReadOnly = false;
            Sw_Tipo.IsReadOnly = false;
            Sw_Cliente.IsReadOnly = false;
            Sw_Tipo.IsReadOnly = false;
            Sw_Estado.IsReadOnly = false;
            Dgv_Buscador2.Enabled = false;
        }

        private void MP_InHabilitar()
        {
            Tb_Id.ReadOnly = true;
            Tb_Observacion.ReadOnly = true;
            Sw_Tipo.IsReadOnly = true;
            Sw_Cliente.IsReadOnly = true;
            Sw_Tipo.IsReadOnly = true;
            Dgv_Buscador2.Enabled = true;
            Sw_Estado.IsReadOnly = true;
        }

        private void MP_Limpiar()
        {
            try
            {     
                _concepto.Id = 0;
                _concepto.Descripcion = "";
                _concepto.Fecha = DateTime.Today;
                _concepto.TipoMovimiento = 1;
                _concepto.TipoMovimientoValor = true;
                _concepto.AjusteCliente = true;
                _concepto.Estado = true;
                VConceptosBindingSource.ResetCurrentItem();
                MP_LimpiarColor();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }

        public void MP_LimpiarColor()
        {           
            Tb_Observacion.BackColor = Color.White;
        }

        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_Buscador2.Row = _Pos;
                if (_Pos > -1)
                {
                    var concepto = (EntList)Dgv_Buscador2.GetRow(_Pos).DataRow;
                  
                    _concepto.Id = concepto.Id;
                    _concepto.Descripcion = concepto.Descripcion;
                    _concepto.TipoMovimientoValor = concepto.TipoMovimiento == "INGRESO" ? true : false;
                    _concepto.AjusteCliente = concepto.TipoMovimiento == "SI" ? true : false;
                    _concepto.Estado = concepto.Estado == "HABILITADO" ? true : false;
                    VConceptosBindingSource.ResetCurrentItem();                   
                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_Buscador2.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Filtrar(int tipo)
        {
            MP_CargarListado();
            if (Dgv_Buscador2.RowCount > 0)
            {
                _MPos = tipo == 1 ? 0 : Dgv_Buscador2.Row;
                MP_MostrarRegistro(_MPos);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void Dgv_Buscador2_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_Buscador2.Row >= 0 && Dgv_Buscador2.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_Buscador2.Row);
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = 0;
                this.MP_MostrarRegistro(_MPos);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (_MPos > 0)
                {
                    _MPos -= 1;
                    this.MP_MostrarRegistro(_MPos);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (_MPos < Dgv_Buscador2.RowCount - 1)
                {
                    _MPos += 1;
                    this.MP_MostrarRegistro(_MPos);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_Buscador2.RowCount - 1;
                this.MP_MostrarRegistro(_MPos);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion


        #region Metodo heredados      
        public override bool MH_NuevoRegistro()
        {
            try
            {
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var idAuxiliar = _concepto.Id;
                    _concepto.Usuario = UTGlobal.Usuario;
                    var conceptoId = servicio.Concepto_Guardar(_concepto);
                    if (idAuxiliar == 0)
                    {
                        MP_Filtrar(1);
                        MP_Limpiar();
                    }
                    else
                    {
                        MP_Filtrar(2);
                        this.MP_InHabilitar();
                        MH_Habilitar();//El menu 
                    }

                    string mensaje = idAuxiliar == 0 ? GLMensaje.Nuevo_Exito("AJUSTE", conceptoId.ToString()) :
                                                       GLMensaje.Modificar_Exito("AJUSTE", conceptoId.ToString());
                    MP_MostrarMensajeExito(mensaje);
                }
                return true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return true;
            }
        }

        public override bool MH_Eliminar()
        {
            try
            {
                int idConcepto = _concepto.Id;
                Efecto efecto = new Efecto();
                efecto.Tipo = 2;
                efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
                efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
                efecto.ShowDialog();
                bool resul = false;
                if (efecto.Band)
                {
                    List<string> Mensaje = new List<string>();
                    var LMensaje = Mensaje.ToArray();
                    var resultado = new ServiceDesktop.ServiceDesktopClient().Concepto_Eliminar(idConcepto);
                    if (resultado)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, _concepto.Id.ToString()));
                    }
                    else
                        MP_MostrarMensajeError("NO SE PUEDE ELIMINAR TIENE TRANSACCIONES RELACIONADAS");
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }

        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();
           
        }

        public override void MH_Modificar()
        {
            MP_Habilitar();
            
        }

        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(2);
        }

        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {
                
                if (_concepto.Descripcion == string.Empty)
                {
                    Tb_Observacion.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Tb_Observacion.BackColor = Color.White;
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }
        }

        #endregion
    }
}
