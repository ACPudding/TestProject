using System;
using System.IO;
using System.Windows.Forms;
using FGOServantBasicInformationAnalyzer.Properties;

namespace FGOServantBasicInformationAnalyzer
{
    public partial class JibanText : Form
    {
        public JibanText()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (SkillLvs.EEB)
                button1.Enabled = false;
            else
                button1.Enabled = true;
            textBox1.Text = JibanStringData.str1;
            textBox4.Text = JibanStringData.str2;
            textBox2.Text = JibanStringData.str3;
            textBox6.Text = JibanStringData.str4;
            textBox3.Text = JibanStringData.str5;
            textBox7.Text = JibanStringData.str6;
            textBox5.Text = JibanStringData.str7;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = Directory.GetCurrentDirectory();
            var folder = new DirectoryInfo(path + @"\Android\");
            var outputdir = new DirectoryInfo(path + @"\Output\");
            var output = "";
            output = "文本1:\n\r" + JibanStringData.str1 + "\n\r" +
                     "文本2:\n\r" + JibanStringData.str2 + "\n\r" +
                     "文本3:\n\r" + JibanStringData.str3 + "\n\r" +
                     "文本4:\n\r" + JibanStringData.str4 + "\n\r" +
                     "文本5:\n\r" + JibanStringData.str5 + "\n\r" +
                     "文本6:\n\r" + JibanStringData.str6 + "\n\r" +
                     "文本7:\n\r" + JibanStringData.str7;
            if (!Directory.Exists(outputdir.FullName))
                Directory.CreateDirectory(outputdir.FullName);
            File.WriteAllText(outputdir.FullName + "羁绊文本_" + SkillLvs.svtid + "_" + SkillLvs.svtnme + ".txt", output);
            MessageBox.Show(
                "导出完成.\n\r文件名为: " + outputdir.FullName + "羁绊文本_" + SkillLvs.svtid + "_" + SkillLvs.svtnme + ".txt",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}