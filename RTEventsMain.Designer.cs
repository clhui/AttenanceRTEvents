namespace RTEvents
{
    partial class RTEventsMain
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "123456,qweq",
            "12312",
            "2312312",
            "12312",
            "1231"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "时代大厦所多"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "12312312"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "312312"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "123123", System.Drawing.SystemColors.Info, System.Drawing.Color.Lime, new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "34534")}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTEventsMain));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbRTShow = new System.Windows.Forms.ListBox();
            this.testConnectTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.DisConnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textBoxMQPsw = new System.Windows.Forms.TextBox();
            this.textBoxMQPort = new System.Windows.Forms.TextBox();
            this.textBoxMQUser = new System.Windows.Forms.TextBox();
            this.textBoxMQIP = new System.Windows.Forms.TextBox();
            this.baseURLBox = new System.Windows.Forms.TextBox();
            this.machineListView = new System.Windows.Forms.ListView();
            this.machineNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.machineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.machineIp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.machineState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.failCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerRefreshMachine = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbRTShow);
            this.groupBox3.Location = new System.Drawing.Point(8, 149);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 355);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "消息日志";
            // 
            // lbRTShow
            // 
            this.lbRTShow.FormattingEnabled = true;
            this.lbRTShow.ItemHeight = 12;
            this.lbRTShow.Location = new System.Drawing.Point(6, 17);
            this.lbRTShow.Name = "lbRTShow";
            this.lbRTShow.Size = new System.Drawing.Size(445, 328);
            this.lbRTShow.TabIndex = 4;
            // 
            // testConnectTimer
            // 
            this.testConnectTimer.Interval = 60000;
            this.testConnectTimer.Tick += new System.EventHandler(this.rtTimer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.urlBox);
            this.groupBox2.Controls.Add(this.DisConnect);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblState);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Controls.Add(this.textBoxMQPsw);
            this.groupBox2.Controls.Add(this.textBoxMQPort);
            this.groupBox2.Controls.Add(this.textBoxMQUser);
            this.groupBox2.Controls.Add(this.textBoxMQIP);
            this.groupBox2.Controls.Add(this.baseURLBox);
            this.groupBox2.Location = new System.Drawing.Point(8, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 141);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备操作";
            // 
            // urlBox
            // 
            this.urlBox.Location = new System.Drawing.Point(72, 48);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(379, 21);
            this.urlBox.TabIndex = 1;
            this.urlBox.Text = "/attendance/checkinoutApi/getMachineDetail";
            // 
            // DisConnect
            // 
            this.DisConnect.BackColor = System.Drawing.Color.Khaki;
            this.DisConnect.BackgroundImage = global::AttenanceRTEvents.Properties.Resources.logbg;
            this.DisConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisConnect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.DisConnect.ForeColor = System.Drawing.Color.Yellow;
            this.DisConnect.Location = new System.Drawing.Point(131, 110);
            this.DisConnect.Name = "DisConnect";
            this.DisConnect.Size = new System.Drawing.Size(75, 23);
            this.DisConnect.TabIndex = 8;
            this.DisConnect.Text = "DisConnect";
            this.DisConnect.UseVisualStyleBackColor = false;
            this.DisConnect.Click += new System.EventHandler(this.disConnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(316, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "MQ密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "MQ账号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "MQIP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "项目url";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "域名地址";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.ForeColor = System.Drawing.Color.Crimson;
            this.lblState.Location = new System.Drawing.Point(222, 115);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(161, 12);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "Current State:Disconnected";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnConnect.BackgroundImage = global::AttenanceRTEvents.Properties.Resources.logbg;
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnConnect.ForeColor = System.Drawing.Color.Chartreuse;
            this.btnConnect.Location = new System.Drawing.Point(18, 110);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(78, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "ConnectAll";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textBoxMQPsw
            // 
            this.textBoxMQPsw.Location = new System.Drawing.Point(363, 75);
            this.textBoxMQPsw.Name = "textBoxMQPsw";
            this.textBoxMQPsw.PasswordChar = '*';
            this.textBoxMQPsw.Size = new System.Drawing.Size(88, 21);
            this.textBoxMQPsw.TabIndex = 5;
            this.textBoxMQPsw.Text = "admin";
            // 
            // textBoxMQPort
            // 
            this.textBoxMQPort.Location = new System.Drawing.Point(140, 75);
            this.textBoxMQPort.Name = "textBoxMQPort";
            this.textBoxMQPort.Size = new System.Drawing.Size(43, 21);
            this.textBoxMQPort.TabIndex = 3;
            this.textBoxMQPort.Text = "5672";
            // 
            // textBoxMQUser
            // 
            this.textBoxMQUser.Location = new System.Drawing.Point(236, 75);
            this.textBoxMQUser.Name = "textBoxMQUser";
            this.textBoxMQUser.Size = new System.Drawing.Size(74, 21);
            this.textBoxMQUser.TabIndex = 4;
            this.textBoxMQUser.Text = "admin";
            // 
            // textBoxMQIP
            // 
            this.textBoxMQIP.Location = new System.Drawing.Point(41, 75);
            this.textBoxMQIP.Name = "textBoxMQIP";
            this.textBoxMQIP.Size = new System.Drawing.Size(93, 21);
            this.textBoxMQIP.TabIndex = 2;
            this.textBoxMQIP.Text = "127.0.0.1";
            // 
            // baseURLBox
            // 
            this.baseURLBox.Location = new System.Drawing.Point(72, 26);
            this.baseURLBox.Name = "baseURLBox";
            this.baseURLBox.Size = new System.Drawing.Size(379, 21);
            this.baseURLBox.TabIndex = 0;
            this.baseURLBox.Text = "http://dk.bj.staff.xdf.cn";
            // 
            // machineListView
            // 
            this.machineListView.BackColor = System.Drawing.Color.MediumTurquoise;
            this.machineListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.machineNum,
            this.machineName,
            this.machineIp,
            this.machineState,
            this.uid,
            this.failCount});
            this.machineListView.FullRowSelect = true;
            this.machineListView.GridLines = true;
            this.machineListView.HideSelection = false;
            this.machineListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.machineListView.Location = new System.Drawing.Point(10, 29);
            this.machineListView.Name = "machineListView";
            this.machineListView.Size = new System.Drawing.Size(547, 425);
            this.machineListView.TabIndex = 67;
            this.machineListView.UseCompatibleStateImageBehavior = false;
            this.machineListView.View = System.Windows.Forms.View.Details;
            // 
            // machineNum
            // 
            this.machineNum.Text = "设备编号";
            this.machineNum.Width = 89;
            // 
            // machineName
            // 
            this.machineName.Text = "设备名";
            this.machineName.Width = 146;
            // 
            // machineIp
            // 
            this.machineIp.Text = "设备IP";
            this.machineIp.Width = 158;
            // 
            // machineState
            // 
            this.machineState.Text = "设备状态";
            this.machineState.Width = 77;
            // 
            // uid
            // 
            this.uid.Text = "uid";
            this.uid.Width = 0;
            // 
            // failCount
            // 
            this.failCount.Text = "失败次数";
            // 
            // timerRefreshMachine
            // 
            this.timerRefreshMachine.Enabled = true;
            this.timerRefreshMachine.Interval = 10000;
            this.timerRefreshMachine.Tick += new System.EventHandler(this.timerRefreshMachine_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.machineListView);
            this.groupBox1.Location = new System.Drawing.Point(488, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 468);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备信息";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::AttenanceRTEvents.Properties.Resources.logo;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::AttenanceRTEvents.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(488, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(563, 40);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // RTEventsMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(1059, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RTEventsMain";
            this.Text = "打卡机事件监听程序";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Timer testConnectTimer;
        private System.Windows.Forms.ListBox lbRTShow;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox baseURLBox;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button DisConnect;
        private System.Windows.Forms.ListView machineListView;
        private System.Windows.Forms.ColumnHeader machineNum;
        private System.Windows.Forms.ColumnHeader machineName;
        private System.Windows.Forms.ColumnHeader machineIp;
        private System.Windows.Forms.ColumnHeader machineState;
        private System.Windows.Forms.Timer timerRefreshMachine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader uid;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMQPsw;
        private System.Windows.Forms.TextBox textBoxMQPort;
        private System.Windows.Forms.TextBox textBoxMQUser;
        private System.Windows.Forms.TextBox textBoxMQIP;
        private System.Windows.Forms.ColumnHeader failCount;
    }
}

