namespace HMM.AppGUI
{
    partial class frmDBA
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.btnInitiate = new System.Windows.Forms.Button();
            this.txtSQL = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnIUD = new System.Windows.Forms.Button();
            this.grdDetails = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInitiate
            // 
            this.btnInitiate.Location = new System.Drawing.Point(13, 23);
            this.btnInitiate.Name = "btnInitiate";
            this.btnInitiate.Size = new System.Drawing.Size(75, 23);
            this.btnInitiate.TabIndex = 0;
            this.btnInitiate.Text = "Initialize";
            this.btnInitiate.UseVisualStyleBackColor = true;
            this.btnInitiate.Click += new System.EventHandler(this.btnInitiate_Click);
            // 
            // txtSQL
            // 
            this.txtSQL.Location = new System.Drawing.Point(153, 23);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(264, 83);
            this.txtSQL.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(469, 32);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnIUD
            // 
            this.btnIUD.Location = new System.Drawing.Point(565, 32);
            this.btnIUD.Name = "btnIUD";
            this.btnIUD.Size = new System.Drawing.Size(75, 23);
            this.btnIUD.TabIndex = 3;
            this.btnIUD.Text = "IUD";
            this.btnIUD.UseVisualStyleBackColor = true;
            this.btnIUD.Click += new System.EventHandler(this.btnIUD_Click);
            // 
            // grdDetails
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdDetails.DisplayLayout.Appearance = appearance1;
            this.grdDetails.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdDetails.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.grdDetails.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdDetails.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.grdDetails.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdDetails.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdDetails.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.grdDetails.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDetails.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdDetails.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdDetails.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.grdDetails.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdDetails.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.grdDetails.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdDetails.DisplayLayout.Override.CellAppearance = appearance8;
            this.grdDetails.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdDetails.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.grdDetails.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.grdDetails.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.grdDetails.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdDetails.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.grdDetails.DisplayLayout.Override.RowAppearance = appearance11;
            this.grdDetails.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdDetails.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.grdDetails.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.grdDetails.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDetails.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDetails.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grdDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdDetails.Location = new System.Drawing.Point(0, 123);
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.Size = new System.Drawing.Size(694, 198);
            this.grdDetails.TabIndex = 8;
            this.grdDetails.Text = "ultraGrid1";
            // 
            // frmDBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 321);
            this.Controls.Add(this.grdDetails);
            this.Controls.Add(this.btnIUD);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtSQL);
            this.Controls.Add(this.btnInitiate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDBA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDBA";
            ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInitiate;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSQL;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnIUD;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDetails;
    }
}