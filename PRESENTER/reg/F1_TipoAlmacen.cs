using DevComponents.DotNetBar;
using ENTITY.inv.TipoAlmacen.view;
using Janus.Windows.GridEX;
using PRESENTER.frm;
using PRESENTER.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using UTILITY.Global;

namespace PRESENTER.reg
{
    public partial class F1_TipoAlmacen : MODEL.ModeloF1
    {
        public F1_TipoAlmacen()
        {
            InitializeComponent();
            this.MP_Inhabilitar();
            this.MP_CargarTiposDeAlmacen();
            MP_AsignarPermisos();
            SuperTabBuscar.Visible = false;
            BtnImprimir.Visible = false;
        }

        #region Variables de instancia

        private static int index;
        public static List<VTipoAlmacenListar> listaTipoAlmacenes;
        private string _NombreFormulario = "TPO DE ALMACEN";
        #endregion

        #region Metodos Privados        
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(),
                PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano,
                eToastGlowColor.Green, eToastPosition.TopCenter);
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

        private void MP_Inhabilitar()
        {
            Tb_Descripcion.ReadOnly = true;
            Tb_TipoAlmacen.ReadOnly = true;
            lblId.Visible = false;
            Dgv_TiposAlmacen.Enabled = true;
        }

        private void MP_Habilitar()
        {
            Tb_Descripcion.ReadOnly = false;
            Tb_TipoAlmacen.ReadOnly = false;
            Dgv_TiposAlmacen.Enabled = false;
        }
        private void MP_CargarTiposDeAlmacen()
        {
            try
            {
                listaTipoAlmacenes = new ServiceDesktop.ServiceDesktopClient().TipoAlmacenListar().ToList();
                if (listaTipoAlmacenes.Count() >= 0)
                {
                    index = 0;
                    this.MP_MostrarRegistro(index);

                    Dgv_TiposAlmacen.DataSource = listaTipoAlmacenes;
                    Dgv_TiposAlmacen.RetrieveStructure();
                    Dgv_TiposAlmacen.AlternatingColors = true;

                    Dgv_TiposAlmacen.RootTable.Columns[0].Key = "Id";
                    Dgv_TiposAlmacen.RootTable.Columns[0].Caption = "Id";
                    Dgv_TiposAlmacen.RootTable.Columns[0].Visible = false;

                    Dgv_TiposAlmacen.RootTable.Columns[1].Key = "Nombre";
                    Dgv_TiposAlmacen.RootTable.Columns[1].Caption = "Nombre";
                    Dgv_TiposAlmacen.RootTable.Columns[1].Width = 250;
                    Dgv_TiposAlmacen.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_TiposAlmacen.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_TiposAlmacen.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_TiposAlmacen.RootTable.Columns[1].Visible = true;

                    Dgv_TiposAlmacen.RootTable.Columns[2].Key = "Descripcion";
                    Dgv_TiposAlmacen.RootTable.Columns[2].Caption = "Descripcion";
                    Dgv_TiposAlmacen.RootTable.Columns[2].Width = 450;
                    Dgv_TiposAlmacen.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_TiposAlmacen.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_TiposAlmacen.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_TiposAlmacen.RootTable.Columns[2].Visible = true;

                    //Habilitar filtradores
                    Dgv_TiposAlmacen.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_TiposAlmacen.FilterMode = FilterMode.Automatic;
                    Dgv_TiposAlmacen.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_TiposAlmacen.GroupByBoxVisible = false;
                    Dgv_TiposAlmacen.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Reiniciar()
        {
            this.Tb_Descripcion.Text = "";
            this.Tb_TipoAlmacen.Text = "";
        }

        private void MP_MostrarRegistro(int index)
        {
            var tipoAlmacen = listaTipoAlmacenes[index];
            lblId.Text = tipoAlmacen.Id.ToString();
            Tb_Descripcion.Text = tipoAlmacen.Descripcion;
            Tb_TipoAlmacen.Text = tipoAlmacen.Nombre;

            this.LblPaginacion.Text = (index + 1) + "/" + listaTipoAlmacenes.Count;
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarTiposDeAlmacen();
            if (Dgv_TiposAlmacen.RowCount > 0)
            {
                index = 0;
                MP_MostrarRegistro(tipo == 1 ? index : Dgv_TiposAlmacen.RowCount -1);
            }
            else
            {
                MP_Reiniciar();
                LblPaginacion.Text = "0/0";
            }
        }
        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            this.MP_Reiniciar();
            base.MH_Nuevo();
            this.MP_Habilitar();
        }
        public override void MH_Modificar()
        {
            MP_Habilitar();
            
        }
        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_Inhabilitar();
            this.MP_Reiniciar();
        }

        public override bool MH_Validar()
        {
            if (string.IsNullOrEmpty(Tb_TipoAlmacen.Text))
            {
                Tb_TipoAlmacen.BackColor = Color.Red;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool MH_NuevoRegistro()
        {
            var tipoAlmacen = new VTipoAlmacen
            {
                Descripcion = Tb_Descripcion.Text,
                Nombre = Tb_TipoAlmacen.Text
            };

            try
            {
                var IdTipo = lblId.Text == "" ? 0 : Convert.ToInt32(lblId.Text);
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    servicio.TipoAlmacenGuardar(tipoAlmacen, ref IdTipo);
                }
                if (IdTipo == 0)
                {
                    this.MP_Habilitar();
                    this.MP_Reiniciar();
                    this.MP_CargarTiposDeAlmacen();
                }
                else
                {
                    this.MP_Filtrar(2);
                    this.MP_Inhabilitar();
                    MH_Habilitar();//El menu  
                }
                ToastNotification.Show(this, GLMensaje.Nuevo_Exito("TIPO ALMACEN", IdTipo.ToString()), Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                return true;

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError("No se puedo guardar el Tipo de Almacen");
                return false;
            }
        }
        public override bool MH_Eliminar()
        {
            try
            {
                int IdAlmacen = Convert.ToInt32(lblId.Text);
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
                    using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                    {
                        resul = servicio.EliminarTipoAlmacen(IdAlmacen, ref LMensaje);
                    }
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, lblId.Text));
                    }
                    else
                    {
                        //Obtiene los codigos de productos sin stock
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
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, lblId.Text));
                        }
                    }
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        #endregion

        #region Eventos

        private void F1_TipoAlmacen_Load(object sender, EventArgs e)
        {
            LblTitulo.Text = "TIPOS DE ALMACEN";
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            index = 0;
            this.MP_MostrarRegistro(index);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index -= 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (index < listaTipoAlmacenes.Count - 1)
            {
                index += 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            index = listaTipoAlmacenes.Count - 1;
            this.MP_MostrarRegistro(index);
        }
        private void Dgv_TiposAlmacen_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_TiposAlmacen.Row >= 0 && Dgv_TiposAlmacen.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_TiposAlmacen.Row);
            }
        }
        private void Dgv_TiposAlmacen_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion


    }
}
