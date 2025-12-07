namespace ScaryHouse
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowGroup1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowGroup2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowGroup3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowControls = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBroker = new System.Windows.Forms.Label();
            this.textBoxBroker = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.numericTimeout = new System.Windows.Forms.NumericUpDown();
            this.buttonUpdateConfig = new System.Windows.Forms.Button();
            this.buttonPub1 = new System.Windows.Forms.Button();
            this.buttonPub2 = new System.Windows.Forms.Button();
            this.buttonPub3 = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.flowControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.flowGroup1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowGroup2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowGroup3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowControls, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1161, 550);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowGroup1
            // 
            this.flowGroup1.AutoScroll = true;
            this.flowGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGroup1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowGroup1.Location = new System.Drawing.Point(3, 3);
            this.flowGroup1.Name = "flowGroup1";
            this.flowGroup1.Size = new System.Drawing.Size(284, 544);
            this.flowGroup1.TabIndex = 0;
            this.flowGroup1.WrapContents = false;
            // 
            // flowGroup2
            // 
            this.flowGroup2.AutoScroll = true;
            this.flowGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGroup2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowGroup2.Location = new System.Drawing.Point(293, 3);
            this.flowGroup2.Name = "flowGroup2";
            this.flowGroup2.Size = new System.Drawing.Size(284, 544);
            this.flowGroup2.TabIndex = 1;
            this.flowGroup2.WrapContents = false;
            // 
            // flowGroup3
            // 
            this.flowGroup3.AutoScroll = true;
            this.flowGroup3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGroup3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowGroup3.Location = new System.Drawing.Point(583, 3);
            this.flowGroup3.Name = "flowGroup3";
            this.flowGroup3.Size = new System.Drawing.Size(284, 544);
            this.flowGroup3.TabIndex = 2;
            this.flowGroup3.WrapContents = false;
            // 
            // flowControls
            // 
            this.flowControls.AutoScroll = true;
            this.flowControls.Controls.Add(this.labelBroker);
            this.flowControls.Controls.Add(this.textBoxBroker);
            this.flowControls.Controls.Add(this.buttonConnect);
            this.flowControls.Controls.Add(this.labelTimeout);
            this.flowControls.Controls.Add(this.numericTimeout);
            this.flowControls.Controls.Add(this.buttonUpdateConfig);
            this.flowControls.Controls.Add(this.buttonPub1);
            this.flowControls.Controls.Add(this.buttonPub2);
            this.flowControls.Controls.Add(this.buttonPub3);
            this.flowControls.Controls.Add(this.buttonReset);
            this.flowControls.Controls.Add(this.label1);
            this.flowControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowControls.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowControls.Location = new System.Drawing.Point(873, 3);
            this.flowControls.Name = "flowControls";
            this.flowControls.Padding = new System.Windows.Forms.Padding(10);
            this.flowControls.Size = new System.Drawing.Size(285, 544);
            this.flowControls.TabIndex = 3;
            this.flowControls.WrapContents = false;
            // 
            // labelBroker
            // 
            this.labelBroker.AutoSize = true;
            this.labelBroker.Location = new System.Drawing.Point(13, 13);
            this.labelBroker.Margin = new System.Windows.Forms.Padding(3);
            this.labelBroker.Name = "labelBroker";
            this.labelBroker.Size = new System.Drawing.Size(72, 13);
            this.labelBroker.TabIndex = 0;
            this.labelBroker.Text = "MQTT Broker";
            // 
            // textBoxBroker
            // 
            this.textBoxBroker.Location = new System.Drawing.Point(13, 32);
            this.textBoxBroker.Name = "textBoxBroker";
            this.textBoxBroker.Size = new System.Drawing.Size(160, 20);
            this.textBoxBroker.TabIndex = 1;
            this.textBoxBroker.Text = "192.168.1.6";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(13, 58);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(160, 23);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect MQTT";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelTimeout
            // 
            this.labelTimeout.AutoSize = true;
            this.labelTimeout.Location = new System.Drawing.Point(13, 90);
            this.labelTimeout.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.Size = new System.Drawing.Size(88, 13);
            this.labelTimeout.TabIndex = 7;
            this.labelTimeout.Text = "Offline timeout (s)";
            // 
            // numericTimeout
            // 
            this.numericTimeout.Location = new System.Drawing.Point(13, 109);
            this.numericTimeout.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericTimeout.Name = "numericTimeout";
            this.numericTimeout.Size = new System.Drawing.Size(160, 20);
            this.numericTimeout.TabIndex = 8;
            this.numericTimeout.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericTimeout.ValueChanged += new System.EventHandler(this.numericTimeout_ValueChanged);
            // 
            // buttonUpdateConfig
            // 
            this.buttonUpdateConfig.Location = new System.Drawing.Point(13, 138);
            this.buttonUpdateConfig.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.buttonUpdateConfig.Name = "buttonUpdateConfig";
            this.buttonUpdateConfig.Size = new System.Drawing.Size(160, 23);
            this.buttonUpdateConfig.TabIndex = 3;
            this.buttonUpdateConfig.Text = "Update Configuration";
            this.buttonUpdateConfig.UseVisualStyleBackColor = true;
            this.buttonUpdateConfig.Click += new System.EventHandler(this.buttonUpdateConfig_Click);
            // 
            // buttonPub1
            // 
            this.buttonPub1.Location = new System.Drawing.Point(13, 170);
            this.buttonPub1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.buttonPub1.Name = "buttonPub1";
            this.buttonPub1.Size = new System.Drawing.Size(160, 23);
            this.buttonPub1.TabIndex = 4;
            this.buttonPub1.Text = "Publish: music/group1";
            this.buttonPub1.UseVisualStyleBackColor = true;
            this.buttonPub1.Click += new System.EventHandler(this.buttonPub1_Click);
            // 
            // buttonPub2
            // 
            this.buttonPub2.Location = new System.Drawing.Point(13, 202);
            this.buttonPub2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.buttonPub2.Name = "buttonPub2";
            this.buttonPub2.Size = new System.Drawing.Size(160, 23);
            this.buttonPub2.TabIndex = 5;
            this.buttonPub2.Text = "Publish: music/group2";
            this.buttonPub2.UseVisualStyleBackColor = true;
            this.buttonPub2.Click += new System.EventHandler(this.buttonPub2_Click);
            // 
            // buttonPub3
            // 
            this.buttonPub3.Location = new System.Drawing.Point(13, 234);
            this.buttonPub3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.buttonPub3.Name = "buttonPub3";
            this.buttonPub3.Size = new System.Drawing.Size(160, 23);
            this.buttonPub3.TabIndex = 6;
            this.buttonPub3.Text = "Publish: music/group3";
            this.buttonPub3.UseVisualStyleBackColor = true;
            this.buttonPub3.Click += new System.EventHandler(this.buttonPub3_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(13, 266);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(160, 23);
            this.buttonReset.TabIndex = 9;
            this.buttonReset.Text = "Reset All";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 550);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "ScaryHouse - Node Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowControls.ResumeLayout(false);
            this.flowControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowGroup1;
        private System.Windows.Forms.FlowLayoutPanel flowGroup2;
        private System.Windows.Forms.FlowLayoutPanel flowGroup3;
        private System.Windows.Forms.FlowLayoutPanel flowControls;
        private System.Windows.Forms.Label labelBroker;
        private System.Windows.Forms.TextBox textBoxBroker;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.NumericUpDown numericTimeout;
        private System.Windows.Forms.Button buttonUpdateConfig;
        private System.Windows.Forms.Button buttonPub1;
        private System.Windows.Forms.Button buttonPub2;
        private System.Windows.Forms.Button buttonPub3;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}

