using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using PetriNetworkSimulator.Entities.Enums;
using System.Threading;
using System.Resources;

namespace PetriNetworkSimulator.Utils
{
    public delegate void CultureHandler();

    public class CultureHelper
    {
        private static CultureHelper instance;

        private Language lang;
        private ResourceManager localizationResourcesManager;
        public event CultureHandler changeCulture;

        public ResourceManager RM
        {
            get { return this.localizationResourcesManager; }
        }

        public Language ActualLanguage
        {
            get { return this.lang; }
            set { 
                if ( value != null ) {
                    this.lang = value;
                    Thread.CurrentThread.CurrentUICulture = this.lang.Culture;
                    if (this.changeCulture != null)
                    {
                        this.changeCulture();
                    }
                }
            }
        }

        private CultureHelper()
        {
            this.localizationResourcesManager = new ResourceManager("PetriNetworkSimulator.Localization.Localization", System.Reflection.Assembly.GetExecutingAssembly());

            this.lang = Language.getEnumByCultureInfo(Thread.CurrentThread.CurrentUICulture);
        }

        public static CultureHelper getInstance()
        {
            if (CultureHelper.instance == null)
            {
                CultureHelper.instance = new CultureHelper();
            }
            return CultureHelper.instance;
        }

    }
}
