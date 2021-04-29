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
    public partial class ModalSettings : Form
    {

        /// <summary>
        /// Controls the number of grid columns in the universe
        /// </summary>
        public int UniverseHeight { get { return m_universeHeight; } }

        /// <summary>
        /// Controls the number of grid rows in the universe
        /// </summary>
        public int UniverseWidth { get {return m_universeWidth; } }
        /// <summary>
        /// Controls the delay between the ticks of generations
        /// </summary>
        public int GenerationalDelay { get { return m_generationalDelay; } }

        public Form1.BoundaryType BoundaryType { get; private set; }

        private int m_universeWidth;
        private int m_universeHeight;
        private int m_generationalDelay;
        private bool m_boundaryChangedOnInit = false;

        public ModalSettings(int _uniHeight, int _uniWidth, int _delay, Form1.BoundaryType _boundaryType)
        {
            InitializeComponent();

            universeWidthText.Text = _uniWidth.ToString();
            universeHeightText.Text = _uniHeight.ToString();
            generationalDelayText.Text = _delay.ToString();

            m_universeWidth = _uniWidth;
            m_universeHeight = _uniHeight;
            m_generationalDelay = _delay;

            m_boundaryChangedOnInit = true;
            BoundaryType = _boundaryType;

            universeWidthText.TextChanged += UniverseWidthText_TextChanged;
            universeHeightText.TextChanged += UniverseHeightText_TextChanged;
            generationalDelayText.TextChanged += GenerationalDelayText_TextChanged;

            boundaryComboBox.SelectedIndexChanged += BoundaryComboBox_SelectedIndexChanged;

            InitializeBoundaryComboBox();
        }

        #region Events
        private void BoundaryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!m_boundaryChangedOnInit)
            {
                BoundaryType = (Form1.BoundaryType)boundaryComboBox.SelectedItem;
                
            }

            m_boundaryChangedOnInit = false;
        }

        private void InitializeBoundaryComboBox()
        {
            boundaryComboBox.DataSource = Enum.GetValues(typeof(Form1.BoundaryType));
            boundaryComboBox.SelectedItem = BoundaryType;
        }
        private void GenerationalDelayText_TextChanged(object sender, EventArgs e)
        {
            ValidateTextToInt(ref generationalDelayText, ref m_generationalDelay);
        }

        private void UniverseHeightText_TextChanged(object sender, EventArgs e)
        {
            ValidateTextToInt(ref universeHeightText, ref m_universeHeight);
        }

        private void UniverseWidthText_TextChanged(object sender, EventArgs e)
        {
            ValidateTextToInt(ref universeWidthText, ref m_universeWidth);
        }
        private void ValidateTextToInt(ref TextBox _target, ref int _destination)
        {
            int textLength = _target.Text.Length;
            string currentText = _target.Text;
            if (textLength == 0)
                return;

            int lastNum = -1;
            char lastChar = currentText[textLength - 1];
            if(!int.TryParse(lastChar.ToString(),out lastNum))
            {
                string updatedString = string.Empty;
                for(int i = 0; i < textLength - 1; i++)
                {
                    updatedString += currentText[i];
                }
                _target.Text = updatedString;
            }
            else
            {
                _destination = int.Parse(_target.Text);
            }
        }

        #endregion
    }
}
