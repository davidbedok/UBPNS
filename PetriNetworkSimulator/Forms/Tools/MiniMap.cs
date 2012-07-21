using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Network;

namespace PetriNetworkSimulator.Forms.Tools
{
    public partial class MiniMap : PetriNetworkSimulator.Forms.Common.GeneralToolWindow
    {
      
        public MiniMap()
        {
            InitializeComponent();
        }

        public MiniMap(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();
            this.pbMiniMap.Image = new Bitmap(100, 100);
            this.pbMiniMap.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void initMiniMap(PetriNetwork network)
        {
            this.pbMiniMap.Image = new Bitmap(network.Width, network.Height);
        }

        public override void draw(PetriNetwork network)
        {
            if (this.menuItem.Checked)
            {
                if (network != null)
                {
                    this.initMiniMap(network);
                    Graphics g = Graphics.FromImage(this.pbMiniMap.Image);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.White);
                    network.draw(g, null, false);
                }
                else
                {
                    Graphics g = Graphics.FromImage(this.pbMiniMap.Image);
                    g.Clear(Color.White);
                }
                this.pbMiniMap.Refresh();
            }
        }

    }
}
