using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Forms.Common;

namespace PetriNetworkSimulator.Forms.Common
{
    public partial class GeneralToolWindow : Form, IUpdateCulture
    {
        
        protected MDIParent parentForm;
        protected ToolStripMenuItem menuItem;

        public string ToolInfo
        {
            set { this.tsslInfo.Text = value; }
        }

        public GeneralToolWindow()
        {
            InitializeComponent();
        }

        public GeneralToolWindow(MDIParent parentForm, ToolStripMenuItem menuItem)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.menuItem = menuItem;
            this.updateCulture();
            CultureHelper.getInstance().changeCulture += new CultureHandler(updateCulture);
            
            this.ttsmO100.Tag = 1.0;
            this.ttsmO80.Tag = 0.8;
            this.ttsmO60.Tag = 0.6;
            this.ttsmO40.Tag = 0.4;
            this.ttsmO20.Tag = 0.2;
        }

        public void updateCulture()
        {
            this.Text = CultureHelper.getInstance().RM.GetString(this.GetType().FullName.ToString());
        }

        private void GeneralToolWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( (this.parentForm != null) && (this.menuItem != null) )
            {
                e.Cancel = true;
                this.menuItem.Checked = false;
                // this.parentForm.setCkeckedToolStripMenuItem(this.menuItem,false);
                this.Hide();
            }
        }

        private void GeneralToolWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (this.parentForm != null)
            {
                if (this.Visible)
                {
                    this.parentForm.writeConsole("Show " + this.Text);
                }
                else
                {
                    this.parentForm.writeConsole("Hide " + this.Text);
                }
            }
        }

        private void changeOpacity(object sender, EventArgs e)
        {
            this.ttsmO100.Checked = false;
            this.ttsmO80.Checked = false;
            this.ttsmO60.Checked = false;
            this.ttsmO40.Checked = false;
            this.ttsmO20.Checked = false;

            if (sender is ToolStripMenuItem)
            {
                if ((sender as ToolStripMenuItem).Tag is Double)
                {
                    this.Opacity = (double)(sender as ToolStripMenuItem).Tag;
                    (sender as ToolStripMenuItem).Checked = true;
                }
            }
        }

        public virtual void draw(PetriNetwork network)
        {
            // 
        }

        public void sendToolWindowToBack()
        {
            if (this.menuItem.Checked)
            {
                this.TopMost = false;
                this.SendToBack();
            } 
        }

        public void bringToolWindowToFront()
        {
            if (this.menuItem.Checked)
            {
                this.BringToFront();
                this.TopMost = true;
            }
        }

    }
}
