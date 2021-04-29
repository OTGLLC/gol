using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class ModalRandom : Form
    {
        public bool UseSeed { get; private set; }
        public bool UseRandom { get; private set; }
        public int CurrentSeed { get; private set; }
        public ModalRandom()
        {
            InitializeComponent();

            noSeedRandom.CheckedChanged += SeedRandom_CheckedChanged;
            seedRandom.CheckedChanged += SeedRandom_CheckedChanged;
            noRandom.CheckedChanged += SeedRandom_CheckedChanged;

            seedDisplayPanel.Enabled = false;
        }
        public void OnDisplay(bool _useRandom, bool _useSeed,int _currentSeed)
        {
            seedTextBox.Text = _currentSeed.ToString();
            CurrentSeed = _currentSeed;

            if(!_useRandom)
            {
                noRandom.Checked = true;
            }
            else if(_useSeed)
            {
                seedRandom.Checked = true;
            }
            else if(_useRandom && !_useSeed)
            {
                noSeedRandom.Checked = true;
            }
        }
        private void SeedRandom_CheckedChanged(object sender, EventArgs e)
        {
            if(noSeedRandom.Checked)
            {
                UseRandom = true;
                UseSeed = false;
                seedDisplayPanel.Enabled = false;
            }
            else if(seedRandom.Checked)
            {
                UseRandom = true;
                UseSeed = true;
                seedDisplayPanel.Enabled = true;
            }
            else if(noRandom.Checked)
            {
                UseRandom = false;
                UseSeed = false;
                seedDisplayPanel.Enabled = false;
                seedDisplayPanel.Enabled = false;
            }
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void randomSeedButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            seedTextBox.Text = r.Next(1000, int.MaxValue).ToString();
            CurrentSeed = int.Parse(seedTextBox.Text);
        }
    }
}
