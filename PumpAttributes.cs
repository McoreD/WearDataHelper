using System;
using System.ComponentModel;

namespace WearDataHelper
{
    public class PumpAttributes
    {
        [Browsable(false)]
        public string PartUniqueID { get; set; }

        [Browsable(false)]
        public string WorkOrderNumber { get; set; }

        [Description("Residual Life in hours")]
        public string ResidualLife { get; set; }

        public string PumpServiceLife { get; set; }
        public string Notes { get; set; }

        [Browsable(false)]
        public string DateOverhaul { get; set; }

        internal string PartName { get; set; }
        internal string ImageFilePath { get; set; }
    }
}