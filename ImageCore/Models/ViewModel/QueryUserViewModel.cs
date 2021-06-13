using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageCore.Models.ViewModel
{
    public class QueryUserViewModel
    {
        [JsonProperty("UserIds")]
        public List<string> UserIds = new List<string>();
        [JsonProperty("UserNames")]
        public List<string> Usernames = new List<string>();
    }
}