using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Origin.YMC.Web.Services.Api.Models
{
    public class AccessTokenViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }

        [JsonProperty(".issued")]
        public DateTimeOffset? Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTimeOffset? Expires { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}