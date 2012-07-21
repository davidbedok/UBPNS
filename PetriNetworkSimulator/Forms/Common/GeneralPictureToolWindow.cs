using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Forms.Dialogs;
using PetriNetworkSimulator.Entities.Network;

namespace PetriNetworkSimulator.Forms.Common
{
    public class GeneralPictureToolWindow : GeneralToolWindow
    {
        protected int width;
        protected int height;

        protected double stretchX;
        protected double stretchY;

        protected PetriNetwork network;

        public GeneralPictureToolWindow()
        {
           //
        }

        public GeneralPictureToolWindow(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
           //
        }

        protected void calculateStrech(PictureBox picture)
        {
            this.stretchX = (double)((double)this.width / picture.Size.Width);
            this.stretchY = (double)((double)this.height / picture.Size.Height);
        }

        protected void init(PictureBox picture, int width, int height)
        {
            this.width = width;
            this.height = height;
            picture.Image = new Bitmap(this.width, this.height);
            this.calculateStrech(picture);
        }

        protected void exportAsImage(PictureBox picture)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image (*.png)|*.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picture.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        protected PointF convertPixelToCoord(int x, int y)
        {
            return new PointF((int)(x * this.stretchX), (int)(y * this.stretchY));
        }

        protected void exportAsTextOrTeX(string[] data)
        {
            if (this.network != null)
            {
                TextExportForm export = new TextExportForm();
                export.ExportText = data;
                if (export.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        protected void clear(PictureBox picture)
        {
            Graphics g = Graphics.FromImage(picture.Image);
            g.Clear(Color.White);
        }

    }
}
