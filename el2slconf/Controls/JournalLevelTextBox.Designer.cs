namespace Bng.EL2SL.Configurator.Controls
{
    partial class JournalLevelTextBox
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.MaskedTextBox();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Dock = System.Windows.Forms.DockStyle.Right;
            this.button.Location = new System.Drawing.Point(52, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(24, 20);
            this.button.TabIndex = 1;
            this.button.Text = "...";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.OnClick);
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.HidePromptOnLeave = true;
            this.textBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Mask = "00:0";
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(33, 20);
            this.textBox.TabIndex = 2;
            this.textBox.Validated += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(36, 3);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 3;
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // JournalLevelTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button);
            this.MaximumSize = new System.Drawing.Size(76, 20);
            this.MinimumSize = new System.Drawing.Size(76, 20);
            this.Name = "JournalLevelTextBox";
            this.Size = new System.Drawing.Size(76, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.MaskedTextBox textBox;
        private System.Windows.Forms.CheckBox checkBox;
    }
}
