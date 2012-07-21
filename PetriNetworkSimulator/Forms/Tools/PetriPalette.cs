using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Main;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;

namespace PetriNetworkSimulator.Forms.Tools
{
    public delegate void NetworkActionHandler(NetworkToolboxAction selectedAction);
    public delegate void NetworkItemHandler(NetworkToolboxItem selectedItem);

    public partial class PetriPalette : GeneralToolWindow
    {
        private NetworkToolboxItem selectedItem;
        private NetworkToolboxAction selectedAction;
        public event NetworkActionHandler selectedActionChanged;
        public event NetworkItemHandler selectedItemChanged;

        public NetworkToolboxItem SelectedItem
        {
            get { return this.selectedItem; }
        }

        public NetworkToolboxAction SelectedAction
        {
            get { return this.selectedAction; }
        }

        public PetriPalette()
        {
            InitializeComponent();
        }

        public PetriPalette(MDIParent parentForm, ToolStripMenuItem menuItem)
            : base(parentForm, menuItem)
        {
            InitializeComponent();

            bClear.Tag = NetworkToolboxAction.CLEAR;
            bSelectAll.Tag = NetworkToolboxAction.SELECTALL;
            bClearSelection.Tag = NetworkToolboxAction.CLEARSELECTION;
            bReverseSelection.Tag = NetworkToolboxAction.REVERSESELECTION;
            bDeleteSelected.Tag = NetworkToolboxAction.DELETESELECTED;

            rbSelect.Tag = NetworkToolboxItem.SELECT;
            rbSingleSelect.Tag = NetworkToolboxItem.SINGLESELECT;
            rbSelectEdge.Tag = NetworkToolboxItem.SELECTEDGE;
            rbMove.Tag = NetworkToolboxItem.MOVE;
            rbDelete.Tag = NetworkToolboxItem.DELETE;
            rbPosition.Tag = NetworkToolboxItem.POSITION;
            rbTransition.Tag = NetworkToolboxItem.TRANSITION;
            rbEdge.Tag = NetworkToolboxItem.EDGE;
            rbToken.Tag = NetworkToolboxItem.TOKEN;
            rbDeleteToken.Tag = NetworkToolboxItem.DELETETOKEN;
            rbNote.Tag = NetworkToolboxItem.NOTE;
        }

        private void commonButtonClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                if ((sender as Button).Tag is NetworkToolboxAction)
                {
                    this.selectedAction = (NetworkToolboxAction)(sender as Button).Tag;
                    this.parentForm.writeConsole("PetriPalette - selected action: " + this.selectedAction);
                    if (this.selectedActionChanged != null)
                    {
                        this.selectedActionChanged(this.selectedAction);
                    }
                }
            }
        }

        private void commonRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if ((sender as RadioButton).Checked)
                {
                    if ((sender as RadioButton).Tag is NetworkToolboxItem)
                    {
                        this.selectedItem = (NetworkToolboxItem)(sender as RadioButton).Tag;
                        this.parentForm.writeConsole("PetriPalette - selected item: " + this.selectedItem);
                        if (this.selectedItemChanged != null)
                        {
                            this.selectedItemChanged(this.selectedItem);
                        }
                    }
                }
            }
        }

        public override void draw(PetriNetwork network)
        {
            //
        }

    }
}
