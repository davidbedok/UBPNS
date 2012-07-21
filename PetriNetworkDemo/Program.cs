using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PetriNetworkLibrary.Model.Network;
using PetriNetworkLibrary.Event;
using PetriNetworkLibrary.Model.Base;
using PetriNetworkLibrary.Utility;
using PetriNetworkLibrary.Model.NetworkItem;
using PetriNetworkLibrary.Model.Edge;

namespace PetriNetworkDemo
{
    public class Program
    {
        private static void eventHandler(AbstractEventDrivenItem item, EventType eventType)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("#### eventHandler: (item: " + item.Name + ", eventType: "+eventType+")");
            if (item is Position)
            {
                Position position = (Position)item;
                sb.Append(" token count: " + position.TokenCount);
            }
            System.Console.WriteLine(sb.ToString());
        }

        private static void eventHandler2(AbstractEventDrivenItem item, EventType eventType)
        {
            System.Console.Write(" (eh) ");
        }

        private static void Main(string[] args)
        {
            System.Console.WriteLine("# PetriNetworkDemo");
            Random rand = new Random();
            PetriNetwork network = PetriNetwork.openFromXml(rand, @"networks\Demo.pn.xml");

            if (network != null)
            {
                // System.Console.WriteLine(network);

                try
                {
                    network.bindPetriEvent("alma", new PetriHandler(eventHandler));
                }
                catch (ArgumentException e)
                {
                    System.Console.WriteLine(e.Message);
                }

                System.Console.WriteLine("\nList of events: ");
                List<String> listOfEvents = network.EventsName;
                foreach (String item in listOfEvents)
                {
                    System.Console.Write(item + " ");
                    network.bindPetriEvent(item, new PetriHandler(eventHandler2));
                    network.bindPetriEvent(item, new PetriHandler(eventHandler));
                }

                System.Console.WriteLine("\nList of states: ");
                List<String> listOfStates = network.StatesName;
                foreach (String item in listOfStates)
                {
                    System.Console.Write(item + " ");
                }
                System.Console.WriteLine("\n");

                try
                {
                    network.setStartState("alma");
                }
                catch (ArgumentException e)
                {
                    System.Console.WriteLine(e.Message);
                }

                network.setStartState("m2");

                FireEvent fireEvent = FireEvent.INITFIRE;
                FireReturn fireReturn = null;
                while (!FireEvent.DEADLOCK.Equals(fireEvent))
                {
                    fireReturn = network.fire();
                    System.Console.WriteLine(fireReturn);
                    // Thread.Sleep(1000);
                    fireEvent = fireReturn.FireEvent;
                }

                System.Console.WriteLine(network);
            }
            else
            {
                System.Console.WriteLine("Network is null.");
            }
            
        }
    }
}
