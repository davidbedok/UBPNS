using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using PetriNetworkSimulator.Entities.Common.Network;
using PetriNetworkSimulator.Entities.Enums;
using PetriNetworkSimulator.Controls;

namespace PetriNetworkSimulator.Utils
{
    public static class ControlHelper
    {
        private static int CONTROLCOUNT = 0;

        public static TextBox getPropertyTextBox(string text, EventHandler handler, NetworkProperty networkProperty, bool disabled)
        {
            TextBox textbox = new TextBox();
            textbox.Name = "tbCustomTextBox_" + ControlHelper.CONTROLCOUNT++;
            textbox.Text = text;
            textbox.TextChanged += handler;
            textbox.Tag = networkProperty;
            textbox.ReadOnly = disabled;
            textbox.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return textbox;
        }

        public static TextBox getPropertyTextBox(string text, EventHandler handler, PropertyTag propertyTag, bool disabled)
        {
            TextBox textbox = new TextBox();
            textbox.Name = "tbCustomTextBox_" + ControlHelper.CONTROLCOUNT++;
            textbox.Text = text;
            textbox.TextChanged += handler;
            textbox.Tag = propertyTag;
            textbox.Enabled = !disabled;
            textbox.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return textbox;
        }

        public static Button getPropertyButton(string text, EventHandler handler, NetworkProperty networkProperty, bool disabled)
        {
            Button button = new Button();
            button.Name = "tbCustomButton_" + ControlHelper.CONTROLCOUNT++;
            button.Text = text;
            button.Click += handler;
            button.Tag = networkProperty;
            button.Enabled = !disabled;
            button.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return button;
        }

        public static Button getPropertyButton(string text, EventHandler handler, PropertyTag propertyTag, bool disabled)
        {
            Button button = new Button();
            button.Name = "tbCustomButton_" + ControlHelper.CONTROLCOUNT++;
            if ("DEL".Equals(text))
            {
                button.Text = "";
                button.Image = PetriNetworkSimulator.Properties.Resources.exitApplication;
            }
            else
            {
                button.Text = text;
            }
            button.Click += handler;
            button.Tag = propertyTag;
            button.Enabled = !disabled;
            button.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return button;
        }

        public static NumericUpDown getPropertyNumericUpDown(decimal value, decimal minimumValue, decimal maximumValue, EventHandler handler, NetworkProperty networkProperty, bool disabled, int decimalPlaces)
        {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Name = "tbCustomNumericUpDown_" + ControlHelper.CONTROLCOUNT++;
            numericUpDown.Minimum = minimumValue;
            numericUpDown.Maximum = maximumValue;
            numericUpDown.Value = value;
            numericUpDown.DecimalPlaces = decimalPlaces;
            numericUpDown.ValueChanged += handler;
            numericUpDown.Tag = networkProperty;
            numericUpDown.Enabled = !disabled;
            numericUpDown.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return numericUpDown;
        }

        public static ComboBox getPropertyTransitionTypeComboBox(TransitionType transitionType, EventHandler handler, PropertyTag propertyTag, bool disabled)
        {
            ComboBox control = new ComboBox();
            control.Name = "tbCustomComboBox_" + ControlHelper.CONTROLCOUNT++;
            foreach (TransitionType item in TransitionType.Values)
            {
                control.Items.Add(item);
            }
            control.SelectedItem = transitionType;
            control.DropDownStyle = ComboBoxStyle.DropDownList;
            control.SelectedValueChanged += handler;
            control.Tag = propertyTag;
            control.Enabled = !disabled;
            control.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return control;
        }

        public static ComboBox getPropertyEdgeTypeComboBox(EdgeType edgeType, EventHandler handler, PropertyTag propertyTag, bool disabled)
        {
            ComboBox control = new ComboBox();
            control.Name = "tbCustomComboBox_" + ControlHelper.CONTROLCOUNT++;
            foreach (EdgeType item in EdgeType.Values)
            {
                control.Items.Add(item);
            }
            control.SelectedItem = edgeType;
            control.DropDownStyle = ComboBoxStyle.DropDownList;
            control.SelectedValueChanged += handler;
            control.Tag = propertyTag;
            control.Enabled = !disabled;
            control.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return control;
        }
        
        public static PropertyGroupMoveTool getPropertyGroupMoveTool(float smallStep, float longStep, AbstractNetwork network, PropertyGroupMoveHandler handler, NetworkProperty networkProperty, bool disabled)
        {
            PropertyGroupMoveTool tool = new PropertyGroupMoveTool();
            tool.Name = "pgmtCustomPropertyGroupMoveTool" + ControlHelper.CONTROLCOUNT++;
            tool.Network = network;
            tool.Property = networkProperty;
            tool.valueChanged += handler;
            tool.SmallStep = smallStep;
            tool.LongStep = longStep;
            tool.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return tool;
        }

        public static Panel getPropertyColorPanel(Color color, NetworkProperty networkProperty, EventHandler handler)
        {
            Panel panel = new Panel();
            panel.Name = "pgmtCustomPropertyColorPanel" + ControlHelper.CONTROLCOUNT++;
            panel.Width = 20;
            panel.Height = 20;
            panel.BackColor = color;
            panel.Click += handler;
            panel.Tag = networkProperty;
            return panel;
        }

        public static Panel getPropertyColorPanel(Color color, EventHandler handler, PropertyTag propertyTag, bool disabled)
        {
            Panel panel = new Panel();
            panel.Name = "pgmtCustomPropertyColorPanel" + ControlHelper.CONTROLCOUNT++;
            panel.Width = 20;
            panel.Height = 20;
            panel.BackColor = color;
            panel.Click += handler;
            panel.Tag = propertyTag;
            panel.Enabled = !disabled;
            return panel;
        }

        public static Label getPropertyFontPanel(Font font, NetworkProperty networkProperty, EventHandler handler)
        {
            Label label = new Label();
            label.Name = "pgmtCustomPropertyFontLabel" + ControlHelper.CONTROLCOUNT++;
            label.Text = font.FontFamily.Name + " (" + font.Size.ToString() + ")";
            label.Font = font;
            label.Click += handler;
            label.Tag = networkProperty;
            label.Enabled = true;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            return label;
        }

        public static CheckBox getPropertyCheckboxPanel(bool status, NetworkProperty networkProperty, EventHandler handler)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.Name = "pgmtCustomPropertyColorPanel" + ControlHelper.CONTROLCOUNT++;
            checkbox.Checked = status;
            checkbox.Text = "";
            checkbox.CheckedChanged += handler;
            checkbox.Tag = networkProperty;
            return checkbox;
        }

        public static Label getPropertyLabel( string text, System.Windows.Forms.AnchorStyles anchorStyles = AnchorStyles.Left )
        {
            Label label = new Label();
            label.Name = "lCustomLabel_" + ControlHelper.CONTROLCOUNT++;
            label.Text = text;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Anchor = anchorStyles;
            label.AutoSize = true;
            return label;
        }

        public static Label getHeaderLabel(string text)
        {
            return ControlHelper.getPropertyLabel(text, ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right))));
        }

    }
}
