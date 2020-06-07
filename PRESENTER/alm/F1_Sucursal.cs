using DevComponents.DotNetBar;
using ENTITY.inv.Sucursal.View;
using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Sucursal : MODEL.ModeloF1
    {
        public F1_Sucursal()
        {
            InitializeComponent();
            this.MP_InHabilitar();
            this.MP_CargarListaSucursales();
            MP_AsignarPermisos();
        }

        #region Variables globales        

        private static int index;
        private static List<VSucursalLista> listaSucursal;

        #endregion

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

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
        private void MP_InHabilitar()
        {
            this.Tb_Descrip.ReadOnly = true;
            this.Tb_Direcc.ReadOnly = true;
            this.Tb_Telef.ReadOnly = true;
            this.lblId.Visible = false;
            Dgv_Almacenes.Enabled = false;
        }

        private void MP_Habilitar()
        {
            this.Tb_Descrip.ReadOnly = false;
            this.Tb_Direcc.ReadOnly = false;
            this.Tb_Telef.ReadOnly = false;
            Dgv_Almacenes.Enabled = true;
        }

        private void MP_Limpiar()
        {
            this.Tb_Descrip.Text = "";
            this.Tb_Direcc.Text = "";
            this.Tb_Telef.Text = "";
            this.lblId.Text = "";
            this.LblPaginacion.Text = "";

            Dgv_Almacenes.DataSource = null;
        }

        private void MP_CargarListaSucursales()
        {
            index = 0;
            try
            {
                listaSucursal = new ServiceDesktop.ServiceDesktopClient().SucursalListar().ToList();
                if (listaSucursal != null && listaSucursal.Count >= 0)
                {
                    this.MP_MostrarRegistro(index);
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_MostrarRegistro(int index)
        {
            var sucursal = listaSucursal[index];
            lblId.Text = sucursal.Id.ToString();
            Tb_Descrip.Text = sucursal.Descripcion;
            Tb_Direcc.Text = sucursal.Direccion;
            Tb_Telef.Text = sucursal.Telefono;

            this.MP_CargarDetalleRegistro(sucursal.Id);

            this.LblPaginacion.Text = (index + 1) + "/" + listaSucursal.Count;
        }

        private void MP_CargarDetalleRegistro(int id)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ListarAlmacenXSucursalId(id).ToList();
                if (result.Count >= 0)
                {
                    Dgv_Almacenes.DataSource = result;
                    Dgv_Almacenes.RetrieveStructure();
                    Dgv_Almacenes.AlternatingColors = true;

                    Dgv_Almacenes.RootTable.Columns[0].Key = "Id";
                    Dgv_Almacenes.RootTable.Columns[0].Caption = "Id";
                    Dgv_Almacenes.RootTable.Columns[0].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[1].Key = "Descripcion";
                    Dgv_Almacenes.RootTable.Columns[1].Caption = "Sucursal";
                    Dgv_Almacenes.RootTable.Columns[1].Width = 200;
                    Dgv_Almacenes.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[1].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[2].Key = "Direccion";
                    Dgv_Almacenes.RootTable.Columns[2].Caption = "Direccion";
                    Dgv_Almacenes.RootTable.Columns[2].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[2].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[3].Key = "Telefono";
                    Dgv_Almacenes.RootTable.Columns[3].Caption = "Telefono";
                    Dgv_Almacenes.RootTable.Columns[3].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[3].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[4].Key = "Sucursal";
                    Dgv_Almacenes.RootTable.Columns[4].Caption = "Sucursal";
                    Dgv_Almacenes.RootTable.Columns[4].Width = 100;
                    Dgv_Almacenes.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[4].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[5].Key = "SucursalId";
                    Dgv_Almacenes.RootTable.Columns[5].Caption = "SucursalId";
                    Dgv_Almacenes.RootTable.Columns[5].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[6].Key = "TipoAlmacen";
                    Dgv_Almacenes.RootTable.Columns[6].Caption = "TipoAlmacen";
                    Dgv_Almacenes.RootTable.Columns[6].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[6].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[7].Key = "TipoAlmacenId";
                    Dgv_Almacenes.RootTable.Columns[7].Caption = "TipoAlmacenId";
                    Dgv_Almacenes.RootTable.Columns[7].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[8].Key = "Encargado";
                    Dgv_Almacenes.RootTable.Columns[8].Caption = "Encargado";
                    Dgv_Almacenes.RootTable.Columns[8].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[8].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[8].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[8].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[8].Visible = true;

                    //Habilitar filtradores
                    Dgv_Almacenes.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Almacenes.FilterMode = FilterMode.Automatic;
                    Dgv_Almacenes.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Almacenes.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Almacenes.GroupByBoxVisible = false;
                    Dgv_Almacenes.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Limpiar();
            this.MP_Habilitar();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_InHabilitar();
            this.MP_CargarListaSucursales();
        }

        public override bool MH_NuevoRegistro()
        {
            var sucursal = new VSucursal
            {
                Descripcion = Tb_Descrip.Text,
                Direccion = Tb_Direcc.Text,
                Estado = 1,
                //Imagen = _imagen,
                //Latitud = Convert.ToDecimal(_latitud),
                //Longitud = Convert.ToDecimal(_longitud),
                Telefono = Tb_Telef.Text,
                Usuario = UTGlobal.Usuario
            };

            var mensaje = "";

            try
            {
                if (new ServiceDesktop.ServiceDesktopClient().SucursalGuardar(sucursal))
                {
                    base.MH_Habilitar();
                    this.MP_InHabilitar();
                    this.MP_CargarListaSucursales();
                    mensaje = GLMensaje.Modificar_Exito("SUCURSALES", Tb_Descrip.Text);
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                    return true;
                }
                else
                {
                    mensaje = GLMensaje.Registro_Error("SUCURSALES");
                    this.MP_MostrarMensajeError(mensaje);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }

        public override bool MH_Validar()
        {
            if (string.IsNullOrEmpty(Tb_Descrip.Text))
            {
                Tb_Descrip.BackColor = Color.Red;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Eventos

        private void F1_Deposito_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "SUCURSALES";
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
            if (index < listaSucursal.Count - 1)
            {
                index += 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            index = listaSucursal.Count - 1;
            this.MP_MostrarRegistro(index);
        }

        #endregion        
    }
}
