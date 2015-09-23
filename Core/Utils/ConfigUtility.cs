namespace DbDeployer.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class ConfigUtility
    {
        private string _filePath;

        private const string _serverNameXML = "ServerName";
        private const string _dbNameXML = "DatabaseName";
        private const string _ipXML = "IP";
        private const string _ipXpreXML = "IP_Xpre";
        private const string _ipXtestXML = "IP_Xtest";
        private const string _ipReleaseXML = "IP_Release";

        public ConfigUtility()
        {
            this._filePath = AppDomain.CurrentDomain.BaseDirectory + "IPSetting.xml";

            if (!File.Exists(this._filePath))
            {
                using (FileStream fs = File.Create(this._filePath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xmlDoc.AppendChild(docNode);

                    XmlNode configurationNode = xmlDoc.CreateElement("configuration");
                    xmlDoc.AppendChild(configurationNode);

                    XmlNode appSettingsNode = xmlDoc.CreateElement("appSettings");
                    configurationNode.AppendChild(appSettingsNode);

                    XmlNode ipSettingsNode = xmlDoc.CreateElement("IpSettings");
                    configurationNode.AppendChild(ipSettingsNode);

                    xmlDoc.Save(fs);
                    fs.Close();
                }
            }

        }

        public string GetKey(string serverName, string dbName)
        {
            return serverName + "-" + dbName.ToUpper();
        }

        public bool AddSetting(string serverName, string dbName, string ip, string ipXpre, string ipXtest, string ipRelease)
        {
            if (!string.IsNullOrEmpty(serverName)
                    && !string.IsNullOrEmpty(dbName)
                    && !string.IsNullOrEmpty(ip)
                    && !string.IsNullOrEmpty(ipXpre)
                    && !string.IsNullOrEmpty(ipXtest)
                    && !string.IsNullOrEmpty(ipRelease)
                    )
            {
                var key = GetKey(serverName, dbName);
                if (this.SettingExists(key))
                {
                    this.DeleteSetting(key);
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(this._filePath);

                XmlNode ipSettingsNode = xmlDoc.SelectSingleNode("configuration/IpSettings");

                // Quinn edit for new IPSetting.xml config
                XmlNode ipSettingNode = xmlDoc.CreateNode(XmlNodeType.Element, "IpSetting", null);

                // Server name
                XmlNode serverNameNode = xmlDoc.CreateNode(XmlNodeType.Element, _serverNameXML, null);
                serverNameNode.InnerText = serverName;
                ipSettingNode.AppendChild(serverNameNode);
                // Db name
                XmlNode dbNameNode = xmlDoc.CreateNode(XmlNodeType.Element, _dbNameXML, null);
                dbNameNode.InnerText = dbName;
                ipSettingNode.AppendChild(dbNameNode);
                // IP
                XmlNode ipNode = xmlDoc.CreateNode(XmlNodeType.Element, _ipXML, null);
                ipNode.InnerText = ip;
                ipSettingNode.AppendChild(ipNode);
                // IP Re-release
                XmlNode ipXpreNode = xmlDoc.CreateNode(XmlNodeType.Element, _ipXpreXML, null);
                ipXpreNode.InnerText = ipXpre;
                ipSettingNode.AppendChild(ipXpreNode);
                // IP Test
                XmlNode ipXTestNode = xmlDoc.CreateNode(XmlNodeType.Element, _ipXtestXML, null);
                ipXTestNode.InnerText = ipXtest;
                ipSettingNode.AppendChild(ipXTestNode);
                // IP Release
                XmlNode ipReleaseNode = xmlDoc.CreateNode(XmlNodeType.Element, _ipReleaseXML, null);
                ipReleaseNode.InnerText = ipRelease;
                ipSettingNode.AppendChild(ipReleaseNode);

                ipSettingsNode.AppendChild(ipSettingNode);

                xmlDoc.Save(this._filePath);
            }

            return true;
        }

        public bool DeleteSetting(string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this._filePath);

            if (!this.SettingExists(key))
            {
                return false;
            }

            XmlNode ipSettingsNode = xmlDoc.SelectSingleNode("configuration/IpSettings");
            
            // Attempt to locate the requested setting.
            int nodeLength = ipSettingsNode.ChildNodes.Count - 1;
            for (int i = nodeLength; i >= 0; i--)
            {
                var serverName = ipSettingsNode.ChildNodes[i].SelectNodes(_serverNameXML)[0].InnerText;
                var dbName = ipSettingsNode.ChildNodes[i].SelectNodes(_dbNameXML)[0].InnerText;

                if (key == GetKey(serverName, dbName))
                {
                    ipSettingsNode.RemoveChild(ipSettingsNode.ChildNodes[i]);
                }
            }

            xmlDoc.Save(this._filePath);
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            return true;
        }

        public bool SettingExists(string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this._filePath);

            XmlNodeList ipSettingNode = xmlDoc.SelectNodes("configuration/IpSettings/IpSetting");
            // Attempt to locate the requested setting.
            foreach (XmlNode childNode in ipSettingNode)
            {
                var serverName = childNode.SelectNodes(_serverNameXML)[0].InnerText;
                var dbName = childNode.SelectNodes(_dbNameXML)[0].InnerText;

                if (key == GetKey(serverName, dbName))
                {
                    return true;
                }
            }

            return false;
        }

        public Dictionary<string, string> GetStandbyServers()
        {
            Dictionary<string, string> _imageServers = new Dictionary<string, string>();
            //load setting of image servers
            string[] imageServerArr = System.Configuration.ConfigurationManager.AppSettings["ImageServers"].Split(';');
            foreach (string imgs in imageServerArr)
            {
                if (imgs.Trim().Length > 0 && imgs.Contains("--"))
                {
                    _imageServers.Add(imgs.Substring(0, imgs.IndexOf("--")), imgs.Substring(imgs.IndexOf("--") + 2, imgs.Length - imgs.IndexOf("--") - 2));
                }
            }

            return _imageServers;
        }

        public Dictionary<string, ServerIpSetting> GetIPSettings()
        {
            var serverName = string.Empty;
            var dbName = string.Empty;
            var ip = string.Empty;
            var ipXpre = string.Empty;
            var ipXtest = string.Empty;
            var ipRelease = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(this._filePath);

            Dictionary<string, ServerIpSetting> ipSettingsDictionary = new Dictionary<string, ServerIpSetting>();

            foreach (XmlNode items in xmlDoc.SelectNodes(@"configuration/IpSettings/IpSetting"))
            {
                serverName = string.Empty;
                dbName = string.Empty;
                ip = string.Empty;
                ipXpre = string.Empty;
                ipXtest = string.Empty;
                ipRelease = string.Empty;

                foreach (XmlNode item in items.ChildNodes)
                {
                    switch (item.Name)
                    {
                        case _serverNameXML:
                            serverName = item.InnerText.ToUpper();
                            break;
                        case _dbNameXML:
                            dbName = item.InnerText;
                            break;
                        case _ipXML:
                            ip = item.InnerText;
                            break;
                        case _ipXpreXML:
                            ipXpre = item.InnerText;
                            break;
                        case _ipXtestXML:
                            ipXtest = item.InnerText;
                            break;
                        case _ipReleaseXML:
                            ipRelease = item.InnerText;
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(serverName)
                    && !string.IsNullOrEmpty(dbName)
                    && !string.IsNullOrEmpty(ip)
                    && !string.IsNullOrEmpty(ipXpre)
                    && !string.IsNullOrEmpty(ipXtest)
                    && !string.IsNullOrEmpty(ipRelease)
                    )
                {
                    var key = GetKey(serverName, dbName);
                    // check whether the key existed
                    if (ipSettingsDictionary.ContainsKey(key))
                    {
                        ipSettingsDictionary[key].UpdateIPSetting(serverName, dbName, ip, ipXpre, ipXtest, ipRelease);
                    }
                    else
                    {
                        ServerIpSetting serverIpSetting = new ServerIpSetting(serverName, dbName, ip, ipXpre, ipXtest, ipRelease);
                        ipSettingsDictionary.Add(key, serverIpSetting);
                    }
                }
            }

            return ipSettingsDictionary;
        }

        public void DeleteIPSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this._filePath);

            XmlNode ipSettingsNode = xmlDoc.SelectSingleNode("configuration/IpSettings");

            int nodeLength = ipSettingsNode.ChildNodes.Count - 1;
            for (int i = nodeLength; i >= 0; i--)
            {
                ipSettingsNode.RemoveChild(ipSettingsNode.ChildNodes[i]);
            }

            xmlDoc.Save(this._filePath);
        }
    }
}
