using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Meli.ApiRestDNA.Shared.Model
{
    public class ErrorDetails
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Support error code
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Error details
        /// </summary>
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
