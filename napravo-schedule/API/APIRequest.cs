﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.API
{
    internal class APIRequest
    {
        public IDictionary<string, string> args;
        public string urlMethod;

        public bool IsValid() => !string.IsNullOrEmpty(urlMethod);
        public bool HasArgs() => args != null;

        public APIRequest(string url)
        {
            this.urlMethod = url;
            args = new Dictionary<string,string>();
        }
        public APIRequest(string url, IDictionary<string,string>? args) : this(url) =>
            this.args = args ?? new Dictionary<string,string>();
        public APIRequest(string url, KeyValuePair<string, string> arg) : this(url) =>
            this.args = new Dictionary<string, string>();
    }
}