

using System;
using static NewsNowProject.Domain.Model.EnumModel;

namespace NewsNowProject.Domain.Model
{
    public class Limit
    {
        public int LimitID { get; set; }
        public LimitType Type { get; set; }
        public DateTime Time { get; set; }
        public bool Status { get; set; } // true - active
        public string Value { get; set; }
    }
}
