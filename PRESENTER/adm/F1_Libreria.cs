using DevComponents.DotNetBar;
using ENTITY.Libreria.View;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Janus.Windows.GridEX;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;
using UTILITY.Enum.EnEstado;
using GMap.NET.Internals;

namespace PRESENTER.adm
{
    public partial class F1_Libreria : ModeloF1
    {
        string _NombreFormulario = "LIBRERIA";
        public F1_Libreria()
        {
            InitializeComponent();
            MP_Iniciar();
        }
        #region Eventos
    
        private void cbPrograma_ValueChanged(object sender, EventArgs e)
        {
            UTGlobal.MG_ArmarCombo(cbCategoria,
                                    new ServiceDesktop.ServiceDesktopClient().TraerCategorias(Convert.ToInt32(cbPrograma.Value)).ToList());
            cbCategoria.Value = 1;
        }
        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (e.Column.Index == Dgv_Detalle.RootTable.Columns["Descrip"].Index)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            if (BtnGrabar.Enabled == true)
            {
                Dgv_Detalle.SetValue("estado", 2);
            }
        }
        private void Dgv_Detalle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (BtnGrabar.Enabled == true)
                {

                    if (e.KeyCode == Keys.Escape)
                    {
                        //Eliminar FIla.
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion
        #region Metodos Privados
        private void MP_Iniciar()
        {
            try
            {
                SuperTabBuscar.Visible = false;
                BtnImprimir.Visible = false;
                BtnNuevo.Visible = false;
                BtnEliminar.Visible = false;
                MP_InicioArmarCombo();
                Panel2.Visible = false;
                MP_AsignarPermisos();                          
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void MP_InicioArmarCombo()
        {
            try
            {              
                UTGlobal.MG_ArmarCombo(cbPrograma,
                                     new ServiceDesktop.ServiceDesktopClient().TraerProgramas().ToList());
                cbPrograma.Value = 1;
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ArmarDetalle(List<VLibreriaLista> lresult)
        {
            try
            {
                //DataTable result = ListaATabla(lresult);
                Dgv_Detalle.DataSource = lresult;
                Dgv_Detalle.RetrieveStructure();
                Dgv_Detalle.AlternatingColors = true;

                Dgv_Detalle.RootTable.Columns["IdGrupo"].Visible = false;
                Dgv_Detalle.RootTable.Columns["IdOrden"].Visible = false;
                
               
                Dgv_Detalle.RootTable.Columns["IdLibrer"].Caption = "Id";
                Dgv_Detalle.RootTable.Columns["IdLibrer"].Width = 130;
                Dgv_Detalle.RootTable.Columns["IdLibrer"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["IdLibrer"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["IdLibrer"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns["IdLibrer"].Visible = true;
           
                Dgv_Detalle.RootTable.Columns["Descrip"].Caption = "Descripción";
                Dgv_Detalle.RootTable.Columns["Descrip"].Width = 130;
                Dgv_Detalle.RootTable.Columns["Descrip"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Descrip"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Descrip"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns["Descrip"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Fecha"].Visible = false;
                Dgv_Detalle.RootTable.Columns["Hora"].Visible = false;
                Dgv_Detalle.RootTable.Columns["Usuario"].Visible = false;
                Dgv_Detalle.RootTable.Columns["estado"].Visible = false;
              
                //Habilitar filtradores              
                //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_Detalle.GroupByBoxVisible = false;
                Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
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


        private void MP_EliminarFila()
        {
            try
            {
                if (Dgv_Detalle.RowCount > 1)
                {
                    Dgv_Detalle.UpdateData();
                    int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                    if (estado == (int)ENEstado.GUARDADO || estado == (int)ENEstado.MODIFICAR)
                    {
                        Dgv_Detalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.ELIMINAR;
                        Dgv_Detalle.UpdateData();
                        Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
                    }
                }
                else
                    throw new Exception("El detalle no puede estar vacio");
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
        private void cbCategoria_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                actualizarDetalle();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void actualizarDetalle()
        {
            var lista = new ServiceDesktop.ServiceDesktopClient().TraerLibreriasXCategoria(Convert.ToInt32(cbPrograma.Value), Convert.ToInt32(cbCategoria.Value)).ToList();
            MP_ArmarDetalle(lista);
        }
        #endregion

        #region Metodos Heredados
        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                var detalle = ((List<VLibreriaLista>)Dgv_Detalle.DataSource).ToArray<VLibreriaLista>();               
                resultado = new ServiceDesktop.ServiceDesktopClient().ModificarLibreria(detalle, ref LMensaje);
                cbCategoria.Focus();
                actualizarDetalle();
                //Resultado
                if (resultado)
                {
                    ToastNotification.Show(this, "Transacción grabada con éxito ", PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                    
                }
                else
                {
                    var mensajeLista = LMensaje.ToList();
                    if (mensajeLista.Count > 0)
                    {
                        var mensaje = "";
                        foreach (var item in mensajeLista)
                        {
                            mensaje = mensaje + "- " + item + "\n";
                        }
                        MP_MostrarMensajeError(mensaje);
                        return false;
                    }
                    else
                    {
                        ToastNotification.Show(this, "Transacción no realizada", PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                    }                    
                }
                return resultado;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return resultado;
            }
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {

                if (cbPrograma.SelectedIndex == -1)
                {
                    cbPrograma.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cbPrograma.BackColor = Color.White;
                if (cbCategoria.SelectedIndex == -1)
                {
                    cbCategoria.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    cbCategoria.BackColor = Color.White;

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
