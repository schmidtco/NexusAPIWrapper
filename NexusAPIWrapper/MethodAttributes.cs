using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper
{
    internal class MethodAttributes
    {
        public class DanskMetodeAttribute : Attribute
        {
            public DanskMetodeAttribute() { }
            public DanskMetodeAttribute(string beskrivelse) { }
        }

        public class EnglishMethodAttribute : Attribute
        {
            public EnglishMethodAttribute() { }
            public EnglishMethodAttribute(string description) { }
        }
    }
}
