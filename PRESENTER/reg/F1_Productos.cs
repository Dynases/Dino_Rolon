using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MODEL;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
using ENTITY.Cliente.View;
using UTILITY.Global;
using UTILITY.Enum;
using UTILITY.Enum.EnEstaticos;
using ENTITY.Libreria.View;
using Janus.Windows.GridEX.EditControls;
using MetroFramework.Controls;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using ENTITY.Producto.View;
using System.IO;
using UTILITY.Enum.EnCarpetas;
using PRESENTER.frm;

namespace PRESENTER.reg
{

    public partial class F1_Productos : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "PRODUCTO";
        string _imagen = "Default.jpg";
        int _idOriginal = 0;
        int _MPos;
        bool _Limpiar = false;
        bool _ModificarImagen = false;
        #endregion
        #region Manejo de eventos
        public F1_Productos()
        {
            InitializeComponent();
            MP_Iniciar();
            SuperTabBuscar.Visible = false;
        }
        private void Dgv_Buscardor_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_Buscardor_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_Buscardor.Row >= 0 && Dgv_Buscardor.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_Buscardor.Row);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_Grupo1_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Grupo1.Text == "" ? "" : Cb_Grupo1.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Grupo1,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1)).ToList());
                    Cb_Grupo1.SelectedIndex = ((List<VLibreria>)Cb_Grupo1.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, GLMensaje.Error);
            }
        }
        private void btn_Grupo2_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Grupo2.Text == "" ? "" : Cb_Grupo2.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Grupo2,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2)).ToList());
                    Cb_Grupo2.SelectedIndex = ((List<VLibreria>)Cb_Grupo2.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Comuniquece con el administrador del sistema");
            }

        }
        private void btn_Grupo3_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Grupo3.Text == "" ? "" : Cb_Grupo3.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Grupo3,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3)).ToList());
                    Cb_Grupo3.SelectedIndex = ((List<VLibreria>)Cb_Grupo3.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Comuniquece con el administrador del sistema");
            }
        }
        private void btn_Grupo4_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO4));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO4),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Grupo4.Text == "" ? "" : Cb_Grupo4.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Grupo4,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO4)).ToList());
                    Cb_Grupo4.SelectedIndex = ((List<VLibreria>)Cb_Grupo4.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Comuniquece con el administrador del sistema");
            }
        }
        private void btn_Grupo5_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO5));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO5),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Grupo5.Text == "" ? "" : Cb_Grupo5.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Grupo5,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO5)).ToList());
                    Cb_Grupo5.SelectedIndex = ((List<VLibreria>)Cb_Grupo5.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Comuniquece con el administrador del sistema");
            }
        }
        private void btn_UnidadVenta_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_VENTA));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_VENTA),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_UnidadVenta.Text == "" ? "" : Cb_UnidadVenta.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_UnidadVenta,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_VENTA)).ToList());
                    Cb_UnidadVenta.SelectedIndex = ((List<VLibreria>)Cb_UnidadVenta.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Comuniquece con el administrador del sistema");
            }
        }

        private void btn_UnidadPeso_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_PESO));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_PESO),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_UniPeso.Text == "" ? "" : Cb_UniPeso.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_UniPeso,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_PESO)).ToList());
                    Cb_UniPeso.SelectedIndex = ((List<VLibreria>)Cb_UniPeso.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Comuniquece con el administrador del sistema");
            }
        }

        private void Cb_Grupo1_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Grupo1, btn_Grupo1);
        }

        private void Cb_Grupo2_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Grupo2, btn_Grupo2);
        }

        private void Cb_Grupo3_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Grupo3, btn_Grupo3);
        }

        private void Cb_Grupo4_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Grupo4, btn_Grupo4);
        }

        private void Cb_Grupo5_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Grupo5, btn_Grupo5);
        }

        private void Cb_UnidadVenta_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_UnidadVenta, btn_UnidadVenta);
        }

        private void Cb_UniPeso_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_UniPeso, btn_UnidadPeso);
        }
        private void BtAdicionar_Click(object sender, EventArgs e)
        {
            MP_CopiarImagenRutaDefinida();
            BtnGrabar.Focus();
        }
        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (Dgv_Buscardor.RowCount > 0)
            {
                _MPos = 0;
                Dgv_Buscardor.Row = _MPos;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_Buscardor.Row;
            if (_MPos > 0 && Dgv_Buscardor.RowCount > 0)
            {
                _MPos = _MPos - 1;
                Dgv_Buscardor.Row = _MPos;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_Buscardor.Row;
            if (_MPos < Dgv_Buscardor.RowCount - 1 && _MPos >= 0)
            {
                _MPos = Dgv_Buscardor.Row + 1;
                Dgv_Buscardor.Row = _MPos;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_Buscardor.Row;
            if (Dgv_Buscardor.RowCount > 0)
            {
                _MPos = Dgv_Buscardor.RowCount - 1;
                Dgv_Buscardor.Row = _MPos;
            }
        }
        private void sw_TipoPro_ValueChanged(object sender, EventArgs e)
        {
            if (sw_TipoPro.Value == true)
            {
                LblProducto.Visible = true;
                LblCantidad.Visible = true;
                Tb_Producto.Visible = true;
                Tb_IdProducto.Visible = true;
                Tb_Cantidad.Visible = true;
            }
            else
            {
                LblProducto.Visible = false;
                LblCantidad.Visible = false;
                Tb_Producto.Visible = false;
                Tb_IdProducto.Visible = false;
                Tb_Cantidad.Visible = false;
            }
        }
        #endregion
        #region Metodos Privados
        private void MP_Iniciar()
        {
            LblTitulo.Text = _NombreFormulario;
            MP_ArmarComboInicial();
            MP_CargarEncabezado();
            MP_InHabilitar();
        }
        private void MP_ArmarComboInicial()
        {
            try
            {
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarCombo(Cb_Grupo1,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_Grupo2,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_Grupo3,
                                    new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                  Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_Grupo4,
                                    new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                  Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO4)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_Grupo5,
                                    new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                  Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO5)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_UnidadVenta,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_VENTA)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_UniPeso,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_PESO)).ToList());
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_CargarEncabezado()
        {
            try
            {
                //int I = ((List<VProductoLista>)Dgv_Buscardor.DataSource).Count;
                var result = new ServiceDesktop.ServiceDesktopClient().ProductoListar().ToList();
                Dgv_Buscardor.DataSource = result;
                if (result.Count > 0)
                {
                    Dgv_Buscardor.RetrieveStructure();
                    Dgv_Buscardor.AlternatingColors = true;

                    Dgv_Buscardor.RootTable.Columns[0].Key = "id";
                    Dgv_Buscardor.RootTable.Columns[0].Visible = false;

                    Dgv_Buscardor.RootTable.Columns[1].Key = "CodProducto";
                    Dgv_Buscardor.RootTable.Columns[1].Visible = false;


                    Dgv_Buscardor.RootTable.Columns[2].Key = "Descripcion";
                    Dgv_Buscardor.RootTable.Columns[2].Caption = "Descripcion";
                    Dgv_Buscardor.RootTable.Columns[2].Width = 380;
                    Dgv_Buscardor.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Buscardor.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Buscardor.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Buscardor.RootTable.Columns[2].Visible = true;

                    Dgv_Buscardor.RootTable.Columns[3].Key = "División";
                    Dgv_Buscardor.RootTable.Columns[3].Caption = "División";
                    Dgv_Buscardor.RootTable.Columns[3].Width = 250;
                    Dgv_Buscardor.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Buscardor.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Buscardor.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Buscardor.RootTable.Columns[3].Visible = true;


                    Dgv_Buscardor.RootTable.Columns[4].Key = "Marca/Tipo";
                    Dgv_Buscardor.RootTable.Columns[4].Caption = "Marca/Tipo";
                    Dgv_Buscardor.RootTable.Columns[4].Width = 250;
                    Dgv_Buscardor.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Buscardor.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Buscardor.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Buscardor.RootTable.Columns[4].Visible = true;

                    Dgv_Buscardor.RootTable.Columns[5].Key = "Categorías/Tipo";
                    Dgv_Buscardor.RootTable.Columns[5].Caption = "CategorIas/tipo";
                    Dgv_Buscardor.RootTable.Columns[5].Width = 200;
                    Dgv_Buscardor.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Buscardor.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_Buscardor.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Buscardor.RootTable.Columns[5].Visible = true;

                    //Dgv_Buscardor.RootTable.Columns[6].Key = "Contacto2";
                    //Dgv_Buscardor.RootTable.Columns[6].Caption = "Contacto2";
                    //Dgv_Buscardor.RootTable.Columns[6].Width = 200;
                    //Dgv_Buscardor.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    //Dgv_Buscardor.RootTable.Columns[6].CellStyle.FontSize = 8;
                    //Dgv_Buscardor.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    //Dgv_Buscardor.RootTable.Columns[6].Visible = true;

                    Dgv_Buscardor.RootTable.Columns[6].Key = "Usuario";
                    Dgv_Buscardor.RootTable.Columns[6].Visible = false;

                    Dgv_Buscardor.RootTable.Columns[7].Key = "Hora";
                    Dgv_Buscardor.RootTable.Columns[7].Visible = false;

                    Dgv_Buscardor.RootTable.Columns[8].Key = "Fecha";
                    Dgv_Buscardor.RootTable.Columns[8].Visible = false;

                    //Habilitar filtradores
                    Dgv_Buscardor.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Buscardor.FilterMode = FilterMode.Automatic;
                    Dgv_Buscardor.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Buscardor.GroupByBoxVisible = false;
                    Dgv_Buscardor.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_Habilitar()
        {
            try
            {
                Tb_Id.ReadOnly = false;
                Tb_CodProducto.ReadOnly = false;
                Tb_Descripcion.ReadOnly = false;
                Tb_CodBarras.ReadOnly = false;
                Tb_Peso.IsInputReadOnly = false;
                Cb_UnidadVenta.Enabled = true;
                Cb_UniPeso.Enabled = true;
                Cb_Grupo1.Enabled = true;
                Cb_Grupo2.Enabled = true;
                Cb_Grupo3.Enabled = true;
                Cb_Grupo4.Enabled = true;
                Cb_Grupo5.Enabled = true;
                BtAdicionar.Enabled = true;
                sw_TipoPro.IsReadOnly = false;  
                Tb_IdProducto.IsInputReadOnly = false;
                Tb_Descripcion.ReadOnly = false;
                Tb_Cantidad.IsInputReadOnly = false;
                UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Imagen, ENSubCarpetas.ImagenesProducto);
                UTGlobal.MG_CrearCarpetaTemporal();
                Dgv_Buscardor.Enabled = false;
                Tb_CodProducto.Focus();
     
                Tb_Producto.ReadOnly = false;
                Tb_IdProducto.IsInputReadOnly = false;
                Tb_Cantidad.IsInputReadOnly = false;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InHabilitar()
        {
            Tb_Id.ReadOnly = true;
            Tb_CodProducto.ReadOnly = true;
            Tb_Descripcion.ReadOnly = true;
            Tb_CodBarras.ReadOnly = true;
            Tb_Peso.IsInputReadOnly = true;
            Cb_UnidadVenta.Enabled = false;
            Cb_UniPeso.Enabled = false;
            Cb_Grupo1.Enabled = false;
            Cb_Grupo2.Enabled = false;
            Cb_Grupo3.Enabled = false;
            Cb_Grupo4.Enabled = false;
            Cb_Grupo5.Enabled = false;
            sw_TipoPro.IsReadOnly = true;
            Tb_IdProducto.IsInputReadOnly = true;
            Tb_Descripcion.ReadOnly = true;
            Tb_Cantidad.IsInputReadOnly = true;
            BtAdicionar.Enabled = false;
            _Limpiar = false;
            Dgv_Buscardor.Enabled = true;
            Tb_Producto.ReadOnly = true;
            Tb_IdProducto.IsInputReadOnly = true;
            Tb_Cantidad.IsInputReadOnly = true;
        }
        private void MP_Limpiar()
        {
            Tb_Id.Clear();
            Tb_CodProducto.Clear();
            Tb_Descripcion.Clear();
            Tb_CodBarras.Clear();
            Tb_Peso.Value=0;
            Tb_IdProducto.Value = 0;
            Tb_Cantidad.Value = 0;
            Tb_Producto.Clear();
            if (_Limpiar == false)
            {
                UTGlobal.MG_SeleccionarCombo(Cb_UnidadVenta);
                UTGlobal.MG_SeleccionarCombo(Cb_UniPeso);
                UTGlobal.MG_SeleccionarCombo(Cb_Grupo1);
                UTGlobal.MG_SeleccionarCombo(Cb_Grupo2);
                UTGlobal.MG_SeleccionarCombo(Cb_Grupo3);
                UTGlobal.MG_SeleccionarCombo(Cb_Grupo4);
                UTGlobal.MG_SeleccionarCombo(Cb_Grupo5);
            }
        }
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_Buscardor.Row = _Pos;
                _idOriginal = (int)Dgv_Buscardor.GetValue("id");
                var tabla = new ServiceDesktop.ServiceDesktopClient().ProductoListarXId(_idOriginal).ToArray();
                var lista = tabla.First();
                Tb_Id.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Id.ToString())).Count() > 0 ? tabla.Select(x => x.Id).First().ToString() : "";
                Tb_CodProducto.Text = tabla.Where(x => !string.IsNullOrEmpty(x.IdProd)).Count() > 0 ? tabla.Select(x => x.IdProd).First().ToString() : "";
                Tb_Descripcion.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Descripcion)).Count() > 0 ? tabla.Select(x => x.Descripcion).First().ToString() : "";
                Tb_CodBarras.Text = tabla.Where(x => !string.IsNullOrEmpty(x.CodBar)).Count() > 0 ? tabla.Select(x => x.CodBar).First().ToString() : "";
                Tb_Peso.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Peso.ToString())).Count() > 0 ? tabla.Select(x => x.Peso).First().ToString() : "";
                Cb_UnidadVenta.Value = tabla.Select(x => x.UniVenta).First();
                Cb_UniPeso.Value = tabla.Select(x => x.UniPeso).First();
                Cb_Grupo1.Value = tabla.Select(x => x.Grupo1).First();
                Cb_Grupo2.Value = tabla.Select(x => x.Grupo2).First();
                Cb_Grupo3.Value = tabla.Select(x => x.Grupo3).First();
                Cb_Grupo4.Value = tabla.Select(x => x.Grupo4).First();
                Cb_Grupo5.Value = tabla.Select(x => x.Grupo5).First();
                Tb_IdProducto.Value = lista.IdProducto;
                Tb_Producto.Text = lista.Producto2;
                sw_TipoPro.Value = lista.Tipo == 1 ? true : false;
                Tb_Cantidad.Value = Convert.ToDouble(lista.Cantidad);
                //Mostrar Imagenes
                MP_MostrarImagen(tabla.Select(x => x.Imagen).First());
                LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_Buscardor.RowCount.ToString();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_MostrarImagen(string _NombreImagen)
        {
            if (_NombreImagen.Equals("Default.jpg") || !File.Exists(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProducto) + _NombreImagen))
            {
                Bitmap img = new Bitmap(PRESENTER.Properties.Resources.PANTALLA);
                Pc_ImgProducto.Image = img;
            }
            else
            {
                if (File.Exists(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProducto) + _NombreImagen))
                {
                    MemoryStream Bin = new MemoryStream();
                    Bitmap img = new Bitmap(new Bitmap(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProducto) + _NombreImagen));
                    img.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Pc_ImgProducto.SizeMode = PictureBoxSizeMode.StretchImage;
                    Pc_ImgProducto.Image = Image.FromStream(Bin);
                    Bin.Dispose();
                }
            }
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarEncabezado();
            if (Dgv_Buscardor.RowCount > 0)
            {
                _MPos = 0;
                MP_MostrarRegistro(tipo == 1 ? _MPos : Dgv_Buscardor.RowCount - 1);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }
        private void MP_SeleccionarButtonCombo(MultiColumnCombo combo, ButtonX btn)
        {
            try
            {
                if (combo.SelectedIndex < 0 && combo.Text != string.Empty)
                {
                    btn.Visible = true;
                }
                else
                {
                    btn.Visible = false;
                }
            }
            catch (Exception ex )
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }
        private string MP_CopiarImagenRutaDefinida()
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" +
                              "|Ficheros GIF|*.gif" +
                              "|Ficheros BMP|*.bmp" +
                              "|Ficheros PNG|*.png" +
                              "|Ficheros TIFF|*.tif";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string ruta = file.FileName;
                    if (file.CheckFileExists)
                    {                        
                        Bitmap img = new Bitmap(new Bitmap(ruta));
                        Bitmap imgM = new Bitmap(new Bitmap(ruta));
                        MemoryStream Bin = new MemoryStream();
                        imgM.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg);

                        if (MP_AccionResult())
                        {
                            int mayor;
                            mayor = Convert.ToInt32(Dgv_Buscardor.GetTotal(Dgv_Buscardor.RootTable.Columns[0], AggregateFunction.Max));
                            _imagen = @"\Imagen_" + Convert.ToString(mayor + 1).Trim() + ".jpg";
                            Pc_ImgProducto.SizeMode = PictureBoxSizeMode.StretchImage;
                            Pc_ImgProducto.Image = Image.FromStream(Bin);

                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                        }
                        else
                        {
                            _imagen = @"\Imagen_" + Tb_Id.Text.Trim() + ".jpg";
                            Pc_ImgProducto.Image = Image.FromStream(Bin);
                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                            _ModificarImagen = true;
                        }
                    }
                    return _imagen;
                }
                return "default.jpg";
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return "";
            }           
        }
        private bool MP_AccionResult()
        {
              return Tb_Id.Text == string.Empty && Tb_CodBarras.ReadOnly == false ? true : false;
        }
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

        }
        #endregion
        #region Metodos Heredados
        public override bool MH_NuevoRegistro()
        {
            try
            {                
                int id = 0;
                bool resultado;
                string mensaje = "";
                //Llena el objeto
                VProducto Producto = new VProducto()
                {
                    IdProd = Tb_CodProducto.Text,
                    Estado = Convert.ToInt32(ENProductoEstado.Activo),
                    Descripcion = Tb_Descripcion.Text,
                    CodBar = Tb_CodBarras.Text,
                    Peso = string.Empty == Tb_Peso.Text ? 0 : Convert.ToDecimal(Tb_Peso.Text),
                    UniVenta = Convert.ToInt32(Cb_UnidadVenta.Value),
                    UniPeso = Convert.ToInt32(Cb_UniPeso.Value),
                    Grupo1 = Convert.ToInt32(Cb_Grupo1.Value),
                    Grupo2 = Convert.ToInt32(Cb_Grupo2.Value),
                    Grupo3 = Convert.ToInt32(Cb_Grupo3.Value),
                    Grupo4 = Convert.ToInt32(Cb_Grupo4.Value),
                    Grupo5 = Convert.ToInt32(Cb_Grupo5.Value),
                    Tipo = sw_TipoPro.Value == true ? 1 : 2 , 
                    Imagen = _imagen,
                    IdProducto = Tb_IdProducto.Text == string.Empty ? 0 : Convert.ToInt32(Tb_IdProducto.Value),
                    Producto2 = Tb_Producto.Text == string.Empty ? "": Tb_Producto.Text,
                    Cantidad = Tb_Cantidad.Text == string.Empty ? 0 : Convert.ToDecimal(Tb_Cantidad.Text),
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (VM_Nuevo) //Registro
                {                   
                    resultado = new ServiceDesktop.ServiceDesktopClient().ProductoGuardar(Producto,ref id);
                    if (resultado)
                    {
                        UTGlobal.MG_MoverImagenRuta(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProducto), _imagen, Pc_ImgProducto);                   
                        MP_Filtrar(1);
                        MP_Limpiar();
                        Tb_Descripcion.Focus();
                        _Limpiar = true;
                        _imagen = "Default.jpg";
                        _ModificarImagen = false;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());                       
                    }                   
                }
                else//Modificar
                {
                    id = Convert.ToInt32(Tb_Id.Text);
                    resultado = new ServiceDesktop.ServiceDesktopClient().ProductoModificar(Producto, id);
                    if (resultado)
                    {
                        if (_ModificarImagen)
                        {
                            UTGlobal.MG_MoverImagenRuta(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProducto), _imagen, Pc_ImgProducto);
                            _ModificarImagen = false;
                        }                       
                        Tb_CodProducto.Focus();
                        MP_Filtrar(2);
                        MP_InHabilitar();
                        _Limpiar = true;
                        _imagen = "Default.jpg";
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                        MH_Inhanbilitar();
                    }                
                }
                //Mensaje resultado de transaccion
                if (resultado)
                {
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                {
                    mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
                return false;
            }
         
        }
        public override bool MH_Eliminar()
        {
            bool resultadoRegistro = false;
            int IdProducto = Convert.ToInt32(Tb_Id.Text);
            bool resultado = new ServiceDesktop.ServiceDesktopClient().ProductoExisteEnCompra(IdProducto);
            if (resultado)
            {
                Bitmap img = new Bitmap(PRESENTER.Properties.Resources.WARNING, 50, 50);
                ToastNotification.Show(this, GLMensaje.Eliminar_Error_Transaciones_Relacionadas(_NombreFormulario),
                                            img, (int)GLMensajeTamano.Mediano, 
                                            eToastGlowColor.Green, 
                                            eToastPosition.TopCenter);
                return false;
            }
           //Pregunta si eliminara el registro
            Efecto efecto = new Efecto();
            efecto.Tipo = 2;
            efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
            efecto.Header = GLMensaje.Mensaje_Principal.ToUpper() ;
            efecto.ShowDialog();
            bool bandera = efecto.Band;
            if (bandera)
            {
                var resul = new ServiceDesktop.ServiceDesktopClient().ProductoEliminar(IdProducto);
                if (resul)
                {
                    ToastNotification.Show(this, GLMensaje.Eliminar_Exito(_NombreFormulario, Tb_Id.Text),
                                           PRESENTER.Properties.Resources.GRABACION_EXITOSA,
                                           (int)GLMensajeTamano.Chico,
                                            eToastGlowColor.Green,
                                           eToastPosition.TopCenter);
                    MP_Filtrar(1);
                    resultadoRegistro = true;
                }
                else
                {
                    Bitmap img = new Bitmap(PRESENTER.Properties.Resources.WARNING, 50, 50);
                    ToastNotification.Show(this, GLMensaje.Eliminar_Error(_NombreFormulario, Tb_Id.Text),
                                                img, (int)GLMensajeTamano.Mediano,
                                                eToastGlowColor.Green,
                                                eToastPosition.TopCenter);
                    resultadoRegistro = false;
                }                
            }
            return resultadoRegistro;
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
            MP_Filtrar(1);
        }     
        public override bool MH_Validar()
        {
            bool _Error = false;
            if (Tb_Descripcion.Text == "")
            {
                Tb_Descripcion.BackColor = Color.Red;
                _Error = true;
            }
            else
                Tb_Descripcion.BackColor = Color.White;

            if (Cb_UnidadVenta.SelectedIndex == -1)
            {
                Cb_UnidadVenta.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_UnidadVenta.BackColor = Color.White;
            if (Cb_UniPeso.SelectedIndex == -1)
            {
                Cb_UniPeso.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_UniPeso.BackColor = Color.White;
            return _Error;
        }
        #endregion

        private void labelX5_Click(object sender, EventArgs e)
        {

        }

     
    }
}
