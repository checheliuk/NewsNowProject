using System;
using System.ComponentModel.DataAnnotations;

namespace NewsNowProject.Domain.Model
{
    public class Proxy
    {
        [Key]
        public int ProxyID { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }
}
