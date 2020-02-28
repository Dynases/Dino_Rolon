using Janus.Windows.GridEX;
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

namespace MODEL
{
    public partial class ModeloAyuda : Form
    {
        #region Varibles
        public  DataTable dtBuscador = new DataTable();
        public  string nombreVista;
        public  int posX, posY;
        public  bool seleccionado;
        public  int Columna = -1;
        public  Janus.Windows.GridEX.GridEXRow filaSelect;
        public  List<GLCelda> listEstrucGrilla;
        //GridEX filaSelect = new GridEX();
        //List(Of Celda)
        #endregion
        public ModeloAyuda(int x, int y, DataTable dt1, string titulo, List<GLCelda> listEst)
        {
            dtBuscador = dt1;
            posX = x;
            posY = y;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            GPPanelP.Text = titulo;
            listEstrucGrilla = listEst;
            seleccionado = false;
            _PMCargarBuscador();
            Columna = 2;
        }
        #region METODOS PRIVADOS
        private void _PMCargarBuscador()
        {
            int anchoVentana = 0;
            grJBuscador.DataSource = dtBuscador;
            grJBuscador.RetrieveStructure();

            for (int i = 0; dtBuscador.Columns.Count -1 >= i; i++)
            {
                if (listEstrucGrilla[i].visible)
                {
                    grJBuscador.RootTable.Columns[i].Caption = listEstrucGrilla[i].titulo;
                    grJBuscador.RootTable.Columns[i].Width = listEstrucGrilla[i].tamano;
                    grJBuscador.RootTable.Columns[i].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    grJBuscador.RootTable.Columns[i].CellStyle.FontSize = 9;

                    DataColumn col = dtBuscador.Columns[i];
                    Type tipo = col.DataType;
                    if (tipo.ToString() == "System.Int32" || tipo.ToString() == "System.Decimal")
                    {
                        grJBuscador.RootTable.Columns[i].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    }
                    if (listEstrucGrilla[i].formato == string.Empty)
                    {
                        grJBuscador.RootTable.Columns[i].FormatString = listEstrucGrilla[i].formato;
                    }
                    anchoVentana = anchoVentana + listEstrucGrilla[i].tamano;

                }
                else
                    grJBuscador.RootTable.Columns[i].Visible = false;
            }
            grJBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            grJBuscador.FilterMode = FilterMode.Automatic;
            grJBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            grJBuscador.GroupByBoxVisible = false;
            grJBuscador.VisualStyle = VisualStyle.Office2007;
        }

        private void ModeloAyuda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar);
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void grJBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyData == Keys.Enter)
            {
                filaSelect = grJBuscador.GetRow();
                seleccionado = true;
                this.Close();
            }
        }

        public void MP_Seleccionar()
        {
            if (Columna >= 0)
            {
                grJBuscador.Select();
                grJBuscador.MoveTo(grJBuscador.FilterRow);
                grJBuscador.Col = Columna;
            }
        }
        #endregion
    }
}
