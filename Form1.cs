namespace Palworld_server_protector_DotNet
{
    using Microsoft.VisualBasic.Devices;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Drawing;
    using static System.Net.WebRequestMethods;

    public partial class Form1 : Form
    {
        private Timer memTimer;
        private Timer saveTimer;
        private Timer getplayerTimer;
        private string cmdPath;
        private string backupPath;
        private string gamedataPath;
        private Int32 memTarget;
        private string rconHost;
        private Int32 rconPort;
        private string rconPassword;
        private Int32 rebootSeconds;


        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            string buildVersion = Application.ProductVersion;
            int endIndex = buildVersion.IndexOf('+'); // 找到版本号中的"+"符号的索引位置
            string version = buildVersion.Substring(0, endIndex); // 使用Substring方法提取从0到endIndex之间的子字符串
            this.Text = $"Palworld Server Protector v{version}";
        }

        private void InitializeTimer()
        {
            memTimer = new Timer();
            memTimer.Interval = 35000; // 设置定时器间隔为5秒
            memTimer.Tick += Timer_Tick;

            saveTimer = new Timer();
            saveTimer.Interval = 35000; // 设置定时器间隔为5秒
            saveTimer.Tick += saveTimer_Tick;

            getplayerTimer = new Timer();
            getplayerTimer.Interval = 3000; // 设置定时器间隔为s秒
            getplayerTimer.Tick += getplayerTimer_Tick;
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            // 获取系统内存使用百分比
            var memoryUsage = Math.Round(GetSystemMemoryUsagePercentage(), 2);
            memProcessbar.Value = (int)memoryUsage;
            memOutput.Text = $"{memoryUsage}%";
            if (checkBox_mem.Checked)
            {
                //OutputMessageAsync($"当前时间：{DateTime.Now}");
                OutputMessageAsync($"内存使用百分比：{memoryUsage}%");
            }



            if (checkBox_reboot.Checked)//自动关服
            {
                if (memoryUsage >= memTarget)
                {
                    try
                    {
                        var isProcessRunning = IsProcessRunning(cmdPath);
                        if (isProcessRunning)
                        {
                            OutputMessageAsync($"内存达到警戒阈值！！！");
                            // 使用rcon向服务端发送指令
                            RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);
                            var info = RconUtils.SendMsg("save");
                            OutputMessageAsync($"{info}");
                            var result = RconUtils.SendMsg($"Shutdown {rebootSeconds} The_server_will_restart_in_{rebootSeconds}_seconds.");

                            OutputMessageAsync($"{result}");
                        }


                    }
                    catch
                    {
                        OutputMessageAsync($"发送指令失败，请检查配置。");

                    }





                }
            }

            if (checkBox_startprocess.Checked)
            { //监控&自启
              // 检查进程是否在运行
                var isProcessRunning = IsProcessRunning(cmdPath);
                OutputMessageAsync($"进程运行状态：{(isProcessRunning ? "运行中" : "未运行")}");
                if (!isProcessRunning)
                {
                    if (!isProcessRunning)
                    {
                        try
                        {
                            OutputMessageAsync($"正在尝试启动服务端...");

                            Process.Start(cmdPath);
                        }
                        catch (Exception ex)
                        {
                            OutputMessageAsync($"服务端启动失败：请检查路径");
                        }
                    }

                }
            }

        }

        private void getplayerTimer_Tick(object sender, EventArgs e) //获取在线玩家
        {
            try
            {
                if (!checkBox_geplayers.Checked)
                {
                    return;
                }

                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);
                var players = RconUtils.ShowPlayers();

                playersCounterLabel.Text = $"当前在线：{players.Count}人";
                // Clear the playersView
                playersView.Items.Clear();

                // Add the players information to the playersView
                foreach (var player in players)
                {
                    var item = new ListViewItem(new[] { player.name, player.uid, player.steam_id });
                    playersView.Items.Add(item);
                }
            }
            catch
            {

            }
        }
        private void CopyGameDataToBackupPath()
        {
            try
            {
                string backupFolderName = $"SaveGames-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.zip";
                string backupFilePath = Path.Combine(backupPath, backupFolderName);

                if (!Directory.Exists(gamedataPath))
                {
                    OutputMessageAsync($"游戏存档路径不存在：{gamedataPath}");
                    return;
                }

                if (!Directory.Exists(backupPath))
                {
                    OutputMessageAsync($"存档备份路径不存在：{backupPath}");
                    return;
                }

                ZipFile.CreateFromDirectory(gamedataPath, backupFilePath);

                OutputMessageAsync($"游戏存档已成功备份");
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"备份存档失败：{ex.Message}");
            }
        }

        private void saveTimer_Tick(object sender, EventArgs e) //存档逻辑
        {
            OutputMessageAsync($"自动存档中...");
            CopyGameDataToBackupPath();

        }

        private float GetSystemMemoryUsagePercentage()
        {
            var info = new ComputerInfo();
            var totalMemory = (float)info.TotalPhysicalMemory;
            var availableMemory = (float)info.AvailablePhysicalMemory;

            var memoryUsage = (totalMemory - availableMemory) / totalMemory;

            return memoryUsage * 100;
        }



        private bool IsProcessRunning(string processPath)
        {
            var processName = Path.GetFileNameWithoutExtension(processPath);
            var processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }


        private void selectCmdbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable Files|*.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                cmdbox.Text = openFileDialog.FileName;
                cmdPath = cmdbox.Text;
                OutputMessageAsync($"已选择服务端路径为：{cmdPath}");
                gamedataPath = Path.Combine(Path.GetDirectoryName(cmdPath), "Pal", "Saved", "SaveGames");
                OutputMessageAsync($"游戏存档路径修改为：{gamedataPath}");
            }
        }

        private async Task OutputMessageAsync(string message)
        {
            await Task.Run(() =>
            {
                outPutbox.Invoke(new Action(() =>
                {
                    outPutbox.AppendText($"[{DateTime.Now.ToString("HH:mm:ss")}] {message}" + Environment.NewLine);

                    if (outPutbox.Lines.Length > 50)
                    {
                        outPutbox.Text = string.Join(Environment.NewLine, outPutbox.Lines.Skip(outPutbox.Lines.Length - 50));
                        outPutbox.SelectionStart = outPutbox.Text.Length;
                        outPutbox.ScrollToCaret();
                    }
                    else
                    {
                        outPutbox.SelectionStart = outPutbox.Text.Length;
                        outPutbox.ScrollToCaret();
                    }
                }));
            });
        }

        private async void startAndstop_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                await Task.CompletedTask;
                var info = RconUtils.SendMsg("info");
                MessageBox.Show(info);
            }
            catch
            {
                MessageBox.Show("连接失败，检查");

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            memTimer.Interval = Convert.ToInt32(checkSecondbox.Value) * 1000;
            memTimer.Start();
            memTarget = Convert.ToInt32(memTargetbox.Value);
            rconHost = "127.0.0.1";
            rconPort = 25575;
            rconPassword = "admin";
            rebootSeconds = 10;
            cmdPath = "";
            backupPath = "";
            playersView.View = View.Details;

            playersView.Columns.Add(new ColumnHeader() { Text = "Name", Width = playersView.Width / 3 });
            playersView.Columns.Add(new ColumnHeader() { Text = "UID", Width = playersView.Width / 3 });
            playersView.Columns.Add(new ColumnHeader() { Text = "Steam ID", Width = playersView.Width / 3 });

            playersView.FullRowSelect = true;
            playersView.MultiSelect = false;
            playersView.HideSelection = false;


            string buildVersion = Application.ProductVersion;
            int endIndex = buildVersion.IndexOf('+'); // 找到版本号中的"+"符号的索引位置
            string version = buildVersion.Substring(0, endIndex); // 使用Substring方法提取从0到endIndex之间的子字符串
            verisionLabel.Text = $"当前版本：{version}";

            OutputMessageAsync($"当前构建版本号：{version}");
            OutputMessageAsync($"本项目开源地址：https://github.com/KirosHan/Palworld-server-protector-DotNet");


            OutputMessageAsync($"请先配置好信息，再勾选功能启动");


        }

        private void checkSecondbox_ValueChanged(object sender, EventArgs e)
        {
            var newSecond = Convert.ToInt32(checkSecondbox.Value);
            memTimer.Interval = newSecond * 1000;
            OutputMessageAsync($"监测周期已调整为：{newSecond}秒");
        }

        private void cmdbox_TextChanged(object sender, EventArgs e)
        {
            cmdPath = cmdbox.Text;
            OutputMessageAsync($"服务端路径修改为：{cmdPath}");
            gamedataPath = Path.Combine(Path.GetDirectoryName(cmdPath), "Pal", "Saved", "SaveGames");
            OutputMessageAsync($"游戏存档路径修改为：{gamedataPath}");


        }

        private void checkBox_startprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdPath == "")
            {
                OutputMessageAsync($"请先设置服务端路径。");
                checkBox_startprocess.Checked = false;

            }
            else if (checkBox_startprocess.Checked)
            {
                OutputMessageAsync($"已开始监控服务端。");
            }
            else
            {
                OutputMessageAsync($"已停止监控服务端。");
            }

        }

        private void checkBox_save_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_save.Checked)
            {
                if (cmdPath == "")
                {
                    OutputMessageAsync($"请先选择服务端路径。");
                    checkBox_save.Checked = false;
                }
                else if (backupPath == "")
                {
                    OutputMessageAsync($"请先选择存档备份路径。");
                    checkBox_save.Checked = false;
                }
                else
                {
                    saveTimer.Interval = Convert.ToInt32(backupSecondsbox.Value) * 1000;
                    saveTimer.Start();
                    OutputMessageAsync($"已启用自动备份存档。");
                }

            }
            else
            {
                saveTimer.Stop();
                OutputMessageAsync($"已停用自动备份存档。");
            }

        }

        private void backupSecondsbox_ValueChanged(object sender, EventArgs e)
        {

            var newBackupSecond = Convert.ToInt32(backupSecondsbox.Value) * 1000;
            saveTimer.Interval = newBackupSecond;
            OutputMessageAsync($"监测周期已调整为：{newBackupSecond}秒");


        }

        private void selectBackuppathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                backupPathbox.Text = folderBrowserDialog.SelectedPath;
                backupPath = backupPathbox.Text;
                OutputMessageAsync($"已选择存档备份路径为：{backupPath}");
            }
        }

        private void backupPathbox_TextChanged(object sender, EventArgs e)
        {
            backupPath = backupPathbox.Text;
            OutputMessageAsync($"已设置存档备份路径为：{backupPath}");
        }

        private void memTargetbox_ValueChanged(object sender, EventArgs e)
        {
            memTarget = Convert.ToInt32(memTargetbox.Value);
            OutputMessageAsync($"内存阈值已调整为：{memTarget}%");

        }

        private void rconPortbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void rconPortbox_TextChanged(object sender, EventArgs e)
        {
            rconPort = Convert.ToInt32(rconPortbox.Text);
            OutputMessageAsync($"Rcon端口已调整为：{rconPort}");

        }

        private void passWordbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            rconPassword = passWordbox.Text;
            //OutputMessageAsync($"密码已设置为：{rconPassword}");
        }

        private void rebootSecondbox_ValueChanged(object sender, EventArgs e)
        {
            rebootSeconds = Convert.ToInt32(rebootSecondbox.Value);
            OutputMessageAsync($"重启延迟已设置为：{rebootSeconds}秒");

        }

        private void checkBox_reboot_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdPath == "")
            {
                checkBox_reboot.Checked = false;
                OutputMessageAsync($"请先选择服务端路径。");
            }

            else if (checkBox_reboot.Checked)
            {
                OutputMessageAsync($"已启用自动关服。");
            }
            else
            {
                OutputMessageAsync($"已停用自动关服。");
            }
        }

        private void checkBox_geplayers_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_geplayers.Checked)
            {
                getplayerTimer.Start();
                OutputMessageAsync($"已启用自动获取在线玩家。");
            }
            else
            {
                getplayerTimer.Stop();
                OutputMessageAsync($"已停用自动获取在线玩家。");
                playersCounterLabel.Text = $"当前在线：未知";
            }
        }

        private void memOutput_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);
            
            
            var info = RconUtils.SendMsg("save");
            OutputMessageAsync($"{info}");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                var result = RconUtils.SendMsg($"Shutdown 10 The_server_will_restart_in_10econds.");

                OutputMessageAsync($"{result}");

            }
            catch (Exception ex)
            {
                OutputMessageAsync($"关服指令发送失败：{ex.Message}");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                var info = RconUtils.SendMsg("info");

                int startIndex = info.IndexOf("[") + 1;
                int endIndex = info.IndexOf("]");
                string version = info.Substring(startIndex, endIndex - startIndex);
                versionLabel.Text = $"服务端版本：{version}";
                OutputMessageAsync($"当前服务端版本：{version}");

            }
            catch (Exception ex)
            {
                OutputMessageAsync($"info指令发送失败：{ex.Message}");
            }
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                textBox1.Text = textBox1.Text.Replace(" ", "_");
                var info = RconUtils.SendMsg($"broadcast {textBox1.Text.Trim()}");

                OutputMessageAsync($"当前服务端版本：{info}");

            }

            catch (Exception ex)
            {
                OutputMessageAsync($"broadcast指令发送失败。{ex.Message}");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = "https://github.com/KirosHan/Palworld-server-protector-DotNet";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
            }
        }

        private void passWordbox_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
