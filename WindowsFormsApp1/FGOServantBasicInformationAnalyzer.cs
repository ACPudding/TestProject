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
using MaterialSkin.Controls;
using MaterialSkin;

namespace WindowsFormsApp1
{
    public partial class FGOServantBasicInformationAnalyzer : MaterialForm
    {
        public FGOServantBasicInformationAnalyzer()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent1()
        {
            this.SuspendLayout();
            // 
            // FGOServantBasicInformationAnalyzer
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "FGOServantBasicInformationAnalyzer";
            this.ResumeLayout(false);

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateData UD = new UpdateData();
            UD.ShowDialog();
        }

        private void FGOServantBasicInformationAnalyzer_Load(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {


                if (c is TextBox)
                {


                    ((TextBox)c).Font = new System.Drawing.Font("FOT - Fate_Go Skip B", 10.5F);
                    ((TextBox)c).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

                }
            }
            SkillLvs.EastenEggCount = 0;
            /*textBox31.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            textBox32.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            textBox33.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            textBox34.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            textBox35.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            textBox36.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            textBox37.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;*/
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ThreadStart childref2 = new ThreadStart(AnalyzeServant);
            Thread childThread2 = new Thread(childref2);
            childThread2.Start();
        }
        public void AnalyzeServant()
        {
            SkillLvs.EEB = false;
            button5.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            textBox1.Text = Regex.Replace(textBox1.Text, @"\s", "");
            string svtID = Convert.ToString(textBox1.Text);
            if (!System.IO.Directory.Exists(gamedata.FullName))
            {
                MessageBox.Show("没有游戏数据,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button5.Enabled = true;
                return;
            }
            else
            {
                if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvt") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtLimit") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstCv") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstIllustrator") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtCard") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDevice") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtTreasureDevice") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceDetail") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkill") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkillDetail") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtSkill"))
                {
                    MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button5.Enabled = true;
                    return;
                }
            }
            if (!Regex.IsMatch(svtID, "^\\d+$"))
            {
                MessageBox.Show("从者ID输入错误,请检查.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button5.Enabled = true;
                return;
            }
            string mstSvt = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvt");
            string mstSvtLimit = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvtLimit");
            string mstCv = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstCv");
            string mstIllustrator = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstIllustrator");
            string mstSvtCard = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvtCard");
            string mstSvtTreasureDevice = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvtTreasureDevice");
            string mstTreasureDevice = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDevice");
            string mstTreasureDeviceDetail = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceDetail");
            string mstSkill = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSkill");
            string mstSvtSkill = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvtSkill");
            string mstSkillDetail = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSkillDetail");
            JArray mstSvtArray = (JArray)JsonConvert.DeserializeObject(mstSvt);
            JArray mstSvtLimitArray = (JArray)JsonConvert.DeserializeObject(mstSvtLimit);
            JArray mstCvArray = (JArray)JsonConvert.DeserializeObject(mstCv);
            JArray mstIllustratorArray = (JArray)JsonConvert.DeserializeObject(mstIllustrator);
            JArray mstSvtCardArray = (JArray)JsonConvert.DeserializeObject(mstSvtCard);
            JArray mstSvtTreasureDevicedArray = (JArray)JsonConvert.DeserializeObject(mstSvtTreasureDevice);
            JArray mstTreasureDevicedArray = (JArray)JsonConvert.DeserializeObject(mstTreasureDevice);
            JArray mstTreasureDeviceDetailArray = (JArray)JsonConvert.DeserializeObject(mstTreasureDeviceDetail);
            JArray mstSkillArray = (JArray)JsonConvert.DeserializeObject(mstSkill);
            JArray mstSvtSkillArray = (JArray)JsonConvert.DeserializeObject(mstSvtSkill);
            JArray mstSkillDetailArray = (JArray)JsonConvert.DeserializeObject(mstSkillDetail);
            if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceLv"))
            {
                MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mstTreasureDeviceLv = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceLv");
            JArray mstTreasureDeviceLvArray = (JArray)JsonConvert.DeserializeObject(mstTreasureDeviceLv);
            string[] PPK = new string[100];
            PPK[11] = "A"; PPK[12] = "A+"; PPK[13] = "A++"; PPK[14] = "A-"; PPK[21] = "B"; PPK[22] = "B+"; PPK[23] = "B++"; PPK[24] = "B-"; PPK[31] = "C"; PPK[32] = "C+"; PPK[33] = "C++"; PPK[34] = "C-"; PPK[41] = "D"; PPK[42] = "D+"; PPK[43] = "D++"; PPK[44] = "D-"; PPK[51] = "E"; PPK[52] = "E+"; PPK[53] = "E++"; PPK[54] = "E-"; PPK[61] = "EX"; PPK[98] = "-"; PPK[0] = "-";
            var svtName = "";
            var svtNameDisplay = "unknown";
            string[] ClassName = new string[1500];
            ClassName[1] = "Saber"; ClassName[2] = "Archer"; ClassName[3] = "Lancer"; ClassName[4] = "Rider"; ClassName[5] = "Caster"; ClassName[6] = "Assassin"; ClassName[7] = "Berserker"; ClassName[8] = "Shielder"; ClassName[9] = "Ruler"; ClassName[10] = "Alterego"; ClassName[11] = "Avenger"; ClassName[23] = "MoonCancer"; ClassName[25] = "Foreigner"; ClassName[20] = "Beast II"; ClassName[22] = "Beast I"; ClassName[24] = "Beast III/R"; ClassName[26] = "Beast III/L"; ClassName[27] = "Beast ?"; ClassName[97] = "不明"; ClassName[1001] = "礼装";
            ClassName[107] = "Berserker"; ClassName[21] = "?"; ClassName[19] = "?"; ClassName[18] = "?"; ClassName[17] = "Grand Caster"; ClassName[16] = "?"; ClassName[15] = "?"; ClassName[14] = "?"; ClassName[13] = "?"; ClassName[12] = "?";
            var svtClass = "unknown";//ClassID
            var svtgender = "unknown";
            string[] gender = new string[4];
            gender[1] = "男性"; gender[2] = "女性"; gender[3] = "其他";
            double[] nprateclassbase = new double[150];
            nprateclassbase[1] = 1.5; nprateclassbase[2] = 1.55; nprateclassbase[3] = 1.45; nprateclassbase[4] = 1.55; nprateclassbase[5] = 1.6; nprateclassbase[6] = 1.45; nprateclassbase[7] = 1.4; nprateclassbase[8] = 1.5; nprateclassbase[9] = 1.5; nprateclassbase[10] = 1.55; nprateclassbase[11] = 1.45; nprateclassbase[23] = 1.6; nprateclassbase[25] = 1.5; nprateclassbase[20] = 0.0; nprateclassbase[22] = 0.0; nprateclassbase[24] = 0.0; nprateclassbase[26] = 0.0; nprateclassbase[27] = 0.0; nprateclassbase[97] = 0.0;
            nprateclassbase[107] = 0.0; nprateclassbase[21] = 0.0; nprateclassbase[19] = 0.0; nprateclassbase[18] = 0.0; nprateclassbase[17] = 0.0; nprateclassbase[16] = 0.0; nprateclassbase[15] = 0.0; nprateclassbase[14] = 0.0; nprateclassbase[13] = 0.0; nprateclassbase[12] = 0.0;
            double[] nprateartscount = new double[4];
            nprateartscount[1] = 1.5; nprateartscount[2] = 1.125; nprateartscount[3] = 1;
            double[] npratemagicbase = new double[100];
            npratemagicbase[11] = 1.02; npratemagicbase[12] = 1.025; npratemagicbase[13] = 1.03; npratemagicbase[14] = 1.015; npratemagicbase[21] = 1; npratemagicbase[22] = 1.005; npratemagicbase[23] = 1.01; npratemagicbase[24] = 0.995; npratemagicbase[31] = 0.99; npratemagicbase[32] = 0.9925; npratemagicbase[33] = 0.995; npratemagicbase[34] = 0.985; npratemagicbase[41] = 0.98; npratemagicbase[42] = 0.9825; npratemagicbase[43] = 0.985; npratemagicbase[44] = 0.975; npratemagicbase[51] = 0.97; npratemagicbase[52] = 0.9725; npratemagicbase[53] = 0.975; npratemagicbase[54] = 0.965; npratemagicbase[61] = 1.04; npratemagicbase[0] = 0.0; npratemagicbase[99] = 0.0;
            var svtstarrate = "unknown";
            double NPrate = 0;
            float starrate = 0;
            float deathrate = 0;
            var svtdeathrate = "unknown";
            var svtillust = "unknown";//illustID 不输出
            var svtcv = "unknown"; ;//CVID 不输出
            var svtcollectionid = "unknown";
            var svtCVName = "unknown";
            var svtILLUSTName = "unknown";
            var svtrarity = "unknown";
            var svthpBase = "unknown";
            var svthpMax = "unknown";
            var svtatkBase = "unknown";
            var svtatkMax = "unknown";
            var svtcriticalWeight = "unknown";
            var svtpower = "unknown";
            var svtdefense = "unknown";
            var svtagility = "unknown";
            var svtmagic = "unknown";
            var svtluck = "unknown";
            var svttreasureDevice = "unknown";
            var svtTDID = "unknown";
            var svtHideAttri = "unknown";
            var CardArrange = "unknown";
            double NPRateTD = 0.00;
            double NPRateArts = 0.00;
            double NPRateBuster = 0.00;
            double NPRateQuick = 0.00;
            double NPRateEX = 0.00;
            double NPRateDef = 0.00;
            string svtClassPassiveID;
            string[] svtClassPassiveIDArray = new string[] { };
            List<string> svtClassPassiveIDList;
            List<string> svtClassPassiveList = new List<string> { };
            string[] svtClassPassiveArray;
            string svtClassPassive = String.Empty;
            int svtArtsCardhit = 1;
            var svtArtsCardhitDamage = "unknown";
            int svtArtsCardQuantity = 0;
            int svtBustersCardhit = 1;
            var svtBustersCardhitDamage = "unknown";
            int svtQuicksCardhit = 1;
            var svtQuicksCardhitDamage = "unknown";
            int svtExtraCardhit = 1;
            var svtExtraCardhitDamage = "unknown";
            int svtNPCardhit = 1;
            var svtNPCardhitDamage = "-";
            var svtNPCardType = "unknown";
            var svtNPDamageType = "unknown";
            var NPName = "unknown";
            var NPruby = "unknown";
            var NPtypeText = "unknown";
            var NPrank = "unknown";
            var NPDetail = "unknown";
            var skill1Name = "unknown";
            var skill1detail = "unknown";
            var skill1ID = "unknown";
            var skill2Name = "unknown";
            var skill2detail = "unknown";
            var skill2ID = "unknown";
            var skill3Name = "unknown";
            var skill3detail = "unknown";
            var skill3ID = "unknown";
            int classData = 0;
            int powerData = 0;
            int defenseData = 0;
            int agilityData = 0;
            int magicData = 0;
            int luckData = 0;
            int TreasureData = 0;
            int genderData = 0;
            bool check = true;
            foreach (var svtIDtmp in mstSvtArray) //查找某个字段与值
            {
                if (((JObject)svtIDtmp)["id"].ToString() == svtID)
                {
                    var mstSvtobjtmp = JObject.Parse(svtIDtmp.ToString());
                    svtName = mstSvtobjtmp["name"].ToString();
                    SkillLvs.svtnme = svtName;
                    svtNameDisplay = mstSvtobjtmp["battleName"].ToString();
                    svtClass = mstSvtobjtmp["classId"].ToString();
                    svtgender = mstSvtobjtmp["genderType"].ToString();
                    svtstarrate = mstSvtobjtmp["starRate"].ToString();
                    svtdeathrate = mstSvtobjtmp["deathRate"].ToString();
                    svtillust = mstSvtobjtmp["illustratorId"].ToString();//illustID
                    svtcv = mstSvtobjtmp["cvId"].ToString();//CVID
                    svtcollectionid = mstSvtobjtmp["collectionNo"].ToString();
                    svtClassPassiveID = mstSvtobjtmp["classPassive"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "").Replace("[", "").Replace("]", "");
                    svtClassPassiveIDList = new List<string>(svtClassPassiveID.Split(','));
                    svtClassPassiveIDArray = svtClassPassiveIDList.ToArray();
                    svtHideAttri = mstSvtobjtmp["attri"].ToString().Replace("1", "人").Replace("2", "天").Replace("3", "地").Replace("4", "星").Replace("5", "兽");
                    CardArrange = mstSvtobjtmp["cardIds"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "").Replace("2", "B").Replace("1", "A").Replace("3", "Q");
                    foreach (char c in CardArrange)
                    {
                        if (c == 'A')
                            svtArtsCardQuantity++;
                    }
                    classData = int.Parse(svtClass);
                    genderData = int.Parse(svtgender);
                    starrate = float.Parse(svtstarrate) / 10;
                    deathrate = float.Parse(svtdeathrate) / 10;
                    break;
                }
            }
            foreach (var svtCardtmp in mstSvtCardArray) //查找某个字段与值
            {
                if (((JObject)svtCardtmp)["svtId"].ToString() == svtID && ((JObject)svtCardtmp)["cardId"].ToString() == "1")
                {
                    var mstSvtCardobjtmp = JObject.Parse(svtCardtmp.ToString());
                    svtArtsCardhitDamage = mstSvtCardobjtmp["normalDamage"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "");
                    foreach (char c in svtArtsCardhitDamage)
                    {
                        if (c == ',')
                            svtArtsCardhit++;
                    }
                }
                if (((JObject)svtCardtmp)["svtId"].ToString() == svtID && ((JObject)svtCardtmp)["cardId"].ToString() == "2")
                {
                    var mstSvtCardobjtmp = JObject.Parse(svtCardtmp.ToString());
                    svtBustersCardhitDamage = mstSvtCardobjtmp["normalDamage"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "");
                    foreach (char c in svtBustersCardhitDamage)
                    {
                        if (c == ',')
                            svtBustersCardhit++;
                    }
                }
                if (((JObject)svtCardtmp)["svtId"].ToString() == svtID && ((JObject)svtCardtmp)["cardId"].ToString() == "3")
                {
                    var mstSvtCardobjtmp = JObject.Parse(svtCardtmp.ToString());
                    svtQuicksCardhitDamage = mstSvtCardobjtmp["normalDamage"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "");
                    foreach (char c in svtQuicksCardhitDamage)
                    {
                        if (c == ',')
                            svtQuicksCardhit++;
                    }
                }
                if (((JObject)svtCardtmp)["svtId"].ToString() == svtID && ((JObject)svtCardtmp)["cardId"].ToString() == "4")
                {
                    var mstSvtCardobjtmp = JObject.Parse(svtCardtmp.ToString());
                    svtExtraCardhitDamage = mstSvtCardobjtmp["normalDamage"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "");
                    foreach (char c in svtExtraCardhitDamage)
                    {
                        if (c == ',')
                            svtExtraCardhit++;
                    }
                }
            }
            foreach (var cvidtmp in mstCvArray) //查找某个字段与值
            {
                if (((JObject)cvidtmp)["id"].ToString() == svtcv)
                {
                    var mstCVobjtmp = JObject.Parse(cvidtmp.ToString());
                    svtCVName = mstCVobjtmp["name"].ToString();
                    break;
                }
            }
            foreach (var svtskill in mstSvtSkillArray) //查找某个字段与值
            {
                if (((JObject)svtskill)["svtId"].ToString() == svtID && ((JObject)svtskill)["num"].ToString() == "1" && ((JObject)svtskill)["priority"].ToString() == "1")
                {
                    var mstsvtskillobjtmp = JObject.Parse(svtskill.ToString());
                    skill1ID = mstsvtskillobjtmp["skillId"].ToString();
                    SkillLvs.skillID1 = skill1ID;
                }
                if (((JObject)svtskill)["svtId"].ToString() == svtID && ((JObject)svtskill)["num"].ToString() == "1" && ((JObject)svtskill)["priority"].ToString() == "2")
                {
                    var mstsvtskillobjtmp = JObject.Parse(svtskill.ToString());
                    skill1ID = mstsvtskillobjtmp["skillId"].ToString();
                    SkillLvs.skillID1 = skill1ID;
                }
                if (((JObject)svtskill)["svtId"].ToString() == svtID && ((JObject)svtskill)["num"].ToString() == "2" && ((JObject)svtskill)["priority"].ToString() == "1")
                {
                    var mstsvtskillobjtmp = JObject.Parse(svtskill.ToString());
                    skill2ID = mstsvtskillobjtmp["skillId"].ToString();
                    SkillLvs.skillID2 = skill2ID;
                }
                if (((JObject)svtskill)["svtId"].ToString() == svtID && ((JObject)svtskill)["num"].ToString() == "2" && ((JObject)svtskill)["priority"].ToString() == "2")
                {
                    var mstsvtskillobjtmp = JObject.Parse(svtskill.ToString());
                    skill2ID = mstsvtskillobjtmp["skillId"].ToString();
                    SkillLvs.skillID2 = skill2ID;
                }
                if (((JObject)svtskill)["svtId"].ToString() == svtID && ((JObject)svtskill)["num"].ToString() == "3" && ((JObject)svtskill)["priority"].ToString() == "1")
                {
                    var mstsvtskillobjtmp = JObject.Parse(svtskill.ToString());
                    skill3ID = mstsvtskillobjtmp["skillId"].ToString();
                    SkillLvs.skillID3 = skill3ID;
                }
                if (((JObject)svtskill)["svtId"].ToString() == svtID && ((JObject)svtskill)["num"].ToString() == "3" && ((JObject)svtskill)["priority"].ToString() == "2")
                {
                    var mstsvtskillobjtmp = JObject.Parse(svtskill.ToString());
                    skill3ID = mstsvtskillobjtmp["skillId"].ToString();
                    SkillLvs.skillID3 = skill3ID;
                }
            }
            foreach (var skilltmp in mstSkillArray) //查找某个字段与值
            {
                if (((JObject)skilltmp)["id"].ToString() == skill1ID)
                {
                    var skillobjtmp = JObject.Parse(skilltmp.ToString());
                    skill1Name = skillobjtmp["name"].ToString();
                    SkillLvs.skillname1 = skill1Name;
                }
                if (((JObject)skilltmp)["id"].ToString() == skill2ID)
                {
                    var skillobjtmp = JObject.Parse(skilltmp.ToString());
                    skill2Name = skillobjtmp["name"].ToString();
                    SkillLvs.skillname2 = skill2Name;
                }
                if (((JObject)skilltmp)["id"].ToString() == skill3ID)
                {
                    var skillobjtmp = JObject.Parse(skilltmp.ToString());
                    skill3Name = skillobjtmp["name"].ToString();
                    SkillLvs.skillname3 = skill3Name;
                }
                foreach (string classpassiveidtmp in svtClassPassiveIDArray)
                {
                    if (((JObject)skilltmp)["id"].ToString() == classpassiveidtmp)
                    {
                        var mstsvtPskillobjtmp = JObject.Parse(skilltmp.ToString());
                        svtClassPassiveList.Add(mstsvtPskillobjtmp["name"].ToString());
                    }
                }
            }
            foreach (var skillDetailtmp in mstSkillDetailArray) //查找某个字段与值
            {
                if (((JObject)skillDetailtmp)["id"].ToString() == skill1ID)
                {
                    var skillDetailobjtmp = JObject.Parse(skillDetailtmp.ToString());
                    skill1detail = skillDetailobjtmp["detail"].ToString().Replace("[{0}]", " [Lv.1 - Lv.10] ").Replace("[g]", "").Replace("[o]", "").Replace("[/g]", "").Replace("[/o]", "").Replace(@"＆", "\r\n ＋").Replace(@"＋", "\r\n ＋").Replace("\r\n \r\n", "\r\n");
                    SkillLvs.skillDetail1 = skill1detail;
                }
                if (((JObject)skillDetailtmp)["id"].ToString() == skill2ID)
                {
                    var skillDetailobjtmp = JObject.Parse(skillDetailtmp.ToString());
                    skill2detail = skillDetailobjtmp["detail"].ToString().Replace("[{0}]", " [Lv.1 - Lv.10] ").Replace("[g]", "").Replace("[o]", "").Replace("[/g]", "").Replace("[/o]", "").Replace(@"＆", "\r\n ＋").Replace(@"＋", "\r\n ＋").Replace("\r\n \r\n", "\r\n");
                    SkillLvs.skillDetail2 = skill2detail;
                }
                if (((JObject)skillDetailtmp)["id"].ToString() == skill3ID)
                {
                    var skillDetailobjtmp = JObject.Parse(skillDetailtmp.ToString());
                    skill3detail = skillDetailobjtmp["detail"].ToString().Replace("[{0}]", " [Lv.1 - Lv.10] ").Replace("[g]", "").Replace("[o]", "").Replace("[/g]", "").Replace("[/o]", "").Replace(@"＆", "\r\n ＋").Replace(@"＋", "\r\n ＋").Replace("\r\n \r\n", "\r\n");
                    SkillLvs.skillDetail3 = skill3detail;
                }
            }
            foreach (var svtTreasureDevicestmp in mstSvtTreasureDevicedArray) //查找某个字段与值
            {
                if (((JObject)svtTreasureDevicestmp)["svtId"].ToString() == svtID && ((JObject)svtTreasureDevicestmp)["priority"].ToString() == "101")
                {
                    var mstsvtTDobjtmp = JObject.Parse(svtTreasureDevicestmp.ToString());
                    svtNPCardhitDamage = mstsvtTDobjtmp["damage"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "");
                    svtNPCardType = mstsvtTDobjtmp["cardId"].ToString().Replace("2", "Buster").Replace("1", "Arts").Replace("3", "Quick");
                    TreasureDevices.TDcardtypeDisplay = svtNPCardType;
                    foreach (char c in svtNPCardhitDamage)
                    {
                        if (c == ',')
                            svtNPCardhit++;
                    }
                    svtTDID = mstsvtTDobjtmp["treasureDeviceId"].ToString();
                    TreasureDevices.TDID = svtTDID;
                    TreasureDevices.TDcardHitsDisplay = svtNPCardhit.ToString() + " hit " + svtNPCardhitDamage;
                }
                if (((JObject)svtTreasureDevicestmp)["svtId"].ToString() == svtID && ((JObject)svtTreasureDevicestmp)["priority"].ToString() == "102")
                {
                    var mstsvtTDobjtmp = JObject.Parse(svtTreasureDevicestmp.ToString());
                    svtTDID = mstsvtTDobjtmp["treasureDeviceId"].ToString();
                    TreasureDevices.TDID = svtTDID;
                }
                if (((JObject)svtTreasureDevicestmp)["svtId"].ToString() == svtID && ((JObject)svtTreasureDevicestmp)["priority"].ToString() == "103")
                {
                    var mstsvtTDobjtmp = JObject.Parse(svtTreasureDevicestmp.ToString());
                    svtTDID = mstsvtTDobjtmp["treasureDeviceId"].ToString();
                    TreasureDevices.TDID = svtTDID;
                }
                if (((JObject)svtTreasureDevicestmp)["svtId"].ToString() == svtID && ((JObject)svtTreasureDevicestmp)["priority"].ToString() == "104")
                {
                    var mstsvtTDobjtmp = JObject.Parse(svtTreasureDevicestmp.ToString());
                    svtTDID = mstsvtTDobjtmp["treasureDeviceId"].ToString();
                    TreasureDevices.TDID = svtTDID;
                }
                if (((JObject)svtTreasureDevicestmp)["svtId"].ToString() == svtID && ((JObject)svtTreasureDevicestmp)["priority"].ToString() == "105")
                {
                    var mstsvtTDobjtmp = JObject.Parse(svtTreasureDevicestmp.ToString());
                    svtTDID = mstsvtTDobjtmp["treasureDeviceId"].ToString();
                    TreasureDevices.TDID = svtTDID;
                    break;
                }
            }
            foreach (var TDDtmp in mstTreasureDeviceDetailArray) //查找某个字段与值
            {
                if (((JObject)TDDtmp)["id"].ToString() == svtTDID)
                {
                    var TDDobjtmp = JObject.Parse(TDDtmp.ToString());
                    NPDetail = TDDobjtmp["detail"].ToString().Replace("[{0}]", " [Lv.1 - Lv.5] ").Replace("[g]", "").Replace("[o]", "").Replace("[/g]", "").Replace("[/o]", "").Replace(@"＆", "\r\n ＋").Replace(@"＋", "\r\n ＋").Replace("\r\n \r\n", "\r\n");
                    TreasureDevices.TDDetailDisplay = NPDetail;
                    break;
                }
            }
            foreach (var TDlvtmp in mstTreasureDeviceLvArray) //查找某个字段与值
            {
                if (((JObject)TDlvtmp)["treaureDeviceId"].ToString() == svtTDID)
                {
                    var TDlvobjtmp = JObject.Parse(TDlvtmp.ToString());
                    NPRateTD = Convert.ToInt32(TDlvobjtmp["tdPoint"].ToString());
                    NPRateArts = Convert.ToInt32(TDlvobjtmp["tdPointA"].ToString());
                    NPRateBuster = Convert.ToInt32(TDlvobjtmp["tdPointB"].ToString());
                    NPRateQuick = Convert.ToInt32(TDlvobjtmp["tdPointQ"].ToString());
                    NPRateEX = Convert.ToInt32(TDlvobjtmp["tdPointEx"].ToString());
                    NPRateDef = Convert.ToInt32(TDlvobjtmp["tdPointDef"].ToString());
                    break;
                }
            }
            foreach (var illustidtmp in mstIllustratorArray) //查找某个字段与值
            {
                if (((JObject)illustidtmp)["id"].ToString() == svtillust)
                {
                    var mstillustobjtmp = JObject.Parse(illustidtmp.ToString());
                    svtILLUSTName = mstillustobjtmp["name"].ToString();
                    break;
                }
            }
            foreach (var svtLimittmp in mstSvtLimitArray) //查找某个字段与值
            {
                if (((JObject)svtLimittmp)["svtId"].ToString() == svtID)
                {
                    var mstsvtLimitobjtmp = JObject.Parse(svtLimittmp.ToString());
                    svtrarity = mstsvtLimitobjtmp["rarity"].ToString();
                    svthpBase = mstsvtLimitobjtmp["hpBase"].ToString();
                    svthpMax = mstsvtLimitobjtmp["hpMax"].ToString();
                    svtatkBase = mstsvtLimitobjtmp["atkBase"].ToString();
                    svtatkMax = mstsvtLimitobjtmp["atkMax"].ToString();
                    svtcriticalWeight = mstsvtLimitobjtmp["criticalWeight"].ToString();
                    svtpower = mstsvtLimitobjtmp["power"].ToString();
                    svtdefense = mstsvtLimitobjtmp["defense"].ToString();
                    svtagility = mstsvtLimitobjtmp["agility"].ToString();
                    svtmagic = mstsvtLimitobjtmp["magic"].ToString();
                    svtluck = mstsvtLimitobjtmp["luck"].ToString();
                    svttreasureDevice = mstsvtLimitobjtmp["treasureDevice"].ToString();
                    powerData = int.Parse(svtpower);
                    defenseData = int.Parse(svtdefense);
                    agilityData = int.Parse(svtagility);
                    magicData = int.Parse(svtmagic);
                    luckData = int.Parse(svtluck);
                    TreasureData = int.Parse(svttreasureDevice);
                    break;
                }
            }
            foreach (var TreasureDevicestmp in mstTreasureDevicedArray) //查找某个字段与值
            {
                if (((JObject)TreasureDevicestmp)["id"].ToString() == svtTDID)
                {
                    var mstTDobjtmp = JObject.Parse(TreasureDevicestmp.ToString());
                    NPName = mstTDobjtmp["name"].ToString();
                    TreasureDevices.TDnameDisplay = NPName;
                    NPrank = mstTDobjtmp["rank"].ToString();
                    NPruby = mstTDobjtmp["ruby"].ToString();
                    TreasureDevices.TDrubyDisplay = NPruby;
                    NPtypeText = mstTDobjtmp["typeText"].ToString();
                    TreasureDevices.TDrankDisplay = NPrank + " ( " + NPtypeText + " ) ";
                    svtNPDamageType = mstTDobjtmp["effectFlag"].ToString().Replace("0", "无伤害宝具").Replace("1", "群体宝具").Replace("2", "单体宝具");
                    TreasureDevices.TDtypeDisplay = svtNPDamageType;
                    if (svtNPDamageType == "无伤害宝具")
                    {
                        svtNPCardhit = 0;
                        svtNPCardhitDamage = "[ - ]";
                    }
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button6.Enabled = true;
                    button9.Enabled = true;
                    button10.Enabled = true;
                    break;
                }
                if (((JObject)TreasureDevicestmp)["seqId"].ToString() == svtID && ((JObject)TreasureDevicestmp)["ruby"].ToString() == "-" && ((JObject)TreasureDevicestmp)["id"].ToString().Length == 3)
                {
                    var mstTDobjtmp2 = JObject.Parse(TreasureDevicestmp.ToString());
                    NPName = mstTDobjtmp2["name"].ToString();
                    NPrank = mstTDobjtmp2["rank"].ToString();
                    NPruby = mstTDobjtmp2["ruby"].ToString();
                    NPtypeText = mstTDobjtmp2["typeText"].ToString();
                    svtNPDamageType = mstTDobjtmp2["effectFlag"].ToString().Replace("0", "-").Replace("1", "群体宝具").Replace("2", "单体宝具");
                    if (svtNPDamageType == "-")
                    {
                        svtNPCardhit = 0;
                        svtNPCardhitDamage = "[ - ]";
                    }
                    NPDetail = "该ID的配卡与宝具解析不准确,请留意.";
                    foreach (var svtTreasureDevicestmp in mstSvtTreasureDevicedArray) //查找某个字段与值
                    {
                        if (((JObject)svtTreasureDevicestmp)["treasureDeviceId"].ToString() == ((JObject)TreasureDevicestmp)["id"].ToString())
                        {
                            var mstsvtTDobjtmp2 = JObject.Parse(svtTreasureDevicestmp.ToString());
                            svtNPCardhitDamage = mstsvtTDobjtmp2["damage"].ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "");
                            svtNPCardType = mstsvtTDobjtmp2["cardId"].ToString().Replace("2", "Buster").Replace("1", "Arts").Replace("3", "Quick");
                            break;
                        }
                    }
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button6.Enabled = false;
                    button9.Enabled = false;
                    button10.Enabled = false;
                    break;
                }
            }
            if (NPDetail == "unknown")
            {
                foreach (var TreasureDevicestmp2 in mstTreasureDevicedArray) //查找某个字段与值
                {
                    if (((JObject)TreasureDevicestmp2)["name"].ToString() == NPName)
                    {
                        var TreasureDevicesobjtmp2 = JObject.Parse(TreasureDevicestmp2.ToString());
                        var newtmpid = TreasureDevicesobjtmp2["id"].ToString();
                        if (newtmpid.Length == 6)
                        {
                            TreasureDevices.FinTDID_TMP = newtmpid;
                            foreach (var TDDtmp2 in mstTreasureDeviceDetailArray) //查找某个字段与值
                            {
                                if (((JObject)TDDtmp2)["id"].ToString() == TreasureDevices.FinTDID_TMP)
                                {
                                    var TDDobjtmp2 = JObject.Parse(TDDtmp2.ToString());
                                    NPDetail = TDDobjtmp2["detail"].ToString().Replace("[{0}]", " [Lv.1 - Lv.5] ").Replace("[g]", "").Replace("[o]", "").Replace("[/g]", "").Replace("[/o]", "").Replace(@"＆", "\r\n ＋").Replace(@"＋", "\r\n ＋").Replace("\r\n \r\n", "\r\n");
                                    TreasureDevices.TDDetailDisplay = NPDetail;
                                }
                            }
                        }
                        else if (newtmpid.Length == 7)
                        {
                            if (newtmpid.Substring(0, 2) == "10" || newtmpid.Substring(0, 2) == "11" || newtmpid.Substring(0, 2) == "23" || newtmpid.Substring(0, 2) == "25")
                            {
                                TreasureDevices.FinTDID_TMP = newtmpid;
                                foreach (var TDDtmp2 in mstTreasureDeviceDetailArray) //查找某个字段与值
                                {
                                    if (((JObject)TDDtmp2)["id"].ToString() == TreasureDevices.FinTDID_TMP)
                                    {
                                        var TDDobjtmp2 = JObject.Parse(TDDtmp2.ToString());
                                        NPDetail = TDDobjtmp2["detail"].ToString().Replace("[{0}]", " [Lv.1 - Lv.5] ").Replace("[g]", "").Replace("[o]", "").Replace("[/g]", "").Replace("[/o]", "").Replace(@"＆", "\r\n ＋").Replace(@"＋", "\r\n ＋").Replace("\r\n \r\n", "\r\n");
                                        TreasureDevices.TDDetailDisplay = NPDetail;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (svtArtsCardQuantity == 0)
                {
                    NPrate = 0;
                }
            else
                {
                    NPrate = nprateclassbase[classData] * nprateartscount[svtArtsCardQuantity] * npratemagicbase[magicData] / svtArtsCardhit / 100;
                    NPrate = Math.Floor(NPrate * 10000)/10000;
                }
            svtClassPassiveArray = svtClassPassiveList.ToArray();
            svtClassPassive = String.Join(", ", svtClassPassiveList.ToArray());
            if (NPName == "unknown")
            {
                button10.Enabled = false;
            }
            if (skill1Name == "unknown")
            {
                button3.Enabled = false;
                button6.Enabled = false;
                button9.Enabled = false;
            }

                if (svtrarity == "unknown")
            {
                MessageBox.Show("从者ID不存在或未实装，请重试.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (Control c in this.Controls)
                {


                    if (c is TextBox && c !=textBox1)
                    {


                        ((TextBox)c).Text = "";


                    }
                }
                label41.Text = "";
                label44.Text = "";
                label43.Text = "";
                button2.Enabled = false;
                JibanStringData.str1 = "";
                JibanStringData.str2 = "";
                JibanStringData.str3 = "";
                JibanStringData.str4 = "";
                JibanStringData.str5 = "";
                JibanStringData.str6 = "";
                JibanStringData.str7 = "";
                button3.Enabled = false;
                button6.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                button5.Enabled = true;
                return;
            }
            textBox2.Text = svtName;
            textBox3.Text = svtNameDisplay;
            textBox4.Text = ClassName[classData];
            if (classData == 3)
            {
                label44.Text = "( x 1.05 △)";
                label43.Text = "( x 1.05 △)";
            }
            else if (classData == 5)
            {
                label44.Text = "( x 0.9 ▽)";
                label43.Text = "( x 0.9 ▽)";
            }
            else if (classData == 6)
            {
                label44.Text = "( x 0.9 ▽)";
                label43.Text = "( x 0.9 ▽)";
            }
            else if (classData == 2)
            {
                label44.Text = "( x 0.95 ▽)";
                label43.Text = "( x 0.95 ▽)";
            }
            else if (classData == 7)
            {
                label44.Text = "( x 1.1 △)";
                label43.Text = "( x 1.1 △)";
            }
            else if (classData == 9)
            {
                label44.Text = "( x 1.1 △)";
                label43.Text = "( x 1.1 △)";
            }
            else if (classData == 11)
            {
                label44.Text = "( x 1.1 △)";
                label43.Text = "( x 1.1 △)";
            }
            else
            {
                label44.Text = "( x 1.0 -)";
                label43.Text = "( x 1.0 -)";
            }
            textBox5.Text = svtrarity + " ☆";
            textBox6.Text = gender[genderData];
            textBox7.Text = svtHideAttri;
            textBox8.Text = svtCVName;
            textBox9.Text = svtILLUSTName;
            textBox10.Text = svtcollectionid;
            textBox11.Text = starrate.ToString() + "%";
            textBox12.Text = deathrate.ToString() + "%";
            textBox13.Text = svtcriticalWeight;
            textBox14.Text = NPrate.ToString("P");
            textBox15.Text = svtClassPassive;
            textBox16.Text = svthpBase;
            textBox17.Text = svtatkBase;
            textBox18.Text = svthpMax;
            textBox19.Text = svtatkMax;
            textBox20.Text = CardArrange;
            textBox21.Text = svtArtsCardhit.ToString() + " hit " + svtArtsCardhitDamage;
            textBox22.Text = svtBustersCardhit.ToString() + " hit " + svtBustersCardhitDamage;
            textBox23.Text = svtQuicksCardhit.ToString() + " hit " + svtQuicksCardhitDamage;
            textBox24.Text = svtExtraCardhit.ToString() + " hit " + svtExtraCardhitDamage;
            textBox25.Text = svtNPCardhit.ToString() + " hit " + svtNPCardhitDamage;
            textBox26.Text = svtNPCardType;
            textBox27.Text = svtNPDamageType;
            textBox28.Text = NPrank + " ( " + NPtypeText + " ) ";
            textBox29.Text = NPruby;
            textBox30.Text = NPName;
            textBox31.Text = NPDetail;
            textBox32.Text = skill1Name;
            textBox33.Text = skill1detail;
            textBox34.Text = skill2detail;
            textBox35.Text = skill2Name;
            textBox36.Text = skill3detail;
            textBox37.Text = skill3Name;
            textBox38.Text = "Quick: " + (NPRateQuick / 10000).ToString("P") + "   Arts: " + (NPRateArts / 10000).ToString("P") + "   Buster: " + (NPRateBuster / 10000).ToString("P") + "\r\nExtra: " + (NPRateEX / 10000).ToString("P") + "   宝具: " + (NPRateTD / 10000).ToString("P") + "   受击: " + (NPRateDef / 10000).ToString("P") ;
            label41.Text = "筋力: " + PPK[powerData] + "    耐久: " + PPK[defenseData] + "    敏捷: " + PPK[agilityData] +
                    "\n魔力: " + PPK[magicData] + "    幸运: " + PPK[luckData] + "    宝具: " + PPK[TreasureData];
            button5.Enabled = true;
            SkillLvs.svtid = svtID;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {


                if (c is TextBox)
                {


                    ((TextBox)c).Text = "";


                }
            }
            label41.Text = "";
            label44.Text = "";
            label43.Text = "";
            button2.Enabled = false;
            JibanStringData.str1 = "";
            JibanStringData.str2 = "";
            JibanStringData.str3 = "";
            JibanStringData.str4 = "";
            JibanStringData.str5 = "";
            JibanStringData.str6 = "";
            JibanStringData.str7 = "";
            button3.Enabled = false;
            button6.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            SkillLvs.EEB = false;

    }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            JibanText JT = new JibanText();
            if (SkillLvs.EEB)
            {
                JibanStringData.str1 = "该解析器由闲着蛋疼啥也不会的现学C#的烂技术的作者ACPudding编写而成.";
                JibanStringData.str2 = "当前版本: V1.5_b4";
                JibanStringData.str3 = "下载游戏数据与解密部分拷贝了nishuoshenme的FGO资源解析器的代码.\r\ngithub地址: https://www.github.com/nishuoshenme";
                JibanStringData.str4 = "作者BGO ID 爱吸吸果冻 \r\n日服ID エイシープリン";
                JibanStringData.str5 = "B站 ID ACPudding";
                JibanStringData.str6 = "彩蛋主界面部分所有数据都是作者随意的设定.若有雷同,纯属巧合.";
                JibanStringData.str7 = "FGOServantBasicInformationAnalyzer\r\n本项目的代码可在Github上查看,地址为: https://github.com/ACPudding/TestProject";
                JT.ShowDialog();
                return;
            }
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string svtID = Convert.ToString(textBox1.Text);
            if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtComment"))
            {
                MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mstSvtComment = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvtComment");
            JArray mstSvtCommentArray = (JArray)JsonConvert.DeserializeObject(mstSvtComment);
            foreach (var SCTMP in mstSvtCommentArray) //查找某个字段与值
            {
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "1")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str1 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "2")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str2 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "3")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str3 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "4")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str4 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "5")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str5 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "6")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str6 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
                if (((JObject)SCTMP)["svtId"].ToString() == svtID && ((JObject)SCTMP)["id"].ToString() == "7")
                {
                    var SCobjtmp = JObject.Parse(SCTMP.ToString());
                    JibanStringData.str7 = SCobjtmp["comment"].ToString().Replace("\n", "\r\n");
                }
            }
            JT.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
                  }

        private void button3_Click(object sender, EventArgs e)
        {
            SkillInfo SI = new SkillInfo();
            if (SkillLvs.EEB)
            {
                SkillLvs.skillnameDisplay = "怪力 B";
                SkillLvs.skillDetailDisplay = "自身の攻撃力をアップ[Lv.1 - Lv.10](2ターン)";
                SkillLvs.skilllv1chargetime = "7";
                SkillLvs.skilllv6chargetime = "6";
                SkillLvs.skilllv10chargetime = "5";
                SkillLvs.skilllv1sval = "10%";
                SkillLvs.skilllv6sval = "20%";
                SkillLvs.skilllv10sval = "30%";
                SI.ShowDialog();
                return;
            }
            //MessageBox.Show("技能幅度窗口显示的幅度有时候会多出几行/错误，故无法有效解析，仅供参考！", "温馨提示:");
            MessageBox.Show("技能幅度窗口显示的为文件中的原始数据，水平有限，无法进行有效解析。\r\n/ 之间的为一个Buff的幅度\r\n1、如果为[a,b]则a为成功率(除以10就是百分比，如1000就是100%),b需要看技能描述，如果为出星或者生命值则b的大小即为幅度，若为NP，则将该数值除以100即为NP值。若b为1，则该组段可以忽略不看。\r\n2、如果为[a,b,c]或者[a,b,c,d]则在一般情况下a表示成功率(同1),b表示持续回合数即Turn,c表示次数(-1即为没有次数限制),d在大多数情况下除以10即为Buff幅度(%)，有时会有例外(可能也是没有意义).\r\n3、如果为[a,b,c,d,e]则a,b,c同2,d和e需要通过源文件进行详细手动分析。", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string svtID = Convert.ToString(textBox1.Text);
            if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkillLv"))
            {
                MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mstSkillLv = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSkillLv");
            JArray mstSkillLvArray = (JArray)JsonConvert.DeserializeObject(mstSkillLv);
            SkillLvs.SKL1str = "";
            SkillLvs.SKL2str = "";
            SkillLvs.SKL3str = "";
            foreach (var SKLTMP in mstSkillLvArray) //查找某个字段与值
            {
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID1 && ((JObject)SKLTMP)["lv"].ToString() == "1")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv1sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv1sval = SkillLvs.skilllv1sval.Substring(0, SkillLvs.skilllv1sval.Length - 2);
                    SkillLvs.skilllv1EffectArray1 = SkillLvs.skilllv1sval.Split('/');
                    string[][] skilllv1EffectArray2 = new string[SkillLvs.skilllv1sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv1EffectArray1.Length; i++)
                    {
                        skilllv1EffectArray2[i] = SkillLvs.skilllv1EffectArray1[i].Split(',');

                        if (skilllv1EffectArray2[i].Length == 2)
                        {
                            if (skilllv1EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL1str = SkillLvs.SKL1str + "";
                            }
                            else
                            {
                                SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv1EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv1EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv1EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv1EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv1EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv1EffectArray2[i][3] + " , " + skilllv1EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                    }
                    SkillLvs.skilllv1chargetime = SKLobjtmp["chargeTurn"].ToString();
                }
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID1 && ((JObject)SKLTMP)["lv"].ToString() == "6")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv6sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv6sval = SkillLvs.skilllv6sval.Substring(0, SkillLvs.skilllv6sval.Length - 2);
                    SkillLvs.skilllv6EffectArray1 = SkillLvs.skilllv6sval.Split('/');
                    string[][] skilllv6EffectArray2 = new string[SkillLvs.skilllv6sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv6EffectArray1.Length; i++)
                    {
                        skilllv6EffectArray2[i] = SkillLvs.skilllv6EffectArray1[i].Split(',');

                        if (skilllv6EffectArray2[i].Length == 2)
                        {
                            if (skilllv6EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL2str = SkillLvs.SKL2str + "";
                            }
                            else
                            {
                                SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv6EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv6EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv6EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv6EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv6EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv6EffectArray2[i][3] + " , " + skilllv6EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                        SkillLvs.skilllv6chargetime = SKLobjtmp["chargeTurn"].ToString();
                    }
                }
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID1 && ((JObject)SKLTMP)["lv"].ToString() == "10")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv10sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv10sval = SkillLvs.skilllv10sval.Substring(0, SkillLvs.skilllv10sval.Length - 2);
                    SkillLvs.skilllv10EffectArray1 = SkillLvs.skilllv10sval.Split('/');
                    string[][] skilllv10EffectArray2 = new string[SkillLvs.skilllv10sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv10EffectArray1.Length; i++)
                    {
                        skilllv10EffectArray2[i] = SkillLvs.skilllv10EffectArray1[i].Split(',');

                        if (skilllv10EffectArray2[i].Length == 2)
                        {
                            if (skilllv10EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL3str = SkillLvs.SKL3str + "";
                            }
                            else
                            {
                                SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv10EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv10EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv10EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv10EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv10EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv10EffectArray2[i][3] + " , " + skilllv10EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                    }
                    SkillLvs.skilllv10chargetime = SKLobjtmp["chargeTurn"].ToString();
                }
            }
                    SkillLvs.skillnameDisplay = SkillLvs.skillname1;
                    SkillLvs.skillDetailDisplay = SkillLvs.skillDetail1;
                    SI.ShowDialog();
            }
        private void button6_Click_1(object sender, EventArgs e)
        {
            SkillInfo SI = new SkillInfo();
            if (SkillLvs.EEB)
            {
                SkillLvs.skillnameDisplay = "鑑識眼(宅) A";
                SkillLvs.skillDetailDisplay = "味方単体のスター集中度をアップ[Lv.1 - Lv.10](1ターン)\r\n + スターを大量獲得[Lv.1 - Lv.10]";
                SkillLvs.skilllv1chargetime = "7";
                SkillLvs.skilllv6chargetime = "6";
                SkillLvs.skilllv10chargetime = "5";
                SkillLvs.skilllv1sval = "3000%\r\n20";
                SkillLvs.skilllv6sval = "4500%\r\n25";
                SkillLvs.skilllv10sval = "6000%\r\n30";
                SI.ShowDialog();
                return;
            }
            //MessageBox.Show("技能幅度窗口显示的幅度有时候会多出几行/错误，故无法有效解析，仅供参考！", "温馨提示:");
            MessageBox.Show("技能幅度窗口显示的为文件中的原始数据，水平有限，无法进行有效解析。\r\n/ 之间的为一个Buff的幅度\r\n1、如果为[a,b]则a为成功率(除以10就是百分比，如1000就是100%),b需要看技能描述，如果为出星或者生命值则b的大小即为幅度，若为NP，则将该数值除以100即为NP值。若b为1，则该组段可以忽略不看。\r\n2、如果为[a,b,c]或者[a,b,c,d]则在一般情况下a表示成功率(同1),b表示持续回合数即Turn,c表示次数(-1即为没有次数限制),d在大多数情况下除以10即为Buff幅度(%)，有时会有例外(可能也是没有意义).\r\n3、如果为[a,b,c,d,e]则a,b,c同2,d和e需要通过源文件进行详细手动分析。", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string svtID = Convert.ToString(textBox1.Text);
            if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkillLv"))
            {
                MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mstSkillLv = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSkillLv");
            JArray mstSkillLvArray = (JArray)JsonConvert.DeserializeObject(mstSkillLv);
            SkillLvs.SKL1str = "";
            SkillLvs.SKL2str = "";
            SkillLvs.SKL3str = "";
            foreach (var SKLTMP in mstSkillLvArray) //查找某个字段与值
            {
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID2 && ((JObject)SKLTMP)["lv"].ToString() == "1")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv1sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv1sval = SkillLvs.skilllv1sval.Substring(0, SkillLvs.skilllv1sval.Length - 2);
                    SkillLvs.skilllv1EffectArray1 = SkillLvs.skilllv1sval.Split('/');
                    string[][] skilllv1EffectArray2 = new string[SkillLvs.skilllv1sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv1EffectArray1.Length; i++)
                    {
                        skilllv1EffectArray2[i] = SkillLvs.skilllv1EffectArray1[i].Split(',');

                        if (skilllv1EffectArray2[i].Length == 2)
                        {
                            if (skilllv1EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL1str = SkillLvs.SKL1str + "";
                            }
                            else
                            {
                                SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv1EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv1EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv1EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv1EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv1EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv1EffectArray2[i][3] + " , " + skilllv1EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                    }
                    SkillLvs.skilllv1chargetime = SKLobjtmp["chargeTurn"].ToString();
                }
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID2 && ((JObject)SKLTMP)["lv"].ToString() == "6")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv6sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv6sval = SkillLvs.skilllv6sval.Substring(0, SkillLvs.skilllv6sval.Length - 2);
                    SkillLvs.skilllv6EffectArray1 = SkillLvs.skilllv6sval.Split('/');
                    string[][] skilllv6EffectArray2 = new string[SkillLvs.skilllv6sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv6EffectArray1.Length; i++)
                    {
                        skilllv6EffectArray2[i] = SkillLvs.skilllv6EffectArray1[i].Split(',');

                        if (skilllv6EffectArray2[i].Length == 2)
                        {
                            if (skilllv6EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL2str = SkillLvs.SKL2str + "";
                            }
                            else
                            {
                                SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv6EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv6EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv6EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv6EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv6EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv6EffectArray2[i][3] + " , " + skilllv6EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                        SkillLvs.skilllv6chargetime = SKLobjtmp["chargeTurn"].ToString();
                    }
                }
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID2 && ((JObject)SKLTMP)["lv"].ToString() == "10")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv10sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv10sval = SkillLvs.skilllv10sval.Substring(0, SkillLvs.skilllv10sval.Length - 2);
                    SkillLvs.skilllv10EffectArray1 = SkillLvs.skilllv10sval.Split('/');
                    string[][] skilllv10EffectArray2 = new string[SkillLvs.skilllv10sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv10EffectArray1.Length; i++)
                    {
                        skilllv10EffectArray2[i] = SkillLvs.skilllv10EffectArray1[i].Split(',');

                        if (skilllv10EffectArray2[i].Length == 2)
                        {
                            if (skilllv10EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL3str = SkillLvs.SKL3str + "";
                            }
                            else
                            {
                                SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv10EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv10EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv10EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv10EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv10EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv10EffectArray2[i][3] + " , " + skilllv10EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                    }
                    SkillLvs.skilllv10chargetime = SKLobjtmp["chargeTurn"].ToString();
                }
            }
            SkillLvs.skillnameDisplay = SkillLvs.skillname2;
            SkillLvs.skillDetailDisplay = SkillLvs.skillDetail2;
            SI.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SkillInfo SI = new SkillInfo();
            if (SkillLvs.EEB)
            {
                SkillLvs.skillnameDisplay = "良心がない EX";
                SkillLvs.skillDetailDisplay = "味方単体のNPを大量増やす[Lv.1 - Lv.10] \r\n + 宝具威力を超绝アップ[Lv.1 - Lv.10] \r\n + 〔1ターン後即死〕状態を付与 【デメリット】";
                SkillLvs.skilllv1chargetime = "12";
                SkillLvs.skilllv6chargetime = "11";
                SkillLvs.skilllv10chargetime = "10";
                SkillLvs.skilllv1sval = "80%\r\n60%\r\n∅";
                SkillLvs.skilllv6sval = "90%\r\n70%\r\n∅";
                SkillLvs.skilllv10sval = "100%\r\n80%\r\n∅";
                SI.ShowDialog();
                return;
            }
            //MessageBox.Show("技能幅度窗口显示的幅度有时候会多出几行/错误，故无法有效解析，仅供参考！", "温馨提示:");
            MessageBox.Show("技能幅度窗口显示的为文件中的原始数据，水平有限，无法进行有效解析。\r\n/ 之间的为一个Buff的幅度\r\n1、如果为[a,b]则a为成功率(除以10就是百分比，如1000就是100%),b需要看技能描述，如果为出星或者生命值则b的大小即为幅度，若为NP，则将该数值除以100即为NP值。若b为1，则该组段可以忽略不看。\r\n2、如果为[a,b,c]或者[a,b,c,d]则在一般情况下a表示成功率(同1),b表示持续回合数即Turn,c表示次数(-1即为没有次数限制),d在大多数情况下除以10即为Buff幅度(%)，有时会有例外(可能也是没有意义).\r\n3、如果为[a,b,c,d,e]则a,b,c同2,d和e需要通过源文件进行详细手动分析。", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string svtID = Convert.ToString(textBox1.Text);
            if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkillLv"))
            {
                MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mstSkillLv = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSkillLv");
            JArray mstSkillLvArray = (JArray)JsonConvert.DeserializeObject(mstSkillLv);
            SkillLvs.SKL1str = "";
            SkillLvs.SKL2str = "";
            SkillLvs.SKL3str = "";
            foreach (var SKLTMP in mstSkillLvArray) //查找某个字段与值
            {
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID3 && ((JObject)SKLTMP)["lv"].ToString() == "1")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv1sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv1sval = SkillLvs.skilllv1sval.Substring(0, SkillLvs.skilllv1sval.Length - 2);
                    SkillLvs.skilllv1EffectArray1 = SkillLvs.skilllv1sval.Split('/');
                    string[][] skilllv1EffectArray2 = new string[SkillLvs.skilllv1sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv1EffectArray1.Length; i++)
                    {
                        skilllv1EffectArray2[i] = SkillLvs.skilllv1EffectArray1[i].Split(',');

                        if (skilllv1EffectArray2[i].Length == 2)
                        {
                            if (skilllv1EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL1str = SkillLvs.SKL1str + "";
                            }
                            else
                            {
                                SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv1EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv1EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv1EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv1EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv1EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL1str = SkillLvs.SKL1str + "[Buff " + "命中率: " + double.Parse(skilllv1EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv1EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv1EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv1EffectArray2[i][3] + " , " + skilllv1EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                    }
                    SkillLvs.skilllv1chargetime = SKLobjtmp["chargeTurn"].ToString();
                }
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID3 && ((JObject)SKLTMP)["lv"].ToString() == "6")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv6sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv6sval = SkillLvs.skilllv6sval.Substring(0, SkillLvs.skilllv6sval.Length - 2);
                    SkillLvs.skilllv6EffectArray1 = SkillLvs.skilllv6sval.Split('/');
                    string[][] skilllv6EffectArray2 = new string[SkillLvs.skilllv6sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv6EffectArray1.Length; i++)
                    {
                        skilllv6EffectArray2[i] = SkillLvs.skilllv6EffectArray1[i].Split(',');

                        if (skilllv6EffectArray2[i].Length == 2)
                        {
                            if (skilllv6EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL2str = SkillLvs.SKL2str + "";
                            }
                            else
                            {
                                SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv6EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv6EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv6EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv6EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv6EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL2str = SkillLvs.SKL2str + "[Buff " + "命中率: " + double.Parse(skilllv6EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv6EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv6EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv6EffectArray2[i][3] + " , " + skilllv6EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                        SkillLvs.skilllv6chargetime = SKLobjtmp["chargeTurn"].ToString();
                    }
                }
                if (((JObject)SKLTMP)["skillId"].ToString() == SkillLvs.skillID3 && ((JObject)SKLTMP)["lv"].ToString() == "10")
                {
                    var SKLobjtmp = JObject.Parse(SKLTMP.ToString());
                    SkillLvs.skilllv10sval = SKLobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                    SkillLvs.skilllv10sval = SkillLvs.skilllv10sval.Substring(0, SkillLvs.skilllv10sval.Length - 2);
                    SkillLvs.skilllv10EffectArray1 = SkillLvs.skilllv10sval.Split('/');
                    string[][] skilllv10EffectArray2 = new string[SkillLvs.skilllv10sval.Length][];
                    for (int i = 0; i < SkillLvs.skilllv10EffectArray1.Length; i++)
                    {
                        skilllv10EffectArray2[i] = SkillLvs.skilllv10EffectArray1[i].Split(',');

                        if (skilllv10EffectArray2[i].Length == 2)
                        {
                            if (skilllv10EffectArray2[i][1] == "1")
                            {
                                SkillLvs.SKL3str = SkillLvs.SKL3str + "";
                            }
                            else
                            {
                                SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "暴击星数/生命/NP值: " + skilllv10EffectArray2[i][1] + " (若为NP值请除以100)]\r\n";
                            }
                        }
                        if (skilllv10EffectArray2[i].Length == 3)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次]\r\n";
                        }
                        if (skilllv10EffectArray2[i].Length == 4)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "幅度: " + double.Parse(skilllv10EffectArray2[i][3]) / 10 + " %]\r\n";
                        }
                        if (skilllv10EffectArray2[i].Length == 5)
                        {
                            SkillLvs.SKL3str = SkillLvs.SKL3str + "[Buff " + "命中率: " + double.Parse(skilllv10EffectArray2[i][0]) / 10 + " % ," + "持续时间: " + skilllv10EffectArray2[i][1] + " Turn(s) ," + "次数: " + skilllv10EffectArray2[i][2].Replace("-1", "∞") + " 次 ," + "技能信息: " + skilllv10EffectArray2[i][3] + " , " + skilllv10EffectArray2[i][4] + " (请自行分析文件)]\r\n";
                        }
                    }
                    SkillLvs.skilllv10chargetime = SKLobjtmp["chargeTurn"].ToString();
                }
            }
            SkillLvs.skillnameDisplay = SkillLvs.skillname3;
            SkillLvs.skillDetailDisplay = SkillLvs.skillDetail3;
            SI.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SkillLvs.EastenEggCount = SkillLvs.EastenEggCount + 1;
            if (SkillLvs.EastenEggCount == 10)
            {
                textBox2.Text = "エイシープリン";
                textBox3.Text = "ACPudding";
                textBox4.Text = "Caster";
                textBox5.Text = "2" + " ☆";
                textBox6.Text = "男性";
                textBox7.Text = "人";
                textBox8.Text = "???";
                textBox9.Text = "???";
                textBox10.Text = "∞";
                textBox11.Text = "10.9" + "%";
                textBox12.Text = "39.0" + "%";
                textBox13.Text = "50";
                textBox14.Text = "1.08%";
                textBox15.Text = "家里蹲 A,狂化 E+";
                textBox16.Text = "1384";
                textBox17.Text = "1136";
                textBox18.Text = "7534";
                textBox19.Text = "6475";
                textBox20.Text = "[Q,A,A,B,B]";
                textBox21.Text = "3" + " hit " + "[16,33,51]";
                textBox22.Text = "6" + " hit " + "[4,9,14,19,23,31]";
                textBox23.Text = "2" + " hit " + "[33,67]";
                textBox24.Text = "3" + " hit " + "[12,25,63]";
                textBox25.Text = "10" + " hit " + "[3,5,3,7,8,10,12,14,16,22]";
                textBox26.Text = "Buster";
                textBox27.Text = "单体宝具";
                textBox28.Text = "D" + " ( " + "对人宝具" + " ) ";
                textBox29.Text = "ノー　ワーク・ベター　ライブ";
                textBox30.Text = "おやすみなさい、勤勉な人々";
                textBox31.Text = "敵単体に超強力な〔NPチャージのサーヴァント〕特攻攻撃 [Lv.1 - Lv.5] <オーバーチャージで効果UP> \r\n + 自身のNPをリチャージ";
                textBox32.Text = "怪力 B";
                textBox33.Text = "自身の攻撃力をアップ[Lv.1 - Lv.10](2ターン)";
                textBox35.Text = "鑑識眼(宅) A";
                textBox34.Text = "味方単体のスター集中度をアップ[Lv.1 - Lv.10](1ターン)\r\n + スターを大量獲得[Lv.1 - Lv.10]";
                textBox37.Text = "良心がない EX";
                textBox36.Text = "味方単体のNPを大量増やす[Lv.1 - Lv.10] \r\n + 宝具威力を超绝アップ[Lv.1 - Lv.10] \r\n + 〔1ターン後即死〕状態を付与 【デメリット】";
                textBox38.Text = "Quick: " + "1.08%" + "   Arts: " + "1.08%" + "   Buster: " + "1.08%" + "\r\nExtra: " + "1.08%" + "   宝具: " + "1.08%" + "   受击: " + "4.00%";
                label41.Text = "筋力: " + "C" + "    耐久: " + "B+" + "    敏捷: " + "C+" +
                        "\n魔力: " + "B++" + "    幸运: " + "B" + "    宝具: " + "C-";
                MessageBox.Show("点击羁绊故事可查看软件和作者信息.", "About:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SkillLvs.EEB = true;
                SkillLvs.EastenEggCount = 0;
                button2.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
                label43.Text = "( x 0.9 ▽)";
                label44.Text = "( x 0.9 ▽)";
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            TreasureDeviceInfo TDI = new TreasureDeviceInfo();
            if (SkillLvs.EEB)
            {
                TreasureDevices.TDcardHitsDisplay = "10" + " hit " + "[3,5,3,7,8,10,12,14,16,22]";
                TreasureDevices.TDcardtypeDisplay = "Buster";
                TreasureDevices.TDDetailDisplay = "敵単体に超強力な〔NPチャージのサーヴァント〕特攻攻撃 [Lv.1 - Lv.5] <オーバーチャージで効果UP> \r\n + 自身のNPをリチャージ";
                TreasureDevices.TDtypeDisplay = "单体宝具";
                TreasureDevices.TDrankDisplay = "D" + " ( " + "对人宝具" + " ) ";
                TreasureDevices.TDnameDisplay = "おやすみなさい、勤勉な人々";
                TreasureDevices.TDrubyDisplay = "ノー　ワーク・ベター　ライブ";
                TreasureDevices.TD1Display = "OC1: 600%,150%,10% /OC2: 600%,162.5%,10% /OC3: 600%,175%,10% /OC4: 600%,187.5%,10% /OC5: 600%,200%,10% ";
                TreasureDevices.TD2Display = "OC1: 800%,150%,10% /OC2: 800%,162.5%,10% /OC3: 800%,175%,10% /OC4: 800%,187.5%,10% /OC5: 800%,200%,10% ";
                TreasureDevices.TD3Display = "OC1: 900%,150%,10% /OC2: 900%,162.5%,10% /OC3: 900%,175%,10% /OC4: 900%,187.5%,10% /OC5: 900%,200%,10% ";
                TreasureDevices.TD4Display = "OC1: 950%,150%,10% /OC2: 950%,162.5%,10% /OC3: 950%,175%,10% /OC4: 950%,187.5%,10% /OC5: 950%,200%,10% ";
                TreasureDevices.TD5Display = "OC1: 1000%,150%,10% /OC2: 1000%,162.5%,10% /OC3: 1000%,175%,10% /OC4: 1000%,187.5%,10% /OC5: 1000%,200%,10% ";
                TDI.ShowDialog();
                return;
            }
            MessageBox.Show("窗口显示的为筛选过后文件中的原始数据，水平有限，无法进行有效解析。请进行手动分析。", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string svtID = Convert.ToString(textBox1.Text);
            if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceLv"))
            {
                MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mstTreasureDeviceLv = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceLv");
            JArray mstTreasureDeviceLvArray = (JArray)JsonConvert.DeserializeObject(mstTreasureDeviceLv);
            foreach (var TDLVtmp in mstTreasureDeviceLvArray) //查找某个字段与值
            {
                if (((JObject)TDLVtmp)["treaureDeviceId"].ToString() == TreasureDevices.TDID && ((JObject)TDLVtmp)["lv"].ToString() == "1")
                {
                    var TDLVobjtmp = JObject.Parse(TDLVtmp.ToString());
                    TreasureDevices.TD1Display = "OC1: " + TDLVobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                        "OC2: " + TDLVobjtmp["svals2"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC3: " + TDLVobjtmp["svals3"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC4: " + TDLVobjtmp["svals4"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC5: " + TDLVobjtmp["svals5"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/");
                }

                if (((JObject)TDLVtmp)["treaureDeviceId"].ToString() == TreasureDevices.TDID && ((JObject)TDLVtmp)["lv"].ToString() == "2")
                {
                    var TDLVobjtmp = JObject.Parse(TDLVtmp.ToString());
                    TreasureDevices.TD2Display = "OC1: " + TDLVobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                        "OC2: " + TDLVobjtmp["svals2"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC3: " + TDLVobjtmp["svals3"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC4: " + TDLVobjtmp["svals4"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC5: " + TDLVobjtmp["svals5"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") ;
                }
                if (((JObject)TDLVtmp)["treaureDeviceId"].ToString() == TreasureDevices.TDID && ((JObject)TDLVtmp)["lv"].ToString() == "3")
                {
                    var TDLVobjtmp = JObject.Parse(TDLVtmp.ToString());
                    TreasureDevices.TD3Display = "OC1: " + TDLVobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                        "OC2: " + TDLVobjtmp["svals2"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC3: " + TDLVobjtmp["svals3"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC4: " + TDLVobjtmp["svals4"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC5: " + TDLVobjtmp["svals5"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") ;
                }
                if (((JObject)TDLVtmp)["treaureDeviceId"].ToString() == TreasureDevices.TDID && ((JObject)TDLVtmp)["lv"].ToString() == "4")
                {
                    var TDLVobjtmp = JObject.Parse(TDLVtmp.ToString());
                    TreasureDevices.TD4Display = "OC1: " + TDLVobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                        "OC2: " + TDLVobjtmp["svals2"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC3: " + TDLVobjtmp["svals3"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC4: " + TDLVobjtmp["svals4"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC5: " + TDLVobjtmp["svals5"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") ;
                }
                if (((JObject)TDLVtmp)["treaureDeviceId"].ToString() == TreasureDevices.TDID && ((JObject)TDLVtmp)["lv"].ToString() == "5")
                {
                    var TDLVobjtmp = JObject.Parse(TDLVtmp.ToString());
                    TreasureDevices.TD5Display = "OC1: " + TDLVobjtmp["svals"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                        "OC2: " + TDLVobjtmp["svals2"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC3: " + TDLVobjtmp["svals3"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC4: " + TDLVobjtmp["svals4"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") + "\r\n" +
                    "OC5: " + TDLVobjtmp["svals5"].ToString().Replace("\n", "").Replace("\r", "").Replace("[", "").Replace("]", "*").Replace("\"", "").Replace(" ", "").Replace("*,", "/") ;
                }
            }
            TDI.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Thread BD = new Thread(bindown);
            BD.Start();
        }
        public void bindown()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=2.13.4");
            JObject res = JObject.Parse(result);
            if (res["response"][0]["fail"]["action"] != null)
            {
                if (res["response"][0]["fail"]["action"].ToString() == "app_version_up")
                {
                    string tmp = res["response"][0]["fail"]["detail"].ToString();
                    tmp = Regex.Replace(tmp, @".*新ver.：(.*)、現.*", "$1", RegexOptions.Singleline);
                    result = HttpRequest.PhttpReq("https://game.fate-go.jp/gamedata/top", "appVer=" + tmp.ToString());
                    res = JObject.Parse(result);
                }
                else
                {
                }
            }
            if (!Directory.Exists(gamedata.FullName))
                Directory.CreateDirectory(gamedata.FullName);
            File.WriteAllText(gamedata.FullName + "bin", result);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo gamedata = new DirectoryInfo(path + @"\Android\masterdata\");
            DirectoryInfo folder = new DirectoryInfo(path + @"\Android\");
            string output = "";
            if (!System.IO.Directory.Exists(gamedata.FullName))
            {
                MessageBox.Show("没有游戏数据,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button5.Enabled = true;
                return;
            }
            else
            {
                if (!File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvt") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtLimit") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstCv") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstIllustrator") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtCard") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDevice") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtTreasureDevice") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstTreasureDeviceDetail") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkill") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSkillDetail") ||
                    !File.Exists(gamedata.FullName + "decrypted_masterdata/" + "mstSvtSkill"))
                {
                    MessageBox.Show("游戏数据损坏,请先点击下方的按钮下载游戏数据.", "温馨提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button5.Enabled = true;
                    return;
                }
            }
            string mstSvt = File.ReadAllText(gamedata.FullName + "decrypted_masterdata/" + "mstSvt");
            JArray mstSvtArray = (JArray)JsonConvert.DeserializeObject(mstSvt);
            foreach (var svtIDtmp in mstSvtArray) //查找某个字段与值
            {
                output = output + "ID: " + ((JObject)svtIDtmp)["id"].ToString() + "    " + "名称: " + ((JObject)svtIDtmp)["name"].ToString() + "\r\n";
            }
            File.WriteAllText(path + "/SearchIDList.txt", output);
            MessageBox.Show("导出成功,文件名为 SearchIDList.txt", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start(path + "/SearchIDList.txt");
        }
    }
}
