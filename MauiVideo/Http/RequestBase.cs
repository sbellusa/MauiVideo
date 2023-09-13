using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVideo.Http
{
    public class RequestBase
    {
        public string Url { get; set; }
        public RequestMethods Method { get; set; }
        public object RequestContent { get; set; }
        public Dictionary<string, string> QueryParamsDictionary { get; set; }
        public Type ResponseType { get; set; }
        public ResponseBase Response { get; set; }
    }


    public class RequestBase<T> : RequestBase where T : ResponseBase
    {
        public RequestBase()
        {
            ResponseType = typeof(T);
        }
        public RequestBase(RequestMethods method, string url)
        {
            ResponseType = typeof(T);
            Url = url;
            Method = method;
        }
    }
}
