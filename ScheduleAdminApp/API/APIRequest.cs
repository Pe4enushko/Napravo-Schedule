using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAdminApp.API
{
    /// <summary>
    /// API request versatile class. url method will be applied to https://pe4enushko.ddns.net/api
    /// </summary>
    internal class APIRequest
    {
        public IDictionary<string, string> args;
        public string requestUrl;

        public bool IsValid() => !string.IsNullOrEmpty(requestUrl);
        public bool HasArgs() => args != null;

        public APIRequest(string url)
        {
            if (url[0] != '/')
                url = '/' + url;

            this.requestUrl = "https://pe4enushko.ddns.net/api" + url;
            args = new Dictionary<string,string>();
        }
        public APIRequest(string url, IDictionary<string, string> args) : this(url) =>
            this.args = args;
        public APIRequest(string url, KeyValuePair<string, string> arg) : this(url) =>
            this.args.Add(arg);
        
            
    }
}
