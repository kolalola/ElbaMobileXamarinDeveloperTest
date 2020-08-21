using Newtonsoft.Json;
using System;

namespace ElbaMobileXamarinDeveloperTest.Core.Dto
{
    public class IntervalDto
    {
        [JsonProperty("start")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end")]
        public DateTime EndDate { get; set; }
    }
}
