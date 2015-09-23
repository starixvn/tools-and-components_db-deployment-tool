namespace DbDeployer.Core
{
    using System;

    public class ServerIpSetting
    {
        public ServerIpSetting() 
        { 
        }

        public ServerIpSetting(string serverName, string dbName, string ip, string ipXpre, string ipXtest, string ipRelease)
        {
            UpdateIPSetting(serverName, dbName, ip, ipXpre, ipXtest, ipRelease);
        }

        public string ServerName 
        { 
            get; 
            set; 
        }

        public string DBName
        {
            get;
            set;
        }

        public string Ip 
        { 
            get; 
            set; 
        }

        public string IpXpre 
        { 
            get; 
            set; 
        }

        public string IpXtest 
        { 
            get; 
            set; 
        }

        public string IpUAT1
        {
            get
            {
                return IpXpre;
            }
        }

        public string IpUAT2
        {
            get
            {
                return IpXpre;
            }
        }

        public string IpUAT3
        {
            get
            {
                return IpXpre;
            }
        }

        public string IpUAT4
        {
            get
            {
                return IpXpre;
            }
        }

        public string IpRelease 
        { 
            get; 
            set; 
        }

        public void UpdateIPSetting(string serverName, string dbName, string ip, string ipXpre, string ipXtest, string ipRelease)
        {
            this.ServerName = serverName;
            this.DBName = dbName;
            this.Ip = ip;
            this.IpXpre = ipXpre;
            this.IpXtest = ipXtest;
            this.IpRelease = ipRelease;
        }

        //public void UpdateIpWithSign(string sign, string value)
        //{
        //    switch (sign)
        //    {
        //        case "":
        //            this.Ip = value;
        //            break;
        //        case "db":
        //            this.DBName = value;
        //            break;
        //        case "xpre":
        //            this.IpXpre = value;
        //            break;
        //        case "xtest":
        //            this.IpXtest = value;
        //            break;
        //        case "release":
        //            this.IpRelease = value;
        //            break;
        //    }
        //}

        public string GetIpByPhase(Phases phase)
        {
            switch (phase)
            {
                case Phases.PreRelease:
                    return this.IpXpre;
                case Phases.Test:
                    return this.IpXtest;
                case Phases.UAT1:
                    return this.IpUAT1;
                case Phases.UAT2:
                    return this.IpUAT2;
                case Phases.UAT3:
                    return this.IpUAT3;
                case Phases.UAT4:
                    return this.IpUAT4;
                case Phases.Release:
                    return this.IpRelease;
                default:
                    return string.Empty;
            }
        }
    }
}
