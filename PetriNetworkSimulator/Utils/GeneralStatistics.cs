using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNetworkSimulator.Utils
{
    public class GeneralStatistics
    {

        private int count;
        private int value;
        private int minimum;
        private int maximum;

        public double Average
        {
            get {
                double ret = 0;
                if (this.count > 0)
                {
                    ret = (double)this.value / (double)this.count;
                }
                return ret;
            }
        }

        public int Minimum
        {
            get { return this.minimum; }
        }

        public int Maximum
        {
            get { return this.maximum; }
        }

        public int Count
        {
            get { return this.count; }
        }

        public int Value
        {
            get { return this.value; }
        }

        public GeneralStatistics()
        {
            this.init();
        }

        public void init( int initValue = 0 )
        {
            this.value = initValue;
            this.count = 0;
            this.minimum = initValue;
            this.maximum = initValue;
        }

        public void add(int number)
        {
            if (this.minimum > number)
            {
                this.minimum = number;
            }
            if (this.maximum < number)
            {
                this.maximum = number;
            }
            this.value += number;
            this.count++;
        }

        public override string ToString()
        {
            return "value: " + this.value + " count: " + this.count + " avg: " + this.Average;
        }

    }
}
