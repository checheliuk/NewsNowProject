using System;

namespace NewsNowProject.Domain.ViewModel
{
    public class ProxyListViewModel
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string protocol { get; set; }
        public string anonymity { get; set; }
        public DateTime lastTested { get; set; }
        public bool allowsRefererHeader { get; set; }
        public bool allowsUserAgentHeader { get; set; }
        public bool allowsCustomHeaders { get; set; }
        public bool allowsCookies { get; set; }
        public bool allowsPost { get; set; }
        public bool allowsHttps { get; set; }
        public string country { get; set; }
        public double connectTime { get; set; }
        public double downloadSpeed { get; set; }
        public double secondsToFirstByte { get; set; }
        public double uptime { get; set; }
    }
}
