using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using PetriNetworkSimulator.Entities.Common.Item.Position;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Utils;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.TokenPlayer;
using PetriNetworkSimulator.Entities.Common.Base;

namespace PetriNetworkSimulator.Entities.Item.NetPosition
{
    public partial class Position : AbstractPosition, IPetriItem
    {

        private List<AbstractToken> tokens;

        private int capacityLimit;

        public string TypeStr
        {
            get { return "Position"; }
        }

        public List<AbstractToken> Tokens
        {
            get { return this.tokens; }
        }

        public int CapacityLimit
        {
            get { return this.capacityLimit; }
            set { this.capacityLimit = value; }
        }

        public Position(string name, long unid, bool showAnnotation, PointF origo, float radius, int capacityLimit)
            : base(name, unid, showAnnotation, origo, radius)
        {
            this.tokens = new List<AbstractToken>();
            this.capacityLimit = capacityLimit;
        }

        public void addToken(AbstractToken item)
        {
            if (!this.tokens.Contains(item))
            {
                this.tokens.Add(item);
            }
        }

        public void addToken(string name, long unid)
        {
            this.addToken(new Token(name, unid, true));
        }

        public void deleteToken()
        {
            if (this.tokens.Count > 0)
            {
                this.tokens.RemoveAt(0);
            }
        }

        public void deleteToken(AbstractToken token)
        {
            if (this.tokens.Contains(token))
            {
                this.tokens.Remove(token);
            }
        }

        public AbstractToken takeAwayToken()
        {
            AbstractToken token = null;
            int count = this.tokens.Count;
            if (count > 0)
            {
                token = this.tokens[0];
                this.deleteToken(token);
            }
            return token;
        }

        public override void draw(Graphics g, bool selected, bool mark, NetworkVisualSettings visualSettings, NetworkVisibleSettings visibleSettings, bool markAsReadyToFire, bool showHelpEllipse)
        {
            base.draw(g, selected, mark, visualSettings, visibleSettings, markAsReadyToFire, showHelpEllipse);
            g.FillEllipse(visualSettings.ShadowBrush, this.ShadowRectangle);
            g.FillEllipse(visualSettings.BackgroundBrush, this.Rectangle);
            
            Pen pen = (mark ? visualSettings.MarkPen : (selected ? visualSettings.SelectedItemPen : visualSettings.DefaultPen));
            g.DrawEllipse(pen, this.Rectangle);
            

            int tokenCount = this.tokens.Count;
            PointF tp = this.Origo;
            switch (tokenCount)
            {
                case 0:

                    break;
                case 1:
                    this.tokens[0].draw(g, this.Origo, visualSettings);
                    break;
                case 2:
                    tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y - AbstractPosition.TOKEN_MOVE_SMALL);
                    this.tokens[0].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X + AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y + AbstractPosition.TOKEN_MOVE_SMALL);
                    this.tokens[1].draw(g, tp, visualSettings);
                    break;
                case 3:
                    tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y - AbstractPosition.TOKEN_MOVE_MINI2);
                    this.tokens[0].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X + AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y - AbstractPosition.TOKEN_MOVE_MINI2);
                    this.tokens[1].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X, this.Origo.Y + AbstractPosition.TOKEN_MOVE_MINI2);
                    this.tokens[2].draw(g, tp, visualSettings);
                    break;
                case 4:
                    tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y - AbstractPosition.TOKEN_MOVE_SMALL);
                    this.tokens[0].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X + AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y + AbstractPosition.TOKEN_MOVE_SMALL);
                    this.tokens[1].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X + AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y - AbstractPosition.TOKEN_MOVE_SMALL);
                    this.tokens[2].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE_SMALL, this.Origo.Y + AbstractPosition.TOKEN_MOVE_SMALL);
                    this.tokens[3].draw(g, tp, visualSettings);
                    break;
                case 5:
                    tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE, this.Origo.Y - AbstractPosition.TOKEN_MOVE);
                    this.tokens[0].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X + AbstractPosition.TOKEN_MOVE, this.Origo.Y + AbstractPosition.TOKEN_MOVE);
                    this.tokens[1].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X + AbstractPosition.TOKEN_MOVE, this.Origo.Y - AbstractPosition.TOKEN_MOVE);
                    this.tokens[2].draw(g, tp, visualSettings);
                    tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE, this.Origo.Y + AbstractPosition.TOKEN_MOVE);
                    this.tokens[3].draw(g, tp, visualSettings);
                    this.tokens[4].draw(g, this.Origo, visualSettings);
                    break;
            }
            if (tokenCount > 5)
            {
                tp = new PointF(this.Origo.X - AbstractPosition.TOKEN_MOVE_MINI, this.Origo.Y - AbstractPosition.TOKEN_MOVE_MINI2);
                this.tokens[0].draw(g, tp, visualSettings);
                g.DrawString(tokenCount.ToString(), visualSettings.DefaultFont, visualSettings.DefaultBrush, this.Origo.X - AbstractPosition.TOKEN_MOVE_MINI, this.Origo.Y);
            }
            if (this.showAnnotation)
            {
                string labelName = (visibleSettings.VisiblePositionLabel ? this.name : "");
                string label = labelName + (this.CapacityLimit > 0 && visibleSettings.VisibleCapacity ? ("".Equals(labelName) ? "" : " (") + this.CapacityLimit.ToString() + ("".Equals(labelName) ? "" : ")") : "");
                if (!"".Equals(label))
                {
                    SizeF textSize = g.MeasureString(label, visualSettings.DefaultFont);
                    float offset = (this.Size.Width - textSize.Width) / 2;
                    g.DrawString(label, visualSettings.DefaultFont, visualSettings.DefaultBrush, this.Point.X + offset + this.LabelOffset.X, this.Point.Y - textSize.Height + this.LabelOffset.Y);
                }
            }
        }

        public long calculateMaxTokenUnid()
        {
            long maxUnid = -1;
            foreach (AbstractToken item in this.tokens)
            {
                if (maxUnid < item.Unid)
                {
                    maxUnid = item.Unid;
                }
            }
            return maxUnid;
        }

        public bool hasEnoughTokens(int weight)
        {
            return (this.tokens.Count >= weight);
        }

        public bool isCapcityLimitInjured(int weight)
        {
            bool ret = false;
            if (this.capacityLimit > 0)
            {
                ret = ((this.tokens.Count + weight) > this.capacityLimit);
            }
            return ret;
        }

        public List<AbstractToken> takeAwayTokens(bool changeStatistics)
        {
            return this.takeAwayTokens(this.tokens.Count, changeStatistics);
        }

        public List<AbstractToken> takeAwayTokens(int weight, bool changeStatistics)
        {
            List<AbstractToken> tokens = new List<AbstractToken>();
            for (int i = 0; i < weight; i++)
            {
                tokens.Add(this.takeAwayToken());
            }
            if (changeStatistics)
            {
                this.addStatistics();
            }
            return tokens;
        }

        public void changeTokens(List<AbstractToken> tokens)
        {
            this.tokens = new List<AbstractToken>();
            this.tokens.AddRange(tokens);
        }

        public void initStatistics()
        {
            if (this.tokens != null)
            {
                this.statistics.init(this.tokens.Count);
            }
        }

        public void addStatistics()
        {
            if (this.tokens != null)
            {
                this.statistics.add(this.tokens.Count);
            }
        }

        public override string ToString()
        {
            return "Position " + this.name;
        }

    }
}
