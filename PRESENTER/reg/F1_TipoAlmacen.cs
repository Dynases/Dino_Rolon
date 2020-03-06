using DevComponents.DotNetBar;
using ENTITY.inv.TipoAlmacen.view;
using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
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
        }

        #region Variables de instancia

        private static int index;
        public static List<VTipoAlmacenListar> listaTipoAlmacenes;

        #endregion

        #region Metodos Privados        

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(),
                PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano,
                eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_Inhabilitar()
        {
            Tb_Descripcion.ReadOnly = true;
            Tb_TipoAlmacen.ReadOnly = true;
            lblId.Visible = false;
        }

        private void MP_Habilitar()
        {
            Tb_Descripcion.ReadOnly = false;
            Tb_TipoAlmacen.ReadOnly = false;
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

        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
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
                if (new ServiceDesktop.ServiceDesktopClient().TipoAlmacenGuardar(tipoAlmacen))
                {
                    this.MP_Inhabilitar();
                    this.MP_Reiniciar();
                    this.MP_CargarTiposDeAlmacen();

                    ToastNotification.Show(this,
                        GLMensaje.Modificar_Exito("TIPOS DE ALMACEN", Tb_TipoAlmacen.Text),
                        PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico,
                        eToastGlowColor.Green, eToastPosition.TopCenter);
                    return true;
                }
                else
                {
                    this.MP_MostrarMensajeError(GLMensaje.Registro_Error("TIPOS DE ALMACEN"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
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

        #endregion        

    }
}
