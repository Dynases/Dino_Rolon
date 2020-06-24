namespace PRESENTER.com.Reporte
{
    partial class F2_CompraIngreso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2_CompraIngreso));
            Janus.Windows.GridEX.GridEXLayout cb_NumGranja_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout cb_Proveedor_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout Cb_Tipo_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.Dt_FechaHasta = new MetroFramework.Controls.MetroDateTime();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.Dt_FechaDesde = new MetroFramework.Controls.MetroDateTime();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.Cb_Estado = new MetroFramework.Controls.MetroComboBox();
            this.Rpt_Reporte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cb_NumGranja = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.cb_Proveedor = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.Cb_Tipo = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
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
            ((System.ComponentModel.ISupportInitialize)(this.cb_NumGranja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Proveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cb_Tipo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMin
            // 
            this.btnMin.Location = new System.Drawing.Point(806, 0);
            // 
            // btnMax
            // 
            this.btnMax.Location = new System.Drawing.Point(826, 0);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(846, 0);
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelMenu.BackgroundImage")));
            this.PanelMenu.Size = new System.Drawing.Size(866, 72);
            // 
            // LblSubtitulo
            // 
            this.LblSubtitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSubtitulo.Size = new System.Drawing.Size(866, 36);
            this.LblSubtitulo.Text = "Para generar el reporte, especifique valores para el filtro de búsqueda y luego s" +
    "eleccione la opción Generar: ";
            // 
            // PanelInferior
            // 
            this.PanelInferior.Location = new System.Drawing.Point(0, 541);
            this.PanelInferior.Size = new System.Drawing.Size(866, 28);
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
            this.BubbleBarUsuario.Location = new System.Drawing.Point(674, 0);
            this.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight;
            this.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black;
            // 
            // TxtNombreUsu
            // 
            this.TxtNombreUsu.Location = new System.Drawing.Point(724, 0);
            this.TxtNombreUsu.ReadOnly = true;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // GPanel_Criterio
            // 
            this.GPanel_Criterio.Controls.Add(this.labelX7);
            this.GPanel_Criterio.Controls.Add(this.Cb_Tipo);
            this.GPanel_Criterio.Controls.Add(this.labelX6);
            this.GPanel_Criterio.Controls.Add(this.cb_Proveedor);
            this.GPanel_Criterio.Controls.Add(this.cb_NumGranja);
            this.GPanel_Criterio.Controls.Add(this.labelX5);
            this.GPanel_Criterio.Controls.Add(this.Cb_Estado);
            this.GPanel_Criterio.Controls.Add(this.labelX4);
            this.GPanel_Criterio.Controls.Add(this.labelX1);
            this.GPanel_Criterio.Controls.Add(this.labelX3);
            this.GPanel_Criterio.Controls.Add(this.Dt_FechaHasta);
            this.GPanel_Criterio.Controls.Add(this.labelX2);
            this.GPanel_Criterio.Controls.Add(this.Dt_FechaDesde);
            this.GPanel_Criterio.Size = new System.Drawing.Size(265, 406);
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
            this.groupPanel1.Location = new System.Drawing.Point(265, 135);
            this.groupPanel1.Size = new System.Drawing.Size(601, 406);
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
            this.panel1.Size = new System.Drawing.Size(595, 378);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.Color.Black;
            this.labelX3.Location = new System.Drawing.Point(62, 46);
            this.labelX3.Name = "labelX3";
            this.labelX3.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX3.Size = new System.Drawing.Size(40, 23);
            this.labelX3.TabIndex = 372;
            this.labelX3.Text = "Hasta";
            // 
            // Dt_FechaHasta
            // 
            this.Dt_FechaHasta.Checked = false;
            this.Dt_FechaHasta.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.Dt_FechaHasta.FontWeight = MetroFramework.MetroDateTimeWeight.Bold;
            this.Dt_FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt_FechaHasta.Location = new System.Drawing.Point(112, 46);
            this.Dt_FechaHasta.MinimumSize = new System.Drawing.Size(0, 25);
            this.Dt_FechaHasta.Name = "Dt_FechaHasta";
            this.Dt_FechaHasta.ShowCheckBox = true;
            this.Dt_FechaHasta.Size = new System.Drawing.Size(138, 27);
            this.Dt_FechaHasta.Style = MetroFramework.MetroColorStyle.Silver;
            this.Dt_FechaHasta.TabIndex = 371;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(61, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX2.Size = new System.Drawing.Size(46, 23);
            this.labelX2.TabIndex = 370;
            this.labelX2.Text = "Desde";
            // 
            // Dt_FechaDesde
            // 
            this.Dt_FechaDesde.CalendarFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.Dt_FechaDesde.Checked = false;
            this.Dt_FechaDesde.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.Dt_FechaDesde.FontWeight = MetroFramework.MetroDateTimeWeight.Bold;
            this.Dt_FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt_FechaDesde.Location = new System.Drawing.Point(112, 12);
            this.Dt_FechaDesde.MinimumSize = new System.Drawing.Size(0, 25);
            this.Dt_FechaDesde.Name = "Dt_FechaDesde";
            this.Dt_FechaDesde.ShowCheckBox = true;
            this.Dt_FechaDesde.Size = new System.Drawing.Size(138, 27);
            this.Dt_FechaDesde.Style = MetroFramework.MetroColorStyle.Silver;
            this.Dt_FechaDesde.TabIndex = 369;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(9, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX1.Size = new System.Drawing.Size(46, 23);
            this.labelX1.TabIndex = 373;
            this.labelX1.Text = "Fecha:";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.Black;
            this.labelX4.Location = new System.Drawing.Point(9, 79);
            this.labelX4.Name = "labelX4";
            this.labelX4.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX4.Size = new System.Drawing.Size(46, 23);
            this.labelX4.TabIndex = 374;
            this.labelX4.Text = "Estado:";
            // 
            // Cb_Estado
            // 
            this.Cb_Estado.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.Cb_Estado.FormattingEnabled = true;
            this.Cb_Estado.ItemHeight = 19;
            this.Cb_Estado.Items.AddRange(new object[] {
            "S/Selección",
            "C/Selección",
            "TODOS"});
            this.Cb_Estado.Location = new System.Drawing.Point(112, 80);
            this.Cb_Estado.Name = "Cb_Estado";
            this.Cb_Estado.Size = new System.Drawing.Size(138, 25);
            this.Cb_Estado.Style = MetroFramework.MetroColorStyle.Silver;
            this.Cb_Estado.TabIndex = 375;
            this.Cb_Estado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Cb_Estado.UseSelectable = true;
            // 
            // Rpt_Reporte
            // 
            this.Rpt_Reporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rpt_Reporte.LocalReport.ReportEmbeddedResource = "PRESENTER.Report.ReportViewer.CompraIngreso.rdlc";
            this.Rpt_Reporte.Location = new System.Drawing.Point(0, 0);
            this.Rpt_Reporte.Name = "Rpt_Reporte";
            this.Rpt_Reporte.ServerReport.BearerToken = null;
            this.Rpt_Reporte.Size = new System.Drawing.Size(595, 378);
            this.Rpt_Reporte.TabIndex = 0;
            this.Rpt_Reporte.Visible = false;
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.Color.Black;
            this.labelX5.Location = new System.Drawing.Point(9, 114);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX5.Size = new System.Drawing.Size(80, 23);
            this.labelX5.TabIndex = 377;
            this.labelX5.Text = "Num. Granja:";
            // 
            // cb_NumGranja
            // 
            this.cb_NumGranja.BackColor = System.Drawing.Color.White;
            cb_NumGranja_DesignTimeLayout.LayoutString = resources.GetString("cb_NumGranja_DesignTimeLayout.LayoutString");
            this.cb_NumGranja.DesignTimeLayout = cb_NumGranja_DesignTimeLayout;
            this.cb_NumGranja.DisabledBackColor = System.Drawing.Color.Blue;
            this.cb_NumGranja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_NumGranja.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Far;
            this.cb_NumGranja.Location = new System.Drawing.Point(95, 115);
            this.cb_NumGranja.Name = "cb_NumGranja";
            this.cb_NumGranja.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom;
            this.cb_NumGranja.Office2007CustomColor = System.Drawing.Color.DodgerBlue;
            this.cb_NumGranja.SelectedIndex = -1;
            this.cb_NumGranja.SelectedItem = null;
            this.cb_NumGranja.Size = new System.Drawing.Size(155, 22);
            this.cb_NumGranja.TabIndex = 382;
            this.cb_NumGranja.Tag = "1";
            this.cb_NumGranja.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // cb_Proveedor
            // 
            this.cb_Proveedor.BackColor = System.Drawing.Color.White;
            cb_Proveedor_DesignTimeLayout.LayoutString = resources.GetString("cb_Proveedor_DesignTimeLayout.LayoutString");
            this.cb_Proveedor.DesignTimeLayout = cb_Proveedor_DesignTimeLayout;
            this.cb_Proveedor.DisabledBackColor = System.Drawing.Color.Blue;
            this.cb_Proveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Proveedor.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Far;
            this.cb_Proveedor.Location = new System.Drawing.Point(95, 143);
            this.cb_Proveedor.Name = "cb_Proveedor";
            this.cb_Proveedor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom;
            this.cb_Proveedor.Office2007CustomColor = System.Drawing.Color.DodgerBlue;
            this.cb_Proveedor.SelectedIndex = -1;
            this.cb_Proveedor.SelectedItem = null;
            this.cb_Proveedor.Size = new System.Drawing.Size(155, 22);
            this.cb_Proveedor.TabIndex = 384;
            this.cb_Proveedor.Tag = "1";
            this.cb_Proveedor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.Color.Black;
            this.labelX6.Location = new System.Drawing.Point(9, 145);
            this.labelX6.Name = "labelX6";
            this.labelX6.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX6.Size = new System.Drawing.Size(80, 23);
            this.labelX6.TabIndex = 385;
            this.labelX6.Text = "Proveedor:";
            // 
            // Cb_Tipo
            // 
            this.Cb_Tipo.BackColor = System.Drawing.Color.White;
            Cb_Tipo_DesignTimeLayout.LayoutString = resources.GetString("Cb_Tipo_DesignTimeLayout.LayoutString");
            this.Cb_Tipo.DesignTimeLayout = Cb_Tipo_DesignTimeLayout;
            this.Cb_Tipo.DisabledBackColor = System.Drawing.Color.Blue;
            this.Cb_Tipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cb_Tipo.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Far;
            this.Cb_Tipo.Location = new System.Drawing.Point(95, 171);
            this.Cb_Tipo.Name = "Cb_Tipo";
            this.Cb_Tipo.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom;
            this.Cb_Tipo.Office2007CustomColor = System.Drawing.Color.DodgerBlue;
            this.Cb_Tipo.SelectedIndex = -1;
            this.Cb_Tipo.SelectedItem = null;
            this.Cb_Tipo.Size = new System.Drawing.Size(155, 22);
            this.Cb_Tipo.TabIndex = 386;
            this.Cb_Tipo.Tag = "1";
            this.Cb_Tipo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.ForeColor = System.Drawing.Color.Black;
            this.labelX7.Location = new System.Drawing.Point(9, 170);
            this.labelX7.Name = "labelX7";
            this.labelX7.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX7.Size = new System.Drawing.Size(80, 23);
            this.labelX7.TabIndex = 387;
            this.labelX7.Text = "Tipo:";
            // 
            // F2_CompraIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 569);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "F2_CompraIngreso";
            this.Text = "F2_CompraIngreso";
            this.Load += new System.EventHandler(this.F2_CompraIngreso_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.cb_NumGranja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Proveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cb_Tipo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal DevComponents.DotNetBar.LabelX labelX1;
        protected internal DevComponents.DotNetBar.LabelX labelX3;
        private MetroFramework.Controls.MetroDateTime Dt_FechaHasta;
        protected internal DevComponents.DotNetBar.LabelX labelX2;
        private MetroFramework.Controls.MetroDateTime Dt_FechaDesde;
        private MetroFramework.Controls.MetroComboBox Cb_Estado;
        protected internal DevComponents.DotNetBar.LabelX labelX4;
        private Microsoft.Reporting.WinForms.ReportViewer Rpt_Reporte;
        protected internal DevComponents.DotNetBar.LabelX labelX5;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        internal Janus.Windows.GridEX.EditControls.MultiColumnCombo cb_NumGranja;
        protected internal DevComponents.DotNetBar.LabelX labelX6;
        internal Janus.Windows.GridEX.EditControls.MultiColumnCombo cb_Proveedor;
        protected internal DevComponents.DotNetBar.LabelX labelX7;
        internal Janus.Windows.GridEX.EditControls.MultiColumnCombo Cb_Tipo;
        //private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        //private Microsoft.Reporting.WinForms.ReportViewer Rpt_Reporte;
        //private Microsoft.Reporting.WinForms.ReportViewer Rpt_Report;
    }
}