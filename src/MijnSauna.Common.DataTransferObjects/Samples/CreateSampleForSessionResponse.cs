using System;

namespace MijnSauna.Common.DataTransferObjects.Samples
{
    public class CreateSampleForSessionResponse
    {
        public Guid SampleId { get; set; }
        public Guid SessionId { get; set; }
    }
}