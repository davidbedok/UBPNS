using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PetriNetworkSimulator.Utils
{
    public class CursorHelper
    {
        private static CursorHelper instance;

        public readonly Cursor TransitionCursor;
        public readonly Cursor TokenCursor;
        public readonly Cursor SelectEdgeCursor;
        public readonly Cursor PositionCursor;
        public readonly Cursor NoteCursor;
        public readonly Cursor EdgeDefCursor;
        public readonly Cursor EdgeCursor;
        
        private CursorHelper()
        {
            this.TransitionCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_transition));
            this.TokenCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_token));
            this.SelectEdgeCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_selectedge));
            this.PositionCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_position));
            this.NoteCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_note));
            this.EdgeDefCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_edge_def));
            this.EdgeCursor = new Cursor(new MemoryStream(Properties.Resources.cursor_edge));
        }

        public static CursorHelper getInstance()
        {
            if (CursorHelper.instance == null)
            {
                CursorHelper.instance = new CursorHelper();
            }
            return CursorHelper.instance;
        }

    }
}
