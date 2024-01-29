using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Palworld_server_protector_DotNet
{
    internal class WebhookJson
    {
        public string GenerateJson(string url, string title, string message)
        {
            // 判断URL是否包含特定字符串
            if (url.Contains("weixin.qq"))
            {
                var markdownObject = new
                {
                    msgtype = "markdown",
                    markdown = new
                    {
                        content = $"<font color=\"warning\">通知 </font>{title}\n ><font color=\"comment\">{message}</font>"
                    }
                };

                return JsonConvert.SerializeObject(markdownObject);
            }
            else
            {
                var unknownObject = new
                {
                    msgtype = "unknow"
                };

                return JsonConvert.SerializeObject(unknownObject);
            }
        }
    }
}
