using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Utils;

namespace PetriNetworkSimulator.Forms.Dialogs
{
    public partial class ApplicationOptions : PetriNetworkSimulator.Forms.Common.GeneralDialogForm
    {
        public ApplicationOptions()
        {
            InitializeComponent();
            this.nudGridSize.Value = (decimal)(Properties.Settings.Default.GridSize);
            this.cbLanguage.Items.Add(Language.ENGLISH);
            this.cbLanguage.Items.Add(Language.HUNGARIAN);
            this.cbLanguage.SelectedItem = CultureHelper.getInstance().ActualLanguage;
            this.cbShowCustomCursor.Checked = Properties.Settings.Default.ShowCustomCursor;
        }

        protected override void dialogOK()
        {
            Properties.Settings.Default.GridSize = (float)this.nudGridSize.Value;
            Language language = (Language)this.cbLanguage.SelectedItem;
            if (language != null)
            {
                CultureHelper.getInstance().ActualLanguage = language;
            }
            Properties.Settings.Default.ShowCustomCursor = this.cbShowCustomCursor.Checked;
        }

        protected override string information()
        {
            return "Here you can set various options of the application.";
        }


    }
}
