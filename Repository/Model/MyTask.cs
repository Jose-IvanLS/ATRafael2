using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository.Model {
   public  class MyTask {

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "taskName")]
        public string TaskName { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string TaskTime { get; set; }

        [JsonProperty(PropertyName = "taskTo")]
        public string TaskTo { get; set; }

        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; } = "ATask";
    }
}
