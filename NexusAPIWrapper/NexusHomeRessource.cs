using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper
{
    public class NexusHomeRessource
    {
        string url;
        string tokenObject;
        const string homeRessourceEndpointURL = "/api/core/mobile/ringsted/v2/";

        public NexusHomeRessource(string url, string tokenObject)
        {
            this.url = url;
            this.tokenObject = tokenObject;
        }


    }

    
}
