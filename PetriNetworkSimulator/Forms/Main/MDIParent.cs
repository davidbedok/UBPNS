using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Tools;
using PetriNetworkSimulator.Forms.Dialogs;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Entities.Edge;
using PetriNetworkSimulator.Entities.Item.NetNote;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Exceptions;
using PetriNetworkSimulator.RecentFiles;
using System.IO;

namespace PetriNetworkSimulator.Forms.Main
{
    public partial class MDIParent : Form, IUpdateCulture
    {

        private long messageNumber;
        private int tmpPropertyPanelRowCount;
        private int tmpTokenPanelRowCount;
        private int tmpEventPanelRowCount;
        private int numberOfMdiChildren;
        private Random rand;
        private RecentFilesHelper recentFilesHelper;

        public bool AlignToGrid
        {
            get { return this.alignToGridToolStripMenuItem.Checked; }
        }

        public bool ShowGrid
        {
            get { return this.showGridToolStripMenuItem.Checked; }
        }

        public MDIParent()
        {
            InitializeComponent();
            this.messageNumber = 0;

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            /*
            this.petriNetworkToolStripMenuItem.Enabled = false;
            if (CryptoHelper.getInstance().HasKey)
            {
                this.petriNetworkToolStripMenuItem.Enabled = true;
            }*/
            
            CryptoHelper.getInstance().cryptoEvent += new CryptoHandler(MDIParent_cryptoEvent);
            CryptoHelper.getInstance().initTrustStore();

            this.rand = new Random();
            
            this.horizontalToolStripMenuItem.Tag = MdiLayout.TileHorizontal;
            this.verticalToolStripMenuItem.Tag = MdiLayout.TileVertical;
            this.cascadeToolStripMenuItem.Tag = MdiLayout.Cascade;
            this.arrangeIconsToolStripMenuItem.Tag = MdiLayout.ArrangeIcons;

            this.updateCulture();
            CultureHelper.getInstance().changeCulture += new CultureHandler(updateCulture);

            this.StartPosition = FormStartPosition.Manual;
            this.Location = Properties.Settings.Default.Location;
            this.WindowState = Properties.Settings.Default.State;
            if (this.WindowState == FormWindowState.Normal) this.Size = Properties.Settings.Default.Size;

            this.initToolWindows();

            this.tsslSelectRectangle.Text = "";
            this.lToken.Visible = false;
            this.refreshToolWindowsPetriNetwork(null);
            this.numberOfMdiChildren = 0;

            this.showGridToolStripMenuItem.Checked = Properties.Settings.Default.ShowGrid;
            this.alignToGridToolStripMenuItem.Checked = Properties.Settings.Default.AlignToGrid;
            this.loadRecentFiles();
            this.writeConsole("PetriNetworkSimulator started.");

            this.tsbFire.Tag = StateMatrixAction.FIRE;
            this.tsbAutoFire.Tag = StateMatrixAction.AUTOFIRE;
            this.tsbStop.Tag = StateMatrixAction.STOPAUTOFIRE;
        }

        private void MDIParent_cryptoEvent(string message)
        {
            this.writeConsole(message);
        }

        private void loadRecentFiles()
        {
            this.recentFilesHelper = new RecentFilesHelper();
            List<RecentFile> recentFiles = recentFilesHelper.RecentFiles;
            int i = 0;
            Keys[] keys = new Keys[] { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };
            
            if (recentFiles != null)
            {
                foreach (RecentFile rf in recentFiles)
                {   
                    ToolStripMenuItem recentFile = new ToolStripMenuItem();
                    recentFile.Name = "tsmiRecentFile_"+i.ToString();
                    if (i < 9)
                    {
                        recentFile.ShortcutKeys = ((Keys)((Keys.Control | keys[i])));
                    }
                    recentFile.Size = new System.Drawing.Size(187, 22);
                    recentFile.Text = rf.NetworkName + " (" + Path.GetFileName(rf.FileName) + ")";
                    recentFile.ToolTipText = rf.Description;
                    recentFile.Tag = rf;
                    recentFile.Click += new EventHandler(recentFile_Click);
                    this.recentFilesToolStripMenuItem.DropDownItems.Add(recentFile);
                    i++;
                }
            }
        }

        private void recentFile_Click(object sender, EventArgs e)
        {
            if ( (sender is ToolStripMenuItem) && ( ((ToolStripMenuItem)sender).Tag != null) )
            {
                if ( ( (ToolStripMenuItem)sender).Tag is RecentFile ) {
                    RecentFile rf = (RecentFile)((ToolStripMenuItem)sender).Tag;
                    if (rf.FileName != null)
                    {
                        this.openPetriNetwork(rf.FileName);
                    }
                }
            }
        }

        public void updateCulture()
        {
            this.Text = CultureHelper.getInstance().RM.GetString("applicationTitle") + " - " + CryptoHelper.getInstance().Subject;
        }

        private void layoutWindow_Click(object sender, EventArgs e)
        {
            this.LayoutMdi((MdiLayout)((ToolStripMenuItem)sender).Tag);
            this.writeConsole("Layout changed (" + ((ToolStripMenuItem)sender).Tag + ").");
        }

        private void MDIParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.StateTransitionDiagram = this.stateMatrixToolStripMenuItem.Checked;
            Properties.Settings.Default.NeighborhoodMatrix = this.neighborhoodMatrixToolStripMenuItem.Checked;
            Properties.Settings.Default.PetriPalette = this.petriPaletteToolStripMenuItem.Checked;
            Properties.Settings.Default.MiniMap = this.miniMapToolStripMenuItem.Checked;
            Properties.Settings.Default.StateHierarchy = this.stateHierarchyToolStripMenuItem.Checked;
            Properties.Settings.Default.StatisticsTool = this.statisticsToolStripMenuItem.Checked;
            Properties.Settings.Default.PetriEventList = this.petriEventListToolStripMenuItem.Checked;
            Properties.Settings.Default.TransitionHistory = this.transitionHistoryToolStripMenuItem.Checked;

            foreach (GeneralToolWindow toolWindow in this.toolWindowList)
            {
                if (toolWindow is MiniMap)
                {
                    Properties.Settings.Default.LocationMiniMap = toolWindow.Location;
                }
                else if (toolWindow is NeighborhoodMatrix)
                {
                    Properties.Settings.Default.LocationNeighborhoodMatrix = toolWindow.Location;
                } else if (toolWindow is PetriEventList)
                {
                    Properties.Settings.Default.LocationPetriEventList = toolWindow.Location;
                }
                else if (toolWindow is PetriPalette)
                {
                    Properties.Settings.Default.LocationPetriPalette = toolWindow.Location;
                }
                else if (toolWindow is StateHierarchyTool)
                {
                    Properties.Settings.Default.LocationStateHierarchy = toolWindow.Location;
                }
                else if (toolWindow is StateMatrix)
                {
                    Properties.Settings.Default.LocationStateTransitionDiagram = toolWindow.Location;
                }
                else if (toolWindow is StatisticsTool)
                {
                    Properties.Settings.Default.LocationStatisticsTool = toolWindow.Location;
                }
                else if (toolWindow is TransitionHistory)
                {
                    Properties.Settings.Default.LocationTransitionHistory = toolWindow.Location;
                }
            }

            Properties.Settings.Default.ShowGrid = this.showGridToolStripMenuItem.Checked;
            Properties.Settings.Default.AlignToGrid = this.alignToGridToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            try
            {
                this.recentFilesHelper.saveToFile();
            }
            catch (SimApplicationException ex)
            {
                ;
            }
        }

        private void MDIParent_Resize(object sender, EventArgs e)
        {
            Properties.Settings.Default.State = this.WindowState;
            if (this.WindowState == FormWindowState.Normal) Properties.Settings.Default.Size = this.Size;
        }

        private void MDIParent_LocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal) Properties.Settings.Default.Location = this.Location;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Create a new Petri net.
        /// </summary>
        private void petriNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if (CryptoHelper.getInstance().HasKey)
            {
                this.writeConsole("Let's create a new Petri net!");
                try
                {
                    CreateNewPetriNetwork dialog = new CreateNewPetriNetwork(this.rand, this.numberOfMdiChildren);
                    bool restoreCheck = false;
                    if (!this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
                    {
                        restoreCheck = true;
                        this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = true;
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PetriNetworkForm child = new PetriNetworkForm(dialog.Network);
                        child.MdiParent = this;
                        child.networkItemSelected += new NetworkItemHandler(child_networkItemSelected);
                        child.childFormClosed += new ChildFormHandler(childFormClosed);
                        child.simulationNotifier += new SimulationNotifierHandler(childSimulationNotifier);

                        this.beforeOpenMdiChild();
                        child.Show();
                        this.refreshToolWindowsPetriNetwork(dialog.Network);
                        this.reDrawActivePetriNetwork(false);
                        this.child_networkItemSelected(dialog.Network.SelectedItems, false);
                        this.writeConsole("A new Petri network (" + dialog.Network.Name + ") was created.");
                    }
                    if (restoreCheck)
                    {
                        this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = false;
                    }
                }
                catch (SimApplicationException ex)
                {
                    this.writeConsole("Application error. Please send an email to the creators (" + ex.Message + ").");
                }
            }
        }

        private void childSimulationNotifier(PetriNetwork network, FireEvent fireEvent)
        {
            switch (fireEvent)
            {
                case FireEvent.DEADLOCK:
                    StateMatrix stateMatrix = (StateMatrix)this.getToolWindow(typeof(StateMatrix));
                    if (stateMatrix != null)
                    {
                        stateMatrix.stopSimulation(network);
                    }
                    break;
            }
        }

        private void tsbSimulationActionsClick(object sender, EventArgs e)
        {
            PetriNetwork network = this.getActivePetriNetwork();
            if (network != null)
            {
                StateMatrix stateMatrix = (StateMatrix)this.getToolWindow(typeof(StateMatrix));
                if (stateMatrix != null)
                {
                    if (sender is ToolStripButton)
                    {
                        ToolStripButton tsb = (ToolStripButton)sender;
                        if ((tsb.Tag != null) && (tsb.Tag is StateMatrixAction))
                        {
                            StateMatrixAction sma = (StateMatrixAction)tsb.Tag;
                            if (StateMatrixAction.AUTOFIRE.Equals(sma))
                            {
                                this.tsbFire.Enabled = false;
                                this.tsbAutoFire.Enabled = false;
                                this.tsbStop.Enabled = true;
                                stateMatrix.startSimulation(network);
                            }
                            else if (StateMatrixAction.STOPAUTOFIRE.Equals(sma))
                            {
                                this.tsbFire.Enabled = true;
                                this.tsbAutoFire.Enabled = true;
                                this.tsbStop.Enabled = false;
                                stateMatrix.stopSimulation(network);
                            }
                            else if (StateMatrixAction.FIRE.Equals(sma))
                            {
                                stateMatrix.fire(network);
                            }
                        }
                    }

                }
            }

        }

        private void childFormClosed(string name)
        {
            this.refreshToolWindowsPetriNetwork(null);
            this.beforeCloseMdiChild();
            this.writeConsole("A Petri network (" + name + ") was closed.");
        }

        public void beforeOpenMdiChild()
        {
            this.numberOfMdiChildren++;
            if (this.numberOfMdiChildren == 1)
            {
                if (CryptoHelper.getInstance().HasKey)
                {
                    this.saveToolStripMenuItem.Enabled = true;
                    this.saveAsToolStripMenuItem.Enabled = true;
                }
                this.exportAsToolStripMenuItem.Enabled = true;
                this.closeToolStripMenuItem.Enabled = true;
            }
        }

        public void beforeCloseMdiChild()
        {
            this.numberOfMdiChildren--;
            if (this.numberOfMdiChildren == 0)
            {
                if (CryptoHelper.getInstance().HasKey)
                {
                    this.saveToolStripMenuItem.Enabled = false;
                    this.saveAsToolStripMenuItem.Enabled = false;
                }
                this.exportAsToolStripMenuItem.Enabled = false;
                this.closeToolStripMenuItem.Enabled = false;
                this.child_networkItemSelected(null, false);
            }
        }

        private void openPetriNetwork(string fileName)
        {
            try
            {
                PetriNetwork network = PetriNetwork.openFromXml(this.rand, fileName);
                if (network != null)
                {
                    PetriNetworkForm child = new PetriNetworkForm(network);
                    child.MdiParent = this;
                    child.networkItemSelected += new NetworkItemHandler(child_networkItemSelected);
                    child.childFormClosed += new ChildFormHandler(childFormClosed);
                    child.simulationNotifier +=new SimulationNotifierHandler(childSimulationNotifier);
                    
                    this.beforeOpenMdiChild();
                    child.Show();

                    this.reDrawActivePetriNetwork(false);
                    this.child_networkItemSelected(network.SelectedItems, false);
                    this.recentFilesHelper.addRecentFile(network.PetriRecentFile);
                    this.writeConsole("A Petri network (" + network.Name + ") was opened.");
                }
            }
            catch (Exception e)
            {
                this.writeConsole(e.Message);
            }
        }

        /// <summary>
        /// Open an exists Petri net.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.writeConsole("Let's open an existing Petri net!");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Petri Network XML file (*.pn.xml)|*.pn.xml";
            bool restoreCheck = false;
            if (!this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
            {
                restoreCheck = true;
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = true;
            }
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.openPetriNetwork(dialog.FileName);
            }
            if (restoreCheck)
            {
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Fire when any visual item will be selected on Petri network MDI child form.
        /// </summary>
        private void child_networkItemSelected(List<AbstractItem> items, bool forceRefresh)
        {
            if (items != null)
            {
                PetriNetwork network = this.getActivePetriNetwork();
                if (network != null)
                {
                    if (items.Count == 0)
                    {
                        if ((forceRefresh) || (this.tlpProperty.Tag == null) || (!this.tlpProperty.Tag.Equals(network)))
                        {
                            // Petri net properties
                            this.lToken.Visible = false;
                            this.tlpProperty.Visible = false;

                            this.tmpPropertyPanelRowCount = 0;
                            this.tlpProperty.Controls.Clear();
                            this.tlpProperty.RowStyles.Clear();

                            this.tmpTokenPanelRowCount = 0;
                            this.tlpToken.Controls.Clear();
                            this.tlpToken.RowStyles.Clear();

                            this.tmpEventPanelRowCount = 0;
                            this.tlpEvent.Controls.Clear();
                            this.tlpEvent.RowStyles.Clear();

                            this.addCustomPropertyTextRow("Name", network.Name, NetworkProperty.NAME, false);
                            this.addCustomPropertyTextRow("Filename", network.FileName, NetworkProperty.FILENAME, true);
                            this.addCustomPropertyTextRow("Certificate", network.CertificateSubject, NetworkProperty.CERTIFICATESUBJECT, true);
                            this.addCustomPropertyTextRow("Last Modification", network.LastModificationDate.ToString(), NetworkProperty.LASTMODIFICATIONDATE, true);

                            this.addCustomPropertyTextRow("Token prefix", network.IdentityProvider.TokenPrefix, NetworkProperty.TOKENPREFIX, false);
                            this.addCustomPropertyTextRow("Position prefix", network.IdentityProvider.PositionPrefix, NetworkProperty.POSITIONPREFIX, false);
                            this.addCustomPropertyTextRow("Transition prefix", network.IdentityProvider.TransitionPrefix, NetworkProperty.TRANSITIONPREFIX, false);
                            this.addCustomPropertyTextRow("Note prefix", network.IdentityProvider.NotePrefix, NetworkProperty.NOTEPREFIX, false);
                            this.addCustomPropertyTextRow("State prefix", network.StatePrefix, NetworkProperty.STATEPREFIX, false);

                            this.addCustomPropertyNumberRow("Default edge weight", network.DefaultEdgeWeight, 1, PetriNetwork.MAX_EDGE_WIDTH, NetworkProperty.EDGEWEIGHT, false, 0);
                            this.addCustomPropertyNumberRow("Width", network.Width, PetriNetwork.WIDTH_MIN, PetriNetwork.WIDTH_MAX, NetworkProperty.SIZE_WIDTH, false, 0);
                            this.addCustomPropertyNumberRow("Height", network.Height, PetriNetwork.HEIGHT_MIN, PetriNetwork.HEIGHT_MAX, NetworkProperty.SIZE_HEIGHT, false, 0);

                            this.addCustomPropertyFontRow("Default Font", network.VisualSettings.DefaultFont, NetworkProperty.DEFAULTFONT);
                            this.addCustomPropertyFontRow("Note Font", network.VisualSettings.NoteFont, NetworkProperty.NOTEFONT);
                            
                            this.addCustomPropertyColorRow("Default Color", network.VisualSettings.DefaultColor, NetworkProperty.DEFAULTCOLOR);
                            this.addCustomPropertyColorRow("Selected Color", network.VisualSettings.SelectedColor, NetworkProperty.SELECTEDCOLOR);
                            this.addCustomPropertyColorRow("Mark Color", network.VisualSettings.MarkColor, NetworkProperty.MARKCOLOR);
                            this.addCustomPropertyColorRow("Shadow Color", network.VisualSettings.ShadowColor, NetworkProperty.SHADOWCOLOR);
                            this.addCustomPropertyColorRow("NotePen Color", network.VisualSettings.NotePenColor, NetworkProperty.NOTEPENCOLOR);
                            this.addCustomPropertyColorRow("NoteBorder Color", network.VisualSettings.NoteBorderColor, NetworkProperty.NOTEBORDERCOLOR);
                            this.addCustomPropertyColorRow("NoteBrush Color", network.VisualSettings.NoteBrushColor, NetworkProperty.NOTEBRUSHCOLOR);
                            this.addCustomPropertyColorRow("Help Color", network.VisualSettings.HelpColor, NetworkProperty.HELPCOLOR);
                            this.addCustomPropertyColorRow("State Color", network.VisualSettings.StateColor, NetworkProperty.STATECOLOR);
                            this.addCustomPropertyColorRow("Ready to Fire Color", network.VisualSettings.MarkAsReadyToFireColor, NetworkProperty.MARKASREADYTOFIRECOLOR);
                            this.addCustomPropertyColorRow("Clock Color", network.VisualSettings.ClockColor, NetworkProperty.CLOCKCOLOR);

                            this.addCustomPropertyCheckboxRow("Visible Notes", network.VisibleSettings.VisibleNotes, NetworkProperty.VISIBLENOTES);
                            this.addCustomPropertyCheckboxRow("Visible TransitionLabel", network.VisibleSettings.VisibleTransitionLabel, NetworkProperty.VISIBLETRANSITIONLABEL);
                            this.addCustomPropertyCheckboxRow("Visible Priority", network.VisibleSettings.VisiblePriority, NetworkProperty.VISIBLEPRIORITY);
                            this.addCustomPropertyCheckboxRow("Visible Capacity", network.VisibleSettings.VisibleCapacity, NetworkProperty.VISIBLECAPACITY);
                            this.addCustomPropertyCheckboxRow("Visible EdgeWeight", network.VisibleSettings.VisibleEdgeWeight, NetworkProperty.VISIBLEEDGEWEIGHT);
                            this.addCustomPropertyCheckboxRow("Visible PositionLabel", network.VisibleSettings.VisiblePositionLabel, NetworkProperty.VISIBLEPOSITIONLABEL);
                            this.addCustomPropertyCheckboxRow("Visible EdgeLabel", network.VisibleSettings.VisibleEdgeLabel, NetworkProperty.VISIBLEEDGELABEL);
                            this.addCustomPropertyCheckboxRow("Visible HelpCircles", network.VisibleSettings.VisibleEdgeHelpLine, NetworkProperty.VISIBLEEDGEHELPLINE);
                            this.addCustomPropertyCheckboxRow("Visible Ready2fire tran.", network.VisibleSettings.VisibleReadyToFireTransitions, NetworkProperty.VISIBLEREADYTOFIRETRANSACTIONS);
                            this.addCustomPropertyCheckboxRow("Visible Clock", network.VisibleSettings.VisibleClock, NetworkProperty.VISIBLECLOCK);

                            this.addCustomPropertyNumberRow("Unid gen number", network.UnidGenNumber, 0, Int64.MaxValue, NetworkProperty.GENNUMBER, true, 0);
                            this.addCustomPropertyNumberRow("Token gen number", network.IdentityProvider.TokenGenNumber, 0, Int32.MaxValue, NetworkProperty.GENNUMBER, true, 0);
                            this.addCustomPropertyNumberRow("Position gen number", network.IdentityProvider.PositionGenNumber, 0, Int32.MaxValue, NetworkProperty.GENNUMBER, true, 0);
                            this.addCustomPropertyNumberRow("Transition gen number", network.IdentityProvider.TransitionGenNumber, 0, Int32.MaxValue, NetworkProperty.GENNUMBER, true, 0);
                            this.addCustomPropertyNumberRow("Note gen number", network.IdentityProvider.NoteGenNumber, 0, Int32.MaxValue, NetworkProperty.GENNUMBER, true, 0);
                            this.addCustomPropertyNumberRow("State gen number", network.StateGenNumber, 0, Int32.MaxValue, NetworkProperty.GENNUMBER, true, 0);

                            this.addEventRow(network, network.PetriEvents.getEvent(EventType.DEADLOCK), false);
                            this.addEventRow(network, network.PetriEvents.getEvent(EventType.CYCLE), false);
                            this.addEventRow(network, network.PetriEvents.getEvent(EventType.TICK), false);

                            this.tlpEvent.Tag = network;
                            this.addLastEventRow();

                            this.tlpEvent.RowCount = this.tmpEventPanelRowCount;

                            this.tlpToken.RowCount = this.tmpTokenPanelRowCount;

                            this.addLastRow();
                            this.tlpProperty.RowCount = this.tmpPropertyPanelRowCount;
                            this.tlpProperty.Tag = network;

                            this.tlpProperty.Visible = true;
                        }
                    }
                    else if ((items.Count == 1) && (items[0] != null))
                    {
                        AbstractItem item = items[0];
                        if ((forceRefresh) || ((this.tlpProperty.Tag == null) || (!this.tlpProperty.Tag.Equals(item))))
                        {
                            this.tlpProperty.Visible = false;

                            this.lToken.Visible = false;
                            this.tmpPropertyPanelRowCount = 0;
                            this.tlpProperty.Controls.Clear();
                            this.tlpProperty.RowStyles.Clear();

                            this.tmpTokenPanelRowCount = 0;
                            this.tlpToken.Controls.Clear();
                            this.tlpToken.RowStyles.Clear();

                            this.tmpEventPanelRowCount = 0;
                            this.tlpEvent.Controls.Clear();
                            this.tlpEvent.RowStyles.Clear();

                            this.addCustomPropertyTextRow("Name", item.Name, NetworkProperty.NAME, false);
                            this.addCustomPropertyTextRow("Unid", item.Unid.ToString(), NetworkProperty.UNID, true);
                            this.addCustomPropertyCheckboxRow("Show annotation", item.ShowAnnotation, NetworkProperty.SHOWANNOTATION);

                            if (item is AbstractNetworkItem)
                            {
                                AbstractNetworkItem networkItem = (AbstractNetworkItem)item; 
                                this.addCustomPropertyNumberRow("Origo X", networkItem.Origo.X, 0, network.Width, NetworkProperty.ORIGO_X, false, 2);
                                this.addCustomPropertyNumberRow("Origo Y", networkItem.Origo.Y, 0, network.Height, NetworkProperty.ORIGO_Y, false, 2);
                                this.addCustomPropertyNumberRow("Point X", networkItem.Point.X, 0, network.Width, NetworkProperty.POINT_X, true, 2);
                                this.addCustomPropertyNumberRow("Point Y", networkItem.Point.Y, 0, network.Height, NetworkProperty.POINT_Y, true, 2);
                                this.addCustomPropertyNumberRow("Label offset X", networkItem.LabelOffset.X, AbstractNetworkItem.MAX_LABEL_OFFSET * (-1), AbstractNetworkItem.MAX_LABEL_OFFSET, NetworkProperty.LABELOFFSET_X, false, 2);
                                this.addCustomPropertyNumberRow("Label offset Y", networkItem.LabelOffset.Y, AbstractNetworkItem.MAX_LABEL_OFFSET * (-1), AbstractNetworkItem.MAX_LABEL_OFFSET, NetworkProperty.LABELOFFSET_Y, false, 2);

                                if (item is Position)
                                {
                                    Position positionItem = (Position)item;
                                    this.addCustomPropertyNumberRow("Radius", positionItem.Radius, AbstractNetworkItem.MINIMUM_RADIUS, AbstractNetworkItem.MAXIMUM_RADIUS, NetworkProperty.RADIUS, false, 2);
                                    this.addCustomPropertyNumberRow("Size width", positionItem.Size.Width, 0, 200, NetworkProperty.SIZE_WIDTH, true, 2);
                                    this.addCustomPropertyNumberRow("Size height", positionItem.Size.Height, 0, 200, NetworkProperty.SIZE_HEIGHT, true, 2);
                                    this.addCustomPropertyNumberRow("Capacity limit", positionItem.CapacityLimit, 0, 100, NetworkProperty.CAPACITYLIMIT, false, 0);
                                    
                                    if (positionItem.Tokens.Count > 0)
                                    {
                                        this.lToken.Visible = true;
                                        this.addFirstTokenRow();
                                        foreach (AbstractToken token in positionItem.Tokens)
                                        {
                                            this.addCustomTokenRow(positionItem, token, false);
                                        }
                                        this.tlpToken.Tag = item;
                                        this.addLastTokenRow();
                                    }
                                    this.addEventRow(positionItem, positionItem.PetriEvents.getEvent(EventType.PREACTIVATE), false);
                                    this.addEventRow(positionItem, positionItem.PetriEvents.getEvent(EventType.POSTACTIVATE), false);
                                    this.tlpEvent.Tag = positionItem;
                                    this.addLastEventRow();
                                }
                                else if (item is Transition)
                                {
                                    Transition transitionItem = (Transition)item;
                                    this.addCustomPropertyNumberRow("Radius", transitionItem.Radius, 0, 200, NetworkProperty.RADIUS, true, 2);
                                    this.addCustomPropertyNumberRow("Size width", transitionItem.Size.Width, AbstractNetworkItem.MINIMUM_WIDTH, 200, NetworkProperty.SIZE_WIDTH, false, 2);
                                    this.addCustomPropertyNumberRow("Size height", transitionItem.Size.Height, AbstractNetworkItem.MINIMUM_HEIGHT, 200, NetworkProperty.SIZE_HEIGHT, false, 2);
                                    this.addCustomPropertyNumberRow("Angle (rad)", (float)transitionItem.AngleRadian, 0, (float)(Math.PI * 2), NetworkProperty.ANGLERAD, true, 2);
                                    this.addCustomPropertyNumberRow("Angle", (float)transitionItem.Angle, 0, 359, NetworkProperty.ANGLE, false, 2);
                                    this.addCustomPropertyNumberRow("Priority", transitionItem.Priority, 0, 100, NetworkProperty.PRIORITY, false, 0);
                                    this.addCustomPropertyTransitionTypeComboBoxRow(network,"Transition type", transitionItem.TransitionType, NetworkProperty.TRANSITIONTYPE, false);
                                    this.addCustomPropertyNumberRow("Delay", transitionItem.Delay, 0, 5000, NetworkProperty.DELAY, false, 0);
                                    this.addCustomPropertyNumberRow("Clock radius", transitionItem.ClockRadius, 5, 50, NetworkProperty.CLOCKRADIUS, false, 2);
                                    this.addCustomPropertyNumberRow("Clock offset X", transitionItem.ClockOffset.X, AbstractNetworkItem.MAX_LABEL_OFFSET * (-1), AbstractNetworkItem.MAX_LABEL_OFFSET, NetworkProperty.CLOCKOFFSET_X, false, 2);
                                    this.addCustomPropertyNumberRow("Clock offset Y", transitionItem.ClockOffset.Y, AbstractNetworkItem.MAX_LABEL_OFFSET * (-1), AbstractNetworkItem.MAX_LABEL_OFFSET, NetworkProperty.CLOCKOFFSET_Y, false, 2);

                                    this.addEventRow(transitionItem, transitionItem.PetriEvents.getEvent(EventType.PREACTIVATE), false);
                                    this.addEventRow(transitionItem, transitionItem.PetriEvents.getEvent(EventType.POSTACTIVATE), false);
                                    this.tlpEvent.Tag = transitionItem;
                                    this.addLastEventRow();
                                }
                                else if (item is Note)
                                {
                                    Note noteItem = (Note)item;
                                    this.addCustomPropertyNumberRow("Size width", noteItem.Size.Width, 50, 500, NetworkProperty.SIZE_WIDTH, false, 2);
                                    this.addCustomPropertyNumberRow("Size height", noteItem.Size.Height, 20, 300, NetworkProperty.SIZE_HEIGHT, false, 2);
                                    this.addCustomPropertyTextRow("Text", noteItem.Text, NetworkProperty.TEXT, false);
                                }
                            }
                            else if ( item is AbstractEdge )
                            {
                                AbstractEdge edge = (AbstractEdge)item;
                                this.addCustomPropertyNumberRow("Weight", edge.Weight, 1, 10, NetworkProperty.WEIGHT, false, 2);
                                this.addCustomPropertyButtonRow(network, "Change direction", "Go", NetworkProperty.CHANGEDIRECTION, false);
                                this.addCustomPropertyButtonRow(network, "Straighten", "Go", NetworkProperty.STRAIGHTEN, false);
                                this.addCustomPropertyNumberRow("Middle offset X", edge.CurveMiddlePointOffset.X, -1 * network.Width, network.Width, NetworkProperty.CURVE_OFFSET_X, false, 2);
                                this.addCustomPropertyNumberRow("Middle offset Y", edge.CurveMiddlePointOffset.Y, -1 * network.Height, network.Height, NetworkProperty.CURVE_OFFSET_Y, false, 2);
                                if (edge is EdgePositionTransition)
                                {
                                    this.addCustomPropertyEdgeTypeComboBoxRow(network, "Edge type", edge.EdgeType, NetworkProperty.EDGETYPE, false);
                                }
                            }
                            else if (item is AbstractToken)
                            {
                                AbstractToken token = (AbstractToken)item;

                            }

                            this.tlpEvent.RowCount = this.tmpEventPanelRowCount;

                            this.tlpToken.RowCount = this.tmpTokenPanelRowCount;

                            this.addLastRow();
                            this.tlpProperty.RowCount = this.tmpPropertyPanelRowCount;
                            this.tlpProperty.Tag = item;

                            this.tlpProperty.Visible = true;
                        }

                    }
                    else if (items.Count > 1)
                    {
                        NetworkPropertyGroup npg = network.getSelectedPropertyGroup();

                        if ((this.tlpProperty.Tag == null) || (!this.tlpProperty.Tag.Equals(npg)))
                        {
                            this.tlpProperty.Visible = false;

                            this.lToken.Visible = false;
                            this.tmpPropertyPanelRowCount = 0;
                            this.tlpProperty.Controls.Clear();
                            this.tlpProperty.RowStyles.Clear();

                            this.tmpTokenPanelRowCount = 0;
                            this.tlpToken.Controls.Clear();
                            this.tlpToken.RowStyles.Clear();

                            this.tmpEventPanelRowCount = 0;
                            this.tlpEvent.Controls.Clear();
                            this.tlpEvent.RowStyles.Clear();

                            switch (npg)
                            {
                                case NetworkPropertyGroup.TRANSITION:
                                case NetworkPropertyGroup.POSITION:
                                case NetworkPropertyGroup.ABSTRACTNETWORKITEM:
                                    this.addCustomPropertyGroupMoveToolRow("Origo X", 1, 10, network, NetworkProperty.ORIGO_X, false);
                                    this.addCustomPropertyGroupMoveToolRow("Origo Y", 1, 10, network, NetworkProperty.ORIGO_Y, false);
                                    this.addCustomPropertyGroupMoveToolRow("Label offset X", 1, 10, network, NetworkProperty.LABELOFFSET_X, false);
                                    this.addCustomPropertyGroupMoveToolRow("Label offset Y", 1, 10, network, NetworkProperty.LABELOFFSET_Y, false);
                                    this.addCustomPropertyButtonRow(network, "Same origo X", "Go", NetworkProperty.SAMEORIGOX, false);
                                    this.addCustomPropertyButtonRow(network, "Same origo Y", "Go", NetworkProperty.SAMEORIGOY, false);
                                    break;
                                case NetworkPropertyGroup.EDGE:
                                    this.addCustomPropertyGroupMoveToolRow("Weight", 1, 3, network, NetworkProperty.WEIGHT, false);
                                    this.addCustomPropertyButtonRow(network, "Change dir.", "Go", NetworkProperty.CHANGEDIRECTION, false);
                                    break;
                                case NetworkPropertyGroup.NONE:

                                    break;
                                case NetworkPropertyGroup.TOKEN:

                                    break;
                            }
                            if (NetworkPropertyGroup.TRANSITION.Equals(npg))
                            {
                                this.addCustomPropertyGroupMoveToolRow("Size width", 1, 3, network, NetworkProperty.SIZE_WIDTH, false);
                                this.addCustomPropertyGroupMoveToolRow("Size height", 1, 3, network, NetworkProperty.SIZE_HEIGHT, false);
                                this.addCustomPropertyGroupMoveToolRow("Angle", 1, 5, network, NetworkProperty.ANGLE, false);     
                                this.addCustomPropertyGroupMoveToolRow("Priority", 1, 5, network, NetworkProperty.PRIORITY, false);
                                this.addCustomPropertyGroupMoveToolRow("Delay", 50, 500, network, NetworkProperty.DELAY, false);
                                this.addCustomPropertyGroupMoveToolRow("Clock radius", 1, 5, network, NetworkProperty.CLOCKRADIUS, false);
                                this.addCustomPropertyGroupMoveToolRow("Clock offset X", 1, 10, network, NetworkProperty.CLOCKOFFSET_X, false);
                                this.addCustomPropertyGroupMoveToolRow("Clock offset Y", 1, 10, network, NetworkProperty.CLOCKOFFSET_Y, false);
                            }
                            else if (NetworkPropertyGroup.POSITION.Equals(npg))
                            {
                                this.addCustomPropertyGroupMoveToolRow("Radius", 1, 5, network, NetworkProperty.RADIUS, false);
                                this.addCustomPropertyGroupMoveToolRow("Capacity limit", 1, 5, network, NetworkProperty.CAPACITYLIMIT, false);
                            }

                            this.tlpEvent.RowCount = this.tmpEventPanelRowCount;

                            this.tlpToken.RowCount = this.tmpTokenPanelRowCount;

                            this.addLastRow();
                            this.tlpProperty.RowCount = this.tmpPropertyPanelRowCount;
                            this.tlpProperty.Tag = npg;

                            this.tlpProperty.Visible = true;
                        }
                    }
                }
            } else {
                this.tmpPropertyPanelRowCount = 0;
                this.tlpProperty.Controls.Clear();
                this.tlpProperty.RowStyles.Clear();
                this.addLastRow();
                this.tlpProperty.RowCount = this.tmpPropertyPanelRowCount;
                this.tlpProperty.Tag = null;
                this.tmpTokenPanelRowCount = 0;
                this.tlpToken.Controls.Clear();
                this.tlpToken.RowStyles.Clear();
                this.tlpToken.RowCount = this.tmpTokenPanelRowCount;
                this.tlpToken.Tag = null;
                this.tmpEventPanelRowCount = 0;
                this.tlpEvent.Controls.Clear();
                this.tlpEvent.RowStyles.Clear();
                this.tlpEvent.RowCount = this.tmpEventPanelRowCount;
                this.tlpEvent.Tag = null;
            }
        }

        /// <summary>
        /// Refresh Petri net of the MDI child window.
        /// </summary>
        private void reDrawActivePetriNetwork(bool minimapRefresh = true)
        {
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                (this.ActiveMdiChild as PetriNetworkForm).parentCallReDraw(NetworkToolboxAction.REFRESH, minimapRefresh);
            }
        }

        private PetriNetwork getActivePetriNetwork()
        {
            PetriNetwork network = null;
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                if ((this.ActiveMdiChild as PetriNetworkForm).Network != null)
                {
                    network = (this.ActiveMdiChild as PetriNetworkForm).Network;
                }
            }
            return network;
        }

        private void MDIParent_MdiChildActivate(object sender, EventArgs e)
        {
            PetriNetwork network = this.getActivePetriNetwork();
            if (network != null)
            {
                this.refreshToolWindowsPetriNetwork(network);
                this.child_networkItemSelected(network.SelectedItems, false);

                this.tsbFire.Enabled = !network.ActiveSimulation;
                this.tsbAutoFire.Enabled = !network.ActiveSimulation;
                this.tsbStop.Enabled = network.ActiveSimulation;
            }
            else
            {
                this.tsbFire.Enabled = false;
                this.tsbAutoFire.Enabled = false;
                this.tsbStop.Enabled = false;
            }
        }

        private string writeMessageNumber()
        {
            return (++this.messageNumber).ToString().PadLeft(4, ' ') + " | ";
        }

        public void writeConsole(string message)
        {
            if ((message != null) && (!"".Equals(message)))
            {
                message = message.Replace("\n", "");
                this.rtbConsole.Text += this.writeMessageNumber() + message + "\n";
                this.rtbConsole.Select(this.rtbConsole.Text.Length - 2, 1);
                this.rtbConsole.ScrollToCaret();
            }
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setStatusStripCoordinates( PointF coord, int rx, int ry)
        {
            this.tsslCoordsValue.Text = coord.X + ":" + coord.Y + "(" + rx + ":" + ry + ")";
        }

        public void setStatusStripSelectRectangles(RectangleF rect)
        {
            this.tsslSelectRectangle.Text = rect.Left + ":" + rect.Top + " - " + rect.Right + ":" + rect.Bottom + " (" + rect.Width + "x" + rect.Height + ")";
        }

        public void removeStatusStripSelectRectangles()
        {
            this.tsslSelectRectangle.Text = "";
        }

        private void tbCLI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.writeConsole(tbCLI.Text);
                tbCLI.Text = "";
            }
        }

        private void tsmiConsoleClear_Click(object sender, EventArgs e)
        {
            this.rtbConsole.Clear();
            this.messageNumber = 0;
        }

        private void saveAsConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Console log (*.txt)|*.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.rtbConsole.SaveFile(dialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CryptoHelper.getInstance().HasKey)
            {
                PetriNetwork network = this.getActivePetriNetwork();
                if (network != null)
                {
                    if (!network.isSaved())
                    {
                        this.saveAs(network);
                    }
                    else
                    {
                        Cursor actCursor = this.Cursor;
                        this.Cursor = Cursors.WaitCursor;
                        network.saveToXml();
                        this.ActiveMdiChild.Text = network.Title;
                        this.recentFilesHelper.addRecentFile(network.PetriRecentFile);
                        // this.child_networkItemSelected(network.SelectedItems, true);
                        this.writeConsole(network.Name + " network was saved as " + network.FileName + " .");
                        this.Cursor = actCursor;
                    }
                }
            }
        }

        private void saveAs(PetriNetwork network)
        {
            if (CryptoHelper.getInstance().HasKey)
            {
                if (network != null)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Petri Network XML file (*.pn.xml)|*.pn.xml";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Cursor actCursor = this.Cursor;
                        this.Cursor = Cursors.WaitCursor;
                        network.saveToXml(dialog.FileName);
                        this.ActiveMdiChild.Text = network.Title;
                        this.recentFilesHelper.addRecentFile(network.PetriRecentFile);
                        // this.child_networkItemSelected(network.SelectedItems, true);
                        this.writeConsole(network.Name + " network was saved as " + network.FileName + " .");
                        this.Cursor = actCursor;
                    }
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PetriNetwork network = this.getActivePetriNetwork();
            this.saveAs(network);
        }

        private void imagepngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is PetriNetworkForm)
            {
                if ((this.ActiveMdiChild as PetriNetworkForm).NetworkImage != null)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Image (*.png)|*.png";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        (this.ActiveMdiChild as PetriNetworkForm).NetworkImage.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        this.writeConsole("Petri network was exported as " + dialog.FileName + ".");
                    }
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.beforeCloseMdiChild();
                this.refreshToolWindowsPetriNetwork(null);
                this.ActiveMdiChild.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool restoreCheck = false;
            if (!this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
            {
                restoreCheck = true;
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = true;
            }
            AboutForm about = new AboutForm();
            about.ShowDialog();
            if (restoreCheck)
            {
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = false;
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool restoreCheck = false;
            if (!this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
            {
                restoreCheck = true;
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = true;
            }
            ApplicationOptions options = new ApplicationOptions();
            if (options.ShowDialog() == DialogResult.OK)
            {
                this.reDrawActivePetriNetwork(false);
                this.petriPalette_selectedItemChanged(this.SelectedToolboxItem);
            }
            if (restoreCheck)
            {
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = false;
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool restoreCheck = false;
            if (!this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
            {
                restoreCheck = true;
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = true;
            }

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.updateCulture();
            /*
            this.petriNetworkToolStripMenuItem.Enabled = false;
            if (CryptoHelper.getInstance().HasKey)
            {
                this.petriNetworkToolStripMenuItem.Enabled = true;
            }
            */
            if (this.numberOfMdiChildren > 0)
            {
                if (CryptoHelper.getInstance().HasKey)
                {
                    this.saveToolStripMenuItem.Enabled = true;
                    this.saveAsToolStripMenuItem.Enabled = true;
                }
            }

            if (restoreCheck)
            {
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = false;
            }	
        }

        private void trustStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool restoreCheck = false;
            if (!this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked)
            {
                restoreCheck = true;
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = true;
            }

            TrustStoreForm trustStoreForm = new TrustStoreForm();
            trustStoreForm.ShowDialog();

            if (restoreCheck)
            {
                this.toolWindowsAlwaysOnTopToolStripMenuItem.Checked = false;
            }
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.reDrawActivePetriNetwork(false);
        }

    }
}
