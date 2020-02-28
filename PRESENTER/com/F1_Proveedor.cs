using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MODEL;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
using ENTITY.Cliente.View;
using UTILITY.Global;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using Janus.Windows.GridEX.EditControls;
using ENTITY.Proveedor.View;
using UTILITY.Enum.EnEstaticos;
using ENTITY.Libreria.View;
using System.Reflection;
using System.IO;
using UTILITY.Enum.EnCarpetas;

namespace PRESENTER.com
{
    public partial class F1_Proveedor : MODEL.ModeloF1
    {
        #region Privado, atributos
        private List<VProveedor_01Lista> _proveedorDetalle;
        #endregion

        public F1_Proveedor()
        {
            InitializeComponent();
            superTabControl1.SelectedTabIndex = 0;
            SuperTabItem3.Visible = false;
            MP_Iniciar();
        }
        #region Variables
        string _NombreFormulario = "PROVEEDORES";
        string _imagen = "Default";
        int _idOriginal = 0;
        int _MPos;
        Double _latitud = 0;
        Double _longitud = 0;
        GMapOverlay _Overlay;
        bool _Limpiar = false;
        bool _NuevoDetalle = false;
        bool _ModificarImagen = false;
        #endregion
        #region Manejo de Eventos 
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            if (VM_Nuevo)
            {
                _proveedorDetalle = new List<VProveedor_01Lista>();
                ArmarDetalle();
            }
        }

        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }
        private void btnMapaMin_Click(object sender, EventArgs e)
        {
            if (Gmc_Proveedor.Zoom >= Gmc_Proveedor.MaxZoom)
                Gmc_Proveedor.Zoom = Gmc_Proveedor.Zoom - 1;
        }

        private void BtnMapaMax_Click(object sender, EventArgs e)
        {
            if (Gmc_Proveedor.Zoom <= Gmc_Proveedor.MaxZoom)
                Gmc_Proveedor.Zoom = Gmc_Proveedor.Zoom + 1;
        }

        private void Gmc_Proveedor_DoubleClick(object sender, EventArgs e)
        {
            _Overlay.Markers.Clear();
            GMapControl gm = ((GMapControl)sender);
            MouseEventArgs hj = ((MouseEventArgs)e);
            PointLatLng plg = gm.FromLocalToLatLng(hj.X, hj.Y);
            _latitud = plg.Lat;
            _longitud = plg.Lng;
            MP_AgregarPunto(plg, "", "");
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = 0;
                Dgv_GBuscador.Row = _MPos;
            }
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_GBuscador.Row;
            if (_MPos > 0 && Dgv_GBuscador.RowCount > 0)
            {
                _MPos = _MPos - 1;
                Dgv_GBuscador.Row = _MPos;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_GBuscador.Row;
            if (_MPos < Dgv_GBuscador.RowCount - 1 && _MPos >= 0)
            {
                _MPos = Dgv_GBuscador.Row + 1;
                Dgv_GBuscador.Row = _MPos;
            }
        }

        private void btnUltimo_Click_1(object sender, EventArgs e)
        {
            _MPos = Dgv_GBuscador.Row;
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = Dgv_GBuscador.RowCount - 1;
                Dgv_GBuscador.Row = _MPos;
            }
        }
        private void btn_TipoAlojamiento_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO_ALOJAMIENTO));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO_ALOJAMIENTO),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_TipoAlojamiento.Text == "" ? "" : Cb_TipoAlojamiento.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_TipoAlojamiento,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO_ALOJAMIENTO)).ToList());
                    Cb_TipoAlojamiento.SelectedIndex = ((List<VLibreria>)Cb_TipoAlojamiento.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, GLMensaje.Error);
            }
        }
        private void btn_Ciudad_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_CIUDAD));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_CIUDAD),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_Ciudad.Text == "" ? "" : Cb_Ciudad.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_Ciudad,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_CIUDAD)).ToList());
                    Cb_Ciudad.SelectedIndex = ((List<VLibreria>)Cb_Ciudad.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, GLMensaje.Error);
            }
        }
        private void btn_TipoProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_TipoProveedor.Text == "" ? "" : Cb_TipoProveedor.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_TipoProveedor,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO)).ToList());
                    Cb_TipoProveedor.SelectedIndex = ((List<VLibreria>)Cb_TipoProveedor.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, GLMensaje.Error);
            }
        }
        private void Btn_LineaGen_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                         Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_LINEA_GENETICA));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_LINEA_GENETICA),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_LineaGen.Text == "" ? "" : Cb_LineaGen.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_LineaGen,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                                Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_LINEA_GENETICA)).ToList());
                    Cb_LineaGen.SelectedIndex = ((List<VLibreria>)Cb_LineaGen.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, GLMensaje.Error);
            }
        }
        private void Cb_TipoProveedor_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_TipoProveedor, btn_TipoProveedor);
        }

        private void Cb_Ciudad_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_Ciudad, btn_Ciudad);
        }

        private void Cb_TipoAlojamiento_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_TipoAlojamiento, btn_TipoAlojamiento);
        }
        private void Cb_LineaGen_ValueChanged(object sender, EventArgs e)
        {
            MP_SeleccionarButtonCombo(Cb_LineaGen, Btn_LineaGen);
        }
        private void Dgv_Detalle_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_Detalle.Row >= 0 && Dgv_Detalle.RowCount >= 0)
                {
                    MP_MostrarRegistroDetalle(Dgv_Detalle.Row);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message.ToUpper());
            }            
        }
        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            MP_HabilitarDetalle();
            MP_HabilitarMenu(1);
            MP_LimpiarDetalle();
            _NuevoDetalle = true;
            Dgv_Detalle.Enabled = false;
            VM_Nuevo = false;
        }
        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion
        #region Metodos Privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                MP_InicioArmarCombo(1);
                MP_IniciarMapa();
                btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Comuniquece con el administrador del sistema");
            }                    
        }
        private void MP_InicioArmarCombo(int detalle)
        {
            if (detalle == 1)
            {
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarCombo(Cb_Ciudad,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_CIUDAD)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_TipoProveedor,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_TipoAlojamiento,
              new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                            Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO_ALOJAMIENTO)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_LineaGen,
                      new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                    Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_LINEA_GENETICA)).ToList());

            }
            else
            {
                UTGlobal.MG_ArmarCombo(Cb_TipoAlojamiento,
              new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                            Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO_ALOJAMIENTO)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_LineaGen,
                      new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR),
                                                                                    Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_LINEA_GENETICA)).ToList());
            }
        
        }
        private void MP_Map()
        {
            Gmc_Proveedor.DragButton = MouseButtons.Left;
            Gmc_Proveedor.CanDragMap = true;
            Gmc_Proveedor.MapProvider = GMapProviders.GoogleMap;
            if (_latitud != 0 && _longitud != 0)
            {
                Gmc_Proveedor.Position = new PointLatLng(_latitud, _longitud);
            }
            else
            {
                _Overlay.Markers.Clear();
                Gmc_Proveedor.Position = new PointLatLng(-17.3931784, -66.1738852);
            }
            Gmc_Proveedor.MinZoom = 0;
            Gmc_Proveedor.MaxZoom = 24;
            Gmc_Proveedor.Zoom = 15.5;
            Gmc_Proveedor.AutoScroll = true;
            GMapProvider.Language = LanguageType.Spanish;
        }
        private void MP_IniciarMapa()
        {          
            _Overlay = new GMapOverlay("points");
            Gmc_Proveedor.Overlays.Add(_Overlay);
            MP_Map();
        }       
        private void MP_CargarEncabezado()
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ProveedorListar();                
                if (result.Count() > 0)
                {
                    Dgv_GBuscador.DataSource = result;
                    Dgv_GBuscador.RetrieveStructure();
                    Dgv_GBuscador.AlternatingColors = true;

                    Dgv_GBuscador.RootTable.Columns[0].Key = "id";
                    Dgv_GBuscador.RootTable.Columns[0].Visible = false;

                    Dgv_GBuscador.RootTable.Columns[1].Key = "Descipcion";
                    Dgv_GBuscador.RootTable.Columns[1].Caption = "Nombre";
                    Dgv_GBuscador.RootTable.Columns[1].Width = 300;
                    Dgv_GBuscador.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns[1].Visible = true;

                    Dgv_GBuscador.RootTable.Columns[2].Key = "Ciudad";
                    Dgv_GBuscador.RootTable.Columns[2].Caption = "Ciudad";
                    Dgv_GBuscador.RootTable.Columns[2].Width = 250;
                    Dgv_GBuscador.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns[2].Visible = false;

                    Dgv_GBuscador.RootTable.Columns[4].Key = "Contacto";
                    Dgv_GBuscador.RootTable.Columns[4].Caption = "Contacto";
                    Dgv_GBuscador.RootTable.Columns[4].Width = 280;
                    Dgv_GBuscador.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns[4].Visible = true;


                    Dgv_GBuscador.RootTable.Columns[3].Key = "NombreCiudad";
                    Dgv_GBuscador.RootTable.Columns[3].Caption = "Ciudad";
                    Dgv_GBuscador.RootTable.Columns[3].Width = 250;
                    Dgv_GBuscador.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns[3].Visible = true;

                    Dgv_GBuscador.RootTable.Columns[5].Key = "Telefono";
                    Dgv_GBuscador.RootTable.Columns[5].Caption = "Telefono";
                    Dgv_GBuscador.RootTable.Columns[5].Width = 250;
                    Dgv_GBuscador.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns[5].Visible = true;

                    //Habilitar filtradores
                    Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                    Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_GBuscador.GroupByBoxVisible = false;
                    Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }            
        }
        private void MP_CargarDetalle(int id)
        {
            try
            {
                _proveedorDetalle = new ServiceDesktop.ServiceDesktopClient().Proveedor_01ListarXId(id).ToList();            
                if (_proveedorDetalle.Count() > 0)
                {
                    ArmarDetalle();
                }
            }              
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message.ToUpper());
            }
           
        }
        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this,
                                   mensaje,
                                   PRESENTER.Properties.Resources.WARNING,
                                   (int)GLMensajeTamano.Mediano,
                                   eToastGlowColor.Green,
                                   eToastPosition.TopCenter);
        }
        private void ArmarDetalle()
        {
            Dgv_Detalle.DataSource = _proveedorDetalle;
            Dgv_Detalle.RetrieveStructure();
            Dgv_Detalle.AlternatingColors = true;

            Dgv_Detalle.RootTable.Columns[0].Key = "id";
            Dgv_Detalle.RootTable.Columns[0].Visible = false;

            Dgv_Detalle.RootTable.Columns[1].Key = "IdLinea";
            Dgv_Detalle.RootTable.Columns[1].Visible = false;

            Dgv_Detalle.RootTable.Columns[2].Key = "Linea";
            Dgv_Detalle.RootTable.Columns[2].Caption = "Linea genetica";
            Dgv_Detalle.RootTable.Columns[2].Width = 300;
            Dgv_Detalle.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns[2].CellStyle.FontSize = 8;
            Dgv_Detalle.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Detalle.RootTable.Columns[2].Visible = true;

            Dgv_Detalle.RootTable.Columns[3].Key = "Fecha";
            Dgv_Detalle.RootTable.Columns[3].Caption = "Fecha de Nacimiento";
            Dgv_Detalle.RootTable.Columns[3].Width = 350;
            Dgv_Detalle.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns[3].CellStyle.FontSize = 8;
            Dgv_Detalle.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns[3].Visible = true;

            Dgv_Detalle.RootTable.Columns[4].Key = "EdadSeman";
            Dgv_Detalle.RootTable.Columns[4].Caption = "Edad en Semanas";
            Dgv_Detalle.RootTable.Columns[4].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns[4].Width = 150;
            Dgv_Detalle.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns[4].CellStyle.FontSize = 8;
            Dgv_Detalle.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns[4].Visible = true;

            Dgv_Detalle.RootTable.Columns[5].Key = "Cantidad";
            Dgv_Detalle.RootTable.Columns[5].Caption = "Aves Alojadas";
            Dgv_Detalle.RootTable.Columns[5].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns[5].Width = 150;
            Dgv_Detalle.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns[5].CellStyle.FontSize = 8;
            Dgv_Detalle.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns[5].Visible = true;

            Dgv_Detalle.RootTable.Columns[6].Key = "IdTipoAloja";
            Dgv_Detalle.RootTable.Columns[6].Visible = false;

            Dgv_Detalle.RootTable.Columns[7].Key = "TipoAlojamiento";
            Dgv_Detalle.RootTable.Columns[7].Caption = "Tipo Alojamiento";
            Dgv_Detalle.RootTable.Columns[7].Width = 150;
            Dgv_Detalle.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns[7].CellStyle.FontSize = 8;
            Dgv_Detalle.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Detalle.RootTable.Columns[7].Visible = true;

            //Habilitar filtradores
            Dgv_Detalle.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            Dgv_Detalle.FilterMode = FilterMode.Automatic;
            Dgv_Detalle.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
            Dgv_Detalle.GroupByBoxVisible = false;
            Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
        }

        private void MP_Habilitar()
        {
            Tb_Cod.ReadOnly = false;
            Tb_CodSpyre.ReadOnly = false;
            Tb_Descripcion.ReadOnly = false;
            Cb_TipoProveedor.Enabled = true;
            Tb_Direccion.ReadOnly = false;
            Tb_Contacto.ReadOnly = false;         
            Tb_Telefono.ReadOnly = false;
            Tb_Email1.ReadOnly = false;
            Tb_Contacto2.ReadOnly = false;
            Tb_Telefono2.ReadOnly = false;
            Tb_Email2.ReadOnly = false;
            Cb_Ciudad.Enabled = true;          
            Dgv_Detalle.Enabled = false;
            sw_Tipo.IsReadOnly = true;
            btn_Agregar.Enabled = false;
            MP_HabilitarDetalle();
            UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Imagen, ENSubCarpetas.ImagenesProveedor);
            UTGlobal.MG_CrearCarpetaTemporal();
        }
        private void MP_HabilitarDetalle()
        {            
            Cb_LineaGen.Enabled = true;
            BtAdicionar.Enabled = true;
            Tb_Fecha.Value = DateTime.Now;
            Tb_Aves.IsInputReadOnly = false;
            Cb_TipoAlojamiento.Enabled = true;                             
        }
        private void MP_HabilitarMenu( int tipo)
        {
            if (tipo == 1)
            {
                BtnNuevo.Enabled = false;
                BtnModificar.Enabled = false;
                BtnEliminar.Enabled = false;
                BtnGrabar.Enabled = true;
                btnSiguiente.Enabled = true;
                BtnImprimir.Enabled = false;
                BtnExportar.Enabled = false;
            }
            else
            {
                BtnNuevo.Enabled = true;
                BtnModificar.Enabled = true;
                BtnEliminar.Enabled = true;
                BtnGrabar.Enabled = false;
                btnSiguiente.Enabled = false;
                BtnImprimir.Enabled = true;
                BtnExportar.Enabled = true;
            }  
        }
        private void MP_InHabilitar()
        {
            Tb_Cod.ReadOnly = true;
            Tb_CodSpyre.ReadOnly = true;
            Tb_Descripcion.ReadOnly = true;
            Cb_TipoProveedor.Enabled = false;
            Cb_Ciudad.Enabled = false;
            Tb_Direccion.ReadOnly = true;
            Tb_Contacto.ReadOnly = true;    
            Tb_Telefono.ReadOnly = true;
            Tb_Email1.ReadOnly = true;
            Tb_Contacto2.ReadOnly = true;
            Tb_Telefono2.ReadOnly = true;
            Tb_Email2.ReadOnly = true;
            Cb_Ciudad.Enabled = false;
            Cb_LineaGen.Enabled = false;
            BtAdicionar.Enabled = false;
            _Limpiar = false;
            Tb_Aves.IsInputReadOnly = true;
            Cb_TipoAlojamiento.Enabled = false;
            
            btn_Agregar.Enabled = true;
            Dgv_Detalle.Enabled = true;
            sw_Tipo.IsReadOnly = false;
        }
        private void MP_Limpiar()
        {
            Tb_Cod.Clear();
            Tb_CodSpyre.Clear();
            Tb_Descripcion.Clear();                       
            Tb_Direccion.Clear();
            Tb_Contacto.Clear();   
            Tb_Telefono.Clear();
            Tb_Email1.Clear();
            Tb_Contacto2.Clear();
            Tb_Telefono2.Clear();
            Tb_Email2.Clear();
            Tb_Fecha.Value = DateTime.Now;
            Tb_Aves.Value = 0;
            

            if (_Limpiar == false)
            {
                UTGlobal.MG_SeleccionarCombo(Cb_Ciudad);
                UTGlobal.MG_SeleccionarCombo(Cb_TipoProveedor);
                UTGlobal.MG_SeleccionarCombo(Cb_TipoAlojamiento);
                UTGlobal.MG_SeleccionarCombo(Cb_LineaGen);
            }
            if (Dgv_Detalle.RowCount!= 0)
            {
                Dgv_Detalle.DataSource = null;
                //((DataTable)Dgv_Detalle.DataSource).Clear();
            }           
            sw_Tipo.IsReadOnly = false;
           //  Dgv_Detalle.DataSource = null;
        }      
         private void MP_LimpiarDetalle()
        {           
            BtAdicionar.Enabled = true;
            Tb_Fecha.Value = DateTime.Now;
            Tb_Aves.Value = 0;           
            if (_Limpiar == false)
            {          
                UTGlobal.MG_SeleccionarCombo(Cb_TipoAlojamiento);
                UTGlobal.MG_SeleccionarCombo(Cb_LineaGen);
            }
            MP_InicioArmarCombo(2);
        }
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_GBuscador.Row = _Pos;
                _idOriginal = (int)Dgv_GBuscador.GetValue("id");
                if (_idOriginal != 0)
                {
                    var tabla = new ServiceDesktop.ServiceDesktopClient().ProveedorListarXId(_idOriginal).ToArray();
                    VProveedor proveedor = tabla.First();
                    if (tabla.Length > 0)
                    {
                        Tb_Cod.Text = proveedor.Id.ToString();
                        Tb_CodSpyre.Text = proveedor.IdSpyre.ToString();
                        Tb_Descripcion.Text = proveedor.Descripcion.ToString();
                        Tb_Direccion.Text = proveedor.Direccion.ToString();
                        Tb_Contacto.Text = proveedor.Contacto.ToString();
                        Tb_Telefono.Text = proveedor.Telefono.ToString();
                        Tb_Email1.Text = proveedor.Email.ToString();
                        Tb_Contacto2.Text = proveedor.Contacto.ToString();
                        Tb_Telefono2.Text = proveedor.Email.ToString();
                        Tb_Email2.Text = proveedor.Email.ToString();
                        sw_Tipo.Value = proveedor.Tipo == 1 ? true : false;
                        Cb_Ciudad.Value = proveedor.Ciudad;
                        Cb_TipoProveedor.Value = proveedor.TipoProveeedor;
                        _latitud = Convert.ToDouble(proveedor.Latitud);
                        _longitud = Convert.ToDouble(proveedor.Longittud);
                        MP_DibujarUbicacion(Tb_Descripcion.Text, "");
                        MP_MostrarImagen(proveedor.Imagen);
                        MP_CargarDetalle(Convert.ToInt32(Tb_Cod.Text));
                        //Tb_Cod.Text = proveedor.Id.ToString();
                        //Tb_CodSpyre.Text = tabla.Where(x => !string.IsNullOrEmpty(x.IdSpyre)).Count() > 0 ? tabla.Select(x => x.IdSpyre).First().ToString() : "";
                        //Tb_Descripcion.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Descripcion)).Count() > 0 ? tabla.Select(x => x.Descripcion).First().ToString() : "";
                        //Tb_Direccion.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Direccion)).Count() > 0 ? tabla.Select(x => x.Direccion).First().ToString() : "";
                        //Tb_Contacto.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Contacto)).Count() > 0 ? tabla.Select(x => x.Contacto).First().ToString() : "";
                        //Tb_Telefono.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Telefono)).Count() > 0 ? tabla.Select(x => x.Telefono).First().ToString() : "";
                        //Tb_Email1.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Email)).Count() > 0 ? tabla.Select(x => x.Email).First().ToString() : "";
                        //sw_Tipo.Value = tabla.Select(x => x.Tipo).First() == 1 ? true : false;
                        //Cb_Ciudad.Value = tabla.Select(x => x.Ciudad).First();
                        //Cb_TipoProveedor.Value = tabla.Select(x => x.TipoProveeedor).First();
                        //_latitud = Convert.ToDouble(tabla.Select(x => x.Latitud).First());
                        //_longitud = Convert.ToDouble(tabla.Select(x => x.Longittud).First());
                        //MP_DibujarUbicacion(Tb_Descripcion.Text, "");
                        //MP_CargarDetalle(Convert.ToInt32(Tb_Cod.Text));
                    }
                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }

        private void MP_MostrarRegistroDetalle(int _Pos)
        {
            if (Dgv_Detalle.RowCount != 0)
            {
                //Aqui fecha validar conversion correcta salta error
                Dgv_Detalle.Row = _Pos;
                Cb_LineaGen.Value = Convert.ToInt32(Dgv_Detalle.GetValue(1));
                Tb_Fecha.Value = (DateTime)Dgv_Detalle.GetValue(3);               
                Tb_Aves.Text = Dgv_Detalle.GetValue(5).ToString();
                Cb_TipoAlojamiento.Value = Convert.ToInt32(Dgv_Detalle.GetValue(6));
            }           
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarEncabezado();
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = 0;
                MP_MostrarRegistro(tipo == 1 ? _MPos : Dgv_GBuscador.RowCount - 1);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }
        private void MP_MostrarImagen(string _NombreImagen)
        {
            if (_NombreImagen.Equals("Default.jpg") || !File.Exists(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProveedor) + _NombreImagen))
            {
                Bitmap img = new Bitmap(PRESENTER.Properties.Resources.PANTALLA);
                Pc_Img.Image = img;
            }
            else
            {
                if (File.Exists(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProveedor) + _NombreImagen))
                {
                    MemoryStream Bin = new MemoryStream();
                    Bitmap img = new Bitmap(new Bitmap(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProveedor) + _NombreImagen));
                    img.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Pc_Img.SizeMode = PictureBoxSizeMode.StretchImage;
                    Pc_Img.Image = Image.FromStream(Bin);
                    Bin.Dispose();
                }
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

                        if (Tb_Cod.Text == String.Empty)
                        {
                            int mayor;
                            mayor = Convert.ToInt32(Dgv_GBuscador.GetTotal(Dgv_GBuscador.RootTable.Columns[0], AggregateFunction.Max));
                            _imagen = @"\Imagen_" + Convert.ToString(mayor + 1).Trim() + ".jpg";
                            Pc_Img.SizeMode = PictureBoxSizeMode.StretchImage;
                            Pc_Img.Image = Image.FromStream(Bin);

                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                        }
                        else
                        {
                            _imagen = @"\Imagen_" + Tb_Cod.Text.Trim() + ".jpg";
                            Pc_Img.Image = Image.FromStream(Bin);
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
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
                return "";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private bool MP_ValidarDetalle()
        {
            bool _Error = false;
            if (Cb_LineaGen.SelectedIndex == -1)
            {
                Cb_LineaGen.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_LineaGen.BackColor = Color.White;

            if (Cb_TipoAlojamiento.SelectedIndex == -1)
            {
                Cb_TipoAlojamiento.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_TipoAlojamiento.BackColor = Color.White;
            if (Tb_Fecha.Value > DateTime.Now.Date)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser mayor ala fecha actual");
                _Error = true;
            }
            if (Tb_Aves.Value < 0)
            {
                MessageBox.Show("La cantidad de aves tiene que ser mayor a 0");
                _Error = true;
            }
            return _Error;
        }
        public static DataTable ListaATabla(List<VProveedor_01Lista> lista)
        {
            DataTable tabla = new DataTable();
            //Crear la Estructura de la Tabla a partir de la Lista de Objetos
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                tabla.Columns.Add(propiedades[i].Name, propiedades[i].PropertyType);
            }
            //Llenar la Tabla desde la Lista de Objetos
            DataRow fila = null;
            for (int i = 0; i < lista.Count; i++)
            {
                propiedades = lista[i].GetType().GetProperties();
                fila = tabla.NewRow();
                for (int j = 0; j < propiedades.Length; j++)
                {
                    fila[j] = propiedades[j].GetValue(lista[i], null);
                }
                tabla.Rows.Add(fila);
            }
            return (tabla);
        }
        private void MP_InsertarDetalle()
        {
            try
            {                
                string Linea, TipoAlojamiento;
                DateTime FechaNac;
                double CantidadAves;
                int IdTipoAlojamiento, IdLineaGen;

                //idProveedorDetalle = _NuevoSeg ?Convert.ToInt32(Dgv_Detalle.GetValue("Id")) : 0;
                IdTipoAlojamiento = Convert.ToInt32(Cb_TipoAlojamiento.Value);
                IdLineaGen = Convert.ToInt32(Cb_LineaGen.Value);
                CantidadAves = Tb_Aves.Value;
                FechaNac = Tb_Fecha.Value.Date;
                Linea = Cb_LineaGen.Text;
                TipoAlojamiento = Cb_TipoAlojamiento.Text;
                TimeSpan Dias = DateTime.Now.Date - FechaNac;
                string edadSema = Convert.ToString(Dias.Days / 7);
                if (_NuevoDetalle) //Insertar un detalle
                {
                    _proveedorDetalle.Insert(_proveedorDetalle.Count, new VProveedor_01Lista()
                    {
                        Id = 0,
                        IdLinea = IdLineaGen,
                        Linea = Linea,
                        FechaNac = FechaNac,
                        EdadSeman = edadSema,
                        Cantidad = Convert.ToDecimal(CantidadAves),
                        IdTipoAloja = IdTipoAlojamiento,
                        TipoAlojamiento = TipoAlojamiento
                    });
                }
                else//Modificar el detalle
                {
                    foreach (var lProveedor in _proveedorDetalle)
                    {
                        if (lProveedor.Id == Convert.ToUInt32(Dgv_Detalle.GetValue("Id")))
                        {
                            lProveedor.IdLinea = IdLineaGen;
                            lProveedor.Linea = Linea;
                            lProveedor.FechaNac = FechaNac;
                            lProveedor.EdadSeman = edadSema;
                            lProveedor.Cantidad = Convert.ToDecimal(CantidadAves);
                            lProveedor.IdTipoAloja = IdTipoAlojamiento;
                            lProveedor.TipoAlojamiento = TipoAlojamiento;
                        }
                    }
                }                
                ArmarDetalle();
                //if (Dgv_Detalle.DataSource != null)
                //{
                //    proveedor_01.Add((VProveedor_01Lista)Dgv_Detalle.DataSource);
                //}               
                ////proveedor_01.Add((VProveedor_01Lista)Dgv_Detalle.DataSource);
                //VProveedor_01Lista proveedor = new VProveedor_01Lista()
                //{
                //    Id = 0,
                //    IdLinea = IdLineaGen,
                //    Linea = Linea,
                //    FechaNac = FechaNac,
                //    EdadSeman = edadSema,
                //    Cantidad = Convert.ToDecimal(CantidadAves),
                //    IdTipoAloja = IdTipoAlojamiento,
                //    TipoAlojamiento = TipoAlojamiento
                //};                
                //proveedor_01.Add(proveedor);
                //Dgv_Detalle.DataSource =  proveedor_01.ToList();

                // Dgv_Detalle.AddItem(proveedor_01);

                //DataRow nuevaFila = ((DataTable)Dgv_Detalle.DataSource).NewRow();
                //nuevaFila[0] = 0;
                //nuevaFila[1] = IdLineaGen;
                //nuevaFila[2] = Linea;
                //nuevaFila[3] = FechaNac;
                //nuevaFila[4] = edadSema;
                //nuevaFila[5] = CantidadAves;
                //nuevaFila[6] = IdTipoAlojamiento;
                //nuevaFila[7] = TipoAlojamiento;
                //((DataTable)Dgv_Detalle.DataSource).Rows.Add(nuevaFila);
                Dgv_Detalle.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        #endregion
        #region Metodos Heredados
        public override bool MH_NuevoRegistro()
        {
            bool resultadoDetalle = false;
            bool resultado = false;
            string mensaje = "";
            if (_NuevoDetalle)
            {
                if (MP_ValidarDetalle())
                {
                    resultadoDetalle = true;
                }
                //Ingresa o modifica un detalle
                MP_InsertarDetalle();
            }
            if (!resultadoDetalle)
            {              
                VProveedor Proveedor = new VProveedor()
                {
                    IdSpyre = Tb_CodSpyre.Text,
                    Descripcion = Tb_Descripcion.Text,
                    Direccion = Tb_Direccion.Text,
                    Contacto = Tb_Contacto.Text,
                    Telefono = Tb_Telefono.Text,
                    Email = Tb_Email1.Text,
                    Contacto2 = Tb_Contacto2.Text,
                    Telefono2 = Tb_Telefono2.Text,
                    Email2 = Tb_Email2.Text,
                    Ciudad = Convert.ToInt32(Cb_Ciudad.Value),
                    Tipo = sw_Tipo.Value == true ? 1 : 2,
                    TipoProveeedor = Convert.ToInt32(Cb_TipoProveedor.Value),
                    Latitud = Convert.ToDecimal(_latitud),
                    Longittud = Convert.ToDecimal(_longitud),
                    Imagen = _imagen,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                int id = Tb_Cod.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Cod.Text);
                int idAux = id;
                var detalle = ((List<VProveedor_01Lista>)Dgv_Detalle.DataSource).ToArray<VProveedor_01Lista>();

                resultado = new ServiceDesktop.ServiceDesktopClient().ProveedorGuardar(Proveedor, detalle, ref id, TxtNombreUsu.Text);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        Tb_CodSpyre.Focus();
                        MP_CargarEncabezado();
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                    }
                    else//Modificar
                    {
                        if (_ModificarImagen)
                        {
                            UTGlobal.MG_MoverImagenRuta(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesProveedor), _imagen, Pc_Img);
                            _ModificarImagen = false;
                        }
                        MP_Filtrar(1);
                        MP_InHabilitar();//El formulario
                        _Limpiar = true;
                        _imagen = "Default.jpg";
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                        MH_Habilitar();//El menu                   
                    }
                }
                //Resultado
                if (resultado)
                {
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                {
                    mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
            }
            else
            {
                mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
            }
            return resultado;
        }
        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();
            _NuevoDetalle = true;

        }
        public override void MH_Modificar()
        {
            MP_Habilitar();
            _proveedorDetalle = (List<VProveedor_01Lista>)Dgv_Detalle.DataSource;
            _NuevoDetalle = false;
        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(1);
        }
        private void MP_AgregarPunto(PointLatLng pointLatLng, string _nombre, string _ci)
        {
            if (_Overlay != null)
            {
                GMarkerGoogle marker = new GMarkerGoogle(pointLatLng, PRESENTER.Properties.Resources.MARKERICONO);
                //añadir tooltip
                MarkerTooltipMode mode = MarkerTooltipMode.OnMouseOver;
                marker.ToolTip = new GMapBaloonToolTip(marker);
                marker.ToolTipMode = mode;
                SolidBrush ToolTipBackColor = new SolidBrush(Color.Blue);
                marker.ToolTip.Fill = ToolTipBackColor;
                marker.ToolTip.Foreground = Brushes.White;
                _Overlay.Markers.Add(marker);
                Gmc_Proveedor.Position = pointLatLng;
            }
        }
        private void MP_DibujarUbicacion(string _nombre, string _ci)
        {
            if (_latitud != 0 && _longitud != 0)
            {
                PointLatLng plg = new PointLatLng(_latitud, _longitud);
                _Overlay.Markers.Clear();
                MP_AgregarPunto(plg, _nombre, _ci);
            }
            else
            {
                _Overlay.Markers.Clear();
                Gmc_Proveedor.Position = new PointLatLng(-17.3931784, -66.1738852);
            }
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

            if (Cb_Ciudad.SelectedIndex == -1)
            {
                Cb_Ciudad.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_Ciudad.BackColor = Color.White;
            if (Cb_TipoProveedor.SelectedIndex == -1)
            {
                Cb_TipoProveedor.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_TipoProveedor.BackColor = Color.White;
            return _Error;
        }   
        #endregion
  
    }
}
