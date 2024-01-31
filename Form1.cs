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
    using System.Runtime.InteropServices;
    using System.Text;
    using static System.Windows.Forms.LinkLabel;
    using System.Net;

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
        private string errorLogname = $"error_{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";
        private string projectUrl = $"https://github.com/KirosHan/Palworld-server-protector-DotNet";
        private Int32 playersTimercounter = 0;
        private Int32 playersTimerthreshold = 600;//每小时触发600次

        private const string ConfigFilePath = "config.ini";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

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
                            if (checkbox_web_reboot.Checked == true) { SendWebhookAsync("内存达到警戒阈值", $"内存使用率：{memoryUsage}%,已尝试关闭服务器。"); }

                            ShowNotification($"内存使用率：{memoryUsage}%,已尝试关闭服务器。");
                        }


                    }
                    catch (Exception ex)
                    {
                        OutputMessageAsync($"发送指令失败，请检查配置。");
                        AppendToErrorLog($"发送指令失败，请检查配置。{ex.Message}");
                        if (checkbox_web_reboot.Checked == true) { SendWebhookAsync("Rcon失败", $"发送关服指令失败，请及时检查。"); }

                        ShowNotification($"发送关服指令失败，请及时检查。");

                    }





                }
            }

            if (checkBox_startprocess.Checked)
            { //监控&自启
              // 检查进程是否在运行
                var isProcessRunning = IsProcessRunning(cmdPath);
                labelForprogram.Text = $"{(isProcessRunning ? "运行中" : "未运行")}";
                OutputMessageAsync($"进程运行状态：{(isProcessRunning ? "运行中" : "未运行")}");
                if (!isProcessRunning)
                {

                    try
                    {

                        Process process;
                        int processId;
                        if (checkBox_args.Checked && arguments.Text != "")
                        {
                            OutputMessageAsync($"正在尝试启动服务端({arguments.Text})...");
                            process = Process.Start(cmdPath, arguments.Text);
                            processId = process.Id;
                        }
                        else
                        {
                            OutputMessageAsync($"正在尝试启动服务端...");

                            process = Process.Start(cmdPath);
                            processId = process.Id;
                        }
                        if (processId > 0)
                        {
                            labelForPid.Text = processId.ToString();
                            labelForpidText.Visible = true;
                            labelForPid.Visible = true;
                            OutputMessageAsync($"服务端启动成功。");
                            if (checkBox_web_startprocess.Checked) { SendWebhookAsync("服务端启动成功", $"服务端启动成功。"); }

                            ShowNotification($"服务端启动成功。");

                        }
                        else
                        {
                            OutputMessageAsync($"服务端启动失败。");
                            if (checkBox_web_startprocess.Checked) { SendWebhookAsync("服务端启动失败", $"服务端启动失败。"); }
                            ShowNotification($"服务端启动失败。");
                        }
                    }
                    catch (Exception ex)
                    {
                        OutputMessageAsync($"服务端启动失败。");
                        AppendToErrorLog($"服务端启动失败：{ex.Message}");
                        if (checkBox_web_startprocess.Checked) { SendWebhookAsync("服务端启动失败", $"服务端启动失败，请及时检查。"); }
                        ShowNotification($"服务端启动失败，请及时检查。");
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

                var playerList = "";
                // Add the players information to the playersView
                foreach (var player in players)
                {
                    var item = new ListViewItem(new[] { player.name, player.uid, player.steam_id });
                    playersView.Items.Add(item);
                    playerList = playerList + player.name + ",";
                }
                playersTimercounter += 1;
                if (playersTimercounter >= playersTimerthreshold)
                {
                    playersTimercounter = 0;
                    playerList = playerList.TrimEnd(',');
                    if (checkBox_web_getplayers.Checked == true) { SendWebhookAsync("在线玩家统计", $"当前在线玩家：{players.Count}人。\r\n{playerList}"); }

                }
            }
            catch (Exception ex)
            {
                AppendToErrorLog($"获取在线玩家失败：{ex.Message}");
            }
        }
        private void CopyGameDataToBackupPath()
        {
            try
            {
                if (backupPath == "")
                {
                    OutputMessageAsync($"未设置备份存放目录。无法备份。");
                    return;
                }
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

                string tempGameDataPath = Path.Combine(Path.GetTempPath(), "TempGameData");
                Directory.CreateDirectory(tempGameDataPath);
                string tempGameDataCopyPath = Path.Combine(tempGameDataPath, "GameData");

                // Copy the game data to the temporary directory
                DirectoryCopy(gamedataPath, tempGameDataCopyPath, true);

                // Create the backup file from the temporary game data directory
                ZipFile.CreateFromDirectory(tempGameDataCopyPath, backupFilePath);

                // Delete the temporary game data directory
                Directory.Delete(tempGameDataPath, true);

                OutputMessageAsync($"游戏存档已成功备份");
                if (checkBox_web_save.Checked) { SendWebhookAsync("存档备份", $"游戏存档已成功备份。"); }
                ShowNotification($"游戏存档已成功备份。");
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"备份存档失败");
                AppendToErrorLog($"备份存档失败：{ex.Message}");
                if (checkBox_web_save.Checked) { SendWebhookAsync("存档备份失败", $"存档备份失败，请及时检查。"); }

                ShowNotification($"存档备份失败，请及时检查。");
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception
            if (!dir.Exists)
            {
                OutputMessageAsync($"游戏存档路径不存在：{sourceDirName}");
                AppendToErrorLog($"游戏存档路径不存在：{sourceDirName}");
                ShowNotification($"游戏存档路径不存在：{sourceDirName}");
            }

            // If the destination directory does not exist, create it
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
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
                gamedataBox.Text = gamedataPath;
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

                    if (outPutbox.Lines.Length > 100)
                    {
                        outPutbox.Text = string.Join(Environment.NewLine, outPutbox.Lines.Skip(outPutbox.Lines.Length - 100));
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



        private void Form1_Load(object sender, EventArgs e)
        {

            playersView.View = View.Details;
            playersView.Columns.Add(new ColumnHeader() { Text = "Name", Width = playersView.Width / 3 });
            playersView.Columns.Add(new ColumnHeader() { Text = "UID", Width = playersView.Width / 3 });
            playersView.Columns.Add(new ColumnHeader() { Text = "Steam ID", Width = playersView.Width / 3 });

            playersView.FullRowSelect = true;
            playersView.MultiSelect = false;
            playersView.HideSelection = false;

            tabControl1.TabPages[0].Text = "服务监控";
            tabControl1.TabPages[1].Text = "自动存档";
            tabControl1.TabPages[2].Text = "Rcon";
            tabControl1.TabPages[3].Text = "通知";
            tabControl1.TabPages[4].Text = "测试功能";

            LoadConfig();
            memTimer.Start();
            string buildVersion = Application.ProductVersion;
            int endIndex = buildVersion.IndexOf('+');
            string version = buildVersion.Substring(0, endIndex); //去掉构建标识符
            verisionLabel.Text = $"当前版本：{version}";
            checkVersion(version);
            OutputMessageAsync($"当前构建版本号：{version}");

            OutputMessageAsync($"请先配置好信息，再勾选功能启动");


        }
        private void checkVersion(string myversion)
        {
            try
            {
                string url = $"http://127.0.0.1/version?v={myversion}";
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string latestVersion = data[0].version;
                    string notice = data[0].notice;
                    string news = data[0].news;
                    if (notice != "")
                    {
                        OutputMessageAsync($"{notice}");
                        ShowNotification($"{notice}",true);
                    }
                    if (news != "")
                    {
                        OutputMessageAsync($"{news}");
                    }

                    if (IsVersionNewer(latestVersion, myversion))
                    {
                        linkLabel2.Text = $"点击下载最新版本(v{latestVersion})";
                        projectUrl = data[0].url;
                        linkLabel2.Visible = true;
                    }

                }
            }
            catch
            { }

        }

        private bool IsVersionNewer(string latestVersion, string myVersion)
        {
            Version latest = new Version(latestVersion);
            Version current = new Version(myVersion);
            return latest > current;
        }
        private void LoadConfig()
        {
            try
            {
                if (System.IO.File.Exists(ConfigFilePath))
                {
                    using (StreamReader reader = new StreamReader(ConfigFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("CmdPath="))
                            {
                                cmdPath = line.Substring("CmdPath=".Length);
                                cmdbox.Text = cmdPath;
                            }
                            else if (line.StartsWith("BackupPath="))
                            {
                                backupPath = line.Substring("BackupPath=".Length);
                                backupPathbox.Text = backupPath;
                            }
                            else if (line.StartsWith("GameDataPath="))
                            {
                                gamedataPath = line.Substring("GameDataPath=".Length);
                                gamedataBox.Text = gamedataPath;
                            }
                            else if (line.StartsWith("MemTarget="))
                            {
                                memTarget = Convert.ToInt32(line.Substring("MemTarget=".Length));
                                memTargetbox.Value = memTarget;
                            }
                            else if (line.StartsWith("RconHost="))
                            {
                                rconHost = line.Substring("RconHost=".Length);
                            }
                            else if (line.StartsWith("RconPort="))
                            {
                                rconPort = Convert.ToInt32(line.Substring("RconPort=".Length));
                                rconPortbox.Text = rconPort.ToString();
                            }
                            else if (line.StartsWith("RconPassword="))
                            {
                                rconPassword = line.Substring("RconPassword=".Length);
                                passWordbox.Text = rconPassword;
                            }
                            else if (line.StartsWith("RebootSeconds="))
                            {
                                rebootSeconds = Convert.ToInt32(line.Substring("RebootSeconds=".Length));
                                rebootSecondbox.Value = rebootSeconds;
                            }
                            else if (line.StartsWith("CheckSeconds="))
                            {
                                memTimer.Interval = Convert.ToInt32(line.Substring("CheckSeconds=".Length)) * 1000;
                                checkSecondbox.Value = memTimer.Interval / 1000;
                            }
                            else if (line.StartsWith("BackupSeconds="))
                            {
                                saveTimer.Interval = Convert.ToInt32(line.Substring("BackupSeconds=".Length)) * 1000;
                                backupSecondsbox.Value = saveTimer.Interval / 1000;
                            }
                            else if (line.StartsWith("Parameters="))
                            {
                                arguments.Text = line.Substring("Parameters=".Length);
                            }
                            else if (line.StartsWith("WebhookUrl="))
                            {
                                webhookBox.Text = line.Substring("WebhookUrl=".Length);
                            }
                            else if (line.StartsWith("isReboot="))
                            {
                                if (line.Substring("isReboot=".Length) == "True")
                                {
                                    checkBox_reboot.Checked = true;
                                }
                                else
                                {
                                    checkBox_reboot.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isStartProcess="))
                            {
                                if (line.Substring("isStartProcess=".Length) == "True")
                                {
                                    checkBox_startprocess.Checked = true;
                                }
                                else
                                {
                                    checkBox_startprocess.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isParameters="))
                            {
                                if (line.Substring("isParameters=".Length) == "True")
                                {
                                    checkBox_args.Checked = true;
                                }
                                else
                                {
                                    checkBox_args.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isNoti="))
                            {
                                if (line.Substring("isNoti=".Length) == "True")
                                {
                                    checkBox_Noti.Checked = true;
                                }
                                else
                                {
                                    checkBox_Noti.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isSave="))
                            {
                                if (line.Substring("isSave=".Length) == "True")
                                {
                                    checkBox_save.Checked = true;
                                }
                                else
                                {
                                    checkBox_save.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isGetPlayers="))
                            {
                                if (line.Substring("isGetPlayers=".Length) == "True")
                                {
                                    checkBox_geplayers.Checked = true;
                                }
                                else
                                {
                                    checkBox_geplayers.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isWebhook="))
                            {
                                if (line.Substring("isWebhook=".Length) == "True")
                                {
                                    checkBox_webhook.Checked = true;
                                }
                                else
                                {
                                    checkBox_webhook.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isWebGetPlayers="))
                            {
                                if (line.Substring("isWebGetPlayers=".Length) == "True")
                                {
                                    checkBox_web_getplayers.Checked = true;
                                }
                                else
                                {
                                    checkBox_web_getplayers.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isWebReboot="))
                            {
                                if (line.Substring("isWebReboot=".Length) == "True")
                                {
                                    checkbox_web_reboot.Checked = true;
                                }
                                else
                                {
                                    checkbox_web_reboot.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isWebSave="))
                            {
                                if (line.Substring("isWebSave=".Length) == "True")
                                {
                                    checkBox_web_save.Checked = true;
                                }
                                else
                                {
                                    checkBox_web_save.Checked = false;
                                }
                            }
                            else if (line.StartsWith("isWebStartProcess="))
                            {
                                if (line.Substring("isWebStartProcess=".Length) == "True")
                                {
                                    checkBox_web_startprocess.Checked = true;
                                }
                                else
                                {
                                    checkBox_web_startprocess.Checked = false;
                                }
                            }
                            else
                            {
                                continue;
                            }

                        }
                    }
                }
                else
                {
                    OutputMessageAsync($"未找到配置文件，已加载默认配置。");
                    ShowNotification($"未找到配置文件，已加载默认配置。");
                    dataInit();
                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"读取配置文件失败。");
                ShowNotification($"读取配置文件失败。");
                AppendToErrorLog($"读取配置文件失败：{ex.Message}");
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // 取消关闭事件
                this.Hide(); // 隐藏窗体
                notifyIcon1.Visible = true; // 显示托盘图标
            }
        }




        private void SaveConfig()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ConfigFilePath))
                {
                    writer.WriteLine("[General]");
                    writer.WriteLine("CmdPath=" + cmdbox.Text);
                    writer.WriteLine("Parameters=" + arguments.Text);
                    writer.WriteLine("BackupPath=" + backupPathbox.Text);
                    writer.WriteLine("GameDataPath=" + gamedataBox.Text);
                    writer.WriteLine("MemTarget=" + memTarget);
                    writer.WriteLine("RconHost=" + rconHost);
                    writer.WriteLine("RconPort=" + rconPortbox.Text);
                    writer.WriteLine("RconPassword=" + passWordbox.Text);
                    writer.WriteLine("RebootSeconds=" + rebootSeconds);
                    writer.WriteLine("CheckSeconds=" + memTimer.Interval / 1000);
                    writer.WriteLine("BackupSeconds=" + saveTimer.Interval / 1000);
                    writer.WriteLine("WebhookUrl=" + webhookBox.Text);
                    if (checkBox_reboot.Checked) { writer.WriteLine("isReboot=True"); } else { writer.WriteLine("isReboot=False"); }
                    if (checkBox_startprocess.Checked) { writer.WriteLine("isStartProcess=True"); } else { writer.WriteLine("isStartProcess=False"); }
                    if (checkBox_args.Checked) { writer.WriteLine("isParameters=True"); } else { writer.WriteLine("isParameters=False"); }
                    if (checkBox_Noti.Checked) { writer.WriteLine("isNoti=True"); } else { writer.WriteLine("isNoti=False"); }
                    if (checkBox_save.Checked) { writer.WriteLine("isSave=True"); } else { writer.WriteLine("isSave=False"); }
                    if (checkBox_geplayers.Checked) { writer.WriteLine("isGetPlayers=True"); } else { writer.WriteLine("isGetPlayers=False"); }
                    if (checkBox_webhook.Checked) { writer.WriteLine("isWebhook=True"); } else { writer.WriteLine("isWebhook=False"); }
                    if (checkBox_web_getplayers.Checked) { writer.WriteLine("isWebGetPlayers=True"); } else { writer.WriteLine("isWebGetPlayers=False"); }
                    if (checkbox_web_reboot.Checked) { writer.WriteLine("isWebReboot=True"); } else { writer.WriteLine("isWebReboot=False"); }
                    if (checkBox_web_save.Checked) { writer.WriteLine("isWebSave=True"); } else { writer.WriteLine("isWebSave=False"); }
                    if (checkBox_web_startprocess.Checked) { writer.WriteLine("isWebStartProcess=True"); } else { writer.WriteLine("isWebStartProcess=False"); }


                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"保存配置文件失败。");
                AppendToErrorLog($"保存配置文件失败：{ex.Message}");

            }
        }




        private void dataInit()
        {
            memTimer.Interval = Convert.ToInt32(checkSecondbox.Value) * 1000;

            memTarget = Convert.ToInt32(memTargetbox.Value);
            rconHost = "127.0.0.1";
            rconPort = 25575;
            rconPassword = "admin";
            rebootSeconds = 10;
            cmdPath = "";
            gamedataPath = "";
            backupPath = "";
            saveTimer.Interval = Convert.ToInt32(backupSecondsbox.Value) * 1000;



        }

        private void checkBox_startprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdPath == "")
            {
                labelForstart.Text = "[ 关闭 ]";
                OutputMessageAsync($"请先设置服务端路径。");
                labelForPid.Visible = false;
                labelForpidText.Visible = false;
                checkBox_startprocess.Checked = false;


            }
            else if (checkBox_startprocess.Checked)
            {
                labelForstart.Text = "[ 开启 ]";
                OutputMessageAsync($"已开始监控服务端。");
            }
            else
            {
                labelForprogram.Text = "未知";
                labelForstart.Text = "[ 关闭 ]";
                labelForPid.Visible = false;
                labelForpidText.Visible = false;
                OutputMessageAsync($"已停止监控服务端。");
            }

        }

        private void checkBox_save_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_save.Checked)
            {
                if (gamedataPath == "")
                {
                    OutputMessageAsync($"请先选择游戏存档路径。");
                    labelForsave.Text = "[ 关闭 ]";
                    checkBox_save.Checked = false;
                }
                else if (backupPath == "")
                {
                    OutputMessageAsync($"请先选择存档备份路径。");
                    labelForsave.Text = "[ 关闭 ]";
                    checkBox_save.Checked = false;
                }
                else
                {
                    saveTimer.Interval = Convert.ToInt32(backupSecondsbox.Value) * 1000;
                    saveTimer.Start();
                    labelForsave.Text = "[ 开启 ]";
                    OutputMessageAsync($"已启用自动备份存档。");
                    OutputMessageAsync($"自动存档中...");
                    CopyGameDataToBackupPath();

                }

            }
            else
            {
                saveTimer.Stop();
                labelForsave.Text = "[ 关闭 ]";
                OutputMessageAsync($"已停用自动备份存档。");
            }

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





        private void rconPortbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }



        private void passWordbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            rconPassword = passWordbox.Text;
            //OutputMessageAsync($"密码已设置为：{rconPassword}");
        }

        private void rebootSecondbox_ValueChanged(object sender, EventArgs e)
        {


        }

        private void checkBox_reboot_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdPath == "")
            {
                checkBox_reboot.Checked = false;
                labelForreboot.Text = "[ 关闭 ]";
                OutputMessageAsync($"请先选择服务端路径。");
            }

            else if (checkBox_reboot.Checked)
            {
                labelForreboot.Text = "[ 开启 ]";
                OutputMessageAsync($"已启用自动关服。");
            }
            else
            {
                labelForreboot.Text = "[ 关闭 ]";
                OutputMessageAsync($"已停用自动关服。");
            }
        }

        private void checkBox_geplayers_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_geplayers.Checked)
            {
                playersTimercounter = playersTimerthreshold;//启动即触发一次
                getplayerTimer.Start();
                labelForgetplayers.Text = "[ 开启 ]";
                OutputMessageAsync($"已启用自动获取在线玩家。");
            }
            else
            {
                getplayerTimer.Stop();
                labelForgetplayers.Text = "[ 关闭 ]";
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
                OutputMessageAsync($"关服指令发送失败。");
                AppendToErrorLog($"关服指令发送失败：{ex.Message}");
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
                OutputMessageAsync($"info指令发送失败。");
                AppendToErrorLog($"info指令发送失败：{ex.Message}");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                textBox1.Text = textBox1.Text.Replace(" ", "_");
                var info = RconUtils.SendMsg($"broadcast {textBox1.Text.Trim()}");

                OutputMessageAsync($"已发送：{info}");

            }

            catch (Exception ex)
            {
                OutputMessageAsync($"broadcast指令发送失败。");
                AppendToErrorLog($"broadcast指令发送失败：{ex.Message}");
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


        private void checkBox_args_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_args.Checked)
            {
                arguments.Enabled = true;
                OutputMessageAsync($"请填写服务端启动参数。");
            }
            else
            {
                arguments.Enabled = false;
            }
        }

        private void selectCustombutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                gamedataBox.Text = folderBrowserDialog.SelectedPath;
                gamedataPath = gamedataBox.Text;
                OutputMessageAsync($"已选择游戏存档路径为：{gamedataPath}");
            }
        }
        private bool isKeyUpEvent_backupSecond = false;
        private void backupSecondsbox_KeyUp(object sender, KeyEventArgs e)
        {
            var newBackupSecond = Convert.ToInt32(backupSecondsbox.Value) * 1000;
            saveTimer.Interval = newBackupSecond;
            isKeyUpEvent_backupSecond = true;
            OutputMessageAsync($"存档周期已调整为：{newBackupSecond / 1000}秒");
        }
        private void backupSecondsbox_ValueChanged(object sender, EventArgs e)
        {
            if (isKeyUpEvent_backupSecond)
            {
                isKeyUpEvent_backupSecond = false;
                return;
            }
            var newBackupSecond = Convert.ToInt32(backupSecondsbox.Value) * 1000;
            saveTimer.Interval = newBackupSecond;
            OutputMessageAsync($"存档周期已调整为：{newBackupSecond / 1000}秒");
        }

        private bool isKeyUpEvent_rebootSecond = false;
        private void rebootSecondbox_KeyUp(object sender, KeyEventArgs e)
        {
            rebootSeconds = Convert.ToInt32(rebootSecondbox.Value);
            isKeyUpEvent_rebootSecond = true;
            OutputMessageAsync($"重启延迟已设置为：{rebootSeconds}秒");
        }

        private void rebootSecondbox_ValueChanged_1(object sender, EventArgs e)
        {
            if (isKeyUpEvent_rebootSecond)
            {
                isKeyUpEvent_rebootSecond = false;
                return;
            }
            rebootSeconds = Convert.ToInt32(rebootSecondbox.Value);
            OutputMessageAsync($"重启延迟已设置为：{rebootSeconds}秒");
        }

        private bool isKeyUpEvent_checkSecond = false;

        private void checkSecondbox_ValueChanged(object sender, EventArgs e)
        {
            if (isKeyUpEvent_checkSecond)
            {
                isKeyUpEvent_checkSecond = false;
                return;
            }
            var newSecond = Convert.ToInt32(checkSecondbox.Value);
            memTimer.Interval = newSecond * 1000;
            OutputMessageAsync($"监测周期已调整为：{newSecond}秒");
        }

        private void checkSecondbox_KeyUp(object sender, KeyEventArgs e)
        {
            var newSecond = Convert.ToInt32(checkSecondbox.Value);
            memTimer.Interval = newSecond * 1000;
            isKeyUpEvent_checkSecond = true;
            OutputMessageAsync($"监测周期已调整为：{newSecond}秒");
        }

        private bool isKeyUpEvent_memTarget = false;

        private void memTargetbox_KeyUp(object sender, KeyEventArgs e)
        {
            memTarget = Convert.ToInt32(memTargetbox.Value);
            isKeyUpEvent_memTarget = true;
            OutputMessageAsync($"内存阈值已调整为：{memTarget}%");
        }
        private void memTargetbox_ValueChanged(object sender, EventArgs e)
        {
            if (isKeyUpEvent_memTarget)
            {
                isKeyUpEvent_memTarget = false;
                return;
            }
            memTarget = Convert.ToInt32(memTargetbox.Value);
            OutputMessageAsync($"内存阈值已调整为：{memTarget}%");

        }

        private void playersView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (playersView.SelectedItems.Count > 0)
            {

                string Uid = playersView.SelectedItems[0].SubItems[1].Text;
                UIDBox.Text = Uid;

            }
        }

        private void kickbutton_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                var info = RconUtils.SendMsg($"KickPlayer {UIDBox.Text.Trim()}");

                OutputMessageAsync($"{info}");

            }

            catch (Exception ex)
            {
                OutputMessageAsync($"Kickplayer指令发送失败。");
                AppendToErrorLog($"Kickplayer指令发送失败：{ex.Message}");
            }
        }

        private void banbutton_Click(object sender, EventArgs e)
        {
            try
            {
                RconUtils.TestConnection(rconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text);

                var info = RconUtils.SendMsg($"BanPlayer {UIDBox.Text.Trim()}");

                OutputMessageAsync($"{info}");

            }

            catch (Exception ex)
            {
                OutputMessageAsync($"BanPlayer指令发送失败。{ex.Message}");
                AppendToErrorLog($"BanPlayer指令发送失败。");
            }
        }
        private void AppendToErrorLog(string content)
        {
            string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "log");
            string errorLogPath = Path.Combine(logFolderPath, errorLogname);

            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            using (StreamWriter writer = System.IO.File.AppendText(errorLogPath))
            {
                writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {content}");
            }
        }

        private configForm configForm; // Declare a field to hold the instance of ConfigForm

        private void settingButton_Click(object sender, EventArgs e)
        {
            if (configForm == null || configForm.IsDisposed) // Check if the configForm is null or disposed
            {
                configForm = new configForm(); // Create a new instance of ConfigForm
                configForm.Show(); // Show the configForm
            }
            else
            {
                configForm.BringToFront(); // Bring the existing configForm to the front
            }
        }

        private void testWebhookbutton_Click(object sender, EventArgs e)
        {

            SendWebhookAsync("测试标题", "这是一条测试推送通知。");

        }
        private async Task SendWebhookAsync(string title, string message)
        {
            if (!checkBox_webhook.Checked)
            {
                return;
            }
            if (webhookBox.Text == "")
            {
                OutputMessageAsync($"Webhook地址为空。");
                return;
            }
            if (!webhookBox.Text.Contains("http"))
            {
                OutputMessageAsync($"Webhook格式不正确。");
                return;
            }
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string webhookUrl = webhookBox.Text;
                    var webhook = new WebhookJson();
                    string json = webhook.GenerateJson(webhookUrl, title, message);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync(webhookUrl, content);
                    OutputMessageAsync($"Webhook发送成功。");
                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"Webhook发送失败。");
                AppendToErrorLog($"Webhook发送失败：{ex.Message}");
            }

        }

        private void checkBox_webhook_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_webhook.Checked)
            {
                webhookBox.Enabled = true;
                testWebhookbutton.Enabled = true;
                labelForwebhook.Text = "[ 开启 ]";
                OutputMessageAsync($"已启用Webhook推送。");
            }
            else
            {
                webhookBox.Enabled = false;
                testWebhookbutton.Enabled = false;
                labelForwebhook.Text = "[ 关闭 ]";
                OutputMessageAsync($"已停用Webhook推送。");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); // 显示窗体
            this.WindowState = FormWindowState.Normal;
      
        }


        private void ShowNotification(string message,Boolean forced = false)
        {
            if (!forced)
            {
                if (checkBox_Noti.Checked)
                {
                    notifyIcon1.BalloonTipText = message;
                    notifyIcon1.ShowBalloonTip(2000);
                }
            }
            else {
                notifyIcon1.BalloonTipText = message;
                notifyIcon1.ShowBalloonTip(2000);
            }
            

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(projectUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveConfig();
   
            Application.Exit();
        }

   

        /**  HTTP功能已弃用
        private Thread httpThread;
        private bool isHttpServerRunning;

        private void checkBox_host_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_host.Checked)
            {
                StartHttpServer();
            }
            else
            {
                StopHttpServer();
            }
        }

        private void StartHttpServer()
        {
            if (!isHttpServerRunning)
            {
                httpThread = new Thread(HttpServerThread);
                httpThread.Start();
                isHttpServerRunning = true;
            }
        }

        private void StopHttpServer()
        {
            if (isHttpServerRunning)
            {
                httpThread.Abort();
                isHttpServerRunning = false;
            }
        }

        private void HttpServerThread()
        {
            string folderPath = Path.Combine(Application.StartupPath, "list");
            string filePath = Path.Combine(folderPath, "banlist.txt");
          

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath).Close();
            }
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add("http://127.0.0.1:8080/");
                listener.Start();

                while (true)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;

                        if (request.Url.LocalPath == "/banlist.txt")
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                                //response.ContentType = "text/plain"; 
                                response.ContentType = "text/html; charset = UTF - 8";

                                response.ContentLength64 = fileBytes.Length;
                                response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                                response.OutputStream.Close();
                            }
                            else
                            {
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                response.Close();
                            }
                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            response.Close();
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                    }
                }

                listener.Stop();
            }
        }
      **/
    }
}
