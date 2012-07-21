using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using PetriNetworkSimulator.Utils;

namespace PetriNetworkSimulator.Entities.Enums
{
    public class Language : CustomEnum
    {

        public static readonly Language HUNGARIAN = new Language("Magyar", "HUNGARIAN", new CultureInfo("hu-HU"));
        public static readonly Language ENGLISH = new Language("English", "ENGLISH", new CultureInfo("en-US"));
        
        private readonly CultureInfo culture;

        private static List<Language> values;

        public CultureInfo Culture
        {
            get { return this.culture; }
        }

        public static Language[] Values
        {
            get
            {
                return Language.values.ToArray();
            }
        }

        private Language(string name, string value, CultureInfo culture) : base(name, value)
        {
            this.culture = culture;
            Language.addNewItem(this);
        }

        public static Language getDefault()
        {
            return Language.ENGLISH;
        }

        public static Language getEnumByCultureInfo(CultureInfo culture)
        {
            Language ret = Language.getDefault();
            switch (culture.Name)
            {
                case "hu-HU":
                    ret = Language.HUNGARIAN;
                    break;
                case "en-US":
                    ret = Language.ENGLISH;
                    break;
            }
            return ret;
        }

        public static Language getEnumByValue(string value)
        {
            Language ret = Language.getDefault();
            Language[] items = Language.Values;
            bool find = false;
            int i = 0;
            while ((i < items.Length) && (!find))
            {
                if (items[i].value.Equals(value.ToUpper()))
                {
                    ret = items[i];
                    find = true;
                }
                ++i;
            }
            return ret;
        }

        private static void addNewItem(Language item)
        {
            if (Language.values == null)
            {
                Language.values = new List<Language>();
            }
            Language.values.Add(item);
        }

    }
}
