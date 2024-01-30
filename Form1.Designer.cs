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
            components = new System.ComponentModel.Container();
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
            labelForwebhook = new Label();
            labelForgetplayers = new Label();
            labelForsave = new Label();
            labelForstart = new Label();
            labelForreboot = new Label();
            label1 = new Label();
            arguments = new TextBox();
            checkBox_args = new CheckBox();
            settingButton = new Button();
            label3 = new Label();
            selectCustombutton = new Button();
            gamedataBox = new TextBox();
            groupBox3 = new GroupBox();
            playersView = new ListView();
            playersCounterLabel = new Label();
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
            label4 = new Label();
            testWebhookbutton = new Button();
            webhookBox = new TextBox();
            checkBox_webhook = new CheckBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            notifyIcon1 = new NotifyIcon(components);
            checkBox_Noti = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)rebootSecondbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkSecondbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memTargetbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backupSecondsbox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox_reboot
            // 
            checkBox_reboot.AutoSize = true;
            checkBox_reboot.Location = new Point(6, 6);
            checkBox_reboot.Name = "checkBox_reboot";
            checkBox_reboot.Size = new Size(75, 21);
            checkBox_reboot.TabIndex = 0;
            checkBox_reboot.Text = "自动关服";
            checkBox_reboot.UseVisualStyleBackColor = true;
            checkBox_reboot.CheckedChanged += checkBox_reboot_CheckedChanged;
            // 
            // checkBox_save
            // 
            checkBox_save.AutoSize = true;
            checkBox_save.Location = new Point(6, 10);
            checkBox_save.Name = "checkBox_save";
            checkBox_save.Size = new Size(75, 21);
            checkBox_save.TabIndex = 0;
            checkBox_save.Text = "自动存档";
            checkBox_save.UseVisualStyleBackColor = true;
            checkBox_save.CheckedChanged += checkBox_save_CheckedChanged;
            // 
            // checkBox_mem
            // 
            checkBox_mem.AutoSize = true;
            checkBox_mem.Location = new Point(99, 6);
            checkBox_mem.Name = "checkBox_mem";
            checkBox_mem.Size = new Size(135, 21);
            checkBox_mem.TabIndex = 0;
            checkBox_mem.Text = "输出内存到数据回显";
            checkBox_mem.UseVisualStyleBackColor = true;
            // 
            // outPutbox
            // 
            outPutbox.BorderStyle = BorderStyle.None;
            outPutbox.Dock = DockStyle.Fill;
            outPutbox.Location = new Point(2, 18);
            outPutbox.Name = "outPutbox";
            outPutbox.Size = new Size(390, 160);
            outPutbox.TabIndex = 0;
            outPutbox.Text = "";
            // 
            // selectCmdbutton
            // 
            selectCmdbutton.Location = new Point(305, 75);
            selectCmdbutton.Name = "selectCmdbutton";
            selectCmdbutton.Size = new Size(40, 23);
            selectCmdbutton.TabIndex = 3;
            selectCmdbutton.Text = "选择";
            selectCmdbutton.UseVisualStyleBackColor = true;
            selectCmdbutton.Click += selectCmdbutton_Click;
            // 
            // rebootSecondbox
            // 
            rebootSecondbox.BorderStyle = BorderStyle.FixedSingle;
            rebootSecondbox.Location = new Point(99, 182);
            rebootSecondbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            rebootSecondbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            rebootSecondbox.Name = "rebootSecondbox";
            rebootSecondbox.Size = new Size(244, 23);
            rebootSecondbox.TabIndex = 2;
            rebootSecondbox.Value = new decimal(new int[] { 10, 0, 0, 0 });
            rebootSecondbox.ValueChanged += rebootSecondbox_ValueChanged_1;
            rebootSecondbox.KeyUp += rebootSecondbox_KeyUp;
            // 
            // labelForpassword
            // 
            labelForpassword.AutoSize = true;
            labelForpassword.Location = new Point(6, 254);
            labelForpassword.Name = "labelForpassword";
            labelForpassword.Size = new Size(68, 17);
            labelForpassword.TabIndex = 6;
            labelForpassword.Text = "管理员密码";
            // 
            // checkSecondbox
            // 
            checkSecondbox.BorderStyle = BorderStyle.FixedSingle;
            checkSecondbox.Location = new Point(99, 147);
            checkSecondbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            checkSecondbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            checkSecondbox.Name = "checkSecondbox";
            checkSecondbox.Size = new Size(244, 23);
            checkSecondbox.TabIndex = 2;
            checkSecondbox.Value = new decimal(new int[] { 20, 0, 0, 0 });
            checkSecondbox.ValueChanged += checkSecondbox_ValueChanged;
            checkSecondbox.KeyUp += checkSecondbox_KeyUp;
            // 
            // labelForrconport
            // 
            labelForrconport.AutoSize = true;
            labelForrconport.Location = new Point(6, 219);
            labelForrconport.Name = "labelForrconport";
            labelForrconport.Size = new Size(61, 17);
            labelForrconport.TabIndex = 6;
            labelForrconport.Text = "Rcon端口";
            // 
            // memTargetbox
            // 
            memTargetbox.BorderStyle = BorderStyle.FixedSingle;
            memTargetbox.Location = new Point(99, 108);
            memTargetbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            memTargetbox.Name = "memTargetbox";
            memTargetbox.Size = new Size(244, 23);
            memTargetbox.TabIndex = 2;
            memTargetbox.Value = new decimal(new int[] { 90, 0, 0, 0 });
            memTargetbox.ValueChanged += memTargetbox_ValueChanged;
            memTargetbox.KeyUp += memTargetbox_KeyUp;
            // 
            // labelForrebootsecond
            // 
            labelForrebootsecond.AutoSize = true;
            labelForrebootsecond.Location = new Point(6, 183);
            labelForrebootsecond.Name = "labelForrebootsecond";
            labelForrebootsecond.Size = new Size(92, 17);
            labelForrebootsecond.TabIndex = 1;
            labelForrebootsecond.Text = "重启延迟（秒）";
            // 
            // labelForchecksecond
            // 
            labelForchecksecond.AutoSize = true;
            labelForchecksecond.Location = new Point(6, 148);
            labelForchecksecond.Name = "labelForchecksecond";
            labelForchecksecond.Size = new Size(92, 17);
            labelForchecksecond.TabIndex = 1;
            labelForchecksecond.Text = "检测周期（秒）";
            // 
            // labelFotmemtarget
            // 
            labelFotmemtarget.AutoSize = true;
            labelFotmemtarget.Location = new Point(6, 113);
            labelFotmemtarget.Name = "labelFotmemtarget";
            labelFotmemtarget.Size = new Size(56, 17);
            labelFotmemtarget.TabIndex = 1;
            labelFotmemtarget.Text = "内存阈值";
            // 
            // labelForcmd
            // 
            labelForcmd.AutoSize = true;
            labelForcmd.Location = new Point(6, 77);
            labelForcmd.Name = "labelForcmd";
            labelForcmd.Size = new Size(56, 17);
            labelForcmd.TabIndex = 1;
            labelForcmd.Text = "启动路径";
            // 
            // passWordbox
            // 
            passWordbox.BorderStyle = BorderStyle.FixedSingle;
            passWordbox.Location = new Point(99, 250);
            passWordbox.Name = "passWordbox";
            passWordbox.Size = new Size(245, 23);
            passWordbox.TabIndex = 1;
            passWordbox.Text = "admin";
            passWordbox.KeyPress += passWordbox_KeyPress;
            // 
            // cmdbox
            // 
            cmdbox.BorderStyle = BorderStyle.FixedSingle;
            cmdbox.Location = new Point(100, 75);
            cmdbox.Name = "cmdbox";
            cmdbox.ReadOnly = true;
            cmdbox.Size = new Size(201, 23);
            cmdbox.TabIndex = 1;
            // 
            // rconPortbox
            // 
            rconPortbox.BorderStyle = BorderStyle.FixedSingle;
            rconPortbox.Location = new Point(99, 217);
            rconPortbox.Name = "rconPortbox";
            rconPortbox.Size = new Size(245, 23);
            rconPortbox.TabIndex = 1;
            rconPortbox.Text = "25575";
            rconPortbox.KeyPress += rconPortbox_KeyPress;
            // 
            // checkBox_startprocess
            // 
            checkBox_startprocess.AutoSize = true;
            checkBox_startprocess.Location = new Point(6, 42);
            checkBox_startprocess.Name = "checkBox_startprocess";
            checkBox_startprocess.Size = new Size(75, 21);
            checkBox_startprocess.TabIndex = 0;
            checkBox_startprocess.Text = "自动启动";
            checkBox_startprocess.UseVisualStyleBackColor = true;
            checkBox_startprocess.CheckedChanged += checkBox_startprocess_CheckedChanged;
            // 
            // selectBackuppathButton
            // 
            selectBackuppathButton.Location = new Point(305, 167);
            selectBackuppathButton.Margin = new Padding(2);
            selectBackuppathButton.Name = "selectBackuppathButton";
            selectBackuppathButton.Size = new Size(40, 23);
            selectBackuppathButton.TabIndex = 4;
            selectBackuppathButton.Text = "选择";
            selectBackuppathButton.UseVisualStyleBackColor = true;
            selectBackuppathButton.Click += selectBackuppathButton_Click;
            // 
            // backupSecondsbox
            // 
            backupSecondsbox.BorderStyle = BorderStyle.FixedSingle;
            backupSecondsbox.Location = new Point(96, 44);
            backupSecondsbox.Margin = new Padding(2);
            backupSecondsbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            backupSecondsbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            backupSecondsbox.Name = "backupSecondsbox";
            backupSecondsbox.Size = new Size(247, 23);
            backupSecondsbox.TabIndex = 3;
            backupSecondsbox.Value = new decimal(new int[] { 1800, 0, 0, 0 });
            backupSecondsbox.ValueChanged += backupSecondsbox_ValueChanged;
            backupSecondsbox.KeyUp += backupSecondsbox_KeyUp;
            // 
            // labelForbackuppath
            // 
            labelForbackuppath.AutoSize = true;
            labelForbackuppath.Location = new Point(6, 165);
            labelForbackuppath.Name = "labelForbackuppath";
            labelForbackuppath.Size = new Size(80, 17);
            labelForbackuppath.TabIndex = 2;
            labelForbackuppath.Text = "备份存放目录";
            // 
            // backupPathbox
            // 
            backupPathbox.BorderStyle = BorderStyle.FixedSingle;
            backupPathbox.Location = new Point(96, 164);
            backupPathbox.Name = "backupPathbox";
            backupPathbox.ReadOnly = true;
            backupPathbox.Size = new Size(203, 23);
            backupPathbox.TabIndex = 1;
            // 
            // labelForbackupsecond
            // 
            labelForbackupsecond.AutoSize = true;
            labelForbackupsecond.Location = new Point(6, 45);
            labelForbackupsecond.Name = "labelForbackupsecond";
            labelForbackupsecond.Size = new Size(92, 17);
            labelForbackupsecond.TabIndex = 1;
            labelForbackupsecond.Text = "存档周期（秒）";
            // 
            // memProcessbar
            // 
            memProcessbar.Location = new Point(16, 33);
            memProcessbar.Margin = new Padding(2);
            memProcessbar.Name = "memProcessbar";
            memProcessbar.Size = new Size(339, 24);
            memProcessbar.Style = ProgressBarStyle.Continuous;
            memProcessbar.TabIndex = 7;
            // 
            // memOutput
            // 
            memOutput.AutoSize = true;
            memOutput.Location = new Point(106, 67);
            memOutput.Margin = new Padding(2, 0, 2, 0);
            memOutput.Name = "memOutput";
            memOutput.Size = new Size(26, 17);
            memOutput.TabIndex = 8;
            memOutput.Text = "0%";
            memOutput.Click += memOutput_Click;
            // 
            // checkBox_geplayers
            // 
            checkBox_geplayers.AutoSize = true;
            checkBox_geplayers.Location = new Point(17, 154);
            checkBox_geplayers.Margin = new Padding(2);
            checkBox_geplayers.Name = "checkBox_geplayers";
            checkBox_geplayers.Size = new Size(123, 21);
            checkBox_geplayers.TabIndex = 10;
            checkBox_geplayers.Text = "自动获取在线玩家";
            checkBox_geplayers.UseVisualStyleBackColor = true;
            checkBox_geplayers.CheckedChanged += checkBox_geplayers_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelForwebhook);
            groupBox1.Controls.Add(labelForgetplayers);
            groupBox1.Controls.Add(labelForsave);
            groupBox1.Controls.Add(labelForstart);
            groupBox1.Controls.Add(labelForreboot);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(memOutput);
            groupBox1.Controls.Add(memProcessbar);
            groupBox1.Location = new Point(406, 2);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(398, 140);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "监控";
            // 
            // labelForwebhook
            // 
            labelForwebhook.AccessibleRole = AccessibleRole.None;
            labelForwebhook.AutoSize = true;
            labelForwebhook.Location = new Point(216, 104);
            labelForwebhook.Name = "labelForwebhook";
            labelForwebhook.Size = new Size(89, 17);
            labelForwebhook.TabIndex = 12;
            labelForwebhook.Text = "Webhook：关";
            // 
            // labelForgetplayers
            // 
            labelForgetplayers.AccessibleRole = AccessibleRole.None;
            labelForgetplayers.AutoSize = true;
            labelForgetplayers.Location = new Point(106, 104);
            labelForgetplayers.Name = "labelForgetplayers";
            labelForgetplayers.Size = new Size(104, 17);
            labelForgetplayers.TabIndex = 12;
            labelForgetplayers.Text = "自动获取玩家：关";
            // 
            // labelForsave
            // 
            labelForsave.AccessibleRole = AccessibleRole.None;
            labelForsave.AutoSize = true;
            labelForsave.Location = new Point(106, 84);
            labelForsave.Name = "labelForsave";
            labelForsave.Size = new Size(80, 17);
            labelForsave.TabIndex = 12;
            labelForsave.Text = "自动存档：关";
            // 
            // labelForstart
            // 
            labelForstart.AccessibleRole = AccessibleRole.None;
            labelForstart.AutoSize = true;
            labelForstart.Location = new Point(16, 104);
            labelForstart.Name = "labelForstart";
            labelForstart.Size = new Size(80, 17);
            labelForstart.TabIndex = 12;
            labelForstart.Text = "自动启动：关";
            // 
            // labelForreboot
            // 
            labelForreboot.AccessibleRole = AccessibleRole.None;
            labelForreboot.AutoSize = true;
            labelForreboot.Location = new Point(16, 84);
            labelForreboot.Name = "labelForreboot";
            labelForreboot.Size = new Size(80, 17);
            labelForreboot.TabIndex = 12;
            labelForreboot.Text = "自动关服：关";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 67);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(92, 17);
            label1.TabIndex = 9;
            label1.Text = "系统内存占用：";
            // 
            // arguments
            // 
            arguments.BorderStyle = BorderStyle.FixedSingle;
            arguments.Enabled = false;
            arguments.Location = new Point(215, 40);
            arguments.Margin = new Padding(2);
            arguments.Name = "arguments";
            arguments.Size = new Size(129, 23);
            arguments.TabIndex = 11;
            arguments.Text = "EpicApp=PalServer";
            // 
            // checkBox_args
            // 
            checkBox_args.AutoSize = true;
            checkBox_args.Location = new Point(99, 42);
            checkBox_args.Margin = new Padding(2);
            checkBox_args.Name = "checkBox_args";
            checkBox_args.Size = new Size(123, 21);
            checkBox_args.TabIndex = 10;
            checkBox_args.Text = "参数启动（可选）";
            checkBox_args.UseVisualStyleBackColor = true;
            checkBox_args.CheckedChanged += checkBox_args_CheckedChanged;
            // 
            // settingButton
            // 
            settingButton.Location = new Point(16, 28);
            settingButton.Name = "settingButton";
            settingButton.Size = new Size(135, 23);
            settingButton.TabIndex = 12;
            settingButton.Text = "服务端配置文件(测试)";
            settingButton.UseVisualStyleBackColor = true;
            settingButton.Click += settingButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 80);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(80, 68);
            label3.TabIndex = 8;
            label3.Text = "游戏存档目录\r\n（根据服务端\r\n位置自动生成\r\n可手动选择）";
            // 
            // selectCustombutton
            // 
            selectCustombutton.Location = new Point(305, 78);
            selectCustombutton.Margin = new Padding(2);
            selectCustombutton.Name = "selectCustombutton";
            selectCustombutton.Size = new Size(40, 23);
            selectCustombutton.TabIndex = 7;
            selectCustombutton.Text = "选择";
            selectCustombutton.UseVisualStyleBackColor = true;
            selectCustombutton.Click += selectCustombutton_Click;
            // 
            // gamedataBox
            // 
            gamedataBox.BorderStyle = BorderStyle.FixedSingle;
            gamedataBox.Location = new Point(96, 79);
            gamedataBox.Margin = new Padding(2);
            gamedataBox.Name = "gamedataBox";
            gamedataBox.ReadOnly = true;
            gamedataBox.Size = new Size(203, 23);
            gamedataBox.TabIndex = 6;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(outPutbox);
            groupBox3.Location = new Point(408, 146);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(394, 180);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Console";
            // 
            // playersView
            // 
            playersView.Location = new Point(5, 5);
            playersView.Margin = new Padding(2);
            playersView.Name = "playersView";
            playersView.Size = new Size(416, 124);
            playersView.TabIndex = 14;
            playersView.UseCompatibleStateImageBehavior = false;
            playersView.ItemSelectionChanged += playersView_ItemSelectionChanged;
            // 
            // playersCounterLabel
            // 
            playersCounterLabel.AutoSize = true;
            playersCounterLabel.Location = new Point(150, 155);
            playersCounterLabel.Margin = new Padding(2, 0, 2, 0);
            playersCounterLabel.Name = "playersCounterLabel";
            playersCounterLabel.Size = new Size(92, 17);
            playersCounterLabel.TabIndex = 15;
            playersCounterLabel.Text = "当前在线：未知";
            // 
            // UIDBox
            // 
            UIDBox.BorderStyle = BorderStyle.FixedSingle;
            UIDBox.Location = new Point(61, 188);
            UIDBox.Margin = new Padding(2);
            UIDBox.Name = "UIDBox";
            UIDBox.Size = new Size(176, 23);
            UIDBox.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 189);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(42, 17);
            label5.TabIndex = 24;
            label5.Text = "UID：";
            // 
            // banbutton
            // 
            banbutton.Location = new Point(331, 187);
            banbutton.Margin = new Padding(2);
            banbutton.Name = "banbutton";
            banbutton.Size = new Size(71, 23);
            banbutton.TabIndex = 23;
            banbutton.Text = "Ban";
            banbutton.UseVisualStyleBackColor = true;
            banbutton.Click += banbutton_Click;
            // 
            // kickbutton
            // 
            kickbutton.Location = new Point(250, 187);
            kickbutton.Margin = new Padding(2);
            kickbutton.Name = "kickbutton";
            kickbutton.Size = new Size(71, 23);
            kickbutton.TabIndex = 23;
            kickbutton.Text = "Kick";
            kickbutton.UseVisualStyleBackColor = true;
            kickbutton.Click += kickbutton_Click;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(293, 263);
            versionLabel.Margin = new Padding(2, 0, 2, 0);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(104, 17);
            versionLabel.TabIndex = 21;
            versionLabel.Text = "服务端版本：未知";
            // 
            // button4
            // 
            button4.Location = new Point(198, 260);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(86, 23);
            button4.TabIndex = 20;
            button4.Text = "获取服本号";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(107, 260);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(86, 23);
            button3.TabIndex = 20;
            button3.Text = "10s安全关服";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(17, 260);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(86, 23);
            button2.TabIndex = 19;
            button2.Text = "服务端存档";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(331, 223);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(71, 23);
            button1.TabIndex = 18;
            button1.Text = "发送";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(61, 223);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(260, 23);
            textBox1.TabIndex = 17;
            textBox1.Text = "This_is_a_rcon_message.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 225);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 16;
            label2.Text = "广播：";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(verisionLabel);
            groupBox5.Controls.Add(linkLabel1);
            groupBox5.Location = new Point(408, 330);
            groupBox5.Margin = new Padding(2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(2);
            groupBox5.Size = new Size(398, 61);
            groupBox5.TabIndex = 17;
            groupBox5.TabStop = false;
            groupBox5.Text = "Version";
            // 
            // verisionLabel
            // 
            verisionLabel.AutoSize = true;
            verisionLabel.Location = new Point(16, 30);
            verisionLabel.Margin = new Padding(2, 0, 2, 0);
            verisionLabel.Name = "verisionLabel";
            verisionLabel.Size = new Size(101, 17);
            verisionLabel.TabIndex = 1;
            verisionLabel.Text = "当前版本：v2.0.0";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(142, 30);
            linkLabel1.Margin = new Padding(2, 0, 2, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(60, 17);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = " By Kiros";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(settingButton);
            groupBox6.Location = new Point(6, 122);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(377, 65);
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
            groupBox7.Location = new Point(5, 5);
            groupBox7.Margin = new Padding(2);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(2);
            groupBox7.Size = new Size(423, 103);
            groupBox7.TabIndex = 19;
            groupBox7.TabStop = false;
            groupBox7.Text = "Webhook";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 69);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(37, 17);
            label4.TabIndex = 3;
            label4.Text = "Url：";
            // 
            // testWebhookbutton
            // 
            testWebhookbutton.Enabled = false;
            testWebhookbutton.Location = new Point(330, 64);
            testWebhookbutton.Margin = new Padding(2);
            testWebhookbutton.Name = "testWebhookbutton";
            testWebhookbutton.Size = new Size(71, 24);
            testWebhookbutton.TabIndex = 2;
            testWebhookbutton.Text = "测试";
            testWebhookbutton.UseVisualStyleBackColor = true;
            testWebhookbutton.Click += testWebhookbutton_Click;
            // 
            // webhookBox
            // 
            webhookBox.BorderStyle = BorderStyle.FixedSingle;
            webhookBox.Enabled = false;
            webhookBox.Location = new Point(57, 67);
            webhookBox.Margin = new Padding(2);
            webhookBox.Name = "webhookBox";
            webhookBox.Size = new Size(270, 23);
            webhookBox.TabIndex = 1;
            // 
            // checkBox_webhook
            // 
            checkBox_webhook.AutoSize = true;
            checkBox_webhook.Location = new Point(16, 33);
            checkBox_webhook.Margin = new Padding(2);
            checkBox_webhook.Name = "checkBox_webhook";
            checkBox_webhook.Size = new Size(132, 21);
            checkBox_webhook.TabIndex = 0;
            checkBox_webhook.Text = "启用Webhook推送";
            checkBox_webhook.UseVisualStyleBackColor = true;
            checkBox_webhook.CheckedChanged += checkBox_webhook_CheckedChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(398, 389);
            tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(checkBox_Noti);
            tabPage1.Controls.Add(arguments);
            tabPage1.Controls.Add(checkBox_reboot);
            tabPage1.Controls.Add(checkBox_args);
            tabPage1.Controls.Add(labelFotmemtarget);
            tabPage1.Controls.Add(labelForchecksecond);
            tabPage1.Controls.Add(selectCmdbutton);
            tabPage1.Controls.Add(labelForcmd);
            tabPage1.Controls.Add(labelForrebootsecond);
            tabPage1.Controls.Add(rebootSecondbox);
            tabPage1.Controls.Add(passWordbox);
            tabPage1.Controls.Add(checkBox_mem);
            tabPage1.Controls.Add(memTargetbox);
            tabPage1.Controls.Add(labelForpassword);
            tabPage1.Controls.Add(cmdbox);
            tabPage1.Controls.Add(labelForrconport);
            tabPage1.Controls.Add(checkBox_startprocess);
            tabPage1.Controls.Add(rconPortbox);
            tabPage1.Controls.Add(checkSecondbox);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(390, 359);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(checkBox_save);
            tabPage2.Controls.Add(selectCustombutton);
            tabPage2.Controls.Add(backupPathbox);
            tabPage2.Controls.Add(gamedataBox);
            tabPage2.Controls.Add(labelForbackuppath);
            tabPage2.Controls.Add(selectBackuppathButton);
            tabPage2.Controls.Add(labelForbackupsecond);
            tabPage2.Controls.Add(backupSecondsbox);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(390, 359);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(UIDBox);
            tabPage3.Controls.Add(playersView);
            tabPage3.Controls.Add(label5);
            tabPage3.Controls.Add(checkBox_geplayers);
            tabPage3.Controls.Add(banbutton);
            tabPage3.Controls.Add(playersCounterLabel);
            tabPage3.Controls.Add(kickbutton);
            tabPage3.Controls.Add(label2);
            tabPage3.Controls.Add(versionLabel);
            tabPage3.Controls.Add(textBox1);
            tabPage3.Controls.Add(button4);
            tabPage3.Controls.Add(button1);
            tabPage3.Controls.Add(button3);
            tabPage3.Controls.Add(button2);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(390, 359);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(groupBox7);
            tabPage4.Controls.Add(groupBox6);
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(390, 359);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // checkBox_Noti
            // 
            checkBox_Noti.AutoSize = true;
            checkBox_Noti.Location = new Point(254, 7);
            checkBox_Noti.Name = "checkBox_Noti";
            checkBox_Noti.Size = new Size(87, 21);
            checkBox_Noti.TabIndex = 12;
            checkBox_Noti.Text = "任务栏通知";
            checkBox_Noti.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(809, 402);
            Controls.Add(tabControl1);
            Controls.Add(groupBox5);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Palworld Server Protector";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)rebootSecondbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkSecondbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)memTargetbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)backupSecondsbox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
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
        private GroupBox groupBox3;
        private ListView playersView;
        private Label playersCounterLabel;
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
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label labelForreboot;
        private Label labelForgetplayers;
        private Label labelForsave;
        private Label labelForstart;
        private Label labelForwebhook;
        private NotifyIcon notifyIcon1;
        private CheckBox checkBox_Noti;
    }
}
