using System;
using System.Collections.Generic;

namespace MijnSauna.Common.DataTransferObjects.Samples
{
    public class GetSamplesForSessionResponse
    {
        public Guid SessionId { get; set; }
        public List<SampleForSession> Samples { get; set; }
    }
}