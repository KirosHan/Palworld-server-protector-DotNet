namespace Palworld_server_protector_DotNet
{
	using Microsoft.VisualBasic.Devices;
	using SharedLibrary;
	using SharedLibrary.Model;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;
    using System.Resources;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;

    public partial class Form1 : Form
    {
        private System.Timers.Timer memTimer;
        private System.Timers.Timer saveTimer;
        private System.Timers.Timer getplayerTimer;
        private System.Timers.Timer getversionTimer;

        private configForm configForm;

        private Settings Settings = new();

        private string errorLogname = $"error_{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";
        private string projectUrl = $"https://github.com/KirosHan/Palworld-server-protector-DotNet";
        private int playersTimercounter = 0;
        private int playersTimerthreshold = 600;//每半小时触发600次
        private int getversionErrorCounter = 0;
        private string versionChcekUrl = $"http://127.0.0.1/version?v=";
        private const string ConfigFilePath = "config.ini";
        private const string JsonConfigFilePath = "config.json";
        private RconCommandClient _rconCommandClient;
        private Dictionary<string, DateTime> playerNotificationTimes = new Dictionary<string, DateTime>();//记录玩家触发通知时间


        // 获取当前文化信息
        CultureInfo ci = CultureInfo.CurrentUICulture;

        private List<PalUserInfo> lastPlayerlist = new();

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeTimer()
        {
            memTimer = new();
            memTimer.Interval = 35000; // 设置定时器间隔为5秒
            memTimer.Elapsed += (sender, args) =>
            {
                Task t = Timer_Tick(sender, args);
                t.Wait();
            };

            saveTimer = new();
            saveTimer.Interval = 35000; // 设置定时器间隔为5秒
            saveTimer.Elapsed += (sender, args) =>
            {
                Task t = saveTimer_Tick(sender, args);
                t.Wait();
            };

            getplayerTimer = new();
            getplayerTimer.Interval = 10000; // 设置定时器间隔为s秒
            getplayerTimer.Elapsed += (sender, args) =>
            {
                Task t = getplayerTimer_Tick(sender, args);
                t.Wait();
            };

            getversionTimer = new();
            getversionTimer.Interval = 10000; // 设置定时器间隔为s秒
            getversionTimer.Elapsed += (sender, args) =>
            {
                Task t = getversionTimer_Tick(sender, args);
                t.Wait();
            };
        }

        private void ExecuteSecure(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(new System.Windows.Forms.MethodInvoker(() =>
                {
                    action();
                }));
            }
            else
            {
                action();
            }
        }
        private void UpdateLabelForProgram(string text)
        {
            if (labelForprogram.InvokeRequired)
            {
                labelForprogram.Invoke(new Action<string>(UpdateLabelForProgram), text);
            }
            else
            {
                labelForprogram.Text = text;
            }
        }
        private async Task Timer_Tick(object sender, EventArgs e)
        {
            // 获取系统内存使用百分比
            var memoryUsage = Math.Round(GetSystemMemoryUsagePercentage(), 2);
            ExecuteSecure(() => memProcessbar.Value = (int)memoryUsage);
            ExecuteSecure(() => memOutput.Text = $"{memoryUsage}%");

            if (Settings.IsOutputMemInfo)
            {
                //OutputMessageAsync($"当前时间：{DateTime.Now}");
                OutputMessageAsync($"内存使用百分比：{memoryUsage}%");
            }



            if (checkBox_reboot.Checked)//自动关服
            {
                if (memoryUsage >= Settings.MemTarget)
                {
                    try
                    {
                        var isProcessRunning = IsProcessRunning(Settings.CmdPath);
                        if (isProcessRunning)
                        {
                            OutputMessageAsync($"内存达到警戒阈值！！！");
                            // 使用rcon向服务端发送指令

                            var info = await _rconCommandClient.Save();

                            OutputMessageAsync($"{info}");

                            var result = await _rconCommandClient.ShutDown(TimeSpan.FromSeconds(Settings.RebootSeconds), $"Shutdown {Settings.RebootSeconds} The_server_will_restart_in_{Settings.RebootSeconds}_seconds.");

                            OutputMessageAsync($"{result}");
                            OutputMessageAsync($"紧急存档中...");
                            await CopyGameDataToBackupPath();
                            if (checkbox_web_reboot.Checked == true)
                            {
                                await SendWebhookAsync("内存达到警戒阈值", $"内存使用率：{memoryUsage}%,已尝试关闭服务器。");
                            }

                            ShowNotification($"内存使用率：{memoryUsage}%,已尝试关闭服务器。");
                        }


                    }
                    catch (Exception ex)
                    {
                        OutputMessageAsync($"发送指令失败，请检查配置。");

                        await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息：{ex.Message}");

                        if (checkbox_web_reboot.Checked == true)
                        {
                            await SendWebhookAsync("Rcon失败", $"发送关服指令失败，请及时检查。");
                        }

                        ShowNotification($"发送关服指令失败，请及时检查。");
                    }
                }
            }

            if (checkBox_startprocess.Checked)
            { //监控&自启
              // 检查进程是否在运行
                var isProcessRunning = IsProcessRunning(Settings.CmdPath);
                UpdateLabelForProgram( $"{(isProcessRunning ? "运行中" : "未运行")}");
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
                            process = Process.Start(Settings.CmdPath, arguments.Text);
                            processId = process.Id;
                        }
                        else
                        {
                            OutputMessageAsync($"正在尝试启动服务端...");

                            process = Process.Start(Settings.CmdPath);
                            processId = process.Id;
                        }
                        if (processId > 0)
                        {
                            labelForPid.Text = processId.ToString();
                            labelForpidText.Visible = true;
                            labelForPid.Visible = true;
                            OutputMessageAsync($"服务端启动成功。");
                            if (checkBox_web_startprocess.Checked)
                            {
                                await SendWebhookAsync("服务端启动成功", $"服务端启动成功。");
                            }

                            ShowNotification($"服务端启动成功。");

                        }
                        else
                        {
                            OutputMessageAsync($"服务端启动失败。");
                            if (checkBox_web_startprocess.Checked)
                            {
                                await SendWebhookAsync("服务端启动失败", $"服务端启动失败。");
                            }
                            ShowNotification($"服务端启动失败。");
                        }
                    }
                    catch (Exception ex)
                    {
                        OutputMessageAsync($"服务端启动失败。");
                        await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x71>>>服务端启动错误>>>错误信息：{ex.Message}");

                        if (checkBox_web_startprocess.Checked)
                        {
                            await SendWebhookAsync("服务端启动失败", $"服务端启动失败，请及时检查。");
                        }
                        ShowNotification($"服务端启动失败，请及时检查。");
                    }


                }


            }

        }

        private Object lockCreateRconClient = new object();

        private async Task getplayerTimer_Tick(object sender, EventArgs e) //获取在线玩家
        {
            try
            {
                if (!checkBox_geplayers.Checked)
                {
                    return;
                }

                lock (lockCreateRconClient)
                {
                    if (_rconCommandClient is null)
                    {
                        _rconCommandClient = new(Settings.RconHost, Settings.RconPort, Settings.RconPassword);
                    }
                }

                var players = await _rconCommandClient.GetPlayers();

                ExecuteSecure(() => playersCounterLabel.Text = $"当前在线：{players.Count}人");
                ExecuteSecure(() => playersView.Items.Clear());

                String playerList = "";
                // Add the players information to the playersView
                foreach (PalUserInfo player in players)
                {
                    var item = new ListViewItem(new string[] { player.Name, player.Uid, player.SteamId.ToString() });
                    ExecuteSecure(() => playersView.Items.Add(item));
                    playerList = playerList + player.Name + ",";
                }

                DateTime now = DateTime.Now;
                List<string> toNotifyNewPlayers = new List<string>();
                List<string> toNotifyOffPlayers = new List<string>();


                var newPlayerlist = "";


                List<PalUserInfo> newPlayers = players.Except(lastPlayerlist).ToList();
                foreach (var p in newPlayers)
                {
                    if (!playerNotificationTimes.ContainsKey(p.Name) || (now - playerNotificationTimes[p.Name]).TotalSeconds >= 5)
                    {
                        toNotifyNewPlayers.Add(p.Name);
                        playerNotificationTimes[p.Name] = now;
                    }
                }


                var offPlayerlist = "";
                List<PalUserInfo> offPlayers = lastPlayerlist.Except(players).ToList();
                foreach (var p in offPlayers)
                {
                    if (!playerNotificationTimes.ContainsKey(p.Name) || (now - playerNotificationTimes[p.Name]).TotalSeconds >= 5)
                    {
                        toNotifyOffPlayers.Add(p.Name);
                        playerNotificationTimes[p.Name] = now;
                    }
                }
                newPlayerlist = string.Join(",", toNotifyNewPlayers.Select(p => $"[{p}]"));
                offPlayerlist = string.Join(",", toNotifyOffPlayers.Select(p => $"[{p}]"));
                /*f发不了中文，qnmd
                var info = await Rcon.SendCommand(Settings.RconHost, Convert.ToInt32(rconPortbox.Text), passWordbox.Text, $"Broadcast {newPlayerlist.Replace(" ", "_")}_join_the_game.");
                OutputMessageAsync($"{info}");
                */
                if (checkBox_playerStatus.Checked == true)
                {
                    if (newPlayerlist != "")
                    {
                        OutputMessageAsync($"{newPlayerlist.TrimEnd(',')}加入了游戏。");
                        await SendWebhookAsync("玩家加入游戏", $"{newPlayerlist.TrimEnd(',')}加入了游戏。");
                    }
                    if (offPlayerlist != "")
                    {
                        OutputMessageAsync($"{offPlayerlist.TrimEnd(',')}离开了游戏。");
                        await SendWebhookAsync("玩家离开游戏", $"{offPlayerlist.TrimEnd(',')}离开了游戏。");
                    }

                }


                lastPlayerlist = players;

                playersTimercounter += 1;
                if (playersTimercounter >= playersTimerthreshold)
                {
                    playersTimercounter = 0;
                    playerList = playerList.TrimEnd(',');
                    if (checkBox_web_getplayers.Checked == true)
                    {
                        await SendWebhookAsync("在线玩家统计", $"当前在线玩家：{players.Count}人。\r\n{playerList}");
                    }

                }
            }
            catch (Exception ex)
            {
                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x67>>>获取在线玩家失败>>>错误信息：{ex.Message}");
            }
        }

        private async Task CopyGameDataToBackupPath()
        {
            try
            {
                if (Settings.BackupPath == "")
                {
                    Invoke(new Action(() => OutputMessageAsync($"未设置备份存放目录。无法备份。")));
                    return;
                }
                string backupFolderName = $"SaveGames-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.zip";
                string backupFilePath = Path.Combine(Settings.BackupPath, backupFolderName);

                if (!Directory.Exists(Settings.GameDataPath))
                {
                    Invoke(new Action(() => OutputMessageAsync($"游戏存档路径不存在：{Settings.GameDataPath}")));
                    return;
                }

                if (!Directory.Exists(Settings.BackupPath))
                {
                    Invoke(new Action(() => OutputMessageAsync($"存档备份路径不存在：{Settings.BackupPath}")));
                    return;
                }

                // 使用唯一标识符确保每次都创建一个新的临时目录
                string tempGameDataPath = Path.Combine(Path.GetTempPath(), $"TempGameData-{Guid.NewGuid()}");
                Directory.CreateDirectory(tempGameDataPath);
                string tempGameDataCopyPath = Path.Combine(tempGameDataPath, "GameData");

                // Copy the game data to the temporary directory
                await DirectoryCopy(Settings.GameDataPath, tempGameDataCopyPath, true);

                ZipFile.CreateFromDirectory(tempGameDataCopyPath, backupFilePath);

                try
                {
                    Directory.Delete(tempGameDataPath, true);
                }
                catch (IOException)
                {
                    // 如果删除失败，可能需要重试或记录错误
                    Task.Delay(1000).Wait(); // 稍等一会儿再次尝试
                    Directory.Delete(tempGameDataPath, true); // 重新尝试删除
                }

                Invoke(new Action(() => OutputMessageAsync($"游戏存档已成功备份")));

                if (checkBox_web_save.Checked)
                {
                    await SendWebhookAsync("存档备份", $"游戏存档已成功备份。");
                }
                ShowNotification($"游戏存档已成功备份。");
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => OutputMessageAsync($"备份存档失败")));


                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x92>>>备份存档错误>>>错误信息：{ex.Message}");

                if (checkBox_web_save.Checked) { SendWebhookAsync("存档备份失败", $"存档备份失败，请及时检查。"); }
                ShowNotification($"存档备份失败，请及时检查。");
            }
        }

        private async Task DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception
            if (!dir.Exists)
            {
                OutputMessageAsync($"游戏存档路径不存在：{sourceDirName}");

                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x91>>>游戏存档路径不存在>>>错误信息：{sourceDirName}");

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
                    await DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private async Task saveTimer_Tick(object sender, EventArgs e) //存档逻辑
        {
            OutputMessageAsync($"自动存档中...");
            await CopyGameDataToBackupPath();
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
                OutputMessageAsync($"已选择服务端路径为：{Settings.CmdPath}");
                gamedataBox.Text = Path.Combine(Path.GetDirectoryName(Settings.CmdPath), "Pal", "Saved", "SaveGames");
                OutputMessageAsync($"游戏存档路径修改为：{Settings.GameDataPath}");
            }
        }

        private void OutputMessageAsync(string message)
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
        }

        private async Task getversionTimer_Tick(object sender, EventArgs e) //获取版本信息
        {
            string buildVersion = Application.ProductVersion;
            int endIndex = buildVersion.IndexOf('+');
            string version = endIndex > 0 ? buildVersion.Substring(0, endIndex) : buildVersion; //去掉构建标识符
            await checkVersion(version);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = groupBox1.Width + 40;

            playersView.View = View.Details;
            playersView.Columns.Add(new ColumnHeader() { Text = "Name", Width = playersView.Width / 3 });
            playersView.Columns.Add(new ColumnHeader() { Text = "UID", Width = playersView.Width / 3 });
            playersView.Columns.Add(new ColumnHeader() { Text = "Steam ID", Width = playersView.Width / 3 });

            playersView.FullRowSelect = true;
            playersView.MultiSelect = false;
            playersView.HideSelection = false;

            comboBox_id.SelectedIndex = 0;
            InitializeTimer();
            string buildVersion = Application.ProductVersion;
            int endIndex = buildVersion.IndexOf('+'); // 找到版本号中的"+"符号的索引位置
            string version = endIndex > 0 ? buildVersion.Substring(0, endIndex) : buildVersion;
            this.Text = $"Palworld Server Protector v{version}";

            // 加载配置文件，处理配置文件和UI之间数据绑定
            LoadConfig();
            SyncUIWithSettings(Settings);
            SetupDataBindings(Settings);

            lock (lockCreateRconClient)
            {
                _rconCommandClient ??= new(Settings.RconHost, Settings.RconPort, Settings.RconPassword);
            }

            memTimer.Start();
            verisionLabel.Text = $"当前版本：{version}";

            OutputMessageAsync($"当前构建版本号：{version}");
        }

        private async Task checkVersion(string myversion)
        {
            try
            {

                using (WebClient client = new WebClient())
                {
                    string json = await client.DownloadStringTaskAsync(new Uri(versionChcekUrl + myversion));
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string latestVersion = data[0].version;
                    string notice = data[0].notice;
                    string news = data[0].news;
                    string updatetime = data[0].date.ToString("yyyy/MM/dd");

                    if (notice != "")
                    {
                        OutputMessageAsync($"{notice}");
                        ShowNotification($"{notice}", true);
                    }
                    if (news != "")
                    {
                        OutputMessageAsync($"{news}");
                    }

                    if (IsVersionNewer(latestVersion, myversion))
                    {

                        this.Invoke(new Action(() =>
                        {
                            linkLabel2.Text = $"点击下载最新版本(v{latestVersion})";
                            projectUrl = data[0].url;
                            linkLabel2.Visible = true;
                        }));

                        OutputMessageAsync($"【更新】新版本v{latestVersion}（{updatetime}）已经发布，请点击最下方链接前往下载更新。");
                    }

                }
                getversionTimer.Stop();
            }
            catch
            {
                if (getversionErrorCounter == 0)
                {
                    getversionTimer.Start();
                }
                getversionErrorCounter++;
                if (getversionErrorCounter >= 5)
                {
                    getversionTimer.Stop();
                }

            }

        }

        private bool IsVersionNewer(string latestVersion, string myVersion)
        {
            Version latest = new Version(latestVersion);
            Version current = new Version(myVersion);
            return latest > current;
        }

        private void SyncUIWithSettings(Settings settings)
        {
            // 同步字符串和数值属性
            cmdbox.Text = settings.CmdPath;
            backupPathbox.Text = settings.BackupPath;
            gamedataBox.Text = settings.GameDataPath;
            memTargetbox.Value = settings.MemTarget;
            //Settings.RconHostbox.Text = settings.RconHost;
            rconPortbox.Text = settings.RconPort.ToString();
            passWordbox.Text = settings.RconPassword;
            rebootSecondbox.Value = settings.RebootSeconds;
            checkSecondbox.Value = settings.CheckSeconds;
            backupSecondsbox.Value = settings.BackupSeconds;
            arguments.Text = settings.Parameters;
            webhookBox.Text = settings.WebhookUrl;

            // 同步布尔属性（复选框）
            checkBox_reboot.Checked = settings.IsReboot;
            checkBox_startprocess.Checked = settings.IsStartProcess;
            checkBox_args.Checked = settings.IsParameters;
            checkBox_Noti.Checked = settings.IsNoti;
            checkBox_save.Checked = settings.IsSave;
            checkBox_geplayers.Checked = settings.IsGetPlayers;
            checkBox_webhook.Checked = settings.IsWebhook;
            checkBox_web_getplayers.Checked = settings.IsWebGetPlayers;
            checkbox_web_reboot.Checked = settings.IsWebReboot;
            checkBox_web_save.Checked = settings.IsWebSave;
            checkBox_web_startprocess.Checked = settings.IsWebStartProcess;
            checkBox_playerStatus.Checked = settings.IsWebPlayerStatus;
            checkBox_mem.Checked = settings.IsOutputMemInfo;
        }

        private void SetupDataBindings(Settings settings)
        {
            // 清除现有的绑定
            ClearDataBindings();

            // 字符串和数值属性的绑定
            cmdbox.DataBindings.Add("Text", settings, nameof(Settings.CmdPath), false, DataSourceUpdateMode.OnPropertyChanged);
            backupPathbox.DataBindings.Add("Text", settings, nameof(Settings.BackupPath), false, DataSourceUpdateMode.OnPropertyChanged);
            gamedataBox.DataBindings.Add("Text", settings, nameof(Settings.GameDataPath), false, DataSourceUpdateMode.OnPropertyChanged);
            memTargetbox.DataBindings.Add("Value", settings, nameof(Settings.MemTarget), false, DataSourceUpdateMode.OnPropertyChanged);
            rconPortbox.DataBindings.Add("Text", settings, nameof(Settings.RconPort), false, DataSourceUpdateMode.OnPropertyChanged);
            passWordbox.DataBindings.Add("Text", settings, nameof(Settings.RconPassword), false, DataSourceUpdateMode.OnPropertyChanged);
            rebootSecondbox.DataBindings.Add("Value", settings, nameof(Settings.RebootSeconds), false, DataSourceUpdateMode.OnPropertyChanged);
            checkSecondbox.DataBindings.Add("Value", settings, nameof(Settings.CheckSeconds), false, DataSourceUpdateMode.OnPropertyChanged);
            backupSecondsbox.DataBindings.Add("Value", settings, nameof(Settings.BackupSeconds), false, DataSourceUpdateMode.OnPropertyChanged);
            arguments.DataBindings.Add("Text", settings, nameof(Settings.Parameters), false, DataSourceUpdateMode.OnPropertyChanged);
            webhookBox.DataBindings.Add("Text", settings, nameof(Settings.WebhookUrl), false, DataSourceUpdateMode.OnPropertyChanged);

            // 布尔属性（复选框）的绑定
            checkBox_reboot.DataBindings.Add("Checked", settings, nameof(Settings.IsReboot), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_startprocess.DataBindings.Add("Checked", settings, nameof(Settings.IsStartProcess), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_args.DataBindings.Add("Checked", settings, nameof(Settings.IsParameters), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_Noti.DataBindings.Add("Checked", settings, nameof(Settings.IsNoti), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_save.DataBindings.Add("Checked", settings, nameof(Settings.IsSave), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_geplayers.DataBindings.Add("Checked", settings, nameof(Settings.IsGetPlayers), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_webhook.DataBindings.Add("Checked", settings, nameof(Settings.IsWebhook), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_web_getplayers.DataBindings.Add("Checked", settings, nameof(Settings.IsWebGetPlayers), false, DataSourceUpdateMode.OnPropertyChanged);
            checkbox_web_reboot.DataBindings.Add("Checked", settings, nameof(Settings.IsWebReboot), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_web_save.DataBindings.Add("Checked", settings, nameof(Settings.IsWebSave), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_web_startprocess.DataBindings.Add("Checked", settings, nameof(Settings.IsWebStartProcess), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_playerStatus.DataBindings.Add("Checked", settings, nameof(Settings.IsWebPlayerStatus), false, DataSourceUpdateMode.OnPropertyChanged);
            checkBox_mem.DataBindings.Add("Checked", settings, nameof(Settings.IsOutputMemInfo), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ClearDataBindings()
        {
            cmdbox.DataBindings.Clear();
            backupPathbox.DataBindings.Clear();
            gamedataBox.DataBindings.Clear();
            memTargetbox.DataBindings.Clear();
            rconPortbox.DataBindings.Clear();
            passWordbox.DataBindings.Clear();
            rebootSecondbox.DataBindings.Clear();
            checkSecondbox.DataBindings.Clear();
            backupSecondsbox.DataBindings.Clear();
            arguments.DataBindings.Clear();
            webhookBox.DataBindings.Clear();

            checkBox_reboot.DataBindings.Clear();
            checkBox_startprocess.DataBindings.Clear();
            checkBox_args.DataBindings.Clear();
            checkBox_Noti.DataBindings.Clear();
            checkBox_save.DataBindings.Clear();
            checkBox_geplayers.DataBindings.Clear();
            checkBox_webhook.DataBindings.Clear();
            checkBox_web_getplayers.DataBindings.Clear();
            checkbox_web_reboot.DataBindings.Clear();
            checkBox_web_save.DataBindings.Clear();
            checkBox_web_startprocess.DataBindings.Clear();
            checkBox_playerStatus.DataBindings.Clear();
            checkBox_mem.DataBindings.Clear();
        }

        private void LoadConfig()
        {
            try
            {
                if (System.IO.File.Exists(JsonConfigFilePath))
                {
                    Settings = Settings.LoadFromConfigFile(JsonConfigFilePath);
                    OutputMessageAsync($"从 {JsonConfigFilePath} 读取配置");
                }
                else
                {
                    if (System.IO.File.Exists(ConfigFilePath))
                    {
                        Settings = Settings.LoadSettingsFromIniFile(ConfigFilePath);
                        OutputMessageAsync($"从 {ConfigFilePath} 读取配置");
                    }
                    else
                    {
                        OutputMessageAsync($"未找到配置文件，已加载默认配置。");
                        ShowNotification($"未找到配置文件，已加载默认配置。");
                    }
                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"读取配置文件失败。");
                ShowNotification($"读取配置文件失败。");

                Logger.Instance.AppendToErrorLog($"ErrorCode:0xA1>>>读取配置文件错误>>>错误信息：{ex.Message}");

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // 取消关闭事件
                this.Hide(); // 隐藏窗体
                ShowNotification($"程序已最小化到托盘。", true);
                notifyIcon1.Visible = true; // 显示托盘图标
            }
        }


        private void SaveConfig()
        {
            Settings.SaveToConfigFile(JsonConfigFilePath);
        }

        private void checkBox_startprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.CmdPath == "")
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
                if (Settings.GameDataPath == "")
                {
                    OutputMessageAsync($"请先选择游戏存档路径。");
                    labelForsave.Text = "[ 关闭 ]";
                    checkBox_save.Checked = false;
                }
                else if (Settings.BackupPath == "")
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
                OutputMessageAsync($"已选择存档备份路径为：{backupPathbox.Text}");
            }
        }

        private void rconPortbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void checkBox_reboot_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.CmdPath == "")
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

        private async void button2_Click(object sender, EventArgs e)
        {
            var info = await _rconCommandClient.Save();
            OutputMessageAsync($"{info}");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = await _rconCommandClient.ShutDown(TimeSpan.FromSeconds(10), $"The server will restart in 10 seconds.");

                if (result)
                {

                    OutputMessageAsync($"The server will restart in 10 seconds.");
                }
                else
                {
                    OutputMessageAsync($"关服指令发送失败。");
                    await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息");
                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"关服指令发送失败。");
                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息：{ex.Message}");
            }

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var info = "";
            try
            {
                info = await _rconCommandClient.GetServerInformation();
                int startIndex = info.IndexOf("[") + 1;
                int endIndex = info.IndexOf("]");
                string version = info.Substring(startIndex, endIndex - startIndex);
                int lastIndex = info.IndexOf("] ");
                string serverName = info.Substring(lastIndex + 2);
                labelForservername.Text = $"服务器名称：{serverName}";
                versionLabel.Text = $"服务端版本：{version}";
                OutputMessageAsync($"当前服务端版本：{version}");

            }
            catch (Exception ex)
            {
                OutputMessageAsync($"发送命令时发生错误。");

                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x68>>>处理服务端版本信息错误>>>返回值为[{info}]>>>错误信息：{ex.Message}");

            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = textBox1.Text.Replace(" ", "_");
                bool info = await _rconCommandClient.Broadcast(textBox1.Text.Trim());
                if (info)
                {
                    OutputMessageAsync($"Broadcasted message: {textBox1.Text.Trim()}");
                }
                else
                {
                    OutputMessageAsync($"broadcast指令发送失败。");
                    await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息");
                }
            }

            catch (Exception ex)
            {
                OutputMessageAsync($"broadcast指令发送失败。");
                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息：{ex.Message}");

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = "https://github.com/KirosHan/Palworld-server-protector-DotNet";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch
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
                OutputMessageAsync($"已选择游戏存档路径为：{Settings.GameDataPath}");
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

        private void rebootSecondbox_ValueChanged(object sender, EventArgs e)
        {
            OutputMessageAsync($"重启延迟已设置为：{Settings.RebootSeconds}秒");
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

        private void memTargetbox_ValueChanged(object sender, EventArgs e)
        {
            OutputMessageAsync($"内存阈值已调整为：{memTargetbox.Value}%");
        }

        private void playersView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (playersView.SelectedItems.Count > 0)
            {

                string Uid = playersView.SelectedItems[0].SubItems[1].Text;
                string Steamid = playersView.SelectedItems[0].SubItems[2].Text;
                string Name = playersView.SelectedItems[0].SubItems[0].Text;
                if (comboBox_id.SelectedIndex == 0)
                {
                    UIDBox.Text = Uid;
                }
                else if (comboBox_id.SelectedIndex == 1)
                {
                    UIDBox.Text = Steamid;
                }
                else if (comboBox_id.SelectedIndex == 2)
                {
                    UIDBox.Text = Name;
                }
                else
                {
                    UIDBox.Text = Uid;
                }


            }
        }

        private async void kickbutton_Click(object sender, EventArgs e)
        {
            try
            {
                bool info = await _rconCommandClient.KickPlayer(long.Parse(UIDBox.Text.Trim()));
                if (info)
                {
                    OutputMessageAsync($"Kicked player {UIDBox.Text.Trim()}");

                }
                else
                {
                    OutputMessageAsync($"Kickplayer指令发送失败。");
                    await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息");
                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"Kickplayer指令发送失败。");
                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息：{ex.Message}");
            }
        }

        private async void banbutton_Click(object sender, EventArgs e)
        {
            try
            {
                bool info = await _rconCommandClient.BanPlayer(long.Parse(UIDBox.Text.Trim()));
                if (info)
                {
                    OutputMessageAsync($"Ban player {UIDBox.Text.Trim()}");

                }
                else
                {
                    OutputMessageAsync($"Banplayer指令发送失败。");
                    await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息");
                }
            }
            catch (Exception ex)
            {
                OutputMessageAsync($"Banplayer指令发送失败。");
                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x69>>>指令发送错误>>>错误信息：{ex.Message}");
            }
        }


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
                await Logger.Instance.AppendToErrorLogAsync($"ErrorCode:0x71>>>Webhook发送错误>>>相关参数：title=[{title}],message=[{message}]>>>错误信息：{ex.Message}");
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


        private void ShowNotification(string message, Boolean forced = false)
        {
            if (!forced)
            {
                if (checkBox_Noti.Checked)
                {
                    notifyIcon1.BalloonTipText = message;
                    notifyIcon1.ShowBalloonTip(2000);
                }
            }
            else
            {
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
            catch
            {
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveConfig();

            Application.Exit();
        }

        private void checkBox_mem_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_mem.Checked)
            {
                OutputMessageAsync($"已启用输出内存占用数据到回显。");
            }
            else
            {
                OutputMessageAsync($"已停用输出内存占用数据到回显。");
            }
        }


        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            var width = groupBox1.Width + tabControl1.Width + 50;
            if (this.Width < width)
            {
                this.Width = width;
                button5.Text = "<<";
            }
            else if (this.Width >= width)
            {
                this.Width = groupBox1.Width + 40;
                button5.Text = ">>";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }

  
            this.Close();
            Form myForm = new Form1(); 
            myForm.Show();

        }

        private void setLocalizationUI()
        {
          


        }

    }
}
