using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetriNetworkLibrary.Model.Network;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Model.TokenPlayer;
using System.Threading;

namespace HorseraceDemo
{
    public partial class HorseRaceForm : Form
    {
        private PetriNetwork network;
        private Random rand;

        [ThreadStatic]
        private List<Token> resultThread;

        public HorseRaceForm()
        {
            InitializeComponent();
            this.rand = new Random();
            
            this.network = PetriNetwork.openFromXml(this.rand, @"network\Horserace.pn.xml");
            this.network.bindPetriEvent("goal", new PetriHandler(eventHandler));
        }

        private void eventHandler(AbstractEventDrivenItem item, EventType eventType)
        {
            if (item is Position)
            {
                Position position = (Position)item;
                List<Token> tokens = position.Tokens;
                if ( (tokens != null) && ( tokens.Count == 1 ) )
                {
                    this.lbResult.Items.Add(tokens[0]);
                }
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            this.lbResult.Items.Clear();
            this.network.setStartState("start");
            FireEvent fireEvent = FireEvent.INITFIRE;
            FireReturn fireReturn = null;
            while (!FireEvent.DEADLOCK.Equals(fireEvent))
            {
                fireReturn = this.network.fire();
                Thread.Sleep(200);
                fireEvent = fireReturn.FireEvent;
            }
        }

        private void bStartThread_Click(object sender, EventArgs e)
        {
            if (!this.race.IsBusy)
            {
                this.network.setStartState("start");
                this.bStartThread.Enabled = false;
                this.lbResultThread.Items.Clear();
                this.race.RunWorkerAsync();
            }
        }

        private void threadEventHandler(AbstractEventDrivenItem item, EventType eventType)
        {
            if (item is Position)
            {
                Position position = (Position)item;
                List<Token> tokens = position.Tokens;
                if ((tokens != null) && (tokens.Count == 1))
                {
                    this.resultThread.Add(tokens[0]);
                }
            }
        }

        private void race_DoWork(object sender, DoWorkEventArgs e)
        {
            PetriNetwork network = PetriNetwork.openFromXml(this.rand, @"network\Horserace.pn.xml");
            network.bindPetriEvent("goal", new PetriHandler(threadEventHandler));
            this.resultThread = new List<Token>();
            int cycle = 0;
            FireEvent fireEvent = FireEvent.INITFIRE;
            FireReturn fireReturn = null;
            while (!FireEvent.DEADLOCK.Equals(fireEvent))
            {
                fireReturn = network.fire();
                if (this.race.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                cycle++;
                Thread.Sleep(200);
                this.race.ReportProgress(cycle);
                fireEvent = fireReturn.FireEvent;
            }
        }

        private void race_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //                          
        }

        private void race_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Token item in this.resultThread)
            {
                this.lbResultThread.Items.Add(item);
            }
            this.bStartThread.Enabled = true;
        }

    }
}
