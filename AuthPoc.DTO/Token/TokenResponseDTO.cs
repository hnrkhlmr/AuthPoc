﻿using System;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace AuthPoc.DTO.Token
{
    public class TokenResponseDTO
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }
    }
}
