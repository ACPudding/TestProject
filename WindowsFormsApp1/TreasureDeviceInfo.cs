using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGOServantBasicInformationAnalyzer;
using FGOServantBasicInformationAnalyzer.Properties;

namespace FGOServantBasicInformationAnalyzer
{
    public partial class TreasureDeviceInfo : Form
    {
        public TreasureDeviceInfo()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TreasureDeviceInfo_Load(object sender, EventArgs e)
        {
            textBox25.Text = TreasureDevices.TDcardHitsDisplay;
            textBox26.Text = TreasureDevices.TDcardtypeDisplay;
            textBox27.Text = TreasureDevices.TDtypeDisplay;
            textBox28.Text = TreasureDevices.TDrankDisplay;
            textBox29.Text = TreasureDevices.TDrubyDisplay;
            textBox30.Text = TreasureDevices.TDnameDisplay;
            textBox31.Text = TreasureDevices.TDDetailDisplay;
            textBox1.Text = TreasureDevices.TD1Display;
            textBox2.Text = TreasureDevices.TD2Display;
            textBox3.Text = TreasureDevices.TD3Display;
            textBox4.Text = TreasureDevices.TD4Display;
            textBox5.Text = TreasureDevices.TD5Display;
        }
    }
}
