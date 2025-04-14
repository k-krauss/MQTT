namespace MQTT
{
    partial class FrmSafe
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
            this.btnPublish = new Button();
            this.btnUnsubscribe = new Button();
            this.btnSubscribe = new Button();
            this.btnClearLog = new Button();
            this.btnDisconnect = new Button();
            this.btnConnect = new Button();
            this.txtLog = new RichTextBox();
            SuspendLayout();
            // 
            // btnPublish
            // 
            this.btnPublish.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnPublish.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnPublish.Location = new Point(447, 513);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new Size(139, 44);
            this.btnPublish.TabIndex = 13;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += btnPublish_Click;
            // 
            // btnUnsubscribe
            // 
            this.btnUnsubscribe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnUnsubscribe.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnUnsubscribe.Location = new Point(302, 513);
            this.btnUnsubscribe.Name = "btnUnsubscribe";
            this.btnUnsubscribe.Size = new Size(139, 44);
            this.btnUnsubscribe.TabIndex = 12;
            this.btnUnsubscribe.Text = "Unsubscribe";
            this.btnUnsubscribe.UseVisualStyleBackColor = true;
            this.btnUnsubscribe.Click += btnUnsubscribe_Click;
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnSubscribe.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnSubscribe.Location = new Point(157, 513);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new Size(139, 44);
            this.btnSubscribe.TabIndex = 11;
            this.btnSubscribe.Text = "Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += btnSubscribe_Click;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnClearLog.Location = new Point(12, 420);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new Size(139, 44);
            this.btnClearLog.TabIndex = 10;
            this.btnClearLog.Text = "Log leeren";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += btnClearLog_Click;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnDisconnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnDisconnect.Location = new Point(1044, 513);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new Size(139, 44);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Verbindung trennen";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.btnConnect.Location = new Point(12, 513);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new Size(139, 44);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Verbinden";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += btnConnect_Click;
            // 
            // txtLog
            // 
            this.txtLog.Dock = DockStyle.Top;
            this.txtLog.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            this.txtLog.Location = new Point(0, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new Size(1195, 408);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "";
            // 
            // FrmSafe
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1195, 563);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.btnUnsubscribe);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtLog);
            this.Name = "FrmSafe";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MQTT - mit Zertifikat";
            this.WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private Button btnPublish;
        private Button btnUnsubscribe;
        private Button btnSubscribe;
        private Button btnClearLog;
        private Button btnDisconnect;
        private Button btnConnect;
        private RichTextBox txtLog;
    }
}