using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class TextExportForm : GeneralDialogForm
    {

        public String[] ExportText
        {
            set {
                this.rtbOutput.Lines = value;
            }
        }

        public TextExportForm()
        {
            InitializeComponent();
            this.hideCancelButton();
        }

        protected override string information()
        {
            return "Here you can see data in pure text (or TeX) format. You can put it to the clipboard, and use it anywhere, for instance it will be a part of the network's documentation.";
        }

    }
}
