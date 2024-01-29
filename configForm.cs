using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Palworld_server_protector_DotNet
{


    public partial class configForm : Form
    {

        public configForm()
        {
            InitializeComponent();
        }

        List<ConfigItem> ParseConfigFile(string content)
        {
            var configItems = new List<ConfigItem>();
            var translator = new ConfigTranslation();
            var noter = new Notes();
            // 提取OptionSettings部分
            var optionSettingsPrefix = "OptionSettings=(";
            var startIndex = content.IndexOf(optionSettingsPrefix);
            if (startIndex != -1)
            {
                startIndex += optionSettingsPrefix.Length;
                var endIndex = content.IndexOf(')', startIndex);
                if (endIndex != -1)
                {
                    var settings = content.Substring(startIndex, endIndex - startIndex);
                    var lines = settings.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            configItems.Add(new ConfigItem
                            {
                                中文 = translator.GetTranslation(parts[0].Trim()),
                                配置项 = parts[0].Trim(),
                                值 = parts[1].Trim(),
                                备注 = noter.GetNote(parts[0].Trim())
                            });
                        }
                    }
                }
            }
            return configItems;
        }




        private string ReadFileContent(string filePath)
        {
            string content = string.Empty;
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch (IOException ex)
            {
                // Handle the exception or display an error message
                MessageBox.Show($"读取文件失败: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return content;
        }

        private void SaveConfigItemsToFile(string filePath, List<ConfigItem> items)
        {
            var sb = new StringBuilder();
            sb.Append("[/Script/Pal.PalGameWorldSettings]\nOptionSettings=(");

            foreach (var item in items)
            {
                sb.Append($"{item.配置项}={item.值},");
            }

            // 移除最后一个逗号
            if (sb.Length > 0)
            {
                sb.Length--;
            }

            sb.Append(")");

            try
            {
                File.WriteAllText(filePath, sb.ToString());
                MessageBox.Show("配置文件保存成功！", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"配置文件保存失败: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "配置文件|*.ini";
            openFileDialog.Title = "选择配置文件";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string filePath = openFileDialog.FileName;
            toolStripTextBox1.Text = filePath;
            string fileContent = ReadFileContent(filePath);
            // 设置数据源
            dataGridView.DataSource = ParseConfigFile(fileContent);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置其他列为只读
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                if (i != 2)
                {
                    dataGridView.Columns[i].ReadOnly = true;
                }
            }




        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource is List<ConfigItem> configItems)
            {
                var filePath = toolStripTextBox1.Text;
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    SaveConfigItemsToFile(filePath, configItems);
                }
                else
                {
                    MessageBox.Show("No file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
