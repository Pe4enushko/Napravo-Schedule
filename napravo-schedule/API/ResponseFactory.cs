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
        static int timeout = 30;
        static MethodInfo[] methodsInfo;
        static ResponseFactory()
        {
            methodsInfo = typeof(ResponseFactory).GetMethods();
        }

        #region Requests
        [Request("Notifications/all")]
        public static async Task<Notification[]> GetAllNotifications(int studentId)
        {
            var arg = new KeyValuePair<string, string>(nameof(studentId), studentId.ToString());
            return await DoRequest<Notification[]>(arg);
        }
        [Request("Notifications/unchecked")]
        public static async Task<Notification[]> GetUncheckedNotifications(int studentId)
        {
            var arg = new KeyValuePair<string, string>(nameof(studentId), studentId.ToString());
            return await DoRequest<Notification[]>(arg);
        }
        [Request("Schedule")]
        public static async Task<ClassReadable[]> GetClassesReadable(string groupTitle)
        {
            var arg = new KeyValuePair<string, string>(nameof(groupTitle), groupTitle);
            return await DoRequest<ClassReadable[]>(arg);
        }
        [Request("Info/Group")]
        public static async Task<Group> GetGroup(string title)
        {
            var arg = new KeyValuePair<string, string>(nameof(title), title);
            return await DoRequest<Group>(arg);
        }
        [Request("Info/GroupTitles")]
        public static async Task<string[]> GetGroupTitles()
        {
            return await DoRequest<string[]>();
        }
        #endregion
        #region Internal
        static string GetMethodRequestMethod(string methodName)
        {
            var attr = (RequestAttribute)methodsInfo
                .First(a => a.Name == methodName)
                .GetCustomAttribute(typeof(RequestAttribute));

            if (attr == null)
                throw new CustomAttributeFormatException(
$"Method probably got no Request attribute stating api request method{methodName}");

            return attr.Method;
        }
        
        static async Task<T> DoRequest<T>([CallerMemberName] string caller = "")
        {
            var response = await new APIWorker<T>(timeout)
                .GetAsync(new APIRequest(GetMethodRequestMethod(caller)));
            return response;
        }
        static async Task<T> DoRequest<T>(KeyValuePair<string, string> arg, [CallerMemberName] string caller = "")
        {
            var response = await new APIWorker<T>(timeout)
                .GetAsync(new APIRequest(GetMethodRequestMethod(caller) ,arg));
            return response;
        }

        static async Task<T> DoRequest<T>(Dictionary<string, string> args, [CallerMemberName] string caller = "")
        {
            var response = await new APIWorker<T>(timeout)
                .GetAsync(new APIRequest(GetMethodRequestMethod(caller), args));
            return response;
        }
        #endregion
    }

    /// <summary>
    /// Use this attribute when method maps 2 requests
    /// </summary>
    class RequestAttribute : Attribute
    {
        public string Method { get; set; }

        public RequestAttribute(string method)
        {
            this.Method = method;
        }
    }
}
