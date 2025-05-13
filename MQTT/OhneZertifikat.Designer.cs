namespace MQTT
{
    partial class OhneZertifikat
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLogPublish = new RichTextBox();
            this.btnPublish = new Button();
            this.btnUnsubscribe = new Button();
            this.btnSubscribe = new Button();
            this.btnDisconnect = new Button();
            this.btnConnect = new Button();
            this.txtLogSubscribe = new RichTextBox();
            SuspendLayout();
            // 
            // txtLogPublish
            // 
            this.txtLogPublish.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtLogPublish.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.txtLogPublish.Location = new Point(626, 3);
            this.txtLogPublish.Name = "txtLogPublish";
            this.txtLogPublish.ReadOnly = true;
            this.txtLogPublish.Size = new Size(568, 408);
            this.txtLogPublish.TabIndex = 21;
            this.txtLogPublish.Text = "";
            // 
            // btnPublish
            // 
            this.btnPublish.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnPublish.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnPublish.Location = new Point(910, 417);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new Size(139, 44);
            this.btnPublish.TabIndex = 20;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += btnPublish_Click;
            // 
            // btnUnsubscribe
            // 
            this.btnUnsubscribe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnUnsubscribe.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnUnsubscribe.Location = new Point(208, 417);
            this.btnUnsubscribe.Name = "btnUnsubscribe";
            this.btnUnsubscribe.Size = new Size(139, 44);
            this.btnUnsubscribe.TabIndex = 19;
            this.btnUnsubscribe.Text = "Unsubscribe";
            this.btnUnsubscribe.UseVisualStyleBackColor = true;
            this.btnUnsubscribe.Click += btnUnsubscribe_Click;
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnSubscribe.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnSubscribe.Location = new Point(63, 417);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new Size(139, 44);
            this.btnSubscribe.TabIndex = 18;
            this.btnSubscribe.Text = "Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += btnSubscribe_Click;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnDisconnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnDisconnect.Location = new Point(1045, 516);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new Size(139, 44);
            this.btnDisconnect.TabIndex = 17;
            this.btnDisconnect.Text = "Verbindung trennen";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnConnect.Location = new Point(13, 516);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new Size(139, 44);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "Verbinden";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += btnConnect_Click;
            // 
            // txtLogSubscribe
            // 
            this.txtLogSubscribe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtLogSubscribe.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.txtLogSubscribe.Location = new Point(1, 3);
            this.txtLogSubscribe.Name = "txtLogSubscribe";
            this.txtLogSubscribe.ReadOnly = true;
            this.txtLogSubscribe.Size = new Size(567, 408);
            this.txtLogSubscribe.TabIndex = 15;
            this.txtLogSubscribe.Text = "";
            // 
            // OhneZertifikat
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1195, 563);
            this.Controls.Add(this.txtLogPublish);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.btnUnsubscribe);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtLogSubscribe);
            this.Name = "OhneZertifikat";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MQTT - ohne Zertifikat";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox txtLogPublish;
        private Button btnPublish;
        private Button btnUnsubscribe;
        private Button btnSubscribe;
        private Button btnDisconnect;
        private Button btnConnect;
        private RichTextBox txtLogSubscribe;
    }
}
