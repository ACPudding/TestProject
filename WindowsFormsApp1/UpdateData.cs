using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FGOServantBasicInformationAnalyzer
{
    public partial class UpdateData : Form
    {
        public UpdateData()
        {
            InitializeComponent();
        }

        private void UpdateData_Load(object sender, EventArgs e)
        {
            var HTTPReq = new Thread(HttpRequestData);
            HTTPReq.Start();
        }

        private void UpdateData_FormClosing(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void HttpRequestData()
        {
            listBox1.Items.Clear();
            progressBar1.Value = 0;
            var path = Directory.GetCurrentDirectory();
            var gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            var folder = new DirectoryInfo(path + @"\Android\");
            progressBar1.Value = progressBar1.Value + 250;
            if (!Directory.Exists(folder.FullName))
            {
                listBox1.Items.Add("正在创建Android目录...");
                listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
                Directory.CreateDirectory(folder.FullName);
            }

            listBox1.Items.Add("开始下载/更新游戏数据......");
            progressBar1.Value = progressBar1.Value + 250;
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            var result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=2.15.0");
            var res = JObject.Parse(result);
            if (res["response"][0]["fail"]["action"] != null)
                switch (res["response"][0]["fail"]["action"].ToString())
                {
                    case "app_version_up":
                    {
                        var tmp = res["response"][0]["fail"]["detail"].ToString();
                        tmp = Regex.Replace(tmp, @".*新ver.：(.*)、現.*", "$1", RegexOptions.Singleline);
                        listBox1.Items.Add("当前游戏版本: " + tmp);
                        listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
                        result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=" + tmp);
                        res = JObject.Parse(result);
                        break;
                    }
                    case "maint":
                    {
                        var tmp = res["response"][0]["fail"]["detail"].ToString();
                        if (MessageBox.Show(
                            "游戏服务器正在维护，请在维护后下载数据. \r\n以下为服务器公告内容:\r\n\r\n『" +
                            tmp.Replace("[00FFFF]", "").Replace("[url=", "")
                                .Replace("][u]公式サイト お知らせ[/u][/url][-]", "") + "』\r\n\r\n点击\"确定\"可打开公告页面.",
                            "维护中", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            var re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
                            var mc = re.Matches(tmp);
                            foreach (Match m in mc)
                            {
                                var url = m.Result("${url}");
                                Process.Start(url);
                            }
                        }

                        Application.ExitThread();
                        Close();
                        return;
                    }
                }

            if (File.Exists(gamedata.FullName + "webview") || File.Exists(gamedata.FullName + "raw") ||
                File.Exists(gamedata.FullName + "assetbundle") || File.Exists(gamedata.FullName + "webview") ||
                File.Exists(gamedata.FullName + "master"))
            {
                var fileinfo = folder.GetFileSystemInfos(); //返回目录中所有文件和子目录
                foreach (var i in fileinfo)
                {
                    if (i is DirectoryInfo) //判断是否文件夹
                    {
                        var subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true); //删除子目录和文件
                        listBox1.Items.Add("删除: " + subdir);
                        listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
                        continue;
                    }

                    i.Delete();
                    listBox1.Items.Add("删除: " + i);
                    listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
                }
            }

            if (!Directory.Exists(gamedata.FullName))
                Directory.CreateDirectory(gamedata.FullName);
            File.WriteAllText(gamedata.FullName + "raw", result);
            File.WriteAllText(gamedata.FullName + "assetbundle",
                res["response"][0]["success"]["assetbundle"].ToString());
            listBox1.Items.Add("Writing file to: " + gamedata.FullName + "assetbundle");
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            progressBar1.Value = progressBar1.Value + 40;
            File.WriteAllText(gamedata.FullName + "master", res["response"][0]["success"]["master"].ToString());
            listBox1.Items.Add("Writing file to: " + gamedata.FullName + "master");
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            progressBar1.Value = progressBar1.Value + 40;
            File.WriteAllText(gamedata.FullName + "webview",
                res["response"][0]["success"]["webview"].ToString());
            listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview");
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            progressBar1.Value = progressBar1.Value + 40;
            var data = File.ReadAllText(gamedata.FullName + "master");
            if (!Directory.Exists(gamedata.FullName + "decrypted_masterdata"))
                Directory.CreateDirectory(gamedata.FullName + "decrypted_masterdata");
            var masterData =
                (Dictionary<string, byte[]>) MasterDataUnpacker.MouseGame2Unpacker(
                    Convert.FromBase64String(data));
            var job = new JObject();
            var miniMessagePacker = new MiniMessagePacker();
            foreach (var item in masterData)
            {
                var unpackeditem = (List<object>) miniMessagePacker.Unpack(item.Value);
                var json = JsonConvert.SerializeObject(unpackeditem, Formatting.Indented);
                File.WriteAllText(gamedata.FullName + "decrypted_masterdata/" + item.Key, json);
                listBox1.Items.Add("Writing file to: " + gamedata.FullName + "decrypted_masterdata/" +
                                   item.Key);
                listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
                progressBar1.Value = progressBar1.Value + 40;
            }

            var data2 = File.ReadAllText(gamedata.FullName + "assetbundle");
            var dictionary =
                (Dictionary<string, object>) MasterDataUnpacker.MouseInfoMsgPack(
                    Convert.FromBase64String(data2));
            var str = dictionary.Aggregate<KeyValuePair<string, object>, string>(null,
                (current, a) => current + a.Key + ": " + a.Value + "\r\n");
            File.WriteAllText(gamedata.FullName + "assetbundle.txt", str);
            listBox1.Items.Add("folder name: " + dictionary["folderName"]);
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            progressBar1.Value = progressBar1.Value + 40;
            var data3 = File.ReadAllText(gamedata.FullName + "webview");
            var dictionary2 =
                (Dictionary<string, object>) MasterDataUnpacker.MouseGame2MsgPack(
                    Convert.FromBase64String(data3));
            var str2 = "baseURL: " + dictionary2["baseURL"] + "\r\n contactURL: " + dictionary2["contactURL"] +
                       "\r\n";
            listBox1.Items.Add(str2);
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            progressBar1.Value = progressBar1.Value + 40;
            var filePassInfo = (Dictionary<string, object>) dictionary2["filePass"];
            str = filePassInfo.Aggregate(str, (current, a) => current + a.Key + ": " + a.Value + "\r\n");
            File.WriteAllText(gamedata.FullName + "webview.txt", str2);
            listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview.txt");
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            listBox1.Items.Add("下载完成，可以开始解析.");
            listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
            progressBar1.Value = progressBar1.Maximum;
            MessageBox.Show("下载完成，可以开始解析.", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.ExitThread();
            Close();
        }
    }
}