using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;

namespace PetriNetworkSimulator.Entities.Utils
{
    public partial class NetworkVisualSettings
    {
        private Font defaultFont;
        private Pen selectedItemPen;
        private Color selectedColor;
        private Color defaultColor;
        private Pen defaultPen;
        private Brush defaultBrush;
        private Brush selectedItemBrush;
        private Color markColor;
        private Pen markPen;
        private Brush markBrush;
        private Color helpColor;
        private Pen helpPen;
        private Pen defaultEdgePen;
        private Pen inhibitorArcPen;
        private Pen resetArcPen;
        private Pen selectedEdgePen;
        private Pen selectedInhibitorArcPen;
        private Pen selectedResetArcPen;

        private Color backgroundColor; // transient
        private Brush backgroundBrush;

        private Color shadowColor;
        private Brush shadowBrush;

        private Color notePenColor;
        private Color noteBorderColor;
        private Color noteBrushColor;

        private Pen noteBorderPen;
        private Pen noteLinePen;
        private Brush noteBrush;
        private Brush noteTextBrush;

        private Font noteFont;

        private Color stateColor; 
        private Pen statePen;
        private Pen stateEdgePen;
        private Brush stateBrush;

        private Color markAsReadyToFireColor;
        private Pen markAsReadyToFirePen;
        private Brush markAsReadyToFireBrush;

        private Color clockColor;
        private Pen clockPen; 
        private Brush clockBrush;
       
        private readonly CustomLineCap arrowCap;
        private readonly CustomLineCap stateArrowCap;
        private readonly CustomLineCap resetArrowCap;
        private readonly CustomLineCap roundCap;
        private float[] dashValues;

        public Color SelectedColor
        {
            get { return this.selectedColor; }
            set { 
                this.selectedColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.selectedItemPen = new Pen(this.selectedColor, 3);
                this.selectedItemBrush = new SolidBrush(this.selectedColor);
                this.selectedEdgePen = new Pen(this.selectedColor, 1);
                this.selectedInhibitorArcPen = new Pen(this.selectedColor, 1);
                this.selectedResetArcPen = new Pen(this.selectedColor, 2);
                this.selectedEdgePen.CustomEndCap = this.arrowCap;
                this.selectedInhibitorArcPen.CustomEndCap = this.roundCap;
                this.selectedResetArcPen.CustomEndCap = this.resetArrowCap;
                this.selectedResetArcPen.DashStyle = DashStyle.Dot;
            }
        }

        public Color DefaultColor
        {
            get { return this.defaultColor; }
            set { 
                this.defaultColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.defaultPen = new Pen(this.defaultColor);
                this.defaultBrush = new SolidBrush(this.defaultColor);
                
                this.noteLinePen = new Pen(this.defaultColor);
                this.noteLinePen.DashPattern = dashValues;
                this.noteLinePen.DashStyle = DashStyle.Custom;
                this.defaultEdgePen = new Pen(this.defaultColor);
                this.inhibitorArcPen = new Pen(this.defaultColor);
                this.resetArcPen = new Pen(this.defaultColor, 2);
                this.defaultEdgePen.CustomEndCap = this.arrowCap;
                this.inhibitorArcPen.CustomEndCap = this.roundCap;
                this.resetArcPen.CustomEndCap = this.resetArrowCap;
                this.resetArcPen.DashStyle = DashStyle.Dot;
            }
        }

        public Color MarkColor
        {
            get { return this.markColor; }
            set { 
                this.markColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.markPen = new Pen(this.markColor, 2);
                this.markBrush = new SolidBrush(this.markColor);
            }
        }

        public Color ShadowColor
        {
            get { return this.shadowColor; }
            set { 
                this.shadowColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.shadowBrush = new SolidBrush(this.shadowColor);
            }
        }

        public Color StateColor
        {
            get { return this.stateColor; }
            set
            {
                this.stateColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.statePen = new Pen(this.stateColor);
                this.stateBrush = new SolidBrush(this.stateColor);
                this.stateEdgePen = new Pen(this.stateColor, 2);
                this.stateEdgePen.CustomEndCap = this.stateArrowCap;
            }
        }

        public Color NotePenColor
        {
            get { return this.notePenColor; }
            set { 
                this.notePenColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.noteTextBrush = new SolidBrush(this.notePenColor);
            }
        }

        public Color NoteBorderColor
        {
            get { return this.noteBorderColor; }
            set { 
                this.noteBorderColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.noteBorderPen = new Pen(this.noteBorderColor);
            }
        }

        public Color NoteBrushColor
        {
            get { return this.noteBrushColor; }
            set { 
                this.noteBrushColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.noteBrush = new SolidBrush(this.noteBrushColor);
            }
        }

        public Color HelpColor
        {
            get { return this.helpColor; }
            set
            {
                this.helpColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.helpPen = new Pen(this.helpColor);
            }
        }

        public Color MarkAsReadyToFireColor
        {
            get { return this.markAsReadyToFireColor; }
            set
            {
                this.markAsReadyToFireColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.markAsReadyToFirePen = new Pen(this.markAsReadyToFireColor);
                this.markAsReadyToFireBrush = new SolidBrush(this.markAsReadyToFireColor);
            }
        }

        public Color ClockColor
        {
            get { return this.clockColor; }
            set
            {
                this.clockColor = Color.FromArgb(value.A, value.R, value.G, value.B);
                this.clockPen = new Pen(this.clockColor);
                this.clockBrush = new SolidBrush(this.clockColor);
            }
        }

        public Font DefaultFont
        {
            get { return this.defaultFont; }
            set { this.defaultFont = value; }
        }

        public Font NoteFont
        {
            get { return this.noteFont; }
            set { this.noteFont = value; }
        }

        public Pen SelectedItemPen
        {
            get { return this.selectedItemPen; }
        }

        public Pen SelectedEdgePen
        {
            get { return this.selectedEdgePen; }
        }

        public Pen DefaultPen
        {
            get { return this.defaultPen; }
        }

        public Pen DefaultEdgePen
        {
            get { return this.defaultEdgePen; }
        }

        public Pen MarkPen
        {
            get { return this.markPen; }
        }

        public Pen NoteBorderPen
        {
            get { return this.noteBorderPen; }
        }

        public Pen NoteLinePen
        {
            get { return this.noteLinePen; }
        }

        public Pen HelpPen
        {
            get { return this.helpPen; }
        }

        public Pen StatePen
        {
            get { return this.statePen; }
        }

        public Pen StateEdgePen
        {
            get { return this.stateEdgePen; }
        }

        public Pen MarkAsReadyToFirePen
        {
            get { return this.markAsReadyToFirePen; }
        }

        public Pen ClockPen
        {
            get { return this.clockPen; }
        }

        public Pen InhibitorArcPen
        {
            get { return this.inhibitorArcPen; }
        }

        public Pen ResetArcPen
        {
            get { return this.resetArcPen; }
        }

        public Pen SelectedResetArcPen
        {
            get { return this.selectedResetArcPen; }
        }

        public Pen SelectedInhibitorArcPen
        {
            get { return this.selectedInhibitorArcPen; }
        }

        public Brush DefaultBrush
        {
            get { return this.defaultBrush; }
        }

        public Brush SelectedItemBrush
        {
            get { return this.selectedItemBrush; }
        }

        public Brush BackgroundBrush
        {
            get { return this.backgroundBrush; }
        }

        public Brush MarkBrush
        {
            get { return this.markBrush; }
        }

        public Brush ShadowBrush
        {
            get { return this.shadowBrush; }
        }

        public Brush NoteBrush
        {
            get { return this.noteBrush; }
        }

        public Brush NoteTextBrush
        {
            get { return this.noteTextBrush; }
        }

        public Brush StateBrush
        {
            get { return this.stateBrush; }
        }

        public Brush MarkAsReadyToFireBrush
        {
            get { return this.markAsReadyToFireBrush; }
        }

        public Brush ClockBrush
        {
            get { return this.clockBrush; }
        }

        public NetworkVisualSettings()
        {
            // Fix settings
            GraphicsPath arrowPath = new GraphicsPath();
            arrowPath.AddLine(new PointF(-4, -6), new PointF(0, 0));
            arrowPath.AddLine(new PointF(0, 0), new PointF(4, -6));
            this.stateArrowCap = new CustomLineCap(null, arrowPath);

            GraphicsPath arrowPath2 = new GraphicsPath();
            arrowPath2.AddLine(new PointF(-8, -12), new PointF(0, 0));
            arrowPath2.AddLine(new PointF(0, 0), new PointF(8, -12));
            this.resetArrowCap = new CustomLineCap(null, arrowPath2);
            this.resetArrowCap.WidthScale = 0.4F;
            
            this.arrowCap = new AdjustableArrowCap(7, 7, true);
            this.arrowCap.SetStrokeCaps(LineCap.Round, LineCap.Round);
            this.arrowCap.WidthScale = 0.4F;

            GraphicsPath roundPath = new GraphicsPath();
            roundPath.AddEllipse(-4F, -8F, 8F, 8F);
            this.roundCap = new CustomLineCap(roundPath, null);
            this.roundCap.WidthScale = 0.4F;
            this.roundCap.SetStrokeCaps(LineCap.Round, LineCap.Round);
            
            this.dashValues = new float[] { 1F, 5F };

            this.backgroundColor = Color.FromArgb(255, 255, 255);
            this.backgroundBrush = new SolidBrush(this.backgroundColor);

            this.DefaultColor = Color.FromArgb(0, 0, 0);
            this.StateColor = Color.FromArgb(0, 0, 0);
            this.ShadowColor = Color.FromArgb(0, 0, 0);
            this.SelectedColor = Color.FromArgb(50, 50, 250);
            this.MarkColor = Color.FromArgb(30, 200, 30);
            this.NotePenColor = Color.FromArgb(0, 0, 0);
            this.NoteBorderColor = Color.FromArgb(205, 219, 36);
            this.NoteBrushColor = Color.FromArgb(255, 255, 128);
            this.HelpColor = Color.FromArgb(200, 200, 200);
            this.MarkAsReadyToFireColor = Color.FromArgb(250, 50, 50);
            this.ClockColor = Color.FromArgb(0, 0, 200);

            FontStyle fontStlye = FontStyle.Bold | FontStyle.Italic;
            this.defaultFont = new Font("Arial", 10, fontStlye);
            this.noteFont = new Font("Arial", 10);
        }

    }
}
