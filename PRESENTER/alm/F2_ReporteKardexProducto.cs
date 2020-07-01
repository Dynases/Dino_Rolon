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
using UTILITY;
using DevComponents.DotNetBar;
using Microsoft.Reporting.WinForms;
using ENTITY.inv.Almacen.View;
using MODEL;
using Janus.Windows.GridEX;



namespace PRESENTER.alm
{   
    public partial class F2_ReporteKardexProducto : MODEL.ModeloF2
    {
        public static List<VDetalleKardex> detalleKardex;
        public F2_ReporteKardexProducto()
        {
            InitializeComponent();          
            Rpt_Reporte.LocalReport.EnableExternalImages = true;
            Rpt_Reporte.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            Rpt_Reporte.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            Rpt_Reporte.ZoomPercent = 100;          
           
        }

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacenes, almacenes);

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_InsertarProducto()
        {
            try
            {
                int idAlmacen = Convert.ToInt32(Cb_Almacenes.Value);
                var result = new ServiceDesktop.ServiceDesktopClient().ListarInventarioXAlmacenId(idAlmacen).ToList();
                Dgv_Producto.DataSource = result;
                if (result.Count > 0)
                {
                    Dgv_Producto.RetrieveStructure();
                    Dgv_Producto.AlternatingColors = true;

                    Dgv_Producto.RootTable.Columns[0].Key = "InventarioId";
                    Dgv_Producto.RootTable.Columns[0].Visible = false;

                    Dgv_Producto.RootTable.Columns[1].Key = "ProductoId";
                    Dgv_Producto.RootTable.Columns[1].Caption = "COD";
                    Dgv_Producto.RootTable.Columns[1].Width = 60;
                    Dgv_Producto.RootTable.Columns[1].Visible = true;
                    //Dgv_Producto.RootTable.Columns[1].EditType = EditType.NoEdit;

                    Dgv_Producto.RootTable.Columns[2].Key = "AlmacenId";
                    Dgv_Producto.RootTable.Columns[2].Visible = false;

                    Dgv_Producto.RootTable.Columns[3].Key = "Descripcion";
                    Dgv_Producto.RootTable.Columns[3].Caption = "Descripcion";
                    Dgv_Producto.RootTable.Columns[3].Width = 320;
                    Dgv_Producto.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Producto.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Producto.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Producto.RootTable.Columns[3].Visible = true;
                    //Dgv_Producto.RootTable.Columns[3].EditType = EditType.NoEdit;

                    Dgv_Producto.RootTable.Columns[4].Key = "Saldo";
                    Dgv_Producto.RootTable.Columns[4].Caption = "Saldo";
                    Dgv_Producto.RootTable.Columns[4].Width = 80;
                    Dgv_Producto.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Producto.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Producto.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Producto.RootTable.Columns[4].Visible = false;
                    Dgv_Producto.RootTable.Columns[4].EditType = EditType.NoEdit;

                    Dgv_Producto.RootTable.Columns[5].Key = "División";
                    Dgv_Producto.RootTable.Columns[5].Caption = "División";
                    Dgv_Producto.RootTable.Columns[5].Width = 110;
                    Dgv_Producto.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Producto.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_Producto.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Producto.RootTable.Columns[5].Visible = true;
                    //Dgv_Producto.RootTable.Columns[5].EditType = EditType.NoEdit;

                    Dgv_Producto.RootTable.Columns[6].Key = "Marca";
                    Dgv_Producto.RootTable.Columns[6].Caption = "Marca";
                    Dgv_Producto.RootTable.Columns[6].Width = 110;
                    Dgv_Producto.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Producto.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_Producto.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Producto.RootTable.Columns[6].Visible = true;
                    //Dgv_Producto.RootTable.Columns[6].EditType = EditType.NoEdit;

                    Dgv_Producto.RootTable.Columns[7].Key = "Categorías";
                    Dgv_Producto.RootTable.Columns[7].Caption = "Categoría";
                    Dgv_Producto.RootTable.Columns[7].Width = 110;
                    Dgv_Producto.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Producto.RootTable.Columns[7].CellStyle.FontSize = 8;
                    Dgv_Producto.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Producto.RootTable.Columns[7].Visible = true;
                    //Dgv_Producto.RootTable.Columns[7].EditType = EditType.NoEdit;

                    Dgv_Producto.RootTable.Columns[8].Key = "Unidad";
                    Dgv_Producto.RootTable.Columns[8].Visible = false;

                    Dgv_Producto.RootTable.Columns[9].Key = "UnidadVentaDisplay";
                    Dgv_Producto.RootTable.Columns[9].Caption = "Unidad";
                    Dgv_Producto.RootTable.Columns[9].Width = 80;
                    Dgv_Producto.RootTable.Columns[9].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Producto.RootTable.Columns[9].CellStyle.FontSize = 8;
                    Dgv_Producto.RootTable.Columns[9].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Producto.RootTable.Columns[9].Visible = false;
                    Dgv_Producto.RootTable.Columns[9].EditType = EditType.NoEdit;

                    //Habilitar filtradores
                    Dgv_Producto.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Producto.FilterMode = FilterMode.Automatic;
                    Dgv_Producto.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_Producto.GroupByBoxVisible = false;
                    Dgv_Producto.VisualStyle = VisualStyle.Office2007;

                }

            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_HabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = true;
                GPanel_Producto.Height = 450;
                MP_InsertarProducto();
                Dgv_Producto.Focus();
                Dgv_Producto.MoveTo(Dgv_Producto.FilterRow);
                Dgv_Producto.Col = 3;

            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_InHabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = false;
                GPanel_Producto.Height = 30;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }


        #endregion

        #region Eventos
        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                int codProducto;
                if (checkTodos.Checked)
                {
                    codProducto = 0;
                }

                else
                {
                    codProducto = Convert.ToInt32(tbCodProducto.Text);
                }

                detalleKardex = new ServiceDesktop.ServiceDesktopClient().ListarDetalleKardex(Dt_FechaInicio.Value, Dt_FechaFin.Value, Convert.ToInt32(Cb_Almacenes.Value), codProducto).ToList();
                
                if (CheckMayorCero.Checked)
                {
                    detalleKardex= detalleKardex.Where(h => h.Saldo > 0).ToList();
                }

               

                ReportesDataSet.DetalleKardexDataTable detalleKardexRows = new ReportesDataSet.DetalleKardexDataTable();

                foreach (var i in detalleKardex)
                {
                    detalleKardexRows.AddDetalleKardexRow(i.Id,
                                                          i.Descripcion,
                                                          i.Unidad,
                                                          i.SaldoAnterior,
                                                          i.Entradas,
                                                          i.Salidas,
                                                          i.Saldo);
                }

                this.Rpt_Reporte.LocalReport.Refresh();

                this.Rpt_Reporte.LocalReport.DataSources.Clear();
                Rpt_Reporte.ProcessingMode = ProcessingMode.Local;         
                Rpt_Reporte.LocalReport.ReportEmbeddedResource = "PRESENTER.Report.ReportViewer.KardexGeneral.rdlc";

                List<ReportParameter> lParametros = new List<ReportParameter> { };
                lParametros.Add(new ReportParameter("FechaI", Dt_FechaInicio.Value.ToShortDateString()));
                lParametros.Add(new ReportParameter("FechaF", Dt_FechaFin.Value.ToShortDateString()));
                lParametros.Add(new ReportParameter("Almacen", Cb_Almacenes.Text));


                ReportDataSource r = new ReportDataSource("DetalleKardex", (DataTable)detalleKardexRows);
                this.Rpt_Reporte.LocalReport.DataSources.Add(r);
                Rpt_Reporte.LocalReport.SetParameters(lParametros);

                this.Rpt_Reporte.LocalReport.Refresh();
                Rpt_Reporte.RefreshReport();
                Rpt_Reporte.Visible = true;

                LblPaginacion.Text = detalleKardex.Count.ToString();

            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void F2_ReporteKardexProducto_Load(object sender, EventArgs e)
        {
            MP_CargarAlmacenes();
            LblTitulo.Text = "KARDEX PRODUCTOS";
            TxtNombreUsu.Text = UTGlobal.Usuario;
        }

        private void Dgv_Producto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyData == Keys.Enter))
                {
                    int f, c;
                    c = Dgv_Producto.Col;
                    f = Dgv_Producto.Row;
                    if ((f >= 0))
                    {
                        tbProducto.Text = Dgv_Producto.GetValue("Descripcion").ToString();
                        tbCodProducto.Text = Dgv_Producto.GetValue("ProductoId").ToString();
                        MP_InHabilitarProducto();
                    }

                }
                if (e.KeyData == Keys.Escape)
                {
                    MP_InHabilitarProducto();
                }

            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }



        private void checkTodos_CheckValueChanged(object sender, EventArgs e)
        {
            if ((checkTodos.Checked))
            {
                checkUno.CheckValue = false;
                tbProducto.Enabled = false;
                tbProducto.BackColor = Color.Gainsboro;
                tbProducto.Clear();
                tbCodProducto.Clear();
            }
        }

        private void checkUno_CheckValueChanged(object sender, EventArgs e)
        {
            if ((checkUno.Checked))
            {
                checkTodos.CheckValue = false;
                tbProducto.Enabled = true;
                tbProducto.BackColor = Color.White;
                tbProducto.Focus();
                MP_HabilitarProducto();
            }
        }

        private void Dgv_Producto_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void tbProducto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                {
                    MP_HabilitarProducto();

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
