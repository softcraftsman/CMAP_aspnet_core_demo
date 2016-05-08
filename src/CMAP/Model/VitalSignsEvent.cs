using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMAP.Model
{
    // Vital Signs
    public class VitalSignsEvent : Event
    {
        internal const string EventClass = "vitalSigns";
        public VitalSigns VitalSigns { get; set; }
    }

    public class VitalSigns
    {
        public int Id { get; set; }

        // Reference to Event
        public int EventId { get; set; }

        //[JsonIgnore]
        public VitalSignsEvent Event { get; set; }

        // Vital Signs
        public int Height { get; set; }             // Centimeters
        public int Weight { get; set; }             // Grams
        public float Temperature { get; set; }      // Celcius
        public int PulseRate { get; set; }
        public int RespirationRate { get; set; }
        public int SystolicPressure { get; set; }   // mm Hg
        public int DiastolicPressure { get; set; }
    }

}
