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
    public partial class Warning : Form
    {
        public Warning(string _message)
        {
            InitializeComponent();

            warningText.Text = _message;
        }
    }
}
