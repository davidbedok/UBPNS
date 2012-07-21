using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Model.History;
using PetriNetworkLibrary.Model.TokenPlayer;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using System.Drawing;
using PetriNetworkLibrary.Event;

namespace PetriNetworkLibrary.Model.Network
{
    public partial class PetriNetwork
    {

        private const int SWAP_TOKEN_NUMBER = 10;

        private void clearStateMatrix()
        {
            this.actualStateVector = null;
            this.stateMatrix.Clear();
            this.saveState(null, this.getNewStateVector(null));
        }

        public FireReturn fire()
        {
            FireEvent fireEvent = FireEvent.NORMALFIRE;
            Transition fireTransition = null;
            if (this.stateMatrix.Count == 0)
            {
                this.clearStateMatrix();
                fireEvent = FireEvent.INITFIRE;
            }
            else if (!(this.stateMatrix[this.stateMatrix.Count - 1].Equals(this.actualStateVector)))
            {
                this.cutStateMatrix(this.actualStateVector);
                fireEvent = FireEvent.RESETFIRE;
            }
            List<Transition> canFire = this.getReadyToFireTransition();
            if (canFire.Count > 0)
            {
                fireTransition = this.chooseFireTransition(canFire);
                if (fireTransition != null)
                {
                    StateVector stateVector = this.simulateTransitionFire(fireTransition, true);
                    this.saveState(null, stateVector);
                }
            }
            else
            {
                fireEvent = FireEvent.DEADLOCK;
            }
            return new FireReturn(fireEvent, fireTransition);
        }

        public void setStartState(string stateName)
        {
            StateVector sv = StateVector.findItemByName(this.stateHierarchy.States, stateName);
            if (sv != null)
            {
                this.stateMatrix.Clear();
                this.returnToState(sv);
            }
            else
            {
                throw new ArgumentException("Cannot set start state, because '" + stateName + "' is not exists as a name of state in this network.");
            }
        }

        private void returnToState(StateVector stateVector)
        {
            List<Position> positions = this.Positions;
            foreach (Position position in positions)
            {
                List<Token> tokens = stateVector.getTokensByPositionUnid(position.Unid);
                position.changeTokens(tokens);
            }
            this.actualStateVector = stateVector;
        }

        private void swapTokens(List<Token> tokens, int indexA, int indexB)
        {
            Token tmpToken = tokens[indexA];
            tokens[indexA] = tokens[indexB];
            tokens[indexB] = tmpToken;
        }

        private void mixTokens(List<Token> tokens)
        {
            if (tokens.Count > 1)
            {
                for (int i = 0; i < PetriNetwork.SWAP_TOKEN_NUMBER; i++)
                {
                    this.swapTokens(tokens, this.rand.Next(tokens.Count), this.rand.Next(tokens.Count));
                }
            }
        }

        private StateVector simulateTransitionFire(Transition fireTransition, bool changeStatistics)
        {
            this.checkHandler(fireTransition, EventType.PREACTIVATE);

            TransitionHistoryItem historyItem = null;
            if (changeStatistics)
            {
                fireTransition.Statistics.add(1);
                historyItem = new TransitionHistoryItem(fireTransition);
            }
            List<Token> tokens = new List<Token>();
            List<AbstractEdge> inputs = this.getAllInputEdge(fireTransition);
            foreach (AbstractEdge edge in inputs)
            {
                if (edge.Start is Position)
                {
                    Position position = (Position)edge.Start;
                    if (EdgeType.NORMAL.Equals(edge.EdgeType))
                    {
                        this.checkHandler(position, EventType.PREACTIVATE);
                        tokens.AddRange(position.takeAwayTokens(edge.Weight, changeStatistics));
                        this.checkHandler(position, EventType.POSTACTIVATE);
                    }
                    else if (EdgeType.RESET.Equals(edge.EdgeType))
                    {
                        this.checkHandler(position, EventType.PREACTIVATE);
                        tokens.AddRange(position.takeAwayTokens(changeStatistics));
                        this.checkHandler(position, EventType.POSTACTIVATE);
                    }
                    else if (EdgeType.INHIBITOR.Equals(edge.EdgeType))
                    {
                        // nothing to do
                    }
                }
            }

            this.checkHandler(fireTransition, EventType.POSTACTIVATE);

            this.mixTokens(tokens);
            if (changeStatistics && historyItem != null)
            {
                historyItem.addToken(tokens);
            }
            
            List<AbstractEdge> outputs = this.getAllOutputEdge(fireTransition);
            int ti = 0;
            foreach (AbstractEdge edge in outputs)
            {
                if (edge.End is Position)
                {
                    Position position = (Position)edge.End;
                    this.checkHandler(position, EventType.PREACTIVATE);
                    for (int ei = 0; ei < edge.Weight; ei++)
                    {
                        Token token = null;
                        if (ti < tokens.Count)
                        {
                            token = tokens[ti++];
                        }
                        else
                        {
                            token = new Token("", this.unidGenNumber++, Color.Black);
                        }
                        position.addToken(token);
                    }
                    if (changeStatistics)
                    {
                        position.addStatistics();
                    }
                    this.checkHandler(position, EventType.POSTACTIVATE);
                }
            }
            if (changeStatistics && historyItem != null)
            {
                this.addItemToTransitionHistory(historyItem);
            }

            return this.getNewStateVector(null);
        }

        private Transition chooseFireTransition(List<Transition> canFire)
        {
            Transition fireTransition = canFire[0];
            if (FireRule.RANDOM.Equals(this.fireRule))
            {
                fireTransition = canFire[this.rand.Next(canFire.Count)];
            }
            else if (FireRule.ASC_UNID.Equals(this.fireRule))
            {
                long minUnid = canFire[0].Unid;
                foreach (Transition tran in canFire)
                {
                    if (minUnid > tran.Unid)
                    {
                        minUnid = tran.Unid;
                        fireTransition = tran;
                    }
                }
            }
            else if (FireRule.DESC_UNID.Equals(this.fireRule))
            {
                long maxUnid = canFire[0].Unid;
                foreach (Transition tran in canFire)
                {
                    if (maxUnid < tran.Unid)
                    {
                        maxUnid = tran.Unid;
                        fireTransition = tran;
                    }
                }
            }
            else if (FireRule.PRIORITY.Equals(this.fireRule))
            {
                long minPriority = canFire[0].Priority;
                foreach (Transition tran in canFire)
                {
                    if (minPriority > tran.Priority)
                    {
                        minPriority = tran.Priority;
                    }
                }
                List<Transition> canFiremin = new List<Transition>();
                foreach (Transition tran in canFire)
                {
                    if (tran.Priority == minPriority)
                    {
                        canFiremin.Add(tran);
                    }
                }
                fireTransition = canFiremin[this.rand.Next(canFiremin.Count)];
            }
            return fireTransition;
        }

        private bool checkCapacityLimit(Transition transition, Dictionary<Position, Int32> touchedPositions)
        {
            bool cFire = true;
            List<AbstractEdge> outputs = this.getAllOutputEdge(transition);
            if (outputs.Count > 0)
            {
                foreach (AbstractEdge edge in outputs)
                {
                    if (edge.End is Position)
                    {
                        int needCapacity = edge.Weight;
                        Position tmpPos = (Position)edge.End;
                        if (touchedPositions.ContainsKey(tmpPos))
                        {
                            needCapacity -= touchedPositions[tmpPos];
                        }
                        if (tmpPos.isCapcityLimitInjured(needCapacity))
                        {
                            cFire = false;
                        }
                    }
                }
            }
            return cFire;
        }

        private List<Transition> getReadyToFireTransition()
        {
            List<Transition> canFire = new List<Transition>();
            List<Transition> transitions = this.Transitions;
            Dictionary<Position, Int32> touchedPositions = new Dictionary<Position, Int32>();
            bool cFire = false;
            foreach (Transition transition in transitions)
            {
                touchedPositions.Clear();
                if (TransitionType.SOURCE.Equals(transition.TransitionType))
                {
                    // touchedPositions: input positions --> itt ilyen nem lehet!
                    cFire = checkCapacityLimit(transition, touchedPositions);
                    if (cFire)
                    {
                        canFire.Add(transition);
                    }
                }
                else
                {
                    List<AbstractEdge> inputs = this.getAllInputEdge(transition);
                    if (inputs.Count > 0)
                    {
                        cFire = true;
                        foreach (AbstractEdge edge in inputs)
                        {
                            if (edge.Start is Position)
                            {
                                if (EdgeType.NORMAL.Equals(edge.EdgeType))
                                {
                                    if (!((Position)edge.Start).hasEnoughTokens(edge.Weight))
                                    {
                                        cFire = false;
                                        break;
                                    }
                                    else
                                    {
                                        touchedPositions.Add((Position)edge.Start, edge.Weight);
                                    }
                                }
                                else if (EdgeType.INHIBITOR.Equals(edge.EdgeType))
                                {
                                    if (((Position)edge.Start).hasEnoughTokens(edge.Weight))
                                    {
                                        cFire = false;
                                        break;
                                    }
                                    else
                                    {
                                        touchedPositions.Add((Position)edge.Start, edge.Weight);
                                    }
                                }
                                else if (EdgeType.RESET.Equals(edge.EdgeType))
                                {
                                    // always can fire
                                }
                            }
                        }
                        if (cFire)
                        {
                            cFire = checkCapacityLimit(transition, touchedPositions);
                            /*
                            // check CapacityLimit
                            List<AbstractEdge> outputs = this.getAllOutputEdge(transition);
                            if (outputs.Count > 0)
                            {
                                foreach (AbstractEdge edge in outputs)
                                {
                                    if (edge.End is Position)
                                    {
                                        int needCapacity = edge.Weight;
                                        Position tmpPos = (Position)edge.End;
                                        if (touchedPositions.ContainsKey(tmpPos))
                                        {
                                            needCapacity -= touchedPositions[tmpPos];
                                        }
                                        if (tmpPos.isCapcityLimitInjured(needCapacity))
                                        {
                                            cFire = false;
                                        }
                                    }
                                }
                            }
                            */
                            if (cFire)
                            {
                                if (TransitionType.SINK.Equals(transition.TransitionType))
                                {
                                    canFire.Add(transition);
                                }
                                else
                                {
                                    List<AbstractEdge> outputs2 = this.getAllOutputEdge(transition);
                                    if (outputs2.Count > 0)
                                    {
                                        canFire.Add(transition);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return canFire;
        }

        protected StateVector addOrFetchStateToOrFromHierarchy(StateVector stateVector)
        {
            StateVector findStateVector = this.stateHierarchy.find(stateVector);
            if (findStateVector == null)
            {
                this.stateHierarchy.addState(stateVector);
                findStateVector = stateVector;
            }
            return findStateVector;
        }

        protected void saveState(string name, StateVector stateVector)
        {
            StateVector findStateVector = null;
            foreach (StateVector sv in this.stateMatrix)
            {
                if (sv.Equals(stateVector))
                {
                    findStateVector = sv;
                    break;
                }
            }
            if (findStateVector == null)
            {
                findStateVector = this.addOrFetchStateToOrFromHierarchy(stateVector);
                if (this.actualStateVector != null)
                {
                    this.checkHandler(this.actualStateVector, EventType.PREACTIVATE); // BEFOREDEACTIVATE
                    this.stateHierarchy.addEdge(this.actualStateVector, findStateVector);
                }
                this.stateMatrix.Add(findStateVector);
                this.actualStateVector = findStateVector;
                findStateVector.Statistics.add(1);
                this.checkHandler(this.actualStateVector, EventType.POSTACTIVATE);
            }
            else
            {
                if (this.actualStateVector != null)
                {
                    this.checkHandler(this.actualStateVector, EventType.PREACTIVATE); // BEFOREDEACTIVATE
                    this.stateHierarchy.addEdge(this.actualStateVector, findStateVector);
                }
                this.actualStateVector = findStateVector;
                findStateVector.Statistics.add(1);
                this.checkHandler(this.actualStateVector, EventType.POSTACTIVATE);
            }
        }

        protected StateVector getNewStateVector(string name)
        {
            if ((name == null) || ("".Equals(name)))
            {
                name = this.statePrefix + (++this.stateGenNumber).ToString();
            }
            return new StateVector(name, this.unidGenNumber++, this);
        }

        private void cutStateMatrix(StateVector last)
        {
            List<StateVector> newStateMatrix = new List<StateVector>();
            foreach (StateVector stateVector in this.stateMatrix)
            {
                if (stateVector.Equals(last))
                {
                    break;
                }
                newStateMatrix.Add(stateVector);
            }
            newStateMatrix.Add(last);
            this.stateMatrix = newStateMatrix;
        }

        private List<AbstractEdge> getAllInputEdge(Transition transition)
        {
            List<AbstractEdge> inputs = new List<AbstractEdge>();
            foreach (AbstractEdge edge in this.Edges)
            {
                if (transition.Equals(edge.End))
                {
                    inputs.Add(edge);
                }
            }
            return inputs;
        }

        private List<AbstractEdge> getAllOutputEdge(Transition transition)
        {
            List<AbstractEdge> outputs = new List<AbstractEdge>();
            foreach (AbstractEdge edge in this.Edges)
            {
                if (transition.Equals(edge.Start))
                {
                    outputs.Add(edge);
                }
            }
            return outputs;
        }

    }
}
