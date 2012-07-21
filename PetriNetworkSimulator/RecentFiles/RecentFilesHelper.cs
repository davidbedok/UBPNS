using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using PetriNetworkSimulator.Exceptions;

namespace PetriNetworkSimulator.RecentFiles
{
    public class RecentFilesHelper
    {
        private const string XML_RECENT_FILES = @"recentfiles.xml";

        private PetriNetworkFiles networkFiles;

        public List<RecentFile> RecentFiles
        {
            get {
                List<RecentFile> ret = new List<RecentFile>();
                if (this.networkFiles != null)
                {
                    RecentFile[] pnrf = this.networkFiles.Items;
                    if (pnrf != null)
                    {
                        ret = pnrf.ToList();
                    }
                }
                return ret;
            }
        }

        public RecentFilesHelper()
        {
            this.loadFromFile(RecentFilesHelper.XML_RECENT_FILES);
            this.removeNotExistsItem();

        }

        private void loadFromFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PetriNetworkFiles));
                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    XmlReader reader = new XmlTextReader(fs);
                    this.networkFiles = (PetriNetworkFiles)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                this.networkFiles = null;
            }
        }

        private void removeNotExistsItem()
        {
            if (this.networkFiles != null)
            {
                List<RecentFile> recentFiles = this.RecentFiles;
                List<RecentFile> existsRecentFiles = new List<RecentFile>();
                foreach (RecentFile rf in recentFiles)
                {
                    if (File.Exists(rf.FileName))
                    {
                        existsRecentFiles.Add(rf);
                    }
                }
                this.networkFiles.Items = existsRecentFiles.ToArray(); 
            }
        }

        public RecentFile find(List<RecentFile> recentFiles, string fileName)
        {
            RecentFile ret = null;
            foreach (RecentFile rf in recentFiles)
            {
                if (rf.FileName.Equals(fileName))
                {
                    ret = rf;
                    break;
                }
            }
            return ret;
        }

        public void addRecentFile(RecentFile recentFile)
        {
            if ((this.networkFiles != null) && ( recentFile != null ) )
            {
                List<RecentFile> recentFiles = this.RecentFiles;
                RecentFile rf = this.find(recentFiles, recentFile.FileName);
                bool find = false;
                if (rf != null)
                {
                    recentFiles.Remove(rf);
                    find = true;
                }
                if (((!recentFiles.Contains(recentFile)) && (rf == null) ) || (find))
                {
                    recentFiles.Add(recentFile);
                    this.networkFiles.Items = recentFiles.ToArray();
                }
            }
        }

        public void saveToFile()
        {
            this.saveToFile(RecentFilesHelper.XML_RECENT_FILES);
        }

        private void saveToFile(string fileName)
        {
            if (this.networkFiles != null)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PetriNetworkFiles));
                    XmlWriter writer = new XmlTextWriter(fileName, Encoding.UTF8);
                    serializer.Serialize(writer, this.networkFiles);
                }
                catch (Exception e)
                {
                    throw new SimApplicationException("Cannot save recent files.",e);
                }
            }
        }

    }
}
