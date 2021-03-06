﻿namespace PRESENTER.com.Reporte
{
    partial class F2_ExportarExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2_ExportarExcel));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.Dt_FechaHasta = new MetroFramework.Controls.MetroDateTime();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.Dt_FechaDesde = new MetroFramework.Controls.MetroDateTime();
            this.Cb_Estado = new MetroFramework.Controls.MetroComboBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.Dgv_GBuscador = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.PanelMenu.SuspendLayout();
            this.PanelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleBarUsuario)).BeginInit();
            this.GPanel_Criterio.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_GBuscador)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMin
            // 
            this.btnMin.Location = new System.Drawing.Point(1103, 0);
            // 
            // btnMax
            // 
            this.btnMax.Location = new System.Drawing.Point(1123, 0);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1143, 0);
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelMenu.BackgroundImage")));
            this.PanelMenu.Size = new System.Drawing.Size(1163, 72);
            // 
            // LblSubtitulo
            // 
            this.LblSubtitulo.Size = new System.Drawing.Size(1163, 36);
            this.LblSubtitulo.Text = "Para generar el reporte, especifique valores para el filtro de búsqueda y luego s" +
    "eleccione la opción Generar: ";
            // 
            // PanelInferior
            // 
            this.PanelInferior.Location = new System.Drawing.Point(0, 258);
            this.PanelInferior.Size = new System.Drawing.Size(1163, 28);
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
            this.BubbleBarUsuario.Location = new System.Drawing.Point(971, 0);
            this.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight;
            this.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black;
            // 
            // TxtNombreUsu
            // 
            this.TxtNombreUsu.Location = new System.Drawing.Point(1021, 0);
            this.TxtNombreUsu.ReadOnly = true;
            // 
            // BtnExportar
            // 
            this.BtnExportar.Click += new System.EventHandler(this.BtnExportar_Click);
            // 
            // GPanel_Criterio
            // 
            this.GPanel_Criterio.Controls.Add(this.Dgv_GBuscador);
            this.GPanel_Criterio.Controls.Add(this.Cb_Estado);
            this.GPanel_Criterio.Controls.Add(this.labelX4);
            this.GPanel_Criterio.Controls.Add(this.labelX1);
            this.GPanel_Criterio.Controls.Add(this.labelX3);
            this.GPanel_Criterio.Controls.Add(this.Dt_FechaHasta);
            this.GPanel_Criterio.Controls.Add(this.labelX2);
            this.GPanel_Criterio.Controls.Add(this.Dt_FechaDesde);
            this.GPanel_Criterio.Controls.Add(this.groupPanel1);
            this.GPanel_Criterio.Dock = System.Windows.Forms.DockStyle.Top;
            this.GPanel_Criterio.Size = new System.Drawing.Size(1163, 100);
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
            this.GPanel_Criterio.Click += new System.EventHandler(this.GPanel_Criterio_Click);
            this.GPanel_Criterio.Controls.SetChildIndex(this.groupPanel1, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.Dt_FechaDesde, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.labelX2, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.Dt_FechaHasta, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.labelX3, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.labelX1, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.labelX4, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.Cb_Estado, 0);
            this.GPanel_Criterio.Controls.SetChildIndex(this.Dgv_GBuscador, 0);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.None;
            this.groupPanel1.Location = new System.Drawing.Point(382, 223);
            this.groupPanel1.Size = new System.Drawing.Size(575, 100);
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
            this.panel1.Size = new System.Drawing.Size(569, 72);
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
            this.labelX1.Location = new System.Drawing.Point(19, 19);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX1.Size = new System.Drawing.Size(46, 23);
            this.labelX1.TabIndex = 378;
            this.labelX1.Text = "Fecha:";
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
            this.labelX3.Location = new System.Drawing.Point(290, 19);
            this.labelX3.Name = "labelX3";
            this.labelX3.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX3.Size = new System.Drawing.Size(40, 23);
            this.labelX3.TabIndex = 377;
            this.labelX3.Text = "Hasta";
            // 
            // Dt_FechaHasta
            // 
            this.Dt_FechaHasta.Checked = false;
            this.Dt_FechaHasta.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.Dt_FechaHasta.FontWeight = MetroFramework.MetroDateTimeWeight.Bold;
            this.Dt_FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt_FechaHasta.Location = new System.Drawing.Point(350, 19);
            this.Dt_FechaHasta.MinimumSize = new System.Drawing.Size(0, 25);
            this.Dt_FechaHasta.Name = "Dt_FechaHasta";
            this.Dt_FechaHasta.ShowCheckBox = true;
            this.Dt_FechaHasta.Size = new System.Drawing.Size(138, 27);
            this.Dt_FechaHasta.Style = MetroFramework.MetroColorStyle.Silver;
            this.Dt_FechaHasta.TabIndex = 376;
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
            this.labelX2.Location = new System.Drawing.Point(76, 19);
            this.labelX2.Name = "labelX2";
            this.labelX2.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX2.Size = new System.Drawing.Size(46, 23);
            this.labelX2.TabIndex = 375;
            this.labelX2.Text = "Desde";
            // 
            // Dt_FechaDesde
            // 
            this.Dt_FechaDesde.CalendarFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.Dt_FechaDesde.Checked = false;
            this.Dt_FechaDesde.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.Dt_FechaDesde.FontWeight = MetroFramework.MetroDateTimeWeight.Bold;
            this.Dt_FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dt_FechaDesde.Location = new System.Drawing.Point(127, 17);
            this.Dt_FechaDesde.MinimumSize = new System.Drawing.Size(0, 25);
            this.Dt_FechaDesde.Name = "Dt_FechaDesde";
            this.Dt_FechaDesde.ShowCheckBox = true;
            this.Dt_FechaDesde.Size = new System.Drawing.Size(138, 27);
            this.Dt_FechaDesde.Style = MetroFramework.MetroColorStyle.Silver;
            this.Dt_FechaDesde.TabIndex = 374;
            // 
            // Cb_Estado
            // 
            this.Cb_Estado.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.Cb_Estado.FormattingEnabled = true;
            this.Cb_Estado.ItemHeight = 19;
            this.Cb_Estado.Items.AddRange(new object[] {
            "Historico de selección"});
            this.Cb_Estado.Location = new System.Drawing.Point(653, 19);
            this.Cb_Estado.Name = "Cb_Estado";
            this.Cb_Estado.Size = new System.Drawing.Size(168, 25);
            this.Cb_Estado.Style = MetroFramework.MetroColorStyle.Silver;
            this.Cb_Estado.TabIndex = 380;
            this.Cb_Estado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Cb_Estado.UseSelectable = true;
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
            this.labelX4.Location = new System.Drawing.Point(610, 19);
            this.labelX4.Name = "labelX4";
            this.labelX4.SingleLineColor = System.Drawing.SystemColors.Control;
            this.labelX4.Size = new System.Drawing.Size(46, 23);
            this.labelX4.TabIndex = 379;
            this.labelX4.Text = "Tipo:";
            // 
            // Dgv_GBuscador
            // 
            this.Dgv_GBuscador.Location = new System.Drawing.Point(988, 6);
            this.Dgv_GBuscador.Name = "Dgv_GBuscador";
            this.Dgv_GBuscador.Size = new System.Drawing.Size(115, 40);
            this.Dgv_GBuscador.TabIndex = 381;
            this.Dgv_GBuscador.Visible = false;
            // 
            // F2_ExportarExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 286);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "F2_ExportarExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXPORTAR A EXCEL";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            this.PanelInferior.ResumeLayout(false);
            this.PanelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleBarUsuario)).EndInit();
            this.GPanel_Criterio.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_GBuscador)).EndInit();
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
        private Janus.Windows.GridEX.GridEX Dgv_GBuscador;
    }
}