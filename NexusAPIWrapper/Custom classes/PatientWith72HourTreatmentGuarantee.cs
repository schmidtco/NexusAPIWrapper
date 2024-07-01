using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper.Custom_classes
{
    internal class PatientWith72HourTreatmentGuarantee
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime TimeOfDischarge { get; set; }
    }
}
