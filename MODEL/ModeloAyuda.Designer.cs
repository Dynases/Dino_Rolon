namespace MODEL
{
    partial class ModeloAyuda
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
            this.GPPanelP = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grJBuscador = new Janus.Windows.GridEX.GridEX();
            this.GPPanelP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grJBuscador)).BeginInit();
            this.SuspendLayout();
            // 
            // GPPanelP
            // 
            this.GPPanelP.BackColor = System.Drawing.Color.Transparent;
            this.GPPanelP.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.GPPanelP.Controls.Add(this.grJBuscador);
            this.GPPanelP.DisabledBackColor = System.Drawing.Color.Empty;
            this.GPPanelP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GPPanelP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPPanelP.Location = new System.Drawing.Point(0, 0);
            this.GPPanelP.Name = "GPPanelP";
            this.GPPanelP.Size = new System.Drawing.Size(736, 295);
            // 
            // 
            // 
            this.GPPanelP.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.GPPanelP.Style.BackColorGradientAngle = 90;
            this.GPPanelP.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.GPPanelP.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPPanelP.Style.BorderBottomWidth = 1;
            this.GPPanelP.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground;
            this.GPPanelP.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPPanelP.Style.BorderLeftWidth = 1;
            this.GPPanelP.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPPanelP.Style.BorderRightWidth = 1;
            this.GPPanelP.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPPanelP.Style.BorderTopWidth = 1;
            this.GPPanelP.Style.CornerDiameter = 4;
            this.GPPanelP.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.GPPanelP.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.GPPanelP.Style.TextColor = System.Drawing.Color.DodgerBlue;
            this.GPPanelP.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.GPPanelP.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.GPPanelP.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.GPPanelP.TabIndex = 2;
            this.GPPanelP.Text = "GroupPanel1";
            // 
            // grJBuscador
            // 
            this.grJBuscador.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grJBuscador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grJBuscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grJBuscador.HeaderFormatStyle.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grJBuscador.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(59)))), ((int)(((byte)(66)))));
            this.grJBuscador.Location = new System.Drawing.Point(0, 0);
            this.grJBuscador.Name = "grJBuscador";
            this.grJBuscador.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom;
            this.grJBuscador.Office2007CustomColor = System.Drawing.Color.DodgerBlue;
            this.grJBuscador.Size = new System.Drawing.Size(730, 273);
            this.grJBuscador.TabIndex = 0;
            this.grJBuscador.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.grJBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grJBuscador_KeyDown);
            // 
            // ModeloAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 295);
            this.Controls.Add(this.GPPanelP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModeloAyuda";
            this.Text = "ModeloAyuda";
           // this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModeloAyuda_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ModeloAyuda_KeyPress);
            this.GPPanelP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grJBuscador)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevComponents.DotNetBar.Controls.GroupPanel GPPanelP;
        internal Janus.Windows.GridEX.GridEX grJBuscador;
    }
}