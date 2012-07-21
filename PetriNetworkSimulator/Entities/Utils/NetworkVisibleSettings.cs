using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Entities.Utils
{
    public partial class NetworkVisibleSettings
    {

        private bool visibleNotes;
        private bool visibleTransitionLabel;
        private bool visiblePriority;
        private bool visibleEdgeWeight;
        private bool visiblePositionLabel;
        private bool visibleEdgeLabel;
        private bool visibleEdgeHelpLine;
        private bool visibleReadyToFireTransitions;
        private bool visibleCapacity;
        private bool visibleClock;

        public bool VisibleNotes
        {
            get { return this.visibleNotes; }
            set { this.visibleNotes = value; }
        }

        public bool VisibleTransitionLabel
        {
            get { return this.visibleTransitionLabel; }
            set { this.visibleTransitionLabel = value; }
        }

        public bool VisiblePriority
        {
            get { return this.visiblePriority; }
            set { this.visiblePriority = value; }
        }

        public bool VisibleEdgeWeight
        {
            get { return this.visibleEdgeWeight; }
            set { this.visibleEdgeWeight = value; }
        }

        public bool VisiblePositionLabel
        {
            get { return this.visiblePositionLabel; }
            set { this.visiblePositionLabel = value; }
        }

        public bool VisibleEdgeLabel
        {
            get { return this.visibleEdgeLabel; }
            set { this.visibleEdgeLabel = value; }
        }

        public bool VisibleEdgeHelpLine
        {
            get { return this.visibleEdgeHelpLine; }
            set { this.visibleEdgeHelpLine = value; }
        }

        public bool VisibleReadyToFireTransitions
        {
            get { return this.visibleReadyToFireTransitions; }
            set { this.visibleReadyToFireTransitions = value; }
        }

        public bool VisibleCapacity
        {
            get { return this.visibleCapacity; }
            set { this.visibleCapacity = value; }
        }

        public bool VisibleClock
        {
            get { return this.visibleClock; }
            set { this.visibleClock = value; }
        }

        public NetworkVisibleSettings()
        {
            this.visibleNotes = true;
            this.visibleTransitionLabel = true;
            this.visiblePriority = true;
            this.visibleEdgeWeight = true;
            this.visiblePositionLabel = true;
            this.visibleEdgeLabel = true;
            this.visibleEdgeHelpLine = true;
            this.visibleReadyToFireTransitions = true;
            this.visibleCapacity = true;
            this.visibleClock = true;
        }

    }
}
