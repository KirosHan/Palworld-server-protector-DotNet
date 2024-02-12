using System.Drawing;
using System.Windows.Forms;

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
			labelForPid = new Label();
			labelForpidText = new Label();
			labelForprogram = new Label();
			label12 = new Label();
			label11 = new Label();
			label10 = new Label();
			label9 = new Label();
			label8 = new Label();
			label7 = new Label();
			label6 = new Label();
			labelForwebhook = new Label();
			labelForgetplayers = new Label();
			labelForsave = new Label();
			labelForstart = new Label();
			labelForreboot = new Label();
			label1 = new Label();
			comboBox_id = new ComboBox();
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
			linkLabel2 = new LinkLabel();
			verisionLabel = new Label();
			linkLabel1 = new LinkLabel();
			groupBox6 = new GroupBox();
			groupBox7 = new GroupBox();
			checkBox_playerStatus = new CheckBox();
			checkBox_web_getplayers = new CheckBox();
			checkbox_web_reboot = new CheckBox();
			checkBox_web_startprocess = new CheckBox();
			checkBox_web_save = new CheckBox();
			label4 = new Label();
			testWebhookbutton = new Button();
			webhookBox = new TextBox();
			checkBox_webhook = new CheckBox();
			tabControl1 = new TabControl();
			tabPage1 = new TabPage();
			tabPage2 = new TabPage();
			tabPage3 = new TabPage();
			labelForservername = new Label();
			tabPage4 = new TabPage();
			groupBox4 = new GroupBox();
			groupBox2 = new GroupBox();
			checkBox_Noti = new CheckBox();
			tabPage5 = new TabPage();
			notifyIcon1 = new NotifyIcon(components);
			contextMenuStrip1 = new ContextMenuStrip(components);
			toolStripMenuItem1 = new ToolStripMenuItem();
			columnHeader_name = new ColumnHeader();
			columnHeader_uid = new ColumnHeader();
			columnHeader_steamId = new ColumnHeader();
			button5 = new Button();
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
			groupBox4.SuspendLayout();
			groupBox2.SuspendLayout();
			tabPage5.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// checkBox_reboot
			// 
			resources.ApplyResources(checkBox_reboot, "checkBox_reboot");
			checkBox_reboot.Name = "checkBox_reboot";
			checkBox_reboot.UseVisualStyleBackColor = true;
			checkBox_reboot.CheckedChanged += checkBox_reboot_CheckedChanged;
			// 
			// checkBox_save
			// 
			resources.ApplyResources(checkBox_save, "checkBox_save");
			checkBox_save.Name = "checkBox_save";
			checkBox_save.UseVisualStyleBackColor = true;
			checkBox_save.CheckedChanged += checkBox_save_CheckedChanged;
			// 
			// checkBox_mem
			// 
			resources.ApplyResources(checkBox_mem, "checkBox_mem");
			checkBox_mem.Name = "checkBox_mem";
			checkBox_mem.UseVisualStyleBackColor = true;
			// 
			// outPutbox
			// 
			resources.ApplyResources(outPutbox, "outPutbox");
			outPutbox.BorderStyle = BorderStyle.None;
			outPutbox.Name = "outPutbox";
			outPutbox.ReadOnly = true;
			// 
			// selectCmdbutton
			// 
			resources.ApplyResources(selectCmdbutton, "selectCmdbutton");
			selectCmdbutton.Name = "selectCmdbutton";
			selectCmdbutton.UseVisualStyleBackColor = true;
			selectCmdbutton.Click += selectCmdbutton_Click;
			// 
			// rebootSecondbox
			// 
			resources.ApplyResources(rebootSecondbox, "rebootSecondbox");
			rebootSecondbox.BorderStyle = BorderStyle.FixedSingle;
			rebootSecondbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
			rebootSecondbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			rebootSecondbox.Name = "rebootSecondbox";
			rebootSecondbox.Value = new decimal(new int[] { 10, 0, 0, 0 });
			rebootSecondbox.ValueChanged += rebootSecondbox_ValueChanged;
			// 
			// labelForpassword
			// 
			resources.ApplyResources(labelForpassword, "labelForpassword");
			labelForpassword.Name = "labelForpassword";
			// 
			// checkSecondbox
			// 
			resources.ApplyResources(checkSecondbox, "checkSecondbox");
			checkSecondbox.BorderStyle = BorderStyle.FixedSingle;
			checkSecondbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
			checkSecondbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			checkSecondbox.Name = "checkSecondbox";
			checkSecondbox.Value = new decimal(new int[] { 20, 0, 0, 0 });
			checkSecondbox.ValueChanged += checkSecondbox_ValueChanged;
			checkSecondbox.KeyUp += checkSecondbox_KeyUp;
			// 
			// labelForrconport
			// 
			resources.ApplyResources(labelForrconport, "labelForrconport");
			labelForrconport.Name = "labelForrconport";
			// 
			// memTargetbox
			// 
			resources.ApplyResources(memTargetbox, "memTargetbox");
			memTargetbox.BorderStyle = BorderStyle.FixedSingle;
			memTargetbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			memTargetbox.Name = "memTargetbox";
			memTargetbox.Value = new decimal(new int[] { 90, 0, 0, 0 });
			memTargetbox.ValueChanged += memTargetbox_ValueChanged;
			// 
			// labelForrebootsecond
			// 
			resources.ApplyResources(labelForrebootsecond, "labelForrebootsecond");
			labelForrebootsecond.Name = "labelForrebootsecond";
			// 
			// labelForchecksecond
			// 
			resources.ApplyResources(labelForchecksecond, "labelForchecksecond");
			labelForchecksecond.Name = "labelForchecksecond";
			// 
			// labelFotmemtarget
			// 
			resources.ApplyResources(labelFotmemtarget, "labelFotmemtarget");
			labelFotmemtarget.Name = "labelFotmemtarget";
			// 
			// labelForcmd
			// 
			resources.ApplyResources(labelForcmd, "labelForcmd");
			labelForcmd.Name = "labelForcmd";
			// 
			// passWordbox
			// 
			resources.ApplyResources(passWordbox, "passWordbox");
			passWordbox.BorderStyle = BorderStyle.FixedSingle;
			passWordbox.Name = "passWordbox";
			// 
			// cmdbox
			// 
			resources.ApplyResources(cmdbox, "cmdbox");
			cmdbox.BorderStyle = BorderStyle.FixedSingle;
			cmdbox.Name = "cmdbox";
			cmdbox.ReadOnly = true;
			// 
			// rconPortbox
			// 
			resources.ApplyResources(rconPortbox, "rconPortbox");
			rconPortbox.BorderStyle = BorderStyle.FixedSingle;
			rconPortbox.Name = "rconPortbox";
			rconPortbox.KeyPress += rconPortbox_KeyPress;
			// 
			// checkBox_startprocess
			// 
			resources.ApplyResources(checkBox_startprocess, "checkBox_startprocess");
			checkBox_startprocess.Name = "checkBox_startprocess";
			checkBox_startprocess.UseVisualStyleBackColor = true;
			checkBox_startprocess.CheckedChanged += checkBox_startprocess_CheckedChanged;
			// 
			// selectBackuppathButton
			// 
			resources.ApplyResources(selectBackuppathButton, "selectBackuppathButton");
			selectBackuppathButton.Name = "selectBackuppathButton";
			selectBackuppathButton.UseVisualStyleBackColor = true;
			selectBackuppathButton.Click += selectBackuppathButton_Click;
			// 
			// backupSecondsbox
			// 
			resources.ApplyResources(backupSecondsbox, "backupSecondsbox");
			backupSecondsbox.BorderStyle = BorderStyle.FixedSingle;
			backupSecondsbox.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
			backupSecondsbox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			backupSecondsbox.Name = "backupSecondsbox";
			backupSecondsbox.Value = new decimal(new int[] { 1800, 0, 0, 0 });
			backupSecondsbox.ValueChanged += backupSecondsbox_ValueChanged;
			backupSecondsbox.KeyUp += backupSecondsbox_KeyUp;
			// 
			// labelForbackuppath
			// 
			resources.ApplyResources(labelForbackuppath, "labelForbackuppath");
			labelForbackuppath.Name = "labelForbackuppath";
			// 
			// backupPathbox
			// 
			resources.ApplyResources(backupPathbox, "backupPathbox");
			backupPathbox.BorderStyle = BorderStyle.FixedSingle;
			backupPathbox.Name = "backupPathbox";
			backupPathbox.ReadOnly = true;
			// 
			// labelForbackupsecond
			// 
			resources.ApplyResources(labelForbackupsecond, "labelForbackupsecond");
			labelForbackupsecond.Name = "labelForbackupsecond";
			// 
			// memProcessbar
			// 
			resources.ApplyResources(memProcessbar, "memProcessbar");
			memProcessbar.Name = "memProcessbar";
			memProcessbar.Style = ProgressBarStyle.Continuous;
			// 
			// memOutput
			// 
			resources.ApplyResources(memOutput, "memOutput");
			memOutput.Name = "memOutput";
			memOutput.Click += memOutput_Click;
			// 
			// checkBox_geplayers
			// 
			resources.ApplyResources(checkBox_geplayers, "checkBox_geplayers");
			checkBox_geplayers.Name = "checkBox_geplayers";
			checkBox_geplayers.UseVisualStyleBackColor = true;
			checkBox_geplayers.CheckedChanged += checkBox_geplayers_CheckedChanged;
			// 
			// groupBox1
			// 
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Controls.Add(labelForPid);
			groupBox1.Controls.Add(labelForpidText);
			groupBox1.Controls.Add(labelForprogram);
			groupBox1.Controls.Add(label12);
			groupBox1.Controls.Add(label11);
			groupBox1.Controls.Add(label10);
			groupBox1.Controls.Add(label9);
			groupBox1.Controls.Add(label8);
			groupBox1.Controls.Add(label7);
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(labelForwebhook);
			groupBox1.Controls.Add(labelForgetplayers);
			groupBox1.Controls.Add(labelForsave);
			groupBox1.Controls.Add(labelForstart);
			groupBox1.Controls.Add(labelForreboot);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(memOutput);
			groupBox1.Controls.Add(memProcessbar);
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			// 
			// labelForPid
			// 
			resources.ApplyResources(labelForPid, "labelForPid");
			labelForPid.Name = "labelForPid";
			// 
			// labelForpidText
			// 
			resources.ApplyResources(labelForpidText, "labelForpidText");
			labelForpidText.Name = "labelForpidText";
			// 
			// labelForprogram
			// 
			resources.ApplyResources(labelForprogram, "labelForprogram");
			labelForprogram.Name = "labelForprogram";
			// 
			// label12
			// 
			resources.ApplyResources(label12, "label12");
			label12.Name = "label12";
			// 
			// label11
			// 
			resources.ApplyResources(label11, "label11");
			label11.Name = "label11";
			// 
			// label10
			// 
			resources.ApplyResources(label10, "label10");
			label10.Name = "label10";
			// 
			// label9
			// 
			resources.ApplyResources(label9, "label9");
			label9.Name = "label9";
			// 
			// label8
			// 
			resources.ApplyResources(label8, "label8");
			label8.Name = "label8";
			// 
			// label7
			// 
			resources.ApplyResources(label7, "label7");
			label7.Name = "label7";
			// 
			// label6
			// 
			resources.ApplyResources(label6, "label6");
			label6.Name = "label6";
			// 
			// labelForwebhook
			// 
			resources.ApplyResources(labelForwebhook, "labelForwebhook");
			labelForwebhook.AccessibleRole = AccessibleRole.None;
			labelForwebhook.Name = "labelForwebhook";
			// 
			// labelForgetplayers
			// 
			resources.ApplyResources(labelForgetplayers, "labelForgetplayers");
			labelForgetplayers.AccessibleRole = AccessibleRole.None;
			labelForgetplayers.Name = "labelForgetplayers";
			// 
			// labelForsave
			// 
			resources.ApplyResources(labelForsave, "labelForsave");
			labelForsave.AccessibleRole = AccessibleRole.None;
			labelForsave.Name = "labelForsave";
			// 
			// labelForstart
			// 
			resources.ApplyResources(labelForstart, "labelForstart");
			labelForstart.AccessibleRole = AccessibleRole.None;
			labelForstart.Name = "labelForstart";
			// 
			// labelForreboot
			// 
			resources.ApplyResources(labelForreboot, "labelForreboot");
			labelForreboot.AccessibleRole = AccessibleRole.None;
			labelForreboot.Name = "labelForreboot";
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// comboBox_id
			// 
			resources.ApplyResources(comboBox_id, "comboBox_id");
			comboBox_id.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox_id.FormattingEnabled = true;
			comboBox_id.Items.AddRange(new object[] { resources.GetString("comboBox_id.Items"), resources.GetString("comboBox_id.Items1"), resources.GetString("comboBox_id.Items2") });
			comboBox_id.Name = "comboBox_id";
			// 
			// arguments
			// 
			resources.ApplyResources(arguments, "arguments");
			arguments.BorderStyle = BorderStyle.FixedSingle;
			arguments.Name = "arguments";
			// 
			// checkBox_args
			// 
			resources.ApplyResources(checkBox_args, "checkBox_args");
			checkBox_args.Name = "checkBox_args";
			checkBox_args.UseVisualStyleBackColor = true;
			checkBox_args.CheckedChanged += checkBox_args_CheckedChanged;
			// 
			// settingButton
			// 
			resources.ApplyResources(settingButton, "settingButton");
			settingButton.Name = "settingButton";
			settingButton.UseVisualStyleBackColor = true;
			settingButton.Click += settingButton_Click;
			// 
			// label3
			// 
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			// 
			// selectCustombutton
			// 
			resources.ApplyResources(selectCustombutton, "selectCustombutton");
			selectCustombutton.Name = "selectCustombutton";
			selectCustombutton.UseVisualStyleBackColor = true;
			selectCustombutton.Click += selectCustombutton_Click;
			// 
			// gamedataBox
			// 
			resources.ApplyResources(gamedataBox, "gamedataBox");
			gamedataBox.BorderStyle = BorderStyle.FixedSingle;
			gamedataBox.Name = "gamedataBox";
			gamedataBox.ReadOnly = true;
			// 
			// groupBox3
			// 
			resources.ApplyResources(groupBox3, "groupBox3");
			groupBox3.Controls.Add(outPutbox);
			groupBox3.Name = "groupBox3";
			groupBox3.TabStop = false;
			// 
			// playersView
			// 
			resources.ApplyResources(playersView, "playersView");
			playersView.Name = "playersView";
			playersView.UseCompatibleStateImageBehavior = false;
			playersView.View = View.Details;
			playersView.ItemSelectionChanged += playersView_ItemSelectionChanged;
			// 
			// playersCounterLabel
			// 
			resources.ApplyResources(playersCounterLabel, "playersCounterLabel");
			playersCounterLabel.Name = "playersCounterLabel";
			// 
			// UIDBox
			// 
			resources.ApplyResources(UIDBox, "UIDBox");
			UIDBox.BorderStyle = BorderStyle.FixedSingle;
			UIDBox.Name = "UIDBox";
			// 
			// banbutton
			// 
			resources.ApplyResources(banbutton, "banbutton");
			banbutton.Name = "banbutton";
			banbutton.UseVisualStyleBackColor = true;
			banbutton.Click += banbutton_Click;
			// 
			// kickbutton
			// 
			resources.ApplyResources(kickbutton, "kickbutton");
			kickbutton.Name = "kickbutton";
			kickbutton.UseVisualStyleBackColor = true;
			kickbutton.Click += kickbutton_Click;
			// 
			// versionLabel
			// 
			resources.ApplyResources(versionLabel, "versionLabel");
			versionLabel.Name = "versionLabel";
			// 
			// button4
			// 
			resources.ApplyResources(button4, "button4");
			button4.Name = "button4";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// button3
			// 
			resources.ApplyResources(button3, "button3");
			button3.Name = "button3";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button2
			// 
			resources.ApplyResources(button2, "button2");
			button2.Name = "button2";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button1
			// 
			resources.ApplyResources(button1, "button1");
			button1.Name = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// textBox1
			// 
			resources.ApplyResources(textBox1, "textBox1");
			textBox1.BorderStyle = BorderStyle.FixedSingle;
			textBox1.Name = "textBox1";
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			// 
			// groupBox5
			// 
			resources.ApplyResources(groupBox5, "groupBox5");
			groupBox5.Controls.Add(linkLabel2);
			groupBox5.Controls.Add(verisionLabel);
			groupBox5.Controls.Add(linkLabel1);
			groupBox5.Name = "groupBox5";
			groupBox5.TabStop = false;
			// 
			// linkLabel2
			// 
			resources.ApplyResources(linkLabel2, "linkLabel2");
			linkLabel2.Name = "linkLabel2";
			linkLabel2.TabStop = true;
			linkLabel2.LinkClicked += linkLabel2_LinkClicked;
			// 
			// verisionLabel
			// 
			resources.ApplyResources(verisionLabel, "verisionLabel");
			verisionLabel.Name = "verisionLabel";
			// 
			// linkLabel1
			// 
			resources.ApplyResources(linkLabel1, "linkLabel1");
			linkLabel1.Name = "linkLabel1";
			linkLabel1.TabStop = true;
			linkLabel1.LinkClicked += linkLabel1_LinkClicked;
			// 
			// groupBox6
			// 
			resources.ApplyResources(groupBox6, "groupBox6");
			groupBox6.Controls.Add(settingButton);
			groupBox6.Name = "groupBox6";
			groupBox6.TabStop = false;
			// 
			// groupBox7
			// 
			resources.ApplyResources(groupBox7, "groupBox7");
			groupBox7.Controls.Add(checkBox_playerStatus);
			groupBox7.Controls.Add(checkBox_web_getplayers);
			groupBox7.Controls.Add(checkbox_web_reboot);
			groupBox7.Controls.Add(checkBox_web_startprocess);
			groupBox7.Controls.Add(checkBox_web_save);
			groupBox7.Controls.Add(label4);
			groupBox7.Controls.Add(testWebhookbutton);
			groupBox7.Controls.Add(webhookBox);
			groupBox7.Controls.Add(checkBox_webhook);
			groupBox7.Name = "groupBox7";
			groupBox7.TabStop = false;
			// 
			// checkBox_playerStatus
			// 
			resources.ApplyResources(checkBox_playerStatus, "checkBox_playerStatus");
			checkBox_playerStatus.Name = "checkBox_playerStatus";
			checkBox_playerStatus.UseVisualStyleBackColor = true;
			// 
			// checkBox_web_getplayers
			// 
			resources.ApplyResources(checkBox_web_getplayers, "checkBox_web_getplayers");
			checkBox_web_getplayers.Name = "checkBox_web_getplayers";
			checkBox_web_getplayers.UseVisualStyleBackColor = true;
			// 
			// checkbox_web_reboot
			// 
			resources.ApplyResources(checkbox_web_reboot, "checkbox_web_reboot");
			checkbox_web_reboot.Name = "checkbox_web_reboot";
			checkbox_web_reboot.UseVisualStyleBackColor = true;
			// 
			// checkBox_web_startprocess
			// 
			resources.ApplyResources(checkBox_web_startprocess, "checkBox_web_startprocess");
			checkBox_web_startprocess.Name = "checkBox_web_startprocess";
			checkBox_web_startprocess.UseVisualStyleBackColor = true;
			// 
			// checkBox_web_save
			// 
			resources.ApplyResources(checkBox_web_save, "checkBox_web_save");
			checkBox_web_save.Name = "checkBox_web_save";
			checkBox_web_save.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			// 
			// testWebhookbutton
			// 
			resources.ApplyResources(testWebhookbutton, "testWebhookbutton");
			testWebhookbutton.Name = "testWebhookbutton";
			testWebhookbutton.UseVisualStyleBackColor = true;
			testWebhookbutton.Click += testWebhookbutton_Click;
			// 
			// webhookBox
			// 
			resources.ApplyResources(webhookBox, "webhookBox");
			webhookBox.BorderStyle = BorderStyle.FixedSingle;
			webhookBox.Name = "webhookBox";
			// 
			// checkBox_webhook
			// 
			resources.ApplyResources(checkBox_webhook, "checkBox_webhook");
			checkBox_webhook.Name = "checkBox_webhook";
			checkBox_webhook.UseVisualStyleBackColor = true;
			checkBox_webhook.CheckedChanged += checkBox_webhook_CheckedChanged;
			// 
			// tabControl1
			// 
			resources.ApplyResources(tabControl1, "tabControl1");
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage2);
			tabControl1.Controls.Add(tabPage3);
			tabControl1.Controls.Add(tabPage4);
			tabControl1.Controls.Add(tabPage5);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			resources.ApplyResources(tabPage1, "tabPage1");
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
			tabPage1.Controls.Add(memTargetbox);
			tabPage1.Controls.Add(labelForpassword);
			tabPage1.Controls.Add(cmdbox);
			tabPage1.Controls.Add(labelForrconport);
			tabPage1.Controls.Add(checkBox_startprocess);
			tabPage1.Controls.Add(rconPortbox);
			tabPage1.Controls.Add(checkSecondbox);
			tabPage1.Name = "tabPage1";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			resources.ApplyResources(tabPage2, "tabPage2");
			tabPage2.Controls.Add(label3);
			tabPage2.Controls.Add(checkBox_save);
			tabPage2.Controls.Add(selectCustombutton);
			tabPage2.Controls.Add(backupPathbox);
			tabPage2.Controls.Add(gamedataBox);
			tabPage2.Controls.Add(labelForbackuppath);
			tabPage2.Controls.Add(selectBackuppathButton);
			tabPage2.Controls.Add(labelForbackupsecond);
			tabPage2.Controls.Add(backupSecondsbox);
			tabPage2.Name = "tabPage2";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			resources.ApplyResources(tabPage3, "tabPage3");
			tabPage3.Controls.Add(comboBox_id);
			tabPage3.Controls.Add(labelForservername);
			tabPage3.Controls.Add(UIDBox);
			tabPage3.Controls.Add(playersView);
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
			tabPage3.Name = "tabPage3";
			tabPage3.UseVisualStyleBackColor = true;
			// 
			// labelForservername
			// 
			resources.ApplyResources(labelForservername, "labelForservername");
			labelForservername.Name = "labelForservername";
			// 
			// tabPage4
			// 
			resources.ApplyResources(tabPage4, "tabPage4");
			tabPage4.Controls.Add(groupBox4);
			tabPage4.Controls.Add(groupBox2);
			tabPage4.Controls.Add(groupBox7);
			tabPage4.Name = "tabPage4";
			tabPage4.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			resources.ApplyResources(groupBox4, "groupBox4");
			groupBox4.Controls.Add(checkBox_mem);
			groupBox4.Name = "groupBox4";
			groupBox4.TabStop = false;
			// 
			// groupBox2
			// 
			resources.ApplyResources(groupBox2, "groupBox2");
			groupBox2.Controls.Add(checkBox_Noti);
			groupBox2.Name = "groupBox2";
			groupBox2.TabStop = false;
			// 
			// checkBox_Noti
			// 
			resources.ApplyResources(checkBox_Noti, "checkBox_Noti");
			checkBox_Noti.Name = "checkBox_Noti";
			checkBox_Noti.UseVisualStyleBackColor = true;
			// 
			// tabPage5
			// 
			resources.ApplyResources(tabPage5, "tabPage5");
			tabPage5.Controls.Add(groupBox6);
			tabPage5.Name = "tabPage5";
			tabPage5.UseVisualStyleBackColor = true;
			// 
			// notifyIcon1
			// 
			resources.ApplyResources(notifyIcon1, "notifyIcon1");
			notifyIcon1.ContextMenuStrip = contextMenuStrip1;
			notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
			// 
			// contextMenuStrip1
			// 
			resources.ApplyResources(contextMenuStrip1, "contextMenuStrip1");
			contextMenuStrip1.ImageScalingSize = new Size(24, 24);
			contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
			contextMenuStrip1.Name = "contextMenuStrip1";
			// 
			// toolStripMenuItem1
			// 
			resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Click += toolStripMenuItem1_Click;
			// 
			// columnHeader_name
			// 
			resources.ApplyResources(columnHeader_name, "columnHeader_name");
			// 
			// columnHeader_uid
			// 
			resources.ApplyResources(columnHeader_uid, "columnHeader_uid");
			// 
			// columnHeader_steamId
			// 
			resources.ApplyResources(columnHeader_steamId, "columnHeader_steamId");
			// 
			// button5
			// 
			resources.ApplyResources(button5, "button5");
			button5.Name = "button5";
			button5.UseVisualStyleBackColor = true;
			button5.MouseDown += button5_MouseDown;
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(button5);
			Controls.Add(tabControl1);
			Controls.Add(groupBox5);
			Controls.Add(groupBox3);
			Controls.Add(groupBox1);
			Name = "Form1";
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
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
			groupBox4.ResumeLayout(false);
			groupBox4.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			tabPage5.ResumeLayout(false);
			contextMenuStrip1.ResumeLayout(false);
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
        private LinkLabel linkLabel2;
        private TabPage tabPage5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label labelForprogram;
        private Label label12;
        private Label labelForpidText;
        private Label labelForPid;
        private GroupBox groupBox4;
        private GroupBox groupBox2;
        private CheckBox checkBox_web_save;
        private CheckBox checkBox_web_startprocess;
        private CheckBox checkbox_web_reboot;
        private CheckBox checkBox_web_getplayers;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private Label labelForservername;
        private CheckBox checkBox_playerStatus;
        private ColumnHeader columnHeader_name;
        private ColumnHeader columnHeader_uid;
        private ColumnHeader columnHeader_steamId;
        private ComboBox comboBox_id;
        private Button button5;
    }
}
