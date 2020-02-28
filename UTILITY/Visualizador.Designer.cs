namespace UTILITY
{
    partial class Visualizador
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
            this.ReporteGeneral = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReporteGeneral
            // 
            this.ReporteGeneral.ActiveViewIndex = -1;
            this.ReporteGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReporteGeneral.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReporteGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReporteGeneral.Location = new System.Drawing.Point(0, 0);
            this.ReporteGeneral.Name = "ReporteGeneral";
            this.ReporteGeneral.Size = new System.Drawing.Size(743, 417);
            this.ReporteGeneral.TabIndex = 0;
            this.ReporteGeneral.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // Visualizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 417);
            this.Controls.Add(this.ReporteGeneral);
            this.Name = "Visualizador";
            this.Text = "Visualizador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer ReporteGeneral;
    }
}