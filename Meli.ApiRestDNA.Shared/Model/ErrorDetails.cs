using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Meli.ApiRestDNA.Shared.Model
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
