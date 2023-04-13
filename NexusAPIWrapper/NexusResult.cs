using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NexusAPIWrapper
{
    public class NexusResult
    {
        public HttpStatusCode httpStatusCode { get; private set; }
        public String httpStatusText { get; private set; }
        public Object Result { get; private set; }
        public NexusResult(HttpStatusCode statuscode, String statustext, Object result)
        {
            httpStatusCode = statuscode;
            httpStatusText = statustext;
            Result = result;
        }
    }
}
