namespace OAuth1
    {
    partial class Form1
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
            this.btnTwitter = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTweet = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTwitter = new System.Windows.Forms.TabPage();
            this.tabQuickbooks = new System.Windows.Forms.TabPage();
            this.btnQbOauth = new System.Windows.Forms.Button();
            this.tabXero = new System.Windows.Forms.TabPage();
            this.btnXeroOAuth = new System.Windows.Forms.Button();
            this.tabMagento = new System.Windows.Forms.TabPage();
            this.btnMagentoOauth = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabTwitter.SuspendLayout();
            this.tabQuickbooks.SuspendLayout();
            this.tabXero.SuspendLayout();
            this.tabMagento.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTwitter
            // 
            this.btnTwitter.Location = new System.Drawing.Point(25, 34);
            this.btnTwitter.Name = "btnTwitter";
            this.btnTwitter.Size = new System.Drawing.Size(156, 30);
            this.btnTwitter.TabIndex = 0;
            this.btnTwitter.Text = "Twitter OAuth1.0a";
            this.btnTwitter.UseVisualStyleBackColor = true;
            this.btnTwitter.Click += new System.EventHandler(this.btnTwitter_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 225);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(798, 361);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Step1: Do 3-Legged OAuth1 to get an Access Token";
            // 
            // btnTweet
            // 
            this.btnTweet.Location = new System.Drawing.Point(25, 98);
            this.btnTweet.Name = "btnTweet";
            this.btnTweet.Size = new System.Drawing.Size(154, 28);
            this.btnTweet.TabIndex = 3;
            this.btnTweet.Text = "Tweet";
            this.btnTweet.UseVisualStyleBackColor = true;
            this.btnTweet.Click += new System.EventHandler(this.btnTweet_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Step 2: Send a Tweet using the Access Token";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(530, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Note: Once we have an access token, we can save it and re-use it, even after we c" +
    "lose this app and restart it. ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTwitter);
            this.tabControl1.Controls.Add(this.tabQuickbooks);
            this.tabControl1.Controls.Add(this.tabXero);
            this.tabControl1.Controls.Add(this.tabMagento);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 225);
            this.tabControl1.TabIndex = 6;
            // 
            // tabTwitter
            // 
            this.tabTwitter.Controls.Add(this.btnTwitter);
            this.tabTwitter.Controls.Add(this.label3);
            this.tabTwitter.Controls.Add(this.label1);
            this.tabTwitter.Controls.Add(this.label2);
            this.tabTwitter.Controls.Add(this.btnTweet);
            this.tabTwitter.Location = new System.Drawing.Point(4, 22);
            this.tabTwitter.Name = "tabTwitter";
            this.tabTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tabTwitter.Size = new System.Drawing.Size(790, 199);
            this.tabTwitter.TabIndex = 0;
            this.tabTwitter.Text = "Twitter";
            this.tabTwitter.UseVisualStyleBackColor = true;
            // 
            // tabQuickbooks
            // 
            this.tabQuickbooks.Controls.Add(this.btnQbOauth);
            this.tabQuickbooks.Location = new System.Drawing.Point(4, 22);
            this.tabQuickbooks.Name = "tabQuickbooks";
            this.tabQuickbooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuickbooks.Size = new System.Drawing.Size(790, 199);
            this.tabQuickbooks.TabIndex = 1;
            this.tabQuickbooks.Text = "Quickbooks";
            this.tabQuickbooks.UseVisualStyleBackColor = true;
            // 
            // btnQbOauth
            // 
            this.btnQbOauth.Location = new System.Drawing.Point(28, 31);
            this.btnQbOauth.Name = "btnQbOauth";
            this.btnQbOauth.Size = new System.Drawing.Size(152, 30);
            this.btnQbOauth.TabIndex = 0;
            this.btnQbOauth.Text = "Quickbooks OAuth1.0a";
            this.btnQbOauth.UseVisualStyleBackColor = true;
            this.btnQbOauth.Click += new System.EventHandler(this.btnQbOauth_Click);
            // 
            // tabXero
            // 
            this.tabXero.Controls.Add(this.btnXeroOAuth);
            this.tabXero.Location = new System.Drawing.Point(4, 22);
            this.tabXero.Name = "tabXero";
            this.tabXero.Size = new System.Drawing.Size(790, 199);
            this.tabXero.TabIndex = 2;
            this.tabXero.Text = "Xero";
            this.tabXero.UseVisualStyleBackColor = true;
            // 
            // btnXeroOAuth
            // 
            this.btnXeroOAuth.Location = new System.Drawing.Point(27, 27);
            this.btnXeroOAuth.Name = "btnXeroOAuth";
            this.btnXeroOAuth.Size = new System.Drawing.Size(170, 33);
            this.btnXeroOAuth.TabIndex = 0;
            this.btnXeroOAuth.Text = "Xero OAuth1.0a";
            this.btnXeroOAuth.UseVisualStyleBackColor = true;
            this.btnXeroOAuth.Click += new System.EventHandler(this.btnXeroOAuth_Click);
            // 
            // tabMagento
            // 
            this.tabMagento.Controls.Add(this.btnMagentoOauth);
            this.tabMagento.Location = new System.Drawing.Point(4, 22);
            this.tabMagento.Name = "tabMagento";
            this.tabMagento.Size = new System.Drawing.Size(790, 199);
            this.tabMagento.TabIndex = 3;
            this.tabMagento.Text = "Magento";
            this.tabMagento.UseVisualStyleBackColor = true;
            // 
            // btnMagentoOauth
            // 
            this.btnMagentoOauth.Location = new System.Drawing.Point(26, 21);
            this.btnMagentoOauth.Name = "btnMagentoOauth";
            this.btnMagentoOauth.Size = new System.Drawing.Size(191, 28);
            this.btnMagentoOauth.TabIndex = 0;
            this.btnMagentoOauth.Text = "Magento OAuth1.0a";
            this.btnMagentoOauth.UseVisualStyleBackColor = true;
            this.btnMagentoOauth.Click += new System.EventHandler(this.btnMagentoOauth_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 586);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "OAuth1.0a Test App - Requires Chilkat v9.5.0.62 or greater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabTwitter.ResumeLayout(false);
            this.tabTwitter.PerformLayout();
            this.tabQuickbooks.ResumeLayout(false);
            this.tabXero.ResumeLayout(false);
            this.tabMagento.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.Button btnTwitter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTweet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTwitter;
        private System.Windows.Forms.TabPage tabQuickbooks;
        private System.Windows.Forms.Button btnQbOauth;
        private System.Windows.Forms.TabPage tabXero;
        private System.Windows.Forms.Button btnXeroOAuth;
        private System.Windows.Forms.TabPage tabMagento;
        private System.Windows.Forms.Button btnMagentoOauth;
        }
    }

