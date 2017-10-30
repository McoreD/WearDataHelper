using System;

namespace WearDataHelper
{
    public class PumpAttributes
    {
        public string PartUniqueID { get; set; }
        public string WorkOrderNumber { get; set; }
        public string ResidualLife { get; set; }
        public string PumpServiceLife { get; set; }
        public string Notes { get; set; }
        public DateTime DateOverhaul { get; set; }

        internal string ImageFilePath { get; set; }
    }
}