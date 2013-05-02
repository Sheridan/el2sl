namespace Bng.EL2SL.Configurator
{
    partial class FormJT2SL
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
            this.tlpFacilityPriority = new System.Windows.Forms.TableLayoutPanel();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.bCacncel = new System.Windows.Forms.Button();
            this.lInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tlpFacilityPriority
            // 
            this.tlpFacilityPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpFacilityPriority.AutoSize = true;
            this.tlpFacilityPriority.ColumnCount = 1;
            this.tlpFacilityPriority.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFacilityPriority.Location = new System.Drawing.Point(12, 64);
            this.tlpFacilityPriority.Margin = new System.Windows.Forms.Padding(1);
            this.tlpFacilityPriority.Name = "tlpFacilityPriority";
            this.tlpFacilityPriority.RowCount = 1;
            this.tlpFacilityPriority.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFacilityPriority.Size = new System.Drawing.Size(449, 280);
            this.tlpFacilityPriority.TabIndex = 0;
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(12, 37);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(224, 17);
            this.cbEnabled.TabIndex = 1;
            this.cbEnabled.Text = "Enable logging this journal and Event type";
            this.cbEnabled.UseVisualStyleBackColor = true;
            this.cbEnabled.CheckedChanged += new System.EventHandler(this.cbEnabled_CheckedChanged);
            // 
            // bCacncel
            // 
            this.bCacncel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCacncel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCacncel.Location = new System.Drawing.Point(368, 37);
            this.bCacncel.Name = "bCacncel";
            this.bCacncel.Size = new System.Drawing.Size(93, 23);
            this.bCacncel.TabIndex = 2;
            this.bCacncel.Text = "Cancel";
            this.bCacncel.UseVisualStyleBackColor = true;
            // 
            // lInfo
            // 
            this.lInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfo.Location = new System.Drawing.Point(13, 13);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(448, 21);
            this.lInfo.TabIndex = 3;
            this.lInfo.Text = "J.T";
            this.lInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormJT2SL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(473, 354);
            this.Controls.Add(this.lInfo);
            this.Controls.Add(this.bCacncel);
            this.Controls.Add(this.cbEnabled);
            this.Controls.Add(this.tlpFacilityPriority);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormJT2SL";
            this.Text = "Translate table";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpFacilityPriority;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.Button bCacncel;
        private System.Windows.Forms.Label lInfo;
    }
}