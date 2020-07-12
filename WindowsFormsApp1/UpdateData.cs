using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading;
using FGOServantBasicInformationAnalyzer;
using FGOServantBasicInformationAnalyzer.Properties;
using WindowsFormsApp1;

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
            Thread HTTPReq = new Thread(HttpRequestData);
            HTTPReq.Start();
        }
        private void UpdateData_FormClosing(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void HttpRequestData()
        {
            listBox1.Items.Clear();
            progressBar1.Value = 0;
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            progressBar1.Value = progressBar1.Value + 500;
            if (!System.IO.Directory.Exists(folder.FullName))
            {
                listBox1.Items.Add("正在创建Android目录...");
                listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                System.IO.Directory.CreateDirectory(folder.FullName);
                if (File.Exists(gamedata.FullName + "webview") || File.Exists(gamedata.FullName + "raw") || File.Exists(gamedata.FullName + "assetbundle") || File.Exists(gamedata.FullName + "webview") || File.Exists(gamedata.FullName + "master"))
                {
                    FileSystemInfo[] fileinfo = folder.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                            listBox1.Items.Add("删除: " + subdir);
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            continue;
                        }
                        i.Delete();
                        listBox1.Items.Add("删除: " + i);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    }
                    listBox1.Items.Add("开始下载/更新游戏数据......");
                    progressBar1.Value = progressBar1.Value + 500;
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    string result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=2.13.2");
                    JObject res = JObject.Parse(result);
                    if (res["response"][0]["fail"]["action"] != null)
                    {
                        if (res["response"][0]["fail"]["action"].ToString() == "app_version_up")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            tmp = Regex.Replace(tmp, @".*新ver.：(.*)、現.*", "$1", RegexOptions.Singleline);
                            listBox1.Items.Add("当前游戏版本: " + tmp.ToString());
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=" + tmp.ToString());
                            res = JObject.Parse(result);
                        }
                        else if (res["response"][0]["fail"]["action"].ToString() == "maint")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            if(MessageBox.Show("游戏服务器正在维护，请在维护后下载数据. \r\n以下为服务器公告内容:\r\n\r\n『" + tmp.Replace("[00FFFF]", "").Replace("[url=", "").Replace("][u]公式サイト お知らせ[/u][/url][-]", "") + "』\r\n\r\n点击\"确定\"可打开公告页面.", "维护中", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
                                MatchCollection mc = re.Matches(tmp);
                                foreach (Match m in mc)
                                {
                                    string url = m.Result("${url}");
                                    System.Diagnostics.Process.Start(url);
                                }
                            }
                            Application.ExitThread();
                            this.Close();
                            return;
                        }
                        else
                        {
                        }
                    }
                    if (!Directory.Exists(gamedata.FullName))
                        Directory.CreateDirectory(gamedata.FullName);
                    File.WriteAllText(gamedata.FullName + "raw", result);
                    File.WriteAllText(gamedata.FullName + "assetbundle", res["response"][0]["success"]["assetbundle"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "assetbundle");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "master", res["response"][0]["success"]["master"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "master");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "webview", res["response"][0]["success"]["webview"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data = File.ReadAllText(gamedata.FullName + "master");
                    if (!Directory.Exists(gamedata.FullName + "decrypted_masterdata"))
                        Directory.CreateDirectory(gamedata.FullName + "decrypted_masterdata");
                    Dictionary<string, byte[]> masterData = (Dictionary<string, byte[]>)MasterDataUnpacker.MouseGame2Unpacker(Convert.FromBase64String(data));
                    JObject job = new JObject();
                    MiniMessagePacker miniMessagePacker = new MiniMessagePacker();
                    foreach (KeyValuePair<string, byte[]> item in masterData)
                    {
                        List<object> unpackeditem = (List<object>)miniMessagePacker.Unpack(item.Value);
                        string json = JsonConvert.SerializeObject(unpackeditem, Formatting.Indented);
                        File.WriteAllText(gamedata.FullName + "decrypted_masterdata/" + item.Key, json);
                        listBox1.Items.Add("Writing file to: " + gamedata.FullName + "decrypted_masterdata/" + item.Key);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                        progressBar1.Value = progressBar1.Value + 38;
                    }
                    string data2 = File.ReadAllText(gamedata.FullName + "assetbundle");
                    Dictionary<string, object> dictionary = (Dictionary<string, object>)MasterDataUnpacker.MouseInfoMsgPack(Convert.FromBase64String(data2));
                    string str = null;
                    foreach (var a in dictionary)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "assetbundle.txt", str);
                    listBox1.Items.Add("folder name: " + dictionary["folderName"].ToString());
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data3 = File.ReadAllText(gamedata.FullName + "webview");
                    Dictionary<string, object> dictionary2 = (Dictionary<string, object>)MasterDataUnpacker.MouseGame2MsgPack(Convert.FromBase64String(data3));
                    string str2 = "baseURL: " + dictionary2["baseURL"].ToString() + "\r\n contactURL: " + dictionary2["contactURL"].ToString() + "\r\n";
                    listBox1.Items.Add(str2);
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    Dictionary<string, object> filePassInfo = (Dictionary<string, object>)dictionary2["filePass"];
                    foreach (var a in filePassInfo)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "webview.txt", str2);
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview.txt");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    listBox1.Items.Add("下载完成，可以开始解析.");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Maximum;
                    MessageBox.Show("下载完成，可以开始解析.", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.ExitThread();
                    this.Close();
                    return;
                }
                else
                {
                    FileSystemInfo[] fileinfo = folder.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                            listBox1.Items.Add("删除: " + subdir);
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            continue;
                        }
                        i.Delete();
                        listBox1.Items.Add("删除: " + i);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    }
                    listBox1.Items.Add("开始下载/更新游戏数据......");
                    progressBar1.Value = progressBar1.Value + 500;
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    string result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=2.13.2");
                    JObject res = JObject.Parse(result);
                    if (res["response"][0]["fail"]["action"] != null)
                    {
                        if (res["response"][0]["fail"]["action"].ToString() == "app_version_up")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            tmp = Regex.Replace(tmp, @".*新ver.：(.*)、現.*", "$1", RegexOptions.Singleline);
                            listBox1.Items.Add("当前游戏版本: " + tmp.ToString());
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=" + tmp.ToString());
                            res = JObject.Parse(result);
                        }
                        else if (res["response"][0]["fail"]["action"].ToString() == "maint")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            if (MessageBox.Show("游戏服务器正在维护，请在维护后下载数据. \r\n以下为服务器公告内容:\r\n\r\n『" + tmp.Replace("[00FFFF]", "").Replace("[url=", "").Replace("][u]公式サイト お知らせ[/u][/url][-]", "") + "』\r\n\r\n点击\"确定\"可打开公告页面.", "维护中", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
                                MatchCollection mc = re.Matches(tmp);
                                foreach (Match m in mc)
                                {
                                    string url = m.Result("${url}");
                                    System.Diagnostics.Process.Start(url);
                                }
                            }
                            Application.ExitThread();
                            this.Close();
                            return;
                        }
                        else
                        {
                        }
                    }
                    if (!Directory.Exists(gamedata.FullName))
                        Directory.CreateDirectory(gamedata.FullName);
                    File.WriteAllText(gamedata.FullName + "raw", result);
                    File.WriteAllText(gamedata.FullName + "assetbundle", res["response"][0]["success"]["assetbundle"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "assetbundle");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "master", res["response"][0]["success"]["master"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "master");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "webview", res["response"][0]["success"]["webview"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data = File.ReadAllText(gamedata.FullName + "master");
                    if (!Directory.Exists(gamedata.FullName + "decrypted_masterdata"))
                        Directory.CreateDirectory(gamedata.FullName + "decrypted_masterdata");
                    Dictionary<string, byte[]> masterData = (Dictionary<string, byte[]>)MasterDataUnpacker.MouseGame2Unpacker(Convert.FromBase64String(data));
                    JObject job = new JObject();
                    MiniMessagePacker miniMessagePacker = new MiniMessagePacker();
                    foreach (KeyValuePair<string, byte[]> item in masterData)
                    {
                        List<object> unpackeditem = (List<object>)miniMessagePacker.Unpack(item.Value);
                        string json = JsonConvert.SerializeObject(unpackeditem, Formatting.Indented);
                        File.WriteAllText(gamedata.FullName + "decrypted_masterdata/" + item.Key, json);
                        listBox1.Items.Add("Writing file to: " + gamedata.FullName + "decrypted_masterdata/" + item.Key);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                        progressBar1.Value = progressBar1.Value + 38;
                    }
                    string data2 = File.ReadAllText(gamedata.FullName + "assetbundle");
                    Dictionary<string, object> dictionary = (Dictionary<string, object>)MasterDataUnpacker.MouseInfoMsgPack(Convert.FromBase64String(data2));
                    string str = null;
                    foreach (var a in dictionary)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "assetbundle.txt", str);
                    listBox1.Items.Add("folder name: " + dictionary["folderName"].ToString());
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data3 = File.ReadAllText(gamedata.FullName + "webview");
                    Dictionary<string, object> dictionary2 = (Dictionary<string, object>)MasterDataUnpacker.MouseGame2MsgPack(Convert.FromBase64String(data3));
                    string str2 = "baseURL: " + dictionary2["baseURL"].ToString() + "\r\n contactURL: " + dictionary2["contactURL"].ToString() + "\r\n";
                    listBox1.Items.Add(str2);
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    Dictionary<string, object> filePassInfo = (Dictionary<string, object>)dictionary2["filePass"];
                    foreach (var a in filePassInfo)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "webview.txt", str2);
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview.txt");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    listBox1.Items.Add("下载完成，可以开始解析.");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Maximum;
                    MessageBox.Show("下载完成，可以开始解析.", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.ExitThread();
                    this.Close();
                    return;
                }
            }
            else
            {
                if (File.Exists(gamedata.FullName + "webview") || File.Exists(gamedata.FullName + "raw") || File.Exists(gamedata.FullName + "assetbundle") || File.Exists(gamedata.FullName + "webview") || File.Exists(gamedata.FullName + "master"))
                {
                    FileSystemInfo[] fileinfo = folder.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                            listBox1.Items.Add("删除: " + subdir);
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            continue;
                        }
                        i.Delete();
                        listBox1.Items.Add("删除: " + i);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    }
                    listBox1.Items.Add("开始下载/更新游戏数据......");
                    progressBar1.Value = progressBar1.Value + 500;
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    string result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=2.13.2");
                    JObject res = JObject.Parse(result);
                    if (res["response"][0]["fail"]["action"] != null)
                    {
                        if (res["response"][0]["fail"]["action"].ToString() == "app_version_up")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            tmp = Regex.Replace(tmp, @".*新ver.：(.*)、現.*", "$1", RegexOptions.Singleline);
                            listBox1.Items.Add("当前游戏版本: " + tmp.ToString());
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=" + tmp.ToString());
                            res = JObject.Parse(result);
                        }
                        else if (res["response"][0]["fail"]["action"].ToString() == "maint")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            if (MessageBox.Show("游戏服务器正在维护，请在维护后下载数据. \r\n以下为服务器公告内容:\r\n\r\n『" + tmp.Replace("[00FFFF]", "").Replace("[url=", "").Replace("][u]公式サイト お知らせ[/u][/url][-]", "") + "』\r\n\r\n点击\"确定\"可打开公告页面.", "维护中", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
                                MatchCollection mc = re.Matches(tmp);
                                foreach (Match m in mc)
                                {
                                    string url = m.Result("${url}");
                                    System.Diagnostics.Process.Start(url);
                                }
                            }
                            Application.ExitThread();
                            this.Close();
                            return;
                        }
                        else
                        {
                        }
                    }
                    if (!Directory.Exists(gamedata.FullName))
                        Directory.CreateDirectory(gamedata.FullName);
                    File.WriteAllText(gamedata.FullName + "raw", result);
                    File.WriteAllText(gamedata.FullName + "assetbundle", res["response"][0]["success"]["assetbundle"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "assetbundle");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "master", res["response"][0]["success"]["master"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "master");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "webview", res["response"][0]["success"]["webview"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data = File.ReadAllText(gamedata.FullName + "master");
                    if (!Directory.Exists(gamedata.FullName + "decrypted_masterdata"))
                        Directory.CreateDirectory(gamedata.FullName + "decrypted_masterdata");
                    Dictionary<string, byte[]> masterData = (Dictionary<string, byte[]>)MasterDataUnpacker.MouseGame2Unpacker(Convert.FromBase64String(data));
                    JObject job = new JObject();
                    MiniMessagePacker miniMessagePacker = new MiniMessagePacker();
                    foreach (KeyValuePair<string, byte[]> item in masterData)
                    {
                        List<object> unpackeditem = (List<object>)miniMessagePacker.Unpack(item.Value);
                        string json = JsonConvert.SerializeObject(unpackeditem, Formatting.Indented);
                        File.WriteAllText(gamedata.FullName + "decrypted_masterdata/" + item.Key, json);
                        listBox1.Items.Add("Writing file to: " + gamedata.FullName + "decrypted_masterdata/" + item.Key);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                        progressBar1.Value = progressBar1.Value + 38;
                    }
                    string data2 = File.ReadAllText(gamedata.FullName + "assetbundle");
                    Dictionary<string, object> dictionary = (Dictionary<string, object>)MasterDataUnpacker.MouseInfoMsgPack(Convert.FromBase64String(data2));
                    string str = null;
                    foreach (var a in dictionary)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "assetbundle.txt", str);
                    listBox1.Items.Add("folder name: " + dictionary["folderName"].ToString());
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data3 = File.ReadAllText(gamedata.FullName + "webview");
                    Dictionary<string, object> dictionary2 = (Dictionary<string, object>)MasterDataUnpacker.MouseGame2MsgPack(Convert.FromBase64String(data3));
                    string str2 = "baseURL: " + dictionary2["baseURL"].ToString() + "\r\n contactURL: " + dictionary2["contactURL"].ToString() + "\r\n";
                    listBox1.Items.Add(str2);
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    Dictionary<string, object> filePassInfo = (Dictionary<string, object>)dictionary2["filePass"];
                    foreach (var a in filePassInfo)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "webview.txt", str2);
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview.txt");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    listBox1.Items.Add("下载完成，可以开始解析.");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Maximum;
                    MessageBox.Show("下载完成，可以开始解析.", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.ExitThread();
                    this.Close();
                    return;
                }
                else
                {
                    FileSystemInfo[] fileinfo = folder.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                            listBox1.Items.Add("删除: " + subdir);
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            continue;
                        }
                        i.Delete();
                        listBox1.Items.Add("删除: " + i);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    }
                    listBox1.Items.Add("开始下载/更新游戏数据......");
                    progressBar1.Value = progressBar1.Value + 500;
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    string result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=2.13.2");
                    JObject res = JObject.Parse(result);
                    if (res["response"][0]["fail"]["action"] != null)
                    {
                        if (res["response"][0]["fail"]["action"].ToString() == "app_version_up")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            tmp = Regex.Replace(tmp, @".*新ver.：(.*)、現.*", "$1", RegexOptions.Singleline);
                            listBox1.Items.Add("当前游戏版本: " + tmp.ToString());
                            listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                            result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=" + tmp.ToString());
                            res = JObject.Parse(result);
                        }
                        else if (res["response"][0]["fail"]["action"].ToString() == "maint")
                        {
                            string tmp = res["response"][0]["fail"]["detail"].ToString();
                            if (MessageBox.Show("游戏服务器正在维护，请在维护后下载数据. \r\n以下为服务器公告内容:\r\n\r\n『" + tmp.Replace("[00FFFF]", "").Replace("[url=", "").Replace("][u]公式サイト お知らせ[/u][/url][-]", "") + "』\r\n\r\n点击\"确定\"可打开公告页面.", "维护中", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
                                MatchCollection mc = re.Matches(tmp);
                                foreach (Match m in mc)
                                {
                                    string url = m.Result("${url}");
                                    System.Diagnostics.Process.Start(url);
                                }
                            }
                            Application.ExitThread();
                            this.Close();
                            return;
                        }
                        else
                        {
                        }
                    }
                    if (!Directory.Exists(gamedata.FullName))
                        Directory.CreateDirectory(gamedata.FullName);
                    File.WriteAllText(gamedata.FullName + "raw", result);
                    File.WriteAllText(gamedata.FullName + "assetbundle", res["response"][0]["success"]["assetbundle"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "assetbundle");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "master", res["response"][0]["success"]["master"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "master");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    File.WriteAllText(gamedata.FullName + "webview", res["response"][0]["success"]["webview"].ToString());
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data = File.ReadAllText(gamedata.FullName + "master");
                    if (!Directory.Exists(gamedata.FullName + "decrypted_masterdata"))
                        Directory.CreateDirectory(gamedata.FullName + "decrypted_masterdata");
                    Dictionary<string, byte[]> masterData = (Dictionary<string, byte[]>)MasterDataUnpacker.MouseGame2Unpacker(Convert.FromBase64String(data));
                    JObject job = new JObject();
                    MiniMessagePacker miniMessagePacker = new MiniMessagePacker();
                    foreach (KeyValuePair<string, byte[]> item in masterData)
                    {
                        List<object> unpackeditem = (List<object>)miniMessagePacker.Unpack(item.Value);
                        string json = JsonConvert.SerializeObject(unpackeditem, Formatting.Indented);
                        File.WriteAllText(gamedata.FullName + "decrypted_masterdata/" + item.Key, json);
                        listBox1.Items.Add("Writing file to: " + gamedata.FullName + "decrypted_masterdata/" + item.Key);
                        listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                        progressBar1.Value = progressBar1.Value + 38;
                    }
                    string data2 = File.ReadAllText(gamedata.FullName + "assetbundle");
                    Dictionary<string, object> dictionary = (Dictionary<string, object>)MasterDataUnpacker.MouseInfoMsgPack(Convert.FromBase64String(data2));
                    string str = null;
                    foreach (var a in dictionary)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "assetbundle.txt", str);
                    listBox1.Items.Add("folder name: " + dictionary["folderName"].ToString());
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    string data3 = File.ReadAllText(gamedata.FullName + "webview");
                    Dictionary<string, object> dictionary2 = (Dictionary<string, object>)MasterDataUnpacker.MouseGame2MsgPack(Convert.FromBase64String(data3));
                    string str2 = "baseURL: " + dictionary2["baseURL"].ToString() + "\r\n contactURL: " + dictionary2["contactURL"].ToString() + "\r\n";
                    listBox1.Items.Add(str2);
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Value + 38;
                    Dictionary<string, object> filePassInfo = (Dictionary<string, object>)dictionary2["filePass"];
                    foreach (var a in filePassInfo)
                    {
                        str += a.Key + ": " + a.Value.ToString() + "\r\n";
                    }
                    File.WriteAllText(gamedata.FullName + "webview.txt", str2);
                    listBox1.Items.Add("Writing file to: " + gamedata.FullName + "webview.txt");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    listBox1.Items.Add("下载完成，可以开始解析.");
                    listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    progressBar1.Value = progressBar1.Maximum;
                    MessageBox.Show("下载完成，可以开始解析.", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.ExitThread();
                    this.Close();
                    return;
                }
            }
        }
    }
}
