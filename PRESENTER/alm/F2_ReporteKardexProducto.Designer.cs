namespace PRESENTER.alm
{
    partial class F2_ReporteKardexProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2_ReporteKardexProducto));
            Janus.Windows.GridEX.GridEXLayout Cb_Almacenes_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.Dt_FechaInicio = new MetroFramework.Controls.MetroDateTime();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.Dt_FechaFin = new MetroFramework.Controls.MetroDateTime();
            this.Cb_Almacenes = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.Rpt_Reporte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.checkTodos = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkUno = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.GPanel_Producto = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.Dgv_Producto = new Janus.Windows.GridEX.GridEX();
            this.tbCodProducto = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbProducto = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.checkTodosSaldo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.CheckMayorCero = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.PanelMenu.SuspendLayout();
            this.PanelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleBarUsuario)).BeginInit();
            this.GPanel_Criterio.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cb_Almacenes)).BeginInit();
            this.Panel2.SuspendLayout();
            this.GPanel_Producto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Producto)).BeginInit();
            this.Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMin
            // 
            this.btnMin.Location = new System.Drawing.Point(1425, 0);
            // 
            // btnMax
            // 
            this.btnMax.Location = new System.Drawing.Point(1445, 0);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1465, 0);
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelMenu.BackgroundImage")));
            this.PanelMenu.Size = new System.Drawing.Size(1485, 72);
            // 
            // LblSubtitulo
            // 
            this.LblSubtitulo.Size = new System.Drawing.Size(1485, 36);
            this.LblSubtitulo.Text = "Para generar el reporte, especifique valores para el filtro de búsqueda y luego s" +
    "eleccione la opción Generar";
            // 
            // PanelInferior
            // 
            this.PanelInferior.Location = new System.Drawing.Point(0, 507);
            this.PanelInferior.Size = new System.Drawing.Size(1485, 28);
            // 
            // BubbleBarUsuario
            // 
            // 
            // 
            // 
            this.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3;
            this.BubbleBarUsuario.Location = new System.Drawing.Point(1293, 0);
            this.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight;
            this.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black;
            // 
            // TxtNombreUsu
            // 
            this.TxtNombreUsu.Location = new System.Drawing.Point(1343, 0);
            this.TxtNombreUsu.ReadOnly = true;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // GPanel_Criterio
            // 
            this.GPanel_Criterio.Controls.Add(this.labelX5);
            this.GPanel_Criterio.Controls.Add(this.Panel3);
            this.GPanel_Criterio.Controls.Add(this.tbProducto);
            this.GPanel_Criterio.Controls.Add(this.tbCodProducto);
            this.GPanel_Criterio.Controls.Add(this.labelX6);
            this.GPanel_Criterio.Controls.Add(this.Panel2);
            this.GPanel_Criterio.Controls.Add(this.Cb_Almacenes);
            this.GPanel_Criterio.Controls.Add(this.labelX4);
            this.GPanel_Criterio.Controls.Add(this.labelX3);
            this.GPanel_Criterio.Controls.Add(this.Dt_FechaFin);
            this.GPanel_Criterio.Controls.Add(this.labelX1);
            this.GPanel_Criterio.Controls.Add(this.labelX2);
            this.GPanel_Criterio.Controls.Add(this.Dt_FechaInicio);
            this.GPanel_Criterio.Size = new System.Drawing.Size(346, 372);
            // 
            // 
            // 
            this.GPanel_Criterio.Style.BackColor = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.Style.BackColorGradientAngle = 90;
            this.GPanel_Criterio.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderBottomWidth = 1;
            this.GPanel_Criterio.Style.BorderColor = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderLeftWidth = 1;
            this.GPanel_Criterio.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderRightWidth = 1;
            this.GPanel_Criterio.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderTopWidth = 1;
            this.GPanel_Criterio.Style.CornerDiameter = 4;
            this.GPanel_Criterio.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.GPanel_Criterio.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.GPanel_Criterio.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.GPanel_Criterio.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // groupPanel1
            // 
            this.groupPanel1.Size = new System.Drawing.Size(1139, 372);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColor = System.Drawing.Color.Transparent;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Rpt_Reporte);
            this.panel1.Size = new System.Drawing.Size(1133, 344);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.labelX2.Location = new System.Drawing.Point(69, 23);
            this.labelX2.Name = "labelX2";
            this.labelX2.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX2.Size = new System.Drawing.Size(45, 23);
            this.labelX2.TabIndex = 366;
            this.labelX2.Text = "Desde:";
            // 
            // Dt_FechaInicio
            // 
            this.Dt_FechaInicio.CalendarFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.Dt_FechaInicio.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.Dt_FechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt_FechaInicio.Location = new System.Drawing.Point(131, 21);
            this.Dt_FechaInicio.MinimumSize = new System.Drawing.Size(0, 25);
            this.Dt_FechaInicio.Name = "Dt_FechaInicio";
            this.Dt_FechaInicio.Size = new System.Drawing.Size(138, 27);
            this.Dt_FechaInicio.TabIndex = 365;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.labelX1.Location = new System.Drawing.Point(13, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX1.Size = new System.Drawing.Size(45, 23);
            this.labelX1.TabIndex = 367;
            this.labelX1.Text = "Fecha";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.labelX3.Location = new System.Drawing.Point(69, 62);
            this.labelX3.Name = "labelX3";
            this.labelX3.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX3.Size = new System.Drawing.Size(50, 23);
            this.labelX3.TabIndex = 369;
            this.labelX3.Text = "Hasta:";
            // 
            // Dt_FechaFin
            // 
            this.Dt_FechaFin.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.Dt_FechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt_FechaFin.Location = new System.Drawing.Point(131, 62);
            this.Dt_FechaFin.MinimumSize = new System.Drawing.Size(0, 25);
            this.Dt_FechaFin.Name = "Dt_FechaFin";
            this.Dt_FechaFin.Size = new System.Drawing.Size(138, 27);
            this.Dt_FechaFin.TabIndex = 368;
            // 
            // Cb_Almacenes
            // 
            this.Cb_Almacenes.BackColor = System.Drawing.Color.White;
            Cb_Almacenes_DesignTimeLayout.LayoutString = resources.GetString("Cb_Almacenes_DesignTimeLayout.LayoutString");
            this.Cb_Almacenes.DesignTimeLayout = Cb_Almacenes_DesignTimeLayout;
            this.Cb_Almacenes.DisabledBackColor = System.Drawing.Color.Blue;
            this.Cb_Almacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cb_Almacenes.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Far;
            this.Cb_Almacenes.Location = new System.Drawing.Point(13, 133);
            this.Cb_Almacenes.Name = "Cb_Almacenes";
            this.Cb_Almacenes.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom;
            this.Cb_Almacenes.Office2007CustomColor = System.Drawing.Color.DodgerBlue;
            this.Cb_Almacenes.SelectedIndex = -1;
            this.Cb_Almacenes.SelectedItem = null;
            this.Cb_Almacenes.Size = new System.Drawing.Size(298, 22);
            this.Cb_Almacenes.TabIndex = 370;
            this.Cb_Almacenes.Tag = "1";
            this.Cb_Almacenes.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.labelX4.Location = new System.Drawing.Point(13, 104);
            this.labelX4.Name = "labelX4";
            this.labelX4.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX4.Size = new System.Drawing.Size(125, 23);
            this.labelX4.TabIndex = 371;
            this.labelX4.Text = "Almacen:";
            // 
            // Rpt_Reporte
            // 
            this.Rpt_Reporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rpt_Reporte.LocalReport.ReportEmbeddedResource = "PRESENTER.Report.ReportViewer.CompraIngreso.rdlc";
            this.Rpt_Reporte.Location = new System.Drawing.Point(0, 0);
            this.Rpt_Reporte.Name = "Rpt_Reporte";
            this.Rpt_Reporte.ServerReport.BearerToken = null;
            this.Rpt_Reporte.Size = new System.Drawing.Size(1133, 344);
            this.Rpt_Reporte.TabIndex = 1;
            this.Rpt_Reporte.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.checkTodos);
            this.Panel2.Controls.Add(this.checkUno);
            this.Panel2.Location = new System.Drawing.Point(214, 196);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(125, 35);
            this.Panel2.TabIndex = 373;
            // 
            // checkTodos
            // 
            // 
            // 
            // 
            this.checkTodos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkTodos.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkTodos.Checked = true;
            this.checkTodos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTodos.CheckValue = "Y";
            this.checkTodos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTodos.Location = new System.Drawing.Point(57, 7);
            this.checkTodos.Name = "checkTodos";
            this.checkTodos.Size = new System.Drawing.Size(65, 23);
            this.checkTodos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkTodos.TabIndex = 252;
            this.checkTodos.Text = "Todos";
            this.checkTodos.CheckValueChanged += new System.EventHandler(this.checkTodos_CheckValueChanged);
            // 
            // checkUno
            // 
            // 
            // 
            // 
            this.checkUno.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkUno.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkUno.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkUno.Location = new System.Drawing.Point(7, 7);
            this.checkUno.Name = "checkUno";
            this.checkUno.Size = new System.Drawing.Size(48, 23);
            this.checkUno.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkUno.TabIndex = 251;
            this.checkUno.Text = "Uno";
            this.checkUno.CheckValueChanged += new System.EventHandler(this.checkUno_CheckValueChanged);
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.labelX6.Location = new System.Drawing.Point(13, 172);
            this.labelX6.Name = "labelX6";
            this.labelX6.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX6.Size = new System.Drawing.Size(125, 23);
            this.labelX6.TabIndex = 376;
            this.labelX6.Text = "Producto:";
            // 
            // GPanel_Producto
            // 
            this.GPanel_Producto.CanvasColor = System.Drawing.SystemColors.Control;
            this.GPanel_Producto.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.GPanel_Producto.Controls.Add(this.Dgv_Producto);
            this.GPanel_Producto.DisabledBackColor = System.Drawing.Color.Empty;
            this.GPanel_Producto.Location = new System.Drawing.Point(310, 125);
            this.GPanel_Producto.Name = "GPanel_Producto";
            this.GPanel_Producto.Size = new System.Drawing.Size(780, 29);
            // 
            // 
            // 
            this.GPanel_Producto.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.GPanel_Producto.Style.BackColorGradientAngle = 90;
            this.GPanel_Producto.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.GPanel_Producto.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Producto.Style.BorderBottomWidth = 1;
            this.GPanel_Producto.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.GPanel_Producto.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Producto.Style.BorderLeftWidth = 1;
            this.GPanel_Producto.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Producto.Style.BorderRightWidth = 1;
            this.GPanel_Producto.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Producto.Style.BorderTopWidth = 1;
            this.GPanel_Producto.Style.CornerDiameter = 4;
            this.GPanel_Producto.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.GPanel_Producto.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.GPanel_Producto.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.GPanel_Producto.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.GPanel_Producto.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.GPanel_Producto.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.GPanel_Producto.TabIndex = 354;
            this.GPanel_Producto.Text = "PRODUCTOS";
            this.GPanel_Producto.Visible = false;
            // 
            // Dgv_Producto
            // 
            this.Dgv_Producto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Producto.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Producto.Name = "Dgv_Producto";
            this.Dgv_Producto.Size = new System.Drawing.Size(774, 8);
            this.Dgv_Producto.TabIndex = 0;
            this.Dgv_Producto.EditingCell += new Janus.Windows.GridEX.EditingCellEventHandler(this.Dgv_Producto_EditingCell);
            this.Dgv_Producto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_Producto_KeyDown);
            // 
            // tbCodProducto
            // 
            // 
            // 
            // 
            this.tbCodProducto.Border.Class = "TextBoxBorder";
            this.tbCodProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbCodProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCodProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.tbCodProducto.Location = new System.Drawing.Point(85, 172);
            this.tbCodProducto.Name = "tbCodProducto";
            this.tbCodProducto.PreventEnterBeep = true;
            this.tbCodProducto.Size = new System.Drawing.Size(22, 22);
            this.tbCodProducto.TabIndex = 378;
            this.tbCodProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCodProducto.Visible = false;
            // 
            // tbProducto
            // 
            this.tbProducto.BackColor = System.Drawing.Color.Gainsboro;
            // 
            // 
            // 
            this.tbProducto.Border.Class = "TextBoxBorder";
            this.tbProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbProducto.DisabledBackColor = System.Drawing.Color.White;
            this.tbProducto.Enabled = false;
            this.tbProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.tbProducto.ForeColor = System.Drawing.Color.Black;
            this.tbProducto.Location = new System.Drawing.Point(13, 203);
            this.tbProducto.MaxLength = 200;
            this.tbProducto.Name = "tbProducto";
            this.tbProducto.PreventEnterBeep = true;
            this.tbProducto.Size = new System.Drawing.Size(190, 22);
            this.tbProducto.TabIndex = 379;
            this.tbProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbProducto_KeyDown);
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.checkTodosSaldo);
            this.Panel3.Controls.Add(this.CheckMayorCero);
            this.Panel3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel3.Location = new System.Drawing.Point(88, 249);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(180, 40);
            this.Panel3.TabIndex = 381;
            // 
            // checkTodosSaldo
            // 
            // 
            // 
            // 
            this.checkTodosSaldo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkTodosSaldo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkTodosSaldo.Checked = true;
            this.checkTodosSaldo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTodosSaldo.CheckValue = "Y";
            this.checkTodosSaldo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTodosSaldo.Location = new System.Drawing.Point(103, 9);
            this.checkTodosSaldo.Name = "checkTodosSaldo";
            this.checkTodosSaldo.Size = new System.Drawing.Size(60, 23);
            this.checkTodosSaldo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkTodosSaldo.TabIndex = 257;
            this.checkTodosSaldo.Text = "Todos";
            // 
            // CheckMayorCero
            // 
            // 
            // 
            // 
            this.CheckMayorCero.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.CheckMayorCero.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.CheckMayorCero.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckMayorCero.Location = new System.Drawing.Point(10, 10);
            this.CheckMayorCero.Name = "CheckMayorCero";
            this.CheckMayorCero.Size = new System.Drawing.Size(80, 23);
            this.CheckMayorCero.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CheckMayorCero.TabIndex = 257;
            this.CheckMayorCero.Text = "Mayor a 0";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.labelX5.Location = new System.Drawing.Point(13, 259);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX5.Size = new System.Drawing.Size(65, 23);
            this.labelX5.TabIndex = 382;
            this.labelX5.Text = "Saldo:";
            // 
            // F2_ReporteKardexProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1485, 535);
            this.Controls.Add(this.GPanel_Producto);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "F2_ReporteKardexProducto";
            this.Text = "F2_ReporteKardexProducto";
            this.Load += new System.EventHandler(this.F2_ReporteKardexProducto_Load);
            this.Controls.SetChildIndex(this.PanelMenu, 0);
            this.Controls.SetChildIndex(this.LblSubtitulo, 0);
            this.Controls.SetChildIndex(this.PanelInferior, 0);
            this.Controls.SetChildIndex(this.GPanel_Criterio, 0);
            this.Controls.SetChildIndex(this.groupPanel1, 0);
            this.Controls.SetChildIndex(this.GPanel_Producto, 0);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            this.PanelInferior.ResumeLayout(false);
            this.PanelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleBarUsuario)).EndInit();
            this.GPanel_Criterio.ResumeLayout(false);
            this.GPanel_Criterio.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Cb_Almacenes)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.GPanel_Producto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Producto)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal DevComponents.DotNetBar.LabelX labelX2;
        private MetroFramework.Controls.MetroDateTime Dt_FechaInicio;
        protected internal DevComponents.DotNetBar.LabelX labelX1;
        protected internal DevComponents.DotNetBar.LabelX labelX3;
        private MetroFramework.Controls.MetroDateTime Dt_FechaFin;
        internal Janus.Windows.GridEX.EditControls.MultiColumnCombo Cb_Almacenes;
        internal DevComponents.DotNetBar.LabelX labelX4;
        private Microsoft.Reporting.WinForms.ReportViewer Rpt_Reporte;
        internal System.Windows.Forms.Panel Panel2;
        internal DevComponents.DotNetBar.Controls.CheckBoxX checkTodos;
        internal DevComponents.DotNetBar.Controls.CheckBoxX checkUno;
        internal DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.GroupPanel GPanel_Producto;
        private Janus.Windows.GridEX.GridEX Dgv_Producto;
        internal DevComponents.DotNetBar.Controls.TextBoxX tbCodProducto;
        private DevComponents.DotNetBar.Controls.TextBoxX tbProducto;
        internal DevComponents.DotNetBar.LabelX labelX5;
        internal System.Windows.Forms.Panel Panel3;
        internal DevComponents.DotNetBar.Controls.CheckBoxX checkTodosSaldo;
        internal DevComponents.DotNetBar.Controls.CheckBoxX CheckMayorCero;
    }
}