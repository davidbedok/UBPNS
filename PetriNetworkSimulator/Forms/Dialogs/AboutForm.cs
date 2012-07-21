using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class AboutForm : Form
    {

        private Version version;

        public AboutForm()
        {
            InitializeComponent();
            this.version = Assembly.GetExecutingAssembly().GetName().Version;
            this.lVersion.Text = this.version.ToString();
        }
    }
}
