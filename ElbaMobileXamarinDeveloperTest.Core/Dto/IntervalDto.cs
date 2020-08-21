using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
