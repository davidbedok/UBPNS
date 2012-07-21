using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkSimulator.Forms.Common;
using PetriNetworkSimulator.Forms.Tools;
using PetriNetworkSimulator.Forms.Dialogs;
using PetriNetworkSimulator.Entities.Network;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Common.Item.Base;
using PetriNetworkSimulator.Entities.Common.Base;
using PetriNetworkSimulator.Entities.Common.TokenPlayer;
using PetriNetworkSimulator.Entities.Common.Edge;
using PetriNetworkSimulator.Entities.Common.Item.Transition;
using PetriNetworkSimulator.Utils;
using PetriNetworkSimulator.Controls;
using PetriNetworkSimulator.Entities.Item.NetNote;
using PetriNetworkSimulator.Entities.Item.NetPosition;
using PetriNetworkSimulator.Entities.Item.NetTransition;
using PetriNetworkSimulator.Entities.Event;
using PetriNetworkSimulator.Entities.State.Vector;

namespace PetriNetworkSimulator.Forms.Main
{
    partial class MDIParent
    {
        private const float PROPERTY_ROW_HEIGHT = 22F;
        private const float PROPERTY_ROW_HEIGHT_BUTTON = 27F;

        private void addCustomPropertyTextRow(string labelText, string textBoxText, NetworkProperty networkProperty, bool disabled)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyTextBox(textBoxText, new EventHandler(dynamicProperty_ValueChanged), networkProperty, disabled), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyNumberRow(string labelText, float value, float minimumValue, float maximumValue, NetworkProperty networkProperty, bool disabled, int decimalPlaces)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyNumericUpDown((decimal)value, (decimal)minimumValue, (decimal)maximumValue, new EventHandler(dynamicProperty_ValueChanged), networkProperty, disabled, decimalPlaces), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyTransitionTypeComboBoxRow(AbstractNetwork network, string labelText, TransitionType transitionType, NetworkProperty networkProperty, bool disabled)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyTransitionTypeComboBox(transitionType, new EventHandler(dynamicProperty_ValueChanged), new PropertyTag(networkProperty, network), disabled), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyEdgeTypeComboBoxRow(AbstractNetwork network, string labelText, EdgeType edgeType, NetworkProperty networkProperty, bool disabled)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyEdgeTypeComboBox(edgeType, new EventHandler(dynamicProperty_ValueChanged), new PropertyTag(networkProperty, network), disabled), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomGroupPropertyButtonRow(AbstractNetwork network, string labelText, string buttonText, NetworkProperty networkProperty, bool disabled)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT_BUTTON));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyButton(buttonText, new EventHandler(dynamicProperty_ValueChanged), new PropertyTag(networkProperty, network), disabled), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyButtonRow(AbstractNetwork network, string labelText, string buttonText, NetworkProperty networkProperty, bool disabled)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT_BUTTON));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyButton(buttonText, new EventHandler(dynamicProperty_ValueChanged), new PropertyTag(networkProperty, network), disabled), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyColorRow(string labelText, Color color, NetworkProperty networkProperty)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyColorPanel(color,networkProperty, new EventHandler(PropertyColor_Click)), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyFontRow(string labelText, Font font, NetworkProperty networkProperty)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyFontPanel(font, networkProperty, new EventHandler(PropertyColor_Click)), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyCheckboxRow(string labelText, bool status, NetworkProperty networkProperty)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyCheckboxPanel(status, networkProperty, new EventHandler(dynamicProperty_ValueChanged)), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomPropertyGroupMoveToolRow(string labelText, float smallStep, float longStep, AbstractNetwork network, NetworkProperty networkProperty, bool disabled)
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT_BUTTON));
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyLabel(labelText), 0, this.tmpPropertyPanelRowCount);
            this.tlpProperty.Controls.Add(ControlHelper.getPropertyGroupMoveTool(smallStep, longStep, network, new PropertyGroupMoveHandler(PropertyGroupMoveTool_valueChanged), networkProperty, disabled), 1, this.tmpPropertyPanelRowCount);
            this.tmpPropertyPanelRowCount++;
        }

        private void addCustomTokenRow(AbstractItem parent, AbstractToken token, bool disabled)
        {
            this.tlpToken.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpToken.Controls.Add(ControlHelper.getPropertyLabel(token.Unid.ToString()), 0, this.tmpTokenPanelRowCount);
            this.tlpToken.Controls.Add(ControlHelper.getPropertyTextBox(token.Name, new EventHandler(tokenAndEventValueChanged), new PropertyTag(null, token, NetworkProperty.NAME), disabled), 1, this.tmpTokenPanelRowCount);
            this.tlpToken.Controls.Add(ControlHelper.getPropertyColorPanel(token.TokenColor, new EventHandler(tokenAndEventValueChanged), new PropertyTag(null, token, NetworkProperty.COLOR), disabled), 2, this.tmpTokenPanelRowCount);                 
            this.tlpToken.Controls.Add(ControlHelper.getPropertyButton("DEL", new EventHandler(tokenAndEventValueChanged), new PropertyTag(parent, token, NetworkProperty.DELETE), disabled), 3, this.tmpTokenPanelRowCount);
            this.tmpTokenPanelRowCount++;
        }

        private void addEventRow(Object parentObject, PetriEvent petriEvent, bool disabled)
        {
            this.tlpEvent.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpEvent.Controls.Add(ControlHelper.getPropertyLabel(petriEvent.Type.ToString()), 0, this.tmpEventPanelRowCount);
            this.tlpEvent.Controls.Add(ControlHelper.getPropertyTextBox(petriEvent.Name, new EventHandler(tokenAndEventValueChanged), new PropertyTag(parentObject, petriEvent), disabled), 1, this.tmpEventPanelRowCount);
            this.tmpEventPanelRowCount++;
        }

        private void addLastRow()
        {
            this.tlpProperty.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tmpPropertyPanelRowCount++;
        }

        private void addFirstTokenRow()
        {
            this.tlpToken.RowStyles.Add(new RowStyle(SizeType.Absolute, MDIParent.PROPERTY_ROW_HEIGHT));
            this.tlpToken.Controls.Add(ControlHelper.getHeaderLabel("Unid"), 0, this.tmpTokenPanelRowCount);
            this.tlpToken.Controls.Add(ControlHelper.getHeaderLabel("Name"), 1, this.tmpTokenPanelRowCount);
            this.tlpToken.Controls.Add(ControlHelper.getHeaderLabel(""), 2, this.tmpTokenPanelRowCount);
            this.tmpTokenPanelRowCount++;
        }

        private void addLastTokenRow()
        {
            this.tlpToken.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tmpTokenPanelRowCount++;
        }

        private void addLastEventRow()
        {
            this.tlpEvent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tmpEventPanelRowCount++;
        }

        private void refreshPropertyData(AbstractItem item)
        {
            foreach (Control control in this.tlpProperty.Controls)
            {
                if ((control != null) && (control is NumericUpDown) && (control.Tag != null) && (control.Tag is NetworkProperty))
                {
                    NumericUpDown nud = (NumericUpDown)control;
                    nud.ValueChanged -= new EventHandler(dynamicProperty_ValueChanged);
                    NetworkProperty propertyTag = (NetworkProperty)control.Tag;
                    if (item is AbstractNetworkItem)
                    {
                        AbstractNetworkItem networkItem = (AbstractNetworkItem)item;
                        try
                        {
                            switch (propertyTag)
                            {
                                case NetworkProperty.ORIGO_X:
                                    nud.Value = (decimal)networkItem.Origo.X;
                                    break;
                                case NetworkProperty.ORIGO_Y:
                                    nud.Value = (decimal)networkItem.Origo.Y;
                                    break;
                                case NetworkProperty.POINT_X:
                                    nud.Value = (decimal)networkItem.Point.X;
                                    break;
                                case NetworkProperty.POINT_Y:
                                    nud.Value = (decimal)networkItem.Point.Y;
                                    break;
                                case NetworkProperty.RADIUS:
                                    nud.Value = (decimal)networkItem.Radius;
                                    break;
                                case NetworkProperty.SIZE_WIDTH:
                                    nud.Value = (decimal)networkItem.Size.Width;
                                    break;
                                case NetworkProperty.SIZE_HEIGHT:
                                    nud.Value = (decimal)networkItem.Size.Height;
                                    break;
                                case NetworkProperty.LABELOFFSET_X:
                                    nud.Value = (decimal)networkItem.LabelOffset.X;
                                    break;
                                case NetworkProperty.LABELOFFSET_Y:
                                    nud.Value = (decimal)networkItem.LabelOffset.Y;
                                    break;
                            }
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            this.writeConsole("An error happened, while refresh properties. "+ e.Message);
                        }
                    }
                    else if (item is AbstractEdge)
                    {
                        AbstractEdge edge = (AbstractEdge)item;
                        try
                        {
                            switch (propertyTag)
                            {
                                case NetworkProperty.CURVE_OFFSET_X:
                                    nud.Value = (decimal)edge.CurveMiddlePointOffset.X;
                                    break;
                                case NetworkProperty.CURVE_OFFSET_Y:
                                    nud.Value = (decimal)edge.CurveMiddlePointOffset.Y;
                                    break;
                                case NetworkProperty.WEIGHT:
                                    nud.Value = (decimal)edge.Weight;
                                    break;
                            }
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            this.writeConsole("An error happened, while refresh properties. " + e.Message);
                        }
                    }
                    nud.ValueChanged += new EventHandler(dynamicProperty_ValueChanged);
                }
            }
        }

        private void dynamicProperty_ValueChanged(object sender, EventArgs e)
        {
            if ((sender is NumericUpDown) || (sender is TextBox) || (sender is Button) || (sender is CheckBox) || (sender is ComboBox))
            {
                if ((this.tlpProperty != null) && (this.tlpProperty.Tag != null))
                {
                    if (this.tlpProperty.Tag is AbstractItem)
                    {
                        AbstractItem item = (AbstractItem)this.tlpProperty.Tag;
                        if (sender is NumericUpDown)
                        {
                            NumericUpDown nud = (NumericUpDown)sender;
                            if (nud.Tag is NetworkProperty)
                            {
                                NetworkProperty propertyTag = (NetworkProperty)nud.Tag;
                                float nudValue = (float)nud.Value;
                                if (item is AbstractNetworkItem)
                                {
                                    AbstractNetworkItem networkItem = (AbstractNetworkItem)item;
                                    switch (propertyTag)
                                    {
                                        case NetworkProperty.ORIGO_X:
                                            networkItem.Origo = new PointF(nudValue, networkItem.Origo.Y);
                                            break;
                                        case NetworkProperty.ORIGO_Y:
                                            networkItem.Origo = new PointF(networkItem.Origo.X, nudValue);
                                            break;
                                        case NetworkProperty.POINT_X:
                                            networkItem.Point = new PointF(nudValue, networkItem.Point.Y);
                                            break;
                                        case NetworkProperty.POINT_Y:
                                            networkItem.Point = new PointF(networkItem.Point.X, nudValue);
                                            break;
                                        case NetworkProperty.RADIUS:
                                            networkItem.Radius = nudValue;
                                            break;
                                        case NetworkProperty.LABELOFFSET_X:
                                            networkItem.LabelOffset = new PointF(nudValue, networkItem.LabelOffset.Y);
                                            break;
                                        case NetworkProperty.LABELOFFSET_Y:
                                            networkItem.LabelOffset = new PointF(networkItem.LabelOffset.X, nudValue);
                                            break;
                                    }
                                    if (item is Position)
                                    {
                                        Position itemPosition = (Position)item;
                                        switch (propertyTag)
                                        {
                                            case NetworkProperty.SIZE_WIDTH:
                                                networkItem.Size = new SizeF(nudValue, nudValue);
                                                break;
                                            case NetworkProperty.SIZE_HEIGHT:
                                                networkItem.Size = new SizeF(nudValue, nudValue);
                                                break;
                                            case NetworkProperty.CAPACITYLIMIT:
                                                itemPosition.CapacityLimit = (int)nud.Value;
                                                break;
                                        }
                                    }
                                    else if (item is Transition)
                                    {
                                        Transition itemTransition = (Transition)item;
                                        switch (propertyTag)
                                        {
                                            case NetworkProperty.SIZE_WIDTH:
                                                networkItem.Size = new SizeF(nudValue, networkItem.Size.Height);
                                                break;
                                            case NetworkProperty.SIZE_HEIGHT:
                                                networkItem.Size = new SizeF(networkItem.Size.Width, nudValue);
                                                break;
                                            case NetworkProperty.ANGLE:
                                                itemTransition.Angle = nudValue;
                                                break;
                                            case NetworkProperty.PRIORITY:
                                                itemTransition.Priority = (int)nud.Value;
                                                break;
                                            case NetworkProperty.DELAY:
                                                itemTransition.Delay = (int)nud.Value;
                                                break;
                                            case NetworkProperty.CLOCKRADIUS:
                                                itemTransition.ClockRadius = nudValue;
                                                break;
                                            case NetworkProperty.CLOCKOFFSET_X:
                                                itemTransition.ClockOffset = new PointF(nudValue, itemTransition.ClockOffset.Y);
                                                break;
                                            case NetworkProperty.CLOCKOFFSET_Y:
                                                itemTransition.ClockOffset = new PointF(itemTransition.ClockOffset.X, nudValue);
                                                break;
                                        }
                                    }
                                    else if (item is Note)
                                    {
                                        Note itemNote = (Note)item;
                                        switch (propertyTag)
                                        {
                                            case NetworkProperty.SIZE_WIDTH:
                                                networkItem.Size = new SizeF(nudValue, networkItem.Size.Height);
                                                break;
                                            case NetworkProperty.SIZE_HEIGHT:
                                                networkItem.Size = new SizeF(networkItem.Size.Width, nudValue);
                                                break;
                                        }
                                    }
                                }
                                else if (item is AbstractEdge)
                                {
                                    AbstractEdge edge = (AbstractEdge)item;
                                    switch (propertyTag)
                                    {
                                        case NetworkProperty.WEIGHT:
                                            edge.Weight = (int)nudValue;
                                            break;
                                        case NetworkProperty.CURVE_OFFSET_X:
                                            edge.CurveMiddlePointOffset = new PointF(nudValue, edge.CurveMiddlePointOffset.Y);
                                            break;
                                        case NetworkProperty.CURVE_OFFSET_Y:
                                            edge.CurveMiddlePointOffset = new PointF(edge.CurveMiddlePointOffset.X, nudValue);
                                            break;
                                    }
                                }

                            }
                        }
                        if (sender is TextBox)
                        {
                            TextBox tb = (TextBox)sender;
                            if (tb.Tag is NetworkProperty)
                            {
                                NetworkProperty propertyTag = (NetworkProperty)tb.Tag;
                                string tbText = (string)tb.Text;
                                if (item is AbstractItem)
                                {
                                    switch (propertyTag)
                                    {
                                        case NetworkProperty.NAME:
                                            item.Name = tbText;
                                            break;
                                    }
                                    if (item is Note)
                                    {
                                        Note itemNote = (Note)item;
                                        switch (propertyTag)
                                        {
                                            case NetworkProperty.TEXT:
                                                itemNote.Text = tbText;
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        if (sender is Button)
                        {
                            Button bu = (Button)sender;
                            if (bu.Tag is PropertyTag)
                            {
                                PropertyTag propertyTag = (PropertyTag)bu.Tag;
                                if (item is AbstractEdge)
                                {
                                    AbstractEdge edge = (AbstractEdge)item;
                                    switch (propertyTag.Property)
                                    {
                                        case NetworkProperty.CHANGEDIRECTION:
                                            if (propertyTag.Network != null)
                                            {
                                                propertyTag.Network.changeEdgeDirection(edge,true);
                                            }
                                            break;
                                        case NetworkProperty.STRAIGHTEN:
                                            edge.CurveMiddlePointOffset = new PointF(0, 0);
                                            List<AbstractItem> selItems = new List<AbstractItem>();
                                            selItems.Add(edge);
                                            this.refreshPropertyData(edge);
                                            break;
                                    }
                                }
                            }
                        }
                        if (sender is ComboBox)
                        {
                            ComboBox cb = (ComboBox)sender;
                            if (cb.Tag is PropertyTag)
                            {
                                PropertyTag propertyTag = (PropertyTag)cb.Tag;
                                if (item is AbstractTransition)
                                {
                                    TransitionType transitionType = (TransitionType)cb.SelectedItem;
                                    Transition transition = (Transition)item;
                                    switch (propertyTag.Property)
                                    {
                                        case NetworkProperty.TRANSITIONTYPE:
                                            transition.TransitionType = transitionType;
                                            ((PetriNetwork)propertyTag.Network).changeTransitionType(transition, transitionType);
                                            break;
                                    }
                                }
                                else if (item is AbstractEdge)
                                {
                                    EdgeType edgeType = (EdgeType)cb.SelectedItem;
                                    AbstractEdge edge = (AbstractEdge)item;
                                    switch (propertyTag.Property)
                                    {
                                        case NetworkProperty.EDGETYPE:
                                            edge.EdgeType = edgeType;
                                            this.refreshPropertyData(edge);
                                            break;
                                    }
                                }
                            }
                        }
                        if (sender is CheckBox)
                        {
                            CheckBox cb = (CheckBox)sender;
                            if (cb.Tag is NetworkProperty)
                            {
                                NetworkProperty propertyTag = (NetworkProperty)cb.Tag;
                                if (item is AbstractItem)
                                {
                                    switch (propertyTag)
                                    {
                                        case NetworkProperty.SHOWANNOTATION:
                                            item.ShowAnnotation = cb.Checked;
                                            break;
                                    }
                                }
                            }
                        }
                        this.refreshPropertyData(item);
                        this.reDrawActivePetriNetwork();
                    }
                    else if (this.tlpProperty.Tag is NetworkPropertyGroup)
                    {
                        NetworkPropertyGroup npg = (NetworkPropertyGroup)this.tlpProperty.Tag;
                        if (sender is Button)
                        {
                            Button bu = (Button)sender;
                            if (bu.Tag is PropertyTag)
                            {
                                PropertyTag propertyTag = (PropertyTag)bu.Tag;
                                switch (propertyTag.Property)
                                {
                                    case NetworkProperty.CHANGEDIRECTION:
                                    case NetworkProperty.SAMEORIGOX:
                                    case NetworkProperty.SAMEORIGOY:
                                        propertyTag.Network.modifySelectedItems(propertyTag.Property, 0);
                                        break;
                                }
                                this.reDrawActivePetriNetwork();
                            }
                        }
                    }
                    else if (this.tlpProperty.Tag is PetriNetwork)
                    {
                        PetriNetwork network = (PetriNetwork)this.tlpProperty.Tag;
                        if (sender is TextBox)
                        {
                            TextBox tb = (TextBox)sender;
                            if (tb.Tag is NetworkProperty)
                            {
                                NetworkProperty property = (NetworkProperty)tb.Tag;
                                switch (property)
                                {
                                    case NetworkProperty.NAME:
                                        network.Name = tb.Text;
                                        if (this.ActiveMdiChild != null)
                                        {
                                            this.ActiveMdiChild.Text = network.Title;
                                        }
                                        break;
                                    case NetworkProperty.TOKENPREFIX:
                                        network.IdentityProvider.TokenPrefix = tb.Text;
                                        break;
                                    case NetworkProperty.POSITIONPREFIX:
                                        network.IdentityProvider.PositionPrefix = tb.Text;
                                        break;
                                    case NetworkProperty.TRANSITIONPREFIX:
                                        network.IdentityProvider.TransitionPrefix = tb.Text;
                                        break;
                                    case NetworkProperty.NOTEPREFIX:
                                        network.IdentityProvider.NotePrefix = tb.Text;
                                        break;
                                    case NetworkProperty.STATEPREFIX:
                                        network.StatePrefix = tb.Text;
                                        break;
                                }
                                this.reDrawActivePetriNetwork();
                            }
                        }
                        else if (sender is NumericUpDown)
                        {
                            NumericUpDown nud = (NumericUpDown)sender;
                            if (nud.Tag is NetworkProperty)
                            {
                                NetworkProperty property = (NetworkProperty)nud.Tag;
                                switch (property)
                                {
                                    case NetworkProperty.EDGEWEIGHT:
                                        network.DefaultEdgeWeight = (int)nud.Value;
                                        break;
                                    case NetworkProperty.SIZE_WIDTH:
                                        network.Width = (int)nud.Value;
                                        break;
                                    case NetworkProperty.SIZE_HEIGHT:
                                        network.Height = (int)nud.Value;
                                        break;
                                }
                                this.reDrawActivePetriNetwork();
                            }
                        }
                        else if (sender is CheckBox)
                        {
                            CheckBox che = (CheckBox)sender;
                            if (che.Tag is NetworkProperty)
                            {
                                NetworkProperty property = (NetworkProperty)che.Tag;
                                switch (property)
                                {
                                    case NetworkProperty.VISIBLEEDGELABEL:
                                        network.VisibleSettings.VisibleEdgeLabel = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLEEDGEWEIGHT:
                                        network.VisibleSettings.VisibleEdgeWeight = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLENOTES:
                                        network.VisibleSettings.VisibleNotes = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLEPOSITIONLABEL:
                                        network.VisibleSettings.VisiblePositionLabel = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLEPRIORITY:
                                        network.VisibleSettings.VisiblePriority = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLECAPACITY:
                                        network.VisibleSettings.VisibleCapacity = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLETRANSITIONLABEL:
                                        network.VisibleSettings.VisibleTransitionLabel = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLEEDGEHELPLINE:
                                        network.VisibleSettings.VisibleEdgeHelpLine = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLEREADYTOFIRETRANSACTIONS:
                                        network.VisibleSettings.VisibleReadyToFireTransitions = che.Checked;
                                        break;
                                    case NetworkProperty.VISIBLECLOCK:
                                        network.VisibleSettings.VisibleClock = che.Checked;
                                        break;
                                }
                                this.reDrawActivePetriNetwork();
                            }
                        }
                    }
                }
            }
        }

        private void tokenAndEventValueChanged(object sender, EventArgs e)
        {
            if ((sender != null) && ((sender is TextBox) || (sender is Button) || (sender is Panel)))
            {
                Control control = (Control)sender;
                if ((control.Tag != null) && ( control.Tag is PropertyTag))
                {
                    PropertyTag propertyTag = (PropertyTag)control.Tag;
                    if (control is TextBox)
                    {
                        TextBox tb = (TextBox)control;
                        string tbText = (string)tb.Text;
                        if ( (propertyTag.ParentObject != null) && ( propertyTag.PetriEvent != null ) )
                        {
                            EventTrunk trunk = null;
                            if (propertyTag.ParentObject is IPetriEvent)
                            {
                                trunk = ((IPetriEvent)propertyTag.ParentObject).PetriEvents;
                            }
                            if (trunk != null)
                            {
                                trunk.modifyEvent(propertyTag.PetriEvent.Type, tbText);
                            }
                        } else if (propertyTag.Item is AbstractItem)
                        {
                            switch (propertyTag.Property)
                            {
                                case NetworkProperty.NAME:
                                    propertyTag.Item.Name = tbText;
                                    break;
                            }
                        }
                    }
                    if (control is Button)
                    {
                        if ((propertyTag.Item is AbstractToken) && (propertyTag.Parent != null) && ( propertyTag.Parent is Position))
                        {
                            Position positionItem = (Position)propertyTag.Parent;
                            switch (propertyTag.Property)
                            {
                                case NetworkProperty.DELETE:
                                    positionItem.deleteToken((AbstractToken)propertyTag.Item);
                                    break;
                            }
                        }
                        this.child_networkItemSelected(this.getActivePetriNetwork().SelectedItems, true);
                        this.reDrawActivePetriNetwork();
                    }
                    if (control is Panel)
                    {
                        if (propertyTag.Item is AbstractToken)
                        {
                            AbstractToken token = (AbstractToken)propertyTag.Item;
                            Panel pa = (Panel)sender;
                            Color color = Color.FromArgb(0, 0, 0);
                            switch (propertyTag.Property)
                            {
                                case NetworkProperty.COLOR:
                                    color = this.showColorDialog(token.TokenColor);
                                    token.TokenColor = color;
                                    break;
                            }
                            pa.BackColor = color;
                            this.reDrawActivePetriNetwork();
                        }
                    }
                }
            }
        }

        private void PropertyGroupMoveTool_valueChanged(AbstractNetwork network, NetworkProperty networkProperty, float value)
        {
            if ((this.tlpProperty != null) && (this.tlpProperty.Tag != null))
            {
                if (this.tlpProperty.Tag is NetworkPropertyGroup)
                {
                    NetworkPropertyGroup npg = (NetworkPropertyGroup)this.tlpProperty.Tag;
                    network.modifySelectedItems(networkProperty, value);
                    this.reDrawActivePetriNetwork();
                }
            }
        }

        private Color showColorDialog( Color color )
        {
            Color ret = color;
            ColorDialog dialog = new ColorDialog();
            dialog.Color = color;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                color = dialog.Color;
            }
            return color;
        }

        private Font showFontDialog(Font font)
        {
            Font ret = font; 
            FontDialog dialog = new FontDialog();
            dialog.Font = font;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                font = dialog.Font;
            }
            return font;
        }

        private void PropertyColor_Click(object sender, EventArgs e)
        {
            if ((this.tlpProperty != null) && (this.tlpProperty.Tag != null))
            {
                if (this.tlpProperty.Tag is PetriNetwork)
                {
                    PetriNetwork network = (PetriNetwork)this.tlpProperty.Tag;
                    if (sender is Panel)
                    {
                        Panel pa = (Panel)sender;
                        if ((pa.Tag != null) && (pa.Tag is NetworkProperty))
                        {
                            Color color = Color.FromArgb(0, 0, 0);
                            NetworkProperty property = (NetworkProperty)pa.Tag;
                            switch (property)
                            {
                                case NetworkProperty.DEFAULTCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.DefaultColor);
                                    network.VisualSettings.DefaultColor = color;
                                    break;
                                case NetworkProperty.SELECTEDCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.SelectedColor);
                                    network.VisualSettings.SelectedColor = color;
                                    break;
                                case NetworkProperty.MARKCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.MarkColor);
                                    network.VisualSettings.MarkColor = color;
                                    break;
                                case NetworkProperty.SHADOWCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.ShadowColor);
                                    network.VisualSettings.ShadowColor = color;
                                    break;
                                case NetworkProperty.NOTEPENCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.NotePenColor);
                                    network.VisualSettings.NotePenColor = color;
                                    break;
                                case NetworkProperty.NOTEBORDERCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.NoteBorderColor);
                                    network.VisualSettings.NoteBorderColor = color;
                                    break;
                                case NetworkProperty.NOTEBRUSHCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.NoteBrushColor);
                                    network.VisualSettings.NoteBrushColor = color;
                                    break;
                                case NetworkProperty.HELPCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.HelpColor);
                                    network.VisualSettings.HelpColor = color;
                                    break;
                                case NetworkProperty.STATECOLOR:
                                    color = this.showColorDialog(network.VisualSettings.StateColor);
                                    network.VisualSettings.StateColor = color;
                                    break;
                                case NetworkProperty.MARKASREADYTOFIRECOLOR:
                                    color = this.showColorDialog(network.VisualSettings.MarkAsReadyToFireColor);
                                    network.VisualSettings.MarkAsReadyToFireColor = color;
                                    break;
                                case NetworkProperty.CLOCKCOLOR:
                                    color = this.showColorDialog(network.VisualSettings.ClockColor);
                                    network.VisualSettings.ClockColor = color;
                                    break;
                            }
                            pa.BackColor = color;
                        }
                    }
                    else if (sender is Label)
                    {
                        Label la = (Label)sender;
                        if ((la.Tag != null) && (la.Tag is NetworkProperty))
                        {
                            Font font = new Font("Arial", 10);
                            NetworkProperty property = (NetworkProperty)la.Tag;
                            switch (property)
                            {
                                case NetworkProperty.DEFAULTFONT:
                                    font = this.showFontDialog(network.VisualSettings.DefaultFont);
                                    network.VisualSettings.DefaultFont = font;
                                    break;
                                case NetworkProperty.NOTEFONT:
                                    font = this.showFontDialog(network.VisualSettings.NoteFont);
                                    network.VisualSettings.NoteFont = font;
                                    break;
                            }
                            la.Text = font.FontFamily.Name + " (" + font.Size.ToString() + ")";
                            la.Font = font;
                        }
                    }
                    this.reDrawActivePetriNetwork();
                }
            }
        }

    }
}
