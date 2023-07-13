using napravo_schedule.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace napravo_schedule.API
{
    public static class ResponseFactory
    {
        static Dictionary<Type, string> methodUrlMap = new();
        static MethodInfo[] methodsInfo;
        static ResponseFactory()
        {
            FillUrlMap();
            methodsInfo = typeof(ResponseFactory).GetMethods();
        }
        static void FillUrlMap()
        {
            methodUrlMap.Add(typeof(ClassReadable[]), "Schedule");
            methodUrlMap.Add(typeof(Group), "Info/Group");
        }
        public static async Task<ClassReadable[]> GetClassesReadable(string groupTitle)
        {
            var method = GetMethodUrl();
            var arg = new KeyValuePair<string, string>(nameof(groupTitle), groupTitle);
            return await DoRequest<ClassReadable[]>(method, arg);
        }
        public static async Task<Group> GetGroup(string title)
        {
            var method = GetMethodUrl();
            var arg = new KeyValuePair<string, string>(nameof(title), title);
            return await DoRequest<Group>(method, arg);
        }
        #region Internal
        static string GetMethodUrl([CallerMemberName] string caller = "")
        {
            return methodUrlMap[methodsInfo
                .First(a => a.Name == caller)
                .ReturnParameter.ParameterType];
        }
        static Type GetMethodReturnType(string methodName) => 
            methodsInfo.First(a => a.Name == methodName).ReturnType;
        
        static async Task<T> DoRequest<T>(string url, int timeout = 30)
        {
            var response = await new APIWorker<T>(timeout)
                .GetAsync(new APIRequest(url));
            return response;
        }

        static async Task<T> DoRequest<T>(string url, KeyValuePair<string, string> arg, int timeout = 30)
        {
            var response = await new APIWorker<T>(timeout)
                .GetAsync(new APIRequest(url, arg));
            return response;
        }

        static async Task<T> DoRequest<T>(string url, Dictionary<string, string> args, int timeout = 30)
        {
            var response = await new APIWorker<T>(timeout)
                .GetAsync(new APIRequest(url, args));
            return response;
        }
        #endregion
    }
}
