namespace chatControls
{
    partial class chatPost
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Wymagana metoda Wsparcia projektanta - nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.PostBox = new System.Windows.Forms.GroupBox();
            this.Message = new System.Windows.Forms.Label();
            this.PostBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PostBox
            // 
            this.PostBox.Controls.Add(this.Message);
            this.PostBox.Location = new System.Drawing.Point(6, 6);
            this.PostBox.Name = "PostBox";
            this.PostBox.Size = new System.Drawing.Size(287, 44);
            this.PostBox.TabIndex = 0;
            this.PostBox.TabStop = false;
            this.PostBox.Text = "groupBox1";
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Location = new System.Drawing.Point(69, 16);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(35, 13);
            this.Message.TabIndex = 0;
            this.Message.Text = "label1";
            // 
            // chatPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PostBox);
            this.Name = "chatPost";
            this.Size = new System.Drawing.Size(311, 80);
            this.PostBox.ResumeLayout(false);
            this.PostBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PostBox;
        private System.Windows.Forms.Label Message;
    }
}
