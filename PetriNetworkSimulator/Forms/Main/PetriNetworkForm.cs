using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.Item.Note;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using System.Xml.Schema;
using PetriNetworkSimulator.Entities.Common.Item.Position;
using PetriNetworkSimulator.Forms.Dialogs;

namespace PetriNetworkSimulator.Forms.Main
{
    public delegate void NetworkItemHandler(List<AbstractItem> items, bool forceRefresh);
    public delegate void ChildFormHandler(string name);
    public delegate void SimulationNotifierHandler(PetriNetwork network, FireEvent fireEvent);

    public partial class PetriNetworkForm : Form
    {
        private static int WHEEL_DELTA_SENSITIVITY = 50;
        private static int FORM_WIDTH = 25;
        private static int FORM_HEIGHT = 84; // 62
        
        private PetriNetwork petriNetwork;
        
        private int zoomValue;
        private double stretchX;
        private double stretchY;

        private bool isStartEdgeAvailable;
        private bool isStartSelectRectangle;
        private bool isStartMove;
        private AbstractNetworkItem startEdge;
        private AbstractNetworkItem endEdge;
        private AbstractItem moveItem;
        private MoveCorner moveCorner;
        private Point startCoordinates;
        private Point endCoordinates;
        private PointF startCoordinatesF;
        private PointF endCoordinatesF;
        private Rectangle theRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
        
        public event NetworkItemHandler networkItemSelected;
        public event ChildFormHandler childFormClosed;
        public event SimulationNotifierHandler simulationNotifier;

        private Pen gridPen;

        public PetriNetwork Network
        {
            get { return this.petriNetwork; }
        }

        public Image NetworkImage
        {
            get { return this.pbPetriNetwork.Image; }
        }

        public PetriNetworkForm(PetriNetwork petriNetwork)
        {
            this.DoubleBuffered = true;
            InitializeComponent();

            this.petriNetwork = petriNetwork;
            this.petriNetwork.dimensionChanged += new DimensionHandler(petriNetwork_dimensionChanged);
            this.petriNetwork.eventHandler += new PetriNetworkEventHandler(sendEventMessageToConsole);
            this.petriNetwork.eventNotifier += new PetriNetworkNotifier(petriNetwork_eventNotifier);

            this.petriNetwork_dimensionChanged(petriNetwork.Width, petriNetwork.Height);
            this.pbPetriNetwork.SizeMode = PictureBoxSizeMode.StretchImage;

            this.zoomValue = 100;

            this.MouseWheel += new MouseEventHandler(pbPetriNetwork_MouseWheel);

            this.ClearPetriNetwork();
            this.calculateStrech();

            this.modifyWindowText();

            this.isStartEdgeAvailable = false;
            this.isStartSelectRectangle = false;
            this.isStartMove = false;
            this.startEdge = null;
            this.endEdge = null;
            this.moveItem = null;

            this.Text = this.petriNetwork.Title;
            this.rtbDescription.Text = this.petriNetwork.Description;

            this.gridPen = new Pen(Color.FromArgb(220, 220, 220));
            this.tsslInfo.Text = "";
        }

        private void petriNetwork_eventNotifier(NetworkNotifierAction action)
        {
            switch (action)
            {
                case NetworkNotifierAction.SIMULATIONMUSTSTOPED:
                    this.stopAutoFire();
                    break;
            }
        }

        private void sendEventMessageToConsole(string message)
        {
            (this.MdiParent as MDIParent).writeConsole(message);
        }

        private void petriNetwork_dimensionChanged(int width, int height)
        {
            this.pbPetriNetwork.Image = new Bitmap(petriNetwork.Width, this.petriNetwork.Height);
            this.pbPetriNetwork.Width = this.petriNetwork.Width;
            this.pbPetriNetwork.Height = this.petriNetwork.Height;
            this.setWindowSize(this.petriNetwork.Width + PetriNetworkForm.FORM_WIDTH, this.petriNetwork.Height + PetriNetworkForm.FORM_HEIGHT);
        }

        private void setWindowSize(int width, int height)
        {
            this.ClientSize = new Size(width, height);
            this.MaximumSize = new Size(this.pbPetriNetwork.Width + PetriNetworkForm.FORM_WIDTH, this.pbPetriNetwork.Height + PetriNetworkForm.FORM_HEIGHT);
        }

        private void modifyWindowText(){
            this.Text = this.petriNetwork.Name + " " + this.zoomValue + "%";
        }

        private void pbPetriNetwork_MouseWheel(object sender, MouseEventArgs e)
        {
            int zoomValueTmp = this.zoomValue + (int)((float)e.Delta / PetriNetworkForm.WHEEL_DELTA_SENSITIVITY);
            if (zoomValueTmp > 30)
            {
                this.zoomValue = zoomValueTmp;
                float zoomValueF = (float)this.zoomValue / 100;

                this.pbPetriNetwork.Width = (int)(petriNetwork.Width * zoomValueF);
                this.pbPetriNetwork.Height = (int)(petriNetwork.Height * zoomValueF);

                this.calculateStrech();

                if (this.zoomValue < 100)
                {
                    this.setWindowSize(this.pbPetriNetwork.Width + PetriNetworkForm.FORM_WIDTH, this.pbPetriNetwork.Height + PetriNetworkForm.FORM_HEIGHT);
                }
                else
                {
                    this.setWindowSize(petriNetwork.Width + PetriNetworkForm.FORM_WIDTH, petriNetwork.Height + PetriNetworkForm.FORM_HEIGHT);
                }

                this.modifyWindowText();
            }
        }

        private void calculateStrech()
        {
            this.stretchX = (double)((double)petriNetwork.Width / this.pbPetriNetwork.Size.Width);
            this.stretchY = (double)((double)petriNetwork.Height / this.pbPetriNetwork.Size.Height);
        }

        public void ClearPetriNetwork()
        {
            Graphics g = Graphics.FromImage(this.pbPetriNetwork.Image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            this.pbPetriNetwork.Refresh();
        }

        private SizeF convertPixelToSize(int width, int height)
        {
            return new SizeF((int)(width * this.stretchX), (int)(height * this.stretchY));
        }

        private SizeF convertPixelToSize(Size size)
        {
            return this.convertPixelToSize(size.Width, size.Height);
        }

        private PointF convertPixelToCoord(int x, int y)
        {
            return new PointF((int)(x * this.stretchX), (int)(y * this.stretchY));
        }

        private PointF convertPixelToCoord(Point point)
        {
            return this.convertPixelToCoord(point.X, point.Y);
        }

        public void parentCallReDraw(NetworkToolboxAction selectedAction, bool minimapRefresh = true)
        {
            bool reDraw = false;
            switch (selectedAction)
            {
                case NetworkToolboxAction.REFRESH:
                    reDraw = true;
                    break;
                case NetworkToolboxAction.CLEAR:
                    this.petriNetwork.clearPetriNetwork();
                    reDraw = true;
                    break;
                case NetworkToolboxAction.SELECTALL:
                    this.petriNetwork.selectAllItem();
                    reDraw = true;
                    break;
                case NetworkToolboxAction.CLEARSELECTION:
                    this.petriNetwork.unselectAllItem();
                    reDraw = true;
                    break;
                case NetworkToolboxAction.REVERSESELECTION:
                    this.petriNetwork.reverseSelection();
                    reDraw = true;
                    break;
                case NetworkToolboxAction.DELETESELECTED:
                    this.petriNetwork.deleteSelectedItems();
                    reDraw = true;
                    break;
            }
            if (reDraw) {
                this.drawPetriNetwork(null, null, minimapRefresh); 
            }
            if (this.networkItemSelected != null)
            {
                this.networkItemSelected(this.petriNetwork.SelectedItems, false);
            }
        }

        private void selectVisualItem(bool ret)
        {
            if ((ret) && (this.networkItemSelected != null))
            {
                this.networkItemSelected(this.petriNetwork.SelectedItems, false);
            }
        }

        private bool selectVisualItem(PointF coordinates)
        {
            bool ret = this.petriNetwork.selectItem(coordinates);
            this.selectVisualItem(ret);
            return ret;
        }

        private bool selectVisualItem(RectangleF rect)
        {
            bool ret = this.petriNetwork.selectItem(rect);
            this.selectVisualItem(ret);
            return ret;
        }

        public void singleSelectItem(AbstractItem item)
        {
            if (item != null)
            {
                bool tmp = this.petriNetwork.isSelected(item);
                this.petriNetwork.unselectAllItem();
                if (!tmp)
                {
                    this.petriNetwork.reverseSelection(item);
                }
                if (this.networkItemSelected != null)
                {
                    this.networkItemSelected(this.petriNetwork.SelectedItems, false);
                }
            }
        }

        private void pbPetriNetwork_MouseClick(object sender, MouseEventArgs e)
        {
            PointF noAlignCoordinates = this.convertPixelToCoord(e.X, e.Y);
            PointF coordinates = this.alignToGrid(noAlignCoordinates);
            AbstractItem item = null;
            AbstractNetworkItem edgeSelectionItem = null;
            switch ((this.MdiParent as MDIParent).SelectedToolboxItem)
            {
                case NetworkToolboxItem.POSITION:
                    if (!this.selectVisualItem(coordinates))
                    {
                        this.petriNetwork.addPosition(null, coordinates, (float)20);
                    }
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.TRANSITION:
                    if (!this.selectVisualItem(coordinates))
                    {
                        this.petriNetwork.addTransition(null, new Random().Next(0, 90), coordinates, new SizeF(5, 20));
                    }
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.TOKEN:
                    item = this.petriNetwork.getVisualItemByCoordinates(noAlignCoordinates);
                    if ((item != null) && ( item is Position ))
                    {
                        this.petriNetwork.addToken((Position)item, null);
                    }
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.NOTE:
                    if (!this.selectVisualItem(coordinates))
                    {
                        this.petriNetwork.addNote(null, coordinates, new SizeF(100, 40), null);
                    }
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.DELETETOKEN:
                    item = this.petriNetwork.getVisualItemByCoordinates(noAlignCoordinates);
                    if ((item != null) && (item is Position))
                    {
                        this.petriNetwork.deleteToken((Position)item);
                    }
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.SELECT:
                    this.selectVisualItem(noAlignCoordinates);
                    this.isStartEdgeAvailable = false;
                    // this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.SINGLESELECT:
                    item = this.petriNetwork.getVisualItemByCoordinates(noAlignCoordinates);
                    this.singleSelectItem(item);
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = false;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.SELECTEDGE:
                    item = this.petriNetwork.getVisualItemByCoordinates(noAlignCoordinates);
                    if (item is AbstractNetworkItem)
                    {
                        edgeSelectionItem = (AbstractNetworkItem)item;
                        this.isStartEdgeAvailable = false;
                        this.isStartSelectRectangle = false;
                        this.isStartMove = false;
                    }
                    break;
                case NetworkToolboxItem.DELETE:
                    item = this.petriNetwork.getVisualItemByCoordinates(noAlignCoordinates);
                    if (item != null)
                    {
                        this.petriNetwork.deleteItemAndConnectedEdges(item);
                    }
                    break;
            }
            this.drawPetriNetwork(null, edgeSelectionItem);
        }

        private void pbPetriNetwork_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NetworkToolboxItem ntItem = (this.MdiParent as MDIParent).SelectedToolboxItem;
            if (!NetworkToolboxItem.TOKEN.Equals(ntItem) && !NetworkToolboxItem.DELETETOKEN.Equals(ntItem))
            {
                PointF noAlignCoordinates = this.convertPixelToCoord(e.X, e.Y);
                AbstractItem item = this.petriNetwork.getVisualItemByCoordinates(noAlignCoordinates);
                if (item != null)
                {
                    ChangeTextValueForm questionForm = new ChangeTextValueForm();
                    questionForm.Text = item.ToString();
                    if (item is AbstractNote)
                    {
                        AbstractNote itemNote = (AbstractNote)item;
                        questionForm.QuestionLabel = "Name: ";
                        questionForm.AnswerValue = itemNote.Text;
                        if (questionForm.ShowDialog() == DialogResult.OK)
                        {
                            itemNote.Text = questionForm.AnswerValue;
                            this.parentCallReDraw(NetworkToolboxAction.REFRESH, true);
                        }
                    }
                    else
                    {
                        questionForm.QuestionLabel = "Name: ";
                        questionForm.AnswerValue = item.Name;
                        if (questionForm.ShowDialog() == DialogResult.OK)
                        {
                            item.Name = questionForm.AnswerValue;
                            this.parentCallReDraw(NetworkToolboxAction.REFRESH, true);
                        }
                    }
                }
            }
        }

        private void drawGrid(Graphics g, Pen gridPen, float gridSize)
        { 
            if (((MDIParent)this.MdiParent).ShowGrid)
            {
                for (float i = 0; i < this.petriNetwork.Width; i += gridSize)
                {
                    g.DrawLine(gridPen, i, 0, i, this.petriNetwork.Height);
                }
                for (float i = 0; i < this.petriNetwork.Height; i += gridSize)
                {
                    g.DrawLine(gridPen, 0, i, this.petriNetwork.Width, i);
                }
            }
        }

        private void drawPetriNetwork(AbstractNetworkItem edgeStartItem, AbstractNetworkItem edgeSelectionItem, bool toolWindowsRefresh = true)
        {
            Graphics g = Graphics.FromImage(this.pbPetriNetwork.Image);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            this.drawGrid(g, this.gridPen, Properties.Settings.Default.GridSize);
            if (this.petriNetwork.isConnectedItem(edgeSelectionItem))
            {
                this.petriNetwork.selectEdge(edgeSelectionItem);
                if (this.networkItemSelected != null)
                {
                    this.networkItemSelected(this.petriNetwork.SelectedItems, false);
                }
            }
            else
            {
                this.petriNetwork.setStartNetworkItemForEdgeSelection(edgeSelectionItem);
            }
            this.petriNetwork.draw(g, edgeStartItem, NetworkToolboxItem.MOVE.Equals((this.MdiParent as MDIParent).SelectedToolboxItem));
            this.pbPetriNetwork.Refresh();
            if (toolWindowsRefresh)
            {
                (this.MdiParent as MDIParent).refreshToolWindowsPetriNetwork(this.petriNetwork);
            }
            else
            {
                (this.MdiParent as MDIParent).refreshMiniMap(this.petriNetwork);
            }
        }

        private void pbPetriNetwork_MouseDown(object sender, MouseEventArgs e)
        {
            AbstractItem item = null;
            this.startCoordinatesF = this.convertPixelToCoord(e.X, e.Y);
            this.startCoordinates = this.pbPetriNetwork.PointToScreen(new Point(e.X, e.Y));
            switch ((this.MdiParent as MDIParent).SelectedToolboxItem)
            {
                case NetworkToolboxItem.EDGE:
                    item = this.petriNetwork.getVisualItemByCoordinates(this.startCoordinatesF);
                    if ((item != null) && ( item is AbstractNetworkItem ))
                    {
                        bool valid = true;
                        if (item is AbstractTransition)
                        {
                            if (TransitionType.SINK.Equals(((AbstractTransition)item).TransitionType))
                            {
                                valid = false;
                            }
                        }
                        if (valid)
                        {
                            AbstractNetworkItem networkItem = (AbstractNetworkItem)item;
                            this.startEdge = networkItem;
                            this.endCoordinates = this.startCoordinates;
                            this.isStartEdgeAvailable = true;
                            this.isStartSelectRectangle = false;
                            this.isStartMove = false;
                            if (!(networkItem is AbstractNote))
                            {
                                this.drawPetriNetwork(networkItem, null);
                            }
                        }
                    }
                    break;
                case NetworkToolboxItem.SELECT:
                    this.isStartEdgeAvailable = false;
                    this.isStartSelectRectangle = true;
                    this.isStartMove = false;
                    break;
                case NetworkToolboxItem.MOVE:
                    // search sizeable corner
                    SearchItemResultTransfer searchItemResultTransfer = this.petriNetwork.getVisualItemEdgesByCoordinates(this.startCoordinatesF);
                    if (searchItemResultTransfer != null)
                    {
                        item = searchItemResultTransfer.Item;
                        this.moveCorner = searchItemResultTransfer.MoveEdge;
                    }
                    else
                    {
                        item = this.petriNetwork.getVisualItemByCoordinates(this.startCoordinatesF);
                        this.moveCorner = MoveCorner.NONE;
                    }
                    if ((item != null) && (item is AbstractItem))
                    {
                        AbstractItem networkItem = (AbstractItem)item;
                        this.moveItem = networkItem;
                        this.endCoordinates = this.startCoordinates;
                        this.isStartEdgeAvailable = false;
                        this.isStartSelectRectangle = false;
                        this.isStartMove = true;
                    }
                    break;
            }
        }

        private void showChildInfo(PointF moveCoordinatesF)
        {
            string statusInfo = null;
            SearchItemResultTransfer searchItemResultTransfer = this.petriNetwork.getVisualItemEdgesByCoordinates(moveCoordinatesF);
            if (searchItemResultTransfer != null)
            {
                statusInfo = searchItemResultTransfer.ToString();
            } else {
                AbstractItem item = this.petriNetwork.getVisualItemByCoordinates(moveCoordinatesF);
                if (item != null)
                {
                     statusInfo = item.ToString();
                }
            }
            if (statusInfo != null)
            {
                this.tsslInfo.Text = statusInfo;
            }
            else
            {
                this.tsslInfo.Text = "";
            }
        }

        private void pbPetriNetwork_MouseMove(object sender, MouseEventArgs e)
        {
            this.showChildInfo(this.convertPixelToCoord(e.X, e.Y));

            (this.MdiParent as MDIParent).setStatusStripCoordinates(this.convertPixelToCoord(e.X, e.Y), e.X, e.Y);
            if ((this.isStartEdgeAvailable) || (this.isStartMove))
            {
                ControlPaint.DrawReversibleLine(this.startCoordinates, this.endCoordinates, this.BackColor);
                this.endCoordinates = this.pbPetriNetwork.PointToScreen(new Point(e.X, e.Y));
                ControlPaint.DrawReversibleLine(this.startCoordinates, this.endCoordinates, this.BackColor);
            } else if (this.isStartSelectRectangle)
            {
                ControlPaint.DrawReversibleFrame(this.theRectangle, this.BackColor, FrameStyle.Dashed);
                Point endPoint = this.pbPetriNetwork.PointToScreen(new Point(e.X, e.Y));
                int width = endPoint.X - this.startCoordinates.X;
                int height = endPoint.Y - this.startCoordinates.Y;
                this.theRectangle = new Rectangle(this.startCoordinates.X, this.startCoordinates.Y, width, height);
                ControlPaint.DrawReversibleFrame(this.theRectangle, this.BackColor, FrameStyle.Dashed);
                (this.MdiParent as MDIParent).setStatusStripSelectRectangles(this.convertSelectRectangle(this.theRectangle));
            }
        }

        private RectangleF convertSelectRectangle(Rectangle rect)
        {
            PointF pf = this.convertPixelToCoord(this.pbPetriNetwork.PointToClient(rect.Location));
            SizeF sf = this.convertPixelToSize(rect.Size);
            return new RectangleF(pf, sf);
        }

        private PointF alignToGrid(PointF coordinates)
        {
            PointF ret = coordinates;
            bool alignToGrid = Properties.Settings.Default.AlignToGrid;
            if (this.MdiParent is MDIParent)
            {
                alignToGrid = ((MDIParent)this.MdiParent).AlignToGrid;
            }
            if (alignToGrid)
            {
                float gridSize = Properties.Settings.Default.GridSize;
                float x = (float)(Math.Round((double)(ret.X / gridSize), 0) * gridSize);
                float y = (float)(Math.Round((double)(ret.Y / gridSize), 0) * gridSize);
                ret = new PointF(x, y);
            }
            return ret;
        }

        private void pbPetriNetwork_MouseUp(object sender, MouseEventArgs e)
        {
            PointF noAlignEndCoordinates = this.convertPixelToCoord(e.X, e.Y);
            this.endCoordinatesF = this.alignToGrid(noAlignEndCoordinates);
            bool find = false;
            switch ((this.MdiParent as MDIParent).SelectedToolboxItem)
            {
                case NetworkToolboxItem.EDGE:
                    if (isStartEdgeAvailable) {
                        AbstractItem edgeItem = this.petriNetwork.getVisualItemByCoordinates(this.endCoordinatesF);
                        if ((edgeItem != null) && ( edgeItem is AbstractNetworkItem ))
                        {
                            AbstractNetworkItem networkItem = (AbstractNetworkItem)edgeItem;
                            this.endEdge = networkItem;
                            if (!(this.startEdge is AbstractNote))
                            {
                                this.petriNetwork.addEdge(this.startEdge, this.endEdge, new PointF(0,0));
                            }
                            else
                            {
                                AbstractNote note = (AbstractNote)this.startEdge;
                                note.AttachedItem = this.endEdge;
                            }
                            find = true;
                        }
                        else if ((edgeItem != null) && (edgeItem is AbstractEdge))
                        {
                            if (this.startEdge is AbstractNote) {
                                AbstractNote note = (AbstractNote)this.startEdge;
                                note.AttachedItem = edgeItem;
                                find = true;
                            }
                        }
                        else if (edgeItem == null)
                        {
                            // auto add position/transition
                            if (this.startEdge is AbstractPosition)
                            {
                                if (!this.selectVisualItem(endCoordinatesF))
                                {
                                    this.endEdge = this.petriNetwork.addTransition(null, new Random().Next(0, 90), endCoordinatesF, new SizeF(5, 20));
                                    this.petriNetwork.addEdge(this.startEdge, this.endEdge, new PointF(0, 0));
                                    find = true;
                                }	
                            }
                            else if (this.startEdge is AbstractTransition)
                            {
                                if (!this.selectVisualItem(endCoordinatesF))
                                {
                                    this.endEdge = this.petriNetwork.addPosition(null, endCoordinatesF, (float)20);
                                    this.petriNetwork.addEdge(this.startEdge, this.endEdge, new PointF(0, 0));
                                    find = true;
                                }
                            }
                        }
                    }
                    break;
                case NetworkToolboxItem.SELECT:
                    if ( (isStartSelectRectangle) && ( this.theRectangle.Width != 0 ) && ( this.theRectangle.Height != 0 ) )
                    {
                        (this.MdiParent as MDIParent).removeStatusStripSelectRectangles();
                        ControlPaint.DrawReversibleFrame(this.theRectangle,this.BackColor, FrameStyle.Dashed);
                        this.petriNetwork.unselectAllItem();
                        find = this.selectVisualItem(this.convertSelectRectangle(this.theRectangle));
                        ControlPaint.DrawReversibleFrame(this.theRectangle, this.BackColor, FrameStyle.Dashed);
                        this.theRectangle = new Rectangle(0, 0, 0, 0);
                    }
                    break;
                case NetworkToolboxItem.MOVE:
                    if (this.isStartMove)
                    {
                        if (this.moveItem is AbstractNetworkItem)
                        {
                            if (MoveCorner.NONE.Equals(this.moveCorner))
                            {
                                if (this.petriNetwork.isSelected(this.moveItem))
                                {
                                    PointF offset = this.alignToGrid(new PointF(this.endCoordinatesF.X - this.startCoordinatesF.X, this.endCoordinatesF.Y - this.startCoordinatesF.Y));
                                    this.petriNetwork.modifySelectedItems(NetworkProperty.ORIGO, offset);
                                }
                                else
                                {
                                    AbstractNetworkItem networkItem = (AbstractNetworkItem)this.moveItem;
                                    networkItem.Origo = this.endCoordinatesF;
                                }
                            }
                            else
                            {
                                // resize
                                PointF offset = new PointF(noAlignEndCoordinates.X - this.startCoordinatesF.X, noAlignEndCoordinates.Y - this.startCoordinatesF.Y);
                                if (this.moveCorner.Equals(MoveCorner.LABEL) && this.moveItem is AbstractNetworkItem)
                                {
                                    AbstractNetworkItem ani = (AbstractNetworkItem)this.moveItem;
                                    ani.LabelOffset = new PointF(ani.LabelOffset.X + offset.X, ani.LabelOffset.Y + offset.Y);
                                }
                                else
                                {
                                    if (this.moveItem is AbstractNote)
                                    {
                                        ((AbstractNote)this.moveItem).setNoteParametersForResize(this.moveCorner, offset);
                                    }
                                    else if (this.moveItem is AbstractPosition)
                                    {
                                        ((AbstractPosition)this.moveItem).setPositionParametersForResize(this.moveCorner, offset);
                                    }
                                    else if (this.moveItem is AbstractTransition)
                                    {
                                        ((AbstractTransition)this.moveItem).setTransitionParametersForResize(this.moveCorner, offset);
                                    }
                                }
                            }
                        }
                        else if (this.moveItem is AbstractEdge)
                        {
                            AbstractEdge networkEdge = (AbstractEdge)this.moveItem;
                            networkEdge.setCurveMiddlePointOffset(this.endCoordinatesF);
                            // networkEdge.CurveMiddlePointOffset = offset;
                        }
                        find = true;
                    }
                    break;
            }
            this.isStartEdgeAvailable = false;
            this.isStartSelectRectangle = false;
            this.isStartMove = false;
            this.startEdge = null;
            this.endEdge = null;
            this.moveItem = null;
            if (find)
            {
                this.drawPetriNetwork(null,null);
            }
        }

        private void coloringPetriSourceByText( string label, Color color )
        {
            int i = 0;
            int namespaceIndex = label.IndexOf(":") + 1;
            int length = label.Length;
            int pos = this.rtbSource.Text.IndexOf(label, i);
            while (pos > 0)
            {
                this.rtbSource.SelectionStart = pos + namespaceIndex;
                this.rtbSource.SelectionLength = length - namespaceIndex;
                this.rtbSource.SelectionColor = color;
                // this.rtbSource.SelectionFont = new System.Drawing.Font("Courier New", 8.25F, FontStyle.Bold); 
                i = pos + length;
                pos = this.rtbSource.Text.IndexOf(label, i);
            }
        }

        private void coloringPetriSource()
        {
            this.rtbSource.Visible = false;
            int i = 0;
            int startIndexQuotationMarks = 0;
            while (i < this.rtbSource.Text.Length)
            {
                if ('"'.Equals(this.rtbSource.Text[i]))
                {
                    if (startIndexQuotationMarks != 0)
                    {
                        this.rtbSource.SelectionStart = startIndexQuotationMarks;
                        this.rtbSource.SelectionLength = (i - startIndexQuotationMarks) + 1;
                        this.rtbSource.SelectionColor = Color.Purple;
                        startIndexQuotationMarks = 0;
                    }
                    else
                    {
                        startIndexQuotationMarks = i;
                    }
                }
                i++;
            }
            this.coloringPetriSourceByText("pos:Position", Color.Blue);
            this.coloringPetriSourceByText("tra:Transition", Color.Green);
            this.coloringPetriSourceByText("edg:Edge", Color.Red);
            this.coloringPetriSourceByText("tok:Token", Color.Salmon);
            this.coloringPetriSourceByText("sv:StateVector", Color.Olive);
            this.coloringPetriSourceByText("se:StateEdge", Color.Orange);
            this.rtbSource.Visible = true;
        }

        private void tcPetriNetwork_Selected(object sender, TabControlEventArgs e)
        {
            if ( this.tcPetriNetwork.SelectedTab != null ) {
                if ( this.tpSource.Equals(this.tcPetriNetwork.SelectedTab) ){
                    /*
                    XmlDocument doc = this.petriNetwork.getXmlDocument();
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    doc.WriteTo(tx);
                    this.rtbSource.Text = sw.ToString();
                    */
                    this.petriNetwork.saveToXml(this.petriNetwork.TmpFileName, false);
                    this.rtbSource.LoadFile(this.petriNetwork.TmpFileName, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            this.petriNetwork.Description = this.rtbDescription.Text;   
        }

        private void bColoring_Click(object sender, EventArgs e)
        {
            this.coloringPetriSource();
        }

        public void autoFire()
        {
            if (!this.petriNetwork.ActiveSimulation)
            {
                if (!autoPlayWorker.IsBusy)
                {
                    this.petriNetwork.ActiveSimulation = true;
                    this.autoPlayWorker.RunWorkerAsync();
                }
                else
                {
                    this.petriNetwork.ActiveSimulation = true;
                }
            }
        }

        private void autoPlayWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int cycle = 0;
            FireReturn fireReturn = null;
            do
            {
                fireReturn = this.petriNetwork.fire(true);
                if (this.autoPlayWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                cycle++;
                if (( fireReturn.FireTransition != null ) && (fireReturn.FireTransition.Delay != 0)){
                    Thread.Sleep(fireReturn.FireTransition.Delay);
                }
                this.autoPlayWorker.ReportProgress(cycle);
                Thread.Sleep(this.petriNetwork.SimulationTimeout);
            } while (!FireEvent.DEADLOCK.Equals(fireReturn.FireEvent));
            e.Result = 0;
        }

        private void autoPlayWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.drawPetriNetwork(null, null, false); 
        }

        private void autoPlayWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.petriNetwork.ActiveSimulation = false;
            if (e.Cancelled)
            {
                MessageBox.Show("Simulation cancelled.","Info",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Result != null)
            {
                MessageBox.Show("Simulation stopped because of deadlock.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (this.simulationNotifier != null)
                {
                    this.simulationNotifier(this.petriNetwork, FireEvent.DEADLOCK);
                }
            }
            else
            {
                MessageBox.Show("No result.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void stopAutoFire()
        {
            if (autoPlayWorker.IsBusy)
            {
                autoPlayWorker.CancelAsync();
                this.petriNetwork.ActiveSimulation = false;
            }
        }

        private void PetriNetworkForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //
        }

        private void PetriNetworkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.stopAutoFire();
            DialogResult dialogResult = MessageBox.Show("Do you want to close the network?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult.OK.Equals(dialogResult))
            {
                if (this.childFormClosed != null)
                {
                    this.childFormClosed(this.petriNetwork.Name);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

    }
}
