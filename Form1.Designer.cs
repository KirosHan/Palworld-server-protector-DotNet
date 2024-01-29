namespace Palworld_server_protector_DotNet
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            checkBox_reboot = new CheckBox();
            checkBox_save = new CheckBox();
            checkBox_mem = new CheckBox();
            outPutbox = new RichTextBox();
            selectCmdbutton = new Button();
            rebootSecondbox = new NumericUpDown();
            labelForpassword = new Label();
            checkSecondbox = new NumericUpDown();
            labelForrconport = new Label();
            memTargetbox = new NumericUpDown();
            labelForrebootsecond = new Label();
            labelForchecksecond = new Label();
            labelFotmemtarget = new Label();
            labelForcmd = new Label();
            passWordbox = new TextBox();
            cmdbox = new TextBox();
            rconPortbox = new TextBox();
            checkBox_startprocess = new CheckBox();
            selectBackuppathButton = new Button();
            backupSecondsbox = new NumericUpDown();
            labelForbackuppath = new Label();
            backupPathbox = new TextBox();
            labelForbackupsecond = new Label();
            memProcessbar = new ProgressBar();
            memOutput = new Label();
            checkBox_geplayers = new CheckBox();
            groupBox1 = new GroupBox();
            arguments = new TextBox();
            checkBox_args = new CheckBox();
            label1 = new Label();
            settingButton = new Button();
            groupBox2 = new GroupBox();
            label3 = new Label();
            selectCustombutton = new Button();
            gamedataBox = new TextBox();
            groupBox3 = new GroupBox();
            playersView = new ListView();
            playersCounterLabel = new Label();
            groupBox4 = new GroupBox();
            UIDBox = new TextBox();
            label5 = new Label();
            banbutton = new Button();
            kickbutton = new Button();
            versionLabel = new Label();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            groupBox5 = new GroupBox();
            verisionLabel = new Label();
            linkLabel1 = new LinkLabel();
            groupBox6 = new GroupBox();
            groupBox7 = new GroupBox();
            testWebhookbutton = new Button();
            webhookBox = new TextBox();
            checkBox_webhook = new CheckBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)rebootSecondbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkSecondbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memTargetbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backupSecondsbox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox_reboot
            // 
            checkBox_reboot.AutoSize = true;
            checkBox_reboot.Location = new Point(25, 143);
            checkBox_reboot.Margin = new Padding(5, 4, 5, 4);
            checkBox_reboot.Name = "checkBox_reboot";
            checkBox_reboot.Size = new Size(108, 28);
            checkBox_reboot.TabIndex = 0;
            checkBox_reboot.Text = "自动关服";
            checkBox_reboot.UseVisualStyleBackColor = true;
            checkBox_reboot.CheckedChanged += checkBox_reboot_CheckedChanged;
            // 
            // checkBox_save
            // 
            checkBox_save.AutoSize = true;
            checkBox_save.Location = new Point(25, 47);
            checkBox_save.Margin = new Padding(5, 4, 5, 4);
            checkBox_save.Name = "checkBox_save";
            checkBox_save.Size = new Size(108, 28);
            checkBox_save.TabIndex = 0;
            checkBox_save.Text = "自动存档";
            checkBox_save.UseVisualStyleBackColor = true;
            checkBox_save.CheckedChanged += checkBox_save_CheckedChanged;
            // 
            // checkBox_mem
            // 
            checkBox_mem.AutoSize = true;
            checkBox_mem.Location = new Point(171, 143);
            checkBox_mem.Margin = new Padding(5, 4, 5, 4);
            checkBox_mem.Name = "checkBox_mem";
            checkBox_mem.Size = new Size(198, 28);
            checkBox_mem.TabIndex = 0;
            checkBox_mem.Text = "输出内存到数据回显";
            checkBox_mem.UseVisualStyleBackColor = true;
            // 
            // outPutbox
            // 
            outPutbox.BorderStyle = BorderStyle.None;
            outPutbox.Dock = DockStyle.Fill;
            outPutbox.Location = new Point(3, 26);
            outPutbox.Margin = new Padding(5, 4, 5, 4);
            outPutbox.Name = "outPutbox";
            outPutbox.Size = new Size(659, 430);
            outPutbox.TabIndex = 0;
            outPutbox.Text = "";
            // 
            // selectCmdbutton
            // 
            selectCmdbutton.Location = new Point(495, 240);
            selectCmdbutton.Margin = new Padding(5, 4, 5, 4);
            selectCmdbutton.Name = "selectCmdbutton";
            selectCmdbutton.Size = new Size(63, 32);
            selectCmdbutton.TabIndex = 3;
            selectCmdbutton.Text = "选择";
            selectCmdbutton.UseVisualStyleBackColor = true;
            selectCmdbutton.Click += selectCmdbutton_Click;
            // 
            // rebootSecondbox
            // 
            rebootSecondbox.BorderStyle = BorderStyle.FixedSingle;
            rebootSecondbox.Location = new Point(171, 391);
            rebootSecondbox.Margin = new Padding(5, 4, 5, 4);
            rebootSecondbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            rebootSecondbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            rebootSecondbox.Name = "rebootSecondbox";
            rebootSecondbox.Size = new Size(383, 30);
            rebootSecondbox.TabIndex = 2;
            rebootSecondbox.Value = new decimal(new int[] { 10, 0, 0, 0 });
            rebootSecondbox.ValueChanged += rebootSecondbox_ValueChanged_1;
            rebootSecondbox.KeyUp += rebootSecondbox_KeyUp;
            // 
            // labelForpassword
            // 
            labelForpassword.AutoSize = true;
            labelForpassword.Location = new Point(25, 493);
            labelForpassword.Margin = new Padding(5, 0, 5, 0);
            labelForpassword.Name = "labelForpassword";
            labelForpassword.Size = new Size(100, 24);
            labelForpassword.TabIndex = 6;
            labelForpassword.Text = "管理员密码";
            // 
            // checkSecondbox
            // 
            checkSecondbox.BorderStyle = BorderStyle.FixedSingle;
            checkSecondbox.Location = new Point(171, 342);
            checkSecondbox.Margin = new Padding(5, 4, 5, 4);
            checkSecondbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            checkSecondbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            checkSecondbox.Name = "checkSecondbox";
            checkSecondbox.Size = new Size(383, 30);
            checkSecondbox.TabIndex = 2;
            checkSecondbox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            checkSecondbox.ValueChanged += checkSecondbox_ValueChanged;
            checkSecondbox.KeyUp += checkSecondbox_KeyUp;
            // 
            // labelForrconport
            // 
            labelForrconport.AutoSize = true;
            labelForrconport.Location = new Point(25, 443);
            labelForrconport.Margin = new Padding(5, 0, 5, 0);
            labelForrconport.Name = "labelForrconport";
            labelForrconport.Size = new Size(89, 24);
            labelForrconport.TabIndex = 6;
            labelForrconport.Text = "Rcon端口";
            // 
            // memTargetbox
            // 
            memTargetbox.BorderStyle = BorderStyle.FixedSingle;
            memTargetbox.Location = new Point(171, 287);
            memTargetbox.Margin = new Padding(5, 4, 5, 4);
            memTargetbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            memTargetbox.Name = "memTargetbox";
            memTargetbox.Size = new Size(383, 30);
            memTargetbox.TabIndex = 2;
            memTargetbox.Value = new decimal(new int[] { 90, 0, 0, 0 });
            memTargetbox.ValueChanged += memTargetbox_ValueChanged;
            memTargetbox.KeyUp += memTargetbox_KeyUp;
            // 
            // labelForrebootsecond
            // 
            labelForrebootsecond.AutoSize = true;
            labelForrebootsecond.Location = new Point(25, 392);
            labelForrebootsecond.Margin = new Padding(5, 0, 5, 0);
            labelForrebootsecond.Name = "labelForrebootsecond";
            labelForrebootsecond.Size = new Size(136, 24);
            labelForrebootsecond.TabIndex = 1;
            labelForrebootsecond.Text = "重启延迟（秒）";
            // 
            // labelForchecksecond
            // 
            labelForchecksecond.AutoSize = true;
            labelForchecksecond.Location = new Point(25, 343);
            labelForchecksecond.Margin = new Padding(5, 0, 5, 0);
            labelForchecksecond.Name = "labelForchecksecond";
            labelForchecksecond.Size = new Size(136, 24);
            labelForchecksecond.TabIndex = 1;
            labelForchecksecond.Text = "检测周期（秒）";
            // 
            // labelFotmemtarget
            // 
            labelFotmemtarget.AutoSize = true;
            labelFotmemtarget.Location = new Point(25, 294);
            labelFotmemtarget.Margin = new Padding(5, 0, 5, 0);
            labelFotmemtarget.Name = "labelFotmemtarget";
            labelFotmemtarget.Size = new Size(82, 24);
            labelFotmemtarget.TabIndex = 1;
            labelFotmemtarget.Text = "内存阈值";
            // 
            // labelForcmd
            // 
            labelForcmd.AutoSize = true;
            labelForcmd.Location = new Point(25, 243);
            labelForcmd.Margin = new Padding(5, 0, 5, 0);
            labelForcmd.Name = "labelForcmd";
            labelForcmd.Size = new Size(82, 24);
            labelForcmd.TabIndex = 1;
            labelForcmd.Text = "启动路径";
            // 
            // passWordbox
            // 
            passWordbox.BorderStyle = BorderStyle.FixedSingle;
            passWordbox.Location = new Point(171, 487);
            passWordbox.Margin = new Padding(5, 4, 5, 4);
            passWordbox.Name = "passWordbox";
            passWordbox.Size = new Size(384, 30);
            passWordbox.TabIndex = 1;
            passWordbox.Text = "admin";
            passWordbox.KeyPress += passWordbox_KeyPress;
            // 
            // cmdbox
            // 
            cmdbox.BorderStyle = BorderStyle.FixedSingle;
            cmdbox.Location = new Point(173, 240);
            cmdbox.Margin = new Padding(5, 4, 5, 4);
            cmdbox.Name = "cmdbox";
            cmdbox.ReadOnly = true;
            cmdbox.Size = new Size(315, 30);
            cmdbox.TabIndex = 1;
            // 
            // rconPortbox
            // 
            rconPortbox.BorderStyle = BorderStyle.FixedSingle;
            rconPortbox.Location = new Point(171, 440);
            rconPortbox.Margin = new Padding(5, 4, 5, 4);
            rconPortbox.Name = "rconPortbox";
            rconPortbox.Size = new Size(384, 30);
            rconPortbox.TabIndex = 1;
            rconPortbox.Text = "25575";
            rconPortbox.KeyPress += rconPortbox_KeyPress;
            // 
            // checkBox_startprocess
            // 
            checkBox_startprocess.AutoSize = true;
            checkBox_startprocess.Location = new Point(25, 193);
            checkBox_startprocess.Margin = new Padding(5, 4, 5, 4);
            checkBox_startprocess.Name = "checkBox_startprocess";
            checkBox_startprocess.Size = new Size(108, 28);
            checkBox_startprocess.TabIndex = 0;
            checkBox_startprocess.Text = "自动启动";
            checkBox_startprocess.UseVisualStyleBackColor = true;
            checkBox_startprocess.CheckedChanged += checkBox_startprocess_CheckedChanged;
            // 
            // selectBackuppathButton
            // 
            selectBackuppathButton.Location = new Point(495, 268);
            selectBackuppathButton.Name = "selectBackuppathButton";
            selectBackuppathButton.Size = new Size(63, 32);
            selectBackuppathButton.TabIndex = 4;
            selectBackuppathButton.Text = "选择";
            selectBackuppathButton.UseVisualStyleBackColor = true;
            selectBackuppathButton.Click += selectBackuppathButton_Click;
            // 
            // backupSecondsbox
            // 
            backupSecondsbox.BorderStyle = BorderStyle.FixedSingle;
            backupSecondsbox.Location = new Point(167, 95);
            backupSecondsbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            backupSecondsbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            backupSecondsbox.Name = "backupSecondsbox";
            backupSecondsbox.Size = new Size(388, 30);
            backupSecondsbox.TabIndex = 3;
            backupSecondsbox.Value = new decimal(new int[] { 1800, 0, 0, 0 });
            backupSecondsbox.ValueChanged += backupSecondsbox_ValueChanged;
            backupSecondsbox.KeyUp += backupSecondsbox_KeyUp;
            // 
            // labelForbackuppath
            // 
            labelForbackuppath.AutoSize = true;
            labelForbackuppath.Location = new Point(25, 265);
            labelForbackuppath.Margin = new Padding(5, 0, 5, 0);
            labelForbackuppath.Name = "labelForbackuppath";
            labelForbackuppath.Size = new Size(118, 24);
            labelForbackuppath.TabIndex = 2;
            labelForbackuppath.Text = "备份存放目录";
            // 
            // backupPathbox
            // 
            backupPathbox.BorderStyle = BorderStyle.FixedSingle;
            backupPathbox.Location = new Point(167, 264);
            backupPathbox.Margin = new Padding(5, 4, 5, 4);
            backupPathbox.Name = "backupPathbox";
            backupPathbox.ReadOnly = true;
            backupPathbox.Size = new Size(318, 30);
            backupPathbox.TabIndex = 1;
            // 
            // labelForbackupsecond
            // 
            labelForbackupsecond.AutoSize = true;
            labelForbackupsecond.Location = new Point(25, 96);
            labelForbackupsecond.Margin = new Padding(5, 0, 5, 0);
            labelForbackupsecond.Name = "labelForbackupsecond";
            labelForbackupsecond.Size = new Size(136, 24);
            labelForbackupsecond.TabIndex = 1;
            labelForbackupsecond.Text = "存档周期（秒）";
            // 
            // memProcessbar
            // 
            memProcessbar.Location = new Point(25, 47);
            memProcessbar.Name = "memProcessbar";
            memProcessbar.Size = new Size(533, 34);
            memProcessbar.Style = ProgressBarStyle.Continuous;
            memProcessbar.TabIndex = 7;
            // 
            // memOutput
            // 
            memOutput.AutoSize = true;
            memOutput.Location = new Point(167, 95);
            memOutput.Name = "memOutput";
            memOutput.Size = new Size(37, 24);
            memOutput.TabIndex = 8;
            memOutput.Text = "0%";
            memOutput.Click += memOutput_Click;
            // 
            // checkBox_geplayers
            // 
            checkBox_geplayers.AutoSize = true;
            checkBox_geplayers.Location = new Point(25, 240);
            checkBox_geplayers.Name = "checkBox_geplayers";
            checkBox_geplayers.Size = new Size(180, 28);
            checkBox_geplayers.TabIndex = 10;
            checkBox_geplayers.Text = "自动获取在线玩家";
            checkBox_geplayers.UseVisualStyleBackColor = true;
            checkBox_geplayers.CheckedChanged += checkBox_geplayers_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(arguments);
            groupBox1.Controls.Add(checkBox_args);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(selectCmdbutton);
            groupBox1.Controls.Add(checkBox_reboot);
            groupBox1.Controls.Add(rebootSecondbox);
            groupBox1.Controls.Add(checkBox_mem);
            groupBox1.Controls.Add(labelForpassword);
            groupBox1.Controls.Add(memOutput);
            groupBox1.Controls.Add(checkBox_startprocess);
            groupBox1.Controls.Add(memProcessbar);
            groupBox1.Controls.Add(checkSecondbox);
            groupBox1.Controls.Add(rconPortbox);
            groupBox1.Controls.Add(labelForrconport);
            groupBox1.Controls.Add(cmdbox);
            groupBox1.Controls.Add(memTargetbox);
            groupBox1.Controls.Add(passWordbox);
            groupBox1.Controls.Add(labelForrebootsecond);
            groupBox1.Controls.Add(labelForcmd);
            groupBox1.Controls.Add(labelForchecksecond);
            groupBox1.Controls.Add(labelFotmemtarget);
            groupBox1.Location = new Point(19, 23);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(592, 538);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "监控";
            // 
            // arguments
            // 
            arguments.BorderStyle = BorderStyle.FixedSingle;
            arguments.Enabled = false;
            arguments.Location = new Point(354, 191);
            arguments.Name = "arguments";
            arguments.Size = new Size(202, 30);
            arguments.TabIndex = 11;
            arguments.Text = "EpicApp=PalServer";
            // 
            // checkBox_args
            // 
            checkBox_args.AutoSize = true;
            checkBox_args.Location = new Point(171, 193);
            checkBox_args.Name = "checkBox_args";
            checkBox_args.Size = new Size(180, 28);
            checkBox_args.TabIndex = 10;
            checkBox_args.Text = "参数启动（可选）";
            checkBox_args.UseVisualStyleBackColor = true;
            checkBox_args.CheckedChanged += checkBox_args_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 95);
            label1.Name = "label1";
            label1.Size = new Size(136, 24);
            label1.TabIndex = 9;
            label1.Text = "系统内存占用：";
            // 
            // settingButton
            // 
            settingButton.Location = new Point(25, 40);
            settingButton.Margin = new Padding(5, 4, 5, 4);
            settingButton.Name = "settingButton";
            settingButton.Size = new Size(212, 32);
            settingButton.TabIndex = 12;
            settingButton.Text = "服务端配置文件(测试)";
            settingButton.UseVisualStyleBackColor = true;
            settingButton.Click += settingButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(selectCustombutton);
            groupBox2.Controls.Add(gamedataBox);
            groupBox2.Controls.Add(selectBackuppathButton);
            groupBox2.Controls.Add(checkBox_save);
            groupBox2.Controls.Add(backupSecondsbox);
            groupBox2.Controls.Add(labelForbackupsecond);
            groupBox2.Controls.Add(labelForbackuppath);
            groupBox2.Controls.Add(backupPathbox);
            groupBox2.Location = new Point(19, 666);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(592, 320);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "自动存档";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 145);
            label3.Name = "label3";
            label3.Size = new Size(118, 96);
            label3.TabIndex = 8;
            label3.Text = "游戏存档目录\r\n（根据服务端\r\n位置自动生成\r\n可手动选择）";
            // 
            // selectCustombutton
            // 
            selectCustombutton.Location = new Point(495, 143);
            selectCustombutton.Name = "selectCustombutton";
            selectCustombutton.Size = new Size(63, 32);
            selectCustombutton.TabIndex = 7;
            selectCustombutton.Text = "选择";
            selectCustombutton.UseVisualStyleBackColor = true;
            selectCustombutton.Click += selectCustombutton_Click;
            // 
            // gamedataBox
            // 
            gamedataBox.BorderStyle = BorderStyle.FixedSingle;
            gamedataBox.Location = new Point(167, 144);
            gamedataBox.Name = "gamedataBox";
            gamedataBox.ReadOnly = true;
            gamedataBox.Size = new Size(318, 30);
            gamedataBox.TabIndex = 6;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(outPutbox);
            groupBox3.Location = new Point(636, 620);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(665, 459);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Console";
            // 
            // playersView
            // 
            playersView.Location = new Point(6, 30);
            playersView.Name = "playersView";
            playersView.Size = new Size(651, 173);
            playersView.TabIndex = 14;
            playersView.UseCompatibleStateImageBehavior = false;
            playersView.ItemSelectionChanged += playersView_ItemSelectionChanged;
            // 
            // playersCounterLabel
            // 
            playersCounterLabel.AutoSize = true;
            playersCounterLabel.Location = new Point(234, 241);
            playersCounterLabel.Name = "playersCounterLabel";
            playersCounterLabel.Size = new Size(136, 24);
            playersCounterLabel.TabIndex = 15;
            playersCounterLabel.Text = "当前在线：未知";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(UIDBox);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(banbutton);
            groupBox4.Controls.Add(kickbutton);
            groupBox4.Controls.Add(versionLabel);
            groupBox4.Controls.Add(button4);
            groupBox4.Controls.Add(button3);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(button1);
            groupBox4.Controls.Add(textBox1);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(playersView);
            groupBox4.Controls.Add(playersCounterLabel);
            groupBox4.Controls.Add(checkBox_geplayers);
            groupBox4.Location = new Point(632, 23);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(665, 440);
            groupBox4.TabIndex = 16;
            groupBox4.TabStop = false;
            groupBox4.Text = "Rcon";
            // 
            // UIDBox
            // 
            UIDBox.BorderStyle = BorderStyle.FixedSingle;
            UIDBox.Location = new Point(94, 288);
            UIDBox.Name = "UIDBox";
            UIDBox.Size = new Size(275, 30);
            UIDBox.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 289);
            label5.Name = "label5";
            label5.Size = new Size(60, 24);
            label5.TabIndex = 24;
            label5.Text = "UID：";
            // 
            // banbutton
            // 
            banbutton.Location = new Point(519, 287);
            banbutton.Name = "banbutton";
            banbutton.Size = new Size(112, 32);
            banbutton.TabIndex = 23;
            banbutton.Text = "Ban";
            banbutton.UseVisualStyleBackColor = true;
            banbutton.Click += banbutton_Click;
            // 
            // kickbutton
            // 
            kickbutton.Location = new Point(391, 287);
            kickbutton.Name = "kickbutton";
            kickbutton.Size = new Size(112, 32);
            kickbutton.TabIndex = 23;
            kickbutton.Text = "Kick";
            kickbutton.UseVisualStyleBackColor = true;
            kickbutton.Click += kickbutton_Click;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(459, 394);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(154, 24);
            versionLabel.TabIndex = 21;
            versionLabel.Text = "服务端版本：未知";
            // 
            // button4
            // 
            button4.Location = new Point(310, 390);
            button4.Name = "button4";
            button4.Size = new Size(135, 32);
            button4.TabIndex = 20;
            button4.Text = "获取服本号";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(167, 390);
            button3.Name = "button3";
            button3.Size = new Size(135, 32);
            button3.TabIndex = 20;
            button3.Text = "10s安全关服";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(25, 390);
            button2.Name = "button2";
            button2.Size = new Size(135, 32);
            button2.TabIndex = 19;
            button2.Text = "服务端存档";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(519, 337);
            button1.Name = "button1";
            button1.Size = new Size(112, 32);
            button1.TabIndex = 18;
            button1.Text = "发送";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(94, 337);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(407, 30);
            textBox1.TabIndex = 17;
            textBox1.Text = "This_is_a_rcon_message.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 340);
            label2.Name = "label2";
            label2.Size = new Size(64, 24);
            label2.TabIndex = 16;
            label2.Text = "广播：";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(verisionLabel);
            groupBox5.Controls.Add(linkLabel1);
            groupBox5.Location = new Point(19, 992);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(592, 86);
            groupBox5.TabIndex = 17;
            groupBox5.TabStop = false;
            groupBox5.Text = "Version";
            // 
            // verisionLabel
            // 
            verisionLabel.AutoSize = true;
            verisionLabel.Location = new Point(25, 42);
            verisionLabel.Name = "verisionLabel";
            verisionLabel.Size = new Size(150, 24);
            verisionLabel.TabIndex = 1;
            verisionLabel.Text = "当前版本：v2.0.0";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(223, 42);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(83, 24);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = " By Kiros";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(settingButton);
            groupBox6.Location = new Point(19, 568);
            groupBox6.Margin = new Padding(5, 4, 5, 4);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(5, 4, 5, 4);
            groupBox6.Size = new Size(592, 92);
            groupBox6.TabIndex = 18;
            groupBox6.TabStop = false;
            groupBox6.Text = "配置文件";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label4);
            groupBox7.Controls.Add(testWebhookbutton);
            groupBox7.Controls.Add(webhookBox);
            groupBox7.Controls.Add(checkBox_webhook);
            groupBox7.Location = new Point(632, 469);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(665, 145);
            groupBox7.TabIndex = 19;
            groupBox7.TabStop = false;
            groupBox7.Text = "Webhook";
            // 
            // testWebhookbutton
            // 
            testWebhookbutton.Enabled = false;
            testWebhookbutton.Location = new Point(519, 91);
            testWebhookbutton.Name = "testWebhookbutton";
            testWebhookbutton.Size = new Size(112, 34);
            testWebhookbutton.TabIndex = 2;
            testWebhookbutton.Text = "测试";
            testWebhookbutton.UseVisualStyleBackColor = true;
            testWebhookbutton.Click += testWebhookbutton_Click;
            // 
            // webhookBox
            // 
            webhookBox.BorderStyle = BorderStyle.FixedSingle;
            webhookBox.Enabled = false;
            webhookBox.Location = new Point(167, 95);
            webhookBox.Name = "webhookBox";
            webhookBox.Size = new Size(346, 30);
            webhookBox.TabIndex = 1;
            // 
            // checkBox_webhook
            // 
            checkBox_webhook.AutoSize = true;
            checkBox_webhook.Location = new Point(25, 47);
            checkBox_webhook.Name = "checkBox_webhook";
            checkBox_webhook.Size = new Size(191, 28);
            checkBox_webhook.TabIndex = 0;
            checkBox_webhook.Text = "启用Webhook推送";
            checkBox_webhook.UseVisualStyleBackColor = true;
            checkBox_webhook.CheckedChanged += checkBox_webhook_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 97);
            label4.Name = "label4";
            label4.Size = new Size(141, 24);
            label4.TabIndex = 3;
            label4.Text = "Webhook Url：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1309, 1088);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Palworld Server Protector";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)rebootSecondbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkSecondbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)memTargetbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)backupSecondsbox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox outPutbox;
        private CheckBox checkBox_mem;
        private CheckBox checkBox_reboot;
        private CheckBox checkBox_save;
        private Label labelForcmd;
        private TextBox cmdbox;
        private Label labelForbackuppath;
        private TextBox backupPathbox;
        private NumericUpDown checkSecondbox;
        private NumericUpDown memTargetbox;
        private Label labelForchecksecond;
        private Label labelFotmemtarget;
        private NumericUpDown rebootSecondbox;
        private Label labelForrebootsecond;
        private Label labelForrconport;
        private TextBox rconPortbox;
        private Label labelForpassword;
        private TextBox passWordbox;
        private Button selectCmdbutton;
        private ProgressBar memProcessbar;
        private Label memOutput;
        private CheckBox checkBox_startprocess;
        private Label labelForbackupsecond;
        private NumericUpDown backupSecondsbox;
        private Button selectBackuppathButton;
        private CheckBox checkBox_geplayers;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ListView playersView;
        private Label playersCounterLabel;
        private GroupBox groupBox4;
        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private Button button2;
        private Button button3;
        private Button button4;
        private GroupBox groupBox5;
        private Label versionLabel;
        private LinkLabel linkLabel1;
        private Label verisionLabel;
        private CheckBox checkBox_args;
        private TextBox arguments;
        private Button selectCustombutton;
        private TextBox gamedataBox;
        private Label label3;
        private Button banbutton;
        private Button kickbutton;
        private TextBox UIDBox;
        private Label label5;
        private Button settingButton;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private CheckBox checkBox_webhook;
        private TextBox webhookBox;
        private Button testWebhookbutton;
        private Label label4;
    }
}
