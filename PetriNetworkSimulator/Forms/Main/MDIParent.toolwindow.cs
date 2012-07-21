using System;
using System.Collections.Generic;
using PetriNetworkSimulator.Forms.Tools;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Forms.Common;
using System.Windows.Forms;
using System.Drawing;
using PetriNetworkSimulator.Entities.State.Vector;
using System.IO;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Entities.Item.NetTransition;

namespace PetriNetworkSimulator.Forms.Main
{
    public partial class MDIParent
    {
        private List<GeneralToolWindow> toolWindowList; 

        public NetworkToolboxItem SelectedToolboxItem
        {
            get {
                PetriPalette pp = (PetriPalette)this.getToolWindow(typeof(PetriPalette));
                return pp.SelectedItem; 
            }
        }

        public GeneralToolWindow getToolWindow(Type toolWindowType)
        {
            GeneralToolWindow ret = null;
            foreach (GeneralToolWindow toolWindow in this.toolWindowList)
            {
                if (toolWindow.GetType().Equals(toolWindowType))
                {
                    ret = toolWindow;
                    break;
                }
            }
            return ret;
        }

        private void initToolWindow(GeneralToolWindow toolWindow, ToolStripMenuItem menuItem, ToolStripButton buttonItem, bool visible, Point location)
        {
            menuItem.Tag = toolWindow;
            buttonItem.Tag = menuItem;
            if (visible)
            {
                toolWindow.Show();
                toolWindow.Location = location;
                menuItem.Checked = true;
            }
            this.toolWindowList.Add(toolWindow);
        }

        private void initToolWindows(){
            this.toolWindowList = new List<GeneralToolWindow>();

            StateHierarchyTool stateHierarchy = new StateHierarchyTool(this, this.stateHierarchyToolStripMenuItem);
            this.initToolWindow(stateHierarchy, this.stateHierarchyToolStripMenuItem, this.tsbStateHierarchy, Properties.Settings.Default.StateHierarchy, Properties.Settings.Default.LocationStateHierarchy);
            stateHierarchy.callStateVector += new StateVectorHandler(stateMatrix_callStateVector);
            stateHierarchy.callAction += new StateActionHandler(stateAction_callAction);

            StateMatrix stateMatrix = new StateMatrix(this, this.stateMatrixToolStripMenuItem);
            this.initToolWindow(stateMatrix, this.stateMatrixToolStripMenuItem, this.tsbStateMatrix, Properties.Settings.Default.StateTransitionDiagram, Properties.Settings.Default.LocationStateTransitionDiagram);
            stateMatrix.callAction += new StateActionHandler(stateAction_callAction);
            stateMatrix.callStateVector += new StateVectorHandler(stateMatrix_callStateVector);
            stateMatrix.callStateMatrixSettings += new StateMatrixSettingsHandler(stateMatrix_callStateMatrixSettings);

            NeighborhoodMatrix neighborhoodMatrix = new NeighborhoodMatrix(this, this.neighborhoodMatrixToolStripMenuItem);
            this.initToolWindow(neighborhoodMatrix, this.neighborhoodMatrixToolStripMenuItem, this.tsbNeighborhoodMatrix, Properties.Settings.Default.NeighborhoodMatrix, Properties.Settings.Default.LocationNeighborhoodMatrix);

            PetriPalette petriPalette = new PetriPalette(this, this.petriPaletteToolStripMenuItem);
            this.initToolWindow(petriPalette, this.petriPaletteToolStripMenuItem, this.tsbPetriPalette, Properties.Settings.Default.PetriPalette, Properties.Settings.Default.LocationPetriPalette);
            petriPalette.selectedActionChanged += new NetworkActionHandler(petriPalette_selectedActionChanged);
            petriPalette.selectedItemChanged += new Tools.NetworkItemHandler(petriPalette_selectedItemChanged);

            MiniMap miniMap = new MiniMap(this, this.miniMapToolStripMenuItem);
            this.initToolWindow(miniMap, this.miniMapToolStripMenuItem, this.tsbMiniMap, Properties.Settings.Default.MiniMap, Properties.Settings.Default.LocationMiniMap);

            StatisticsTool statisticsTool = new StatisticsTool(this, this.statisticsToolStripMenuItem);
            this.initToolWindow(statisticsTool, this.statisticsToolStripMenuItem, this.tsbStatistics, Properties.Settings.Default.StatisticsTool, Properties.Settings.Default.LocationStatisticsTool);

            PetriEventList petriEventList = new PetriEventList(this, this.petriEventListToolStripMenuItem);
            this.initToolWindow(petriEventList, this.petriEventListToolStripMenuItem, this.tsbPetriEventList, Properties.Settings.Default.PetriEventList, Properties.Settings.Default.LocationPetriEventList);

            TransitionHistory transitionHistory = new TransitionHistory(this, this.transitionHistoryToolStripMenuItem);
            this.initToolWindow(transitionHistory, this.transitionHistoryToolStripMenuItem, this.tsbTransitionHistory, Properties.Settings.Default.TransitionHistory, Properties.Settings.Default.LocationTransitionHistory);
            transitionHistory.selectTransition += new TransitionHistoryHandler(transitionHistory_selectTransition);
        }

        private void transitionHistory_selectTransition(Transition selectedTransition)
        {
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                (this.ActiveMdiChild as PetriNetworkForm).singleSelectItem(selectedTransition);
                (this.ActiveMdiChild as PetriNetworkForm).parentCallReDraw(NetworkToolboxAction.REFRESH, false);
            }
        }

        private void petriPalette_selectedItemChanged(NetworkToolboxItem selectedItem)
        {
            Cursor cursor = Cursors.Default;
            switch (selectedItem)
            {
                case NetworkToolboxItem.DELETE:
                    cursor = Cursors.Cross;
                    break;
                case NetworkToolboxItem.DELETETOKEN:
                    cursor = Cursors.Cross;
                    break;
                case NetworkToolboxItem.MOVE:
                    cursor = Cursors.SizeAll;
                    break;
                case NetworkToolboxItem.SELECT:
                    cursor = Cursors.Default;
                    break;
                case NetworkToolboxItem.SINGLESELECT:
                    cursor = Cursors.Hand;
                    break;
            }
            if (Properties.Settings.Default.ShowCustomCursor)
            {
                switch (selectedItem)
                {
                    case NetworkToolboxItem.EDGE:
                        cursor = CursorHelper.getInstance().EdgeDefCursor;
                        break;
                    case NetworkToolboxItem.NOTE:
                        cursor = CursorHelper.getInstance().NoteCursor;
                        break;
                    case NetworkToolboxItem.POSITION:
                        cursor = CursorHelper.getInstance().PositionCursor;
                        break;
                    case NetworkToolboxItem.SELECTEDGE:
                        cursor = CursorHelper.getInstance().SelectEdgeCursor;
                        break;
                    case NetworkToolboxItem.TOKEN:
                        cursor = CursorHelper.getInstance().TokenCursor;
                        break;
                    case NetworkToolboxItem.TRANSITION:
                        cursor = CursorHelper.getInstance().TransitionCursor;
                        break;
                }
            }
            foreach (Form child in this.MdiChildren)
            {
                child.Cursor = cursor;
            }
            // only for MOVE tool..
            this.reDrawActivePetriNetwork(false);
        }

        /// <summary>
        /// Refresh Petri net of the MiniMap, StateMatrix, etc.
        /// </summary>
        public void refreshToolWindowsPetriNetwork(PetriNetwork network)
        {
            PetriNetwork actNetwork = this.getActivePetriNetwork();
            if ( ( ( actNetwork != null ) && ( actNetwork.Equals(network) ) ) || ( network == null ) ) {
                foreach (GeneralToolWindow toolWindow in this.toolWindowList)
                {
                    if (toolWindow.Visible)
                    {
                        toolWindow.draw(network);
                    }
                }
            }
        }

        /// <summary>
        /// Refresh Petri net of the MiniMap, StateMatrix, etc.
        /// </summary>
        public void refreshMiniMap(PetriNetwork network)
        {
            PetriNetwork actNetwork = this.getActivePetriNetwork();
            if ( ( ( actNetwork != null ) && ( actNetwork.Equals(network) ) ) || ( network == null ) )
            {
                MiniMap miniMap = (MiniMap)this.getToolWindow(typeof(MiniMap));
                if ((miniMap != null) && (miniMap.Visible))
                {
                    miniMap.draw(network);
                }
            }
        }

        private void disabledToolAlwaysOnTopToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
            {
                foreach (GeneralToolWindow toolWindow in this.toolWindowList)
                {
                    toolWindow.sendToolWindowToBack();
                }
            }
            else
            {
                foreach (GeneralToolWindow toolWindow in this.toolWindowList)
                {
                    toolWindow.bringToolWindowToFront();
                }
            }
        }

        private void stateMatrix_callStateMatrixSettings(FireRule fireRule, int millisecondsTimeoutForSimulation)
        {
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                if ((this.ActiveMdiChild as PetriNetworkForm).Network != null)
                {
                    PetriNetwork network = (this.ActiveMdiChild as PetriNetworkForm).Network;
                    network.ActualFireRule = fireRule;
                    network.SimulationTimeout = millisecondsTimeoutForSimulation;
                }
            }
        }

        private void stateMatrix_callStateVector(StateVector stateVector)
        {
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                if ((this.ActiveMdiChild as PetriNetworkForm).Network != null)
                {
                    PetriNetwork network = (this.ActiveMdiChild as PetriNetworkForm).Network;
                    network.returnToState(stateVector);
                    this.reDrawActivePetriNetwork(false);
                    this.refreshToolWindowsPetriNetwork(network);
                }
            }
        }

        private void stateAction_callAction(StateMatrixAction action, PetriNetwork network)
        {
            if (network != null)
            {
                bool reDraw = true;
                switch (action)
                {
                    case StateMatrixAction.CLEAR:
                        network.clearStateMatrix(false);
                        break;
                    case StateMatrixAction.FIRE:
                        network.fire(false);
                        this.reDrawActivePetriNetwork(false);
                        break;
                    case StateMatrixAction.CLEARALLSTATE:
                        network.deleteAllStates();
                        break;
                    case StateMatrixAction.AUTOFIRE:
                        if (this.ActiveMdiChild is PetriNetworkForm)
                        {
                            (this.ActiveMdiChild as PetriNetworkForm).autoFire();
                            reDraw = false;
                        }
                        break;
                    case StateMatrixAction.STOPAUTOFIRE:
                        if (this.ActiveMdiChild is PetriNetworkForm)
                        {
                            (this.ActiveMdiChild as PetriNetworkForm).stopAutoFire();
                        }
                        break;
                }
                if (reDraw)
                {
                    this.refreshToolWindowsPetriNetwork(network);
                }
                else
                {
                    this.refreshMiniMap(network);
                }
            }
        }

        /// <summary>
        /// Fire when call an action in Palette.
        /// </summary>
        private void petriPalette_selectedActionChanged(NetworkToolboxAction selectedAction)
        {
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                if ((this.ActiveMdiChild as PetriNetworkForm).Network != null)
                {
                    (this.ActiveMdiChild as PetriNetworkForm).parentCallReDraw(selectedAction);
                }
            }
        }

        private void toolWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                if ((sender as ToolStripMenuItem).Tag is GeneralToolWindow)
                {
                    GeneralToolWindow toolWindow = (GeneralToolWindow)(sender as ToolStripMenuItem).Tag;
                    if (((ToolStripMenuItem)sender).Checked)
                    {
                        if (!toolWindow.Visible)
                        {
                            toolWindow.Show();
                            toolWindow.draw(this.getActivePetriNetwork());
                        }
                    }
                    else
                    {
                        if (toolWindow.Visible)
                        {
                            toolWindow.Hide();
                        }
                    }
                }
            }
        }

        private void toolWindowToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                if ((sender as ToolStripButton).Tag is ToolStripMenuItem)
                {
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender as ToolStripButton).Tag;
                    menuItem.Checked = !menuItem.Checked;
                    this.toolWindowToolStripMenuItem_Click(menuItem, e);
                }
            } 
        }

    }
}
