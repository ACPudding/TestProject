using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGOServantBasicInformationAnalyzer.Properties
{
    public partial class SkillInfo : Form
    {
        public SkillInfo()
        {
            InitializeComponent();
        }

        private void SkillInfo_Load(object sender, EventArgs e)
        {
            textBox1.Text = SkillLvs.skillnameDisplay;
            textBox2.Text = SkillLvs.skillDetailDisplay;
            textBox3.Text = SkillLvs.skilllv1chargetime;
            textBox4.Text = SkillLvs.skilllv6chargetime;
            textBox5.Text = SkillLvs.skilllv10chargetime;
            textBox6.Text = SkillLvs.skilllv1sval;
            textBox7.Text = SkillLvs.skilllv6sval;
            textBox8.Text = SkillLvs.skilllv10sval;
            textBox9.Text = SkillLvs.SKLFuncstr;
        }
    }
}
