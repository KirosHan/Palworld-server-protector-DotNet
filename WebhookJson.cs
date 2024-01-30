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
        public string GenerateJson(string url, string jsontitle, string message)
        {
            // 判断URL是否包含特定字符串
            if (url.Contains("weixin.qq"))
            {
                var markdownObject = new
                {
                    msgtype = "markdown",
                    markdown = new
                    {
                        content = $"<font color=\"warning\">通知 </font>{jsontitle}\n ><font color=\"comment\">{message}</font>"
                    }
                };

                return JsonConvert.SerializeObject(markdownObject);
            }
            else if (url.Contains("dingtalk.com"))
            {
                var markdownObject = new
                {
                    msgtype = "markdown",
                    markdown = new
                    {
                        title = jsontitle,
                        text = $"#### {jsontitle} \n > {message}\n > \n "
                    }
                };

                return JsonConvert.SerializeObject(markdownObject);
            }
            else
            {
                var unknownObject = new
                {
                    msgtype = "normal",
                    data =new
                    {
                        title = jsontitle,
                        content = message
                    }
                };

                return JsonConvert.SerializeObject(unknownObject);
            }
        }
    }
               
            
        
    
}
