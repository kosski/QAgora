namespace chatClient
{
    partial class Form1
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

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda wsparcia projektanta - nie należy modyfikować
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatBox = new System.Windows.Forms.ListBox();
            this.nicktext = new System.Windows.Forms.TextBox();
            this.Message = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Sender = new System.Windows.Forms.Button();
            this.ConnectButt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatBox
            // 
            this.chatBox.Enabled = false;
            this.chatBox.FormattingEnabled = true;
            this.chatBox.Location = new System.Drawing.Point(19, 38);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(302, 173);
            this.chatBox.TabIndex = 0;
            // 
            // nicktext
            // 
            this.nicktext.Location = new System.Drawing.Point(87, 12);
            this.nicktext.Name = "nicktext";
            this.nicktext.Size = new System.Drawing.Size(156, 20);
            this.nicktext.TabIndex = 1;
            // 
            // Message
            // 
            this.Message.Enabled = false;
            this.Message.Location = new System.Drawing.Point(19, 227);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(229, 20);
            this.Message.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // Sender
            // 
            this.Sender.Enabled = false;
            this.Sender.Location = new System.Drawing.Point(254, 215);
            this.Sender.Name = "Sender";
            this.Sender.Size = new System.Drawing.Size(66, 43);
            this.Sender.TabIndex = 4;
            this.Sender.Text = "Send";
            this.Sender.UseVisualStyleBackColor = true;
            this.Sender.Click += new System.EventHandler(this.Sender_Click);
            // 
            // ConnectButt
            // 
            this.ConnectButt.Location = new System.Drawing.Point(248, 12);
            this.ConnectButt.Name = "ConnectButt";
            this.ConnectButt.Size = new System.Drawing.Size(69, 20);
            this.ConnectButt.TabIndex = 5;
            this.ConnectButt.Text = "Connect";
            this.ConnectButt.UseVisualStyleBackColor = true;
            this.ConnectButt.Click += new System.EventHandler(this.ConnectButt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 264);
            this.Controls.Add(this.ConnectButt);
            this.Controls.Add(this.Sender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.nicktext);
            this.Controls.Add(this.chatBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.TextBox nicktext;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Sender;
        private System.Windows.Forms.Button ConnectButt;
    }
}

